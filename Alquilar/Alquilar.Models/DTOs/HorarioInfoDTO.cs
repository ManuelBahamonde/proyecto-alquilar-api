using System;
using System.Collections.Generic;

namespace Alquilar.Models
{
    public class HorarioInfoDTO
    {
        public List<HorarioDTO> Horarios { get; set; }
        public List<DateTime> FechasReservadas { get; set; }
    }
}
