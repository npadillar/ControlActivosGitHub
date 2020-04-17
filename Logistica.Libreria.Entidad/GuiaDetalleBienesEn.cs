using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
    public class GuiaDetalleBienesEn
    {
        public int IdGuiaDetBienes { get; set; }
        public int IdGuia { get; set; }
        public int IdUnidadMedida { get; set; }
        public int Cant { get; set; }
        public string Descripcion { get; set; }
    }
}
