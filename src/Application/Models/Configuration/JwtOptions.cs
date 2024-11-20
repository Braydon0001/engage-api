namespace Engage.Application.Models.Configuration
{
    public class JwtOptions
    {
        public string Provider { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string AllowAnonymous { get; set; }
        public string ApiToken { get; set; }
        public string UsersApiToken { get; set; }
    }
}
