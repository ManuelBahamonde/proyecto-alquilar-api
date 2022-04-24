using System;

namespace Alquilar.DAL
{
    public class TurnoAsignado
    {
        public int IdUsuario { get; set; }
        public int IdInmueble { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } // TODO: this field exists just in case we need to make the app more complex. If that's not neccesary, REMOVE IT. More info in DER.
    }
}
