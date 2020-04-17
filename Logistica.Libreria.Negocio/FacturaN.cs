using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Datos;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Logistica.Libreria.Negocio
{
    public class FacturaN
    {
        FacturaDAO objFac = new FacturaDAO();
        
        public FacturaEn fun_buscar_proveedor_xRuc(string ruc)
        {
            try
            {
                return objFac.buscar_proveedor_xRuc(ruc);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void pr_registrar_empresa_api(FacturaEn emp)
        {
            try
            {
                objFac.registrar_empresa_api(emp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void pr_registrar_factura_guia(FacturaEn factguia)
        {
            try
            {
                objFac.registrar_factura_guia(factguia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }          
        }

        public void pr_registrar_consulta_api(string sede, string usuario, string pagina, string valor, string ip)
        {
            try
            {
                objFac.registrar_consulta_api(sede, usuario, pagina, valor, ip);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void pr_validar_CajaTexto(TextBox txt)
        {
            try
            {
                if (txt.Text == "")
                {
                    txt.BackColor = System.Drawing.Color.Yellow;
                    txt.Focus();
                    throw new Exception("Advertencia: Este campo es obligatorio");
                }
                else
                {
                    txt.BackColor = System.Drawing.Color.White;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }      
    }
}
