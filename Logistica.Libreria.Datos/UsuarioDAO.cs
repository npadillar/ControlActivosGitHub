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
    public class UsuarioDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;

        public DataTable ListarUsuario()
        {
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter("uspListarUsuario", cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable ListarUsuario_Auditoria()
        {
            string sql = "select distinct a.usuario as id, l.nombre";
            sql += " from auditoria a inner join login l";
            sql += " on a.usuario = l.IdLogin";
            sql += " order by 2";
            
            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter(sql, cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.Text;
                adap.Fill(tb);
            }
            return tb;
        }

        public DataTable ListarUsuario_bajas()
        {
            string sql = "select distinct ba.IdLogin as id, l.nombre ";
            sql += "from BajaActivosCabecera ba inner join login l ";
            sql += "on ba.IdLogin = l.IdLogin ";
            sql += "order by 2";

            DataTable tb = new DataTable();
            using (SqlDataAdapter adap = new SqlDataAdapter(sql, cad_cn))
            {
                adap.SelectCommand.CommandType = CommandType.Text;
                adap.Fill(tb);
            }
            return tb;
        }
    }
}
