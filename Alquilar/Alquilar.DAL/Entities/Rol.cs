using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alquilar.DAL
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }
        public string Descripcion { get; set; }
    }
}
