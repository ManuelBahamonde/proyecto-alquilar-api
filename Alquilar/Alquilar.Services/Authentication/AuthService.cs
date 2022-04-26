using Alquilar.DAL;
using Alquilar.Helpers.Consts;
using Alquilar.Helpers.Exceptions;
using Alquilar.Models;
using Alquilar.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Alquilar.Services
{
    public class AuthService
    {

        #region Members
        private readonly UsuarioRepo _usuarioRepo;
        private readonly HorarioService _horarioService;
        private readonly RolService _rolService;
        private readonly EmailService _emailService;
        private readonly ConfigService _configService;
        private readonly FrontendSettings _frontendSettings;
        private readonly ITokenService _tokenService;
        #endregion

        #region Constructor
        public AuthService(UsuarioRepo usuarioRepo,
            HorarioService horarioService,
            RolService rolService,
            EmailService emailService,
            ConfigService configService,
            IOptions<FrontendSettings> options,
            ITokenService tokenService)
        {
            _usuarioRepo = usuarioRepo;
            _horarioService = horarioService;
            _rolService = rolService;
            _emailService = emailService;
            _configService = configService;
            _frontendSettings = options.Value;
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

            var rol = _rolService.GetRolById(usuario.IdRol);
            if (rol == null)
                throw new ArgumentException("El rol especificado no existe.");

            // Beginning Transaction so we can create Usuario and Horario in the same transaction
            _usuarioRepo.BeginTransaction();

            // Saving Usuario
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
                Verificado = rol.Descripcion != RolDescription.INMOBILIARIA,
                DuracionTurno = usuario.Horarios.Count > 0 ? usuario.DuracionTurno : null,
            };

            _usuarioRepo.CreateUsuario(usuarioModel);
            _usuarioRepo.SaveChanges();

            // Saving Horario
            usuario.Horarios.ForEach(horario =>
            {
                horario.IdUsuario = usuarioModel.IdUsuario;

                _horarioService.CreateHorario(horario);
            });
            _usuarioRepo.SaveChanges();

            // Committing changes
            _usuarioRepo.Commit();

            // Let Admins know that a new Inmobiliaria has just registered and is waiting for verification
            if (rol.Descripcion == RolDescription.INMOBILIARIA)
            {
                var admins = _usuarioRepo.GetUsuarios(RolDescription.ADMINISTRADOR);
                var config = _configService.GetConfig();

                admins.ForEach(admin =>
                {
                    var sendRq = new SendEmailRequest
                    {
                        To = admin.Email,
                        Subject = config.NotificacionAdminNuevaInmobiliariaSubject,
                        Body = config.NotificacionAdminNuevaInmobiliariaBody,
                        Tags = new Dictionary<string, string>
                        {
                            { EmailNotificacionTags.NOMBRE_ADMINISTRADOR, admin.Nombre },
                            { EmailNotificacionTags.NOMBRE_INMOBILIARIA, usuarioModel.Nombre },
                            { EmailNotificacionTags.LINK, _frontendSettings.VerifyInmobiliariaUrl }
                        },
                    };
                    _emailService.SendEmail(sendRq);
                });
            }

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

            if (!dbUser.Verificado)
                throw new InvalidOperationException("Tu Usuario todavia no fue verificado por un Administrador. Te notificaremos por correo electronico cuando lo haga.");

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
