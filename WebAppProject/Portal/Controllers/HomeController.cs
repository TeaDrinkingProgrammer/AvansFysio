using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            if (User.Identity.IsAuthenticated) {
                //TODO redirect, change if redirect changes
                if (User.Claims.Any(x => x.Value == "Patient")) {
                    return RedirectToAction("Index", "Patient");
                } else {
                    return RedirectToAction("Index", "Employee");
                }
            }
            return View();
        }
    }
}
