using Logistica.Libreria.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Web.Logistica
{
    public partial class CambiosCodigo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ReportesN objGuiaDetalle = new ReportesN();
                    int idLogistica = int.Parse(Request.QueryString["id"].ToString());
                    dgvListado.DataSource = objGuiaDetalle.fun_Listar_cambiosCodigo_xIdLogistica(idLogistica);
                    dgvListado.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.close();</script>");
        }
    }
}