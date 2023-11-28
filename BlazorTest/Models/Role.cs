namespace BlazorTest.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string GroupTitle { get; set; }
        public string ActivityState { get; set; }
        public bool? IsActive { get { return ActivityState == "Suspend" ? null : ActivityState == "Active"; } set { } }
    }
}
