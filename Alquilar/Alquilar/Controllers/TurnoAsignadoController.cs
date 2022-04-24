using Alquilar.Models;
using Alquilar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Alquilar.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurnoAsignadoController : ControllerBase
    {
        #region Members
        private readonly TurnoAsignadoService _turnoAsignadoService;
        #endregion

        #region Constructor
        public TurnoAsignadoController(TurnoAsignadoService turnoAsignadoService)
        {
            _turnoAsignadoService = turnoAsignadoService;
        }
        #endregion

        #region Endpoints
        [HttpPost]
        public IActionResult AsignarTurno(TurnoAsignadoDTO turnoAsignado)
        {
            var newTurno = _turnoAsignadoService.AsignarTurno(turnoAsignado);

            return StatusCode((int)HttpStatusCode.Created, newTurno);
        }
        #endregion
    }
}
