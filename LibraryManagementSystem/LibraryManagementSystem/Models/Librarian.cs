using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Librarian
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
