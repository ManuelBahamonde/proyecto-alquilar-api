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
            try
            {
                var newImagen = _imagenService.CreateImagen(imagen);

                // TODO: I'm not sure if this response is correct according to REST principles
                return CreatedAtRoute(nameof(GetImagen), new { idImagen = newImagen.IdImagen}, newImagen);
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
            try
            {
                _imagenService.UpdateImagen(idImagen, imagen);

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

        [HttpDelete("{idImagen}")]
        public IActionResult DeleteImagen(int idImagen)
        {
            try
            {
                _imagenService.DeleteImagen(idImagen);

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
