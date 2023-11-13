using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using ASPNET_Core7_WebAPI_JWT.Payload;

namespace ASPNET_Core7_WebAPI_JWT.Middleware.ExceptionHandler{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            // var response = context.Response;
            Response<Object> exModel = new Response<object>();

            switch (exception)
            {
                case ApplicationException ex:
                    exModel.Success = false;
                    exModel.Message = $"ERROR {ex.Message}";
                    exModel.Error = "Application Exception Occured, please retry after sometime.";
                    _logger.LogError(ex, "Error Application {@Response}", exModel);
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
                case AuthenticationException ex: 
                    exModel.Success = false;
                    exModel.Message = $"ERROR {ex.Message }";
                    exModel.Error = "Authentication Failed.";
                    _logger.LogError(ex, "Error Authentication Exception {@Response}", exModel);
                    context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    break;
                case FileNotFoundException ex:
                    exModel.Success = false;
                    exModel.Message = $"ERROR {ex.Message }";
                    exModel.Error = "The requested resource is not found.";
                    _logger.LogError(ex, "Error File Exception {@Response}", exModel);
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
                default:
                    exModel.Success = false;
                    exModel.Message = "ERROR";
                    exModel.Error = "Internal Server Error, Please retry after sometime";
                    _logger.LogError("Error {@Response}", exModel);
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;

            }
            var exResult = JsonSerializer.Serialize(exModel);
            await context.Response.WriteAsync(exResult);
        }
    }
}