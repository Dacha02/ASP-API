using ASPProjekatCarRental.Application.Exceptions;
using ASPProjekatCarRental.Application.Logging;
using FluentValidation;

namespace ASPProjekatCarRental.Api.Core
{
    public class GlobalExceptionHandler
    {

        private readonly RequestDelegate _next;
        private readonly IExceptionLogger _logger;

        public GlobalExceptionHandler(RequestDelegate requestDelegate, IExceptionLogger logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);

                httpContext.Response.ContentType = "application/json";
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;

                if(ex is FobiddenUseCaseExecutionException)
                {
                    statusCode = StatusCodes.Status403Forbidden;
                }

                if(ex is EntityNotFoundException)
                {
                    statusCode = StatusCodes.Status404NotFound;
                }

                if(ex is ValidationException exc)
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    response = new
                    {
                        errors = exc.Errors.Select(x=> new
                        {
                            propery = x.PropertyName,
                            error = x.ErrorMessage
                        })
                    };
                }

                if (ex is UseCaseConflictException conflixExc)
                {
                    statusCode = StatusCodes.Status409Conflict;
                    response = new { message = conflixExc.Message };
                }

                httpContext.Response.StatusCode = statusCode;
                if(response != null)
                {
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }
    }
}
