namespace BlazorTest.Models.Dtos
{
    public class TokenDto
    {
        public string Token { get; set; }
        public string ExpireTime { get; set; }
        public string? Username { get; set; }
        public string[]? Roles { get; set; }
    }
}
