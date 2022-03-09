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
    public class ProvinciaController : ControllerBase
    {
        #region Members
        private readonly ProvinciaService _provinciaService;
        #endregion

        #region Constructor
        public ProvinciaController(ProvinciaService provinciaService)
        {
            _provinciaService = provinciaService;
        }
        #endregion

        #region Endpoints
        [HttpGet]
        public IActionResult GetProvincias()
        {
            var provincias = _provinciaService.GetProvincias();

            var formattedProvincias = provincias.Select(x => new ProvinciaDTO
            {
                IdProvincia = x.IdProvincia,
                Nombre = x.Nombre,
            }).ToList();

            return Ok(formattedProvincias);
        }

        [HttpPost]
        public IActionResult CreateProvincia(ProvinciaDTO provincia)
        {
            try
            {
                var newProvincia = _provinciaService.CreateProvincia(provincia);

                // TODO: I'm not sure if this response is correct according to REST principles
                return CreatedAtRoute(nameof(GetProvincia), new { idProvincia = newProvincia.IdProvincia}, newProvincia);
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

            //if (provincia is null)
            //    return BadRequest("Solicitud no válida");

            //if (string.IsNullOrEmpty(provincia.Nombre))
            //    return BadRequest("El nombre de provincia espcificado no es valido.");

            //var provinciaModel = new Provincia
            //{
            //    Nombre = provincia.Nombre,
            //};

            //try
            //{
            //    _provinciaService.CreateProvincia(provinciaModel);
            //    _provinciaService.SaveChanges();
            //}
            //catch
            //{
            //    return StatusCode(500);
            //}

            //return CreatedAtRoute(nameof(GetProvincia), new { idProvincia = provinciaModel.IdProvincia }, provinciaModel);
        }

        [HttpGet("{idProvincia}", Name = "GetProvincia")]
        public IActionResult GetProvincia(int idProvincia)
        {
            var provinciaModel = _provinciaService.GetProvinciaById(idProvincia);

            if (provinciaModel is null)
                return NotFound();

            return Ok(new ProvinciaDTO
            {
                IdProvincia = provinciaModel.IdProvincia,
                Nombre = provinciaModel.Nombre,
            });
        }



        [HttpPut("{idProvincia}")]
        public IActionResult UpdateProvincia(int idProvincia, ProvinciaDTO provincia)
        {
            try
            {
                _provinciaService.UpdateProvincia(idProvincia, provincia);

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

        [HttpDelete("{idProvincia}")]
        public IActionResult DeleteLocalidad(int idProvincia)
        {
            try
            {
                _provinciaService.DeleteProvincia(idProvincia);

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
