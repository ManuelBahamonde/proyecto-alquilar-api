using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alquilar.DAL
{
    public class Inmueble
    {

        [Key]
        public int IdInmueble { get; set; }
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

        [ForeignKey("IdTipoInmueble")]
        public TipoInmueble TipoInmueble { get; set; }
        public List<Imagen> Imagenes { get; set; }
        public int IdLocalidad { get; set; }
        
        [ForeignKey("IdLocalidad")]
        public Localidad Localidad { get; set; }
        public int IdUsuario { get; set; }
        
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }


    }
}