using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Entities
{
    public class ApiResponseWrapperFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult obj && obj.Value != null)
            {
                var type = obj.Value.GetType();
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ApiResponse<>))
                {
                    await next();
                    return;
                }
            }

            switch (context.Result)
            {
                case OkResult:
                    context.Result = new ObjectResult(ApiResponse<object>.Ok(null))
                    { StatusCode = StatusCodes.Status200OK };
                    break;

                case OkObjectResult ok:
                    context.Result = new ObjectResult(ApiResponse<object>.Ok(ok.Value))
                    { StatusCode = StatusCodes.Status200OK };
                    break;

                case CreatedResult created:
                    context.Result = new ObjectResult(ApiResponse<object>.Ok(created.Value))
                    { StatusCode = StatusCodes.Status201Created };
                    break;

                case BadRequestObjectResult bad:
                    context.Result = new ObjectResult(ApiResponse<object>.Fail("Bad Request", bad.Value))
                    { StatusCode = StatusCodes.Status400BadRequest };
                    break;

                case NotFoundObjectResult nf:
                    context.Result = new ObjectResult(ApiResponse<object>.Fail("Not Found", nf.Value))
                    { StatusCode = StatusCodes.Status404NotFound };
                    break;

                case NotFoundResult:
                    context.Result = new ObjectResult(ApiResponse<object>.Fail("Not Found"))
                    { StatusCode = StatusCodes.Status404NotFound };
                    break;

                case UnauthorizedResult:
                    context.Result = new ObjectResult(ApiResponse<object>.Fail("Unauthorized"))
                    { StatusCode = StatusCodes.Status401Unauthorized };
                    break;

                case ForbidResult:
                    context.Result = new ObjectResult(ApiResponse<object>.Fail("Forbidden"))
                    { StatusCode = StatusCodes.Status403Forbidden };
                    break;
            }

            await next();
        }
    }
}
