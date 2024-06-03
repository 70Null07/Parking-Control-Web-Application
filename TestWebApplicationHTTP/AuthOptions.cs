using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TestWebApplicationHTTP
{
    public class AuthOptions
    {
        // Издатель токена
        public const string ISSUER = "CarSiteApplication";
        // Потребитель токена
        public const string AUDIENCE = "CarSiteUsers";
        // Ключ шифрования
        const string KEY = "FVle{9X8D_rqH0QBC>C,IM|[n&HO?_uyl4tUWdU+DnseG$D#_khfygv5o7.]0~V";
        // Время жизни токена в минутах
        public const int LIFETIME = 20160;
        // Симметричный ключ шифрования из строкового ключа
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
