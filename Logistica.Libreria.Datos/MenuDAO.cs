using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using est = Logistica.Libreria.Entidad.MenuEst;

namespace Logistica.Libreria.Datos
{
    public class MenuDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn2"].ConnectionString;

        public List<est.EST_MODULO> listarModulos_xSistema_xCargo(int cargo)
        {
            try
            {
                List<est.EST_MODULO> mod = new List<est.EST_MODULO>();
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_tbModulo_listar_xSistema_xCargo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idSis", 4);
                    cmd.Parameters.AddWithValue("@idCar", cargo);
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows == true)
                        while (dr.Read())
                        {
                            est.EST_MODULO modulo = new est.EST_MODULO();
                            modulo.idMod = Convert.ToInt16(dr[0]);
                            modulo.descrip = dr[1].ToString();
                            mod.Add(modulo);
                        }
                }
                return mod;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<est.EST_PAGINA> listarPaginas_xModulo_xCargo(int modulo, int cargo)
        {
            try
            {
                List<est.EST_PAGINA> pag = new List<est.EST_PAGINA>();
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_tbPagina_listar_xModulo_xCargo", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idMod", modulo);
                    cmd.Parameters.AddWithValue("@idCar", cargo);
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows == true)
                        while (dr.Read())
                        {
                            est.EST_PAGINA pagina = new est.EST_PAGINA();
                            pagina.descrip = dr["pagina"].ToString();
                            pagina.link = dr["link"].ToString();
                            // pagina.pagper.permiso = dr["permiso"].ToString();
                            pag.Add(pagina);
                        }
                }
                return pag;
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
                using (SqlConnection cn = new SqlConnection(cad_cn))
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
    }
}
