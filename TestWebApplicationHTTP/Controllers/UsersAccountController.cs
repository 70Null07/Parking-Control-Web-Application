using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TestWebApplicationHTTP.Controllers
{
    public class UsersAccountController : Controller
    {
        [HttpPost("/token")]
        public IActionResult Token(string login, string password, string email, string permissions, string audience, DateTime tokenCreatedTime)
        {
            // Список сохраняемых в Payload данных
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Hash, password),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, permissions),
                new Claim(ClaimTypes.DateOfBirth, tokenCreatedTime.ToLongDateString()),
            };
            // Payload из списка данных
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            if (claimsIdentity == null)
                return BadRequest(new { errorText = "Invalid name or password" });
            // JWT-токен на основе Header из класса AuthOptions, Payload из claimsIdentity
            // и Signature на основе ключа из AuthOptions
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: audience,
                notBefore: DateTime.UtcNow,
                claims: claimsIdentity.Claims,
                expires: DateTime.UtcNow.AddDays(14),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );
            // Преобразование объекта JWT в строку
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            // Возврат Json объекта с полями encodedJwt и username
            return Json(new
            {
                access_token = encodedJwt,
                username = claimsIdentity.Name,
            });
        }
    }
}
