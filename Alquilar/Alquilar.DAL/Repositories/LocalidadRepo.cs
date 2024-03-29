﻿using Alquilar.Helpers.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class LocalidadRepo : BaseRepo
    {
        #region Constructor
        public LocalidadRepo(DB db) : base(db) { }
        #endregion

        public List<Localidad> GetLocalidades(string searchText)
        {
            var query = _db
                .Localidad
                .Include(x => x.Provincia)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(x => x.Nombre.Contains(searchText) || x.Provincia.Nombre.Contains(searchText));

            var localidades = query.ToList();

            return localidades;
        }

        public Localidad GetLocalidadById(int idLocalidad)
        {
            var localidad = _db
                .Localidad
                .Include(x => x.Provincia)
                .FirstOrDefault(x => x.IdLocalidad == idLocalidad);

            return localidad;
        }

        public void CreateLocalidad(Localidad localidad)
        {
            if (localidad == null)
                throw new ArgumentNullException(nameof(localidad));

            _db.Localidad.Add(localidad);
        }

        public void UpdateLocalidad(int idLocalidad, Localidad newLocalidad)
        {
            if (newLocalidad == null)
                throw new ArgumentNullException(nameof(newLocalidad));

            var localidad = _db.Localidad.FirstOrDefault(l => l.IdLocalidad == idLocalidad);

            if (localidad is null)
                throw new NotFoundException("No existe la Localidad especificada");

            localidad.Nombre = newLocalidad.Nombre;
            localidad.IdProvincia = newLocalidad.IdProvincia;
            
        }

        public void DeleteLocalidad(int idLocalidad)
        {
            var localidad = _db.Localidad.FirstOrDefault(l => l.IdLocalidad == idLocalidad);

            if (localidad is null)
                throw new NotFoundException("No existe la Localidad especificada");

            _db.Localidad.Remove(localidad);
        }
    }
}