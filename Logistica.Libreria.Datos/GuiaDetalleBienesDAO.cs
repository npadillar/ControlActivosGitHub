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
    public class GuiaDetalleBienesDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        public string InsertarGuiaDetalleBienes(GuiaDetalleBienesEn ObjD)
        {
            string rpta = "";

            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertarGuiaDetalleBienes", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //adicionar campos detalle bienes
                    cmd.Parameters.AddWithValue("@IdGuia", ObjD.IdGuia);
                    cmd.Parameters.AddWithValue("@IdUnidadMedida", ObjD.IdUnidadMedida);
                    cmd.Parameters.AddWithValue("@Cant", ObjD.Cant);
                    cmd.Parameters.AddWithValue("@Descripcion", ObjD.Descripcion);

                    rpta = "Se Guardó Correctamente";
                    cmd.ExecuteNonQuery();
                    // catch
                }
                return rpta;
            }

        }
    }
}
