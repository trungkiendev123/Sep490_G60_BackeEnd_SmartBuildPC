using Sep490_G60_Backend_SmartBuildPC.Requests;
using System.Net;

namespace Sep490_G60_Backend_SmartBuildPC.Responses
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

        public String Message { get; set; }

        public TokenRefresh TokenInformation { get; set; }
    }
}
