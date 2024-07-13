using CleverPointApi.Models;

namespace CleverPointApi.Controllers
{
    public class ControllerTools
    {
        private readonly CleverPointDbContext _context;

        public ControllerTools()
        {
        }

        public static ErrorResponse CreateErrorResponse(Exception e)
        {

            ErrorResponse errorResponse = new ErrorResponse()
            {
                Message = e.Message,
                InnerExceptionMessage = e.InnerException != null ? e.InnerException.Message : null
            };

            return errorResponse;
        }
    }
}
