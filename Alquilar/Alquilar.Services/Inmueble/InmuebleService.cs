using Alquilar.DAL;
using Alquilar.Models;
using Alquilar.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alquilar.Services
{
    public class InmuebleService
    {
        #region Members
        private readonly InmuebleRepo _inmuebleRepo;
        private readonly Token _token;
        #endregion

        #region Constructor
        public InmuebleService(InmuebleRepo inmuebleRepo,
            ITokenService tokenService)
        {
            _inmuebleRepo = inmuebleRepo;
            _token = tokenService.GetToken();
        }
        #endregion

        #region CRUD
        // Create
        public Inmueble CreateInmueble(InmuebleDTO inmueble)
        {
            if (inmueble is null)
                throw new ArgumentException("Inmueble no valido");

            if (string.IsNullOrEmpty(inmueble.Direccion))
                throw new ArgumentException("La direccion del Inmueble espcificado no es valida.");

            var inmuebleModel = new Inmueble
            {
                Direccion = inmueble.Direccion,
                Piso = inmueble.Piso,
                Departamento = inmueble.Departamento,
                Precio = inmueble.Precio,
                Habitaciones = inmueble.Habitaciones,
                Baños = inmueble.Baños,
                Ambientes = inmueble.Ambientes,
                //IdInmuebleExterno = inmueble.IdInmuebleExterno,
                FechaHastaAlquilada = inmueble.FechaHastaAlquilada,
                IdTipoInmueble = inmueble.IdTipoInmueble,
                Imagenes = inmueble.Imagenes.Select(i => new Imagen 
                {
                    Url = i.Url,
                }).ToList(),
                IdLocalidad = inmueble.IdLocalidad,
                IdUsuario = _token.IdUsuario,
        };

            _inmuebleRepo.CreateInmueble(inmuebleModel);
            _inmuebleRepo.SaveChanges();

            return inmuebleModel;
        }

        // Read
        public List<Inmueble> GetInmuebles()
        {
            return _inmuebleRepo.GetInmuebles();
        }

        public Inmueble GetInmuebleById(int idInmueble)
        {
            return _inmuebleRepo.GetInmuebleById(idInmueble);
        }

        // Update
        public void UpdateInmueble(int idInmueble, InmuebleDTO inmueble)
        {
            var inmuebleModel = new Inmueble
            {
                Direccion = inmueble.Direccion,
                Piso = inmueble.Piso,
                Departamento = inmueble.Departamento,
                Precio = inmueble.Precio,
                Habitaciones = inmueble.Habitaciones,
                Baños = inmueble.Baños,
                Ambientes = inmueble.Ambientes,
                //IdInmuebleExterno = inmueble.IdInmuebleExterno,
                FechaHastaAlquilada = inmueble.FechaHastaAlquilada,
                IdTipoInmueble = inmueble.IdTipoInmueble,
                Imagenes = inmueble.Imagenes.Select(i => new Imagen
                {
                    Url = i.Url,
                }).ToList(),
                IdLocalidad = inmueble.IdLocalidad,
                IdUsuario = inmueble.IdUsuario
            };

            _inmuebleRepo.UpdateInmueble(idInmueble, inmuebleModel);
            _inmuebleRepo.SaveChanges();
        }

        // Delete
        public void DeleteInmueble(int idInmueble)
        {
            _inmuebleRepo.DeleteInmueble(idInmueble);
            _inmuebleRepo.SaveChanges();
        }
        #endregion

    }
}
