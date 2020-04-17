using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
    public class FacturaEn
    {
        public int IdLogistica { get; set; }
        public string Ruc { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime FecFinGar { get; set; }
        public string NumeroFactura { get; set; }
        public string Proveedor { get; set; }
        public string Direccion { get; set; }
        //public DateTime FecFactRep { get; set; }
        public int TiempGar { get; set; }

        public string razonSocial;
        public string direccion;
        public string ruc;

        public string usuReg;
        public string pc;
    }
}
