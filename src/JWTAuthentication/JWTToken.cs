using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthentication
{
    public class JWTToken
    {
        private IConfiguration Config { get; }
        private IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        public JWTToken(IConfiguration config)
        {
            this.Config = config;
        }

        public AuthenResponse GenToken(AuthenRequest req)
        {
            //IConfigurationRoot configuration = builder.Build();            
            try
            {
                if (string.IsNullOrEmpty(req.User) || string.IsNullOrEmpty(req.Password))
                    return null;

                //var user = configuration["JWTAuthen:User"];
                //var password = configuration["JWTAuthen:Password"];
                //var Role = configuration["JWTAuthen:Role"];
                //var MaxTokenHour = Convert.ToDouble(configuration["JWTAuthen:MaxTokenHour"]);
                //string? privateKey = configuration["JWTAuthen:PrivateKey"];
                //var EncodeprivateKey = Encoding.ASCII.GetBytes(privateKey);

                var PrivateKey = "560A18CD-6346-4CF0-A2E8-671F9B6B9EA0";
                var user = "admin";
                var password = "P@ssw0rd";
                var Role = "admin";
                var MaxTokenHour = 8;
                var privateKey = Encoding.ASCII.GetBytes(PrivateKey);

                if (req.User.Equals(user) && req.Password.Equals(password))
                {
                    var claimsIdentity = new ClaimsIdentity(new List<Claim>
                    {
                    new Claim(JwtRegisteredClaimNames.Name, req.User),
                    new Claim(ClaimTypes.Role,Role)
                    });

                    var signingCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(privateKey),
                        SecurityAlgorithms.HmacSha256Signature);

                    var securityTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claimsIdentity,
                        Expires = DateTime.UtcNow.AddHours(MaxTokenHour),
                        SigningCredentials = signingCredentials,
                    };

                    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                    var token = jwtSecurityTokenHandler.WriteToken(securityToken);

                    return new AuthenResponse
                    {
                        statusCode = 200,
                        statusMessage = "Success",
                        token = token,
                        expireDate = DateTime.UtcNow.AddHours(MaxTokenHour)
                    };
                }
                else
                {
                    return new AuthenResponse
                    {
                        statusCode = 500,
                        statusMessage = "Failed",
                        token = null,
                        expireDate = null
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new AuthenResponse
                {
                    statusCode = 500,
                    statusMessage = "Failed",
                    token = null,
                    expireDate = null
                };
            }
        }
    }
}
