using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alquilar.DAL
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string Servicio { get; set; }
        public string UrlApi { get; set; }
        public bool Verificado { get; set; }
        public int IdRol { get; set; }
        public int? DuracionTurno { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }
        public int IdLocalidad { get; set; }

        [ForeignKey("IdLocalidad")]
        public Localidad Localidad { get; set; }

        public List<Horario> Horarios { get; set; }

    }
}