//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Itadakimasu.Controllers
//{

//    internal class Autenticacion
//    {
//        private readonly JwtSecurityTokenHandler JwtTokenHandler;
//        internal static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());


//        public Autenticacion()
//        {
//            JwtTokenHandler = new JwtSecurityTokenHandler();
//        }


//        internal string GenerateJwtToken(string paramName)
//        {
//            if (string.IsNullOrEmpty(paramName))
//            {
//                throw new InvalidOperationException("Name is not specified.");
//            }

//            var claims = new[] { new Claim(ClaimTypes.Name, paramName) };
//            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
//            var token = new JwtSecurityToken("ExampleServer", "ExampleClients", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
//            return JwtTokenHandler.WriteToken(token);
//        }
//    }


//    public class TokenController : Controller
//    {
//        public class LoginModel
//        {
//            public string Username { get; set; }
//            public string Password { get; set; }
//        }

//        [AllowAnonymous]
//        [HttpPost]
//        public IActionResult Login([FromBody] LoginModel login)
//        {
//            //POST受け取り
//            string username = login.Username;
//            string password = login.Password;

//            //ダミー認証
//            if (username == "hoge" && password == "password")
//            {
//                //token生成（下の関数で）
//                var tokenString = BuildToken();
//                var response = new
//                {
//                    token = tokenString
//                };

//                return Ok(response);
//            }
//            else
//            {
//                return BadRequest();
//            }

//        }

//        //token生成関数
//        private string BuildToken()
//        {
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345678901234567890"));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//            var token = new JwtSecurityToken(
//                issuer: "hoge",
//                audience: "hoge",
//                expires: DateTime.Now.AddMinutes(30),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }


//        internal string GenerateJwtToken(string userName)
//        {
//            if (string.IsNullOrEmpty(userName))
//            {
//                throw new InvalidOperationException("Name is not specified.");
//            }

//            var claims = new[] { new Claim(ClaimTypes.Name, userName) };
//            var credentials = new SigningCredentials(new SymmetricSecurityKey(Guid.NewGuid().ToByteArray()), SecurityAlgorithms.HmacSha256);
//            var token = new JwtSecurityToken("ExampleServer", "ExampleClients", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
