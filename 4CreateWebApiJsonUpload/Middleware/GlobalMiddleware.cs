using Appplication.Exceptions;
using FluentValidation;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;

namespace _4CreateWebApiJsonUpload.Middleware
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case UnsucesfullDeserializationException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ValidationException:
                        context.Response.StatusCode = (int)(HttpStatusCode.UnprocessableEntity);
                        break;
                }

                await context.Response.WriteAsJsonAsync(ex.ToString());
                context.Response.ContentType = "application/json";
            }
        }
    }
}
