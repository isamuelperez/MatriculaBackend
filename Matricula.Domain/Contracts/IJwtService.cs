using Matricula.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Domain.Contracts
{
    public interface IJwtService
    {
        string GetToken(User user);
    }
}
