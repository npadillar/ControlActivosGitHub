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
    public class InventarioDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;


        public DataTable BuscaInventario(string xcod)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspBuscarInventario", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Codigo", xcod);
                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable BuscaInv(string xcod)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspListarInv", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Codigo", xcod);
                adap.Fill(tb);
            }
            return tb;
        }

            public DataTable ListarInventario()
            {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspListarInventario", cad_cn))
                {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.Fill(tb);
                 }
            return tb;
            }


            public string InsertarInventario(InventarioEn objE)
            {
                string rpta = "";

                {
                    using (SqlConnection cn = new SqlConnection(cad_cn))
                    {
                        // try
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("uspInsertarInventario", cn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CODIGO", objE.Codigo);
                        cmd.Parameters.AddWithValue("@DESCRIPCION", objE.Descripcion);
                        cmd.Parameters.AddWithValue("@SEDE", objE.IdSede);

                        cmd.Parameters.AddWithValue("@Subsede", objE.SubSede);
                        cmd.Parameters.AddWithValue("@Marca", objE.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", objE.Modelo);
                        cmd.Parameters.AddWithValue("@Fecha",  objE.FechaAdquisicion);
                                                   
                        cmd.Parameters.AddWithValue("@AREA", objE.Area);
                        cmd.Parameters.AddWithValue("@PISO", objE.Piso);
                        cmd.Parameters.AddWithValue("@EDIFICIO", objE.Edificio);
                        cmd.Parameters.AddWithValue("@CATEGORIA", objE.IdCategoria);
                        cmd.Parameters.AddWithValue("@ESTADO", objE.Estado);


                        rpta = "Se Guardó Correctamente";
                        cmd.ExecuteNonQuery();
                        // catch
                    }
                    return rpta;
                }
            }
                    public bool EliminarInventario(string xId)
                        {
                            //string rpta = "";

                             {
                                 using (SqlConnection cn = new SqlConnection(cad_cn))
                                 {
                                     // try
                                     cn.Open();
                                     SqlCommand cmd = new SqlCommand("spEliminarInventario", cn);
                                     cmd.CommandType = CommandType.StoredProcedure;
                                     cmd.Parameters.AddWithValue("@Id", xId);

                                     if (cmd.ExecuteNonQuery() <= 0)
                                     {
                                         cmd.Dispose();
                                         return false;
                                     }
                                     return true;
                                 }
                            }
                           }
                    public string ActualizarInventario(InventarioEn ObjN)
                    {
                        string rpta = "";

                        using (SqlConnection cn = new SqlConnection(cad_cn))
                        {
                            // try
                            cn.Open();
                            SqlCommand cmd = new SqlCommand("uspActualizarInventario", cn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Id", ObjN.Id);
                            cmd.Parameters.AddWithValue("@Codigo", ObjN.Codigo);
                            cmd.Parameters.AddWithValue("@Descripcion", ObjN.Descripcion);
                            cmd.Parameters.AddWithValue("@Sede", ObjN.IdSede);

                            cmd.Parameters.AddWithValue("@Subsede", ObjN.SubSede);
                            cmd.Parameters.AddWithValue("@Marca", ObjN.Marca);
                            cmd.Parameters.AddWithValue("@Modelo", ObjN.Modelo);
                            cmd.Parameters.AddWithValue("@Fecha", ObjN.FechaAdquisicion);

                            cmd.Parameters.AddWithValue("@Area", ObjN.Area);
                            cmd.Parameters.AddWithValue("@Piso", ObjN.Piso);
                            cmd.Parameters.AddWithValue("@Edificio", ObjN.Edificio);
                            cmd.Parameters.AddWithValue("@Categoria", ObjN.IdCategoria);
                            cmd.Parameters.AddWithValue("@Estado", ObjN.Estado);

                            rpta = "Se Actualizó Correctamente";
                            cmd.ExecuteNonQuery();
                            // catch
                        }
                        return rpta;

                    }
    
    }
}
