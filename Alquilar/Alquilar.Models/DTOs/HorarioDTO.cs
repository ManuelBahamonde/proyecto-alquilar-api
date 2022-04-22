using System;

namespace Alquilar.Models
{
    public class HorarioDTO
    {
        public string DiaSemana { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int? NumeroDiaSemana { get; set; }
        public int? IdUsuario { get; set; }
    }
}
