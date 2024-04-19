namespace BlazorTest.Models.Dtos
{
    public class RegisterDto
    {
        public string NickName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool TermsAgreed { get; set; }
    }
}
