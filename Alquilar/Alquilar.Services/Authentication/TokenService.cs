using Alquilar.DAL;
using Alquilar.Helpers.Consts;
using Alquilar.Models;
using Alquilar.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alquilar.Services
{
    public class TokenService : ITokenService
    {
        #region Members
        private Token _token;
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructor
        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }
        #endregion

        #region Methods
        public Token GenerateToken(Usuario user)
        {
            var jwt = GenerateJWT(user);

            return new Token
            {
                Bearer = jwt,
                Nombre = user.Nombre,
                Email = user.Email,
                NombreUsuario = user.NombreUsuario,
                NombreRol = user.Rol.Descripcion
            };
        }

        public Token GetToken()
        {
            return _token;
        }

        public void SetToken(Token token)
        {
            _token = token;
        }
        #endregion

        #region Helpers
        private string GenerateJWT(Usuario user)
        {
            // Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(CustomClaimTypes.Email, user.Email),
                new Claim(CustomClaimTypes.NombreUsuario, user.NombreUsuario),
                new Claim(CustomClaimTypes.NombreRol, user.Rol.Descripcion),
            };

            // Issue Token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwtSettings.DaysBeforeExpiration),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        #endregion
    }
}
