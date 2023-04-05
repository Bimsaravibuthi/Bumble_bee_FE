using Bumble_bee_API_2.Models;
using Bumble_bee_FE.Controllers;
using Bumble_bee_FE.Models;
using Bumble_bee_FE.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace Bumble_bee_FE.API
{
    public class APICommunicator
    {
        HttpClientHandler _clientHandler = new();
        public APICommunicator()
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, sert, chain, sslPolicyErrors) => { return true; };
        }

        public Token Login(string email, string password)
        {
            Token token = new();

            string loginCredential = EncryptAndDecrypt.EncryptString(email+"<|SP|>"+password);

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = httpClient.GetAsync("https://localhost:7056/Login/Login/" + loginCredential).Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    token = JsonConvert.DeserializeObject<Token>(apiResponse);
                }
            }

            if(token.accessToken != null)
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwt = tokenHandler.ReadJwtToken(token.accessToken);
                token.userId = jwt.Subject;
                token.userName = jwt.Claims.First(c => c.Type == "Name").Value;
                DateTime issuedAt = jwt.ValidFrom;
            }

            return token;
        }
        
        public List<User> GetAllUsers()
        {
            List<User> users = new();

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = httpClient.GetAsync("https://localhost:7056/User/GetUser(s)").Result)
                {
                    string apiResponse = response.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<List<User>>(apiResponse);
                }
            }
            return users;
        }

        public StatusCode AddUser(User user)
        {
            StatusCode statusCode = new();
            using (var httpClient = new HttpClient(_clientHandler))
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync("https://localhost:7056/User/AddUser", content).Result)
                {
                    string apiresponse = response.Content.ReadAsStringAsync().Result;
                    statusCode = JsonConvert.DeserializeObject<StatusCode>(apiresponse);
                }
            }
            return statusCode;
        }
    }
}
