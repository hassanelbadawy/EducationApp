using EducationApp.Data;
using EducationApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = model.Password,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _context.Users
                .FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                ViewBag.Message = "User not found";

                return View(model);
            }

            if (user.PasswordHash != model.Password)
            {
                ViewBag.Message = "Wrong password";

                return View(model);
            }
            HttpContext.Session.SetInt32(
                "UserId",
                user.UserId
            );

            HttpContext.Session.SetString(
                "FullName",
                user.FullName
            );

            HttpContext.Session.SetString(
                "Email",
                user.Email
            );

            return RedirectToAction(
                "Index",
                "Dashboard"
            );
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Login",
                "Account"
            );
        }

    }


}