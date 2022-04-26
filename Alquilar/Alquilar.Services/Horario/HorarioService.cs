using Alquilar.DAL;
using Alquilar.Helpers.Consts;
using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Alquilar.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alquilar.Services
{
    public class HorarioService
    {
        #region Members
        private readonly HorarioRepo _horarioRepo;
        private readonly InmuebleService _inmuebleService;
        private readonly TurnoAsignadoService _turnoAsignadoService;
        private readonly Token _token;
        #endregion

        #region Constructor
        public HorarioService(HorarioRepo horarioRepo,
            InmuebleService inmuebleService,
            TurnoAsignadoService turnoAsignadoService,
            ITokenService tokenService)
        {
            _horarioRepo = horarioRepo;
            _inmuebleService = inmuebleService;
            _turnoAsignadoService = turnoAsignadoService;
            _token = tokenService.GetToken();
        }
        #endregion

        #region Methods
        public List<HorarioDTO> GetUserHorarios(int idUsuario)
        {
            var horarios = _horarioRepo.GetUserHorarios(idUsuario);
            return horarios.Select(h => new HorarioDTO
            {
                DiaSemana = h.DiaSemana,
                NumeroDiaSemana = DiaSemana.DiaSemanaIndex[h.DiaSemana],
                HoraInicio = h.HoraInicio,
                HoraFin = h.HoraFin
            }).ToList();
        }
        
        public HorarioInfoDTO GetHorariosForInmueble(int idInmueble)
        {
            var inmueble = _inmuebleService.GetInmuebleById(idInmueble);
            var horarios = GetUserHorarios(inmueble.IdUsuario);
            var turnosAsignados = _turnoAsignadoService.GetTurnosAsignadosParaInmueble(idInmueble, DateTime.Now);

            return new HorarioInfoDTO
            {
                DuracionTurno = inmueble.Usuario.DuracionTurno,
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

        public void UpdateHorario(int idHorario, HorarioDTO horario)
        {
            var previousHorario = _horarioRepo.GetHorarioById(idHorario);

            if (previousHorario.IdUsuario != _token.IdUsuario)
                throw new NotAuthorizedException("El Horario que intentaste modificar no te pertenece.");

            var horarioModel = new Horario
            {
                DiaSemana = horario.DiaSemana,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
            };

            _horarioRepo.UpdateHorario(idHorario, horarioModel);
            _horarioRepo.SaveChanges();
        }
        
        public void DeleteHorario(int idHorario)
        {
            _horarioRepo.DeleteHorario(idHorario);
            _horarioRepo.SaveChanges();
        }
        #endregion
    }
}
