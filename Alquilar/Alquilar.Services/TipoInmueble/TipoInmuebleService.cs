using Alquilar.DAL;
using Alquilar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Services
{
    public class TipoInmuebleService
    {
        #region Members
        private readonly TipoInmuebleRepo _tipoInmuebleRepo;
        #endregion

        #region Constructor
        public TipoInmuebleService(TipoInmuebleRepo tipoInmuebleRepo)
        {
            _tipoInmuebleRepo = tipoInmuebleRepo;
        }
        #endregion

        #region CRUD
        // Create
        public TipoInmueble CreateTipoInmueble(TipoInmuebleDTO tipoInmueble)
        {
            if (tipoInmueble is null)
                throw new ArgumentException("TipoInmueble no valido");

            if (string.IsNullOrEmpty(tipoInmueble.Nombre))
                throw new ArgumentException("El nombre de TipoInmueble espcificado no es valido.");

            var tipoInmuebleModel = new TipoInmueble
            {
                Nombre = tipoInmueble.Nombre,
            };

            _tipoInmuebleRepo.CreateTipoInmueble(tipoInmuebleModel);
            _tipoInmuebleRepo.SaveChanges();

            return tipoInmuebleModel;
        }

        // Read
        public List<TipoInmueble> GetTipoInmuebles()
        {
            return _tipoInmuebleRepo.GetTipoInmuebles();
        }

        public TipoInmueble GetTipoInmuebleById(int idTipoInmueble)
        {
            return _tipoInmuebleRepo.GetTipoInmuebleById(idTipoInmueble);
        }

        // Update
        public void UpdateTipoInmueble(int idTipoInmueble, TipoInmuebleDTO tipoInmueble)
        {
            var tipoInmuebleModel = new TipoInmueble
            {
                Nombre = tipoInmueble.Nombre,
            };

            _tipoInmuebleRepo.UpdateTipoInmueble(idTipoInmueble, tipoInmuebleModel);
            _tipoInmuebleRepo.SaveChanges();
        }

        // Delete
        public void DeleteTipoInmueble(int idTipoInmueble)
        {
            _tipoInmuebleRepo.DeleteTipoInmueble(idTipoInmueble);
            _tipoInmuebleRepo.SaveChanges();
        }
        #endregion
    }
}
