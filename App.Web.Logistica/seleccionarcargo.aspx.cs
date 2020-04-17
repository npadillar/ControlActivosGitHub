using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logistica.Libreria.Negocio;

namespace App.Web.Logistica
{
    public partial class seleccionarcargo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoginN objAcc = new LoginN();
                    string idLocal = Request.QueryString["il"].ToString();
                    dgvListado.DataSource = objAcc.fun_listarCargos_usuario_xSede(Session["usuario"].ToString(), idLocal);
                    dgvListado.DataBind();
                }
            }
            catch (Exception ex)
            {
                dvError.InnerHtml = ex.Message;
                dvError.Visible = true;
            }
        }

        protected void dgvListado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt16(e.CommandArgument);
                int idCar = Convert.ToInt16(dgvListado.DataKeys[index].Value);

                if (index != -1 && e.CommandName == "seleccionar")
                {
                    Session["cargo"] = idCar;
                    Response.Redirect("menu.aspx");
                }
            }
            catch (Exception ex)
            {
                dvError.InnerHtml = ex.Message;
                dvError.Visible = true;
            }
        }
    }
}