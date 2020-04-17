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
    public class BajaActivosN
    {
        BajaActivosDAO objBaja = new BajaActivosDAO();
         public string InsertarBajaActivosCabecera(BajaActivosCabeceraEn objC)
            {
              return objBaja.InsertarBajaActivosCabecera(objC);
                   
            }
         public string InsertarBajaActivosDetalle(BajaActivosDetalleEn ObjD)
         {
             return objBaja.InsertarBajaActivosDetalle(ObjD);
         }
         public DataTable ReporteBajaCabecera(LogisticaEn objLo, BajaActivosCabeceraEn objBC, DateTime dfechaini, DateTime dfechafin)
         {
           
             return objBaja.ReporteBajaCabecera(objLo,objBC, dfechaini,dfechafin);
         }

         public DataTable ReporteBajaDetalle(int xIdBaja)
         {
             
             return objBaja.ReporteBajaDetalle(xIdBaja);
         }
        public DataTable BuscarBaja(string xcod)
        {
            return objBaja.BuscarBaja(xcod);
        }

        public bool fun_bloquear_activo_baja(string xCodigo)
        {
            try
            {
                Int16 contar = objBaja.bloquear_activo_baja(xCodigo);
                bool b = false;
                if (contar > 0) b = true;
                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
