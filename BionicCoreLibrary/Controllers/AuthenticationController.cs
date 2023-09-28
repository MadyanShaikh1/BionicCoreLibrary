using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.Core.CoreServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BionicCoreLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(Users users)
        {
            string result = await authenticationService.Authenticate(users);
            return Ok(result);
        }
    }
}
