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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Web.Logistica
{
    public partial class BuscarActivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;


            //ModificarLogisticaN objModfLog = new ModificarLogisticaN();

            //DataTable DTModLog = objModfLog.BuscarCodigo(txtCodigo.Text);
            //gvReporte.DataSource = DTModLog;
            //gvReporte.DataBind();
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.close();</script>");
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            //this.Page.Response.Write("<script language ='JavaScript'>window.alert('ingreso.');</script>");
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BajaActivosN objBaja = new BajaActivosN();
            if (objBaja.fun_bloquear_activo_baja(txtCodigo.Text))
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('El activo ya fue dado de baja');</script>");
            }
            else
            {
                LogisticaN objBuscarcodigoenGuia = new LogisticaN();
                Session["CODIGO"] = null;
                DataTable DTBuscarCodigoenGuia = objBuscarcodigoenGuia.BuscarCodigoenGuia(txtCodigo.Text);

                gvReporte.DataSource = DTBuscarCodigoenGuia;
                gvReporte.DataBind();

                if (((System.Data.DataTable)(gvReporte.DataSource)).ExtendedProperties.Count != 0)
                {
                    Session["CODIGO"] = txtCodigo.Text;
                }
            }
        }


        protected void gvReporte_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            GridViewRow row = gvReporte.Rows[e.NewSelectedIndex];

            string cod = row.Cells[2].Text;

            Session["IdInventario"] = row.Cells[1].Text;
            Session["Codigo"] = row.Cells[2].Text;

            //  Response.Write("<script>window.open('Guia.aspx','popup','width=1200,height=500,top=200,left=200,addressbar=0') </script>");
            //  Response.Redirect("Guia.aspx?IdLogistica=" + cod);


            //Response.Redirect("Guia.aspx?IdLogistica=" + cod);
            //Response.Write("<script>window.close();</script>");
            ClientScript.RegisterClientScriptBlock(GetType(), "Refresca", "window.opener.location.reload(); window.close();", true);
        }
    }
}