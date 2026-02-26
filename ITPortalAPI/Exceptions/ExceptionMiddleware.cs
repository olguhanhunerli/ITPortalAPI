using ITPortal.Entities;
using System.Net;
using System.Text.Json;

namespace ITPortalAPI.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var payload = ApiResponse<object>.Fail("Sunucu hatası", new
                {
                    ex.Message,
                    ex.StackTrace
                });

                await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
            }
        }
    }
}
