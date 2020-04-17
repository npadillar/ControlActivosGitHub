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
    public class GuiaDetalleActivosDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        public string InsertarGuiaDetalleActivos(GuiaDetalleActivosEn ObjD)
        {
            string rpta = "";

            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    // try
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("uspInsertarGuiaDetalleActivos", cn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //adicionar campos detalle
                    cmd.Parameters.AddWithValue("@IdGuia", ObjD.IdGuia);
                    cmd.Parameters.AddWithValue("@IdUnidadMedida", ObjD.IdUnidadMedida);
                    cmd.Parameters.AddWithValue("@IdLogistica", ObjD.IdLogistica);

                    rpta = "Se Guardó Correctamente";
                    cmd.ExecuteNonQuery();
                    // catch
                }
                return rpta;
            }

        }

        public DataTable BuscarGuiaActivos(string xcod)
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspListarGuiaActivo", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Guia", xcod);
                adap.Fill(tb);
            }
            return tb;
        }
    }
}
