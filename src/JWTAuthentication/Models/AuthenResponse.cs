using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class AuthenResponse
    {
        public int? statusCode { get; set; }
        public string? statusMessage { get; set; }
        public string? token { get; set; }
        public DateTime? expireDate { get; set; }
    }
}
