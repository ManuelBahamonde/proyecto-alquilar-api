using Alquilar.DAL;
using Alquilar.Helpers.Consts;
using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alquilar.Services
{
    public class TurnoAsignadoService
    {
        #region Members
        private readonly TurnoAsignadoRepo _turnoAsignadoRepo;
        private readonly InmuebleService _inmuebleService;
        #endregion

        #region Constructor
        public TurnoAsignadoService(TurnoAsignadoRepo turnoAsignadoRepo,
            InmuebleService inmuebleService)
        {
            _turnoAsignadoRepo = turnoAsignadoRepo;
            _inmuebleService = inmuebleService;
        }
        #endregion

        #region Methods
        public TurnoAsignado AsignarTurno(TurnoAsignadoDTO turnoAsignado)
        {
            if (turnoAsignado is null)
                throw new ArgumentException("Turno no valido.");

            if (turnoAsignado.Fecha <= DateTime.Now)
                throw new ArgumentException("La fecha del turno no es valida.");

            var inmueble = _inmuebleService.GetInmuebleById(turnoAsignado.IdInmueble);
            if (inmueble == null)
                throw new NotFoundException("No existe el Inmueble especificado.");

            var horariosDueño = _turnoAsignadoRepo.GetUserHorarios(inmueble.IdUsuario);
            if (!horariosDueño.Any(x => DiaSemana.DiaSemanaIndex[x.DiaSemana] == ((int)turnoAsignado.Fecha.DayOfWeek - 1)
                && turnoAsignado.Fecha.TimeOfDay.Ticks >= x.HoraInicio.TimeOfDay.Ticks
                && turnoAsignado.Fecha.TimeOfDay.Ticks <= x.HoraFin.Ticks))
                throw new ArgumentException("El Dueño no recibe visitas en el horario especificado.");

            var turnosAsignadosParaFechaSolicitada = GetTurnosAsignadosParaInmueble(turnoAsignado.IdInmueble, turnoAsignado.Fecha, turnoAsignado.Fecha);
            if (turnosAsignadosParaFechaSolicitada.Count > 0)
                throw new ArgumentException("La fecha solicitada ya esta reservada.");

            var turnoModel = new TurnoAsignado
            {
                IdUsuario = turnoAsignado.IdUsuario,
                IdInmueble = turnoAsignado.IdInmueble,
                Fecha = turnoAsignado.Fecha,
                Estado = null, // TODO: make this variable to work if needed
            };

            _turnoAsignadoRepo.CreateTurno(turnoModel);
            _turnoAsignadoRepo.SaveChanges();

            // TODO: send email to owner to let him know about the new turno

            return turnoModel;
        }

        public List<TurnoAsignadoDTO> GetTurnosAsignadosParaInmueble(int idInmueble, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var turnosAsignadosParaFechaSolicitada = _turnoAsignadoRepo.GetTurnosAsignadosParaInmueble(idInmueble, dateFrom, dateTo);

            return turnosAsignadosParaFechaSolicitada.Select(x => new TurnoAsignadoDTO
            {
                IdUsuario = x.IdUsuario,
                IdInmueble = x.IdInmueble,
                Fecha = x.Fecha
            }).ToList();
        }
        #endregion

    }
}
