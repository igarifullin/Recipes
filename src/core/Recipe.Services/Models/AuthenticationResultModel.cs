namespace Recipe.Services.Models
{
    public class AuthenticationResultModel
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}