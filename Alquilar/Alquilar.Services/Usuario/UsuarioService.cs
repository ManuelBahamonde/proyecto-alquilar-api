using Alquilar.DAL;
using Alquilar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Services
{
    public class UsuarioService
    {

        #region Members
        private readonly UsuarioRepo _usuarioRepo;
        #endregion

        #region Constructor
        public UsuarioService(UsuarioRepo usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }
        #endregion

        #region CRUD
        // Create
        public Usuario CreateUsuario(UsuarioDTO usuario)
        {
            if (usuario is null)
                throw new ArgumentException("Usuario no valido");

            if (string.IsNullOrEmpty(usuario.NombreUsuario))
                throw new ArgumentException("El nombre de Usuario espcificado no es valido.");
            if (string.IsNullOrEmpty(usuario.Clave))
                throw new ArgumentException("La clave espcificada no es valida.");
            if (string.IsNullOrEmpty(usuario.Nombre))
                throw new ArgumentException("El nombre espcificado no es valido.");


            var usuarioModel = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Clave = usuario.Clave,
                Nombre = usuario.Nombre,
                Telefono = usuario.Telefono,
                Email = usuario.Email,
                Direccion = usuario.Direccion,
                Piso = usuario.Piso,
                Servicio = usuario.Servicio,
                UrlApi = usuario.UrlApi,
                IdRol = usuario.IdRol,
                IdLocalidad = usuario.IdLocalidad
        };

            _usuarioRepo.CreateUsuario(usuarioModel);
            _usuarioRepo.SaveChanges();

            return usuarioModel;
        }

        // Read
        public List<Usuario> GetUsuarios()
        {
            return _usuarioRepo.GetUsuarios();
        }

        public Usuario GetUsuarioById(int idUsuario)
        {
            return _usuarioRepo.GetUsuarioById(idUsuario);
        }

        // Update
        public void UpdateUsuario(int idUsuario, UsuarioDTO usuario)
        {
            var usuarioModel = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Clave = usuario.Clave,
                Nombre = usuario.Nombre,
                Telefono = usuario.Telefono,
                Email = usuario.Email,
                Direccion = usuario.Direccion,
                Piso = usuario.Piso,
                Servicio = usuario.Servicio,
                UrlApi = usuario.UrlApi,
                IdRol = usuario.IdRol,
                IdLocalidad = usuario.IdLocalidad
            };

            _usuarioRepo.UpdateUsuario(idUsuario, usuarioModel);
            _usuarioRepo.SaveChanges();
        }

        // Delete
        public void DeleteUsuario(int idUsuario)
        {
            _usuarioRepo.DeleteUsuario(idUsuario);
            _usuarioRepo.SaveChanges();
        }
        #endregion

    }
}
