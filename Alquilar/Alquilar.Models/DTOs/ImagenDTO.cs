using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Models
{
    public class ImagenDTO
    {
        public int? IdImagen { get; set; }
        public string Url { get; set; }

        public int? IdUsuario { get; set; }

        public int? IdInmueble { get; set; }

    }
}
