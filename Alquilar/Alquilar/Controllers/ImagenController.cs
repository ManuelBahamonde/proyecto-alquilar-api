using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Alquilar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagenController : ControllerBase
    {
        #region Members
        private readonly ImagenService _imagenService;
        #endregion

        #region Constructor
        public ImagenController(ImagenService imagenService)
        {
            _imagenService = imagenService;
        }
        #endregion

        #region Endpoints
        [HttpGet]
        public IActionResult GetImagenes()
        {
            var imagenes = _imagenService.GetImagenes();

            var formattedImagens = imagenes.Select(x => new ImagenDTO
            {
                IdImagen = x.IdImagen,
                Url = x.Url,
                IdUsuario = x.IdUsuario,
                IdInmueble = x.IdInmueble,
            }).ToList();

            return Ok(formattedImagens);
        }

        [HttpPost]
        public IActionResult CreateImagen(ImagenDTO imagen)
        {
            var newImagen = _imagenService.CreateImagen(imagen);

            
            return CreatedAtRoute(nameof(GetImagen), new { idImagen = newImagen.IdImagen }, newImagen);
        }

        [HttpGet("{idImagen}", Name = "GetImagen")]
        public IActionResult GetImagen(int idImagen)
        {
            var imagenModel = _imagenService.GetImagenById(idImagen);

            if (imagenModel is null)
                return NotFound();

            return Ok(new ImagenDTO
            {
                IdImagen = imagenModel.IdImagen,
                Url = imagenModel.Url,
                IdUsuario = imagenModel.IdUsuario,
                IdInmueble = imagenModel.IdInmueble,
            });
        }



        [HttpPut("{idImagen}")]
        public IActionResult UpdateImagen(int idImagen, ImagenDTO imagen)
        {
            _imagenService.UpdateImagen(idImagen, imagen);

            
            return NoContent();
        }

        [HttpDelete("{idImagen}")]
        public IActionResult DeleteImagen(int idImagen)
        {
            _imagenService.DeleteImagen(idImagen);

            
            return NoContent();
        }


        #endregion
    }
}
