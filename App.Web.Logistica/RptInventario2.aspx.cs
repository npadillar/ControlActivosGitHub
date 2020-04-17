using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;
// ADICIONAR LIBRERIAS PDF
using System.ComponentModel;
using System.Drawing;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;

namespace App.Web.Logistica.Reportes
{
    public partial class RptInventario2 : System.Web.UI.Page
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
                        txtFechaHasta.Enabled = true;

                        MenuN objMenu = new MenuN();
                        if (objMenu.fun_MostrarControl_xCargo(8, Convert.ToInt16(Session["cargo"]))) btnExportarExcel.Visible = true;
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
            CategoriaN objN = new CategoriaN();
            SedeN ObjSede = new SedeN();
            CondicionN ObjCond = new CondicionN();
            UsuarioN ObjUsuario = new UsuarioN();

            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataSource = objN.ListarCategoria();
            ddlCategoria.DataBind();
            //ddlCategoria.Items.Insert(0, " ----------------- Seleccione ----------------- ");
            //Listar Sede
            ddlSede.DataTextField = "Descripcion";
            ddlSede.DataValueField = "IdSede";
            ddlSede.DataSource = ObjSede.ListarSede("partida");
            ddlSede.DataBind();
            //ddlSede.Items.Insert(0, " ----------------- Seleccione ----------------- ");
            //Listar Condición
            ddlCondicion.DataTextField = "Descripcion";
            ddlCondicion.DataValueField = "IdCondicion";
            ddlCondicion.DataSource = ObjCond.ListarCondicion();
            ddlCondicion.DataBind();

            ddlUsuario.DataTextField = "Nombre";
            ddlUsuario.DataValueField = "IdLogin";
            ddlUsuario.DataSource = ObjUsuario.ListarUsuario();
            ddlUsuario.DataBind();


            DateTime fecha = DateTime.Now;
            txtFechaDesde.Text = fecha.AddDays(-30).ToString("2017-01-31");
            DateTime fecha2 = DateTime.Now;
            txtFechaHasta.Text = (fecha2.ToString("yyyy-MM-dd"));
            //txtFechaDesde.Text = DateTime.Now.AddDays(-20).ToShortDateString();  
            //txtFechaHasta.Text = DateTime.Now.ToShortDateString();

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LogisticaEn objLog = new LogisticaEn();
            LoginEn ObjLogin = new LoginEn();

            objLog.Codigo = txtCodigo.Text.Trim();
            objLog.codAnterior = txtCodAnterior.Text.Trim();
            objLog.Descripcion = txtDescripcion.Text;
            ObjLogin.IdLogin = int.Parse(ddlUsuario.SelectedValue);
            objLog.IdSede = int.Parse(ddlSede.SelectedValue);
            objLog.Edificio = txtEdificio.Text;
            objLog.IdCategoria = int.Parse(ddlCategoria.SelectedValue);

            DateTime vfechaini = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime vfechafin = Convert.ToDateTime(txtFechaHasta.Text);
            int vCondicion = int.Parse(ddlCondicion.SelectedValue);
            objLog.Aula = txtAula.Text;
            objLog.Area = txtArea.Text;
            string xRuc = txtRuc.Text;
            string xNroFactura = txtNroFactura.Text;

            ReportesN obj = new ReportesN();

            gdvDatos.Columns[5].Visible = true; // columna código anterior
            gdvDatos.DataSource = obj.ListarReporte(objLog, vfechaini, vfechafin, vCondicion, xRuc, xNroFactura, ObjLogin);
            gdvDatos.DataBind();

            // columna código anterior
            if (!ckbMostrar.Checked) gdvDatos.Columns[5].Visible = false;

