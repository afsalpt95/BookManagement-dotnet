namespace BookManagement.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public  string Email { get; set; }
        public required string Password { get; set; }
        public bool Status { get; set; } = true;

        public string? Interests { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
