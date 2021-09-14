using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alquilar.DAL
{
    public class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }
        public string Nombre { get; set; }
    }
}