            lblRegistros.Text = gdvDatos.Rows.Count.ToString();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            string nomfile = "REPORTE_LOGISTICA_" + DateTime.Now.ToShortDateString() + ".pdf";

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + nomfile);

            //Document doc = new Document(PageSize.A4, 0.0F, 0.0F, 20.0F, 0.0F);
            Document doc = new Document(PageSize.A4.Rotate(), 10, 20, 20, 10);
            PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();


            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            iTextSharp.text.Font Negrita = new iTextSharp.text.Font(bfTimes, 18, iTextSharp.text.Font.BOLD, BaseColor.BLACK);


            iTextSharp.text.Font Negrita2 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font Formato = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            //iTextSharp.text.Font font = new iTextSharp.text.Font(BaseFont.TIMES_ROMAN, 6, font.BaseFont );
            BaseColor colorh = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#A9D0F5"));

            // Escribimos el encabezamiento en el documento
            doc.Add(Chunk.NEWLINE);

            PdfPTable tblcabecera_1 = new PdfPTable(1);
            //imagen

            Paragraph paragraph = new Paragraph("Getting Started ITextSharp.");
            string imageURL = Server.MapPath(".") + "/Iconos/logo.jpg";
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(imageURL);


            //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"C:\logistica\Sis.Logistica\Iconos\logo.jpg");
            imagen.ScalePercent(70f);
            imagen.Alignment = Element.ALIGN_LEFT;
            doc.Add(imagen);
            ////titulo
            Paragraph Titulo = new Paragraph("Reporte de Activos", Negrita);
            Titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(Titulo);
            ////Feha
            DateTime fec = Convert.ToDateTime(DateTime.Now.ToString());
            Paragraph Fecha = new Paragraph("Fecha:" + " " + fec, Negrita2);
            Titulo.Alignment = Element.ALIGN_CENTER;

            doc.Add(Fecha);

            doc.Add(Chunk.NEWLINE);

            ///tabla
            PdfPTable tblTabla = new PdfPTable(20);
            tblTabla.WidthPercentage = 100;
            //dar tamaño a la celda

            float[] medidaCeldas = { 2.4f, 4.5f, 4.8f, 6.8f, 7.5f, 4.2f, 4.8f, 3f, 4.5f, 3f, 3.6f, 3f, 4f, 4.5f, 5.8f, 5f, 4f, 5.2f, 4.2f, 4.2f };
            tblTabla.SetWidths(medidaCeldas);

            //// Configuramos el título de las columnas de la tabla


            //FILA1
            PdfPCell clNro = new PdfPCell(new Phrase("Nro", Negrita2));
            clNro.BorderWidth = 1;
            clNro.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clNro);

            PdfPCell clUsuario = new PdfPCell(new Phrase("Usuario", Negrita2));
            clUsuario.BorderWidth = 1;
            clUsuario.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clUsuario);

            PdfPCell clFecDoc = new PdfPCell(new Phrase("Fecha Doc", Negrita2));
            clFecDoc.BorderWidth = 1;
            clFecDoc.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clFecDoc);

            PdfPCell clCodBarras = new PdfPCell(new Phrase("Codigo Barras", Negrita2));
            clCodBarras.BorderWidth = 1;
            clCodBarras.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clCodBarras);

            PdfPCell clDesc = new PdfPCell(new Phrase("Descripción", Negrita2));
            clDesc.HorizontalAlignment = Element.ALIGN_CENTER;
            clDesc.BorderWidth = 1;
            tblTabla.AddCell(clDesc);

            PdfPCell clCategoria = new PdfPCell(new Phrase("Categoría", Negrita2));
            clCategoria.BorderWidth = 1;
            clCategoria.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clCategoria);
            
            PdfPCell clUsuAsig = new PdfPCell(new Phrase("Usuario Asignado", Negrita2));
            clUsuAsig.BorderWidth = 1;
            clUsuAsig.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clUsuAsig);
            
            PdfPCell clSede = new PdfPCell(new Phrase("Sede", Negrita2));
            clSede.BorderWidth = 1;
            clSede.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clSede);
            
            PdfPCell clEdificio = new PdfPCell(new Phrase("Edificio", Negrita2));
            clEdificio.BorderWidth = 1;
            clEdificio.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clEdificio);

            PdfPCell clPiso = new PdfPCell(new Phrase("Piso", Negrita2));
            clPiso.BorderWidth = 1;
            clPiso.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clPiso);

            PdfPCell clArea = new PdfPCell(new Phrase("Área", Negrita2));
            clArea.BorderWidth = 1;
            clArea.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clArea);

            PdfPCell clAula = new PdfPCell(new Phrase("Aula", Negrita2));
            clAula.BorderWidth = 1;
            clAula.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clAula);

            PdfPCell clMarca = new PdfPCell(new Phrase("Marca", Negrita2));
            clMarca.BorderWidth = 1;
            clMarca.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clMarca);
            
            PdfPCell clModelo = new PdfPCell(new Phrase("Modelo", Negrita2));
            clModelo.BorderWidth = 1;
            clModelo.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clModelo);

            PdfPCell clSerie = new PdfPCell(new Phrase("Serie", Negrita2));
            clSerie.BorderWidth = 1;
            clSerie.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clSerie);

            PdfPCell clCondicion = new PdfPCell(new Phrase("Condición", Negrita2));
            clCondicion.BorderWidth = 1;
            clCondicion.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clCondicion);

            PdfPCell clFecReg = new PdfPCell(new Phrase("Fecha Reg", Negrita2));
            clFecReg.BorderWidth = 1;
            clFecReg.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clFecReg);

            PdfPCell clFecCompra = new PdfPCell(new Phrase("Fecha Compra", Negrita2));
            clFecCompra.BorderWidth = 1;
            clFecCompra.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clFecCompra);

            PdfPCell clRuc = new PdfPCell(new Phrase("Ruc", Negrita2));
            clRuc.BorderWidth = 1;
            clRuc.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clRuc);

            PdfPCell clNroFact = new PdfPCell(new Phrase("Nro Factura", Negrita2));
            clNroFact.BorderWidth = 1;
            clNroFact.HorizontalAlignment = Element.ALIGN_CENTER;
            tblTabla.AddCell(clNroFact);


            for (int i = 0; i < gdvDatos.Rows.Count; i++)
            {
                clNro = new PdfPCell(new Phrase(Convert.ToString(i + 1), Formato));
                clNro.HorizontalAlignment = Element.ALIGN_CENTER;
                clNro.BorderWidth = 1;
                tblTabla.AddCell(clNro);

                clUsuario = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[2].Text, Formato));
                clUsuario.BorderWidth = 1;
                tblTabla.AddCell(clUsuario);

                clFecDoc = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[3].Text, Formato));
                clFecDoc.BorderWidth = 1;
                tblTabla.AddCell(clFecDoc);

                clCodBarras = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[4].Text, Formato));
                clCodBarras.BorderWidth = 1;
                tblTabla.AddCell(clCodBarras);

                clDesc = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[6].Text, Formato));
                clDesc.BorderWidth = 1;
                tblTabla.AddCell(clDesc);

                clCategoria = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[7].Text, Formato));
                clCategoria.BorderWidth = 1;
                tblTabla.AddCell(clCategoria);

                clUsuAsig = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[8].Text, Formato));
                clUsuAsig.BorderWidth = 1;
                tblTabla.AddCell(clUsuAsig);

                clSede = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[9].Text, Formato));
                clSede.BorderWidth = 1;
                tblTabla.AddCell(clSede);

                clEdificio = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[10].Text, Formato));
                clEdificio.BorderWidth = 1;
                tblTabla.AddCell(clEdificio);

                clPiso = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[11].Text, Formato));
                clPiso.BorderWidth = 1;
                tblTabla.AddCell(clPiso);

                //validar campos vacios
                if (gdvDatos.Rows[i].Cells[12].Text.Equals("&nbsp;"))
                    clArea = new PdfPCell(new Phrase("", Formato));
                else
                    clArea = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[12].Text, Formato));
                clArea.BorderWidth = 1;
                tblTabla.AddCell(clArea);


                if (gdvDatos.Rows[i].Cells[13].Text.Equals("&nbsp;"))
                    clAula = new PdfPCell(new Phrase("", Formato));
                else
                    clAula = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[13].Text, Formato));
                clAula.BorderWidth = 1;
                tblTabla.AddCell(clAula);

                
                if (gdvDatos.Rows[i].Cells[14].Text.Equals("&nbsp;"))
                    clMarca = new PdfPCell(new Phrase("", Formato));
                else
                    clMarca = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[14].Text, Formato));
                clMarca.BorderWidth = 1;
                tblTabla.AddCell(clMarca);
                                
                
                if (gdvDatos.Rows[i].Cells[15].Text.Equals("&nbsp;"))
                    clModelo = new PdfPCell(new Phrase("", Formato));
                else
                    clModelo = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[15].Text, Formato));
                clModelo.BorderWidth = 1;
                tblTabla.AddCell(clModelo);


                if (gdvDatos.Rows[i].Cells[16].Text.Equals("&nbsp;"))
                    clSerie = new PdfPCell(new Phrase("", Formato));
                else
                    clSerie = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[16].Text, Formato));
                clSerie.BorderWidth = 1;
                tblTabla.AddCell(clSerie);                
                                
                
                if (gdvDatos.Rows[i].Cells[17].Text.Equals("&nbsp;"))
                    clCondicion = new PdfPCell(new Phrase("", Formato));
                else
                    clCondicion = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[17].Text, Formato));
                clCondicion.BorderWidth = 1;
                tblTabla.AddCell(clCondicion);
                                
                
                if (gdvDatos.Rows[i].Cells[18].Text.Equals("&nbsp;"))
                    clFecReg = new PdfPCell(new Phrase("", Formato));
                else
                    clFecReg = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[18].Text, Formato));
                clFecReg.BorderWidth = 1;
                tblTabla.AddCell(clFecReg);
                                
                
                if (gdvDatos.Rows[i].Cells[19].Text.Equals("&nbsp;"))
                    clFecCompra = new PdfPCell(new Phrase("", Formato));
                else
                    clFecCompra = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[19].Text, Formato));
                clFecCompra.BorderWidth = 1;
                tblTabla.AddCell(clFecCompra);
                                
                
                if (gdvDatos.Rows[i].Cells[20].Text.Equals("&nbsp;"))
                    clRuc = new PdfPCell(new Phrase("", Formato));
                else
                    clRuc = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[20].Text, Formato));
                clRuc.BorderWidth = 1;
                tblTabla.AddCell(clRuc);


                if (gdvDatos.Rows[i].Cells[21].Text.Equals("&nbsp;"))
                    clNroFact = new PdfPCell(new Phrase("", Formato));
                else
                    clNroFact = new PdfPCell(new Phrase(gdvDatos.Rows[i].Cells[21].Text, Formato));
                clNroFact.BorderWidth = 1;
                tblTabla.AddCell(clNroFact);
            }

            doc.Add(tblTabla);

            //Process.Start("C:/compartido/Logistica.pdf");
            doc.Close();
            //writer.Close();

        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (gdvDatos.Rows.Count == 0)
                {
                    throw new Exception("No existen datos a exportar.");    
                }

                string strBody = "<html><body>";
                strBody += "<table style='width: 1300px;'>";
                strBody += "<tr>";
                strBody += "<td style='width: 150px; padding: 5px;'>";
                strBody += "<img src='https://sistemas.sise.com.pe/sistemas/img/logo_rojo.png' />";
                strBody += "</td>";
                strBody += "<td style='padding: 5px;' colspan='21'>";
                strBody += "<h2 style='text-align: center; margin: 0px;'>REPORTE DE ACTIVOS</h3>";
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
                strBody += "<td>C&Oacute;DIGO</td>";
                strBody += "<td>COD ANTERIOR</td>";
                strBody += "<td>DESCRIPCI&Oacute;N</td>";
                strBody += "<td>CATEGOR&Iacute;A</td>";
                strBody += "<td>USU ASIGNADO</td>";
                strBody += "<td>SEDE</td>";
                strBody += "<td>EDIFICIO</td>";
                strBody += "<td>PISO</td>";
                strBody += "<td>&Aacute;REA</td>";
                strBody += "<td>AULA</td>";
                strBody += "<td>MARCA</td>";
                strBody += "<td>MODELO</td>";
                strBody += "<td>SERIE</td>";
                strBody += "<td>COND.</td>";
                strBody += "<td>FEC REG</td>";
                strBody += "<td>FEC COMPRA</td>";
                strBody += "<td>RUC</td>";
                strBody += "<td>NRO FACT</td>";
                strBody += "<td>OBSERV</td>";
                strBody += "</tr>";

                int i = 1;
                foreach (GridViewRow row in gdvDatos.Rows)
                {
                    strBody += "<tr style='text-align: center; vertical-align: middle;'>";
                    strBody += "<td>" + i + "</td>";
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
                    strBody += "<td>" + row.Cells[13].Text + "</td>";
                    strBody += "<td>" + row.Cells[14].Text + "</td>";
                    strBody += "<td>" + row.Cells[15].Text + "</td>";
                    strBody += "<td>" + row.Cells[16].Text + "</td>";
                    strBody += "<td>" + row.Cells[17].Text + "</td>";
                    strBody += "<td>" + row.Cells[18].Text + "</td>";
                    strBody += "<td>" + row.Cells[19].Text + "</td>";
                    strBody += "<td>" + row.Cells[20].Text + "</td>";
                    strBody += "<td>" + row.Cells[21].Text + "</td>";
                    strBody += "<td>" + row.Cells[22].Text + "</td>";
                    strBody += "</tr>";
                    i += 1;
                }
                
                strBody += "</table>";
                strBody += "</body></html>";

                string fileName = "Reporte_activos.xls";
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