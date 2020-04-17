using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;

namespace App.Web.Logistica.Reportes
{
    public partial class ReporteBajaActivos2 : System.Web.UI.Page
    {
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

                        CargarDatos();
                        MenuN objMenu = new MenuN();
                        if (objMenu.fun_MostrarControl_xCargo(8, Convert.ToInt16(Session["cargo"]))) btnExportar.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }


        private void CargarDatos()
        {
            MotivoBajaActivosN objM = new MotivoBajaActivosN();
            DisposicionBajaActivosN objD = new DisposicionBajaActivosN();
            UsuarioN ObjUsuario = new UsuarioN();
            //  SedeN ObjSede = new SedeN();
            //CondicionN ObjCond = new CondicionN();

            txtFechaDesde.Text = Convert.ToDateTime("01/01/2017").ToString("yyyy-MM-dd");
            DateTime fecha2 = DateTime.Now;
            txtFechaHasta.Text = (fecha2.ToString("yyyy-MM-dd"));

            cboUsuario.DataTextField = "Nombre";
            cboUsuario.DataValueField = "id";
            cboUsuario.DataSource = ObjUsuario.fun_ListarUsuario_bajas();
            cboUsuario.DataBind();

            ddlMotivoBaja.DataTextField = "Descripcion";
            ddlMotivoBaja.DataValueField = "IdMotivo";
            ddlMotivoBaja.DataSource = objM.ListarMotivoBajaActivos();
            ddlMotivoBaja.DataBind();
            //ddlCategoria.Items.Insert(0, " ----------------- Seleccione ----------------- ");

            ddlDisposicion.DataTextField = "Descripcion";
            ddlDisposicion.DataValueField = "IdDisposicion";
            ddlDisposicion.DataSource = objD.ListarDisposicionBajaActivos();
            ddlDisposicion.DataBind();
            //ddlCategoria.Items.Insert(0, " ----------------- Seleccione ----------------- ");

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BajaActivosCabeceraEn objBajaCabecera = new BajaActivosCabeceraEn();
            LogisticaEn objLogistica = new LogisticaEn();
            //SedeEn objSede = new SedeEn();
            //SedeEn objSede2 = new SedeEn();

            if (txtIdBaja.Text == "")
                objBajaCabecera.IdBajaCabecera = 0;
            else
                objBajaCabecera.IdBajaCabecera = int.Parse(txtIdBaja.Text);

            objBajaCabecera.IdMotivo = int.Parse(ddlMotivoBaja.SelectedValue);
            objBajaCabecera.IdDisposicion = int.Parse(ddlDisposicion.SelectedValue);
            objBajaCabecera.usuario = int.Parse(cboUsuario.SelectedValue);
            objLogistica.Codigo = (txtCodigo.Text);
            objLogistica.Serie = (txtSerie.Text);
            objLogistica.Marca = (txtMarca.Text);
            objLogistica.Modelo = (txtModelo.Text);
            objLogistica.Descripcion = "";


            DateTime vfechaini = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime vfechafin = Convert.ToDateTime(txtFechaHasta.Text);

            BajaActivosN obj = new BajaActivosN();

            gvBajaActivos.DataSource = obj.ReporteBajaCabecera(objLogistica, objBajaCabecera, vfechaini, vfechafin);
            gvBajaActivos.DataBind();
        }


        protected void gvBajaActivos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        //protected void gvBajaActivos_SelectedIndexChanging1(object sender, GridViewSelectEventArgs e)
        //{
        //    GridViewRow row = gvBajaActivos.Rows[e.NewSelectedIndex];

        //    int id = Convert.ToInt32(gvBajaActivos.DataKeys[e.NewSelectedIndex].Values[0]);
        //    Session["xidBajaCab"] = id;
        //    //int act = Convert.ToInt32(gvBajaActivos.DataKeys[e.NewSelectedIndex].Values[1]);
        //    //Session["xActivos"] = act;
        //    Response.Write("<script>window.open('DetalleBajaActivos.aspx','popup','width=1300,height=400,top=200,left=200,addressbar=0') </script>");

