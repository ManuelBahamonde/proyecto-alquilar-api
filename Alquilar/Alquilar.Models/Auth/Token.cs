
namespace Alquilar.Models
{
    public class Token
    {
        public string Bearer { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreRol { get; set; }
    }
}
