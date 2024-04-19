namespace BlazorTest.Models
{
    public class Permission
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ActivityState { get; set; }
        public Guid[]? RoleIds { get; set; }

        public bool? IsActive { get { return ActivityState == "Suspend" ? null : ActivityState == "Active"; } set { } }
    }
}
