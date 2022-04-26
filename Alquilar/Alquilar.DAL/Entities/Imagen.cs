using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alquilar.DAL
{
    public class Imagen
    {
        [Key]
        public int IdImagen{ get; set; }
        public string Url { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdInmueble { get; set; }
    }
}
