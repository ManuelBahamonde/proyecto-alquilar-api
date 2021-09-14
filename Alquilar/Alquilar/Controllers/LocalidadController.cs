using Alquilar.DAL;
using Alquilar.Models;
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
        private readonly LocalidadRepo _localidadRepo;
        #endregion

        #region Constructor
        public LocalidadController(LocalidadRepo localidadRepo)
        {
            _localidadRepo = localidadRepo;
        }
        #endregion

        #region Endpoints
        [HttpGet]
        public IActionResult GetLocalidades()
        {
            var localidades = _localidadRepo.GetLocalidades();

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
            if (localidad is null)
                return BadRequest("Solicitud no válida");

            if (string.IsNullOrEmpty(localidad.Nombre))
                return BadRequest("El nombre de localidad espcificado no es valido.");

            var localidadModel = new Localidad
            {
                IdProvincia = localidad.IdProvincia,
                Nombre = localidad.Nombre,
            };

            try
            {

                _localidadRepo.CreateLocalidad(localidadModel);
                _localidadRepo.SaveChanges();
            }
            catch
            {
                return StatusCode(500);
            }

            return CreatedAtRoute(nameof(GetLocalidad), new { idLocalidad = localidadModel.IdLocalidad }, localidadModel);
        }

        [HttpGet("{idLocalidad}", Name = "GetLocalidad")]
        public IActionResult GetLocalidad(int idLocalidad)
        {
            var localidadModel = _localidadRepo.GetLocalidadById(idLocalidad);

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
        #endregion
    }
}
