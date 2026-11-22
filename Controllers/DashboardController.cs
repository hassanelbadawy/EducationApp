using Microsoft.AspNetCore.Mvc;

namespace EducationApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }

            ViewBag.FullName =
                HttpContext.Session.GetString("FullName");

            return View();
        }
    }
}
