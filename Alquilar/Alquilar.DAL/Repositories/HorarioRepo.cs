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

        public List<Horario> GetUserHorarios(int idUsuario)
        {
            return _db
                .Horario
                .Include(x => x.Usuario)
                .Where(x => x.IdUsuario == idUsuario)
                .ToList();
        }

        public List<TurnoAsignado> GetTurnosAsignadosParaInmueble(int idInmueble, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = _db.TurnoAsignado.Where(x => x.IdInmueble == idInmueble);

            if (dateFrom.HasValue)
                query = query.Where(x => x.Fecha >= dateFrom);

            if (dateTo.HasValue)
                query = query.Where(x => x.Fecha <= dateTo);

            return query.ToList();
        }

        public void CreateHorario(Horario horario)
        {
            if (horario == null)
                throw new ArgumentNullException(nameof(horario));

            _db.Horario.Add(horario);
        }

        public void CreateTurno(TurnoAsignado turnoAsignado)
        {
            if (turnoAsignado == null)
                throw new ArgumentNullException(nameof(turnoAsignado));

            _db.TurnoAsignado.Add(turnoAsignado);
        }
    }
}
