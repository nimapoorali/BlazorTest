namespace BlazorTest.Models
{
    public class NPResult
    {
        public bool IsSuccess { get; set; }
        public IReadOnlyList<string> Successes { get; set; }
        public bool IsFailed { get; set; }
        public IReadOnlyList<string> Errors { get; set; }
    }

    public class NPResult<T> : NPResult
    {
        public NPResult() : base()
        {
        }
        public T? Value { get; set; }
    }
}
