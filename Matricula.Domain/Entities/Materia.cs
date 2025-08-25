using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Domain.Entities
{
    public class Materia
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int Credits { get; set; } = 3;
        public ICollection<MateriaTeacher>? Teachers { get; set; }
    }
}
