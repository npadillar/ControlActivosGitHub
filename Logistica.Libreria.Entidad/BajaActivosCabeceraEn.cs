using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
    public class BajaActivosCabeceraEn
    {
        public int IdBajaCabecera { get; set; }
        public int IdLogin { get; set; }
        public DateTime FechaBaja { get; set; }
        public int IdMotivo { get; set; }
        public int IdDisposicion { get; set; }
        public int usuario { get; set; }
        public string ip { get; set; }
    }
}
