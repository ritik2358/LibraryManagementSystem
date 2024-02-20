using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class LibrarianController : Controller
    {
        private static List<Librarian> librarians = new List<Librarian>();
        private static List<Publication> publications = new List<Publication>();
        private static List<BookStock> bookStocks = new List<BookStock>();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LibrarianLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LibrarianLogin(Librarian loginLibrarian)
        {
            var librarian = librarians.FirstOrDefault(l => l.UserName == loginLibrarian.UserName && l.Password == loginLibrarian.Password);
            if (librarian != null)
            {
                return RedirectToAction("LibrarianDashboard");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(loginLibrarian);
            }
        }

        public ActionResult LibrarianSignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LibrarianSignUp(Librarian newLibrarian)
        {
            if (ModelState.IsValid)
            {
                if (librarians.Any(l => l.UserName == newLibrarian.UserName))
                {
                    ModelState.AddModelError("Username", "Username already exists. Please choose a different one.");
                    return View(newLibrarian);
                }
                newLibrarian.Id = librarians.Count + 1;
                librarians.Add(newLibrarian);
                return RedirectToAction("LibrarianLogin");
            }
            return View(newLibrarian);
        }

        public ActionResult LibrarianDashboard()
        {
            ViewBag.WelcomeMessage = "Welcome Librarian";

            return View();
        }

        public ActionResult AddPublication()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPublication(Publication publication)
        {
            if (ModelState.IsValid)
            {
                publication.Id = publications.Count + 1;
                publications.Add(publication);
                TempData["SuccessMessage"] = "Book Publication Added Successfully";
                return RedirectToAction("LibrarianDashboard");
            }
            return View(publication);
        }

        public ActionResult AddBookStock()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBookStock(BookStock bookStock)
        {
            if (ModelState.IsValid)
            {
                var publication = publications.FirstOrDefault(p => p.BookId == bookStock.BookId && p.BookName == bookStock.BookName);
                if (publication == null)
                {
                    TempData["ErrorMessage"] = "Book doesn't exist";
                    return RedirectToAction("LibrarianDashboard");
                }
                bookStocks.Add(bookStock);

                TempData["SuccessMessage"] = "Book Stock Added Successfully";

                return RedirectToAction("LibrarianDashboard");
            }
            return View(bookStock);
        }

        private static List<Branch> branches = new List<Branch>();

        public ActionResult AddBranch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBranch(Branch branch)
        {
            if (ModelState.IsValid)
            {
                var publication = publications.FirstOrDefault(p => p.BookId == branch.BookId && p.BookName == branch.BookName);
                if (publication == null)
                {
                    TempData["ErrorMessage"] = "Book doesn't exist";
                    return RedirectToAction("LibrarianDashboard");
                }
                branches.Add(branch);

                TempData["SuccessMessage"] = "Book Branch Added Successfully";

                return RedirectToAction("LibrarianDashboard");
            }
            return View(branch);
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                var students = DataStorage.Students;
                if (students.Any(s => s.UserName == student.UserName))
                {
                    ModelState.AddModelError("UserName", "Username already exists. Please choose a different one.");
                    return View(student);
                }

                students.Add(student);
                TempData["SuccessMessage"] = "Student added successfully.";
                return RedirectToAction("LibrarianDashboard");
            }
            return View(student);
        }
        public static List<Student> GetStudentsList()
        {
            return DataStorage.Students;
        }
        public ActionResult IssueBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IssueBook(IssueBookViewModel model)
        {
            var book = publications.FirstOrDefault(b => b.BookId == model.BookId && b.BookName == model.BookName);
            if (book == null)
            {
                ModelState.AddModelError("", "Book not found");
                return View(model);
            }
            var student = DataStorage.Students.FirstOrDefault(s => s.UserName == model.StudentUserName && s.Name == model.StudentName);
            if (student == null)
            {
                ModelState.AddModelError("", "Student not found");
                return View(model);
            }
            DataStorage.issuedBooks.Add(new IssuedBook
            {
                Book = book,
                Student = student,
                IssueDate = DateTime.Now
            });

            TempData["SuccessMessage"] = "Book issued successfully";

            return RedirectToAction("LibrarianDashboard");
        }

        public ActionResult ReturnBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReturnBook(ReturnBookViewModel model)
        {
            var issuedBook = DataStorage.issuedBooks.FirstOrDefault(ib => ib.Book.BookId == model.BookId && ib.Student.UserName == model.StudentUserName);
            if (issuedBook == null)
            {
                ModelState.AddModelError("", "Issued book not found");
                return View(model);
            }
            DataStorage.issuedBooks.Remove(issuedBook);
            TempData["SuccessMessage"] = "Book returned successfully";
            return RedirectToAction("LibrarianDashboard");
        }
    }
}