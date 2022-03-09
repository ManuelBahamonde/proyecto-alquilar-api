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
    public class LocalidadController : ControllerBase
    {
        #region Members
        private readonly LocalidadService _localidadService;
        #endregion

        #region Constructor
        public LocalidadController(LocalidadService localidadService)
        {
            _localidadService = localidadService;
        }
        #endregion

        // TODO: avoid repeating try catches. Find a way to make endpoints code to be inside a global try catch
        #region Endpoints
        [HttpGet]
        public IActionResult GetLocalidades()
        {
            var localidades = _localidadService.GetLocalidades();

            var formattedLocalidades = localidades.Select(x => new LocalidadDTO
            {
                IdLocalidad = x.IdLocalidad,
                Nombre = x.Nombre,
                Label = $"{x.Nombre}, {x.Provincia.Nombre}"
            }).ToList();

            return Ok(formattedLocalidades);
        }

        [HttpPost]
        public IActionResult CreateLocalidad(LocalidadDTO localidad)
        {
            try
            {
               var newLocalidad = _localidadService.CreateLocalidad(localidad);

                // TODO: I'm not sure if this response is correct according to REST principles
                return CreatedAtRoute(nameof(GetLocalidad), new { idLocalidad = newLocalidad.IdLocalidad }, newLocalidad);
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

        [HttpGet("{idLocalidad}", Name = "GetLocalidad")]
        public IActionResult GetLocalidad(int idLocalidad)
        {
            var localidadModel = _localidadService.GetLocalidadById(idLocalidad);

            if (localidadModel is null)
                return NotFound();

            return Ok(new LocalidadDTO
            {
                IdLocalidad = localidadModel.IdLocalidad,
                IdProvincia = localidadModel.IdProvincia,
                Nombre = localidadModel.Nombre,
                Label = $"{localidadModel.Nombre}, {localidadModel.Provincia.Nombre}"
            });
        }

        [HttpPut("{idLocalidad}")]
        public IActionResult UpdateLocalidad(int idLocalidad, LocalidadDTO localidad)
        {
            try
            {
                _localidadService.UpdateLocalidad(idLocalidad, localidad);

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

        [HttpDelete("{idLocalidad}")]
        public IActionResult DeleteLocalidad(int idLocalidad)
        {
            try
            {
                _localidadService.DeleteLocalidad(idLocalidad);

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
