using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
   public class GuiaCabeceraEn
    {
        public int IdGuia { get; set; }
        public int IdLogin { get; set; }
        public string SedePartida { get; set; }
        public string SedeLlegada { get; set; }
        public string OrigenDestinoExterno { get; set; }
        public DateTime Fecha { get; set; }
        public string Transportista { get; set; }
        public int IdMotivoTraslado { get; set; }
        public int Activos { get; set; }
        public int IdLoginRecibido { get; set; }
        public int IdLoginAnulado { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int IdEstado { get; set; }
        public DateTime FechaAnulacion { get; set; }
        public string observ { get; set; }
        public string ip { get; set; }
    }
}
