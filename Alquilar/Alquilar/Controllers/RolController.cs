﻿using Alquilar.DAL;
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
            try
            {
                var newRol = _rolService.CreateRol(rol);

                // TODO: I'm not sure if this response is correct according to REST principles
                return CreatedAtRoute(nameof(GetRol), new { idRol = newRol.IdRol}, newRol);
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
            try
            {
                _rolService.UpdateRol(idRol, rol);

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

        [HttpDelete("{idRol}")]
        public IActionResult DeleteRol(int idRol)
        {
            try
            {
                _rolService.DeleteRol(idRol);

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