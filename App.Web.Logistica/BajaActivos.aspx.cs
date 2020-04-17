using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using System.Web.UI.HtmlControls;
using Logistica.Libreria.Negocio;
// ADICIONAR LIBRERIAS PDF
using System.ComponentModel;
using System.Drawing;

using System.Net.Mail;


//using System.Drawing.Image;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net;

namespace App.Web.Logistica
{
    public partial class RptBajaActivos : System.Web.UI.Page
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        MotivoBajaActivosN objMotivo = new MotivoBajaActivosN();
        DisposicionBajaActivosN objDispo = new DisposicionBajaActivosN();

        protected void page_load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;
           // Listar motivo baja
            ddlMotivo.DataTextField = "Descripcion";
            ddlMotivo.DataValueField = "IdMotivo";
            ddlMotivo.DataSource = objMotivo.ListarMotivoBajaActivos();
            ddlMotivo.DataBind();
            ddlMotivo.Items.Insert(0, " -------- Seleccione -------- ");
            // Listar Disposición baja
            ddlDisposicion.DataTextField = "Descripcion";
            ddlDisposicion.DataValueField = "IdDisposicion";
            ddlDisposicion.DataSource = objDispo.ListarDisposicionBajaActivos();
            ddlDisposicion.DataBind();
            ddlDisposicion.Items.Insert(0, " -------- Seleccione -------- ");
            DateTime fecha = DateTime.Now;
            txtFechaBaja.Text= (fecha.ToString("yyyy-MM-dd"));
          //  DateTime.Parse(fechac.ToString("yyyy-MM-dd"))
            //prueba grid activos
            DataTable Tabla = new DataTable();
            Tabla.Columns.Add("Nª");
            Tabla.Columns.Add("IdLogistica");
            Tabla.Columns.Add("Codigo");
            Tabla.Columns.Add("Descripcion");
            Tabla.Columns.Add("Origen");
            Tabla.Columns.Add("Detalle");


