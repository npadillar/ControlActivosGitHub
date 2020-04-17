using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;
using System.IO;
using iTextSharp.text.html.simpleparser;
using RestSharp;
using System.Net;
using Tecactus;
using Newtonsoft.Json;

namespace App.Web.Logistica
{
    public partial class ListarLogistica : System.Web.UI.Page
    {
        SedeN ObjSede = new SedeN();
        public DataTable dt { get; set; }
        public string Proveedor { get; set; }
        public string sede { get; set; }

        FacturaN objFact = new FacturaN();

        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        LogisticaN objLogistica = new LogisticaN();
        ModificarLogisticaN objModLog = new ModificarLogisticaN();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    CategoriaN objN = new CategoriaN();
                    CondicionN ObjCon = new CondicionN();

                    if (!String.IsNullOrEmpty(this.Request.QueryString["cod"]))
                    {
                        var cod = this.Request.QueryString["cod"];
                        TxtCodigo.Text = cod;
                    }

                    //if (Session["Perfil"] == null)
                    //{
                    //    return;
                    //}

                    //if (int.Parse(Session["Perfil"].ToString()) == 3) // si es perfil vigilancia 
                    //{
                    //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('<<Acceso Denegado>>');</script>");
                    //    Response.Redirect("~/login.aspx");

                    //}                   

                    // DateTime fecha = DateTime.Now;


                        //TxtFecha.Text = fecha.ToShortDateString();
                        //Listar Categoria
                        DDLCategoria.DataTextField = "Descripcion";
                    DDLCategoria.DataValueField = "IdCategoria";
                    DDLCategoria.DataSource = objN.ListarCategoria();
                    DDLCategoria.DataBind();
                    DDLCategoria.Items.Insert(0, " ----------------- Seleccione ----------------- ");
                    //Listar Sede
                    DDLSede.DataTextField = "Descripcion";
                    DDLSede.DataValueField = "IdSede";
                    DDLSede.DataSource = ObjSede.ListarSede("partida");
                    DDLSede.DataBind();
                    DDLSede.Items.Insert(0, " ----------------- Seleccione ----------------- ");
                    TxtCodigo.Focus();
                    //Listar Condicion
                    DDLCondicion.DataTextField = "Descripcion";
                    DDLCondicion.DataValueField = "IdCondicion";
                    DDLCondicion.DataSource = ObjCon.ListarCondicion();
                    DDLCondicion.DataBind();
                    //DDLCondicion.Items.Insert(0, " ----------------- Seleccione ----------------- ");
                    Bloquear();

