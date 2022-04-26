using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Alquilar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        #region Members
        private readonly UsuarioService _usuarioService;
        #endregion

        #region Constructor
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        #endregion

        #region Endpoints
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = _usuarioService.GetUsuarios();

            return Ok(usuarios);
        }

        [HttpGet("{idUsuario}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int idUsuario)
        {
            var usuario = _usuarioService.GetUsuarioById(idUsuario);

            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPut("{idUsuario}")]
        public IActionResult UpdateUsuario(int idUsuario, UsuarioDTO usuario)
        {
            _usuarioService.UpdateUsuario(idUsuario, usuario);

            return NoContent();
        }

        [HttpDelete("{idUsuario}")]
        public IActionResult DeleteUsuario(int idUsuario)
        {
            _usuarioService.DeleteUsuario(idUsuario);

            
            return NoContent();
        }

        [HttpGet("verificacion")]
        public IActionResult GetUsuariosPorVerificar()
        {
            var usuarios = _usuarioService.GetUsuariosPorVerificar();

            return Ok(usuarios);
        }
        
        [HttpPost("verificacion/{idUsuario}")]
        public IActionResult VerifyUsuario(int idUsuario)
        {
            _usuarioService.VerifyUsuario(idUsuario);

            return Ok();
        }
        #endregion
    }
}
