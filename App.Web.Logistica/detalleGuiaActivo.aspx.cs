using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;

namespace App.Web.Logistica
{
    public partial class detalleGuiaActivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            txtCodigo.Text = Request.QueryString["c"];
            //txtCodigo.Text = Session["CODIGO"].ToString().ToUpper();
            ModificarLogisticaN objModfAc = new ModificarLogisticaN();

            DataTable DTModLog = objModfAc.BuscarCodigoActivo(txtCodigo.Text);
            gvDetalleActivo.DataSource = DTModLog;
            gvDetalleActivo.DataBind();
        }
    }
}