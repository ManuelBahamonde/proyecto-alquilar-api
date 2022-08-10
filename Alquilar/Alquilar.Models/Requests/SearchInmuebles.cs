using System;

namespace Alquilar.Models
{
    public class SearchInmueblesRequest
    {
        public int? HabitacionesMin { get; set; }
        public int? HabitacionesMax { get; set; }
        public int? BanosMin { get; set; }
        public int? BanosMax { get; set; }
        public int? AmbientesMin { get; set; }
        public int? AmbientesMax { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public int? IdLocalidad { get; set; }

        public int? IdUsuario { get; set; }
    }
}
