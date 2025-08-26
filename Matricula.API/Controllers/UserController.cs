using Matricula.Application.Login;
using Matricula.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Matricula.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        public UserController(IUnitOfWork unitOfWork, IJwtService jwtService, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        [HttpPost("authentication")]
        public ActionResult Authentication(AuthenticationRequest request)
        {
            var response = new AuthenticationCommand(_unitOfWork, _jwtService).Handle(request);
            if (response.Status == 200)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
