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
            var newProvincia = _provinciaService.CreateProvincia(provincia);

            
            return CreatedAtRoute(nameof(GetProvincia), new { idProvincia = newProvincia.IdProvincia }, newProvincia);
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
            _provinciaService.UpdateProvincia(idProvincia, provincia);

            
            return NoContent();
        }

        [HttpDelete("{idProvincia}")]
        public IActionResult DeleteProvincia(int idProvincia)
        {
            _provinciaService.DeleteProvincia(idProvincia);

            
            return NoContent();
        }
        #endregion
    }
}
