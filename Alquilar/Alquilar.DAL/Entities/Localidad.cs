using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alquilar.DAL
{
    public class Localidad
    {
        [Key]
        public int IdLocalidad { get; set; }
        public string Nombre { get; set; }
        public int IdProvincia { get; set; }
        public string NombreCompleto { get => $"{Nombre}, {Provincia?.Nombre}" ;}

        [ForeignKey("IdProvincia")]
        public Provincia Provincia { get; set; }
    }
}
