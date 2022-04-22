using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Models
{
    public class TurnoAsignadoDTO
    {
        public int IdUsuario { get; set; }
        public int IdInmueble { get; set; }
        public DateTime Fecha { get; set; }
    }
}
