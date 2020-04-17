using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
   public class GuiaTemporalEn
    {
       public int IdLogistica { get; set; }
       public string Codigo { get; set; }
       public string Descripcion { get; set; }
       public string Origen { get; set; }
       public string Detalle { get; set; }
       public string Condicion { get; set; }
       public int idUnidadMedida { get; set; }
       public string UnidadMedida { get; set; }
    }
}
