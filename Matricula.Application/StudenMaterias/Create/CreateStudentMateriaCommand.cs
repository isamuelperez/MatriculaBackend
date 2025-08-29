using Matricula.Application.Shared;
using Matricula.Domain.Contracts;
using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Application.StudenMaterias.Create
{
    public class CreateStudentMateriaCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public CreateStudentMateriaCommand(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }
        public async Task<Response<StudentMateria>> Handle(CreateStudentMateriaRequest request)
        {
            
         /*
            int userId = _authenticationService.GetIdUser();

            if (userId <= 0) return new Response<StudentMateria>("El usuario no esta autenticado.", 500);

            var userAuthenticated = _unitOfWork.GenericRepository<User>().Find(userId);

            if (userAuthenticated is null) return new Response<StudentMateria>("No se pudo encontrar el usuario.", 500);
           */
            if (2!=2) return new Response<StudentMateria>("El usuario no tiene permisos para reggistrar estudiates.", 400);
           

            else
            {
                try
                {

                    foreach (var item in request.Materias)
                    {
                        await _unitOfWork.BeginTransaction();
                        var teacher = _unitOfWork.GenericRepository<MateriaTeacher>().FindFirstOrDefault(m => m.MateriaId == item);

                        StudentMateria studentMateria = new StudentMateria();

                        studentMateria.StudentId = request.StudentId;
                        studentMateria.TeacherId = teacher.TeacherId;
                        studentMateria.MateriaId = item;


                        var estudianteCreate = _unitOfWork.GenericRepository<StudentMateria>().AddAndReturn(studentMateria);
                        await _unitOfWork.Commit();

                    }

                    return new Response<StudentMateria>($"Se asignaron las materias correctamente", 200);

                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<StudentMateria>($"Error", 500);
                }
            }
        }
    }
}