            gvActivo.DataSource = Tabla;
            gvActivo.DataBind();
            Session["Datos"] = Tabla;
            //fin prueba
            Bloquear();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            BajaActivosN objBaja = new BajaActivosN();
            if (objBaja.fun_bloquear_activo_baja(txtCodigo.Text))
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('El activo ya fue dado de baja');</script>");
            }
            else
            {
                int tot = 0;
                LogisticaN objBuscarcodigoenGuia = new LogisticaN();

                DataTable DTBuscarCodigoenGuia = objBuscarcodigoenGuia.BuscarCodigoenGuia(txtCodigo.Text);
                int x = 0;
                if (DTBuscarCodigoenGuia.Rows.Count <= 0)
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('No se encontró el Código.');</script>");
                    txtCodigo.Text = string.Empty;
                    txtCodigo.Focus();
                    return;
                }

                tot = gvActivo.Rows.Count;
                for (int i = 0; i < tot; i += 1)
                {
                    if (txtCodigo.Text == gvActivo.Rows[i].Cells[2].Text)
                    {
                        this.Page.Response.Write("<script language ='JavaScript'>window.alert('El Código ya existe.');</script>");
                        txtCodigo.Text = string.Empty;
                        txtCodigo.Focus();
                        return;

                    }
                }

                DataRow row = DTBuscarCodigoenGuia.Rows[0];

                //hdIdLogistica.Value =  row["idlogistica"].ToString();
                string descripcion = "";
                string codigo = "";
                int IdLogistica = 0;
                string detalle = "";
                string Origen = "";
                string Condicion = "";
                IdLogistica = int.Parse(row["IdLogistica"].ToString());
                codigo = row["Codigo"].ToString();
                descripcion = row["Descripcion"].ToString();
                Origen = row["Origen"].ToString();
                detalle = row["Detalle"].ToString();
                Condicion = row["Condicion"].ToString();
                txtCondicion.Text = Condicion;
                //Condicion = row["Condicion"].ToString();

                DataTable Tabla = new DataTable();
                Tabla = (DataTable)Session["Datos"];


                Tabla.Rows.Add(x, IdLogistica, codigo, descripcion, Origen, detalle);
                gvActivo.DataSource = Tabla;
                gvActivo.DataBind();
                txtCodigo.Text = string.Empty;
                //  Session["Datos"] = Tabla;

                //gvActivo.DataSource = DTBuscarCodigoenGuia;
                //gvActivo.DataBind();
            }
        }
        public void Habilitar()
        {
            ddlMotivo.Enabled = true;
            ddlDisposicion.Enabled = true;
            txtFechaBaja.Enabled = true;
            txtCodigo.Enabled = true;
            btnBuscar.Enabled = true;
        }
        public void Bloquear()
        {
            ddlMotivo.Enabled = false;
            ddlDisposicion.Enabled = false;
            txtFechaBaja.Enabled = false;
            txtCodigo.Enabled = false;
            btnBuscar.Enabled = false;
        }

        protected void btnBajaActivo_Click(object sender, EventArgs e)
        {
            //validar
            if (ddlMotivo.SelectedIndex == 0)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Elija una Opción.');</script>");
                ddlMotivo.Focus();
                return;
            }
            if (ddlDisposicion.SelectedIndex == 0)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Elija una Opción.');</script>");
                ddlMotivo.Focus();
                return;
            }

            if (txtCondicion.Text == "Activo")
            {
                BajaActivosCabeceraEn bajaCabecera = new BajaActivosCabeceraEn();
                bajaCabecera.IdLogin = int.Parse(Session["rpta"].ToString().ToUpper());
                bajaCabecera.FechaBaja = DateTime.Parse(txtFechaBaja.Text);
                bajaCabecera.IdMotivo = int.Parse(ddlMotivo.SelectedValue);
                bajaCabecera.IdDisposicion = int.Parse(ddlDisposicion.SelectedValue);
                bajaCabecera.ip = Request.UserHostAddress;


                List<BajaActivosDetalleEn> listDetBaja = new List<BajaActivosDetalleEn>();
                for (int i = 0; i < gvActivo.Rows.Count; i++)
			    {
			        BajaActivosDetalleEn detBaja = new BajaActivosDetalleEn();
                    detBaja.IdLogistica = Convert.ToInt32(gvActivo.DataKeys[i].Values[0]);
                    detBaja.usuario = bajaCabecera.IdLogin;
                    detBaja.ip = bajaCabecera.ip;
                    listDetBaja.Add(detBaja);
			    }

                transacciones objTrans = new transacciones();
                if (objTrans.fun_registrar_baja(bajaCabecera, listDetBaja));
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Baja registrada correctamente.');</script>");
                    btnBajaActivo.Enabled = false;
                    btnNuevo.Enabled = true;
                    btnImprimir.Enabled = true;
                }
            }
            else if(txtCondicion.Text == "Baja")
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('El código ya fue dado de Baja');</script>");
            }
        }
        
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Habilitar();
            ddlMotivo.SelectedIndex = 0;
            ddlDisposicion.SelectedIndex = 0;
            DateTime fecha = DateTime.Now;
            txtFechaBaja.Text = (fecha.ToString("yyyy-MM-dd"));
            txtCodigo.Text = string.Empty;
            btnBajaActivo.Enabled = true;
            btnNuevo.Enabled = false;
            
            DataTable Tabla = new DataTable();
            Tabla.Columns.Add("Nª");
            Tabla.Columns.Add("IdLogistica");
            Tabla.Columns.Add("Codigo");
            Tabla.Columns.Add("Descripcion");
            Tabla.Columns.Add("Origen");
            Tabla.Columns.Add("Detalle");
            gvActivo.DataSource = null;
            gvActivo.DataBind();
            Session["Datos"] = Tabla;

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string nomfile = "BAJA_ACTIVOS" + DateTime.Now.ToShortDateString() + ".pdf";

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + nomfile);

            //Document doc = new Document(PageSize.A4, 0.0F, 0.0F, 20.0F, 0.0F);
            Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
            PdfWriter.GetInstance(doc, Response.OutputStream);
            doc.Open();

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            iTextSharp.text.Font Negrita = new iTextSharp.text.Font(bfTimes, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font Negrita2 = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font Negrita10 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font Formato = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            //iTextSharp.text.Font font = new iTextSharp.text.Font(BaseFont.TIMES_ROMAN, 6, font.BaseFont );
            BaseColor colorh = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#A9D0F5"));

            // Escribimos el encabezamiento en el documento
            //     doc.Add(Chunk.NEWLINE);

            PdfPTable tblcabecera_1 = new PdfPTable(1);
            //imagen

            Paragraph paragraph = new Paragraph("Getting Started ITextSharp.");

            //cabecera
            PdfPTable tblcabecera = new PdfPTable(4);
            tblcabecera.WidthPercentage = 100;
            float[] medidaCabecera = { 10f, 10f, 10f ,10F};
            tblcabecera.SetWidths(medidaCabecera);

            //fila1 
            string imageURL = Server.MapPath(".") + "/Iconos/logo.jpg";
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
            jpg.ScalePercent(35f);
            PdfPCell imageCell = new PdfPCell(jpg);
            
            imageCell.Border = 0;
            imageCell.Colspan = 4;
            // imageCell.Padding=(10f);
            imageCell.HorizontalAlignment = (Element.ALIGN_LEFT);

            tblcabecera.AddCell(imageCell);

            // tblcabecera.AddCell(cllogo);
                       
            //FILA2
            PdfPCell clTitulo = new PdfPCell(new Phrase("FORMATO DE BAJA DE ACTIVOS FIJOS", Negrita));
            clTitulo.BorderWidth = 0;
            clTitulo.Colspan = 4;
            clTitulo.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clTitulo);
            //espacio
            PdfPCell clespacio2 = new PdfPCell(new Phrase(" ", Negrita2));
            clespacio2.BorderWidth = 0;
            clespacio2.Colspan = 4;
            clespacio2.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clespacio2);

            //FILA3
            ////Feha
            DateTime fec = Convert.ToDateTime(DateTime.Now.ToString());

            PdfPCell clfecha = new PdfPCell(new Phrase("Fecha:" + " " + fec, Negrita2));
            clfecha.BorderWidth = 0;
            clfecha.Colspan = 2;
            clfecha.HorizontalAlignment = Element.ALIGN_LEFT;
            tblcabecera.AddCell(clfecha);
            
            string usuario = Session["Usuario"].ToString().ToUpper();
            PdfPCell clUsuario = new PdfPCell(new Phrase("Usuario:" + " " + usuario, Negrita2));
            clUsuario.BorderWidth = 0;
            clUsuario.Colspan = 2;
            clUsuario.HorizontalAlignment = Element.ALIGN_RIGHT;
            tblcabecera.AddCell(clUsuario);
            //espacio
            PdfPCell clespacio3 = new PdfPCell(new Phrase(" ", Negrita2));
            clespacio3.BorderWidth = 0;
            clespacio3.Colspan = 4;
            clespacio3.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clespacio3);

            ///////
            //Fila4

            PdfPCell clMotivo = new PdfPCell(new Phrase("Motivo Baja Activos:", Negrita2));
            clMotivo.BorderWidth = 0;
         //   clSede1.Colspan = 2;
            clMotivo.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clMotivo);
                       
            PdfPCell clMot = new PdfPCell(new Phrase(ddlMotivo.SelectedItem.ToString(), Formato));
            clMot.BorderWidth = 0;
        //    clSede2.Colspan = 2;
            clMot.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clMot);

            PdfPCell clFecbaja = new PdfPCell(new Phrase("Fecha Baja Activo", Negrita2));
            clFecbaja.BorderWidth = 0;
           // clnum.Rowspan = 2;
            clFecbaja.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clFecbaja);

            //DateTime fecha = DateTime.Now;
            DateTime fecha;
            fecha = DateTime.Parse(txtFechaBaja.Text);
            PdfPCell clFecbaja1 = new PdfPCell(new Phrase(fecha.ToShortDateString(), Formato));
            clFecbaja1.BorderWidth = 0;
          //  clSede3.Colspan = 2;
            clFecbaja1.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clFecbaja1);

            PdfPCell clespacio4 = new PdfPCell(new Phrase(" ", Negrita2));
            clespacio4.BorderWidth = 0;
            clespacio4.Colspan = 4;
            clespacio4.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clespacio4);

            doc.Add(tblcabecera);
               
            ///tabla
            PdfPTable tblTabla = new PdfPTable(5);
            tblTabla.WidthPercentage = 100;
            //dar tamaño a la celda

            float[] medidaCeldas1 = { 2f, 4.2f, 6f, 6f,6f};
            tblTabla.SetWidths(medidaCeldas1);

            ////////////////////////////////////////////////////////////////////////////////////////

            //FILA1
            PdfPCell clNro = new PdfPCell(new Phrase("Nª ", Negrita2));

            clNro.BorderWidth = 1;
            clNro.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clCodigo = new PdfPCell(new Phrase("Código", Negrita2));

            clCodigo.BorderWidth = 1;
            clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clDescrip = new PdfPCell(new Phrase("Descripción", Negrita2));
            clDescrip.BorderWidth = 1;
            clDescrip.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clOrigen = new PdfPCell(new Phrase("Origen", Negrita2));
            clOrigen.BorderWidth = 1;
            clOrigen.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clDetalle = new PdfPCell(new Phrase("Detalle", Negrita2));
            clDetalle.BorderWidth = 1;
            clDetalle.HorizontalAlignment = Element.ALIGN_CENTER;


            //FILA1
            tblTabla.AddCell(clNro);
            tblTabla.AddCell(clCodigo);
            tblTabla.AddCell(clDescrip);
            tblTabla.AddCell(clOrigen);
            tblTabla.AddCell(clDetalle);


            for (int i = 0; i < gvActivo.Rows.Count; i++)
            {
                clNro = new PdfPCell(new Phrase(Convert.ToString(i + 1), Formato));
                clNro.HorizontalAlignment = Element.ALIGN_CENTER;
                clNro.BorderWidth = 1;

                clCodigo = new PdfPCell(new Phrase(gvActivo.Rows[i].Cells[2].Text, Formato));
                clCodigo.BorderWidth = 1;
                clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;

                clDescrip = new PdfPCell(new Phrase(gvActivo.Rows[i].Cells[3].Text, Formato));
                clDescrip.BorderWidth = 1;
                clDescrip.HorizontalAlignment = Element.ALIGN_CENTER;

                clOrigen = new PdfPCell(new Phrase(gvActivo.Rows[i].Cells[4].Text, Formato));
                clOrigen.BorderWidth = 1;
                clOrigen.HorizontalAlignment = Element.ALIGN_CENTER;

                clDetalle = new PdfPCell(new Phrase(gvActivo.Rows[i].Cells[5].Text, Formato));
                clDetalle.BorderWidth = 1;
                clDetalle.HorizontalAlignment = Element.ALIGN_CENTER;
                
                tblTabla.AddCell(clNro);
                tblTabla.AddCell(clCodigo);
                tblTabla.AddCell(clDescrip);
                tblTabla.AddCell(clOrigen);
                tblTabla.AddCell(clDetalle);
            }
        doc.Add(tblTabla);
            ///////

            //Firmas
            PdfPTable tblPie = new PdfPTable(8);
            tblPie.WidthPercentage = 100;
            //dar tamaño a la celda
            float[] medidaCel = { 10f, 10f, 10f, 10f, 10f ,10f,10f,10f};
            tblPie.SetWidths(medidaCel);
                    tblPie.TotalWidth = 800.0F;
                    tblPie.LockedWidth = true;
                    tblPie.SpacingBefore = 70.0F;
                    tblPie.HorizontalAlignment = Element.ALIGN_BOTTOM;

            PdfPCell xf_6 = new PdfPCell(new Phrase("**Adjuntar Informe Técnico", Formato));
            xf_6.BorderWidth = 0;
            xf_6.Colspan = 8;
            xf_6.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            tblPie.AddCell(xf_6);

            PdfPCell xf_ = new PdfPCell(new Phrase(" ", Negrita));
            xf_.BorderWidth = 0;
            xf_.Colspan = 8;
            xf_.Rowspan = 5;
            tblPie.AddCell(xf_);
           
            PdfPCell xf_1 = new PdfPCell(new Phrase(" ", Negrita));
            xf_1.BorderWidth = 0;
            xf_1.Colspan = 8;
            xf_1.Rowspan = 5;
            tblPie.AddCell(xf_1);

            PdfPCell xf_2 = new PdfPCell(new Phrase(" ", Negrita));
            xf_2.BorderWidth = 0;
            xf_2.Colspan = 8;
            xf_2.Rowspan = 5;
            tblPie.AddCell(xf_2);

            PdfPCell xfirma = new PdfPCell(new Phrase("FIRMA DEL USUARIO", Negrita2));
            xfirma.BorderWidth = 0;
            xfirma.BorderWidthTop = 2;
            xfirma.Colspan = 2;
            xfirma.HorizontalAlignment = Element.ALIGN_BOTTOM;
            xfirma.HorizontalAlignment = Element.ALIGN_CENTER;
            tblPie.AddCell(xfirma);


            PdfPCell xf_3 = new PdfPCell(new Phrase(" ", Negrita));
            xf_3.BorderWidth = 0;
            tblPie.AddCell(xf_3);


            PdfPCell xVB = new PdfPCell(new Phrase("FIRMA DIRECTOR DE SEDE", Negrita2));
            xVB.BorderWidth = 0;
            xVB.BorderWidthTop = 2;
            xVB.Colspan = 2;
            //xVB.HorizontalAlignment = Element.ALIGN_BOTTOM;
           xVB.HorizontalAlignment = Element.ALIGN_CENTER;
            xVB.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
            xVB.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tblPie.AddCell(xVB);


            PdfPCell xf_4 = new PdfPCell(new Phrase(" ", Negrita));
            xf_4.BorderWidth = 0;
            tblPie.AddCell(xf_4);

            //PdfPCell xf_7 = new PdfPCell(new Phrase(" ", Negrita));
            //xf_7.BorderWidth = 0;
            //xf_7.Colspan = 5;
            //tblPie.AddCell(xf_7);

            ////FIRMA DIRECOTR ADMINISTRACION
            //PdfPCell xf_5 = new PdfPCell(new Phrase(" ", Negrita));
            //xf_5.BorderWidth = 0;
            //xf_5.Colspan = 3;
            //tblPie.AddCell(xf_5);

            PdfPCell xfirma3 = new PdfPCell(new Phrase("FIRMA DIRECTOR DE ADMINISTRACION", Negrita2));
            xfirma3.BorderWidth = 0;
            xfirma3.BorderWidthTop = 2;
            xfirma3.Colspan = 2;
            //xVB.HorizontalAlignment = Element.ALIGN_BOTTOM;
            xfirma3.HorizontalAlignment = Element.ALIGN_CENTER;
            xfirma3.VerticalAlignment = iTextSharp.text.Element.ALIGN_BOTTOM;
            xfirma3.VerticalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tblPie.AddCell(xfirma3);

            doc.Add(tblPie);

            ///////////////////////////////////////////////////////

            doc.Close();


            btnImprimir.Enabled = false;
        
        }

        protected void gvActivo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt16(e.CommandArgument);

                if (index != -1 && e.CommandName == "quitar")
                {
                    DataTable Tabla = new DataTable();
                    Tabla = (DataTable)Session["Datos"];

                    Tabla.Rows.RemoveAt(index);
                    gvActivo.DataSource = Tabla;
                    gvActivo.DataBind();
                    Session["Datos"] = Tabla;
                }
            }
            catch (Exception ex)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }




        // protected void Button1_Click(object sender, EventArgs e)
        //{
        //     CreateTestMessage1("smtp.gmail.com", 587);

        // }

        //public static void CreateTestMessage1(string server, int port)
        //{
        //    string to = "yeimi.vay@gmail.com";
        //    string from = "yeimi.vay@gmail.com";
        //    string subject = "Using the new SMTP client.";
        //    string body = @"Using this new feature, you can send an e-mail message from an application very easily.";
        //    MailMessage message = new MailMessage(from, to, subject, body);
        //    SmtpClient client = new SmtpClient(server, port);
        //    // Credentials are necessary if the server requires the client 
        //    // to authenticate before it will send e-mail on the client's behalf.
        //    client.Credentials = CredentialCache.DefaultNetworkCredentials;

        //    try
        //    {
        //        client.Send(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        //  Console.WriteLine("Exception caught in CreateTestMessage1(): {0}",
        //        throw new Exception("No se ha podido enviar el email", ex.InnerException);
        //   //  ex.InnerException;

        //    }
        //}

    }
}