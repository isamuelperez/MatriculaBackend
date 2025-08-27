using Matricula.Application.Materias.Get;
using Matricula.Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Matricula.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public MateriaController(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;

        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetAll()
        {
            var response = await new GetMateriaQuery(_unitOfWork, _authenticationService).Handle();
            if (response.Status == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("get")]
        public async Task<ActionResult> Get(int id)
        {
            var response = await new GetMateriaQuery(_unitOfWork, _authenticationService).Handle(id);
            if (response.Status == 200)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
