using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;
using System.Windows.Forms;
using System.Text;

namespace App.Web.Logistica
{
    public partial class MasterPrincipal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Usuario"] == null || Session["Usuario"].ToString() == "")
                {
                    //Response.Redirect("https://sistemas.sise.com.pe/sistemas/");
                }
                else
                {
                    lblUsuario.Text = Session["Usuario"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }   
    }
}