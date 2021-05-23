using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogicLayer.JWTs
{
    public class JwtOptions
    {
        public const string ISSUER = "https://localhost:44365"; // издатель токена
        public const string AUDIENCE = "https://localhost:44328"; // потребитель токена
        public const string KEY = "ThEHouseSeCRetKeyOfJwTtoUseItONReQuEStwiTHaUtHoriZE";   // ключ для шифрации
        public const int LIFETIME = 15; // время жизни токена - 1 минута
    }
}
