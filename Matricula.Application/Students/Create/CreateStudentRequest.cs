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
    public class CreateStudentRequest
    {

        public string? Identification { get; set; }
        public string? Name { get; set; }
        public string? Programa { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
