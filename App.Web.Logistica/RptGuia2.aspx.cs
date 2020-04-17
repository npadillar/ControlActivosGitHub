using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;
using System.Data;

namespace App.Web.Logistica.Reportes
{
    public partial class RptGuia2 : System.Web.UI.Page
    {
        public DataTable dtDatos { get; set; }
        funcionesN objFun = new funcionesN();

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
                        columnasGrid();

                        MenuN objMenu = new MenuN();
                        if (objMenu.fun_MostrarControl_xCargo(8, Convert.ToInt16(Session["cargo"]))) btnExportar.Visible = true;
                    }
                    else
                    {
                        dtDatos = (DataTable)ViewState["dtDatos"];
                    }
                } 
            }
            catch (Exception ex)
            {
                Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }


        private void columnasGrid()
        {
            dtDatos = new DataTable();
            dtDatos.Columns.Clear();
            dtDatos.Rows.Clear();

            dtDatos.Columns.Add("idGuia");
            dtDatos.Columns.Add("Usuario");
            dtDatos.Columns.Add("Fecha_Envio");
            dtDatos.Columns.Add("Transportista");
            dtDatos.Columns.Add("Punto_Partida");
            dtDatos.Columns.Add("Punto_Llegada");
            dtDatos.Columns.Add("Motivo_Traslado");
            dtDatos.Columns.Add("Activos");
            dtDatos.Columns.Add("Usuario_Recepciona");
            dtDatos.Columns.Add("Usuario_Anulacion");
            dtDatos.Columns.Add("Fecha_Recepcion");
            dtDatos.Columns.Add("Fecha_Anulacion");
            dtDatos.Columns.Add("Estado");
            dtDatos.Columns.Add("detalle");
            ViewState.Add("dtDatos", dtDatos);
        }

        private void CargarDatos()
        {
            MotivoTrasladoN objM = new MotivoTrasladoN();
            SedeN ObjSede = new SedeN();
            EstadoN objEstado = new EstadoN();
            //CondicionN ObjCond = new CondicionN();

            ddlTraslado.DataTextField = "Descripcion";
            ddlTraslado.DataValueField = "IdMotivoTraslado";
            ddlTraslado.DataSource = objM.ListarMotivoTraslado();
            ddlTraslado.DataBind();
            //ddlCategoria.Items.Insert(0, " ----------------- Seleccione ----------------- ");
            //Listar Sede
            ddlSedePatida.DataTextField = "Descripcion";
            ddlSedePatida.DataValueField = "IdSede";
            ddlSedePatida.DataSource = ObjSede.ListarSede("partida");
            ddlSedePatida.DataBind();
            //ddlSede.Items.Insert(0, " ----------------- Seleccione ----------------- ");
            ddlSedeLlegada.DataTextField = "Descripcion";
            ddlSedeLlegada.DataValueField = "IdSede";
            ddlSedeLlegada.DataSource = ObjSede.ListarSede("mantenimiento");
            ddlSedeLlegada.DataBind();
            //Listar Estado
            //ddlSede.Items.Insert(0, " ----------------- Seleccione ----------------- ");
            ddlEstado.DataTextField = "Descripcion";
            ddlEstado.DataValueField = "IdEstado";
            ddlEstado.DataSource = objEstado.ListarEstado();
            ddlEstado.DataBind();
            //Listar Condición

            DateTime fecha = DateTime.Now;
            txtFechaDesde.Text = fecha.AddDays(-30).ToString("yyyy-MM-dd");
            DateTime fecha2 = DateTime.Now;
            txtFechaHasta.Text = (fecha2.ToString("yyyy-MM-dd"));
            //txtFechaDesde.Text = DateTime.Now.AddDays(-20).ToShortDateString();
            //txtFechaHasta.Text = DateTime.Now.ToShortDateString();

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            GuiaCabeceraEn objGuia = new GuiaCabeceraEn();
            GuiaCabeceraEn objGuia2 = new GuiaCabeceraEn();
            LogisticaEn objCodigo = new LogisticaEn();
            //GuiaCabeceraEn IdGuia = new GuiaCabeceraEn();
            SedeEn objSede = new SedeEn();
            SedeEn objSede2 = new SedeEn();
            objGuia.IdMotivoTraslado = int.Parse(ddlTraslado.SelectedValue);
            objGuia.Activos = int.Parse(cboTipoGuia.SelectedValue);
            objSede.IdSede = int.Parse(ddlSedePatida.SelectedValue);
            objGuia2.IdEstado = int.Parse(ddlEstado.SelectedValue);

            //IdGuia.IdGuia = int.Parse(txtNumeroGuia.Text);
            objSede2.IdSede = int.Parse(ddlSedeLlegada.SelectedValue);
            DateTime vfechaini = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime vfechafin = Convert.ToDateTime(txtFechaHasta.Text);
            string vGuia = txtNumeroGuia.Text;
            objCodigo.Codigo = txtCodigo.Text;
            ReportesN obj = new ReportesN();

            DataTable dtGuia = new DataTable();
            dtGuia = obj.ReporteGuia(objGuia, objSede, objSede2, vfechaini, vfechafin, objGuia2, vGuia, objCodigo);

            dtDatos.Rows.Clear();
            string detalle = "";

            foreach (DataRow row in dtGuia.Rows)
            {
                detalle = obj.fun_GuiaDetalle_mostrarUno(Convert.ToInt32(row["idGuia"]), Convert.ToInt16(row["Activos"]));
                detalle = objFun.fun_RemplazarLetras(detalle);

                dtDatos.Rows.Add(row["idGuia"],
                                 row["Usuario"],
                                 row["Fecha_Envio"],
                                 row["Transportista"],
                                 row["Punto_Partida"],
                                 row["Punto_Llegada"],
                                 row["Motivo_Traslado"],
                                 row["Activos"],
                                 row["Usuario_Recepciona"],
                                 row["Usuario_Anulacion"],
                                 row["Fecha_Recepcion"],
                                 row["Fecha_Anulacion"],
                                 row["estado"],
                                 detalle);
            }

            ViewState.Add("dtDatos", dtDatos);
            gvGuia.DataSource = dtDatos;
            gvGuia.DataBind();
        }


        protected void gvGuia_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string id = gvGuia.DataKeys[e.NewSelectedIndex].Values[0].ToString();
            string act = gvGuia.DataKeys[e.NewSelectedIndex].Values[1].ToString();

            string script = "<script>window.open('DetalleGuia.aspx?c=" + id + "&a=" + act + "','popup','width=1300,height=500,top=200,left=200,addressbar=0') </script>";

            ScriptManager.RegisterStartupScript(this, typeof(Page), "popup", script, false);
        }

        protected void ddlSedeLlegada_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtAnular_Click(object sender, EventArgs e)
        {

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvGuia.Rows.Count == 0)
                {
                    throw new Exception("No existen datos a exportar.");
                }

                string strBody = "<html><body>";
                strBody += "<table style='width: 1000px;'>";
                strBody += "<tr>";
                strBody += "<td style='width: 150px; padding: 5px;'>";
                strBody += "<img src='https://sistemas.sise.com.pe/sistemas/img/logo_rojo.png' />";
                strBody += "</td>";
                strBody += "<td style='padding: 5px;' colspan='11'>";
                strBody += "<h2 style='text-align: center; margin: 0px;'>REPORTE DE GUIAS ACTIVOS - BIENES</h3>";
                strBody += "</td>";
                strBody += "</tr>";
                strBody += "</table>";

                strBody += "<br>";
                strBody += "<p>Fecha: " + DateTime.Now + "</p>";

                strBody += "<table border='1'>";
                strBody += "<tr style='background: #CCCCCC; font-weight: bold; text-align: center;'>";
                strBody += "<td>NRO GU&Iacute;A</td>";
                strBody += "<td>DETALLE</td>";
                strBody += "<td>FECHA</td>";
                strBody += "<td>USU EMISOR</td>";
                strBody += "<td>TRANSPORTISTA</td>";
                strBody += "<td>PUNTO PARTIDA</td>";
                strBody += "<td>PUNTO LLEGADA</td>";
                strBody += "<td>MOTIVO</td>";
                strBody += "<td>USU RECEPCI&Oacute;N</td>";
                strBody += "<td>FEC RECEPCI&Oacute;N</td>";
                strBody += "<td>ESTADO</td>";
                strBody += "<td>USU ANULACI&Oacute;N</td>";
                strBody += "<td>FEC ANULACI&Oacute;N</td>";
                strBody += "</tr>";

                foreach (GridViewRow row in gvGuia.Rows)
                {
                    strBody += "<tr style='text-align: center; vertical-align: middle;'>";
                    strBody += "<td>" + row.Cells[1].Text + "</td>";
                    strBody += "<td>" + row.Cells[2].Text + "</td>";
                    strBody += "<td>" + row.Cells[3].Text + "</td>";
                    strBody += "<td>" + row.Cells[4].Text + "</td>";
                    strBody += "<td>" + row.Cells[5].Text + "</td>";
                    strBody += "<td>" + row.Cells[6].Text + "</td>";
                    strBody += "<td>" + row.Cells[7].Text + "</td>";
                    strBody += "<td>" + row.Cells[9].Text + "</td>";
                    strBody += "<td>" + row.Cells[10].Text + "</td>";
                    strBody += "<td>" + row.Cells[11].Text + "</td>";
                    strBody += "<td>" + row.Cells[12].Text + "</td>";
                    strBody += "<td>" + row.Cells[13].Text + "</td>";
                    strBody += "<td>" + row.Cells[14].Text + "</td>";
                    strBody += "</tr>";
                }

                strBody += "</table>";
                strBody += "</body></html>";

                string fileName = "Reporte_Guias.xls";
                Response.AppendHeader("Content-Type", "application/xls");
                Response.AppendHeader("Content-disposition", "attachment; filename=" + fileName);
                Response.Write(strBody);
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}