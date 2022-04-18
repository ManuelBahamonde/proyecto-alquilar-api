using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alquilar.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        #region Members
        private readonly AuthService _authService;
        private readonly UsuarioService _usuarioService;
        #endregion

        #region Constructor
        public AuthController(AuthService authService,
            UsuarioService usuarioService)
        {
            _authService = authService;
            _usuarioService = usuarioService;
        }
        #endregion

        #region Endpoints
        [HttpPost]
        public IActionResult Login(UsuarioDTO usuario)
        {
            var token = _authService.Login(usuario);

            return Ok(token);
        }

        [HttpPost]
        public IActionResult Register(UsuarioDTO usuario)
        {
            var newUsuario = _authService.Register(usuario);

            return CreatedAtRoute(nameof(Register), new { idUsuario = newUsuario.IdUsuario }, newUsuario);
        }
        #endregion
    }
}
