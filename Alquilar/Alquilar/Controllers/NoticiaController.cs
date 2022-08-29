using Alquilar.DAL;
using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Authorization;
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
        public class NoticiaController : ControllerBase
        {
            #region Members
            private readonly NoticiaService _noticiaService;
            #endregion

            #region Constructor
            public NoticiaController(NoticiaService noticiaService)
            {
                _noticiaService = noticiaService;
            }
        #endregion

        #region Endpoints

        // Get
            [HttpGet]
            [AllowAnonymous]
            public IActionResult GetNoticias()
            {
                var noticias = _noticiaService.GetNoticias();

                var formattedNoticias = noticias.Select(x => new NoticiaDTO
                {
                    IdNoticia = x.IdNoticia,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                }).ToList();

                return Ok(formattedNoticias);
            }

        // GetNoticia
        [HttpGet("{idNoticia}", Name = "GetNoticia")]
            public IActionResult GetNoticia(int idNoticia)
            {
                var noticiaModel = _noticiaService.GetNoticiaById(idNoticia);

                if (noticiaModel is null)
                    return NotFound();

                return Ok(new NoticiaDTO
                {
                    IdNoticia = noticiaModel.IdNoticia,
                    Titulo = noticiaModel.Titulo,
                    Descripcion = noticiaModel.Descripcion,
                });
            }

        // Create
        // Devuelvo estado de respuesta 201
            [HttpPost]
            public IActionResult CreateNoticia(NoticiaDTO noticia)
            {
                var newNoticia = _noticiaService.CreateNoticia(noticia);

                return CreatedAtRoute(nameof(GetNoticia), new { idNoticia = newNoticia.IdNoticia }, newNoticia);
            }

        // Update
        [HttpPut("{idNoticia}")]
            public IActionResult UpdateNoticia(int idNoticia, NoticiaDTO noticia)
            {
                _noticiaService.UpdateNoticia(idNoticia, noticia);

                return NoContent();
            }

        // Delete
        [HttpDelete("{idNoticia}")]
            public IActionResult DeleteNoticia(int idNoticia)
            {
                _noticiaService.DeleteNoticia(idNoticia);

                return NoContent();
            }
            #endregion
        }
    }
