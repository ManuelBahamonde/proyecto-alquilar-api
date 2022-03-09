using Alquilar.DAL;
using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // TODO: avoid repeating try catches. Find a way to make endpoints code to be inside a global try catch
        #region Endpoints
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var usuarios = _usuarioService.GetUsuarios();

            var formattedUsuarios = usuarios.Select(x => new UsuarioDTO
            {
                IdUsuario = x.IdUsuario,
                NombreUsuario = x.NombreUsuario,
                Clave = x.Clave,
                Nombre = x.Nombre,
                Telefono = x.Telefono,
                Email = x.Email,
                Direccion = x.Direccion,
                Piso = x.Piso,
                Servicio = x.Servicio,
                UrlApi = x.UrlApi,
                IdRol = x.IdRol,
                IdLocalidad = x.IdLocalidad
        }).ToList();

            return Ok(formattedUsuarios);
        }

        [HttpPost]
        public IActionResult CreateUsuario(UsuarioDTO usuario)
        {
            try
            {
               var newUsuario = _usuarioService.CreateUsuario(usuario);

                // TODO: I'm not sure if this response is correct according to REST principles
                return CreatedAtRoute(nameof(GetUsuario), new { idUsuario = newUsuario.IdUsuario }, newUsuario);
            }
            catch (ArgumentException exc)
            {
                return BadRequest(new ResponseError
                {
                    Message = exc.Message,
                });
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{idUsuario}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int idUsuario)
        {
            var usuarioModel = _usuarioService.GetUsuarioById(idUsuario);

            if (usuarioModel is null)
                return NotFound();

            return Ok(new UsuarioDTO
            {

                IdUsuario = usuarioModel.IdUsuario,
                NombreUsuario = usuarioModel.NombreUsuario,
                Clave = usuarioModel.Clave,
                Nombre = usuarioModel.Nombre,
                Telefono = usuarioModel.Telefono,
                Email = usuarioModel.Email,
                Direccion = usuarioModel.Direccion,
                Piso = usuarioModel.Piso,
                Servicio = usuarioModel.Servicio,
                UrlApi = usuarioModel.UrlApi,
                IdRol = usuarioModel.IdRol,
                IdLocalidad = usuarioModel.IdLocalidad
            });
        }

        [HttpPut("{idUsuario}")]
        public IActionResult UpdateUsuario(int idUsuario, UsuarioDTO usuario)
        {
            try
            {
                _usuarioService.UpdateUsuario(idUsuario, usuario);

                // TODO: I'm not sure if this response is correct according to REST principles
                return NoContent();
            }
            catch (ArgumentException exc)
            {
                return BadRequest(new ResponseError
                {
                    Message = exc.Message,
                });
            }
            catch (NotFoundException exc)
            {
                return NotFound(new ResponseError
                {
                    Message = exc.Message,
                });
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{idUsuario}")]
        public IActionResult DeleteUsuario(int idUsuario)
        {
            try
            {
                _usuarioService.DeleteUsuario(idUsuario);

                // TODO: I'm not sure if this response is correct according to REST principles
                return NoContent();
            }
            catch (ArgumentException exc)
            {
                return BadRequest(new ResponseError
                {
                    Message = exc.Message,
                });
            }
            catch (NotFoundException exc)
            {
                return NotFound(new ResponseError
                {
                    Message = exc.Message,
                });
            }
            catch
            {
                return StatusCode(500);
            }
        }
        #endregion
    }
}
