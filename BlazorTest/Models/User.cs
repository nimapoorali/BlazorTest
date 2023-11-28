namespace BlazorTest.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? NickName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsMobileVerified { get; set; }
        public string? Mobile { get; set; }
        public bool? IsEmailVerified { get; set; }
        public string? Email { get; set; }
        public string? ActivityState { get; set; }
        public bool? IsActive { get { return ActivityState == "Suspend" ? null : ActivityState == "Active"; } set { } }
    }
}
