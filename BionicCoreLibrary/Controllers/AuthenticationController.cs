using BionicCoreLibrary.Core.Concrete;
using BionicCoreLibrary.Core.CoreServices;
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

        [HttpPost]
        public async Task<IActionResult> Authenticate(Users users)
        {
            string result = await authenticationService.Authenticate(users);
            return Ok(result);
        }
    }
}
