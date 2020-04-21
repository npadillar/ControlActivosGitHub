using System;
using System.IO;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// ADICIONAR LIBRERIAS PDF
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;


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

using RestSharp;
using Newtonsoft.Json;

namespace App.Web.Logistica
{

    public partial class Guia : System.Web.UI.Page
    {

        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        LogisticaN objLogistica = new LogisticaN();
        MotivoTrasladoN objTraslado = new MotivoTrasladoN();
        UnidadMedidaN objUnidad = new UnidadMedidaN();
        SedeN objSede = new SedeN();

        public string sede { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (Page.IsPostBack)
            {
                if (Session["CODIGO"] != null)
                {
                    // IdGuia.Text = Session["CODIGO"].ToString();
                    // 
                    LogisticaN objBuscarcodigoenGuia = new LogisticaN();

                    string cod = Session["CODIGO"].ToString();

                    ////// DataTable dt = new DataTable();

                    DataTable DTBuscarCodigoenGuia = objBuscarcodigoenGuia.BuscarCodigoenGuia(Session["CODIGO"].ToString());
                    DataRow row = DTBuscarCodigoenGuia.Rows[0];
                    GuiaTemporalEn GuiaEn = new GuiaTemporalEn();
                    GuiaTemporalN guiaTem = new GuiaTemporalN();
                    GuiaEn.IdLogistica = int.Parse(row["IdLogistica"].ToString()); //row["nombre"].ToString();
                    GuiaEn.Codigo = cod;
                    GuiaEn.Descripcion = (row["Descripcion"].ToString());
                    GuiaEn.Origen = (row["Origen"].ToString());
                    GuiaEn.Detalle = (row["Detalle"].ToString());
                    GuiaEn.Condicion = (row["Condicion"].ToString());
                    GuiaEn.idUnidadMedida = int.Parse(ddlUnidadMedida.SelectedValue);
                    GuiaEn.UnidadMedida = ddlUnidadMedida.SelectedItem.ToString();

                    string codigox = guiaTem.InsertarGuiaTemporal(GuiaEn);
                    GuiaTemporalN guia = new GuiaTemporalN();

                    gvDetalle.DataSource = guia.ListarGuiaTemporal();
                    gvDetalle.DataBind();
                    (Session["CODIGO"]) = null;
                    ////GridView1.DataSource = DTBuscarCodigoenGuia;
                    ////GridView1.DataBind();
                }

                return;

            }
            if (Session["Usuario"] != null)
            {
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
                GuiaTemporalN guiat = new GuiaTemporalN();
                gvDetalle.DataSource = guiat.EliminarGuiaTemporal();
                Bloquear();
            }
        }

        public void limpiar()
        {
            Transportista.Text = string.Empty;
            ddlPuntoLlegada.SelectedIndex = 0;
            ddlPuntoPartida.SelectedIndex = 0;
            ddlUnidadMedida.SelectedIndex = 7;
            ddlMotivoTrasldo.SelectedIndex = 0;
            txtllegada.Text = string.Empty;

            //txtDescripcion.Text = string.Empty;
            //Session["Datos"] = null;
            //gvBienes.DataBind();
            gvDetalle.DataBind();
            Transportista.Focus();
            GuiaTemporalN guia = new GuiaTemporalN();
            gvDetalle.DataSource = guia.EliminarGuiaTemporal();
        }
        public void Habilitar()
        {
            Transportista.Enabled = true;
            ddlPuntoLlegada.Enabled = true;
            ddlPuntoPartida.Enabled = true;
            ddlUnidadMedida.Enabled = true;
            ddlMotivoTrasldo.Enabled = true;
            btnBuscar.Enabled = true;
        }
        public void Bloquear()
        {
            Transportista.Enabled = false;
            ddlPuntoLlegada.Enabled = false;
            ddlPuntoPartida.Enabled = false;
            ddlUnidadMedida.Enabled = false;
            ddlMotivoTrasldo.Enabled = false;
            btnBuscar.Enabled = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Response.Write("<script>window.open('BuscarCodigoenGuia.aspx','popup','width=1200,height=500,top=200,left=200,addressbar=0') </script>");

        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (gvDetalle.Rows.Count == 0)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('No existen activos agregados en la lista de la guía. Verifique por favor.');</script>");
                return;
            }


