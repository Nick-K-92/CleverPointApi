namespace CleverPointApi.Models
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public string? Message { get; set; }
        public string? InnerExceptionMessage { get; set; }
    }
}
