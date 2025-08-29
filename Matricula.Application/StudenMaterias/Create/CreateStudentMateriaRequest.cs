using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Application.StudenMaterias.Create
{
    public class CreateStudentMateriaRequest
    {

        public int StudentId { get; set; }
        public List<int>? Materias { get; set; }

    }

    public class MateriaDTO
    {
        public int Id { get; set; }
    }
}
