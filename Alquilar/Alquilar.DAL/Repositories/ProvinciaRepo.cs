using Alquilar.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class ProvinciaRepo
    {
        #region Members
        private readonly DB _db;
        #endregion

        #region Constructor
        public ProvinciaRepo(DB db)
        {
            _db = db;
        }
        #endregion

        public List<Provincia> GetProvincias()
        {
            var provincias = _db.Provincia.ToList();

            return provincias;
        }

        public Provincia GetProvinciaById(int idProvincia)
        {
            var provincia = _db.Provincia.FirstOrDefault(x => x.IdProvincia == idProvincia);

            return provincia;
        }

        public void CreateProvincia(Provincia provincia)
        {
            if (provincia == null)
                throw new ArgumentNullException(nameof(provincia));

            _db.Provincia.Add(provincia);
        }

        public void UpdateProvincia(int idProvincia, Provincia newProvincia)
        {
            if (newProvincia == null)
                throw new ArgumentNullException(nameof(newProvincia));

            var provincia = _db.Provincia.FirstOrDefault(l => l.IdProvincia == idProvincia);

            if (provincia is null)
                throw new NotFoundException("No existe la Provincia especificada");

            provincia.Nombre = newProvincia.Nombre;
        }

        public void DeleteProvincia(int idProvincia)
        {
            var provincia = _db.Provincia.FirstOrDefault(l => l.IdProvincia == idProvincia);

            if (provincia is null)
                throw new NotFoundException("No existe la Provincia especificada");

            _db.Provincia.Remove(provincia);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
