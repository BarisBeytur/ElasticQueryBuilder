namespace ElasticQueryBuilder.Models.Dtos
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool isAuthenticated { get; set; }
    }
}
