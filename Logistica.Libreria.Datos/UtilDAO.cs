using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Datos
{
    public class UtilDAO
    {
        public DataTable fun_ejecutar_script_dt(string bd, string sp, string tipo = "text")
        {
            ConexionDAO objCn = new ConexionDAO();

            using (SqlConnection cn = new SqlConnection(objCn.conex(bd)))
            {
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sp, cn);
                if (tipo == "text")
                    da.SelectCommand.CommandType = CommandType.Text;
                else
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public object fun_ejecutar_script(string bd, string sp, string tipo = "text")
        {
            ConexionDAO objCn = new ConexionDAO();

            using (SqlConnection cn = new SqlConnection(objCn.conex(bd)))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sp, cn);
                cmd.CommandType = (tipo == "text") ? CommandType.Text : CommandType.StoredProcedure;
                object x = cmd.ExecuteScalar();
                return x;
            }
        }
    }
}
