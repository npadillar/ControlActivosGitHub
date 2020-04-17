using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Logistica.Libreria.Entidad;

namespace Logistica.Libreria.Datos
{
      
    public class BajaActivosDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;

        public string InsertarBajaActivosCabecera(BajaActivosCabeceraEn objC)
        {
            int cod = 0;

            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertarBajaActivos", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // cmd.Parameters.AddWithValue("@IdGuia", ObjG.IdGuia);
                    cmd.Parameters.AddWithValue("@IdLogin", objC.IdLogin);
                    cmd.Parameters.AddWithValue("@FechaBaja", objC.FechaBaja);
                    cmd.Parameters.AddWithValue("@IdMotivo", objC.IdMotivo);
                    cmd.Parameters.AddWithValue("@IdDisposicion", objC.IdDisposicion);
                    cmd.Parameters.AddWithValue("@ip", objC.ip);

                    SqlParameter retval = new SqlParameter("@IdBajaCabecera", 0);
                    retval.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(retval);
                    cmd.ExecuteNonQuery();
                    cod  = Int32.Parse((cmd.Parameters["@IdBajaCabecera"].Value.ToString()));

                    cmd.Dispose();
                    return Convert.ToString(cod);

                    cmd.ExecuteNonQuery();

                }

            }

        }

        public DataTable BuscarBaja(string xcod)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspBuscarBaja", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@IdCondicion", xcod);
                adap.Fill(tb);
            }
            return tb;
        }

        public string InsertarBajaActivosDetalle(BajaActivosDetalleEn ObjD)
        {
            string rpta = "";

            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertarBajaActivosDetalle", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //adicionar campos detalle
                    cmd.Parameters.AddWithValue("@IdBajaCabecera", ObjD.IdBajaCabecera);
                    cmd.Parameters.AddWithValue("@IdLogistica", ObjD.IdLogistica);
                    rpta = "Se Guardó Correctamente";
                    cmd.ExecuteNonQuery();
                    // catch
                }
                return rpta;
            }

        }

        public DataTable ReporteBajaCabecera(LogisticaEn objLo, BajaActivosCabeceraEn objBC, DateTime dfechaini, DateTime dfechafin)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspBuscarBajaActivosCabecera", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@idBaja", objBC.IdBajaCabecera);
                adap.SelectCommand.Parameters.AddWithValue("@Motivo", objBC.IdMotivo);
                adap.SelectCommand.Parameters.AddWithValue("@Disposicion", objBC.IdDisposicion);
                adap.SelectCommand.Parameters.AddWithValue("@FechaIni", dfechaini);
                adap.SelectCommand.Parameters.AddWithValue("@FechaFin", dfechafin);
                adap.SelectCommand.Parameters.AddWithValue("@codigo", objLo.Codigo);
                adap.SelectCommand.Parameters.AddWithValue("@descripcion",objLo.Descripcion);
                adap.SelectCommand.Parameters.AddWithValue("@serie", objLo.Serie);
                adap.SelectCommand.Parameters.AddWithValue("@marca", objLo.Marca);
                adap.SelectCommand.Parameters.AddWithValue("@modelo", objLo.Modelo);
                adap.SelectCommand.Parameters.AddWithValue("@usuario", objBC.usuario);

                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable ReporteBajaDetalle(int xIdBaja)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspBuscarBajaActivosDetalle", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@IdBaja", xIdBaja);
                adap.Fill(tb);
            }
            return tb;
        }

        public Int16 bloquear_activo_baja(string xCodigo)
        {
            try
            {
                Int16 contar = 0;
                string sql = "select count(*) from BajaActivosDetalle where IdLogistica = (select IdLogistica from LOGISTICA where Codigo = '" + xCodigo + "')";
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;
                    contar = Convert.ToInt16(cmd.ExecuteScalar());
                }
                return contar;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
