namespace MyLibrary.Domain
{
    public class Customer
    {
        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public int Id { get; init; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }

        public DateTime CreatedDate { get; init; } = DateTime.UtcNow;

    }
}
