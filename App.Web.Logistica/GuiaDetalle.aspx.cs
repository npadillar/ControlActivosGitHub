using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;
// ADICIONAR LIBRERIAS PDF
using System.ComponentModel;
using System.Drawing;

//using System.Drawing.Image;

using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Windows.Forms;

namespace App.Web.Logistica
{
    public partial class GuiaDetalle : System.Web.UI.Page
    {
        public int xidGuia { get; set; }
        GuiaCabeceraN objGuiaCabecera = new GuiaCabeceraN();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    xidGuia = int.Parse(Request.QueryString["c"]);
                    int xActivos = int.Parse(Request.QueryString["a"]);
                    ViewState.Add("xidGuia", xidGuia);

                    lblNroGuia.Text = xidGuia.ToString();

                    string usuario = Session["Usuario"].ToString().ToUpper();
                    string idlogin = Session["rpta"].ToString().ToUpper();
                    txtUsuario.Text = usuario;
                    txtFecha.Text = DateTime.Now.ToString();

                    ReportesN objGuiaDetalle = new ReportesN();
                    DataTable DTDetalleGuia = objGuiaDetalle.ReporteGuiaDetalle(xidGuia, xActivos);
                    gvDetalleGuia.DataSource = DTDetalleGuia;
                    gvDetalleGuia.DataBind();

                    MenuN objMenu = new MenuN();
                    if (objMenu.fun_MostrarControl_xCargo(8, Convert.ToInt16(Session["cargo"]))) btnExportar.Visible = true;

                    lblObserv.Text = objGuiaCabecera.fun_traer_observacion(xidGuia);

                    string estado = Request.QueryString["e"].ToString();
                    string usuEmisor = Request.QueryString["ue"].ToString();
                    string usuRecepcion = Request.QueryString["ur"].ToString();

                    string motitras= Request.QueryString["mt"].ToString();

                    if (motitras == "Reparación" && usuRecepcion == "" && xActivos==1)
                    {
                        btnRecepcion.Visible = false;
                        btnReparacion.Visible = true;
                    }
                    else
                    {
                        btnRecepcion.Visible = true;
                        btnReparacion.Visible = false;
                    }

                    if (estado == "Anulada")
                    {
                        btnRecepcion.Enabled = false;
                        btnReparacion.Enabled = false;
                        btnAnular.Enabled = false;
                    }
                    else
                    {
                        if (usuRecepcion != "") btnRecepcion.Enabled = false;
                        if (usuario.ToUpper() != usuEmisor.ToUpper())
                        {
                            btnAnular.Enabled = false;
                        }
                        else
                        {
                            if (usuRecepcion != "") btnAnular.Enabled = false;
                        }
                    }
                }
                else
                {
                    xidGuia = (int)ViewState["xidGuia"];
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.close();</script>");
        }

