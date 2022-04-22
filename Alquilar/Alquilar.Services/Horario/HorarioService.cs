using Alquilar.DAL;
using Alquilar.Helpers.Consts;
using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alquilar.Services
{
    public class HorarioService
    {
        #region Members
        private readonly HorarioRepo _horarioRepo;
        private readonly InmuebleRepo _inmuebleRepo;
        #endregion

        #region Constructor
        public HorarioService(HorarioRepo horarioRepo,
            InmuebleRepo inmuebleRepo)
        {
            _horarioRepo = horarioRepo;
            _inmuebleRepo = inmuebleRepo;
        }
        #endregion

        #region Methods
        public HorarioInfoDTO GetHorariosForInmueble(int idInmueble)
        {
            var inmueble = _inmuebleRepo.GetInmuebleById(idInmueble);
            var horarios = _horarioRepo.GetUserHorarios(inmueble.IdUsuario);
            var turnosAsignados = _horarioRepo.GetTurnosAsignadosParaInmueble(idInmueble, DateTime.Now);

            return new HorarioInfoDTO
            {
                Horarios = horarios.Select(h => new HorarioDTO
                {
                    DiaSemana = h.DiaSemana,
                    NumeroDiaSemana = DiaSemana.DiaSemanaIndex[h.DiaSemana],
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin
                }).ToList(),
                FechasReservadas = turnosAsignados.Select(f => f.Fecha).ToList(),
            };
        }
        
        public TurnoAsignado AsignarTurno(TurnoAsignadoDTO turnoAsignado)
        {
            if (turnoAsignado is null)
                throw new ArgumentException("Turno no valido.");

            if (turnoAsignado.Fecha <= DateTime.Now)
                throw new ArgumentException("La fecha del turno no es valida.");

            var inmueble = _inmuebleRepo.GetInmuebleById(turnoAsignado.IdInmueble);
            if (inmueble == null)
                throw new NotFoundException("No existe el Inmueble especificado.");

            var turnosAsignadosParaFechaSolicitada = _horarioRepo.GetTurnosAsignadosParaInmueble(turnoAsignado.IdInmueble, turnoAsignado.Fecha, turnoAsignado.Fecha);
            if (turnosAsignadosParaFechaSolicitada.Count > 0)
                throw new ArgumentException("La fecha solicitada ya esta reservada.");

            var turnoModel = new TurnoAsignado
            {
                IdUsuario = turnoAsignado.IdUsuario,
                IdInmueble = turnoAsignado.IdInmueble,
                Fecha = turnoAsignado.Fecha,
                Estado = null, // TODO: make this variable to work if needed
            };

            _horarioRepo.CreateTurno(turnoModel);
            _horarioRepo.SaveChanges();

            return turnoModel;
        }

        public Horario CreateHorario(HorarioDTO horario)
        {
            if (!DiaSemana.DiaSemanaIndex.Keys.Contains(horario.DiaSemana))
                throw new ArgumentException("Algun dia de los horarios no es valido.");

            if (!horario.IdUsuario.HasValue)
                throw new ArgumentNullException("Usuario no valido.");

            var horarioModel = new Horario
            {
                DiaSemana = horario.DiaSemana,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                IdUsuario = horario.IdUsuario.Value
            };

            _horarioRepo.CreateHorario(horarioModel);
            _horarioRepo.SaveChanges();

            return horarioModel;
        }
        #endregion
    }
}