            if (String.IsNullOrEmpty(Transportista.Text))
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Ingrese Transportista.');</script>");
                Transportista.Focus();

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
            ObjInsertGuia.Transportista = Transportista.Text;
            ObjInsertGuia.IdMotivoTraslado = int.Parse(ddlMotivoTrasldo.SelectedValue);
            ObjInsertGuia.Activos = 1;
            ObjInsertGuia.IdEstado = int.Parse(txtEstado.Text);
            ObjInsertGuia.observ = txtObserv.Text.Trim();
            ObjInsertGuia.ip = Request.UserHostAddress;
            GuiaCabeceraN objGC = new GuiaCabeceraN();


            string rpta = "";
            rpta = objGC.InsertarGuiaCabecera(ObjInsertGuia);
            hdnIdGuia.Value = rpta;

            //Session["rpta"]=rpta;
            GuiaDetalleActivoN objDetalle = new GuiaDetalleActivoN();
            FacturaEn ObjInsertFactura = new FacturaEn();//**//
            FacturaN ObjFacGuia = new FacturaN();          

            //adicionar temporal  activos
            ObjInsertDet.IdGuia = int.Parse(rpta);
            //                ObjInsertDet.IdUnidadMedida = int.Parse(ddlUnidadMedida.SelectedValue);
            int cant = 0;
            cant = gvDetalle.Rows.Count;
            for (int i = 0; i < cant; i += 1)
            {
                ObjInsertFactura.IdLogistica = int.Parse(gvDetalle.Rows[i].Cells[0].Text);//**//
                ObjInsertFactura.Ruc = txtRuc.Text.Trim();
                ObjInsertFactura.Proveedor = Transportista.Text.Trim();
                ObjInsertFactura.Direccion = txtDireccion.Text.Trim();
                ObjFacGuia.pr_registrar_factura_guia(ObjInsertFactura);//**//

                ObjInsertDet.IdLogistica = int.Parse(gvDetalle.Rows[i].Cells[0].Text);
                //gvEmpleados.DataKeys[e.NewSelectedIndex].Values
                //    int id = Convertir .ToInt32 (GridView1.DataKeys [e.Row.RowIndex] .values [0]);
                //   int id = Convert.ToInt32(gvDetalle.DataKeys[i].Values[0]);
                ObjInsertDet.IdUnidadMedida = Convert.ToInt32(gvDetalle.DataKeys[i].Values[0]);
                objDetalle.InsertarGuiaDetalleActivos(ObjInsertDet);
            }
            ObjInsertGuia.IdGuia = int.Parse(rpta);
            objGC.pr_registrar_auditoria_guia(ObjInsertGuia);

            this.Page.Response.Write("<script language ='JavaScript'>window.alert('Guia registrada correctamente.');</script>");
            //vaciar tabla temporal
            //     GuiaTemporalN guia = new GuiaTemporalN();
            //     gvDetalle.DataSource = guia.EliminarGuiaTemporal();
            //     limpiar();
            //rpta captura el id de la guia
            //prueba de consulta
            hdIdGuia.Value = rpta;
            gvconsulta.DataSource = objDetalle.BuscarGuiaActivos(rpta);
            //string login = gvconsulta.Rows[0].Cells[2].Text;
            hdusuario.Value = Session["Usuario"].ToString().ToUpper();
            gvconsulta.DataBind();
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnImprimir.Enabled = true;
            Bloquear();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            Habilitar();
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnImprimir.Enabled = false;
            ddlMotivoTrasldo.Focus();
        }

        private void imprimirguia(string _dir, string Usuario)
        {
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnImprimir.Enabled = false;
            string xnombre;


            // Convert.ToDateTime(row("campofecha")).ToString("dd/MM/yyyy");
            //xnombre = "GUIAACTIVOS" + DateTime.Now.ToString("yyyymmdd") + ".pdf";
            xnombre = "GUIAACTIVOS" + int.Parse(hdnIdGuia.Value) + ".pdf";

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + xnombre.ToString());

