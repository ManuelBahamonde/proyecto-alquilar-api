using Alquilar.DAL;
using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Alquilar.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alquilar.Services
{
    public class UsuarioService
    {
        #region Members
        private readonly UsuarioRepo _usuarioRepo;
        private readonly HorarioService _horarioService;
        #endregion

        #region Constructor
        public UsuarioService(UsuarioRepo usuarioRepo,
            HorarioService horarioService,
            ITokenService tokenService)
        {
            _usuarioRepo = usuarioRepo;
            _horarioService = horarioService;
        }
        #endregion

        #region CRUD
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
            var usuarioModel = _usuarioRepo.GetUsuarioById(idUsuario);

            usuarioModel.Clave = usuario.Clave;
            usuarioModel.Nombre = usuario.Nombre;
            usuarioModel.Telefono = usuario.Telefono;
            usuarioModel.Email = usuario.Email;
            usuarioModel.Piso = usuario.Piso;
            usuarioModel.Servicio = usuario.Servicio;
            usuarioModel.UrlApi = usuario.UrlApi;
            usuarioModel.IdRol = usuario.IdRol;
            usuarioModel.IdLocalidad = usuario.IdLocalidad;

            _usuarioRepo.BeginTransaction();

            var rqIdsHorario = usuario
                .Horarios
                .Where(x => x.IdHorario.HasValue)
                .Select(x => x.IdHorario)
                .ToList();

            var toDeleteIdsHorario = usuarioModel.Horarios.Where(x => !rqIdsHorario.Contains(x.IdHorario)).Select(h => h.IdHorario).ToList();

            usuario.Horarios.ForEach(h =>
            {
                if (!h.IdHorario.HasValue)
                    _horarioService.CreateHorario(h);
                else
                    _horarioService.UpdateHorario(h.IdHorario.Value, h);
            });
            toDeleteIdsHorario.ForEach(_horarioService.DeleteHorario);

            _usuarioRepo.UpdateUsuario(idUsuario, usuarioModel);
            _usuarioRepo.SaveChanges();
            _usuarioRepo.Commit();
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
