using Matricula.Application.Shared;
using Matricula.Domain.Contracts;
using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Matricula.Application.Teachers.Get.GetResponse;

namespace Matricula.Application.Teachers.Get
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

            //if (userId <= 0) return new Response<List<GetResponse>>("Error en auntenticación", 500, null);

            var userAuthenticated = _unitOfWork.GenericRepository<User>().Find(userId);

            //if (userAuthenticated is null) return new Response<List<GetResponse>>("Error en auntenticación", 500, null);
           

            if (userAuthenticated.Rol != "Administrador") new Response<List<GetResponse>>("No tienes permiso para solicitar esta información", 200, null);
            try
            {
                var teachers = _unitOfWork.GenericRepository<Teacher>().FindBy(includeProperties: "User").ToList();

                if (teachers != null)
                {
                    return new Response<List<GetResponse>>($"Profesores encontrados", 200, MapGetAllTeacher(teachers));
                }

                return new Response<List<GetResponse>>("No hay profesores registrados", 200, null);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Response<List<GetResponse>>($"Error", 500);
            }

        }

        public async Task<Response<GetResponse>> Handle(int idTeacher)
        {
            await _unitOfWork.BeginTransaction();

            try
            {
                var teachersMaterias = _unitOfWork.GenericRepository<MateriaTeacher>().FindBy(t => t.TeacherId == idTeacher, includeProperties: "Materia,Teacher").ToList();

                if (teachersMaterias.Count() > 0)
                {
                    return new Response<GetResponse>($"Profesores encontrados", 200, MapGetTeacher(teachersMaterias));
                }

                return new Response<GetResponse>("No se encontro el profesores", 200, null);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Response<GetResponse>($"Error", 500);
            }

        }

        private List<GetResponse> MapGetAllTeacher(List<Teacher> teachers)
        {

            List<GetResponse> getResponses = new List<GetResponse>();
            List<MateriaDTo> materias;

            List<MateriaTeacher> listMaterias = new List<MateriaTeacher>();
            listMaterias = _unitOfWork.GenericRepository<MateriaTeacher>().FindBy(includeProperties: "Materia,Teacher").ToList();

            GetResponse getResponse;

            foreach (var t in teachers)
            {
                materias = new List<MateriaDTo>();
                foreach (var m in listMaterias)
                {
                    if (t.Id == m.TeacherId)
                    {
                        MateriaDTo materia = new MateriaDTo();
                        materia.Id = m.Materia.Id;
                        materia.Name = m.Materia.Name;
                        materia.Credits = m.Materia.Credits;
                        materia.Code = m.Materia.Code;
                        materias.Add(materia);
                    }

                }
                var user = _unitOfWork.GenericRepository<User>().FindBy(u => u.Id == t.User.Id).FirstOrDefault();
                getResponse = new GetResponse();
                getResponse.Id = t.Id;
                getResponse.Identification = t.Identification;
                getResponse.Name = t.Name;
                getResponse.Email = t.User.Email;
                getResponse.Materias = materias;

                getResponses.Add(getResponse);
            }
            return getResponses;
        }

     


        private GetResponse MapGetTeacher(List<MateriaTeacher> teachersMaterias)
        {

            GetResponse getResponse = new GetResponse();
            List<MateriaDTo> materias = new List<MateriaDTo>(); ;

            List<StudentMateria> listMaterias = new List<StudentMateria>();
            foreach (var m in teachersMaterias)
            {
                MateriaDTo materia = new MateriaDTo();
                materia.Id = m.Materia.Id;
                materia.Name = m.Materia.Name;
                materia.Credits = m.Materia.Credits;
                materia.Code = m.Materia.Code;
                materias.Add(materia);
            }

            var item = teachersMaterias.FirstOrDefault();
            var user = _unitOfWork.GenericRepository<User>().FindBy(u => u.Id == item.TeacherId).FirstOrDefault();

            getResponse.Id = item.TeacherId;
            getResponse.Identification = item.Teacher.Identification;
            getResponse.Name = item.Teacher.Name;
            getResponse.Email = user.Email;
            getResponse.Materias = materias;
            return getResponse;
        }



    }
}