                    columnasGrid();
                }
                else
                {
                    dt = (DataTable)ViewState["dt"];
                }
            }
            catch (Exception ex)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }

        private void columnasGrid()
        {
            dt = new DataTable();
            dt.Columns.Clear();
            dt.Rows.Clear();

            dt.Columns.Add("codigo");
            dt.Columns.Add("descrip");
            dt.Columns.Add("area");
            dt.Columns.Add("edificio");
            dt.Columns.Add("aula");
            dt.Columns.Add("modelo");
            dt.Columns.Add("ruc");
            dt.Columns.Add("proveedor");
            dt.Columns.Add("nroFact");
            dt.Columns.Add("idSede");
            dt.Columns.Add("sede");
            dt.Columns.Add("piso");
            dt.Columns.Add("idCategoria");
            dt.Columns.Add("categoria");
            dt.Columns.Add("serie");
            dt.Columns.Add("marca");
            dt.Columns.Add("fecCompra");
            dt.Columns.Add("fecFinGar");
            dt.Columns.Add("observ");
            dt.Columns.Add("usuAsignado");
            ViewState.Add("dt", dt);
        }

        protected void BtnLogistica_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }

        //private void validarRUC()
        //{
        //    if(txtRuc.Text.Length < 11)
        //    {
        //        txtRuc.Focus();
        //        txtRuc.BackColor = System.Drawing.Color.Yellow;
        //        throw new Exception("ERROR: El RUC debe ser de 11 dígitos");
        //    }
        //}

        protected void btnRuc_Click(object sender, ImageClickEventArgs e)
        {
            FacturaN objFac = new FacturaN();

            try
            {
                btnRuc.Enabled = false;
                txtProveedor.Text = "";
                txtDireccion.Text = "";
                txtProveedor.Attributes.Add("readonly", "true");
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
                    txtProveedor.Text = empresa.razonSocial;
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
                        txtProveedor.Attributes.Remove("readonly");
                        txtDireccion.Attributes.Remove("readonly");
                        throw new Exception(error);
                    }

                    if (emp.estado_contribuyente != "ACTIVO")
                    {
                        throw new Exception("El estado del contribuyente es inactivo ante la SUNAT");
                    }

                    txtProveedor.Text = emp.razon_social;

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
                txtRuc.Enabled = false;
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

        public void Bloquear()
        {
            TxtCod.Enabled = false;
            TxtDescripcion.Enabled = false;
            TxtArea.Enabled = false;
            txtEdificio.Enabled = false;
            txtAula.Enabled = false;
            DDLCondicion.Enabled = false;
            txtModelo.Enabled = false;
            txtRuc.Enabled = false;
            txtNroFactura.Enabled = false;
            DDLSede.Enabled = false;
            TxtPiso.Enabled = false;
            DDLCategoria.Enabled = false;
            txtSerie.Enabled = false;
            txtMarca.Enabled = false;
            txtFechaCompra.Enabled = false;
            txtObservacion.Enabled = false;
            txtUsu.Enabled = false;
            txtProveedor.Enabled = false;
            txtTiempo.Enabled = false;
        }

        public void Habilitar()
        {
            TxtCod.Enabled = true;
            TxtDescripcion.Enabled = true;
            TxtArea.Enabled = true;
            txtEdificio.Enabled = true;
            txtAula.Enabled = true;
            DDLCondicion.Enabled = false;
           // txtRuc.Enabled = true;
            //txtNroFactura.Enabled = true;
            DDLSede.Enabled = true;
            TxtPiso.Enabled = true;
            DDLCategoria.Enabled = true;
            //txtFechaCompra.Enabled = true;  ESTE NO LO DESACTIVE
            txtObservacion.Enabled = true;
            txtUsu.Enabled = true; // // // 
            txtProveedor.Enabled = true;
            //rbMeses.Enabled = true;
            //rbAnio.Enabled = true;
            txtTiempo.Enabled = true;
        }

        public void limpiar()
        {
            TxtCodigo.Text = string.Empty;
            TxtCod.Text = String.Empty;
            //TxtId.Text = String.Empty;
            hdId.Value = String.Empty;
            TxtDescripcion.Text = String.Empty;
            DDLSede.SelectedIndex = 0;
            TxtArea.Text = String.Empty;
            TxtPiso.Text = String.Empty;
            txtEdificio.Text = String.Empty;
            DDLCategoria.SelectedIndex = 0;
            //adicionar
            txtAula.Text = string.Empty;
            txtSerie.Text = string.Empty;
            //txtNumero.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            //DDLCondicion.SelectedIndex = 0;
            txtRuc.Text = string.Empty;
            //txtFechaCompra.Text = string.Empty; ESTE TAMPOCO
            txtNroFactura.Text = string.Empty;
            txtObservacion.Text = string.Empty;
            txtUsu.Text = string.Empty; // // //
            txtTiempo.Text = string.Empty;
            txtProveedor.Text = string.Empty;
            DDLCondicion.Enabled = false;
            TxtCod.Focus();
            ckbMasivo.Visible = true;

            dt.Rows.Clear();
            dgvListado.DataSource = dt;
            dgvListado.DataBind();
            dvError.Visible = false;
        }

        public void Validar()
        {
            if (String.IsNullOrEmpty(TxtCod.Text))
            {
                //lblcodigo.Text = "*Ingrese Código";
                TxtCod.Focus();
                //lblcodigo.Visible = true;
                return;
            }

        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            //Habilitar();
        }

        protected void BtnNuevo_Click1(object sender, ImageClickEventArgs e)
        {
            //txtFechaCompra.Text = DateTime.Parse("2017-01-31").ToString("yyyy-MM-dd"); //último cambio 
            //txtFechaFin.Text = DateTime.Parse("2017-01-31").ToString("yyyy-MM-dd");
            DateTime fecha = DateTime.Now;
            txtFechaCompra.Text = (fecha.ToString("yyyy-MM-dd"));
            txtFechaFin.Text = (fecha.ToString("yyyy-MM-dd"));

            //txtFechaFin.Text = (fecha.ToString("yyyy-MM-dd"));
            //txtTiempo.Text = "0";

            limpiar();
            BtnGraba.Enabled = true;
            lblgrabar.Enabled = true;
            txtRuc.Enabled = true;
            txtFechaCompra.Enabled = true; //no estoy segura ver
            txtTiempo.Enabled = true;
            txtNroFactura.Enabled = true;
            BtnGraba.Visible = true;
            lblgrabar.Visible = true;
            BtnModificar.Visible = false;
            txtUsu.Enabled = true;
            txtModelo.Enabled = true;
            txtSerie.Enabled = true;
            txtMarca.Enabled = true;
            Habilitar();

            //Listar Sede
            DDLSede.DataTextField = "Descripcion";
            DDLSede.DataValueField = "IdSede";
            DDLSede.DataSource = ObjSede.ListarSede("mantenimiento");
            DDLSede.DataBind();
            DDLSede.Items.Insert(0, " ----------------- Seleccione ----------------- ");
        }

        protected void LnkNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            DDLCondicion.Enabled = false;
            BtnGraba.Enabled = true;
            lblgrabar.Enabled = true;
            txtRuc.Enabled = true;
            txtFechaCompra.Enabled = true;
            txtTiempo.Enabled = true;
            txtTiempo.Text = "0";
            BtnGraba.Visible = true;
            lblgrabar.Visible = true;
            BtnModificar.Visible = false;
        }

        private bool validarCampos(string tipo)
        {
            bool b = false;
    
            try
            {
                LogisticaN objBuscar = new LogisticaN();
                string xcodigo = TxtCod.Text.Trim();

                if (tipo == "insert")
                {
                    DataTable DTILogistica = objBuscar.BuscarLogistica(xcodigo);

                    if (DTILogistica.Rows.Count > 0)
                    {
                        TxtCod.Focus();
                        throw new Exception("El Codigo ya Existe");
                    }
                }
                else
                {
                    if (TxtCod.Text.Trim() != TxtCodigo.Text.Trim())
                    {
                        if (objBuscar.fun_validar_noRepetir_cod(xcodigo) > 0)
                        {
                            TxtCod.Focus();
                            throw new Exception("El Codigo ya Existe");
                        }
                    }
                }             

                if (String.IsNullOrEmpty(TxtCod.Text))
                {
                    TxtCod.Focus();
                    throw new Exception("Ingrese el código");
                }

                if (String.IsNullOrEmpty(TxtDescripcion.Text))
                {
                    TxtDescripcion.Focus();
                    throw new Exception("Ingrese Descripción");
                }
                if (DDLSede.SelectedIndex == 0)
                {
                    DDLSede.Focus();
                    throw new Exception("Seleccione Sede");
                }

                if (String.IsNullOrEmpty(TxtArea.Text))
                {
                    TxtArea.Focus();
                    throw new Exception("Ingrese Area");
                }

                if (String.IsNullOrEmpty(TxtPiso.Text))
                {
                    TxtPiso.Focus();
                    throw new Exception("Ingrese Piso");
                }

                if (String.IsNullOrEmpty(txtEdificio.Text))
                {
                    txtEdificio.Focus();
                    throw new Exception("Ingrese Edificio");
                }

                if (String.IsNullOrEmpty(txtUsu.Text))
                {
                    txtUsu.Focus();
                    throw new Exception("Asigne un Usuario");
                }

                if (DDLCategoria.SelectedIndex == 0)
                {
                    DDLCategoria.Focus();
                    throw new Exception("Seleccione Categoria");
                }


                if (tipo == "insert")
                {
                    validar_campos_ruc();
                }
                else
                {
                    if (DateTime.Parse(hdFecha.Value) > DateTime.Parse("2018-12-31")) validar_campos_ruc();
                }
                
                b = true;
                return b;
            }
            catch (Exception ex)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
                return false;
            } 
        }
        
        void validar_campos_ruc()
        {
            if (String.IsNullOrEmpty(txtRuc.Text))
            {
                txtRuc.Focus();
                throw new Exception("Ingrese RUC");
            }

            if (String.IsNullOrEmpty(txtProveedor.Text.Trim()))
            {
                txtProveedor.Focus();
                throw new Exception("Ingrese Proveedor");
            }

            if (String.IsNullOrEmpty(txtNroFactura.Text))
            {
                txtNroFactura.Focus();
                throw new Exception("Ingrese Nro Comprobante");
            }

            if (String.IsNullOrEmpty(txtFechaCompra.Text))
            {
                txtFechaCompra.Focus();
                throw new Exception("Ingrese Fecha de Compra");
            }

            if (String.IsNullOrEmpty(txtTiempo.Text))
            {
                txtTiempo.Text = "0";
                txtTiempo_TextChanged(null, null);
            }
        }

        private void registrarActivo()
        {
            if (!validarCampos("insert")) return;

            int id1 = 0;
            if (hdId.Value == "")
            {
                id1 = 1;
            }
            else
            {
                id1 = int.Parse(hdId.Value);
            }

            LogisticaEn ObjInsert = new LogisticaEn();
            FacturaEn objFac = new FacturaEn();

            ObjInsert.IdInventario = id1;
            ObjInsert.IdLogin = int.Parse(Session["rpta"].ToString());
            ObjInsert.Fecha = DateTime.Now;
            ObjInsert.Codigo = TxtCod.Text.Trim();
            ObjInsert.Descripcion = TxtDescripcion.Text.Trim();
            ObjInsert.IdSede = int.Parse(DDLSede.SelectedValue);
            ObjInsert.Area = TxtArea.Text.Trim();
            ObjInsert.Piso = TxtPiso.Text.Trim();
            ObjInsert.Edificio = txtEdificio.Text.Trim();
            ObjInsert.IdCategoria = int.Parse(DDLCategoria.SelectedValue);
            ObjInsert.ip = Request.UserHostAddress;
            //Adicionar
            ObjInsert.Aula = txtAula.Text.Trim();
            ObjInsert.Serie = txtSerie.Text.Trim();
            ObjInsert.Marca = txtMarca.Text.Trim();
            ObjInsert.Modelo = txtModelo.Text.Trim();
            ObjInsert.IdCondicion = int.Parse(DDLCondicion.SelectedValue);
            ObjInsert.Observacion = txtObservacion.Text.Trim();
            ObjInsert.UsuAsignado = txtUsu.Text.Trim();
            objFac.Ruc = txtRuc.Text;
            objFac.NumeroFactura = txtNroFactura.Text;
            objFac.Proveedor = txtProveedor.Text;
            objFac.Direccion = txtDireccion.Text;
            objFac.TiempGar = int.Parse(txtTiempo.Text);

            DateTime fechac = DateTime.Parse(txtFechaCompra.Text);
            DateTime fecFinGar = DateTime.Parse(txtFechaFin.Text);

            objFac.FechaCompra = DateTime.Parse(fechac.ToString("yyyy-MM-dd"));
            objFac.FecFinGar = DateTime.Parse(fecFinGar.ToString("yyyy-MM-dd"));

            LogisticaN objNeg = new LogisticaN();
            objNeg.InsertarLogistica(ObjInsert, objFac);

            this.Page.Response.Write("<script language ='JavaScript'>window.alert('Activo registrado correctamente.');</script>");
            limpiar();
            BtnGraba.Enabled = false;
            lblgrabar.Enabled = false;
            BtnGraba.Visible = false;
            lblgrabar.Visible = false;
            Bloquear();
        }

        private void registrarActivos_masivo()
        {
            int id1 = 0;
            if (hdId.Value == "")
            {
                id1 = 1;
            }
            else
            {
                id1 = int.Parse(hdId.Value);
            }

            List<LogisticaEn> listLogis = new List<LogisticaEn>();
            List<FacturaEn> listFact = new List<FacturaEn>();

            foreach (DataRow row in dt.Rows)
            {
                LogisticaEn logis = new LogisticaEn();
                FacturaEn fact = new FacturaEn();

                logis.IdInventario = id1;
                logis.IdLogin = int.Parse(Session["rpta"].ToString());
                logis.Fecha = DateTime.Now;
                logis.Codigo = row["codigo"].ToString();
                logis.Descripcion = row["descrip"].ToString();
                logis.IdSede = Convert.ToInt16(row["idSede"]);
                logis.Area = row["area"].ToString();
                logis.Piso = row["piso"].ToString();
                logis.Edificio = row["edificio"].ToString();
                logis.IdCategoria = Convert.ToInt16(row["idCategoria"]);
                logis.ip = Request.UserHostAddress;
                //Adicionar
                logis.Aula = row["aula"].ToString();
                logis.Serie = row["serie"].ToString();
                logis.Marca = row["marca"].ToString();
                logis.Modelo = row["modelo"].ToString();
                logis.IdCondicion = 1; // activo
                logis.Observacion = row["observ"].ToString();
                logis.UsuAsignado = row["usuAsignado"].ToString();
                fact.Ruc = row["ruc"].ToString();
                fact.NumeroFactura = row["nroFact"].ToString();
                DateTime fechac = DateTime.Parse(row["fecCompra"].ToString());
                fact.FechaCompra = DateTime.Parse(fechac.ToString("yyyy-MM-dd"));

                DateTime fecFinGar = DateTime.Parse(row["fecFinGar"].ToString());
                fact.FecFinGar = DateTime.Parse(fecFinGar.ToString("yyyy-MM-dd"));

                fact.Proveedor = txtProveedor.Text;
                fact.Direccion = txtDireccion.Text;
                fact.TiempGar = int.Parse(txtTiempo.Text);

                listLogis.Add(logis);
                listFact.Add(fact);
            }

            transacciones objTrans = new transacciones();

            if (objTrans.fun_registrar_activos_masivo(listLogis, listFact))
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Activos registrados correctamente.');</script>");
                limpiar();
                BtnGraba.Enabled = false;
                lblgrabar.Enabled = false;
                BtnGraba.Visible = false;
                lblgrabar.Visible = false;
                Bloquear();
            }
        }

        protected void BtnGraba_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (ckbMasivo.Checked)
                    registrarActivos_masivo();
                else
                    registrarActivo();
            }
            catch (Exception ex)
            {
                //this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
                Response.Write(ex.Message);
            }
        }

        protected void LnkGraba_Click(object sender, EventArgs e)
        {
            //LogisticaEn ObjInsert = new LogisticaEn();

            //if (String.IsNullOrEmpty(TxtCod.Text))
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Ingrese el código');</script>");
            //    TxtCod.Focus();

            //    return;
            //}

            //if (String.IsNullOrEmpty(TxtDescripcion.Text))
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Ingrese Descripción.');</script>");
            //    TxtDescripcion.Focus();

            //    return;
            //}
            //if (DDLSede.SelectedIndex == 0)
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Seleccione Sede.');</script>");
            //    DDLSede.Focus();
            //    return;
            //}

            //if (String.IsNullOrEmpty(TxtArea.Text))
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Ingrese Area.');</script>");
            //    TxtArea.Focus();
            //    return;
            //}

            //if (String.IsNullOrEmpty(TxtPiso.Text))
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Ingrese Piso.');</script>");
            //    TxtPiso.Focus();
            //    return;
            //}

            //if (String.IsNullOrEmpty(txtEdificio.Text))
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Ingrese Edificio.');</script>");
            //    txtEdificio.Focus();
            //    return;
            //}

            //if (DDLCategoria.SelectedIndex == 0)
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Seleccione Categoria.');</script>");
            //    DDLCategoria.Focus();
            //    return;
            //}
            ////if (String.IsNullOrEmpty(txtAula.Text))
            ////{
            ////    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Seleccione Aula.');</script>");
            ////    txtAula.Focus();
            ////    return;
            ////}
            //if (String.IsNullOrEmpty(txtMarca.Text))
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Seleccione Marca.');</script>");
            //    txtMarca.Focus();
            //    return;
            //}
            //if (DDLCondicion.SelectedIndex == 0)
            //{
            //    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Seleccione Condicion.');</script>");
            //    DDLCondicion.Focus();
            //    return;
            //}

            //int id1 = 0;
            //if (hdId.Value == "")
            //{
            //    id1 = 1;
            //}
            //else
            //{
            //    id1 = int.Parse(hdId.Value);
            //}

            //ObjInsert.IdInventario = id1;
            //////ObjInsert.IdLogin = 1; // int.Parse(lblid.Text);
            //ObjInsert.IdLogin = int.Parse(Session["rpta"].ToString());
            //ObjInsert.Fecha = DateTime.Now;   //DateTime.Parse(TxtFecha.Text);
            //ObjInsert.Codigo = TxtCod.Text;
            //ObjInsert.Descripcion = TxtDescripcion.Text;
            ////  ObjInsert.IdSede = int.Parse(TxtSede.Text);
            //ObjInsert.IdSede = int.Parse(DDLSede.SelectedValue);
            //ObjInsert.Area = TxtArea.Text;
            //ObjInsert.Piso = TxtPiso.Text;
            //ObjInsert.Edificio = txtEdificio.Text;
            //// ObjInsert.Categoria = TxtCategoria.Text;
            //ObjInsert.IdCategoria = int.Parse(DDLCategoria.SelectedValue);

            //ObjInsert.Aula = txtAula.Text;
            //ObjInsert.Serie = txtSerie.Text;
            ////ObjInsert.Numero = txtNumero.Text;
            //ObjInsert.Marca = txtMarca.Text;
            //ObjInsert.Modelo = txtModelo.Text;
            //ObjInsert.IdCondicion = int.Parse(DDLCondicion.SelectedValue);

            //LogisticaN objNeg = new LogisticaN();
            //objNeg.InsertarLogistica(ObjInsert);



            //this.Page.Response.Write("<script language ='JavaScript'>window.alert('Activo registrado correctamente');</script>");
            //limpiar();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //txtNroFactura.Enabled = true;
                LogisticaN objBuscar = new LogisticaN();
                string xcodigo;
                xcodigo = TxtCodigo.Text;
                if (String.IsNullOrEmpty(xcodigo))
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Ingrese un código.');</script>");
                    TxtCodigo.Focus();
                    return;
                }
                DataTable DTILogistica = objBuscar.BuscarLogistica(xcodigo);
                if (DTILogistica.Rows.Count <= 0)
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('No se encontró el Código.');</script>");
                    TxtCodigo.Text = string.Empty;
                    TxtCodigo.Focus();
                    return;
                }
                // enviar datos a caja de texto
                DataRow row = DTILogistica.Rows[0];

                hdIdLogistica.Value = row["idlogistica"].ToString();
                hdIdInventario.Value = row["idinventario"].ToString();
                hdFecha.Value = row["Fecha"].ToString();

                hdId.Value = row["IdInventario"].ToString();
                TxtCod.Text = row["Codigo"].ToString();
                TxtDescripcion.Text = row["Descripcion"].ToString();
                DDLSede.SelectedValue = row["IdSede"].ToString();
                TxtArea.Text = row["Area"].ToString();
                TxtPiso.Text = row["Piso"].ToString().Trim();
                txtEdificio.Text = row["Edificio"].ToString();
                DDLCategoria.SelectedValue = row["IdCategoria"].ToString();
                txtUsu.Text = row["UsuAsignado"].ToString();// // //
                DDLCondicion.SelectedValue = row["IdCondicion"].ToString();
                hdIdCondicion.Value = DDLCondicion.SelectedValue;
                hdCondicion.Value = DDLCondicion.SelectedItem.ToString();
                txtAula.Text = row["Aula"].ToString();
                txtDireccion.Text = row["direccion"].ToString();////
                txtFechaCompra.Text = row["FechaCompra"].ToString();
                txtFechaFin.Text = row["FecFinGar"].ToString();////
                txtTiempo.Text = row["TiempGar"].ToString().Trim();
                txtNroFactura.Text = row["NumeroFactura"].ToString();
                txtProveedor.Text = row["Proveedor"].ToString();

                if (!String.IsNullOrEmpty(row["Marca"].ToString().Trim()))
                {
                    txtMarca.Text = row["Marca"].ToString();
                    txtMarca.Enabled = false;
                }
                else
                {
                    txtMarca.Text = "";
                    txtMarca.Enabled = true;
                }

                if (!String.IsNullOrEmpty(row["Modelo"].ToString().Trim()))
                {
                    txtModelo.Text = row["Modelo"].ToString();
                    txtModelo.Enabled = false;
                }
                else
                {
                    txtModelo.Text = "";
                    txtModelo.Enabled = true;
                }


                if (!String.IsNullOrEmpty(row["serie"].ToString().Trim()))
                {
                    txtSerie.Text = row["serie"].ToString();
                    txtSerie.Enabled = false;
                }
                else
                {
                    txtSerie.Text = "";
                    txtSerie.Enabled = true;
                }

                if (!String.IsNullOrEmpty(row["Observacion"].ToString()))
                {
                    txtObservacion.Text = row["Observacion"].ToString();
                }

                if ((row["NumeroFactura"].ToString() == ""))
                {
                    txtNroFactura.Enabled = true;
                }
                else
                {
                    txtNroFactura.Text = row["NumeroFactura"].ToString();
                    txtNroFactura.Enabled = false;
                }

                if (row["FechaCompra"].ToString() == "")
                {
                    DateTime fechac = DateTime.Now;
                    txtFechaCompra.Text = fechac.ToString("2017-01-31");
                    txtFechaCompra.Enabled = true;
                }
                else
                {
                    DateTime fechac;
                    fechac = DateTime.Parse(row["FechaCompra"].ToString());
                    txtFechaCompra.Text = fechac.ToString("yyyy-MM-dd");

                    if (fechac == DateTime.Parse("2017-01-31")) // no tiene fecha de compra
                        txtFechaCompra.Enabled = true;
                    else
                        txtFechaCompra.Enabled = false;
                }

                if (row["FecFinGar"].ToString() == "")
                {
                    txtFechaFin.Text = txtFechaCompra.Text;
                }
                else
                {
                    DateTime fechaF;
                    fechaF = DateTime.Parse(row["FecFinGar"].ToString());
                    txtFechaFin.Text = fechaF.ToString("yyyy-MM-dd");
                }

                if(row["TiempGar"].ToString() == "0" || row["TiempGar"].ToString() == "")
                {
                    txtTiempo.Text = "0";
                    txtTiempo.Enabled = true;
                }
                else
                {
                    txtTiempo.Text = row["TiempGar"].ToString();
                    txtTiempo.Enabled = false;
                }

                if ((row["Ruc"].ToString() == ""))
                {
                    txtRuc.Enabled = true;
                }else
                {
                    txtRuc.Text = row["Ruc"].ToString();
                }

                TxtArea.Enabled = true;
                txtEdificio.Enabled = true;
                txtAula.Enabled = true;
                DDLSede.Enabled = true;
                TxtPiso.Enabled = true;
                txtUsu.Enabled = true;
                BtnModificar.Visible = true;
                BtnModificar.Enabled = true;

                BajaActivosN objBaja = new BajaActivosN();
                if (objBaja.fun_bloquear_activo_baja(xcodigo))
                {
                    BtnModificar.Visible = false;
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('El activo no se puede modificar porque fue dado de baja');</script>");
                }

                int cargo = int.Parse(Session["cargo"].ToString());
                // 24 = asistente contable | 28 = asistente de logistica | 20 = asistente de programacion | 16 = jefe de programacion | 6 = Gerente de Sistemas
                //if (cargo == 24 || cargo == 28 || cargo == 20 || cargo == 16 || cargo == 6) 
                if (cargo == 20 || cargo == 16 || cargo == 3) 
                {
                    TxtCod.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }

        //protected void LnkBuscar_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("BuscarInventario.aspx");
        //}

        //protected void LnkBuscar_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect("BuscarInventario.aspx");
        //}

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (fun_validarCampos_Guardar())
                //{
                if (!validarCampos("update")) return;
                LogisticaEn LogEn = new LogisticaEn();
                FacturaEn Fact = new FacturaEn();

                LogEn.IdLogistica = int.Parse(hdIdLogistica.Value);
                LogEn.IdInventario = int.Parse(hdIdInventario.Value);
                LogEn.IdLogin = int.Parse(Session["rpta"].ToString());
                LogEn.Fecha = DateTime.Parse(hdFecha.Value);

                LogEn.Codigo = TxtCod.Text;
                LogEn.Descripcion = TxtDescripcion.Text;
                LogEn.IdSede = int.Parse(DDLSede.SelectedValue);
                LogEn.Area = TxtArea.Text;
                //LogEn.Piso = TxtPiso.Text;
                LogEn.Piso = TxtPiso.Text.Trim();
                LogEn.Edificio = txtEdificio.Text.Trim();
                LogEn.IdCategoria = int.Parse(DDLCategoria.SelectedValue);
                LogEn.Serie = txtSerie.Text;
                LogEn.Marca = txtMarca.Text;
                LogEn.Modelo = txtModelo.Text;
                LogEn.IdCondicion = int.Parse(DDLCondicion.SelectedValue);
                Fact.Ruc = txtRuc.Text;
                Fact.Proveedor = txtProveedor.Text;
                Fact.Direccion = txtDireccion.Text;
                Fact.NumeroFactura = txtNroFactura.Text;
                Fact.FechaCompra = DateTime.Parse(txtFechaCompra.Text);
                Fact.TiempGar = int.Parse(txtTiempo.Text);
                Fact.FecFinGar = DateTime.Parse(txtFechaFin.Text);
                LogEn.Aula = txtAula.Text;
                LogEn.Observacion = txtObservacion.Text.Trim();
                LogEn.UsuAsignado = txtUsu.Text;
                LogEn.ip = Request.UserHostAddress;

                //NotaDet.Nota = int.Parse(xnota.Text);
                string cod = objModLog.InsertarModificarLog(LogEn, Fact);
                string codigo = objLogistica.ActualizarLogistica(LogEn, Fact);
                
                if (String.IsNullOrEmpty(txtUsu.Text)) // // // 
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Asigne un Usuario.');</script>");
                    txtUsu.Focus();

                    return;
                }

                this.Page.Response.Write("<script language ='JavaScript'>window.alert('Activo Actualizado correctamente.');</script>");
                //GVListar.EditIndex = -1;
                //ListarInv();
                BtnModificar.Enabled = false;
                BtnModificar.Visible = false;
                Bloquear();             
            }
            catch (Exception ex)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('"+ex.Message+"');</script>");
                return;
            }
        }

        protected void hdIdLogistica_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validarCampos("insert")) return;
                string codigo = TxtCod.Text.Trim();
                string codDt = "";

                foreach (DataRow row in dt.Rows)
                {
                    codDt = row["codigo"].ToString();

                    if (codigo == codDt)
                    {
                        throw new Exception("El activo con código: " + codigo + " ya esta registrado en la lista.");
                    }
                }

                dt.Rows.Add(codigo,
                            TxtDescripcion.Text.Trim(),
                            TxtArea.Text.Trim(),
                            txtEdificio.Text.Trim(),
                            txtAula.Text.Trim(),
                            txtModelo.Text.Trim(),
                            txtRuc.Text.Trim(),
                            txtProveedor.Text.Trim(),
                            txtNroFactura.Text.Trim(),
                            DDLSede.SelectedValue,
                            DDLSede.SelectedItem,
                            TxtPiso.Text.Trim(),
                            DDLCategoria.SelectedValue,
                            DDLCategoria.SelectedItem,
                            txtSerie.Text.Trim(),
                            txtMarca.Text.Trim(),
                            txtFechaCompra.Text,
                            txtFechaFin.Text,
                            txtObservacion.Text.Trim(),
                            txtUsu.Text.Trim());

                ViewState.Add("dt", dt);
                dgvListado.DataSource = dt;
                dgvListado.DataBind();
            }
            catch (Exception ex)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
                return;
                //Response.Write(ex.Message);
            }
        }

        protected void dgvListado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt16(e.CommandArgument);
                GridViewRow row = dgvListado.Rows[index];

                if (index != -1 && e.CommandName == "quitar")
                {
                    dt.Rows.RemoveAt(index);
                    ViewState.Add("dt", dt);
                    dgvListado.DataSource = dt;
                    dgvListado.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
                return;
            }
        }

        protected void ckbMasivo_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbMasivo.Checked)
            {
                btnAgregar.Visible = true;
                dgvListado.Visible = true;
            }
            else
            {
                btnAgregar.Visible = false;
                dgvListado.Visible = false;
            }
        }

        protected void ckbUpdMasivo_CheckedChanged(object sender, EventArgs e)
        {
            Response.Redirect("updateMasivo.aspx", true);
        }

        //protected void rbMeses_CheckedChanged(object sender, EventArgs e)
        //{
        //    DateTime fechaCompra = Convert.ToDateTime(txtFechaCompra.Text);
        //    DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

        //    fechaFin = fechaCompra.AddMonths(Convert.ToInt16(txtTiempo.Text));
        //    txtFechaFin.Text = fechaFin.ToString("yyyy-MM-dd");
        //}

        //protected void rbAnio_CheckedChanged(object sender, EventArgs e)
        //{
        //    DateTime fechaCompra = Convert.ToDateTime(txtFechaCompra.Text);
        //    DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

        //    fechaFin = fechaCompra.AddYears(Convert.ToInt16(txtTiempo.Text));
        //    txtFechaFin.Text = fechaFin.ToString("yyyy-MM-dd");
        //}

        protected void txtTiempo_TextChanged(object sender, EventArgs e)
        {
            DateTime fechaCompra = Convert.ToDateTime(txtFechaCompra.Text);
            DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

            if (txtTiempo.Text.Length < 0)
            {
                txtTiempo.Text = "0";
                txtFechaFin.Text = txtFechaCompra.Text;
            }
            else
            {           
                fechaFin = fechaCompra.AddMonths(Convert.ToInt16(txtTiempo.Text));
                txtFechaFin.Text = fechaFin.ToString("yyyy-MM-dd");
            }
        }

        //public void ListarInv()
        //{
        //    InventarioN objInventario = new InventarioN();
        //    GVListar.DataSource = objInventario.BuscaInv(txtBuscar.Text);
        //    GVListar.DataBind();
        //}

        //protected void txtBuscar_TextChanged(object sender, EventArgs e)
        //{
        //    ListarInv();
        //}

        //protected void btnBuscarCodigo_Click(object sender, ImageClickEventArgs e)
        //{
        //    ListarInv();
        //}
        //protected void GVListar_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        //{
        //    //  string xId = GVListar.Rows[e.RowIndex].Cells[1].Text;
        //    //string CodigoBuscado;
        //    hdCodigobuscado.Value = GVListar.Rows[e.NewSelectedIndex].Cells[2].Text;

        //}
    }
}