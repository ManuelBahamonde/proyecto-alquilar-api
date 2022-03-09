using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Models
{
    public class UsuarioDTO
    {
        public int? IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string Servicio { get; set; }
        public string UrlApi { get; set; }
        public int IdRol { get; set; }
        public int IdImagen { get; set; }
        public int IdLocalidad { get; set; }
    }
}
