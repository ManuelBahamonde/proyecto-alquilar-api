﻿using Alquilar.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class TipoInmuebleRepo
    {
        #region Members
        private readonly DB _db;
        #endregion

        #region Constructor
        public TipoInmuebleRepo(DB db)
        {
            _db = db;
        }
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

            tipoInmueble.IdTipoInmueble = newTipoInmueble.IdTipoInmueble;
            tipoInmueble.Descripcion = newTipoInmueble.Descripcion;
        }

        public void DeleteTipoInmueble(int idTipoInmueble)
        {
            var tipoInmueble = _db.TipoInmueble.FirstOrDefault(l => l.IdTipoInmueble == idTipoInmueble);

            if (tipoInmueble is null)
                throw new NotFoundException("No existe la TipoInmueble especificada");

            _db.TipoInmueble.Remove(tipoInmueble);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
