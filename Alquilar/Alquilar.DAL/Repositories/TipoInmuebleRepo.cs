using Alquilar.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class TipoInmuebleRepo : BaseRepo
    {
        #region Constructor
        public TipoInmuebleRepo(DB db) : base(db) { }
        #endregion

        public List<TipoInmueble> GetTipoInmuebles()
        {
            var tipoInmuebles = _db.TipoInmueble.ToList();

            return tipoInmuebles;
        }

        public TipoInmueble GetTipoInmuebleById(int idTipoInmueble)
        {
            var tipoInmueble = _db.TipoInmueble.FirstOrDefault(x => x.IdTipoInmueble == idTipoInmueble);

            return tipoInmueble;
        }

        public void CreateTipoInmueble(TipoInmueble tipoInmueble)
        {
            if (tipoInmueble == null)
                throw new ArgumentNullException(nameof(tipoInmueble));

            _db.TipoInmueble.Add(tipoInmueble);
        }

        public void UpdateTipoInmueble(int idTipoInmueble, TipoInmueble newTipoInmueble)
        {
            if (newTipoInmueble == null)
                throw new ArgumentNullException(nameof(newTipoInmueble));

            var tipoInmueble = _db.TipoInmueble.FirstOrDefault(l => l.IdTipoInmueble == idTipoInmueble);

            if (tipoInmueble is null)
                throw new NotFoundException("No existe el TipoInmueble especificado");

            tipoInmueble.Nombre = newTipoInmueble.Nombre;
        }

        public void DeleteTipoInmueble(int idTipoInmueble)
        {
            var tipoInmueble = _db.TipoInmueble.FirstOrDefault(l => l.IdTipoInmueble == idTipoInmueble);

            if (tipoInmueble is null)
                throw new NotFoundException("No existe la TipoInmueble especificada");

            _db.TipoInmueble.Remove(tipoInmueble);
        }
    }
}
