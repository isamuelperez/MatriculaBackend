using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Domain.Entities
{
    public class Person
    {
        public virtual int Id { get; private set; }
        public virtual string ? Name { get; set; }
        public virtual string ? Identification { get; set; }
        public virtual User? User { get; set; }
    }

    public class User
    {
        public virtual long Id { get; private set; }
        public virtual string? Email { get; set; }
        public virtual string? Password { get; set; }
        public virtual string? Rol { get; set; }
    }
}
