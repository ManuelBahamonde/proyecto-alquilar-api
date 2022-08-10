using Alquilar.DAL;
using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Alquilar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoInmuebleController : ControllerBase
    {
        #region Members
        private readonly TipoInmuebleService _tipoInmuebleService;
        #endregion

        #region Constructor
        public TipoInmuebleController(TipoInmuebleService tipoInmuebleService)
        {
            _tipoInmuebleService = tipoInmuebleService;
        }
        #endregion

        #region Endpoints
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetTipoInmuebles()
        {
            var tipoInmuebles = _tipoInmuebleService.GetTipoInmuebles();

            var formattedTipoInmuebles = tipoInmuebles.Select(x => new TipoInmuebleDTO
            {
                IdTipoInmueble = x.IdTipoInmueble,
                Nombre = x.Nombre,
            }).ToList();

            return Ok(formattedTipoInmuebles);
        }

        [HttpPost]
        public IActionResult CreateTipoInmueble(TipoInmuebleDTO tipoInmueble)
        {
            var newTipoInmueble = _tipoInmuebleService.CreateTipoInmueble(tipoInmueble);

            
            return CreatedAtRoute(nameof(GetTipoInmueble), new { idTipoInmueble = newTipoInmueble.IdTipoInmueble }, newTipoInmueble);
        }

        [HttpGet("{idTipoInmueble}", Name = "GetTipoInmueble")]
        public IActionResult GetTipoInmueble(int idTipoInmueble)
        {
            var tipoInmuebleModel = _tipoInmuebleService.GetTipoInmuebleById(idTipoInmueble);

            if (tipoInmuebleModel is null)
                return NotFound();

            return Ok(new TipoInmuebleDTO
            {
                IdTipoInmueble = tipoInmuebleModel.IdTipoInmueble,
                Nombre = tipoInmuebleModel.Nombre,
            });
        }



        [HttpPut("{idTipoInmueble}")]
        public IActionResult UpdateTipoInmueble(int idTipoInmueble, TipoInmuebleDTO tipoInmueble)
        {
            _tipoInmuebleService.UpdateTipoInmueble(idTipoInmueble, tipoInmueble);

            
            return NoContent();
        }

        [HttpDelete("{idTipoInmueble}")]
        public IActionResult DeleteTipoInmueble(int idTipoInmueble)
        {
            _tipoInmuebleService.DeleteTipoInmueble(idTipoInmueble);

            
            return NoContent();
        }
        #endregion
    }
}
