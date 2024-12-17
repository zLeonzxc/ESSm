using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESSmAPI.Data;
using ESSmAPI.Models;

namespace ESSmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;

            if (!_context.Users.Any())
            {
                _context.Users.AddRange(
                    new User { Name = "admin", Username = "admin", Password = "password", Email = "admin@example.com", IsLoggedIn = false },
                    new User { Name = "John Smith", Username = "jsmith", Password = "jsmith123", Email = "johnsmith@example.com", IsLoggedIn = false }
                );
                _context.SaveChanges();
            }
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> FindAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> FindUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetUser", new { id = user.Id }, user);
            return CreatedAtAction(nameof(FindUser), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // LOGIN: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser([FromBody] UserDTO userDTO)
        {

            var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == userDTO.Username);
            if (user == null)
            {
                Console.WriteLine("Login attempt failed: Username [{0}] not found \n[ESSM1004]", userDTO.Username); // user does not exist
                return NotFound();
            }

            if (user.Password != userDTO.Password)
            {
                Console.WriteLine("Login attempt failed: Incorrect password for username [{0}] \n[ESSM1002]", userDTO.Username); // incorrect password for existing username
                return NotFound();
            }

            user.IsLoggedIn = true;
            await _context.SaveChangesAsync();

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("User [{0}] has logged in. \nLogin status: [{1}] \nTime:[{2:dd MMMM yyyy}, {2:t}]", user.Username, user.IsLoggedIn, DateTime.Now);
            Console.WriteLine(new string('-', 50));
            return Ok(user.Name);
        }

        // LOGOUT: api/Users/logout
        [HttpPost("logout")]
        public async Task<ActionResult<User>> LogoutUser([FromBody] string username)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == username);

                if (user == null)
                {
                    Console.WriteLine("User with username: [{0}] could not be found", username); // ESSM1004
                    return NotFound();
                }

                if (user.IsLoggedIn)
                {
                    user.IsLoggedIn = false;

                    await _context.SaveChangesAsync(); // Ensure changes are saved to the database

                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine("User [{0}] has logged out. \nLogin status: [{1}] \nTime:[{2:dd MMMM yyyy}, {2:t}]", user.Username, user.IsLoggedIn, DateTime.Now);
                    Console.WriteLine(new string('-', 50));
                    return Ok(user.Name);
                }
                else
                {
                    // should be unreachable code
                    // scenario: api server down -> all users (isLoggedIn) status set to false
                    Console.WriteLine("User with username: [{0}] is not logged in", username);
                    return BadRequest("User is not logged in");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: {0}", ex.Message); // ESSM1004
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
