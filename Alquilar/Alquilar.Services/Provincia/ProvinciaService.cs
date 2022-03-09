using Alquilar.DAL;
using Alquilar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Services
{
    public class ProvinciaService
    {
        #region Members
        private readonly ProvinciaRepo _provinciaRepo;
        #endregion

        #region Constructor
        public ProvinciaService(ProvinciaRepo provinciaRepo)
        {
            _provinciaRepo = provinciaRepo;
        }
        #endregion

        #region CRUD
        // Create
        public Provincia CreateProvincia(ProvinciaDTO provincia)
        {
            if (provincia is null)
                throw new ArgumentException("Provincia no valida");

            if (string.IsNullOrEmpty(provincia.Nombre))
                throw new ArgumentException("El nombre de Provincia espcificado no es valido.");

            var provinciaModel = new Provincia
            {
                Nombre = provincia.Nombre,
            };

            _provinciaRepo.CreateProvincia(provinciaModel);
            _provinciaRepo.SaveChanges();

            return provinciaModel;
        }

        // Read
        public List<Provincia> GetProvincias()
        {
            return _provinciaRepo.GetProvincias();
        }

        public Provincia GetProvinciaById(int idProvincia)
        {
            return _provinciaRepo.GetProvinciaById(idProvincia);
        }

        // Update
        public void UpdateProvincia(int idProvincia, ProvinciaDTO provincia)
        {
            var provinciaModel = new Provincia
            {
                Nombre = provincia.Nombre,
            };

            _provinciaRepo.UpdateProvincia(idProvincia, provinciaModel);
            _provinciaRepo.SaveChanges();
        }

        // Delete
        public void DeleteProvincia(int idProvincia)
        {
            _provinciaRepo.DeleteProvincia(idProvincia);
            _provinciaRepo.SaveChanges();
        }
        #endregion



    }
}
