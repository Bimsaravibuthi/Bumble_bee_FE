using Bumble_bee_API_2.Models;
using Bumble_bee_FE.API;
using Bumble_bee_FE.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Bumble_bee_FE.Controllers
{
    public class UserController : Controller
    {
        HttpClientHandler _clientHandler = new();
        public UserController()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, sert, chain, sslPolicyErrors) => { return true; };
        }
        APICommunicator _APICommunicator = new();
        public IActionResult User()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAllUsers(string userType)
        {
            var result = _APICommunicator.GetAllUsers();
            string json = JsonConvert.SerializeObject(result);
            return Json(json);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            var result = _APICommunicator.AddUser(user);
            string json = JsonConvert.SerializeObject(result);
            return Json(json);
        }
    }
}
