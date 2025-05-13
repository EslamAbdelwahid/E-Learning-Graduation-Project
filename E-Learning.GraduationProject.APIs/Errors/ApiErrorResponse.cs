
namespace E_Learning.GraduationProject.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public ApiErrorResponse(int statusCode, string? errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetDefaultMessage(statusCode);
        }


        private string? GetDefaultMessage(int statusCode)
        {
            var message = statusCode switch
            {
                400 => "A bad Request You have Made",
                401 => "You are not Authorized",
                404 => "Resource was not found",
                500 => "Server Error",
                _ => null
            };
            return message;
        }
    }
}
