using Bumble_bee_FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bumble_bee_FE.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login) 
        {
            using(var httpClient = new HttpClient())
            {

            }
            return View();
        }
    }
}
