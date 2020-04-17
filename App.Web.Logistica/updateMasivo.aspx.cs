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
using RestSharp;
using Tecactus;
using Newtonsoft.Json;
using System.Net;

namespace App.Web.Logistica
{
    public partial class updateMasivo : System.Web.UI.Page
    {
        SedeN ObjSede = new SedeN();
        public DataTable dt { get; set; }
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

                    DateTime fecha = DateTime.Now;

                    //DDLCategoria.DataTextField = "Descripcion";
                    //DDLCategoria.DataValueField = "IdCategoria";
                    //DDLCategoria.DataSource = objN.ListarCategoria();
                    //DDLCategoria.DataBind();
                    //DDLCategoria.Items.Insert(0, " ----------------- Seleccione ----------------- ");
                    //Listar Sede
                    DDLSede.DataTextField = "Descripcion";
                    DDLSede.DataValueField = "IdSede";
                    DDLSede.DataSource = ObjSede.ListarSede("mantenimiento");
                    DDLSede.DataBind();
                    DDLSede.Items.Insert(0, " ----------------- Seleccione ----------------- ");
                    TxtCodigo.Focus();

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
            dt.Columns.Add("ruc");
            dt.Columns.Add("nroFact");
            dt.Columns.Add("fecCompra");
            dt.Columns.Add("observ");
            ViewState.Add("dt", dt);
        }

