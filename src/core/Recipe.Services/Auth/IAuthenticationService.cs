using Recipe.Services.Models;

namespace Recipe.Services.Auth
{
    public interface IAuthenticationService
    {
        AuthenticationResultModel Authenticate(AuthenticateModel request);
    }
}