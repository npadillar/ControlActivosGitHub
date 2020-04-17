using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistica.Libreria.Entidad;
using System.Data;

namespace Logistica.Libreria.Datos
{
    public class ReparacionesDAO
    {
        ConexionDAO objCn = new ConexionDAO();

        public int registrar_reparacion_cab(ReparacionEn rep)
        {
            try
            {
                int idReparacion = 0;
                using (SqlConnection cn = new SqlConnection(objCn.conex("activos")))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_ReparacionCabecera_ins", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idGuia", rep.idGuia);
                    cmd.Parameters.AddWithValue("@total", rep.total);
                    cmd.Parameters.AddWithValue("@observ", rep.observ);
                    idReparacion = int.Parse(cmd.ExecuteScalar().ToString());
                }
                return idReparacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void registrar_reparacion_det(ReparacionDetEn det)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(objCn.conex("activos")))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_ReparacionDetalle_ins", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idReparacion", det.idReparacion);
                    cmd.Parameters.AddWithValue("@codigo", det.codigo);
                    cmd.Parameters.AddWithValue("@nroFactura", det.nroFactura);
                    cmd.Parameters.AddWithValue("@fecReparacion", det.fecReparacion);
                    cmd.Parameters.AddWithValue("@tiempoGar", det.tiempoGar);
                    cmd.Parameters.AddWithValue("@fecFinGar", det.fecFinGar);
                    cmd.Parameters.AddWithValue("@costo", det.costo);
                    cmd.Parameters.AddWithValue("@motivo", det.motivo);
                    cmd.Parameters.AddWithValue("@trabRealizado", det.trabRealizado);
                    cmd.Parameters.AddWithValue("@transportista", det.transportista);
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
