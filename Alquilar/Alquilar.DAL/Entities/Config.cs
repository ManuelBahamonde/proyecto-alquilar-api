using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.DAL
{
    public class Config
    {
        [Key]
        public int IdConfig { get; set; }
        public string NotificacionTurnoEmailSubject { get; set; }
        public string NotificacionTurnoEmailBody { get; set; }
        public string NotificacionAdminNuevaInmobiliariaSubject { get; set; }
        public string NotificacionAdminNuevaInmobiliariaBody { get; set; }
        public string NotificacionInmobiliariaVerificadaSubject { get; set; }
        public string NotificacionInmobiliariaVerificadaBody { get; set; }
        public string NotificacionInmobiliariaRechazadaSubject { get; set; }
        public string NotificacionInmobiliariaRechazadaBody { get; set; }
    }
}
