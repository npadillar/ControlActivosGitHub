using Logistica.Libreria.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistica.Libreria.Entidad ;

namespace Logistica.Libreria.Negocio
{
    public class ReportesN
    {
        
        ReportesDAO objdatos = new ReportesDAO();
        public DataTable ListarReporte(LogisticaEn objLog, DateTime FechaIni, DateTime FechaFin, int Condicion, string xRuc, string xNroFactura, LoginEn ObjLogin)
        {
            return objdatos.ListarReporte(objLog, FechaIni, FechaFin, Condicion, xRuc, xNroFactura,ObjLogin);
        }

        public DataTable ReporteGuia(GuiaCabeceraEn objGC,SedeEn objSede,SedeEn objSede2, DateTime dfechaini, DateTime dfechafin,GuiaCabeceraEn ObjEstado, string IdGuia, LogisticaEn objCodigo)
        {
                  
            return objdatos.ReporteGuia(objGC,objSede,objSede2, dfechaini, dfechafin,ObjEstado, IdGuia, objCodigo);
            
        }


        public DataTable ReporteGuiaDetalle(int xIdGuia, int xActivo)
        {
            funcionesN objFun = new funcionesN();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            dt = objdatos.ReporteGuiaDetalle(xIdGuia, xActivo);

            if (xActivo == 0) // bienes
            {
                dt2.Columns.Add("cantidad");
                dt2.Columns.Add("Descripcion");
                dt2.Columns.Add("Unidad Medida");

                foreach (DataRow row in dt.Rows)
                {
                    dt2.Rows.Add(row["cantidad"].ToString(),
                                 objFun.fun_RemplazarLetras(row["Descripcion"].ToString()),
                                 row["Unidad Medida"]);
                }
            }
            else // activo
            {
                dt2.Columns.Add("IdGuia");
                dt2.Columns.Add("Codigo");
                dt2.Columns.Add("Descripcion");
                dt2.Columns.Add("Origen");
                dt2.Columns.Add("Detalle");
                dt2.Columns.Add("Condicion");
                dt2.Columns.Add("Unidad de Medida");

                foreach (DataRow row in dt.Rows)
                {
                    dt2.Rows.Add(row["IdGuia"].ToString(),
                                 row["Codigo"].ToString(),
                                 objFun.fun_RemplazarLetras(row["Descripcion"].ToString()),
                                 objFun.fun_RemplazarLetras(row["Origen"].ToString()),
                                 objFun.fun_RemplazarLetras(row["Detalle"].ToString()),
                                 row["Condicion"].ToString(),
                                 row["Unidad de Medida"]);
                }
            }

            return dt2;
        }

        public string fun_GuiaDetalle_mostrarUno(int xIdGuia, int xActivo)
        {
            try
            {
                return objdatos.GuiaDetalle_mostrarUno(xIdGuia, xActivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable fun_cantidad_cambios_codigo()
        {
            try
            {
                return objdatos.cantidad_cambios_codigo();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable fun_ListarReporte_cambiosCodigo(DateTime fecIni, DateTime fecFin, string codigo, int cant)
        {
            try
            {
                return objdatos.ListarReporte_cambiosCodigo(fecIni, fecFin, codigo, cant);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable fun_Listar_cambiosCodigo_xIdLogistica(int idLogistica)
        {
            try
            {
                return objdatos.Listar_cambiosCodigo_xIdLogistica(idLogistica);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
