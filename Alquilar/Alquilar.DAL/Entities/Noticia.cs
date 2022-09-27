using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Alquilar.DAL
{
    public class Noticia
    {
        [Key]
        public int IdNoticia { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }
}
