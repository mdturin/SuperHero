namespace JwtWebApiTutorial
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
