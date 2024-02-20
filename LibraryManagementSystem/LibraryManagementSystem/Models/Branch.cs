using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Branch
    {
        [Required(ErrorMessage = "Book ID is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Book Name is required")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Branch Name is required")]
        public string BranchName { get; set; }
    }
}
