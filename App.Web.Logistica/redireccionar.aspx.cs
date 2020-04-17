using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logistica.Libreria.Negocio;
using System.Globalization;

namespace App.Web.Logistica
{
    public partial class redireccionar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoginN objLogin = new LoginN();
                    string usuario = Request.QueryString["u"];
                    Session["Usuario"] = usuario;
                    string nomUsu = objLogin.fun_traer_nombre_persona(usuario);
                    Session["nomUsuario"] = nomUsu;
                    Session["sede"] = Request.QueryString["s"];
                    Session["cargo"] = Request.QueryString["c"];
                    Session["nomSede"] = Request.QueryString["ns"];
                    Session["idLocal"] = Request.QueryString["il"];
                    Session["rpta"] = objLogin.fun_traer_idTra_persona(usuario);

                    nomUsu = nomUsu.Substring(0, nomUsu.LastIndexOf(" ")).ToLower();
                    nomUsu = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nomUsu);

                    objLogin.pr_Guardar_usuario(Convert.ToInt32(Session["rpta"]), nomUsu, usuario);
                    Response.Redirect("menu.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}