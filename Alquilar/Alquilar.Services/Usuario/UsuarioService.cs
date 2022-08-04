using Alquilar.DAL;
using Alquilar.Helpers.Consts;
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
        private readonly EmailService _emailService;
        private readonly ConfigService _configService;
        private readonly Token _token;
        #endregion

        #region Constructor
        public UsuarioService(UsuarioRepo usuarioRepo,
            HorarioService horarioService,
            EmailService emailService,
            ConfigService configService,
            ITokenService tokenService)
        {
            _usuarioRepo = usuarioRepo;
            _horarioService = horarioService;
            _emailService = emailService;
            _configService = configService;
            _token = tokenService.GetToken();
        }
        #endregion

        #region CRUD
        // Read
        public List<UsuarioDTO> GetUsuarios()
        {
            var usuarios = _usuarioRepo.GetUsuarios();

            return usuarios.Select(MapUsuarioToDTO).ToList();
        }

        public UsuarioDTO GetUsuarioById(int idUsuario)
        {
            var usuarioModel = _usuarioRepo.GetUsuarioById(idUsuario);

            return MapUsuarioToDTO(usuarioModel);
        }

        // Update
        public void UpdateUsuario(int idUsuario, UsuarioDTO usuario)
        {
            var usuarioModel = _usuarioRepo.GetUsuarioById(idUsuario);

            usuarioModel.Nombre = usuario.Nombre;
            usuarioModel.Telefono = usuario.Telefono;
            usuarioModel.Email = usuario.Email;
            usuarioModel.Piso = usuario.Piso;
            usuarioModel.IdLocalidad = usuario.IdLocalidad;
            usuarioModel.DuracionTurno = usuario.DuracionTurno;

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
                {
                    h.IdUsuario = usuarioModel.IdUsuario;
                    _horarioService.CreateHorario(h);
                }
                else
                {
                    _horarioService.UpdateHorario(h.IdHorario.Value, h);
                }
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

        public List<UsuarioDTO> GetUsuariosPorVerificar()
        {
            if (_token.NombreRol != RolDescription.ADMINISTRADOR)
                throw new NotAuthorizedException("No autorizado");

            var usuarios = _usuarioRepo.GetUsuariosNoVerificados();

            return usuarios.Select(MapUsuarioToDTO).ToList();
        }

        public void VerifyUsuario(int idUsuario, bool reject)
        {
            if (_token.NombreRol != RolDescription.ADMINISTRADOR)
                throw new NotAuthorizedException("No autorizado");

            var usuario = _usuarioRepo.GetUsuarioById(idUsuario);
            if (usuario == null)
                throw new NotFoundException("No existe el Usuario especificado.");

            if (usuario.Rol.Descripcion != RolDescription.INMOBILIARIA || usuario.Verificado)
                throw new InvalidOperationException("El Usuario especificado no es una Inmobiliaria o bien ya esta Verificado.");

            if (reject)
            {
                DeleteUsuario(idUsuario);
            } else
            {
                usuario.Verificado = true;
                _usuarioRepo.SaveChanges();
            }

            // Letting Inmobiliaria know that its User has been just verified/rejected
            var config = _configService.GetConfig();

            var sendRq = new SendEmailRequest
            {
                To = usuario.Email,
                Subject = reject ? config.NotificacionInmobiliariaRechazadaSubject : config.NotificacionInmobiliariaVerificadaSubject,
                Body = reject ? config.NotificacionInmobiliariaRechazadaBody : config.NotificacionInmobiliariaVerificadaBody,
                Tags = new Dictionary<string, string>
                {
                    { EmailNotificacionTags.NOMBRE_INMOBILIARIA, usuario.Nombre }
                },
            };
            _emailService.SendEmail(sendRq);
        }

        #region Private Helpers
        public static UsuarioDTO MapUsuarioToDTO(Usuario usuario)
        {
            if (usuario == null)
                return null;

            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
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
                IdLocalidad = usuario.IdLocalidad,
                NombreCompletoLocalidad = usuario.Localidad.NombreCompleto,
                DuracionTurno = usuario.DuracionTurno,
                Horarios = usuario.Horarios.Select(h => HorarioService.MapHorarioToDTO(h)).ToList(),
            };
        }
        #endregion
    }
}
