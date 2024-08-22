namespace SchoolDiary.WEB.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse()
        {
            IsSuccess = false;
            ErrorMessage = "Something went wrong...";
        }
        public ApiResponse(T result)
        {
            IsSuccess = true;
            Result = result;
            ErrorMessage = string.Empty;
        }
        public ApiResponse(Exception exception)
        {
            IsSuccess = false;
            Result = default(T);
            ErrorMessage = exception.Message;
        }
    }
}
