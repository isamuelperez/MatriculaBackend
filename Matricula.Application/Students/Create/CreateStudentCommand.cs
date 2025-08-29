using Matricula.Application.Shared;
using Matricula.Domain.Contracts;
using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Application.Students.Create
{
    public class CreateStudentCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public CreateStudentCommand(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }
        public async Task<Response<Student>> Handle(CreateStudentRequest request)
        {
            await _unitOfWork.BeginTransaction();
            
            if(1==2) return new Response<Student>($"Se creo correctamente el estudiante: ", 201);

            else
            {
                try
                {
                    var estudiante = _unitOfWork.GenericRepository<Student>().
                        FindBy(m => m.Identification == request.Identification)?.FirstOrDefault();
                    if (estudiante is null)
                    {
                        var userCreate = CreateUser(request);
                        await _unitOfWork.BeginTransaction();
                        var estudianteCreate = _unitOfWork.GenericRepository<Student>().AddAndReturn(MapStudent(request, userCreate));
                        await _unitOfWork.Commit();
                        return new Response<Student>($"Se creo correctamente el estudiante: {estudianteCreate.Name}", 201);
                    }

                    return new Response<Student>($"El Estudiante con Identificación: {request.Identification} ya existe", 400);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<Student>($"Error", 500);
                }
            }
        }

        private User CreateUser(CreateStudentRequest request)
        {
            var userCreate = _unitOfWork.GenericRepository<User>().AddAndReturn(MapUser(request));
            _unitOfWork.Commit();
            _unitOfWork.GenericRepository<User>().Max(u => u.Id);
  
            return userCreate;
        }

        private Student MapStudent(CreateStudentRequest createStudentRequest, User userCreate)
        {
            return new()
            {
                User = userCreate,
                Identification = createStudentRequest.Identification,
                Name = createStudentRequest.Name,
                Programa = createStudentRequest.Programa,
           
            };
        }

        private User MapUser(CreateStudentRequest createStudentRequest)
        {
            return new()
            {
                Email = createStudentRequest.Email,
                Password = createStudentRequest.Password,
                Rol = "Estudiante",

            };
        }
    }
}
