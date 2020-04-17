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
    public class LogisticaDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;

        public string InsertarLogistica(LogisticaEn objE,FacturaEn objF)
        {
            string rpta = "";

            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertarLogistica", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdInventario", objE.IdInventario);
                    cmd.Parameters.AddWithValue("@IdLogin", objE.IdLogin);
                    cmd.Parameters.AddWithValue("@Fecha", objE.Fecha);
                    cmd.Parameters.AddWithValue("@CODIGO", objE.Codigo);
                    cmd.Parameters.AddWithValue("@DESCRIPCION", objE.Descripcion);
                    cmd.Parameters.AddWithValue("@SEDE", objE.IdSede);
                    cmd.Parameters.AddWithValue("@AREA", objE.Area);
                    cmd.Parameters.AddWithValue("@PISO", objE.Piso);
                    cmd.Parameters.AddWithValue("@EDIFICIO", objE.Edificio);
                    cmd.Parameters.AddWithValue("@CATEGORIA", objE.IdCategoria);
                    cmd.Parameters.AddWithValue("@AULA", objE.Aula);
                    cmd.Parameters.AddWithValue("@SERIE", objE.Serie);
                    cmd.Parameters.AddWithValue("@MARCA", objE.Marca);
                    cmd.Parameters.AddWithValue("@MODELO", objE.Modelo);
                    cmd.Parameters.AddWithValue("@CONDICION", objE.IdCondicion);
                   //cmd.Parameters.AddWithValue("@IdLogistica", objF.IdLogistica);
                    cmd.Parameters.AddWithValue("@Ruc", objF.Ruc);
                    cmd.Parameters.AddWithValue("@FechaCompra", objF.FechaCompra);
                    cmd.Parameters.AddWithValue("@Proveedor", objF.Proveedor);
                    cmd.Parameters.AddWithValue("@Direccion", objF.Direccion);
                    cmd.Parameters.AddWithValue("@TiempGar", objF.TiempGar);
                    cmd.Parameters.AddWithValue("@FecFinGar", objF.FecFinGar);
                    cmd.Parameters.AddWithValue("@NumeroFactura", objF.NumeroFactura);
                    cmd.Parameters.AddWithValue("@Observacion", objE.Observacion);
                    cmd.Parameters.AddWithValue("@UsuAsignado", objE.UsuAsignado);
                    cmd.Parameters.AddWithValue("@ip", objE.ip);

                    rpta = "Se Guardó Correctamente";
                    cmd.ExecuteNonQuery();
                    // catch
                }
                return rpta;
            }

        }

        public DataTable BuscarLogistica(string xcod)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter ("uspBuscarLogistica", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Codigo", xcod);
                adap.Fill(tb);
            }
            return tb;
        }

        public int validar_noRepetir_cod(string xcod)
        {
            int contar = 0;
            using (SqlConnection cn = new SqlConnection(cad_cn))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("usp_logistica_validarCod", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo", xcod);
                contar = int.Parse(cmd.ExecuteScalar().ToString());
            }
            return contar;
        }

        public DataTable BuscarCodigoenGuia(string xcod)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspBuscarCodigoenGuia", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Codigo", xcod);
                adap.Fill(tb);
            }
            return tb;
        }

        public string ActualizarLogistica(LogisticaEn ObjN, FacturaEn ObjFac)
        {
            string rpta = "";

            using (SqlConnection cn = new SqlConnection(cad_cn))
            {
                // try
                cn.Open();
                SqlCommand cmd = new SqlCommand("uspActualizarLogistica", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdLogistica", ObjN.IdLogistica);
                cmd.Parameters.AddWithValue("@IdInventario", ObjN.IdInventario);
                cmd.Parameters.AddWithValue("@IdLogin", ObjN.IdLogin);
                cmd.Parameters.AddWithValue("@fecha", ObjN.Fecha);
                
                cmd.Parameters.AddWithValue("@Codigo", ObjN.Codigo);
                cmd.Parameters.AddWithValue("@Descripcion", ObjN.Descripcion);
                cmd.Parameters.AddWithValue("@Sede", ObjN.IdSede);
                cmd.Parameters.AddWithValue("@Area", ObjN.Area);
                cmd.Parameters.AddWithValue("@Piso", ObjN.Piso);
                cmd.Parameters.AddWithValue("@Edificio", ObjN.Edificio);
                cmd.Parameters.AddWithValue("@Categoria", ObjN.IdCategoria);

                cmd.Parameters.AddWithValue("@Serie", ObjN.Serie);
                cmd.Parameters.AddWithValue("@Marca", ObjN.Marca);
                cmd.Parameters.AddWithValue("@Modelo", ObjN.Modelo);
                cmd.Parameters.AddWithValue("@Idcondicion", ObjN.IdCondicion);
                cmd.Parameters.AddWithValue("@AULA", ObjN.Aula);

                cmd.Parameters.AddWithValue("@RUC", ObjFac.Ruc);
                cmd.Parameters.AddWithValue("@Proveedor", ObjFac.Proveedor);
                cmd.Parameters.AddWithValue("@direccion", ObjFac.Direccion);
                cmd.Parameters.AddWithValue("@NumeroFactura", ObjFac.NumeroFactura);
                cmd.Parameters.AddWithValue("@TiempoGar", ObjFac.TiempGar);
                cmd.Parameters.AddWithValue("@FechaCompra", ObjFac.FechaCompra);
                cmd.Parameters.AddWithValue("@fechaFinGar", ObjFac.FecFinGar);

                cmd.Parameters.AddWithValue("@Observacion", ObjN.Observacion);
                cmd.Parameters.AddWithValue("@UsuAsignado", ObjN.UsuAsignado);
                cmd.Parameters.AddWithValue("@ip", ObjN.ip);

                rpta = "Se Actualizó Correctamente";
                cmd.ExecuteNonQuery();
                // catch
            }
            return rpta;

        }

        public void ActualizarLogistica_masivo(string cod, LogisticaEn logis, FacturaEn ObjFac)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspLogistica_modificarMasivo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@codigo", cod);
                    cmd.Parameters.AddWithValue("@area", logis.Area);
                    cmd.Parameters.AddWithValue("@edificio", logis.Edificio);
                    cmd.Parameters.AddWithValue("@aula", logis.Aula);
                    cmd.Parameters.AddWithValue("@piso", logis.Piso);
                    cmd.Parameters.AddWithValue("@Proveedor", ObjFac.Proveedor);
                    cmd.Parameters.AddWithValue("@ruc", ObjFac.ruc);
                    cmd.Parameters.AddWithValue("@nroComprobante", ObjFac.NumeroFactura);
                    cmd.Parameters.AddWithValue("@fechaCompra", ObjFac.FechaCompra);
                    cmd.Parameters.AddWithValue("@TiempoGar", ObjFac.TiempGar);
                    cmd.Parameters.AddWithValue("@fechaFinGar", ObjFac.FecFinGar);
                    cmd.Parameters.AddWithValue("@sede", logis.IdSede);
                    cmd.Parameters.AddWithValue("@usuAsignado", logis.UsuAsignado);
                    cmd.Parameters.AddWithValue("@idLogin", logis.IdLogin);
                    cmd.Parameters.AddWithValue("@ip", logis.ip);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}
