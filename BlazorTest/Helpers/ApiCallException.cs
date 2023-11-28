using System.Net;

namespace BlazorTest.Helpers
{
    public class ApiCallException : Exception
    {
        //public int ErrorCode { get; set; }
        //public HttpStatusCode HttpStatusCode { get; set; }
        public string[] ErrorMessages { get; set; }
    }
}
