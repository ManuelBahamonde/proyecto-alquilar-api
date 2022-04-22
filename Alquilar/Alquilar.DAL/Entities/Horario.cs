using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alquilar.DAL
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }
        public string DiaSemana { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
