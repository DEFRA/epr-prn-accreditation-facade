

using EPR.Accreditation.Facade.Common.Exceptions;
using System.Diagnostics;

namespace EPR.Accreditation.Facade.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ResponseCodeException responseCodeException)
            {
                context.Response.StatusCode = (int)responseCodeException.StatusCode;
                context.Response.ContentType = "text/plain"; // Set the content type as needed
                await context.Response.WriteAsync(responseCodeException.Message); // Include the exception message in the response body
            }
            // Handle other exceptions if needed
            catch (Exception ex)
            {
                // Log other exceptions
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
#if DEBUG
                Debug.WriteLine($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
#endif
            }
        }
    }
}