        protected void btnRecepcion_Click(object sender, EventArgs e)
        {
                string idlogin = Session["rpta"].ToString().ToUpper();
                //actualizar guiacabecera
                GuiaCabeceraEn objGuiaEn = new GuiaCabeceraEn();
                objGuiaEn.IdGuia = xidGuia;
                objGuiaEn.IdLoginRecibido = int.Parse(idlogin);
                objGuiaEn.FechaRecepcion = DateTime.Parse(txtFecha.Text);
                objGuiaEn.ip = Request.UserHostAddress;
                string cod = objGuiaCabecera.ActualizarGuiaCabecera(objGuiaEn);
                //string  mensaje = ("<script language ='JavaScript'>window.confirm('Va a salir de la aplicación ¿Desea salir realmente?'); </script>");
                //if (mensaje == "Ok")
                //{
                //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Gracias por visitarnos'); </script>");
                //}
                //var mensaj = (this.Page.Response.Write("<script language ='JavaScript'>window.confirm('Va a salir de la aplicación ¿Desea salir realmente?'); </script>"));

                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Se registró la Recepción del Activo'); </script>");

                btnRecepcion.Enabled = false;
                btnImprimir.Enabled = true;          
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string nomfile = "GUIARECEPCION" + DateTime.Now.ToShortDateString() + ".pdf";

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
            iTextSharp.text.Font Negrita = new iTextSharp.text.Font(bfTimes, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font Negrita2 = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font Negrita10 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font Formato = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            //iTextSharp.text.Font font = new iTextSharp.text.Font(BaseFont.TIMES_ROMAN, 6, font.BaseFont );
            BaseColor colorh = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#A9D0F5"));

            // Escribimos el encabezamiento en el documento
            //     doc.Add(Chunk.NEWLINE);

            PdfPTable tblcabecera_1 = new PdfPTable(1);
            //imagen

            Paragraph paragraph = new Paragraph("Getting Started ITextSharp.");

            //////string imageURL = Server.MapPath(".") + "/logo.jpg";
            //////iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(imageURL);

            //////imagen.ScalePercent(50f);
            //////imagen.Alignment = Element.ALIGN_LEFT;
            //////doc.Add(imagen);
            //cabecera
            PdfPTable tblcabecera = new PdfPTable(3);
            tblcabecera.WidthPercentage = 100;
            float[] medidaCabecera = { 10f, 10f, 10f };
            tblcabecera.SetWidths(medidaCabecera);

            //fila1 y 2
            string imageURL = Server.MapPath(".") + "/Iconos/logo.jpg";
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
            jpg.ScalePercent(35f);
            PdfPCell imageCell = new PdfPCell(jpg);
            imageCell.Colspan = 2; // either 1 if you need to insert one cell
            imageCell.Border = 0;
            imageCell.Rowspan = 2;
            // imageCell.Padding=(10f);
            imageCell.HorizontalAlignment = (Element.ALIGN_CENTER);

            tblcabecera.AddCell(imageCell);

            // tblcabecera.AddCell(cllogo);

            PdfPCell clruc = new PdfPCell(new Phrase("R.U.C. 20504653570", Negrita));
            clruc.BorderWidth = 1;
            clruc.Rowspan = 2;
            clruc.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clruc);

            //FILA3
            PdfPCell clasoc = new PdfPCell(new Phrase("ASOCIACION CULTURAL SUDAMERICANA", Negrita10));
            clasoc.BorderWidth = 0;
            clasoc.Colspan = 2;
            clasoc.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clasoc);

