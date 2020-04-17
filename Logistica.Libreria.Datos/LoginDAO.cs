using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Logistica.Libreria.Entidad;

namespace Logistica.Libreria.Datos
{
    public class LoginDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        string cad_cn2 = ConfigurationManager.ConnectionStrings["cn2"].ConnectionString;
        string sql;

        public int ValidarAcceso(string Usuario, string Clave)
        {
            int rpta = 0;
            LoginEn ObjE = new LoginEn();
            using (SqlConnection cn = new SqlConnection(cad_cn))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("uspValidarAcceso", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Usuario", Usuario);
                cmd.Parameters.AddWithValue("@Clave", Clave);



                //SqlParameter retVal = new SqlParameter("@Valor",ObjE.IdLogin);
                SqlParameter retVal = new SqlParameter("@Valor", 0);
                retVal.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retVal);
                cmd.ExecuteNonQuery();
                rpta = Int32.Parse(cmd.Parameters["@Valor"].Value.ToString());
                if (rpta == 0)
                {
                    cmd.Dispose();
                    return rpta;
                }
                return rpta;
            }
        }

        public int ObtenerPerfil(int xIdUsuario)
        {
            int rpta = 0;
            LoginEn ObjE = new LoginEn();
            using (SqlConnection cn = new SqlConnection(cad_cn))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("uspObtenerPerfil", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUsuario", xIdUsuario);

                SqlParameter retVal = new SqlParameter("@IdPerfil", 0);
                retVal.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retVal);
                cmd.ExecuteNonQuery();
                rpta = Int32.Parse(cmd.Parameters["@IdPerfil"].Value.ToString());
                if (rpta == 0)
                {
                    cmd.Dispose();
                    return rpta;
                }
                return rpta;
            }
        }

        public DataTable ObtenerMenuUsuario(int intPerfil)
        {
            DataTable dt = new DataTable();
            LoginEn ObjE = new LoginEn();
            using (SqlConnection cn = new SqlConnection(cad_cn))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("uspObtenerOpciones", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPerfil", intPerfil);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }



        // new 11-01-19
        public DataTable fun_ejecutar_sp_sin_parametros(string sede, string sp, string tipo = "sp")
        {
            using (SqlConnection cn = new SqlConnection(cad_cn2))
            {
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sp, cn);
                if (tipo == "sp")
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    da.SelectCommand.CommandType = CommandType.Text;
                }
                da.SelectCommand.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable ListarSedes()
        {
            try
            {
                return fun_ejecutar_sp_sin_parametros("ACC", "usp_tbLocal_listarSedes");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool login(string usuario, string clave)
        {
            try
            {
                bool b = false;
                int fila;
                using (SqlConnection cn = new SqlConnection(cad_cn2))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_tbLogin_acceso", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    fila = Convert.ToInt16(cmd.ExecuteScalar());

                    if (fila > 0)
                    {
                        b = true;
                    }
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool verificar_estadoUsu(string usuario)
        {
            try
            {
                bool b = false;
                int fila;
                using (SqlConnection cn = new SqlConnection(cad_cn2))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_tbLogin_estado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    fila = Convert.ToInt16(cmd.ExecuteScalar());

                    if (fila > 0)
                    {
                        b = true;
                    }
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool verificar_AlcanceTotal(string usuario)
        {
            try
            {
                bool b = false;
                int fila;
                using (SqlConnection cn = new SqlConnection(cad_cn2))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_tbLogin_alcanceTotal", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@idSis", 4); // sistema de control activos
                    fila = Convert.ToInt16(cmd.ExecuteScalar());

                    if (fila > 0)
                    {
                        b = true;
                    }
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable listarCargos_usuario_xSede(string usuario, string idLocal)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cad_cn2))
                {
                    cn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("usp_tbLogin_listar_cargos_usu", cn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@usuario", usuario);
                    da.SelectCommand.Parameters.AddWithValue("@sistema", 4);  // sistema de control activos
                    da.SelectCommand.Parameters.AddWithValue("@idLocal", idLocal);
                    da.SelectCommand.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool mostrarControl_xCargo(int control, int cargo)
        {
            try
            {
                bool b = false;
                using (SqlConnection cn = new SqlConnection(cad_cn2))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_tbControles_permiso_xCargo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idControl", control);
                    cmd.Parameters.AddWithValue("@idCar", cargo);
                    int contar = Convert.ToInt16(cmd.ExecuteScalar());

                    if (contar > 0)
                    {
                        b = true;
                    }
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string traer_nombre_persona(string usuario)
        {
            try
            {
                string nombre = "";
                using (SqlConnection cn = new SqlConnection(cad_cn2))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_tbUsuario_traerNombre", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    nombre = cmd.ExecuteScalar().ToString();
                }
                return nombre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Int32 traer_idTra_persona(string usuario)
        {
            try
            {
                Int32 idTra = 0;
                sql = "select idTra from tbUsuario where email = '" + usuario + "'";
                using (SqlConnection cn = new SqlConnection(cad_cn2))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand(sql, cn);
                    cmd.CommandType = CommandType.Text;
                    
                    if (cmd.ExecuteScalar() is DBNull)
                        idTra = 0;
                    else
                        idTra = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return idTra;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public void Guardar_usuario(int idTrabajador, string nombre, string usuario)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspRegistrarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idTra", idTrabajador);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
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
