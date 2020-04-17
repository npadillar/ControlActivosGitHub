using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logistica.Libreria.Negocio;
using System.Data;
using System.Globalization;

namespace App.Web.Logistica
{
    public partial class login : System.Web.UI.Page
    {
        LoginN objLogin = new LoginN();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("Usuario");
            Session.Remove("nomUsuario");
            Session.Remove("nomSede");
            Session.Remove("cargo");
            Session.Remove("rpta");

            try
            {
                if (!Page.IsPostBack)
                {
                    cboSede.DataSource = objLogin.fun_ListarSedes();
                    cboSede.DataValueField = "bd";
                    cboSede.DataTextField = "local";
                    cboSede.DataBind();
                    cboSede.SelectedIndex = 2;

                    TxtUsuario.Focus();
                    Session.Abandon();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                if (TxtUsuario.Text.Equals(""))
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('<<Ingresar Usuario>>');</script>");
                    return;
                }
                if (TxtClave.Text.Equals(""))
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('<<Ingresar Clave>>');</script>");
                    return;
                }


                string usu = TxtUsuario.Text.Trim();
                string clave = TxtClave.Text;
                bool alcance = false;


                if (objLogin.fun_login(usu, clave))
                {
                    if (objLogin.fun_verificar_estadoUsu(usu)) // comprobar si el usuario esta activo
                    {
                        alcance = objLogin.fun_verificar_AlcanceTotal(usu);

                        if (alcance)
                        {
                            Redireccionar_Pagina(usu, "");
                        }
                        else
                        {
                            string sede = cboSede.SelectedValue;
                            Redireccionar_Pagina(usu, sede.Substring(0, sede.IndexOf(" - ")));
                        }
                    }
                    else
                    {
                        throw new Exception("Su usuario se encuentra deshabilitado");
                    }
                }
                else
                {
                    throw new Exception("Datos incorrectos... verifique por favor");
                }
            }
            catch (Exception ex)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
                return;
            }
        }


        private void Redireccionar_Pagina(string usuario, string idLocal)
        {
            DataTable dtCargos = new DataTable();
            int contarCargos = 0;

            dtCargos = objLogin.fun_listarCargos_usuario_xSede(usuario, idLocal);
            contarCargos = dtCargos.Rows.Count;

            if (contarCargos == 0)
            {
                throw new Exception("Usted no tiene permisos para acceder a este sistema");
            }
            else
            {
                Session["Usuario"] = TxtUsuario.Text;
                Session["nomUsuario"] = objLogin.fun_traer_nombre_persona(TxtUsuario.Text);
                Session["nomSede"] = cboSede.SelectedItem;
                Session["rpta"] = objLogin.fun_traer_idTra_persona(usuario);

                string nomUsu = Session["nomUsuario"].ToString();
                nomUsu = nomUsu.Substring(0, nomUsu.LastIndexOf(" ")).ToLower();
                nomUsu = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nomUsu);

                objLogin.pr_Guardar_usuario(Convert.ToInt32(Session["rpta"]), nomUsu, usuario);

                if (contarCargos == 1)
                {
                    Session["cargo"] = dtCargos.Rows[0]["idCar"].ToString();
                    Response.Redirect("menu.aspx");
                }
                else
                {
                    Response.Redirect("seleccionarcargo.aspx?il=" + idLocal);
                }
            }
        }

        //private void definirConex_Series()
        //{
        //    string sede = cboSede.SelectedValue;
        //    Session["idLocal"] = sede.Substring(0, sede.IndexOf(" - "));
        //    Session["sede"] = sede.Substring(sede.IndexOf(" - ") + 3);
        //}
    }
}