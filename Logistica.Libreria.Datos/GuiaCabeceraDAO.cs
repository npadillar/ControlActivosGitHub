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
    public class GuiaCabeceraDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        UtilDAO objUtil = new UtilDAO();

        public string InsertarGuiaCabecera(GuiaCabeceraEn ObjG)
        {
            int rpta = 0;

            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertarGuiaCabecera", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                   // cmd.Parameters.AddWithValue("@IdGuia", ObjG.IdGuia);
                    cmd.Parameters.AddWithValue("@IdLogin", ObjG.IdLogin);
                    cmd.Parameters.AddWithValue("@SedePartida", ObjG.SedePartida);
                    cmd.Parameters.AddWithValue("@SedeLlegada", ObjG.SedeLlegada);


                    cmd.Parameters.AddWithValue("@OrigenDestinoExterno", ObjG.OrigenDestinoExterno);

                    cmd.Parameters.AddWithValue("@Fecha", ObjG.Fecha);
                    cmd.Parameters.AddWithValue("@Transportista", ObjG.Transportista);
                    cmd.Parameters.AddWithValue("@IdMotivoTraslado", ObjG.IdMotivoTraslado);
                    cmd.Parameters.AddWithValue("@Activos", ObjG.Activos);
                    cmd.Parameters.AddWithValue("@IdEstado", ObjG.IdEstado);
                    cmd.Parameters.AddWithValue("@observ", ObjG.observ);

                    //adicionar campos detalle
                    //cmd.Parameters.AddWithValue("@IdUnidadMedida", ObjD.IdUnidadMedida);
                    //cmd.Parameters.AddWithValue("@IdLogistica", ObjD.IdLogistica);
                    SqlParameter retval = new SqlParameter("@IdGuia", 0);   
                    retval.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(retval);
                    cmd.ExecuteNonQuery();
                    rpta = Int32.Parse((cmd.Parameters["@IdGuia"].Value.ToString()));
                    //if (rpta == 0)
                    //{
                    cmd.Dispose();
                    return Convert.ToString(rpta);
                    //}
                  //  rpta = "Se Guardó Correctamente";
                    cmd.ExecuteNonQuery();
                     //catch 
                    //return rpta;
                }
               
            }

        }


        public void registrar_auditoria_guia(GuiaCabeceraEn ObjG)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspGuiaAuditoria", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@activos", ObjG.Activos);
                    cmd.Parameters.AddWithValue("@codGuia", ObjG.IdGuia);
                    cmd.Parameters.AddWithValue("@idLogin", ObjG.IdLogin);
                    cmd.Parameters.AddWithValue("@ip", ObjG.ip);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ActualizarGuiaCabecera(GuiaCabeceraEn ObjG)
        {           
            {
                 string rpta = "";
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspActualizarGuiaCabecera", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdGuia", ObjG.IdGuia);
                    cmd.Parameters.AddWithValue("@IdLoginRecibido", ObjG.IdLoginRecibido);
                    cmd.Parameters.AddWithValue("@FechaRecepcion", ObjG.FechaRecepcion);
                    cmd.Parameters.AddWithValue("@ip", ObjG.ip);         
                    
                   rpta = "Se Actualizó Correctamente";
                      cmd.ExecuteNonQuery();
         
                }
            return rpta;
                }

        }

        public string ActualizarEstadoGuia(GuiaCabeceraEn ObjG)
        {
            {
                string rpta = "";
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspActualizarEstado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdGuia", ObjG.IdGuia);
                    cmd.Parameters.AddWithValue("@IdEstado", ObjG.IdEstado);
                    cmd.Parameters.AddWithValue("@IdLoginAnulado", ObjG.IdLoginAnulado);
                    cmd.Parameters.AddWithValue("@FechaAnulacion", ObjG.FechaAnulacion);
                    cmd.Parameters.AddWithValue("@ip", ObjG.ip);

                    rpta = "Se Anulo Correctamente";
                    cmd.ExecuteNonQuery();

                }
                return rpta;
            }

        }

        public DataTable BuscarGuiaCabecera(int xIdGuia)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspBuscarGuiaCabecera", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@IdGuia", xIdGuia);
                adap.Fill(tb);
            }
            return tb;
        }

        public string traer_observacion(int xIdGuia)
        {
            try
            {
                string sql = "select case when observacion is null then 'Sin observación' else observacion end as observ from GuiaCabecera where IdGuia = " + xIdGuia.ToString();
                string observ = "";

                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;
                    observ = cmd.ExecuteScalar().ToString();
                }
                return observ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable Traer_datos_guia(int idGuia)
        {
            try
            {
                string sql = "select fecha, Transportista from GuiaCabecera where IdGuia = " + idGuia.ToString();
                return objUtil.fun_ejecutar_script_dt("activos", sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

