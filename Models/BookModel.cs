using Microsoft.AspNetCore.Http.HttpResults;

namespace BookManagement.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public bool Status { get; set; } = true;
        public required int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public BookModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
      

    }
