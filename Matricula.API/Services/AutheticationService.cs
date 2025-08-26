using Matricula.Domain.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Matricula.API.Services
{
    public class AutheticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AutheticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetIdUser()
        {
            var token = GetToken();
            if (token == null)
                return 0;
            var id = token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            return int.Parse(id);
        }

        public string GetRolUser()
        {
            var token = GetToken();
            if (token == null)
                return "";
            var rol = token.Claims.First(x => x.Type == "role").Value;
            if (rol.Equals("Student"))
                return "Student";
            else if (rol.Equals("Teacher"))
                return "Teacher";
            else
            {
                return "Admin";
            }

        }

        private JwtSecurityToken GetToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers.Where(x => x.Key.Equals("Authorization"))?.Select(x => x.Value).FirstOrDefault().FirstOrDefault();
            if (token == null)
                return null;
            token = token.Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token);

        }
    }
}
