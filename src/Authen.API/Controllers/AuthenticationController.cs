using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JWTAuthentication;
using JWTAuthentication.Models;

namespace Authen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JWTToken _jwtToken;

        public AuthenticationController(JWTToken jwtToken)
        {
            _jwtToken = jwtToken;
        }

        [HttpPost]
        public ActionResult<AuthenResponse> Authenticate([FromBody] AuthenRequest req)
        {
            var authenticationResponse = _jwtToken.GenToken(req);
            if(authenticationResponse == null) return Unauthorized();
            return Ok(authenticationResponse);
        }
    }
}
