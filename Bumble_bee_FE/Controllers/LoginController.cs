using Bumble_bee_FE.API;
using Bumble_bee_FE.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;

namespace Bumble_bee_FE.Controllers
{
    public class LoginController : Controller
    {
        APICommunicator _APICommunicator = new();

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
            Token token = _APICommunicator.Login(login.UserEmail, login.Password);
            if(token.status == true && token.userName != null && token.userId != null) 
            {
                var claims = new List<Claim> {
                            new Claim(ClaimTypes.Name, token.userName),
                            new Claim(ClaimTypes.SerialNumber, token.userId),
                            new Claim(ClaimTypes.Email, login.UserEmail),
                            new Claim("LOGGED", "True")
                            };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                ClaimsPrincipal claimsPrincipal = new(identity);
                AuthenticationProperties props = new()
                {
                    IsPersistent = true
                };
                props.ExpiresUtc = DateTime.UtcNow.AddMinutes(55);
                HttpContext.SignInAsync("CookieAuth", claimsPrincipal, props);

                return Redirect("/Home/Dashboard");
            }
            else
            {
                ViewBag.Error = token.errorMessage;
                return Login();
            }
        }
    }
}
