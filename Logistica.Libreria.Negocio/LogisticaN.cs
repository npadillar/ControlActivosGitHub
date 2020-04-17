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
   public  class LogisticaN
    {
       LogisticaDAO objdatos = new LogisticaDAO();

        public string InsertarLogistica(LogisticaEn objE, FacturaEn objF)
        {
           return objdatos.InsertarLogistica(objE,objF);
       }

        public DataTable BuscarLogistica(string xcod)
        {
            return objdatos.BuscarLogistica(xcod);
        }

        public int fun_validar_noRepetir_cod(string xcod)
        {
            return objdatos.validar_noRepetir_cod(xcod);
        }

        public string ActualizarLogistica(LogisticaEn ObjN, FacturaEn ObjFac)
        {
            return objdatos.ActualizarLogistica(ObjN,ObjFac);
        }

        public void pr_ActualizarLogistica_masivo(string cod, LogisticaEn logis, FacturaEn ObjFac)
        {
            try
            {
                objdatos.ActualizarLogistica_masivo(cod, logis, ObjFac);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable BuscarCodigoenGuia(string xcod)
        {
            return objdatos.BuscarCodigoenGuia(xcod);
        }
    }
}
