using Alquilar.Helpers.Consts;
using Alquilar.Models;
using Alquilar.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Alquilar.API.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ITokenService tokenService)
        {
            var claims = ((ClaimsIdentity)httpContext.User.Identity).Claims;

            if (claims.Any())
            {
                // Injecting Token into TokenService so it can be accessed by any service in the App
                tokenService.SetToken(new Token
                {
                    Bearer = GetRawJwt(httpContext.Request.Headers["Authorization"]),
                    Nombre = httpContext.User.Identity.Name,
                    Email = claims.FirstOrDefault(x => x.Type == CustomClaimTypes.Email)?.Value,
                    NombreUsuario = claims.FirstOrDefault(x => x.Type == CustomClaimTypes.NombreUsuario)?.Value,
                    NombreRol = claims.FirstOrDefault(x => x.Type == CustomClaimTypes.NombreRol)?.Value,
                });
            }

            await _next(httpContext);
        }
        
        private static string GetRawJwt(string authHeader)
        {
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer"))
                return "";

            return authHeader["Bearer ".Length..].Trim();
        }
    }
}
