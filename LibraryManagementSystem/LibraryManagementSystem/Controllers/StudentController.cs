using LibraryManagementSystem.Models;
using LibraryManagementSystem.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        LibrarianController librarianController = new LibrarianController();
        public ActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentLogin(Student loginStudent)
        {
            var students = DataStorage.Students;
            var student = students.FirstOrDefault(s => s.UserName == loginStudent.UserName && s.Password == loginStudent.Password);
            if (student != null)
            {
                return RedirectToAction("StudentDashboard");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(loginStudent);
            }
        }
        public ActionResult StudentDashboard()
        {
            ViewBag.WelcomeMessage = "Welcome " + Session["StudentName"];

            return View();
        }

        public ActionResult Account()
        {
            var students = DataStorage.Students;
            var student = students.FirstOrDefault();

            if (student != null)
            {
                return View(student);
            }
            else
            {
                TempData["ErrorMessage"] = "No Student is present";
                return RedirectToAction("StudentDashboard");
            }
        }

        public ActionResult BookReports()
        {
            var students = DataStorage.Students;
            var student = students.FirstOrDefault();
            var studentIssuedBooks = DataStorage.issuedBooks.Where(ib => ib.Student.Id == student.Id).ToList();
            return View(studentIssuedBooks);
        }

    }
}
