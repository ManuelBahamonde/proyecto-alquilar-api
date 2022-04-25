using Alquilar.Helpers.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alquilar.DAL
{
    public class HorarioRepo : BaseRepo
    {
        #region Constructor
        public HorarioRepo(DB db) : base(db) { }
        #endregion

        public Horario GetHorarioById(int idHorario)
        {
            return _db
                .Horario
                .FirstOrDefault(x => x.IdHorario == idHorario);
        }

        public List<Horario> GetUserHorarios(int idUsuario)
        {
            return _db
                .Horario
                .Include(x => x.Usuario)
                .Where(x => x.IdUsuario == idUsuario)
                .ToList();
        }

        public void CreateHorario(Horario horario)
        {
            if (horario == null)
                throw new ArgumentNullException(nameof(horario));

            _db.Horario.Add(horario);
        }

        public void UpdateHorario(int idHorario, Horario newHorario)
        {
            if (newHorario == null)
                throw new ArgumentNullException(nameof(newHorario));

            var horario = _db.Horario.FirstOrDefault(h => h.IdHorario == newHorario.IdHorario);

            if (horario is null)
                throw new NotFoundException("No existe el Horario especificado");

            horario.DiaSemana = horario.DiaSemana;
            horario.HoraInicio = horario.HoraInicio;
            horario.HoraFin = horario.HoraFin;
        }

        public void DeleteHorario(int idHorario)
        {
            var horario = _db.Horario.FirstOrDefault(l => l.IdHorario == idHorario);

            if (horario is null)
                throw new NotFoundException("No existe el Horario especificado.");

            _db.Horario.Remove(horario);
        }
    }
}
