using Matricula.Domain.Contracts;
using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Application.Students.Get
{
    public class GetResponse
    {
        public int Id { get; set; }
        public string? Identification { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Programa { get; set; }
        public List<MateriaDTo> ? Materias { get; set; }
    }

    public class MateriaDTo
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public string ? Code { get; set; }
        public int Credits { get; set; } = 3;
        public string ? Teacher { get; set; }
    }
}
