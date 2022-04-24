using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Alquilar.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ArgumentException exc)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, exc.Message);
            }
            catch (NotFoundException exc)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, exc.Message);
            }
            catch (NotAuthorizedException exc)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.Unauthorized, exc.Message);
            }
            catch (Exception exc)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, exc.Message);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(new ResponseError
            {
                Message = message
            }.ToString());
        }
    }
}
