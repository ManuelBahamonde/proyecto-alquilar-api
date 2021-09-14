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

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
