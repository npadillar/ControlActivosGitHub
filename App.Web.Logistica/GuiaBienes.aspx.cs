using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// ADICIONAR LIBRERIAS PDF
using System.ComponentModel;
using System.Drawing;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;
//correo
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace App.Web.Logistica
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        LogisticaN objLogistica = new LogisticaN();
        MotivoTrasladoN objTraslado = new MotivoTrasladoN();
        UnidadMedidaN objUnidad = new UnidadMedidaN();
        SedeN objSede = new SedeN();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {

                return;

            }

            if ( Session["Usuario"] != null)
            {
            //prueba grid bienes
            DataTable Tabla = new DataTable();
            Tabla.Columns.Add("Cant");
            Tabla.Columns.Add("Descripcion");
            Tabla.Columns.Add("IdUnidadMedida");
            Tabla.Columns.Add("UnidadMedida");

            gvBienes.DataSource = Tabla;
            gvBienes.DataBind();
            Session["Datos"] = Tabla;
            //fin prueba

            //Listar Motivotraslado
            ddlMotivoTrasldo.DataTextField = "Descripcion";
            ddlMotivoTrasldo.DataValueField = "IdMotivoTraslado";
            ddlMotivoTrasldo.DataSource = objTraslado.ListarMotivoTraslado();
            ddlMotivoTrasldo.DataBind();
            ddlMotivoTrasldo.Items.Insert(0, " -------------- Seleccione -------------- ");
            //ListarUnidadMedida
            ddlUnidadMedida.DataTextField = "Descripcion";
            ddlUnidadMedida.DataValueField = "IdUnidadMedida";
            ddlUnidadMedida.DataSource = objUnidad.ListarUnidadMedida();
            ddlUnidadMedida.DataBind();
            ddlUnidadMedida.SelectedIndex = 7; //" ---- Seleccione ---- "
            //listar sedepartida
            ddlPuntoPartida.DataTextField = "Descripcion";
            ddlPuntoPartida.DataValueField = "IdSede";
            ddlPuntoPartida.DataSource = objSede.ListarSede("partida");
            ddlPuntoPartida.DataBind();
            ddlPuntoPartida.Items.Insert(0, " -------------- Seleccione -------------- ");

            //listar sedellegada
            ddlPuntoLlegada.DataTextField = "Descripcion";
            ddlPuntoLlegada.DataValueField = "IdSede";
            ddlPuntoLlegada.DataSource = objSede.ListarSede("mantenimiento");
            ddlPuntoLlegada.DataBind();
            ddlPuntoLlegada.Items.Insert(0, " -------------- Seleccione -------------- ");
     

            string usuario = Session["Usuario"].ToString().ToUpper();
            string idlogin = Session["rpta"].ToString().ToUpper();
            Bloquear();
            }
        }

       public void limpiar()
        {
            txtTransportista.Text = string.Empty;
            ddlPuntoLlegada.SelectedIndex = 0;
            ddlPuntoPartida.SelectedIndex = 0;
            ddlUnidadMedida.SelectedIndex = 7;
            ddlMotivoTrasldo.SelectedIndex = 0;
            txtCantidad.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtllegada.Text = string.Empty;
         //   Session["Datos"] = null;
            gvBienes.DataSource = null;
            gvBienes.DataBind();
            txtTransportista.Focus();
        }
        public void Habilitar()
        {
            txtTransportista.Enabled=true;
            ddlPuntoLlegada.Enabled = true;
            ddlPuntoPartida.Enabled = true;
            ddlUnidadMedida.Enabled = true;
            ddlMotivoTrasldo.Enabled = true;
            txtCantidad.Enabled = true;
            txtDescripcion.Enabled = true;
            btnAdicionar.Enabled = true;   
        }

        public void Bloquear()
        {
            txtTransportista.Enabled = false;
            ddlPuntoLlegada.Enabled = false;
            ddlPuntoPartida.Enabled = false;
            ddlUnidadMedida.Enabled = false;
            ddlMotivoTrasldo.Enabled = false;
            txtCantidad.Enabled = false;
            txtDescripcion.Enabled = false;
            btnAdicionar.Enabled = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (gvBienes.Rows.Count == 0)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('No existen registros agregados en la lista de la guía. Verifique por favor.');</script>");
                return;
            }

            if (String.IsNullOrEmpty(txtTransportista.Text))
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Ingrese Transportista.');</script>");
                txtTransportista.Focus();

                return;
            }
            if (ddlMotivoTrasldo.SelectedIndex == 0)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Seleccione Motivo de Traslado.');</script>");
                ddlMotivoTrasldo.Focus();
                return;
            }

            if (ddlPuntoPartida.SelectedIndex == 0)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Seleccione Punto de Partida.');</script>");
                ddlPuntoPartida.Focus();
                return;
            }
            if (ddlPuntoLlegada.SelectedIndex == 0)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Seleccione Punto de Llegada.');</script>");
                ddlPuntoLlegada.Focus();
                return;
            }

            if (ddlPuntoLlegada.SelectedItem.ToString() == "Otros")
            {
                if (txtllegada.Text == "")
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Especifique Punto de Llegada.');</script>");
                    txtllegada.Focus();
                    return;
                }
            }


            GuiaCabeceraEn ObjInsertGuia = new GuiaCabeceraEn();
            GuiaDetalleActivosEn ObjInsertDet = new GuiaDetalleActivosEn();

            ObjInsertGuia.IdLogin = int.Parse(Session["rpta"].ToString().ToUpper());
            ObjInsertGuia.SedePartida = ddlPuntoPartida.SelectedValue;
            ObjInsertGuia.SedeLlegada = ddlPuntoLlegada.SelectedValue;
            ObjInsertGuia.OrigenDestinoExterno = txtllegada.Text;
           
            ObjInsertGuia.Fecha = DateTime.Now;  //DateTime.Parse(TxtFecha.Text);
            ObjInsertGuia.Transportista = txtTransportista.Text;
            ObjInsertGuia.IdMotivoTraslado = int.Parse(ddlMotivoTrasldo.SelectedValue);
            ObjInsertGuia.Activos = 0;
            ObjInsertGuia.IdEstado = int.Parse(txtEstado.Text);
            ObjInsertGuia.observ = txtObserv.Text.Trim();
            ObjInsertGuia.ip = Request.UserHostAddress;
            GuiaCabeceraN objGC = new GuiaCabeceraN();


            string rpta = "";
            rpta = objGC.InsertarGuiaCabecera(ObjInsertGuia);
            hdnIdGuia.Value = rpta;

            //Session["rpta"]=rpta;
            GuiaDetalleActivoN objDetalle = new GuiaDetalleActivoN();

            
            //adicionar detalle bienes
            GuiaDetalleBienesEn objInsertarBienes = new GuiaDetalleBienesEn();
            GuiaDetalleBienesN objBienes = new GuiaDetalleBienesN();

            objInsertarBienes.IdGuia = int.Parse(rpta);
           
            int tot = 0;
            tot = gvBienes.Rows.Count;
            for (int i = 0; i < tot; i += 1)
            {
                objInsertarBienes.Cant = int.Parse(gvBienes.Rows[i].Cells[0].Text);
                objInsertarBienes.Descripcion = gvBienes.Rows[i].Cells[1].Text;
               // objInsertarBienes.IdUnidadMedida = int.Parse(gvBienes.Rows[i].Cells[2].Text);
                objInsertarBienes.IdUnidadMedida = Convert.ToInt32(gvBienes.DataKeys[i].Values[0]);
                objBienes.InsertarGuiaDetalleBienes(objInsertarBienes);
       
            }
            ObjInsertGuia.IdGuia = int.Parse(rpta);
            objGC.pr_registrar_auditoria_guia(ObjInsertGuia);

            this.Page.Response.Write("<script language ='JavaScript'>window.alert('Guia registrada correctamente.');</script>");
           
          //  limpiar();
            hdusuario.Value = Session["Usuario"].ToString().ToUpper(); 
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnImprimir.Enabled = true;
            hdIdGuia.Value = rpta;
            Bloquear();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Digite Cantidad.');</script>");
                txtCantidad.Focus();
                return;
            }
            if (txtDescripcion.Text == "")
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Digite Descripcion.');</script>");
                txtDescripcion.Focus();
                return;
            }
            DataTable Tabla = new DataTable();
            Tabla = (DataTable)Session["Datos"];
            Tabla.Rows.Add(txtCantidad.Text, txtDescripcion.Text,ddlUnidadMedida.SelectedValue,ddlUnidadMedida.SelectedItem.ToString());
            gvBienes.DataSource = Tabla;
            gvBienes.DataBind();
            Session["Datos"] = Tabla;
            txtCantidad.Text = "";
            txtDescripcion.Text = "";
            ddlUnidadMedida.Focus();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnImprimir.Enabled = false;
            Habilitar();

            DataTable Tabla = new DataTable();
            Tabla.Columns.Add("Cant");
            Tabla.Columns.Add("Descripcion");
            Tabla.Columns.Add("IdUnidadMedida");
            Tabla.Columns.Add("UnidadMedida");

            gvBienes.DataSource = null;
            gvBienes.DataBind();
            Session["Datos"] = Tabla;
        }

        private void imprimirguia(string _dir, string Usuario)
        {
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnImprimir.Enabled = false;

            //string nomfile = "GUIABIENES" + DateTime.Now.ToShortDateString() + ".pdf";
            string xnombre = "GUIABIENES" + int.Parse(hdIdGuia.Value) + ".pdf";

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=" + nomfile);
            Response.AddHeader("content-disposition", "attachment;filename=" + xnombre.ToString());

            //Document doc = new Document(PageSize.A4, 0.0F, 0.0F, 20.0F, 0.0F);
            Document doc = new Document(PageSize.A4.Rotate(), 10, 20, 20, 10);
            PdfWriter.GetInstance(doc, Response.OutputStream);
            // INI PRUEBA
            xnombre = "GUIABIENES";

            //_dir = System.Web.HttpContext.Current.Server.MapPath("~/Bienes");
            //if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);
            //FileStream _stream = new FileStream(string.Format("{0}/" + xnombre + "{1}.pdf", _dir, hdnIdGuia.Value.ToString()), FileMode.Create);

            //PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);

            Session["xnombre"] = string.Format(xnombre + "{1}.pdf", _dir, hdnIdGuia.Value.ToString());

            // FIN PRUEBA
            doc.Open();

            //////detalle de la guia bien
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
            PdfPCell clSede1 = new PdfPCell(new Phrase("Sede San Miguel: Av. La Marina 1429-San Miguel - Lima - Lima / Telefonos: 263-0269", Formato));
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

            PdfPCell clnum = new PdfPCell(new Phrase("Nª" + " " + hdIdGuia.Value, Negrita));
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

            ////Feha
            DateTime fec = Convert.ToDateTime(DateTime.Now.ToString());
            ////Paragraph Fecha = new Paragraph("Fecha:" + " " + fec, Negrita2);
            ////Fecha.Alignment = Element.ALIGN_LEFT;
            ////doc.Add(Fecha);

            PdfPCell clfecha = new PdfPCell(new Phrase("Fecha:" + " " + fec, Negrita2));
            clfecha.BorderWidth = 0;

            clfecha.HorizontalAlignment = Element.ALIGN_LEFT;
            tblFecha.AddCell(clfecha);

            Usuario = hdusuario.Value;
            PdfPCell clUsuario = new PdfPCell(new Phrase("Usuario:" + " " + Usuario, Negrita2));
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

            PdfPCell clTrasportista1 = new PdfPCell(new Phrase(txtTransportista.Text.ToUpper(), Formato));

            clTrasportista1.BorderWidth = 0;
            clTrasportista1.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clMotivo = new PdfPCell(new Phrase("Motivo de Traslado:", Negrita2));
            clMotivo.BorderWidth = 0;
            clMotivo.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clMotivo1 = new PdfPCell(new Phrase(ddlMotivoTrasldo.SelectedItem.ToString(), Formato));
            clMotivo1.BorderWidth = 0;
            clMotivo1.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clPuntoPartida = new PdfPCell(new Phrase("Punto de Partida:", Negrita2));
            //clS1.BorderWidth = 1;
            clPuntoPartida.HorizontalAlignment = Element.ALIGN_CENTER;
            clPuntoPartida.BorderWidth = 0;

            //PREGUNTAR SI ES OTROS
            string ppartida, pllegada;
            ppartida = ddlPuntoPartida.SelectedItem.ToString();

            PdfPCell clPuntoPartida1 = new PdfPCell(new Phrase(ppartida, Formato));
            clPuntoPartida1.HorizontalAlignment = Element.ALIGN_CENTER;
            clPuntoPartida1.BorderWidth = 0;

            PdfPCell clPuntoLlegada = new PdfPCell(new Phrase("Punto de Llegada:", Negrita2));
            //clS1.BorderWidth = 1;
            clPuntoLlegada.HorizontalAlignment = Element.ALIGN_CENTER;
            clPuntoLlegada.BorderWidth = 0;

            //if (ddlPuntoLlegada.SelectedItem.ToString() == "Otros")
            //{
            //    pllegada = txtllegada.Text;
            //}
            //else
            //{
            pllegada = ddlPuntoLlegada.SelectedItem.ToString() + " - " + txtllegada.Text;
            //}

            PdfPCell clPuntoLlegada1 = new PdfPCell(new Phrase(pllegada, Formato));
            //clS1.BorderWidth = 1;
            clPuntoLlegada1.HorizontalAlignment = Element.ALIGN_CENTER;
            clPuntoLlegada1.BorderWidth = 0;

            PdfPCell clObserv = new PdfPCell(new Phrase("Observación:", Negrita2));
            clObserv.BorderWidth = 0;
            clObserv.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clObserv1 = new PdfPCell(new Phrase(txtObserv.Text, Formato));
            clObserv1.BorderWidth = 0;
            clObserv1.HorizontalAlignment = Element.ALIGN_LEFT;
            clObserv1.Colspan = 3;

            PdfPCell clvacio = new PdfPCell(new Phrase(" ", Formato));
            //clS1.BorderWidth = 1;
            clvacio.HorizontalAlignment = Element.ALIGN_LEFT;
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
            tblTabla1.AddCell(clObserv);
            tblTabla1.AddCell(clObserv1);
            //  tblTabla1.AddCell(clUnidadMedida);
            //tblTabla1.AddCell(clUnidadMedida1);
            tblTabla1.AddCell(clvacio);

            doc.Add(tblTabla1);

            doc.Add(Chunk.NEWLINE);



            ///tabla
            PdfPTable tblTabla = new PdfPTable(3);
            tblTabla.WidthPercentage = 100;
            //dar tamaño a la celda

            float[] medidaCeldas1 = { 1.7f, 4.2f, 4.2f };
            tblTabla.SetWidths(medidaCeldas1);

            ////////////////////////////////////////////////////////////////////////////////////////

            //FILA1
            PdfPCell clCantidad = new PdfPCell(new Phrase("Cantidad", Negrita2));

            clCantidad.BorderWidth = 1;
            clCantidad.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clDescripcion = new PdfPCell(new Phrase("Descripción", Negrita2));

            clDescripcion.BorderWidth = 1;
            clDescripcion.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clUnidadMedida = new PdfPCell(new Phrase("Unidad de Medida:", Negrita2));
            //clS1.BorderWidth = 1;
            clUnidadMedida.HorizontalAlignment = Element.ALIGN_CENTER;
            clUnidadMedida.BorderWidth = 1;


            //FILA1
            tblTabla.AddCell(clCantidad);
            tblTabla.AddCell(clDescripcion);
            tblTabla.AddCell(clUnidadMedida);
            for (int i = 0; i < gvBienes.Rows.Count; i++)
            {
                ////clNro = new PdfPCell(new Phrase(Convert.ToString(i + 1), Formato));
                ////clNro.HorizontalAlignment = Element.ALIGN_CENTER;
                ////clNro.BorderWidth = 1;

                clCantidad = new PdfPCell(new Phrase(gvBienes.Rows[i].Cells[0].Text.ToString(), Formato));
                clCantidad.BorderWidth = 1;
                clCantidad.HorizontalAlignment = Element.ALIGN_CENTER;

                clDescripcion = new PdfPCell(new Phrase(gvBienes.Rows[i].Cells[1].Text, Formato));
                clDescripcion.BorderWidth = 1;
                clDescripcion.HorizontalAlignment = Element.ALIGN_CENTER;

                clUnidadMedida = new PdfPCell(new Phrase(gvBienes.Rows[i].Cells[3].Text, Formato));
                clUnidadMedida.BorderWidth = 1;
                clUnidadMedida.HorizontalAlignment = Element.ALIGN_CENTER;

                tblTabla.AddCell(clCantidad);
                tblTabla.AddCell(clDescripcion);
                tblTabla.AddCell(clUnidadMedida);

            }

            doc.Add(tblTabla);


            ///////////////////////////////////////////////////////

            doc.Close();

            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnImprimir.Enabled = false;
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string Usuario = "";
            string _dir = "";

            try
            {
                Usuario = hdusuario.Value;
                _dir = System.Web.HttpContext.Current.Server.MapPath("~/Bienes");
                ///////////////////////////////////////////////////////
                imprimirguia(_dir, Usuario);

                //NADA DE CORREOS DIJO EL SR. ENRIQUE
                //Enviar correo adjunto de la guia
                //Configuración del Mensaje
                //MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                ////Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
                //mail.From = new MailAddress("logistica@sise.com.pe", "Logistica", Encoding.UTF8);
                ////Aquí ponemos el asunto del correo
                //mail.Subject = "Guia de Bienes";

                ////  txtEmail.Text = Session["xnombre"].ToString();
                //string archiv = Session["xnombre"].ToString();
                ////Aquí ponemos el mensaje que incluirá el correo
                ////  string Usuario = hdusuario.Value;
                //mail.Body = "Se Envia la Guia de Bienes registrado por el usuario" + "  " + Usuario.ToUpper();


                ////Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                //if (ddlPuntoLlegada.SelectedValue.ToString() == "2") //pp
                //{
                //    //string xcorreo = "yayra@sise.com.pe";
                //    //string xcorreo2 = "soportepp@sise.com.pe";
                //    //string xcorreo3 = "mlugo@sise.com.pe";
                //    string xcorreo4 = "jnunez@sise.com.pe";
                //    //mail.To.Add(xcorreo);
                //    //mail.To.Add(xcorreo2);
                //    //mail.To.Add(xcorreo3);
                //    mail.To.Add(xcorreo4);

                //}
                //if (ddlPuntoLlegada.SelectedValue == "1") //"SAN MIGUEL"
                //{
                //    string xcorreo = "yayra@sise.com.pe";
                //    string xcorreo2 = "soportesm@sise.com.pe";
                //    string xcorreo3 = "framos@sise.com.pe";
                //    string xcorreo4 = "jnunez@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    mail.To.Add(xcorreo3);
                //    mail.To.Add(xcorreo4);
                //}
                //if (ddlPuntoLlegada.SelectedValue == "4")//COMAS
                //{
                //    string xcorreo = "yayra@sise.com.pe";
                //    string xcorreo2 = "soporteun@sise.com.pe";
                //    string xcorreo3 = "imarin@sise.com.pe";
                //    string xcorreo4 = "jnunez@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    mail.To.Add(xcorreo3);
                //    mail.To.Add(xcorreo4);
                //}
                //if (ddlPuntoLlegada.SelectedValue == "3")//tupac
                //{
                //    string xcorreo = "yayra@sise.com.pe";
                //    string xcorreo2 = "soporteun@sise.com.pe";
                //    string xcorreo3 = "imarin@sise.com.pe";
                //    string xcorreo4 = "jnunez@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    mail.To.Add(xcorreo3);
                //    mail.To.Add(xcorreo4);
                //}
                //if (ddlPuntoLlegada.SelectedValue == "5")//ventanilla
                //{
                //    string xcorreo = "yayra@sise.com.pe";
                //    string xcorreo2 = "soportepp@sise.com.pe";
                //    string xcorreo3 = "mlugo@sise.com.pe";
                //    string xcorreo4 = "jnunez@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    mail.To.Add(xcorreo3);
                //    mail.To.Add(xcorreo4);
                //}
                //if (ddlPuntoLlegada.SelectedValue == "6")//otros 6
                //{
                //    string xcorreo = "yayra@sise.com.pe";
                //    string xcorreo2 = "jnunez@sise.com.pe";
                //    string xcorreo3 = "esandoval@sise.com.pe";

                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    mail.To.Add(xcorreo3);
                //}
                //// INI prueba

                //mail.Attachments.Add(new Attachment(_dir + "/" + archiv.ToString()));
                //// FIN prueba 

                ////Configuracion del SMTP
                //SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                //                       //Especificamos las credenciales con las que enviaremos el mail
                //SmtpServer.Credentials = new System.Net.NetworkCredential("guiaactivos@sise.com.pe", "Logistica");
                //SmtpServer.EnableSsl = true;
                //SmtpServer.Send(mail);
                //this.Page.Response.Write("<script language ='JavaScript'>window.alert('<<Correo Enviado Correctamente>>');</script>");
                //fin de la enviar correo
                btnImprimir.Enabled = false;
            }
            catch (Exception)
            {
                imprimirguia(_dir, Usuario);
            }
            
        }
    }
}