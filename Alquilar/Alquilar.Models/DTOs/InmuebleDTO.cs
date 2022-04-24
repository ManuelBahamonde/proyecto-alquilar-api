using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alquilar.Models
{
    public class InmuebleDTO
    {
        public int? IdInmueble { get; set; }
        public string Direccion { get; set; }
        public int? Piso { get; set; }
        public string Departamento { get; set; }
        public decimal Precio { get; set; }
        public int? Habitaciones { get; set; }
        public int? Baños { get; set; }
        public int? Ambientes { get; set; }
        //public int? IdInmuebleExterno { get; set; }
        public DateTime? FechaHastaAlquilada { get; set; }
        public int IdTipoInmueble { get; set; }
        public List<ImagenDTO> Imagenes { get; set; } = new List<ImagenDTO>();
        public int IdLocalidad { get; set; }
        public int IdUsuario { get; set; }

        public string NombreVendedor { get; set; }
        public string NombreTipoInmueble { get; set; }
        public string UrlImagenPresentacion { get; set; }
        public string Ubicacion { get; set; }

        public string EmailVendedor { get; set; }
        public string TelefonoVendedor { get; set; }
    }
}
