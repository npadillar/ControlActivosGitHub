using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistica.Libreria.Entidad;

namespace Logistica.Libreria.Datos
{
    public class ReportesDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;

        public DataTable ListarReporte(LogisticaEn objLog, DateTime dfechaini, DateTime dfechafin, int Condicion, string xRuc, string xNroFactura,LoginEn ObjLogin)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspBuscarFechas", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Codigo", objLog.Codigo);
                adap.SelectCommand.Parameters.AddWithValue("@codAnterior", objLog.codAnterior);
                adap.SelectCommand.Parameters.AddWithValue("@Descripcion", objLog.Descripcion);
                adap.SelectCommand.Parameters.AddWithValue("@IdLogin", ObjLogin.IdLogin);
                adap.SelectCommand.Parameters.AddWithValue("@Sede", objLog.IdSede);
                adap.SelectCommand.Parameters.AddWithValue("@Edificio", objLog.Edificio);
                adap.SelectCommand.Parameters.AddWithValue("@Categoria", objLog.IdCategoria);
                adap.SelectCommand.Parameters.AddWithValue("@FechaIni", dfechaini);
                adap.SelectCommand.Parameters.AddWithValue("@FechaFin", dfechafin);
                adap.SelectCommand.Parameters.AddWithValue("@Condicion", Condicion);
                adap.SelectCommand.Parameters.AddWithValue("@Aula", objLog.Aula);
                adap.SelectCommand.Parameters.AddWithValue("@Area", objLog.Area);
                adap.SelectCommand.Parameters.AddWithValue("@Ruc", xRuc);
                adap.SelectCommand.Parameters.AddWithValue("@NumeroFactura", xNroFactura);
              

                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable ReporteGuia(GuiaCabeceraEn objGC,SedeEn objSede,SedeEn objSede2, DateTime dfechaini, DateTime dfechafin,GuiaCabeceraEn objEstado, string IdGuia, LogisticaEn objCodigo)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspReporterGuia", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@MotivoTraslado", objGC.IdMotivoTraslado);
                adap.SelectCommand.Parameters.AddWithValue("@SedePartida", objSede.IdSede);
                adap.SelectCommand.Parameters.AddWithValue("@SedeLlegada", objSede2.IdSede);
                adap.SelectCommand.Parameters.AddWithValue("@FechaIni", dfechaini);
                adap.SelectCommand.Parameters.AddWithValue("@FechaFin", dfechafin);
                adap.SelectCommand.Parameters.AddWithValue("@Estado", objEstado.IdEstado);
                adap.SelectCommand.Parameters.AddWithValue("@Idguia", IdGuia);
                adap.SelectCommand.Parameters.AddWithValue("@codigo", objCodigo.Codigo);
                adap.SelectCommand.Parameters.AddWithValue("@tipo", objGC.Activos);

                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable ReporteGuiaDetalle(int xIdGuia,int xActivo)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspBuscarDetalleGuia", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@IdGuia", xIdGuia);
                adap.SelectCommand.Parameters.AddWithValue("@Activos", xActivo);


                adap.Fill(tb);
            }
            return tb;
        }
        
        public string GuiaDetalle_mostrarUno(int xIdGuia, int xActivo)
        {
            try
            {
                string texto = "";
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = cad_cn;
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspGuiaDetalle_MostrarUno", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdGuia", xIdGuia);
                    cmd.Parameters.AddWithValue("@Activos", xActivo);
                    texto = cmd.ExecuteScalar().ToString();
                }
                return texto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public DataTable cantidad_cambios_codigo()
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("usp_auditoria_cantCambios_codigo", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable ListarReporte_cambiosCodigo(DateTime fecIni, DateTime fecFin, string codigo, int cant)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("usp_logistica_rpt_cambiosCodigo", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@fecIni", fecIni);
                adap.SelectCommand.Parameters.AddWithValue("@fecFin", fecFin);
                adap.SelectCommand.Parameters.AddWithValue("@codigo", codigo);
                adap.SelectCommand.Parameters.AddWithValue("@cant", cant);
                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable Listar_cambiosCodigo_xIdLogistica(int idLogistica)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("usp_auditoria_listarCambios_codigo", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@idLogistica", idLogistica);
                adap.Fill(tb);
            }
            return tb;
        }
    }
}
