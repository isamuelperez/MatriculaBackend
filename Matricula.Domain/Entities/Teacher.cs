using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Domain.Entities
{
    public class Teacher : Person
    {
        public ICollection<MateriaTeacher>? MateriasAssigned { get; set; }
    }
}
