using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class IssuedBook
    {
        public Publication Book { get; set; }
        public Student Student { get; set; }

        [Display(Name = "Issue Date")]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
    }

    public class IssueBookViewModel
    {
        [Required(ErrorMessage = "Book Id is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Book Name is required")]
        public string BookName { get; set; }

        [Required(ErrorMessage = "Student Username is required")]
        public string StudentUserName { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        public string StudentName { get; set; }
    }

    public class ReturnBookViewModel
    {
        [Required(ErrorMessage = "Book Id is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Student Username is required")]
        public string StudentUserName { get; set; }
    }
}
