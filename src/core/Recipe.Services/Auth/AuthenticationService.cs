using Recipe.Services.Models;
using Recipe.Data;

namespace Recipe.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthContext _context;

        public AuthenticationService(AuthContext context)
        {
            _context = context;
        }
        
        public AuthenticationResultModel Authenticate(AuthenticateModel request)
        {
            // some logic with authentication
            return new AuthenticationResultModel();
        }
    }
}