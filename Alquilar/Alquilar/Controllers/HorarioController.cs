using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Alquilar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HorarioController : ControllerBase
    {

        #region Members
        private readonly HorarioService _horarioService;
        #endregion

        #region Constructor
        public HorarioController(HorarioService horarioService)
        {
            _horarioService = horarioService;
        }
        #endregion

        #region Endpoints
        [HttpGet("{idInmueble}")]
        public IActionResult GetHorariosForInmueble(int idInmueble)
        {
            var horarios = _horarioService.GetHorariosForInmueble(idInmueble);

            return Ok(horarios);
        }
        #endregion
    }
}
