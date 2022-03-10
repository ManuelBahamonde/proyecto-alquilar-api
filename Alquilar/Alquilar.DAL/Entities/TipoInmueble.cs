using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alquilar.DAL
{
    public class TipoInmueble
    {
        [Key]
        public int IdTipoInmueble { get; set; }
        public string Nombre { get; set; }
    }
}
