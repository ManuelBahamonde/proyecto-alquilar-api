using Alquilar.DAL;
using Alquilar.Models;
using System;
using System.Collections.Generic;

namespace Alquilar.Services
{
    public class LocalidadService
    {
        #region Members
        private readonly LocalidadRepo _localidadRepo;
        #endregion

        #region Constructor
        public LocalidadService(LocalidadRepo localidadRepo)
        {
            _localidadRepo = localidadRepo;
        }
        #endregion

        #region CRUD
        // Create
        public Localidad CreateLocalidad(LocalidadDTO localidad)
        {
            if (localidad is null)
                throw new ArgumentException("Localidad no valida");

            if (string.IsNullOrEmpty(localidad.Nombre))
                throw new ArgumentException("El nombre de Localidad espcificado no es valido.");

            var localidadModel = new Localidad
            {
                IdProvincia = localidad.IdProvincia,
                Nombre = localidad.Nombre,
            };

            _localidadRepo.CreateLocalidad(localidadModel);
            _localidadRepo.SaveChanges();

            return localidadModel;
        }

        // Read
        public List<Localidad> GetLocalidades()
        {
            return _localidadRepo.GetLocalidades();
        }

        public Localidad GetLocalidadById(int idLocalidad)
        {
            return _localidadRepo.GetLocalidadById(idLocalidad);
        }

        // Update
        public void UpdateLocalidad(int idLocalidad, LocalidadDTO localidad)
        {
            var localidadModel = new Localidad
            {
                IdProvincia = localidad.IdProvincia,
                Nombre = localidad.Nombre,
            };

            _localidadRepo.UpdateLocalidad(idLocalidad, localidadModel);
            _localidadRepo.SaveChanges();
        }

        // Delete
        public void DeleteLocalidad(int idLocalidad)
        {
            _localidadRepo.DeleteLocalidad(idLocalidad);
            _localidadRepo.SaveChanges();
        }
        #endregion



    }
}
