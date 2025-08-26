using Matricula.Application.Shared;
using Matricula.Domain.Contracts;
using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Application.Login
{
    public class AuthenticationCommand
    {
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _UnitOfWork;

        public AuthenticationCommand(IUnitOfWork UnitOfWork, IJwtService jwtService)
        {
            _UnitOfWork = UnitOfWork;
            _jwtService = jwtService;
        }

        public Response<AuthenticationResponse> Handle(AuthenticationRequest request)
        {
            _UnitOfWork.BeginTransaction();

            var user = _UnitOfWork.GenericRepository<User>().FindBy(x => x.Email == request.Email
            && x.Password == request.Password).FirstOrDefault();
            if (user is null)
                return new Response<AuthenticationResponse>("Usuario y/o clave incorrecta.", 400);

            var response = new AuthenticationResponse();
            response.Email = user.Email;
            response.UserId = user.Id;
            response.Rol = user.Rol;
            response.Token = _jwtService.GetToken(user);
            return new Response<AuthenticationResponse>("Se consultó correctamente.", response);


        }
    }
}
