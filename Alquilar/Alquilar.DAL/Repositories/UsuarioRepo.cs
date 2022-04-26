using Alquilar.Helpers.Consts;
using Alquilar.Helpers.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class UsuarioRepo : BaseRepo
    {
        #region Constructor
        public UsuarioRepo(DB db) : base(db) { }
        #endregion

        public List<Usuario> GetUsuarios(string rolDescription = null)
        {
            var query = _db
                .Usuario
                .Include(x => x.Rol)
                .Include(x => x.Localidad)
                .AsQueryable();

            if (!string.IsNullOrEmpty(rolDescription))
                query = query.Where(x => x.Rol.Descripcion == rolDescription);

            var usuarios = query.ToList();

            return usuarios;
        }

        public Usuario GetUsuarioById(int idUsuario)
        {
            var usuario = _db
                .Usuario
                .Include(x => x.Rol)
                .Include(x => x.Localidad)
                .Include(x => x.Horarios)
                .FirstOrDefault(x => x.IdUsuario == idUsuario);

            return usuario;
        }

        public Usuario GetUsuarioByNombreUsuario(string nombreUsuario)
        {
            var usuario = _db
                .Usuario
                .Include(x => x.Rol)
                .Include(x => x.Localidad)
                .FirstOrDefault(x => x.NombreUsuario == nombreUsuario);

            return usuario;
        }

        public void CreateUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            _db.Usuario.Add(usuario);
        }

        public void UpdateUsuario(int idUsuario, Usuario newUsuario)
        {
            if (newUsuario == null)
                throw new ArgumentNullException(nameof(newUsuario));

            var usuario = _db.Usuario.FirstOrDefault(l => l.IdUsuario == idUsuario);

            if (usuario is null)
                throw new NotFoundException("No existe el Usuario especificado");


            usuario.NombreUsuario = newUsuario.NombreUsuario;
            usuario.Clave = newUsuario.Clave;
            usuario.Nombre = newUsuario.Nombre;
            usuario.Telefono = newUsuario.Telefono;
            usuario.Email = newUsuario.Email;
            usuario.Direccion = newUsuario.Direccion;
            usuario.Piso = newUsuario.Piso;
            usuario.Servicio = newUsuario.Servicio; 
            usuario.UrlApi = newUsuario.UrlApi;
            usuario.IdRol = newUsuario.IdRol;
            usuario.IdLocalidad = newUsuario.IdLocalidad;

        }

        public void DeleteUsuario(int idUsuario)
        {
            var usuario = _db.Usuario.FirstOrDefault(l => l.IdUsuario == idUsuario);

            if (usuario is null)
                throw new NotFoundException("No existe el Usuario especificado");

            _db.Usuario.Remove(usuario);
        }

        public List<Usuario> GetUsuariosNoVerificados()
        {
            return _db
                .Usuario
                .Include(x => x.Rol)
                .Where(x => x.Rol.Descripcion == RolDescription.INMOBILIARIA && !x.Verificado)
                .ToList();
        }
    }
}