using System;

namespace Alquilar.Models
{
    public class SearchInmueblesRequest
    {
        public int? Habitaciones { get; set; }
        public int? Banos { get; set; }
        public int? Ambientes { get; set; }
        public DateTime? FechaDisponibilidad { get; set; }
        public int? IdLocalidad { get; set; }
    }
}
