using Matricula.Application.StudenMaterias.Create;
using Matricula.Application.Students.Create;
using Matricula.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Matricula.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMateriaController : Controller
    {
       
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;

        public StudentMateriaController(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;

        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateStudentMateriaRequest request)
        {
            var response = await new CreateStudentMateriaCommand(_unitOfWork, _authenticationService).Handle(request);
            if (response.Status == 201)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
