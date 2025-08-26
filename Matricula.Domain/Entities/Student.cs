using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Domain.Entities
{
    public class Student : Person
    {
        public string ? Programa { get; set; }
        public ICollection<StudentMateria>? StudentMaterias { get; set; }
    }
}
