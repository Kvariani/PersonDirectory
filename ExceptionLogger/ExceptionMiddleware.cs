using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PersonDirectory.Api.Extensions
{
    public static partial class ExceptionMiddlewareExtensions
    {
        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _next;
            public ExceptionMiddleware(RequestDelegate next) => _next = next;

            public async Task InvokeAsync(HttpContext httpContext)
            {
                try
                {
                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, ex.Message);
                    await HandleGlobalExceptionAsync(httpContext, ex);
                }
            }

            private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Something went wrong !Internal Server Error"
                }.ToString());
            }
        }
    }
}