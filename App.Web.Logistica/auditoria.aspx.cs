using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logistica.Libreria.Negocio;
using est = Logistica.Libreria.Entidad.AuditoriaEst;
using OfficeOpenXml;
using System.IO;
using System.Data;

namespace App.Web.Logistica
{
    public partial class auditoria1 : System.Web.UI.Page
    {
        AuditoriaN objAudi = new AuditoriaN();

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

                        txtFechaDesde.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                        txtFechaHasta.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        llenarCombos();
                    }
                    lblError.Text = "";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }

        private void llenarCombos()
        {
            UsuarioN ObjUsuario = new UsuarioN();

            //cboModulo.DataSource = objAudi.fun_listar_paginas();
            //cboModulo.DataTextField = "pagina";
            //cboModulo.DataValueField = "pagina";
            //cboModulo.DataBind();

            cboUsuario.DataTextField = "nombre";
            cboUsuario.DataValueField = "id";
            cboUsuario.DataSource = ObjUsuario.fun_ListarUsuario_Auditoria();
            cboUsuario.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                est.EST_AUDITORIA audi = new est.EST_AUDITORIA();

                audi.fecIni = DateTime.Parse(txtFechaDesde.Text);
                audi.fecFin = DateTime.Parse(txtFechaHasta.Text);
                audi.accion = cboAccion.SelectedValue;
                audi.idUsuario = int.Parse(cboUsuario.SelectedValue);
                audi.pagina = cboModulo.SelectedValue;
                audi.codigo = txtCodigo.Value;
                audi.idTabla = 0;
                if (txtIdTabla.Text.Trim() != "") audi.idTabla = int.Parse(txtIdTabla.Text.Trim());
                                    
                dgvDatos.DataSource = objAudi.fun_listar_auditoria(audi);
                dgvDatos.DataBind();
                lblRegistros.Text = dgvDatos.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }


        private void Demo() {
            
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.Rows.Count == 0) throw new Exception("No existen datos a exportar.");

                string strBody = "<html><body>";
                strBody += "<table style='width: 1000px;'>";
                strBody += "<tr>";
                strBody += "<td style='width: 150px; padding: 5px;'>";
                strBody += "<img src='https://sistemas.sise.com.pe/sistemas/img/logo_rojo.png' />";
                strBody += "</td>";
                strBody += "<td style='padding: 5px;' colspan='12'>";
                strBody += "<h2 style='text-align: center; margin: 0px;'>REPORTE DE AUDITOR&Iacute;A</h3>";
                strBody += "</td>";
                strBody += "</tr>";
                strBody += "</table>";

                strBody += "<br>";
                strBody += "<p>Fecha: " + DateTime.Now + "</p>";

                strBody += "<table border='1'>";
                strBody += "<tr style='background: #CCCCCC; font-weight: bold; text-align: center;'>";
                strBody += "<td>ID</td>";
                strBody += "<td>C&Oacute;DIGO</td>";
                strBody += "<td>ACCI&Oacute;N</td>";
                strBody += "<td>P&Aacute;GINA</td>";
                strBody += "<td>CAMPO</td>";
                strBody += "<td>VALOR ANTERIOR</td>";
                strBody += "<td>VALOR NUEVO</td>";
                strBody += "<td>USUARIO</td>";
                strBody += "<td>FECHA</td>";
                strBody += "<td>HORA</td>";
                strBody += "<td>IP</td>";
                strBody += "</tr>";

                foreach (GridViewRow row in dgvDatos.Rows)
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
                    strBody += "</tr>";
                }

                strBody += "</table>";
                strBody += "</body></html>";

                string fileName = "Reporte_auditoria.xls";
                Response.AppendHeader("Content-Type", "application/xls");
                Response.AppendHeader("Content-disposition", "attachment; filename=" + fileName);
                Response.Write(strBody);
                Response.End();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}