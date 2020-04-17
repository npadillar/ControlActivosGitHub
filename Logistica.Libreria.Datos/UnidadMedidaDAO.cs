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
  public class UnidadMedidaDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;

        public DataTable ListarUnidadMedida()
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspListarUnidadMedida", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.Fill(tb);
            }
            return tb;
        }
    }
}
