using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class InmuebleRepo : BaseRepo
    {
        #region Constructor
        public InmuebleRepo(DB db) : base(db) { }
        #endregion

        public List<Inmueble> GetInmuebles(SearchInmueblesRequest rq = null)
        {
            var query = _db
                .Inmueble
                .Include(x => x.TipoInmueble)
                .Include(x => x.Localidad).ThenInclude(x => x.Provincia)
                .Include(x => x.Usuario)
                .Include(x => x.Imagenes)
                .AsQueryable();

            if (rq != null)
            {
                if (rq.Habitaciones.HasValue)
                    query = query.Where(x => x.Habitaciones == rq.Habitaciones.Value);

                if (rq.Banos.HasValue)
                    query = query.Where(x => x.Baños == rq.Banos.Value);

                if (rq.Habitaciones.HasValue)
                    query = query.Where(x => x.Habitaciones == rq.Habitaciones.Value);

                if (rq.Ambientes.HasValue)
                    query = query.Where(x => x.Ambientes == rq.Ambientes.Value);

                if (rq.FechaDisponibilidad.HasValue)
                    query = query.Where(x => !x.FechaHastaAlquilada.HasValue || x.FechaHastaAlquilada < rq.FechaDisponibilidad);

                if (rq.IdLocalidad.HasValue)
                    query = query.Where(x => x.IdLocalidad == rq.IdLocalidad.Value);

                if (rq.IdUsuario.HasValue)
                    query = query.Where(x => x.IdUsuario == rq.IdUsuario.Value);
            }

            return query.ToList();
        }

        public Inmueble GetInmuebleById(int idInmueble)
        {
            var inmueble = _db
                .Inmueble
                .Include(x => x.TipoInmueble)
                .Include(x => x.Localidad).ThenInclude(x => x.Provincia)
                .Include(x => x.Usuario)
                .Include(x => x.Imagenes)
                .FirstOrDefault(x => x.IdInmueble == idInmueble);

            return inmueble;
        }

        public void CreateInmueble(Inmueble inmueble)
        {
            if (inmueble == null)
                throw new ArgumentNullException(nameof(inmueble));

            _db.Inmueble.Add(inmueble);
        }

        public void UpdateInmueble(int idInmueble, Inmueble newInmueble)
        {
            if (newInmueble == null)
                throw new ArgumentNullException(nameof(newInmueble));

            var inmueble = _db.Inmueble.FirstOrDefault(l => l.IdInmueble == idInmueble);

            if (inmueble is null)
                throw new NotFoundException("No existe el Inmueble especificado");

            inmueble.Direccion = newInmueble.Direccion;
            inmueble.Piso = newInmueble.Piso;
            inmueble.Departamento = newInmueble.Departamento;
            inmueble.Precio = newInmueble.Precio;
            inmueble.Habitaciones = newInmueble.Habitaciones;
            inmueble.Baños = newInmueble.Baños;
            inmueble.Ambientes = newInmueble.Ambientes;
            //inmueble.IdInmuebleExterno = newInmueble.IdInmuebleExterno;
            inmueble.FechaHastaAlquilada = newInmueble.FechaHastaAlquilada;
            inmueble.IdTipoInmueble = newInmueble.IdTipoInmueble;
            inmueble.Imagenes = newInmueble.Imagenes;
            inmueble.IdLocalidad = newInmueble.IdLocalidad;
            inmueble.IdUsuario = newInmueble.IdUsuario;
        }

        public void DeleteInmueble(int idInmueble)
        {
            var inmueble = _db.Inmueble.FirstOrDefault(l => l.IdInmueble == idInmueble);

            if (inmueble is null)
                throw new NotFoundException("No existe el Inmueble especificado");

            _db.Inmueble.Remove(inmueble);
        }
    }
}