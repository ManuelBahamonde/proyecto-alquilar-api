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
                NombreTipoInmueble = x.TipoInmueble.Nombre,
                Ubicacion = x.Localidad.NombreCompleto,
                UrlImagenPresentacion = x.Imagenes.FirstOrDefault()?.Url
            }).ToList();

            return Ok(formattedInmuebles);
        }

        [HttpPost]
        public IActionResult CreateInmueble(InmuebleDTO inmueble)
        {
            var newInmueble = _inmuebleService.CreateInmueble(inmueble);

            
            return CreatedAtRoute(nameof(GetInmueble), new { idInmueble = newInmueble.IdInmueble }, newInmueble);
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
                IdLocalidad= inmuebleModel.IdLocalidad,
                NombreCompletoLocalidad= inmuebleModel.Localidad.NombreCompleto,
                IdTipoInmueble = inmuebleModel.IdTipoInmueble,
                IdUsuario = inmuebleModel.IdUsuario,
                FechaHastaAlquilada = inmuebleModel.FechaHastaAlquilada.HasValue && inmuebleModel.FechaHastaAlquilada > DateTime.Now ? inmuebleModel.FechaHastaAlquilada : null,
                Imagenes = inmuebleModel.Imagenes.Select(i => new ImagenDTO 
                {
                    IdImagen = i.IdImagen,
                    Url = i.Url,
                    IdInmueble = i.IdInmueble,
                    IdUsuario = i.IdUsuario
                }).ToList(),

                NombreVendedor = inmuebleModel.Usuario.Nombre,
                EmailVendedor = inmuebleModel.Usuario.Email,
                TelefonoVendedor = inmuebleModel.Usuario.Telefono,
                NombreTipoInmueble = inmuebleModel.TipoInmueble.Nombre,
            });
        }

        [HttpPut("{idInmueble}")]
        public IActionResult UpdateInmueble(int idInmueble, InmuebleDTO inmueble)
        {
            _inmuebleService.UpdateInmueble(idInmueble, inmueble);

            
            return NoContent();
        }

        [HttpDelete("{idInmueble}")]
        public IActionResult DeleteInmueble(int idInmueble)
        {
            _inmuebleService.DeleteInmueble(idInmueble);

            
            return NoContent();
        }
        #endregion
    }
}
