namespace ESSmPrototype.Models
{
    public class Family : Employee
    {
        public string? LegalName { get; set; }
        public string? GuardianName { get; set; }
        public int? Age { get; set; }
        public string? Occupation { get; set; }
        public string? Contact { get; set; }
        public string? Relationship { get; set; }
        public string? Email { get; set; }


        public Family(string legalName, string guardianName, int age, string occupation, string contact, string relationship, string email) : base(legalName)
        {
            LegalName = legalName;
            GuardianName = guardianName;
            Age = age;
            Occupation = occupation;
            Contact = contact;
            Relationship = relationship;
            Email = email;
        }
    }
}
