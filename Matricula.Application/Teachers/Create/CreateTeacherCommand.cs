using Matricula.Application.Shared;
using Matricula.Domain.Contracts;
using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Application.Teachers.Create
{
    public class CreateTeacherCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public CreateTeacherCommand(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public async Task<Response<Teacher>> Handle(CreateTeacherRequest request)
        {
            await _unitOfWork.BeginTransaction();
            //if (request is null) return new Response<Teacher>("El profesor nulo", 400);
            int userId = _authenticationService.GetIdUser();

            //if (userId <= 0) return new Response<Teacher>("El usuario no esta autenticado.", 500);

            var userAuthenticated = _unitOfWork.GenericRepository<User>().Find(1);

            if (userAuthenticated is null) return new Response<Teacher>("No se pudo encontrar el usuario.", 500);
            
            if (userAuthenticated.Rol != "Administrador") return new Response<Teacher>("El usuario no tiene permisos para registrar rofesores.", 400);

            else
            {
                try
                {
                    var estudiante = _unitOfWork.GenericRepository<Teacher>().
                        FindBy(m => m.Identification == request.Identification)?.FirstOrDefault();
                    if (estudiante is null)
                    {
                        var userCreate = CreateUser(request);
                        var estudianteCreate = _unitOfWork.GenericRepository<Teacher>().AddAndReturn(MapStudent(request, userCreate));
                        await _unitOfWork.Commit();
                        return new Response<Teacher>($"Se creo correctamente el profesor: {estudianteCreate.Name}", 201);
                    }

                    return new Response<Teacher>($"El profesor con Identificación: {request.Identification} ya existe", 400);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<Teacher>($"Error", 500);
                }
            }
        }

        private User CreateUser(CreateTeacherRequest request)
        {

            var userCreate = _unitOfWork.GenericRepository<User>().AddAndReturn(MapUser(request));
            _unitOfWork.GenericRepository<User>().Max(u => u.Id);

            return userCreate;
        }

        private Teacher MapStudent(CreateTeacherRequest createStudentRequest, User userCreate)
        {
            return new()
            {
                User = userCreate,
                Identification = createStudentRequest.Identification,
                Name = createStudentRequest.Name,

            };
        }

        private User MapUser(CreateTeacherRequest createStudentRequest)
        {
            return new()
            {
                Email = createStudentRequest.Email,
                Password = createStudentRequest.Password,
                Rol = "Profesor",

            };
        }
    }
}
