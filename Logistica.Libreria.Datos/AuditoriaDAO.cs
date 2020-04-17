using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using est = Logistica.Libreria.Entidad.AuditoriaEst;

namespace Logistica.Libreria.Datos
{
    public class AuditoriaDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;

        public DataTable listar_paginas()
        {
            try
            {
                string sql = "select distinct pagina from auditoria";
                DataTable dt = new DataTable();
                using (SqlDataAdapter adap = new SqlDataAdapter(sql, cad_cn))
                {
                    adap.SelectCommand.CommandType = CommandType.Text;
                    adap.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable listar_auditoria(est.EST_AUDITORIA audi)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter adap = new SqlDataAdapter("usp_auditoria_listar", cad_cn))
                {
                    adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adap.SelectCommand.Parameters.AddWithValue("@fecIni", audi.fecIni);
                    adap.SelectCommand.Parameters.AddWithValue("@fecFin", audi.fecFin);
                    adap.SelectCommand.Parameters.AddWithValue("@accion", audi.accion);
                    adap.SelectCommand.Parameters.AddWithValue("@usuario", audi.idUsuario);
                    adap.SelectCommand.Parameters.AddWithValue("@pagina", audi.pagina);
                    adap.SelectCommand.Parameters.AddWithValue("@codigo", audi.codigo);
                    adap.SelectCommand.Parameters.AddWithValue("@idTabla", audi.idTabla);
                    adap.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
