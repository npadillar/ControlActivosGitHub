using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Datos;

namespace Logistica.Libreria.Negocio
{
    public class ModificarLogisticaN
    {
        ModificarLogisticaDAO objdatos = new ModificarLogisticaDAO();
        public string InsertarModificarLog(LogisticaEn objE, FacturaEn objFact)
        {
            return objdatos.InsertarModificarLog(objE, objFact);
        }

        public string fun_InsertarModificarLog_masivo(LogisticaEn objE, FacturaEn ObjFac)
        {
            return objdatos.InsertarModificarLog_masivo(objE, ObjFac);
        }

        public DataTable BuscarCodigo(string xcod)
        {
            return objdatos.BuscarCodigo(xcod);
        }

        public DataTable BuscarCodigoActivo(string xcoda)
        {
            return objdatos.BuscarCodigoActivo(xcoda);
        }

        public DataTable BuscarCodigo1(string xcodg)
        {
            return objdatos.BuscarCodigo1(xcodg);
        }

        public DataTable fun_listar_historial_activo(string xCodigo)
        {
            try
            {
                return objdatos.listar_historial_activo(xCodigo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
