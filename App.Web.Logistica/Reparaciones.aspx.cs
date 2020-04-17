using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestSharp;
using System.Net;
using Tecactus;
using Newtonsoft.Json;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

namespace App.Web.Logistica
{
    public partial class Reparaciones : System.Web.UI.Page
    {
        public string sede { get; set; }  
        public int xidGuia { get; set; }
        public DataTable dtDatos { get; set; }

        GuiaCabeceraN objGuia = new GuiaCabeceraN();

        protected void Page_Load(object sender, EventArgs e)
        {          
            try
            {
                if (!IsPostBack)
                {
                    xidGuia = int.Parse(Request.QueryString["c"]);
                    ViewState.Add("xidGuia", xidGuia);
                    lblGuia.Text = xidGuia.ToString();
                    txtUsuario.Text = Session["Usuario"].ToString().ToUpper();
                    txtFecha.Text = DateTime.Now.ToString();

                    CargarDatos();
                    //columnasGrid();
                }
                else
                {
                    xidGuia = (int)ViewState["xidGuia"];
                }              
            }
            catch (Exception ex)
            {
                //Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
                Response.Write(ex.Message);
            }
        }

        private void CargarDatos()
        {
            GuiaCabeceraEn guia = new GuiaCabeceraEn();
            ReportesN objGuiaDetalle = new ReportesN();

            guia = objGuia.fun_Traer_datos_guia(xidGuia);

            DateTime fecha = DateTime.Now;
            txtFechaFin.Text = (fecha.ToString("yyyy-MM-dd"));

            txtFechaEnvio.Text = guia.Fecha.ToString("yyyy-MM-dd");
            txtProveedor.Text = guia.Transportista;
                        
            DataTable DTDetalleGuia = objGuiaDetalle.ReporteGuiaDetalle(xidGuia, 1);
            gvRepara.DataSource = DTDetalleGuia;
            gvRepara.DataBind();
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

        //protected void btnRuc_Click(object sender, ImageClickEventArgs e)
        //{
        //    FacturaN objFac = new FacturaN();

        //    try
        //    {
        //        btnRuc.Enabled = false;
        //        txtProveedor.Text = "";
        //        //txtDireccion.Text = "";
        //        txtProveedor.Attributes.Add("readonly", "true");
        //       // txtDireccion.Attributes.Add("readonly", "true");

        //        string ruc = txtRuc.Text.Trim();

        //        if (ruc == "")
        //        {
        //            throw new Exception("Por favor ingrese el RUC a consultar");
        //        }
        //        else
        //        {
        //            if (ruc.Length != 11) throw new Exception("El RUC ingresado no es válido. Debe ser de 11 dígitos");
        //        }

        //        // verificar si existe en la tabla tbSunat
        //        FacturaEn empresa = new FacturaEn();
        //        empresa = objFac.fun_buscar_proveedor_xRuc(ruc);

        //        if (empresa.razonSocial != null)
        //        {
        //            txtProveedor.Text = empresa.razonSocial;
        //            //txtDireccion.Text = empresa.direccion;
        //           // if (empresa.direccion == "" || empresa.direccion == "-") txtDireccion.Attributes.Remove("readonly");
        //        }
        //        else
        //        {
        //            //Mi api token personal sunat Jholy
        //            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjRhMjc5YzM1YWIzN2FiYjUyYjVkZWIyYTcwYmY3ZjAyODZkMTRiYzVhZjNjNWI2Mzk2YmFlN2Y5NWQ4ZGE5ODBjN2Q1NzYzNzAxMDliN2ZlIn0.eyJhdWQiOiIxIiwianRpIjoiNGEyNzljMzVhYjM3YWJiNTJiNWRlYjJhNzBiZjdmMDI4NmQxNGJjNWFmM2M1YjYzOTZiYWU3Zjk1ZDhkYTk4MGM3ZDU3NjM3MDEwOWI3ZmUiLCJpYXQiOjE1NTM1MzE5ODQsIm5iZiI6MTU1MzUzMTk4NCwiZXhwIjoxNTg1MTU0Mzg0LCJzdWIiOiIyNzA1Iiwic2NvcGVzIjpbInVzZS1zdW5hdCJdfQ.dfvZ-onl8Hn6m-LjnQ8qVwFBe0Ii8k1w8qdKgq7vbL4UdGyziMyL5bWVgZ7anXEeyxlJEV1-Q6m_kRA7SJSD2S0j2VBtTyyAdSDuNefEi-CnS-b5aqbmRDp1bBBlsfQdav5EZyHDUkP1xVgeX_0bjgQhCWB72nOmqG7FUZqGJWvpPE1E2g8rY4leLcZeQC4ULsKNVZUuOTIq_wvJUOHu9FxHEM5p2R3dXWTOHDJCV4GRCFhyMrenA7SV40BcfmZiT_3hAf4FEKf8M-FXWxWa-p4Ry5BBYCBuoy4VdO7ADpoTvV-_TEdgV4giREjuTzDBvx6mANy2Rc-MHfElrr4ApgvdeYTgK2dUOSr1hmQ5O1MMgCVHla8QhV2LDwwE9zML-KVXHUkSmmCzKMC8dBXex271nhLrN9cZ55Kf8OZ3p78iwpsiLt-B-a8IWszOyIbi27TkbUCPDL8OygAo3rsS-ST2Os8bsmcPxBQDEuzXMs0myTEKAkO-LFP40V1JK-CRp-6d5AoCgbWj1aSOiBx6ECKrd4T0TeTBdrFRQnL37DZNgcm6puMb5l2YVyKoRYEYJL3c8U8HAMU99XYitKQEQHaQ03bPMbrhwmnFLWuWGpZN9ujm3EDmJjrlEZHBHg5NoX8-dMhYxlqgAUTEbq_EkJ29ZPnR9Tx5Gh-vfhar4Xk";

        //            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //            var client = new RestClient("https://consulta.pe/");

        //            var request = new RestRequest("api/sunat/query/ruc ", Method.POST);
        //            request.AddParameter("ruc", ruc);

        //            // agregando Headers
        //            request.AddHeader("Accept", "application/json");
        //            request.AddHeader("Authorization", "Bearer " + token);

        //            // ejecutando el request
        //            IRestResponse response = client.Execute(request);
        //            var content = response.Content; // raw content as string
        //            Tecactus.Api.Sunat.Company emp = new Tecactus.Api.Sunat.Company();

        //            try
        //            {
        //                emp = JsonConvert.DeserializeObject<Tecactus.Api.Sunat.Company>(content);
        //            }
        //            catch (Exception)
        //            {
        //                string error = JsonConvert.DeserializeObject<string>(content);
        //                txtProveedor.Attributes.Remove("readonly");
        //                //txtDireccion.Attributes.Remove("readonly");
        //                throw new Exception(error);
        //            }

        //            if (emp.estado_contribuyente != "ACTIVO")
        //            {
        //                throw new Exception("El estado del contribuyente es inactivo ante la SUNAT");
        //            }

        //            txtProveedor.Text = emp.razon_social;

        //            //if (emp.direccion == "" || emp.direccion == "-")
        //            //    txtDireccion.Attributes.Remove("readonly");
        //            //else
        //            //    txtDireccion.Text = emp.direccion;

        //            // registrar empresa en la tabla tbSunat y la consulta en la tabla auditoria
        //            empresa.ruc = ruc;
        //            empresa.razonSocial = emp.razon_social;
        //            //empresa.direccion = txtDireccion.Text.Trim();
        //            empresa.usuReg = Session["usuario"].ToString();
        //            empresa.pc = Request.UserHostAddress;

        //            objFac.pr_registrar_empresa_api(empresa);
        //            //objFac.pr_registrar_consulta_api(sede, Session["usuario"].ToString(), "ListarLogistica.aspx", ruc, Session["ip"].ToString());
        //        }
        //        txtRuc.Enabled = false;
        //        btnRuc.Enabled = true;
        //        dvError.Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        btnRuc.Enabled = true;
        //        //objFac.pr_registrar_consulta_api(sede, Session["usuario"].ToString(), "ListarLogistica.aspx", txtRuc.Text, Session["ip"].ToString());
        //        mostrarError(ex);

        //        dvError.InnerHtml = ex.Message;
        //        dvError.Visible = true;
        //    }
        //}

        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                validarFechas(txtFecReparacion);
                validar_campos();

                transacciones objTrans = new transacciones();
                ReparacionEn rep = new ReparacionEn();
                List<ReparacionDetEn> detalle = new List<ReparacionDetEn>();
                int x = 0;
                double total = 0.00;

                foreach (GridViewRow row in gvRepara.Rows)
                {
                    ReparacionDetEn det = new ReparacionDetEn();
                    TextBox txtCosto, txtMotivo, txtTrabajo = new TextBox();
                    txtCosto = (TextBox)gvRepara.Rows[x].FindControl("txtCosto");
                    txtMotivo = (TextBox)gvRepara.Rows[x].FindControl("txtMotivo");
                    txtTrabajo = (TextBox)gvRepara.Rows[x].FindControl("txtTrabajo");
                    
                    // validar
                    if (String.IsNullOrWhiteSpace(txtCosto.Text)) throw new Exception("Ingrese Costo");
                    if (String.IsNullOrWhiteSpace(txtMotivo.Text)) throw new Exception("Ingrese Motivo");
                    if (String.IsNullOrWhiteSpace(txtTrabajo.Text)) throw new Exception("Ingrese Trabajo Realizado");
                    // -----------------------------------------------------

                    det.codigo = row.Cells[0].Text;
                    det.nroFactura = txtNroFactura.Text;
                    det.fecReparacion = DateTime.Parse(txtFecReparacion.Text);
                    det.tiempoGar = int.Parse(txtTiempo.Text);
                    det.fecFinGar = DateTime.Parse(txtFechaFin.Text);
                    det.transportista = txtProveedor.Text;
                    det.costo = double.Parse(txtCosto.Text);
                    det.motivo = txtMotivo.Text;
                    det.trabRealizado = txtTrabajo.Text;

                    detalle.Add(det);
                    total += det.costo;
                    x++;
                }

                // cabecera
                txtTotal.Text = total.ToString();
                rep.idGuia = xidGuia;
                rep.total = total;
                rep.observ = txtObservacion.Text.Trim();

                // datos guia
                GuiaCabeceraEn guia = new GuiaCabeceraEn();
                guia.IdGuia = xidGuia;
                guia.IdLoginRecibido = int.Parse(Session["rpta"].ToString());
                guia.FechaRecepcion = DateTime.Parse(txtFecha.Text);
                guia.ip = Request.UserHostAddress;

                if (objTrans.fun_registrar_reparacion(rep, detalle, guia))
                {
                    //Response.Write("listo");
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Se registró la Recepción del Activo'); </script>");

                    BtnRegistrar.Enabled = false;
                    BtnImprimir.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }

        protected void BtnImprimir_Click(object sender, EventArgs e)
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
            PdfPCell clSede1 = new PdfPCell(new Phrase("Sede San Miguel: Av. La Marina 1429 - San Miguel - Lima - Lima / Telefonos: 263-0269 / 500-4600", Formato));
            clSede1.BorderWidth = 0;
            clSede1.Colspan = 2;
            clSede1.HorizontalAlignment = Element.ALIGN_CENTER;
            tblcabecera.AddCell(clSede1);


            //FILA5
            PdfPCell clSede2 = new PdfPCell(new Phrase("Sede Comas: Av. Universitaria 1250 - Comas - Lima - Lima / Telefonos: 537-6090", Formato));
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

                ReportesN objGuiaDetalle = new ReportesN();//**//
                DataTable DTDetalle = objGuiaDetalle.ReporteGuiaDetalle(xidGuia, 1);
                gvDetalle.DataSource = DTDetalle;
                gvDetalle.DataBind();

                for (int i = 0; i < gvRepara.Rows.Count; i++)
                {
                    ////clNro = new PdfPCell(new Phrase(Convert.ToString(i + 1), Formato));
                    ////clNro.HorizontalAlignment = Element.ALIGN_CENTER;
                    ////clNro.BorderWidth = 1;

                    clCodigo = new PdfPCell(new Phrase(gvRepara.Rows[i].Cells[0].Text, Formato));
                    clCodigo.BorderWidth = 1;
                    clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;

                    clDescripcion = new PdfPCell(new Phrase(gvRepara.Rows[i].Cells[1].Text, Formato));
                    clDescripcion.BorderWidth = 1;
                    clDescripcion.HorizontalAlignment = Element.ALIGN_CENTER;

                    clOrigen = new PdfPCell(new Phrase(gvDetalle.Rows[i].Cells[3].Text, Formato));///***///
                    clOrigen.BorderWidth = 1;
                    clOrigen.HorizontalAlignment = Element.ALIGN_CENTER;

                    //clOrigen = new PdfPCell(new Phrase(gvRepara.Rows[i].Cells[3].Text, Formato));
                    //clOrigen.BorderWidth = 1;
                    //clOrigen.HorizontalAlignment = Element.ALIGN_CENTER;

                    clDetalle = new PdfPCell(new Phrase(gvRepara.Rows[i].Cells[2].Text, Formato));
                    clDetalle.BorderWidth = 1;
                    clDetalle.HorizontalAlignment = Element.ALIGN_CENTER;                    

                    clCondicion = new PdfPCell(new Phrase(gvDetalle.Rows[i].Cells[5].Text, Formato));//**//
                    clCondicion.BorderWidth = 1;
                    clCondicion.HorizontalAlignment = Element.ALIGN_CENTER;

                    clUnidadMedida = new PdfPCell(new Phrase(gvDetalle.Rows[i].Cells[6].Text, Formato));//**//
                    clUnidadMedida.BorderWidth = 1;
                    clUnidadMedida.HorizontalAlignment = Element.ALIGN_CENTER;

                    //clCondicion = new PdfPCell(new Phrase(gvRepara.Rows[i].Cells[5].Text, Formato));
                    //clCondicion.BorderWidth = 1;
                    //clCondicion.HorizontalAlignment = Element.ALIGN_CENTER;

                    //clUnidadMedida = new PdfPCell(new Phrase(gvRepara.Rows[i].Cells[6].Text, Formato));
                    //clUnidadMedida.BorderWidth = 1;
                    //clUnidadMedida.HorizontalAlignment = Element.ALIGN_CENTER;

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

                for (int i = 0; i < gvRepara.Rows.Count; i++)
                {
                    ////clNro = new PdfPCell(new Phrase(Convert.ToString(i + 1), Formato));
                    ////clNro.HorizontalAlignment = Element.ALIGN_CENTER;
                    ////clNro.BorderWidth = 1;

                    clCant = new PdfPCell(new Phrase(gvRepara.Rows[i].Cells[0].Text.ToString(), Formato));
                    clCant.BorderWidth = 1;
                    clCant.HorizontalAlignment = Element.ALIGN_CENTER;

                    clDescrip = new PdfPCell(new Phrase(gvRepara.Rows[i].Cells[1].Text, Formato));
                    clDescrip.BorderWidth = 1;
                    clDescrip.HorizontalAlignment = Element.ALIGN_CENTER;

                    clUnidadMe = new PdfPCell(new Phrase(gvRepara.Rows[i].Cells[2].Text, Formato));
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

        protected void txtTiempo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                validarFechas(txtFecReparacion);

                DateTime fechaRep = Convert.ToDateTime(txtFecReparacion.Text);
                DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

                if (txtTiempo.Text.Length < 0)
                {
                    txtTiempo.Text = "0";
                    txtFechaFin.Text = txtFecReparacion.Text;
                }
                else
                {
                    fechaFin = fechaRep.AddMonths(Convert.ToInt16(txtTiempo.Text));
                    txtFechaFin.Text = fechaFin.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void validarFechas(TextBox txt)
        {
            try
            {
                DateTime fecha = Convert.ToDateTime(txt.Text);
                int anio = fecha.Year;

                if (anio.ToString().Length != 4)
                {
                    throw new Exception("Error");                   
                }
                else
                {
                    txt.BackColor = System.Drawing.Color.White;
                    dvError.Visible = false;
                }
            }
            catch (Exception)
            {
                txt.Focus();
                txt.BackColor = System.Drawing.Color.Yellow;
                throw new Exception("Ingrese Fecha de Reparación");
            }
        }
        void validar_campos()
        {
            if (String.IsNullOrEmpty(txtTiempo.Text))
            {
                txtTiempo.Focus();
                throw new Exception("Ingrese Tiempo de Garantía en Meses");
            }

            if (String.IsNullOrEmpty(txtNroFactura.Text))
            {
                txtNroFactura.Focus();
                throw new Exception("Ingrese Nro de Comprobante");
            }                        
        }
      
    }
}