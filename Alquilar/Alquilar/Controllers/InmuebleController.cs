using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Alquilar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InmuebleController : ControllerBase
    {
        #region Members
        private readonly InmuebleService _inmuebleService;
        #endregion

        #region Constructor
        public InmuebleController(InmuebleService inmuebleService)
        {
            _inmuebleService = inmuebleService;
        }
        #endregion

        // TODO: avoid repeating try catches. Find a way to make endpoints code to be inside a global try catch
        #region Endpoints
        [HttpGet]
        public IActionResult GetInmuebles()
        {
            var inmuebles = _inmuebleService.GetInmuebles();

            var formattedInmuebles = inmuebles.Select(x => new InmuebleDTO
            {
                IdInmueble = x.IdInmueble,
                Direccion = x.Direccion,
                Precio = x.Precio,
                NombreVendedor = x.Usuario.Nombre,
                NombreTipoInmueble = x.TipoInmueble.Descripcion,
                Ubicacion = $"{x.Localidad.Nombre}, {x.Localidad.Provincia.Nombre}",
                UrlImagenPresentacion = x.Imagenes.FirstOrDefault()?.Url
            }).ToList();

            return Ok(formattedInmuebles);
        }

        [HttpPost]
        public IActionResult CreateInmueble(InmuebleDTO inmueble)
        {
            try
            {
               var newInmueble = _inmuebleService.CreateInmueble(inmueble);

                // TODO: I'm not sure if this response is correct according to REST principles
                return CreatedAtRoute(nameof(GetInmueble), new { idInmueble = newInmueble.IdInmueble }, newInmueble);
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

        [HttpGet("{idInmueble}", Name = "GetInmueble")]
        public IActionResult GetInmueble(int idInmueble)
        {
            var inmuebleModel = _inmuebleService.GetInmuebleById(idInmueble);

            if (inmuebleModel is null)
                return NotFound();

            return Ok(new InmuebleDTO
            {
                IdInmueble = inmuebleModel.IdInmueble,
                Direccion = inmuebleModel.Direccion,
                Piso = inmuebleModel.Piso,
                Departamento = inmuebleModel.Departamento,
                Precio = inmuebleModel.Precio,
                Habitaciones = inmuebleModel.Habitaciones,
                Baños = inmuebleModel.Baños,
                Ambientes = inmuebleModel.Ambientes,
                FechaHastaAlquilada = inmuebleModel.FechaHastaAlquilada,
                Imagenes = inmuebleModel.Imagenes.Select(i => new ImagenDTO 
                {
                    Url = i.Url
                }).ToList(),

                NombreVendedor = inmuebleModel.Usuario.Nombre,
                EmailVendedor = inmuebleModel.Usuario.Email,
                TelefonoVendedor = inmuebleModel.Usuario.Telefono,
                NombreTipoInmueble = inmuebleModel.TipoInmueble.Descripcion,
            });
        }

        [HttpPut("{idInmueble}")]
        public IActionResult UpdateInmueble(int idInmueble, InmuebleDTO inmueble)
        {
            try
            {
                _inmuebleService.UpdateInmueble(idInmueble, inmueble);

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

        [HttpDelete("{idInmueble}")]
        public IActionResult DeleteInmueble(int idInmueble)
        {
            try
            {
                _inmuebleService.DeleteInmueble(idInmueble);

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
