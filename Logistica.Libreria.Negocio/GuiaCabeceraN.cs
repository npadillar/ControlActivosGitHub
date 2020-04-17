using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Datos;

namespace Logistica.Libreria.Negocio
{
    public class GuiaCabeceraN
    {
        GuiaCabeceraDAO objguia = new GuiaCabeceraDAO();

        public string InsertarGuiaCabecera(GuiaCabeceraEn objE)
        {
            return objguia.InsertarGuiaCabecera(objE);
        }

        public void pr_registrar_auditoria_guia(GuiaCabeceraEn ObjG)
        {
            try
            {
                objguia.registrar_auditoria_guia(ObjG);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ActualizarGuiaCabecera(GuiaCabeceraEn ObjG)
        {
            return objguia.ActualizarGuiaCabecera(ObjG);
        }

        public string ActualizarEstadoGuia(GuiaCabeceraEn ObjG)
        {
            return objguia.ActualizarEstadoGuia(ObjG);
        }

        public DataTable BuscarGuiaCabecera(int xIdGuia)
        {
            return objguia.BuscarGuiaCabecera(xIdGuia);
        }

        public string fun_traer_observacion(int xIdGuia)
        {
            try
            {
                return objguia.traer_observacion(xIdGuia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GuiaCabeceraEn fun_Traer_datos_guia(int idGuia)
        {
            try
            {
                DataTable dt = new DataTable();
                GuiaCabeceraEn guia = new GuiaCabeceraEn();

                dt = objguia.Traer_datos_guia(idGuia);
                guia.Fecha = Convert.ToDateTime(dt.Rows[0]["fecha"]);
                guia.Transportista = dt.Rows[0]["Transportista"].ToString();
                return guia;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
