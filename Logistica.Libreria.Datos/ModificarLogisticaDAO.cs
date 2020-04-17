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
    public class ModificarLogisticaDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        
        public string InsertarModificarLog(LogisticaEn objE, FacturaEn objFact)
        {
            string rpta = "";

                {
                    using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertarModificarLog", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdLogistica", objE.IdLogistica);
                    cmd.Parameters.AddWithValue("@Idlogin", objE.IdLogin);
                    cmd.Parameters.AddWithValue("@Codigo", objE.Codigo);
                    cmd.Parameters.AddWithValue("@Descripcion", objE.Descripcion);
                    cmd.Parameters.AddWithValue("@IdSede", objE.IdSede);
                    cmd.Parameters.AddWithValue("@Area", objE.Area);
                    cmd.Parameters.AddWithValue("@Piso", objE.Piso);
                    cmd.Parameters.AddWithValue("@Edificio", objE.Edificio);
                    cmd.Parameters.AddWithValue("@IdCategoria", objE.IdCategoria);
                    cmd.Parameters.AddWithValue("@Aula", objE.Aula);
                    cmd.Parameters.AddWithValue("@Serie", objE.Serie);
                    cmd.Parameters.AddWithValue("@Marca", objE.Marca);
                    cmd.Parameters.AddWithValue("@Modelo", objE.Modelo);

                    cmd.Parameters.AddWithValue("@RUC", objFact.Ruc);
                    cmd.Parameters.AddWithValue("@Proveedor", objFact.Proveedor);
                    cmd.Parameters.AddWithValue("@Direccion", objFact.Direccion);
                    cmd.Parameters.AddWithValue("@NumeroFactura", objFact.NumeroFactura);
                    cmd.Parameters.AddWithValue("@TiempGar", objFact.TiempGar);
                    cmd.Parameters.AddWithValue("@FechaCompra", objFact.FechaCompra);
                    cmd.Parameters.AddWithValue("@FecFinGar", objFact.FecFinGar);

                    cmd.Parameters.AddWithValue("@IdCondicion", objE.IdCondicion);
                    cmd.Parameters.AddWithValue("@Observacion", objE.Observacion);
                    cmd.Parameters.AddWithValue("@UsuAsignado", objE.UsuAsignado);

                    rpta = "Se Guardó Correctamente";
                    cmd.ExecuteNonQuery();
                    // catch
                }
                return rpta;
            }
}

        public string InsertarModificarLog_masivo(LogisticaEn objE, FacturaEn ObjFac)
        {
            try
            {
                string rpta = "";
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertarModificarLog_masivo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Idlogin", objE.IdLogin);
                    cmd.Parameters.AddWithValue("@Codigo", objE.Codigo);
                    cmd.Parameters.AddWithValue("@IdSede", objE.IdSede);
                    cmd.Parameters.AddWithValue("@Area", objE.Area);
                    cmd.Parameters.AddWithValue("@Piso", objE.Piso);
                    cmd.Parameters.AddWithValue("@Edificio", objE.Edificio);
                    cmd.Parameters.AddWithValue("@IdCategoria", objE.IdCategoria);
                    cmd.Parameters.AddWithValue("@Aula", objE.Aula);
                    cmd.Parameters.AddWithValue("@UsuAsignado", objE.UsuAsignado);

                    //cmd.Parameters.AddWithValue("@RUC", ObjFac.Ruc);
                    //cmd.Parameters.AddWithValue("@Proveedor", ObjFac.Proveedor);
                    //cmd.Parameters.AddWithValue("@NumeroFactura", ObjFac.NumeroFactura);
                    //cmd.Parameters.AddWithValue("@TiempoGar", ObjFac.TiempGar);
                    ////cmd.Parameters.AddWithValue("@Tipo", ObjFac.Tipo);
                    //cmd.Parameters.AddWithValue("@fechaFinGar", ObjFac.FecFinGar);

                    rpta = "Se Guardó Correctamente";
                    cmd.ExecuteNonQuery();
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable BuscarCodigo(string xcod)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspListarModLog", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Codigo", xcod);
                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable BuscarCodigoActivo(string xcoda)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspBuscarDetalleGuiaxActivo", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Codigo", xcoda);
                adap.Fill(tb);
            }
            return tb;
        }
        public DataTable BuscarCodigo1(string xcodg)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspListarLogistica", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Codigo", xcodg);
                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable listar_historial_activo(string xCodigo)
        {
            try
            {
                DataTable tb = new DataTable();
                using (SqlDataAdapter adap = new SqlDataAdapter("uspListar_Historial_activo", cad_cn))
                {
                    adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adap.SelectCommand.Parameters.AddWithValue("@codigo", xCodigo);
                    adap.Fill(tb);
                }
                return tb;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