            PdfPCell clguia = new PdfPCell(new Phrase("GUIA DE REMISION", Negrita));
            clguia.BorderWidth = 1;
            clguia.Rowspan = 2;
            clguia.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clguia);

            //FILA4
            PdfPCell clSede1 = new PdfPCell(new Phrase("Sede San Miguel: Av. La Marina 1429-San Miguel - Lima - Lima / Telefonos: 263-0269 / 500-4600", Formato));
            clSede1.BorderWidth = 0;
            clSede1.Colspan = 2;
            clSede1.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clSede1);


            //FILA5
            PdfPCell clSede2 = new PdfPCell(new Phrase("Sede Comas: Av. Universitaria 1250-Comas - Lima - Lima / Telefonos: 537-6090", Formato));
            clSede2.BorderWidth = 0;
            clSede2.Colspan = 2;
            clSede2.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clSede2);

            PdfPCell clnum = new PdfPCell(new Phrase("Nª" + " " + xidGuia, Negrita));
            clnum.BorderWidth = 1;
            clnum.Rowspan = 2;
            clnum.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clnum);

            //FILA6
            PdfPCell clSede3 = new PdfPCell(new Phrase("Sede Puente Piedra: Calle La Victoria 509 - Puente Piedra - Lima - Lima / Telefonos: 586-7283", Formato));
            clSede3.BorderWidth = 0;
            clSede3.Colspan = 2;
            clSede3.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clSede3);

            PdfPCell clespacio = new PdfPCell(new Phrase(" ", Formato));
            clespacio.BorderWidth = 0;
            clespacio.Colspan = 3;
            clespacio.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clespacio);

            doc.Add(tblcabecera);


            PdfPTable tblFecha = new PdfPTable(2);
            tblFecha.WidthPercentage = 100;
            //dar tamaño a la celda
            float[] medidaCeldasx = { 10f, 10f };
            tblFecha.SetWidths(medidaCeldasx);
            //capturar valores de Guia Cabecera
            GuiaCabeceraN objBuscar = new GuiaCabeceraN();
            DataTable DTILogistica = objBuscar.BuscarGuiaCabecera(xidGuia);
            DataRow row = DTILogistica.Rows[0];

            string usu = row["Usuario que Envia"].ToString();
            string trans = row["Transportista"].ToString();
            string traslado = row["Motivo Traslado"].ToString();
            string PPARTIDA = row["SedePartida"].ToString();
            string PLLEGADA = row["SedeLlegada"].ToString();
            string UsuRecepciona = row["Usuario que Recepciona"].ToString();
            string FecRecepciona = row["Fecha Recepcion"].ToString();
            DateTime fecenvio = DateTime.Parse(row["Fecha de Envio"].ToString());
            int estado = int.Parse(row["Activo"].ToString());
            ////Feha
            DateTime fecRecibido = Convert.ToDateTime(DateTime.Now.ToString());

            PdfPCell clfecha = new PdfPCell(new Phrase("Fecha Envío:" + " " + fecenvio, Negrita2));
            clfecha.BorderWidth = 0;

            clfecha.HorizontalAlignment = Element.ALIGN_LEFT;
            tblFecha.AddCell(clfecha);

            string usuario = Session["Usuario"].ToString().ToUpper();
            PdfPCell clUsuario = new PdfPCell(new Phrase("Usuario:" + " " + usu, Negrita2));
            clUsuario.BorderWidth = 0;

            clUsuario.HorizontalAlignment = Element.ALIGN_RIGHT;
            tblFecha.AddCell(clUsuario);

            doc.Add(tblFecha);
            doc.Add(Chunk.NEWLINE);
            ///////

            PdfPTable tblTabla1 = new PdfPTable(4);
            tblTabla1.WidthPercentage = 100;
            //dar tamaño a la celda

            float[] medidaCeldas = { 1.7f, 4.2f, 4f, 4.2f };
            tblTabla1.SetWidths(medidaCeldas);


            //FILA1
            PdfPCell clespacio2 = new PdfPCell(new Phrase(" ", Negrita2));

            clespacio2.BorderWidth = 0;
            clespacio2.Colspan = 4;
            clespacio2.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clTrasportista = new PdfPCell(new Phrase("Transportista:", Negrita2));

            clTrasportista.BorderWidth = 0;
            clTrasportista.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clTrasportista1 = new PdfPCell(new Phrase(trans.ToUpper(), Formato));

            clTrasportista1.BorderWidth = 0;
            clTrasportista1.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clMotivo = new PdfPCell(new Phrase("Motivo de Traslado:", Negrita2));
            clMotivo.BorderWidth = 0;
            clMotivo.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clMotivo1 = new PdfPCell(new Phrase(traslado.ToString(), Formato));
            clMotivo1.BorderWidth = 0;
            clMotivo1.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clPuntoPartida = new PdfPCell(new Phrase("Punto de Partida:", Negrita2));
            //clS1.BorderWidth = 1;
            clPuntoPartida.HorizontalAlignment = Element.ALIGN_CENTER;
            clPuntoPartida.BorderWidth = 0;

            PdfPCell clPuntoPartida1 = new PdfPCell(new Phrase(PPARTIDA, Formato));
            //clS1.BorderWidth = 1;
            clPuntoPartida1.HorizontalAlignment = Element.ALIGN_CENTER;
            clPuntoPartida1.BorderWidth = 0;

            PdfPCell clPuntoLlegada = new PdfPCell(new Phrase("Punto de Llegada:", Negrita2));
            //clS1.BorderWidth = 1;
            clPuntoLlegada.HorizontalAlignment = Element.ALIGN_CENTER;
            clPuntoLlegada.BorderWidth = 0;

            PdfPCell clPuntoLlegada1 = new PdfPCell(new Phrase(PLLEGADA, Formato));
            //clS1.BorderWidth = 1;
            clPuntoLlegada1.HorizontalAlignment = Element.ALIGN_CENTER;
            clPuntoLlegada1.BorderWidth = 0;

            PdfPCell clUsuRecepciona = new PdfPCell(new Phrase("Usuario que Recepciona:", Negrita2));
            //clS1.BorderWidth = 1;
            clUsuRecepciona.HorizontalAlignment = Element.ALIGN_CENTER;
            clUsuRecepciona.BorderWidth = 0;

            PdfPCell clUsuRecepciona1 = new PdfPCell(new Phrase(UsuRecepciona, Formato));
            //clS1.BorderWidth = 1;
            clUsuRecepciona1.HorizontalAlignment = Element.ALIGN_CENTER;
            clUsuRecepciona1.BorderWidth = 0;

            PdfPCell clFecRecepciona = new PdfPCell(new Phrase("Fecha de Recepción:", Negrita2));
            //clS1.BorderWidth = 1;
            clFecRecepciona.HorizontalAlignment = Element.ALIGN_CENTER;
            clFecRecepciona.BorderWidth = 0;

            PdfPCell clFecRecepciona1 = new PdfPCell(new Phrase(FecRecepciona, Formato));
            //clS1.BorderWidth = 1;
            clFecRecepciona1.HorizontalAlignment = Element.ALIGN_CENTER;
            clFecRecepciona1.BorderWidth = 0;

            PdfPCell clvacio = new PdfPCell(new Phrase(" ", Formato));
            //clS1.BorderWidth = 1;
            clvacio.HorizontalAlignment = Element.ALIGN_CENTER;
            clvacio.BorderWidth = 0;
            clvacio.Colspan = 4;

            //FILA1
            tblTabla1.AddCell(clespacio2);
            tblTabla1.AddCell(clTrasportista);
            tblTabla1.AddCell(clTrasportista1);
            tblTabla1.AddCell(clMotivo);
            tblTabla1.AddCell(clMotivo1);
            tblTabla1.AddCell(clPuntoPartida);
            tblTabla1.AddCell(clPuntoPartida1);
            tblTabla1.AddCell(clPuntoLlegada);
            tblTabla1.AddCell(clPuntoLlegada1);
            tblTabla1.AddCell(clUsuRecepciona);
            tblTabla1.AddCell(clUsuRecepciona1);
            tblTabla1.AddCell(clFecRecepciona);
            tblTabla1.AddCell(clFecRecepciona1);
            tblTabla1.AddCell(clvacio);

            doc.Add(tblTabla1);

            doc.Add(Chunk.NEWLINE);


            if (estado == 1) // si es detalle activo
            {
                ///tabla
                PdfPTable tblTabla = new PdfPTable(6);
                tblTabla.WidthPercentage = 100;
                //dar tamaño a la celda

                float[] medidaCeldas1 = { 2f, 4.2f, 6f, 6f, 2f, 2f };
                tblTabla.SetWidths(medidaCeldas1);

                ////////////////////////////////////////////////////////////////////////////////////////

                //FILA1
                PdfPCell clCodigo = new PdfPCell(new Phrase("Código", Negrita2));

                clCodigo.BorderWidth = 1;
                clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clDescripcion = new PdfPCell(new Phrase("Descripción", Negrita2));

                clDescripcion.BorderWidth = 1;
                clDescripcion.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clOrigen = new PdfPCell(new Phrase("Origen", Negrita2));
                clOrigen.BorderWidth = 1;
                clOrigen.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clDetalle = new PdfPCell(new Phrase("Detalle", Negrita2));
                clDetalle.BorderWidth = 1;
                clDetalle.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clCondicion = new PdfPCell(new Phrase("Condición", Negrita2));
                //clS1.BorderWidth = 1;
                clCondicion.HorizontalAlignment = Element.ALIGN_CENTER;
                clCondicion.BorderWidth = 1;

                PdfPCell clUnidadMedida = new PdfPCell(new Phrase("Unidad Medida", Negrita2));
                //clS1.BorderWidth = 1;
                clUnidadMedida.HorizontalAlignment = Element.ALIGN_CENTER;
                clUnidadMedida.BorderWidth = 1;

                //FILA1
                tblTabla.AddCell(clCodigo);
                tblTabla.AddCell(clDescripcion);
                tblTabla.AddCell(clOrigen);
                tblTabla.AddCell(clDetalle);
                tblTabla.AddCell(clCondicion);
                tblTabla.AddCell(clUnidadMedida);

                for (int i = 0; i < gvDetalleGuia.Rows.Count; i++)
                {
                    ////clNro = new PdfPCell(new Phrase(Convert.ToString(i + 1), Formato));
                    ////clNro.HorizontalAlignment = Element.ALIGN_CENTER;
                    ////clNro.BorderWidth = 1;

                    clCodigo = new PdfPCell(new Phrase(gvDetalleGuia.Rows[i].Cells[1].Text, Formato));
                    clCodigo.BorderWidth = 1;
                    clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;

                    clDescripcion = new PdfPCell(new Phrase(gvDetalleGuia.Rows[i].Cells[2].Text, Formato));
                    clDescripcion.BorderWidth = 1;
                    clDescripcion.HorizontalAlignment = Element.ALIGN_CENTER;

                    clOrigen = new PdfPCell(new Phrase(gvDetalleGuia.Rows[i].Cells[3].Text, Formato));
                    clOrigen.BorderWidth = 1;
                    clOrigen.HorizontalAlignment = Element.ALIGN_CENTER;

                    clDetalle = new PdfPCell(new Phrase(gvDetalleGuia.Rows[i].Cells[4].Text, Formato));
                    clDetalle.BorderWidth = 1;
                    clDetalle.HorizontalAlignment = Element.ALIGN_CENTER;

                    clCondicion = new PdfPCell(new Phrase(gvDetalleGuia.Rows[i].Cells[5].Text, Formato));
                    clCondicion.BorderWidth = 1;
                    clCondicion.HorizontalAlignment = Element.ALIGN_CENTER;

                    clUnidadMedida = new PdfPCell(new Phrase(gvDetalleGuia.Rows[i].Cells[6].Text, Formato));
                    clUnidadMedida.BorderWidth = 1;
                    clUnidadMedida.HorizontalAlignment = Element.ALIGN_CENTER;

                    tblTabla.AddCell(clCodigo);
                    tblTabla.AddCell(clDescripcion);
                    tblTabla.AddCell(clOrigen);
                    tblTabla.AddCell(clDetalle);
                    tblTabla.AddCell(clCondicion);
                    tblTabla.AddCell(clUnidadMedida);

                }
                doc.Add(tblTabla);
            }

            if (estado == 0)
            {
                ///tabla
                PdfPTable tblTablaBi = new PdfPTable(3);
                tblTablaBi.WidthPercentage = 100;
                //dar tamaño a la celda

                float[] medidaCeldas2 = { 1.7f, 4.2f, 4.2f };
                tblTablaBi.SetWidths(medidaCeldas2);

                ////////////////////////////////////////////////////////////////////////////////////////

                //FILA1
                PdfPCell clCant = new PdfPCell(new Phrase("Cantidad", Negrita2));

                clCant.BorderWidth = 1;
                clCant.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clDescrip = new PdfPCell(new Phrase("Descripción", Negrita2));

                clDescrip.BorderWidth = 1;
                clDescrip.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clUnidadMe = new PdfPCell(new Phrase("Unidad de Medida:", Negrita2));
                //clS1.BorderWidth = 1;
                clUnidadMe.HorizontalAlignment = Element.ALIGN_CENTER;
                clUnidadMe.BorderWidth = 1;


                //FILA1

                tblTablaBi.AddCell(clCant);
                tblTablaBi.AddCell(clDescrip);
                tblTablaBi.AddCell(clUnidadMe);

                for (int i = 0; i < gvDetalleGuia.Rows.Count; i++)
                {
                    ////clNro = new PdfPCell(new Phrase(Convert.ToString(i + 1), Formato));
                    ////clNro.HorizontalAlignment = Element.ALIGN_CENTER;
                    ////clNro.BorderWidth = 1;

                    clCant = new PdfPCell(new Phrase(gvDetalleGuia.Rows[i].Cells[0].Text.ToString(), Formato));
                    clCant.BorderWidth = 1;
                    clCant.HorizontalAlignment = Element.ALIGN_CENTER;

                    clDescrip = new PdfPCell(new Phrase(gvDetalleGuia.Rows[i].Cells[1].Text, Formato));
                    clDescrip.BorderWidth = 1;
                    clDescrip.HorizontalAlignment = Element.ALIGN_CENTER;

                    clUnidadMe = new PdfPCell(new Phrase(gvDetalleGuia.Rows[i].Cells[2].Text, Formato));
                    clUnidadMe.BorderWidth = 1;
                    clUnidadMe.HorizontalAlignment = Element.ALIGN_CENTER;

                    tblTablaBi.AddCell(clCant);
                    tblTablaBi.AddCell(clDescrip);
                    tblTablaBi.AddCell(clUnidadMe);
                    // doc.Add(tblTablaBi);
                }
                doc.Add(tblTablaBi);
            }




            ///////////////////////////////////////////////////////

            doc.Close();
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            string idlogin = Session["rpta"].ToString().ToUpper();
            //actualizar guiacabecera
            GuiaCabeceraN objEstadoGuia = new GuiaCabeceraN();
            GuiaCabeceraEn objGuiaEn = new GuiaCabeceraEn();
            objGuiaEn.IdGuia = xidGuia;
            objGuiaEn.IdLoginAnulado = int.Parse(idlogin);
            objGuiaEn.FechaAnulacion = DateTime.Parse(txtFecha.Text);
            objGuiaEn.IdEstado = int.Parse(txtEstado.Text);
            objGuiaEn.ip = Request.UserHostAddress;
            string cod = objEstadoGuia.ActualizarEstadoGuia(objGuiaEn);

            //string  mensaje = ("<script language ='JavaScript'>window.confirm('Va a salir de la aplicación ¿Desea salir realmente?'); </script>");
            //if (mensaje == "Ok")
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Gracias por visitarnos'); </script>");
            //}
            //var mensaj = (this.Page.Response.Write("<script language ='JavaScript'>window.confirm('Va a salir de la aplicación ¿Desea salir realmente?'); </script>"));

            this.Page.Response.Write("<script language ='JavaScript'>window.alert('Se Anulo la Guia del Activo'); </script>");

            btnRecepcion.Enabled = false;
            btnReparacion.Enabled = false;
            btnImprimir.Enabled = false;
            btnAnular.Enabled = false;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvDetalleGuia.Rows.Count == 0)
                {
                    throw new Exception("No existen datos a exportar.");
                }

                string strBody = "<html><body>";
                strBody += "<table style='width: 1000px;'>";
                strBody += "<tr>";
                strBody += "<td style='width: 150px; padding: 5px;'>";
                strBody += "<img src='https://sistemas.sise.com.pe/sistemas/img/logo_rojo.png' />";
                strBody += "</td>";
                strBody += "<td style='padding: 5px;' colspan='5'>";
                strBody += "<h2 style='text-align: center; margin: 0px;'>DETALLE DE LA GU&Iacute;A NRO " + lblNroGuia.Text + "</h3>";
                strBody += "</td>";
                strBody += "</tr>";
                strBody += "</table>";

                strBody += "<br>";
                strBody += "<p>Fecha: " + DateTime.Now + "</p>";

                strBody += "<table border='1'>";
                strBody += "<tr style='background: #CCCCCC; font-weight: bold; text-align: center;'>";

                if (Request.QueryString["a"] == "0")  // 0 = bien
                {
                    strBody += "<td>CANTIDAD</td>";
                    strBody += "<td>DESCRIPCI&Oacute;N</td>";
                    strBody += "<td>UNIDAD</td>";
                }
                else // 1 = activo 
                {
                    strBody += "<td>C&Oacute;DIGO</td>";
                    strBody += "<td>DESCRIPCI&Oacute;N</td>";
                    strBody += "<td>ORIGEN</td>";
                    strBody += "<td>DETALLE</td>";
                    strBody += "<td>CONDICI&Oacute;N</td>";
                    strBody += "<td>UNIDAD</td>";
                }

                strBody += "</tr>";

                foreach (GridViewRow row in gvDetalleGuia.Rows)
                {
                    strBody += "<tr style='text-align: center; vertical-align: middle;'>";

                    if (Request.QueryString["a"] == "0")  // 0 = bien
                    {
                        strBody += "<td>" + row.Cells[0].Text + "</td>";
                        strBody += "<td>" + row.Cells[1].Text + "</td>";
                        strBody += "<td>" + row.Cells[2].Text + "</td>";
                    }
                    else // 1 = activo
                    {
                        strBody += "<td>" + row.Cells[1].Text + "</td>";
                        strBody += "<td>" + row.Cells[2].Text + "</td>";
                        strBody += "<td>" + row.Cells[3].Text + "</td>";
                        strBody += "<td>" + row.Cells[4].Text + "</td>";
                        strBody += "<td>" + row.Cells[5].Text + "</td>";
                        strBody += "<td>" + row.Cells[6].Text + "</td>";
                    }

                    strBody += "</tr>";
                }

                strBody += "</table>";
                strBody += "</body></html>";

                string fileName = "Reporte_Guia_detalle.xls";
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

        protected void btnReparacion_Click(object sender, EventArgs e)
        {
            //string script = "<script>window.open('Reparaciones.aspx?') </script>";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "page", script, false);

            string id = lblNroGuia.Text;
            Response.Redirect("Reparaciones.aspx?c=" + id);

        }
    }
}