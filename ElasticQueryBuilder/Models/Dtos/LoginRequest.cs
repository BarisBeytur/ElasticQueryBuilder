namespace ElasticQueryBuilder.Models.Dtos
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FingerPrint { get; set; }
        public string Url { get; set; }
    }
}
