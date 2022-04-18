using Alquilar.DAL;
using Alquilar.Models;
using Alquilar.Services.Interfaces;
using System.Collections.Generic;

namespace Alquilar.Services
{
    public class UsuarioService
    {
        #region Members
        private readonly UsuarioRepo _usuarioRepo;
        private readonly ITokenService _tokenService;
        #endregion

        #region Constructor
        public UsuarioService(UsuarioRepo usuarioRepo,
            ITokenService tokenService)
        {
            _usuarioRepo = usuarioRepo;
            _tokenService = tokenService;
        }
        #endregion

        #region CRUD
        // Read
        public List<Usuario> GetUsuarios()
        {
            var token = _tokenService.GetToken(); // TODO: remove. Written just as an example

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
