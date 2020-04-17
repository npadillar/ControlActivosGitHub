using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
    public class AuditoriaEst
    {
        public struct EST_AUDITORIA
        {
            public DateTime fecIni;
            public DateTime fecFin;
            public string accion;
            public int idUsuario;
            public string pagina;
            public string codigo;
            public int idTabla;
        }
    }
}