            //Document doc = new Document(PageSize.A4, 0.0F, 0.0F, 20.0F, 0.0F);
            Document doc = new Document(PageSize.A4.Rotate(), 10, 20, 20, 10);
            PdfWriter.GetInstance(doc, Response.OutputStream);

            // INI PRUEBA
            xnombre = "GUIAACTIVOS";

            //string _dir = System.Web.HttpContext.Current.Server.MapPath("~/Guias");
            //_dir = System.Web.HttpContext.Current.Server.MapPath("~/Guias");
            //if (!Directory.Exists(_dir)) Directory.CreateDirectory(_dir);
            //FileStream _stream = new FileStream(string.Format("{0}/" + xnombre + "{1}.pdf", _dir, hdnIdGuia.Value.ToString()), FileMode.Create);
            //FileStream _stream = new FileStream(string.Format("{0}/" + xnombre + "{1}.pdf", _dir, DateTime.Now.ToString("yyyymmdd")), FileMode.Create);
            //FileStream _stream = new FileStream(string.Format("{0}/" + xnombre + "{1}.pdf", _dir, "1"), FileMode.Create);
            //PdfWriter writer = PdfWriter.GetInstance(doc, _stream);

            Session["xnombre"] = string.Format(xnombre + "{1}.pdf", _dir, hdnIdGuia.Value.ToString());
            //Session["xnombre"] = string.Format(xnombre + "{1}.pdf", _dir, DateTime.Now.ToString("yyyymmdd"));
            //  Session["xnombre"] = string.Format(xnombre + "{1}.pdf", _dir, "1");
            // FIN PRUEBA


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

            PdfPCell clTrasportista1 = new PdfPCell(new Phrase(Transportista.Text.ToUpper(), Formato));

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


            //PdfPCell clUnidadMedida1 = new PdfPCell(new Phrase(ddlUnidadMedida.SelectedItem.ToString(), Formato));
            ////clS1.BorderWidth = 1;
            //clUnidadMedida1.HorizontalAlignment = Element.ALIGN_CENTER;
            //clUnidadMedida1.BorderWidth = 0;
            ////  clUnidadMedida1.Colspan = 3;

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
            tblTabla1.AddCell(clObserv);
            tblTabla1.AddCell(clObserv1);
            //  tblTabla1.AddCell(clUnidadMedida);
            //tblTabla1.AddCell(clUnidadMedida1);
            tblTabla1.AddCell(clvacio);

            doc.Add(tblTabla1);

            doc.Add(Chunk.NEWLINE);

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

