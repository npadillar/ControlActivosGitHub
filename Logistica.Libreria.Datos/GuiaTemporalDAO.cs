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
      
    public class GuiaTemporalDAO
    {
        
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
          public string InsertarGuiaTemporal(GuiaTemporalEn ObjG)
        {
            string rpta = "";

            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertarGuiaTemporal", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                   // cmd.Parameters.AddWithValue("@IdGuia", ObjG.IdGuia);
                    cmd.Parameters.AddWithValue("@IdLogistica", ObjG.IdLogistica);
                    cmd.Parameters.AddWithValue("@Codigo", ObjG.Codigo);
                    cmd.Parameters.AddWithValue("@Descripcion", ObjG.Descripcion);
                    cmd.Parameters.AddWithValue("@Origen", ObjG.Origen);
                    cmd.Parameters.AddWithValue("@Detalle", ObjG.Detalle);
                    cmd.Parameters.AddWithValue("@Condicion", ObjG.Condicion);
                    cmd.Parameters.AddWithValue("@IdUnidadMedida", ObjG.idUnidadMedida);
                    cmd.Parameters.AddWithValue("@UnidadMedida", ObjG.UnidadMedida);

                    
                    rpta = "Se Guardó Correctamente";
                    cmd.ExecuteNonQuery();
                    // catch
                }
                return rpta;
            }

        }
          public DataTable ListarGuiaTemporal()
          {
              DataTable tb = new DataTable();
              using (SqlDataAdapter adap = new SqlDataAdapter("uspListarGuiaTemporal", cad_cn))
              {
                  adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                  adap.Fill(tb);
              }
              return tb;
          }

          public DataTable EliminarGuiaTemporal()
          {
              DataTable tb = new DataTable();
              using (SqlDataAdapter adap = new SqlDataAdapter("uspEliminarGuiaDetalle", cad_cn))
              {
                  adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                  adap.Fill(tb);
              }
              return tb;
          }
    }
}
