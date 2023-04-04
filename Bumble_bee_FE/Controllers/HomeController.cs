using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bumble_bee_FE.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Policy = "AdminUsersOnly")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
