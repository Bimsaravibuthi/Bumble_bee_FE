using Bumble_bee_FE.Controllers;
using Bumble_bee_FE.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            using (var httpClient = new HttpClient(_clientHandler))
            {
                using (var response = httpClient.GetAsync("https://localhost:7056/Login/Login/" + email + "<|SP|>" + password).Result)
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
    }
}
