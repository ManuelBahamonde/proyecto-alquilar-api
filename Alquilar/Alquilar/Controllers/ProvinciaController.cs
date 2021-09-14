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
    public class ProvinciaController : ControllerBase
    {
        #region Members
        private readonly ProvinciaRepo _provinciaRepo;
        #endregion

        #region Constructor
        public ProvinciaController(ProvinciaRepo provinciaRepo)
        {
            _provinciaRepo = provinciaRepo;
        }
        #endregion

        #region Endpoints
        [HttpGet]
        public IActionResult GetProvincias()
        {
            var provincias = _provinciaRepo.GetProvincias();

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
            if (provincia is null)
                return BadRequest("Solicitud no válida");

            if (string.IsNullOrEmpty(provincia.Nombre))
                return BadRequest("El nombre de localidad espcificado no es valido.");

            var provinciaModel = new Provincia
            {
                Nombre = provincia.Nombre,
            };

            try
            {
                _provinciaRepo.CreateProvincia(provinciaModel);
                _provinciaRepo.SaveChanges();
            }
            catch
            {
                return StatusCode(500);
            }

            return CreatedAtRoute(nameof(GetProvincia), new { idProvincia = provinciaModel.IdProvincia }, provinciaModel);
        }

        [HttpGet("{idProvincia}", Name = "GetProvincia")]
        public IActionResult GetProvincia(int idProvincia)
        {
            var provinciaModel = _provinciaRepo.GetProvinciaById(idProvincia);

            if (provinciaModel is null)
                return NotFound();

            return Ok(new ProvinciaDTO
            {
                IdProvincia = provinciaModel.IdProvincia,
                Nombre = provinciaModel.Nombre,
            });
        }
        #endregion
    }
}
