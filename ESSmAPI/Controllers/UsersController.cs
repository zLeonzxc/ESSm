using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ESSmAPI.Data;
using ESSmAPI.Models;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace ESSmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private string decodedPassword;
        private readonly UserContext _context;
        private readonly CompanyContext _companyContext;
        private User? _currentUser;

        public UsersController(UserContext context, CompanyContext companyContext)
        {
            _context = context;
            _companyContext = companyContext;
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
            // username verification
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == userDTO.Username);
            if (user == null)
            {
                Console.WriteLine("Login attempt failed: Username [{0}] not found \n[ESSM1004]", userDTO.Username); // user does not exist
                return NotFound();
            }

            // password verification
            // decode password from byte[] to string
            var storedPw = System.Text.Encoding.UTF8.GetString(user.Pw);

            if (userDTO.Password != storedPw)
            {
                Console.WriteLine("Login attempt failed: Incorrect password for username [{0}] \n[ESSM1002]", userDTO.Username); // incorrect password for existing username
                return NotFound();
            }

            // check if company code exists
            var company = await _companyContext.Companies.FirstOrDefaultAsync(c => c.CompanyCode == userDTO.CompanyCode);

            if (company == null)
            {
                return BadRequest("Invalid company code");
            }

            if (string.IsNullOrEmpty(user.CompanyCode) || user.CompanyCode != company.CompanyCode)
            {
                user.CompanyCode = company.CompanyCode ?? string.Empty;
            }
            else
            {
                Console.WriteLine("Invalid company code");
            }

            user.IsLoggedIn = true;
            _currentUser = user;
            await _context.SaveChangesAsync();

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("User [{0}] from company [{1}] has logged in. \nLogin status: [{2}] \nTime:[{3:dd MMMM yyyy}, {3:t}]", user.Username, user.CompanyCode, user.IsLoggedIn, DateTime.Now);
            Console.WriteLine(new string('-', 50));
            return Ok(user.Username);
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

                var comCode = user.CompanyCode;

                if (user.IsLoggedIn)
                {
                    user.IsLoggedIn = false;
                    _currentUser = null;
                    await _context.SaveChangesAsync(); // Ensure changes are saved to the database

                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine("User [{0}] from company [{1}] has logged out. \nLogin status: [{2}] \nTime:[{3:dd MMMM yyyy}, {3:t}]", user.Username, comCode, user.IsLoggedIn, DateTime.Now);
                    Console.WriteLine(new string('-', 50));
                    return Ok(user.Username);
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
