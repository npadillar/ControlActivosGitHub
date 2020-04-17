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
    public class SedeDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        public DataTable ListarSede(string pag)
        {
            DataTable tb = new DataTable();

            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = cad_cn;
                cn.Open();
                SqlDataAdapter adap = new SqlDataAdapter("uspListarSede", cn);
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@pag", pag);
                adap.SelectCommand.ExecuteNonQuery();
                adap.Fill(tb);
            }
            return tb;
        }
    }
}
