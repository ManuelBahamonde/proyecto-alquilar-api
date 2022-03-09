using Alquilar.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Models
{
    public class InmuebleDTO
    {
        public int? IdInmueble { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public double Precio { get; set; }
        public int? Habitaciones { get; set; }
        public int? Baños { get; set; }
        public int? Ambientes { get; set; }
        public int? IdInmuebleExterno { get; set; }
        public DateTime? FechaHastaAlquilada { get; set; }
        public int IdTipoInmueble { get; set; }
        public List<Imagen> Imagenes { get; set; }
        public int IdLocalidad { get; set; }
        public int IdUsuario { get; set; }
    }
}
