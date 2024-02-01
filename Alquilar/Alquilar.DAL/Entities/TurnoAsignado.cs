using System;

namespace Alquilar.DAL
{
    public class TurnoAsignado
    {
        public int IdUsuario { get; set; }
        public int IdInmueble { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
    }
}
