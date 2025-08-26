using Matricula.Application.Students.Create;
using Matricula.Domain.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Matricula.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public StudentController(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;

        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateStudentRequest request)
        {
            var response = await new CreateStudentCommand(_unitOfWork, _authenticationService).Handle(request);
            if (response.Status == 201)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