            for (int i = 0; i < gvDetalle.Rows.Count; i++)
            {
                ////clNro = new PdfPCell(new Phrase(Convert.ToString(i + 1), Formato));
                ////clNro.HorizontalAlignment = Element.ALIGN_CENTER;
                ////clNro.BorderWidth = 1;

                clCodigo = new PdfPCell(new Phrase(gvDetalle.Rows[i].Cells[1].Text, Formato));
                clCodigo.BorderWidth = 1;
                clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;

                clDescripcion = new PdfPCell(new Phrase(gvDetalle.Rows[i].Cells[2].Text, Formato));
                clDescripcion.BorderWidth = 1;
                clDescripcion.HorizontalAlignment = Element.ALIGN_CENTER;

                clOrigen = new PdfPCell(new Phrase(gvDetalle.Rows[i].Cells[3].Text, Formato));
                clOrigen.BorderWidth = 1;
                clOrigen.HorizontalAlignment = Element.ALIGN_CENTER;

                clDetalle = new PdfPCell(new Phrase(gvDetalle.Rows[i].Cells[4].Text, Formato));
                clDetalle.BorderWidth = 1;
                clDetalle.HorizontalAlignment = Element.ALIGN_CENTER;

                clCondicion = new PdfPCell(new Phrase(gvDetalle.Rows[i].Cells[5].Text, Formato));
                clCondicion.BorderWidth = 1;
                clCondicion.HorizontalAlignment = Element.ALIGN_CENTER;

                clUnidadMedida = new PdfPCell(new Phrase(gvDetalle.Rows[i].Cells[7].Text, Formato));
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

            doc.Close();

            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnImprimir.Enabled = false;
            hdlnomfile.Value = xnombre;
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            string Usuario = "";
            string _dir = "";

            try
            {
                Usuario = hdusuario.Value;
                _dir = System.Web.HttpContext.Current.Server.MapPath("~/Guias");
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
                //mail.Subject = "Guia de Activos";

                ////  txtEmail.Text = Session["xnombre"].ToString();
                //string archiv = Session["xnombre"].ToString();
                ////Aquí ponemos el mensaje que incluirá el correo
                ////  string Usuario = hdusuario.Value;
                //mail.Body = "Se Envia la Guia de Activos registrado por el usuario" + "  " + Usuario.ToUpper();


             

                //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
                //if (ddlPuntoLlegada.SelectedValue.ToString() == "2") //pp
                //{
                //    string xcorreo = "mzarate@sise.com.pe";
                //    string xcorreo2 = "soportepp@sise.com.pe";
                //    //string xcorreo3 = "mlugo@sise.com.pe";
                //    //string xcorreo4 = "jnunez@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    //mail.To.Add(xcorreo3);
                //    //mail.To.Add(xcorreo4);
                //}
                //if (ddlPuntoLlegada.SelectedValue == "1") //"San Miguel"
                //{
                //    string xcorreo = "mzarate@sise.com.pe";
                //    string xcorreo2 = "mstucchi@sise.com.pe";
                //    //string xcorreo3 = "rsilva@sise.com.pe";
                //    //string xcorreo4 = "jnunez@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    //mail.To.Add(xcorreo3);
                //    //mail.To.Add(xcorreo4);
                //}
                //if (ddlPuntoLlegada.SelectedValue == "4")//comas
                //{
                //    string xcorreo = "mzarate@sise.com.pe";
                //    string xcorreo2 = "soporteun@sise.com.pe";
                //    string xcorreo3 = "imarin@sise.com.pe";
                //    //string xcorreo4 = "jnunez@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    mail.To.Add(xcorreo3);
                //    //mail.To.Add(xcorreo4);
                //}
                //if (ddlPuntoLlegada.SelectedValue == "3")//tupac
                //{
                //    string xcorreo = "mzarate@sise.com.pe";
                //    string xcorreo2 = "soporteun@sise.com.pe";
                //    string xcorreo3 = "imarin@sise.com.pe";
                //    //string xcorreo4 = "jnunez@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    mail.To.Add(xcorreo3);
                //    //mail.To.Add(xcorreo4);
                //}
                //if (ddlPuntoLlegada.SelectedValue == "5")//ventanilla
                //{
                //    string xcorreo = "mzarate@sise.com.pe";
                //    string xcorreo2 = "soportepp@sise.com.pe";
                //    string xcorreo3 = "mlugo@sise.com.pe";
                //    //string xcorreo4 = "jnunez@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    mail.To.Add(xcorreo2);
                //    mail.To.Add(xcorreo3);
                //    //mail.To.Add(xcorreo4);
                //}
                //if (ddlPuntoLlegada.SelectedValue == "6")//otros 6
                //{
                //    string xcorreo = "mzarate.com.pe";
                //    //string xcorreo2 = "jnunez@sise.com.pe";
                //    string xcorreo3 = "esandoval@sise.com.pe";
                //    mail.To.Add(xcorreo);
                //    //mail.To.Add(xcorreo2);
                //    mail.To.Add(xcorreo3);
                //}
                //// INI prueba
                //mail.Attachments.Add(new Attachment(_dir + @"\" + archiv.ToString()));
                //// FIN prueba 

                ////Configuracion del SMTP
                //SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
                //                       //Especificamos las credenciales con las que enviaremos el mail
                //SmtpServer.Credentials = new System.Net.NetworkCredential("guiaactivos@sise.com.pe", "Logistica");
                //SmtpServer.EnableSsl = true;
                //SmtpServer.Send(mail);
                //this.Page.Response.Write("<script language ='JavaScript'>window.alert('<<Correo Enviado Correctamente>>');</script>");
                ////fin de la enviar correo
                btnImprimir.Enabled = false;
            }
            catch (Exception )
            {
                imprimirguia(_dir, Usuario);
            }
        }
        private void mostrarError(Exception ex)
        {
            if (sede == "")
            {
                Response.Redirect("https://sistemas.sise.com.pe/sistemas/");
            }
            else
            {
                dvError.InnerHtml = ex.Message;
                dvError.Visible = true;
            }
        }
        protected void btnRuc_Click(object sender, ImageClickEventArgs e)
        {
            FacturaN objFac = new FacturaN();

            try
            {
                btnRuc.Enabled = false;
                Transportista.Text = "";
                txtllegada.Text = "";
                txtDireccion.Text = "";
                Transportista.Attributes.Add("readonly", "true");
                txtllegada.Attributes.Add("readonly", "true");
                txtDireccion.Attributes.Add("readonly", "true");

                string ruc = txtRuc.Text.Trim();

                if (ruc == "")
                {
                    throw new Exception("Por favor ingrese el RUC a consultar");
                }
                else
                {
                    if (ruc.Length != 11) throw new Exception("El RUC ingresado no es válido. Debe ser de 11 dígitos");
                }

                // verificar si existe en la tabla tbSunat
                FacturaEn empresa = new FacturaEn();
                empresa = objFac.fun_buscar_proveedor_xRuc(ruc);

                if (empresa.razonSocial != null)
                {
                    Transportista.Text = empresa.razonSocial;
                    txtllegada.Text = empresa.razonSocial;
                    txtDireccion.Text = empresa.direccion;
                    if (empresa.direccion == "" || empresa.direccion == "-") txtDireccion.Attributes.Remove("readonly");
                }
                else
                {
                    //Mi api token personal sunat Jholy
                    string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjRhMjc5YzM1YWIzN2FiYjUyYjVkZWIyYTcwYmY3ZjAyODZkMTRiYzVhZjNjNWI2Mzk2YmFlN2Y5NWQ4ZGE5ODBjN2Q1NzYzNzAxMDliN2ZlIn0.eyJhdWQiOiIxIiwianRpIjoiNGEyNzljMzVhYjM3YWJiNTJiNWRlYjJhNzBiZjdmMDI4NmQxNGJjNWFmM2M1YjYzOTZiYWU3Zjk1ZDhkYTk4MGM3ZDU3NjM3MDEwOWI3ZmUiLCJpYXQiOjE1NTM1MzE5ODQsIm5iZiI6MTU1MzUzMTk4NCwiZXhwIjoxNTg1MTU0Mzg0LCJzdWIiOiIyNzA1Iiwic2NvcGVzIjpbInVzZS1zdW5hdCJdfQ.dfvZ-onl8Hn6m-LjnQ8qVwFBe0Ii8k1w8qdKgq7vbL4UdGyziMyL5bWVgZ7anXEeyxlJEV1-Q6m_kRA7SJSD2S0j2VBtTyyAdSDuNefEi-CnS-b5aqbmRDp1bBBlsfQdav5EZyHDUkP1xVgeX_0bjgQhCWB72nOmqG7FUZqGJWvpPE1E2g8rY4leLcZeQC4ULsKNVZUuOTIq_wvJUOHu9FxHEM5p2R3dXWTOHDJCV4GRCFhyMrenA7SV40BcfmZiT_3hAf4FEKf8M-FXWxWa-p4Ry5BBYCBuoy4VdO7ADpoTvV-_TEdgV4giREjuTzDBvx6mANy2Rc-MHfElrr4ApgvdeYTgK2dUOSr1hmQ5O1MMgCVHla8QhV2LDwwE9zML-KVXHUkSmmCzKMC8dBXex271nhLrN9cZ55Kf8OZ3p78iwpsiLt-B-a8IWszOyIbi27TkbUCPDL8OygAo3rsS-ST2Os8bsmcPxBQDEuzXMs0myTEKAkO-LFP40V1JK-CRp-6d5AoCgbWj1aSOiBx6ECKrd4T0TeTBdrFRQnL37DZNgcm6puMb5l2YVyKoRYEYJL3c8U8HAMU99XYitKQEQHaQ03bPMbrhwmnFLWuWGpZN9ujm3EDmJjrlEZHBHg5NoX8-dMhYxlqgAUTEbq_EkJ29ZPnR9Tx5Gh-vfhar4Xk";

                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    var client = new RestClient("https://consulta.pe/");

                    var request = new RestRequest("api/sunat/query/ruc ", Method.POST);
                    request.AddParameter("ruc", ruc);

                    // agregando Headers
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Authorization", "Bearer " + token);

                    // ejecutando el request
                    IRestResponse response = client.Execute(request);
                    var content = response.Content; // raw content as string
                    Tecactus.Api.Sunat.Company emp = new Tecactus.Api.Sunat.Company();

                    try
                    {
                        emp = JsonConvert.DeserializeObject<Tecactus.Api.Sunat.Company>(content);
                    }
                    catch (Exception)
                    {
                        string error = JsonConvert.DeserializeObject<string>(content);
                        Transportista.Attributes.Remove("readonly");
                        txtDireccion.Attributes.Remove("readonly");
                        throw new Exception(error);
                    }

                    if (emp.estado_contribuyente != "ACTIVO")
                    {
                        txtRuc.Focus();
                        txtRuc.BackColor = System.Drawing.Color.Yellow;
                        throw new Exception("Por favor, ingrese número de RUC válido");
                    }

                    Transportista.Text = emp.razon_social;

                    if (emp.direccion == "" || emp.direccion == "-")
                        txtDireccion.Attributes.Remove("readonly");
                    else
                        txtDireccion.Text = emp.direccion;

                    // registrar empresa en la tabla tbSunat y la consulta en la tabla auditoria
                    empresa.ruc = ruc;
                    empresa.razonSocial = emp.razon_social;
                    empresa.direccion = txtDireccion.Text.Trim();
                    empresa.usuReg = Session["usuario"].ToString();
                    empresa.pc = Request.UserHostAddress;

                    objFac.pr_registrar_empresa_api(empresa);
                    //objFac.pr_registrar_consulta_api(sede, Session["usuario"].ToString(), "ListarLogistica.aspx", ruc, Session["ip"].ToString());
                }
                txtRuc.Enabled = true;
                txtRuc.BackColor = System.Drawing.Color.White;
                btnRuc.Enabled = true;
                btnRuc.Enabled = true;
                dvError.Visible = false;
            }
            catch (Exception ex)
            {
                btnRuc.Enabled = true;
                //objFac.pr_registrar_consulta_api(sede, Session["usuario"].ToString(), "ListarLogistica.aspx", txtRuc.Text, Session["ip"].ToString());
                mostrarError(ex);

                dvError.InnerHtml = ex.Message;
                dvError.Visible = true;
            }
        }

        protected void ddlMotivoTrasldo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMotivoTrasldo.SelectedIndex == 14)

            {
                txtRuc.Visible = true;
                ruc.Visible = true;
                btnRuc.Visible = true;
                txtRuc.Enabled = true;
                txtRuc.Text = "";               
                txtRuc.Focus();
                ddlPuntoLlegada.SelectedIndex = 4;
                ddlPuntoLlegada.Enabled = false;

            }
            else
            {
                txtRuc.Visible = false;
                ruc.Visible = false;
                btnRuc.Visible = false;
                ddlPuntoLlegada.SelectedIndex = 0;
                ddlPuntoLlegada.Enabled = true;
                Transportista.Text = "";
                txtllegada.Text = "";
                txtRuc.Visible = false;
            }




        }
    }
}
