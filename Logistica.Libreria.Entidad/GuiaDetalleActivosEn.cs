using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
    public class GuiaDetalleActivosEn
    {
        public int IdGuiaDetActivos { get; set; }
        public int IdGuia { get; set; }
        public int IdUnidadMedida { get; set; }
        public int IdLogistica { get; set; }
    }
}
