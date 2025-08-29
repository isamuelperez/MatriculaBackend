using Matricula.Application.Shared;
using Matricula.Application.Students.Create;
using Matricula.Domain.Contracts;
using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Application.Students.Get
{
    public class GetQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public GetQuery(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public async Task<Response<List<GetResponse>>> Handle()
        {
            await _unitOfWork.BeginTransaction();
            int userId = _authenticationService.GetIdUser();

            if (userId <= 0) return new Response<List<GetResponse>>("Error en auntenticación", 500, null);

            var userAuthenticated = _unitOfWork.GenericRepository<User>().Find(userId);

            if (userAuthenticated is null) return new Response<List<GetResponse>>("Error en auntenticación", 500, null);

            if (userAuthenticated.Rol == "Administrador" || userAuthenticated.Rol =="Estudiante") {

                try
                {
                    var students = _unitOfWork.GenericRepository<Student>().GetAll().ToList();

                    if (students != null)
                    {
                        return new Response<List<GetResponse>>($"Estudiantes encontrados", 200, MapGetAllStudents(students));
                    }

                    return new Response<List<GetResponse>>("No hay Estudiantes registrados", 200, null);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<List<GetResponse>>($"Error", 500);
                }
            }else if (userAuthenticated.Rol == "Estudiante")
            {
                //var studiantesMaterias = StudentMateria(userId);
            }


                return new Response<List<GetResponse>>("No tienes permiso para solicitar esta información", 500, null);
           

        }

        public async Task<Response<GetResponse>> Handle(int idStudent)
        {
            await _unitOfWork.BeginTransaction();

            try
            {
                var studentsMaterias = _unitOfWork.GenericRepository<StudentMateria>().FindBy(s => s.StudentId == idStudent, includeProperties: "Materia,Student,Teacher").ToList();

                if (studentsMaterias.Count> 0)
                {
                    return new Response<GetResponse>($"Estudiantes encontrados", 200, MapGetStudents(studentsMaterias));
                }
                else if(studentsMaterias.Count > 0)
                {
                    var student = _unitOfWork.GenericRepository<Student>().FindFirstOrDefault(e=>e.Id == idStudent);
                    return new Response<GetResponse>($"Estudiantes encontrados", 200, MapStudent(student));
                }

                return new Response<GetResponse>("No hay Estudiantes registrados", 200, null);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Response<GetResponse>($"Error", 500);
            }

        }

        private List<GetResponse> MapGetAllStudents(List<Student> students)
        {

            List<GetResponse> getResponses = new List<GetResponse>();
            List<MateriaDTo> materias;

            List<StudentMateria> listMaterias = new List<StudentMateria>();
            listMaterias = _unitOfWork.GenericRepository<StudentMateria>().FindBy(includeProperties: "Materia,Teacher").ToList();

            GetResponse getResponse;

            foreach (var s in students)
            {
                materias =  new List<MateriaDTo>();
                foreach (var m in listMaterias)
                {
                    if(s.Id == m.StudentId)
                    {
                        MateriaDTo materia = new MateriaDTo();
                        materia.Id = m.Materia.Id;
                        materia.Name = m.Materia.Name;
                        materia.Credits = m.Materia.Credits;
                        materia.Teacher = m.Teacher.Name;
                        materia.Code = m.Materia.Code;
                        materias.Add(materia);
                    }
                   
                }
                var user = _unitOfWork.GenericRepository<User>().FindBy(u => u.Id == s.Id).FirstOrDefault();
                getResponse = new GetResponse();
                getResponse.Id = s.Id;
                getResponse.Identification = s.Identification;
                getResponse.Name = s.Name;
                getResponse.Email = user.Email;
                getResponse.Programa = s.Programa;
                getResponse.Materias = materias;

                getResponses.Add(getResponse);
            }
            return getResponses;
        }


        private GetResponse MapGetStudents(List<StudentMateria> studentsMaterias)
        {

            GetResponse getResponse = new GetResponse();
            List<MateriaDTo> materias = new List<MateriaDTo>(); ;

            List<StudentMateria> listMaterias = new List<StudentMateria>();
            foreach (var m in studentsMaterias)
            {
                MateriaDTo materia = new MateriaDTo();
                materia.Id = m.Materia.Id;
                materia.Name = m.Materia.Name;
                materia.Credits = m.Materia.Credits;
                materia.Teacher = m.Teacher.Name;
                materia.Code = m.Materia.Code;
                materias.Add(materia);
            }

            var item = studentsMaterias.FirstOrDefault();
            var user = _unitOfWork.GenericRepository<User>().FindBy(u => u.Id == item.StudentId).FirstOrDefault();

            getResponse.Id = item.StudentId;
            getResponse.Identification = item.Student.Identification;
            getResponse.Name = item.Student.Name;
            getResponse.Email = user.Email;
            getResponse.Programa = item.Student.Programa;
            getResponse.Materias = materias;
            return getResponse;
        }

        private GetResponse MapStudent(Student student)
        {

            GetResponse getResponse = new GetResponse();
    
            var user = _unitOfWork.GenericRepository<User>().FindBy(u => u.Id == student.Id).FirstOrDefault();

            getResponse.Id = student.Id;
            getResponse.Identification = student.Identification;
            getResponse.Name = student.Name;
            getResponse.Email = user.Email;
            getResponse.Programa = student.Programa;
            getResponse.Materias = null;
            return getResponse;
        }

    }
}