        protected void BtnLogistica_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Menu.aspx");
        }
        
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                LogisticaN objBuscar = new LogisticaN();
                string xcodigo;
                xcodigo = TxtCodigo.Text;
                if (String.IsNullOrEmpty(xcodigo))
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('Ingrese un código.');</script>");
                    TxtCodigo.Focus();
                    return;
                }

                foreach (DataRow rowx in dt.Rows)
                {
                    if (TxtCodigo.Text.Trim() == rowx["codigo"].ToString())
                    {
                        this.Page.Response.Write("<script language ='JavaScript'>window.alert('El item ya existe en la lista.');</script>");
                        TxtCodigo.Focus();
                        return;
                    }
                }

                DataTable DTILogistica = objBuscar.BuscarLogistica(xcodigo);
                if (DTILogistica.Rows.Count <= 0)
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('No se encontró el Código.');</script>");
                    TxtCodigo.Text = string.Empty;
                    TxtCodigo.Focus();
                    return;
                }

                BajaActivosN objBaja = new BajaActivosN();
                if (objBaja.fun_bloquear_activo_baja(xcodigo))
                {
                    this.Page.Response.Write("<script language ='JavaScript'>window.alert('El activo no se puede modificar porque fue dado de baja');</script>");
                }
                else
                {
                    DataRow row = DTILogistica.Rows[0];
                    if (dt.Rows.Count == 0)
                    {
                        // enviar datos a caja de texto
                        hdIdLogistica.Value = row["idlogistica"].ToString();
                        hdIdInventario.Value = row["idinventario"].ToString();
                        hdFecha.Value = row["Fecha"].ToString();

                        hdId.Value = row["IdInventario"].ToString();
                        DDLSede.SelectedValue = row["IdSede"].ToString();
                        TxtArea.Text = row["Area"].ToString();
                        TxtPiso.Text = row["Piso"].ToString().Trim();
                        txtEdificio.Text = row["Edificio"].ToString();
                        //DDLCategoria.SelectedValue = row["IdCategoria"].ToString();
                        txtUsu.Text = row["UsuAsignado"].ToString();
                        txtAula.Text = row["Aula"].ToString();
                        txtRuc.Text = row["Ruc"].ToString();
                        txtProveedor.Text = row["Proveedor"].ToString();////
                        txtNroFactura.Text = row["NumeroFactura"].ToString();////
                        txtFechaCompra.Text = row["FechaCompra"].ToString();////
                        txtFechaFin.Text = row["FecFinGar"].ToString();////
                        txtTiempo.Text = row["TiempGar"].ToString().Trim();////


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

                        if (row["TiempGar"].ToString() == "0" || row["TiempGar"].ToString() == "")
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
                        }
                        else
                        {
                            //txtRuc.Text= row["Ruc"].ToString();
                            txtRuc.Enabled = false;
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


                    }

                string ruc = "", nroFact = "", observ = "", fechaCompra = "";

                    if (!String.IsNullOrEmpty(row["ruc"].ToString())) ruc = row["Ruc"].ToString();
                    if (!String.IsNullOrEmpty(row["NumeroFactura"].ToString())) nroFact = row["NumeroFactura"].ToString();
                    if (!String.IsNullOrEmpty(row["FechaCompra"].ToString())) fechaCompra = row["FechaCompra"].ToString();
                    if (!String.IsNullOrEmpty(row["Observacion"].ToString())) observ = row["Observacion"].ToString();

                    dt.Rows.Add(row["Codigo"],
                                row["Descripcion"],
                                ruc,
                                nroFact,
                                fechaCompra,
                                observ);

                    ViewState.Add("dt", dt);
                    dgvListado.DataSource = dt;
                    dgvListado.DataBind();
                    BtnModificar.Enabled = true;
                    BtnModificar.Visible = true;
                    txtTiempo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
            }
        }

        protected void LnkBuscar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("BuscarInventario.aspx");
        }

        private void limpiar()
        {
            TxtCodigo.Text = "";
            TxtArea.Text = "";
            txtEdificio.Text = "";
            txtAula.Text = "";
            TxtPiso.Text = "";
            //DDLCategoria.SelectedIndex = 0;
            DDLSede.SelectedIndex = 0;
            txtUsu.Text = "";
            dt.Rows.Clear();
            dgvListado.DataSource = dt;
            dgvListado.DataBind();
            BtnModificar.Visible = false;
            txtRuc.Text = "";
            txtProveedor.Text = "";
            txtTiempo.Text = "";
        }

        private bool validarCampos()
        {
            bool b = false;

            if (String.IsNullOrEmpty(TxtArea.Text))
            {
                TxtArea.Focus();
                throw new Exception("Ingrese área");
            }

            if (String.IsNullOrEmpty(txtEdificio.Text))
            {
                txtEdificio.Focus();
                throw new Exception("Ingrese edificio");
            }

            if (String.IsNullOrEmpty(txtAula.Text))
            {
                txtAula.Focus();
                throw new Exception("Ingrese aula");
            }

            if (String.IsNullOrEmpty(TxtPiso.Text))
            {
                TxtPiso.Focus();
                throw new Exception("Ingrese piso");
            }

            //if (DDLCategoria.SelectedIndex == 0)
            //    throw new Exception("Seleccione una categoría");

            if (DDLSede.SelectedIndex == 0)
                throw new Exception("Seleccione una sede");

            if (String.IsNullOrEmpty(txtUsu.Text))
            {
                txtUsu.Focus();
                throw new Exception("Ingrese usuario asignado");
            }


            if (DateTime.Parse(txtFechaCompra.Text) > DateTime.Parse("2018-12-31"))
            {
                if (String.IsNullOrEmpty(txtRuc.Text))
                {
                    txtRuc.Focus();
                    throw new Exception("Ingrese RUC");
                }

                if (String.IsNullOrEmpty(txtProveedor.Text.Trim()))
                {
                    txtRuc.Focus();
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

            b = true;
            return b;
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (fun_validarCampos_Guardar())
                //{
                    validarCampos();

                    transacciones objTrans = new transacciones();
                    LogisticaEn logis = new LogisticaEn();
                    FacturaEn ObjFac = new FacturaEn();
                    List<string> listaCodigos = new List<string>();
                    List<FacturaEn> listaFact = new List<FacturaEn>();

                    // logis.IdLogistica = int.Parse(hdIdLogistica.Value);
                    logis.Area = TxtArea.Text;
                    logis.Edificio = txtEdificio.Text.Trim();
                    logis.Aula = txtAula.Text;
                    logis.Piso = TxtPiso.Text.Trim();
                    //logis.IdCategoria = int.Parse(DDLCategoria.SelectedValue);
                    logis.IdSede = int.Parse(DDLSede.SelectedValue);
                    logis.UsuAsignado = txtUsu.Text;
                    logis.IdLogin = int.Parse(Session["rpta"].ToString());
                    logis.ip = Request.UserHostAddress;

                    foreach (DataRow row in dt.Rows)
                    {
                        string cod = row["codigo"].ToString();
                        listaCodigos.Add(cod);

                        FacturaEn fact = new FacturaEn();
                    fact.ruc = txtRuc.Text;
                    fact.NumeroFactura = txtNroFactura.Text;
                    fact.FechaCompra = DateTime.Parse(txtFechaCompra.Text);
                        fact.Proveedor = txtProveedor.Text;
                        fact.TiempGar = int.Parse(txtTiempo.Text);
                        fact.FecFinGar = DateTime.Parse(txtFechaFin.Text);

                        listaFact.Add(fact);
                    }

                    if (objTrans.fun_modificar_activos_masivo(logis, listaCodigos, listaFact))
                    {
                        this.Page.Response.Write("<script language ='JavaScript'>window.alert('Activos modificados correctamente.');</script>");
                        limpiar();
                    }
                //}
            }
            catch (Exception ex)
            {
                //this.Page.Response.Write("<script language ='JavaScript'>window.alert('" + ex.Message + "');</script>");
                //return;
                Response.Write(ex.Message);
            }
        }
        //private bool fun_validarCampos_Guardar()
        //{
        //    bool b = false;

        //    objFact.pr_validar_CajaTexto(txtRuc);
        //    objFact.pr_validar_CajaTexto(txtProveedor);

        //    if (txtRuc.Text.Length != 11) throw new Exception("El RUC ingresado no es válido. Debe ser de 11 dígitos");

        //    b = true;
        //    return b;
        //}

        protected void hdIdLogistica_ValueChanged(object sender, EventArgs e)
        {

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

        protected void btnRuc_Click(object sender, ImageClickEventArgs e)
        {
            FacturaN objFac = new FacturaN();

            try
            {
                btnRuc.Enabled = false;
                txtProveedor.Text = "";
                //txtDireccion.Text = "";
                txtProveedor.Attributes.Add("readonly", "true");
               // txtDireccion.Attributes.Add("readonly", "true");

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
                   // txtDireccion.Text = empresa.direccion;
                   // if (empresa.direccion == "" || empresa.direccion == "-") txtDireccion.Attributes.Remove("readonly");
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
                        //txtDireccion.Attributes.Remove("readonly");
                        throw new Exception(error);
                    }

                    if (emp.estado_contribuyente != "ACTIVO")
                    {
                        throw new Exception("El estado del contribuyente es inactivo ante la SUNAT");
                    }

                    txtProveedor.Text = emp.razon_social;

                    //if (emp.direccion == "" || emp.direccion == "-")
                    //    txtDireccion.Attributes.Remove("readonly");
                    //else
                    //    txtDireccion.Text = emp.direccion;

                    // registrar empresa en la tabla tbSunat y la consulta en la tabla auditoria
                    empresa.ruc = ruc;
                    empresa.razonSocial = emp.razon_social;
                   // empresa.direccion = txtDireccion.Text.Trim();
                    empresa.usuReg = Session["usuario"].ToString();
                    empresa.pc = Request.UserHostAddress;

                    objFac.pr_registrar_empresa_api(empresa);
                    //objFac.pr_registrar_consulta_api(sede, Session["usuario"].ToString(), "ListarLogistica.aspx", ruc, Session["ip"].ToString());
                }
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

    }
}