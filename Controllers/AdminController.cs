using EducationApp.Data;
using EducationApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Users()
        {
            var role =
                HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction(
                    "Index",
                    "Dashboard"
                );
            }

            var users =
                _context.Users.ToList();

            return View(users);
        }
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var role =
                HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction(
                    "Index",
                    "Dashboard"
                );
            }

            var user =
                _context.Users.FirstOrDefault(
                    x => x.UserId == id
                );

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public IActionResult EditUser(User user)
        {
            var dbUser =
                _context.Users.FirstOrDefault(
                    x => x.UserId == user.UserId
                );

            if (dbUser == null)
            {
                return NotFound();
            }

            dbUser.FullName = user.FullName;
            dbUser.Email = user.Email;

            _context.SaveChanges();

            return RedirectToAction(
                "Users"
            );
        }
        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            var role =
                HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction(
                    "Index",
                    "Dashboard"
                );
            }

            var user =
                _context.Users.FirstOrDefault(
                    x => x.UserId == id
                );

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public IActionResult DeleteUser(User user)
        {
            var dbUser =
                _context.Users.FirstOrDefault(
                    x => x.UserId == user.UserId
                );

            if (dbUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(dbUser);

            _context.SaveChanges();

            return RedirectToAction(
                "Users"
            );
        }
    }
}