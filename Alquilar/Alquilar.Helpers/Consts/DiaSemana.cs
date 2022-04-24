using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alquilar.Helpers.Consts
{
    public static class DiaSemana
    {
        public const string LUNES = "LUNES";
        public const string MARTES = "MARTES";
        public const string MIERCOLES = "MIERCOLES";
        public const string JUEVES = "JUEVES";
        public const string VIERNES = "VIERNES";
        public const string SABADO = "SABADO";
        public const string DOMINGO = "DOMINGO";

        public static Dictionary<string, int> DiaSemanaIndex = new()
        {
            { LUNES, 0 },
            { MARTES, 1 },
            { MIERCOLES, 2 },
            { JUEVES, 3 },
            { VIERNES, 4 },
            { SABADO, 5 },
            { DOMINGO, 6 },
        };
    }
}
