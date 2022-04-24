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

        public void CreateHorario(Horario horario)
        {
            if (horario == null)
                throw new ArgumentNullException(nameof(horario));

            _db.Horario.Add(horario);
        }
    }
}
