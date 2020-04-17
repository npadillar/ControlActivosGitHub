using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
    public class ReparacionEn
    {
        public int idGuia { get; set; }
        public double total { get; set; }
        public string observ { get; set; }
    }

    public class ReparacionDetEn
    {
        public int idReparacion { get; set; }
        public string codigo { get; set; }
        public string nroFactura { get; set; }
        public DateTime fecReparacion { get; set; }
        public int tiempoGar { get; set; }
        public DateTime fecFinGar { get; set; }
        public double costo { get; set; }
        public string motivo { get; set; }
        public string trabRealizado { get; set; }
        public string transportista { get; set; }
    }
}
