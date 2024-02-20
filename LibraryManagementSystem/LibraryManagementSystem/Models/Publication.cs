using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Publication
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book Name is required")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Book ID is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Author Name is required")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Publisher Name is required")]
        public string PublisherName { get; set; }
    }
}
