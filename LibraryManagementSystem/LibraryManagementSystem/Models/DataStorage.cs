using LibraryManagementSystem.Models;
using System.Collections.Generic;

namespace LibraryManagementSystem.Utilities
{
    public static class DataStorage
    {
        public static List<Student> Students { get; set; } = new List<Student>();

        public static List<IssuedBook> issuedBooks = new List<IssuedBook>();
    }
}
