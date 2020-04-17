using Logistica.Libreria.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App.Web.Logistica
{
    public partial class RptCambiosCodigo : System.Web.UI.Page
    {
        ReportesN objRpt = new ReportesN();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Usuario"] == null || Session["Usuario"].ToString() == "")
                {
                    Response.Redirect("https://sistemas.sise.com.pe/sistemas/");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        lblUsuario.Text = Session["Usuario"].ToString();

                        txtFecIni.Text = DateTime.Now.ToString("2017-01-31");
                        txtFecFin.Text = DateTime.Now.ToString("yyyy-MM-dd");

                        cboCantidad.DataTextField = "cant";
                        cboCantidad.DataValueField = "cant";
                        cboCantidad.DataSource = objRpt.fun_cantidad_cambios_codigo();
                        cboCantidad.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                gdvDatos.DataSource = objRpt.fun_ListarReporte_cambiosCodigo(DateTime.Parse(txtFecIni.Text), DateTime.Parse(txtFecFin.Text), txtCodigo.Text.Trim(), int.Parse(cboCantidad.SelectedValue));
                gdvDatos.DataBind();
                lblRegistros.Text = gdvDatos.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }
    }
}