using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class UserAuthen
    {
        public string? User { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
