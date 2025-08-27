using Matricula.Application.Shared;
using Matricula.Domain.Contracts;
using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Application.Materias.Get
{
    public class GetMateriaQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public GetMateriaQuery(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public async Task<Response<List<GetMateriaResponse>>> Handle()
        {
            await _unitOfWork.BeginTransaction();
            try
            {
                var materias = _unitOfWork.GenericRepository<Materia>().GetAll().ToList();

                if (materias != null)
                {
                    return new Response<List<GetMateriaResponse>>($"Materias encontradas", 200, MapGetAllMaterias(materias));
                }

                return new Response<List<GetMateriaResponse>>("No hay materias registradas", 200, null);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Response<List<GetMateriaResponse>>($"Error", 500);
            }

        }

        public async Task<Response<GetMateriaResponse>> Handle(int idMateria)
        {
            await _unitOfWork.BeginTransaction();
            try
            {
                var materia = _unitOfWork.GenericRepository<Materia>().FindBy(m => m.Id ==idMateria).FirstOrDefault();

                if (materia != null)
                {
                    return new Response<GetMateriaResponse>($"Materia encontrada", 200, MapGetMateria(materia));
                }

                return new Response<GetMateriaResponse>("No hay materias registrada", 200, null);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Response<GetMateriaResponse>($"Error", 500);
            }

        }

        private List<GetMateriaResponse> MapGetAllMaterias(List<Materia> materias)
        {

            List<GetMateriaResponse> getResponses = new List<GetMateriaResponse>();


            foreach (var m in materias)
            {
                GetMateriaResponse materia = new GetMateriaResponse();
                materia.Id = m.Id;
                materia.Name = m.Name;
                materia.Code = m.Code;
                materia.Credits = m.Credits;

                getResponses.Add(materia);

            }
            return getResponses;
        }

        private GetMateriaResponse MapGetMateria(Materia materia)
        {

            GetMateriaResponse getResponses = new GetMateriaResponse();

            getResponses.Id = materia.Id;
            getResponses.Name = materia.Name;
            getResponses.Code = materia.Code;
            getResponses.Credits = materia.Credits;

            return getResponses;
        }


    }
}
