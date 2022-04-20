﻿using Alquilar.DAL;
using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Alquilar.Services.Interfaces;
using System;

namespace Alquilar.Services
{
    public class AuthService
    {

        #region Members
        private readonly UsuarioRepo _usuarioRepo;
        private readonly ITokenService _tokenService;
        #endregion

        #region Constructor
        public AuthService(UsuarioRepo usuarioRepo,
            ITokenService tokenService)
        {
            _usuarioRepo = usuarioRepo;
            _tokenService = tokenService;
        }
        #endregion

        #region Methods
        public Usuario Register(UsuarioDTO usuario)
        {
            if (usuario is null)
                throw new ArgumentException("Usuario no valido");

            if (string.IsNullOrEmpty(usuario.NombreUsuario))
                throw new ArgumentException("El nombre de Usuario espcificado no es valido.");
            if (string.IsNullOrEmpty(usuario.Clave))
                throw new ArgumentException("La clave especificada no es valida.");
            if (string.IsNullOrEmpty(usuario.Nombre))
                throw new ArgumentException("El nombre especificado no es valido.");
            if (_usuarioRepo.GetUsuarioByNombreUsuario(usuario.NombreUsuario) != null)
                throw new ArgumentException("Ya existe un Usuario con ese nombre de Usuario.");

            var usuarioModel = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Clave = CreatePasswordHash(usuario.Clave),
                Nombre = usuario.Nombre,
                Telefono = usuario.Telefono,
                Email = usuario.Email,
                Direccion = usuario.Direccion,
                Piso = usuario.Piso,
                Servicio = usuario.Servicio,
                UrlApi = usuario.UrlApi,
                IdRol = usuario.IdRol,
                IdLocalidad = usuario.IdLocalidad,
                Verificado = true, // TODO: hacer que verificado sea false cuando el rol sea Inmobiliaria y no permitir login cuando verificado sea false
            };

            _usuarioRepo.CreateUsuario(usuarioModel);
            _usuarioRepo.SaveChanges();

            return usuarioModel;
        }
        
        public Token Login(UsuarioDTO usuario)
        {
            if (usuario is null)
                throw new ArgumentException("Usuario no valido");

            if (string.IsNullOrEmpty(usuario.NombreUsuario))
                throw new ArgumentException("El nombre de Usuario espcificado no es valido.");
            if (string.IsNullOrEmpty(usuario.Clave))
                throw new ArgumentException("La clave especificada no es valida.");

            var dbUser = _usuarioRepo.GetUsuarioByNombreUsuario(usuario.NombreUsuario);
            if (dbUser == null || dbUser.Clave != CreatePasswordHash(usuario.Clave))
                throw new NotFoundException("El Nombre de Usuario o la Clave son incorrectas.");

            return _tokenService.GenerateToken(dbUser);
        }
        #endregion

        #region Helpers
        private static string CreatePasswordHash(string password)
        {
            // TODO
            return password;
        }
        #endregion
    }
}
