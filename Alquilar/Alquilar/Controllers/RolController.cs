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
    public class RolController : ControllerBase
    {
        #region Members
        private readonly RolService _rolService;
        #endregion

        #region Constructor
        public RolController(RolService rolService)
        {
            _rolService = rolService;
        }
        #endregion

        #region Endpoints
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _rolService.GetRoles();

            var formattedRoles = roles.Select(x => new RolDTO
            {
                IdRol = x.IdRol,
                Descripcion = x.Descripcion,
            }).ToList();

            return Ok(formattedRoles);
        }

        [HttpPost]
        public IActionResult CreateRol(RolDTO rol)
        {
            var newRol = _rolService.CreateRol(rol);

            
            return CreatedAtRoute(nameof(GetRol), new { idRol = newRol.IdRol }, newRol);
        }

        [HttpGet("{idRol}", Name = "GetRol")]
        public IActionResult GetRol(int idRol)
        {
            var rolModel = _rolService.GetRolById(idRol);

            if (rolModel is null)
                return NotFound();

            return Ok(new RolDTO
            {
                IdRol = rolModel.IdRol,
                Descripcion = rolModel.Descripcion,
            });
        }



        [HttpPut("{idRol}")]
        public IActionResult UpdateRol(int idRol, RolDTO rol)
        {
            _rolService.UpdateRol(idRol, rol);

            
            return NoContent();
        }

        [HttpDelete("{idRol}")]
        public IActionResult DeleteRol(int idRol)
        {
            _rolService.DeleteRol(idRol);

            
            return NoContent();
        }
        #endregion
    }
}
