using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alquilar.DAL
{
    public class TurnoAsignadoRepo : BaseRepo
    {
        #region Constructor
        public TurnoAsignadoRepo(DB db) : base(db) { }
        #endregion

        public List<TurnoAsignado> GetTurnosAsignadosParaInmueble(int idInmueble, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = _db.TurnoAsignado.Where(x => x.IdInmueble == idInmueble);

            if (dateFrom.HasValue)
                query = query.Where(x => x.Fecha >= dateFrom);

            if (dateTo.HasValue)
                query = query.Where(x => x.Fecha <= dateTo);

            return query.ToList();
        }

        public void CreateTurno(TurnoAsignado turnoAsignado)
        {
            if (turnoAsignado == null)
                throw new ArgumentNullException(nameof(turnoAsignado));

            _db.TurnoAsignado.Add(turnoAsignado);
        }

        public List<Horario> GetUserHorarios(int idUsuario)
        {
            // Repeating HorarioRepo code to avoid circular dependencies
            return _db
                .Horario
                .Include(x => x.Usuario)
                .Where(x => x.IdUsuario == idUsuario)
                .ToList();
        }
    }
}
