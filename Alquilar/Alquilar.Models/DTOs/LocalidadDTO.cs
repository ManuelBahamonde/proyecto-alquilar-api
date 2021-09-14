using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Models
{
    public class LocalidadDTO
    {
        public int? IdLocalidad { get; set; }
        public int IdProvincia { get; set; }
        public string Nombre { get; set; }
        public string Label { get; set; }
    }
}
