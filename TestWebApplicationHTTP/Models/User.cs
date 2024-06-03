namespace TestWebApplicationHTTP.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Permissions { get; set; }
        public DateTime AccessExpiresTime { get; set;}
        public DateTime LastLoginTime { get; set; }
        public int? isConmirmedEmail { get; set; }
        public string? IPaddress { get; set; }
    }
}