        //}

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvBajaActivos.Rows.Count == 0)
                {
                    Response.Write("<script language ='JavaScript'>window.alert('No existen datos a exportar');</script>");
                }
             
                    //StringBuilder sb = new StringBuilder();
                    //StringWriter sw = new StringWriter(sb);
                    //HtmlTextWriter htw = new HtmlTextWriter(sw);
                    //Page pagina = new Page();
                    //HtmlForm form = new HtmlForm();
                    //pagina.EnableEventValidation = false;
                    //pagina.DesignerInitialize();
                    //pagina.Controls.Add(form);
                    //form.InnerHtml = "<span style='font-weight: bold; text-align: center; font-sise: 18px;'>REPORTE DE BAJAS" + "<br>del: " + txtFechaDesde.Text + "  hasta: " + txtFechaHasta.Text + "</span>";
                    //form.Controls.Add(gvBajaActivos);
                    //pagina.RenderControl(htw);
                    //Response.Clear();
                    //Response.Buffer = true;
                    //Response.ContentType = "application/vnd.ms-excel";
                    //Response.AddHeader("Content-Disposition", "attachment;filename=Reporte" + "_BajaActivos.xls");
                    //Response.Charset = "UTF-8";
                    //Response.ContentEncoding = Encoding.Default;
                    //Response.Write(sb.ToString());
                    //Response.End();

                string strBody = "<html><body>";
                strBody += "<table style='width: 1300px;'>";
                strBody += "<tr>";
                strBody += "<td style='width: 150px; padding: 5px;'>";
                //strBody += "<img src='https://sistemas.sise.com.pe/sistemas/img/logo_rojo.png' />";
                strBody += "</td>";
                strBody += "<td style='padding: 5px;' colspan='12'>";
                strBody += "<h3 style='text-align: center; margin: 0px;'>REPORTE BAJA DE ACTIVOS</h3>";
                strBody += "</td>";
                strBody += "</tr>";
                strBody += "</table>";

                strBody += "<br>";
                strBody += "<p>Fecha: " + DateTime.Now + "</p>";

                strBody += "<table border='1'>";
                strBody += "<tr style='background: #CCCCCC; font-weight: bold; text-align: center;'>";
                strBody += "<td>NRO</td>";
                strBody += "<td>USUARIO</td>";
                strBody += "<td>FEC DOC</td>";
                strBody += "<td>FEC BAJA</td>";
                strBody += "<td>FEC REGISTRO</td>";
                strBody += "<td>MOTIVO</td>";
                strBody += "<td>DISPOSICI&Oacute;N</td>";
                strBody += "<td>C&Oacute;DIGO</td>";
                strBody += "<td>DESCRIPCI&Oacute;N</td>";
                strBody += "<td>SERIE</td>";
                strBody += "<td>MARCA</td>";
                strBody += "<td>MODELO</td>";
                strBody += "<td>SEDE</td>";
                strBody += "</tr>";

                foreach (GridViewRow row in gvBajaActivos.Rows)
                {
                    strBody += "<tr style='text-align: center; height: 25px; vertical-align: middle;'>";
                    strBody += "<td>" + row.Cells[0].Text + "</td>";
                    strBody += "<td>" + row.Cells[1].Text + "</td>";
                    strBody += "<td>" + row.Cells[2].Text + "</td>";
                    strBody += "<td>" + row.Cells[3].Text + "</td>";
                    strBody += "<td>" + row.Cells[4].Text + "</td>";
                    strBody += "<td>" + row.Cells[5].Text + "</td>";
                    strBody += "<td>" + row.Cells[6].Text + "</td>";
                    strBody += "<td>" + row.Cells[7].Text + "</td>";
                    strBody += "<td>" + row.Cells[8].Text + "</td>";
                    strBody += "<td>" + row.Cells[9].Text + "</td>";
                    strBody += "<td>" + row.Cells[10].Text + "</td>";
                    strBody += "<td>" + row.Cells[11].Text + "</td>";
                    strBody += "<td>" + row.Cells[12].Text + "</td>";
                    strBody += "</tr>";
                }

                strBody += "</table>";
                strBody += "</body></html>";

                string fileName = "Reporte_baja_activos.xls";
                Response.AppendHeader("Content-Type", "application/xls");
                Response.AppendHeader("Content-disposition", "attachment; filename=" + fileName);
                Response.Write(strBody);
                Response.End();

            }
            catch (Exception ex)
            {
                Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }
    }
}