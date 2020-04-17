using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Negocio;

namespace App.Web.Logistica
{
    public partial class detalleActivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtCodigo.Text = Request.QueryString["c"];
                    listar_historial();
                    //txtCodigo.Text = Session["CODIGO"].ToString().ToUpper();
                    //ModificarLogisticaN objModfLog = new ModificarLogisticaN();
                    //ModificarLogisticaN objLog = new ModificarLogisticaN();

                    //DataTable DTLog = objLog.BuscarCodigo1(txtCodigo.Text);
                    //gvRep1.DataSource = DTLog;
                    //gvRep1.DataBind();


                    //DataTable DTModLog = objModfLog.BuscarCodigo(txtCodigo.Text);
                    //gvReporte.DataSource = DTModLog;
                    //gvReporte.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void listar_historial()
        {
            ModificarLogisticaN objModLog = new ModificarLogisticaN();
            DataTable dtDatos = new DataTable();
            DataTable dt = new DataTable();

            dt.Columns.Add("Fecha");
            dt.Columns.Add("Código");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Sede");
            dt.Columns.Add("Area");
            dt.Columns.Add("Piso");
            dt.Columns.Add("Edificio");
            dt.Columns.Add("Categoria");
            dt.Columns.Add("Aula");
            dt.Columns.Add("Serie");
            dt.Columns.Add("Marca");
            dt.Columns.Add("Modelo");
            dt.Columns.Add("Condicion");
            dt.Columns.Add("Observacion");
            dt.Columns.Add("UsuAsignado");
            dt.Rows.Clear();

            dtDatos = objModLog.fun_listar_historial_activo(txtCodigo.Text);

            if (dtDatos.Rows.Count == 1) // MOSTRAR HISTORIAL CRUZADO
            {
                DataTable DTLog = objModLog.BuscarCodigo1(txtCodigo.Text);
                gvRep1.DataSource = DTLog;
                gvRep1.DataBind();

                DataTable DTModLog = objModLog.BuscarCodigo(txtCodigo.Text);
                gvReporte.DataSource = DTModLog;
                gvReporte.DataBind();
            }
            else // MOSTRAR HISTORIAL ORDENADO
            {
                // Primer grid
                DataRow row = dtDatos.Rows[0];
                dt.Rows.Add(row["fecha"],
                            row["codigo"],
                            row["nombre"],
                            row["descripcion"],
                            row["sede"],
                            row["area"],
                            row["piso"],
                            row["edificio"],
                            row["categoria"],
                            row["aula"],
                            row["serie"],
                            row["marca"],
                            row["modelo"],
                            row["condicion"],
                            row["observacion"],
                            row["usuAsignado"]);
                gvRep1.DataSource = dt;
                gvRep1.DataBind();
                // ============================================================


                // Segundo grid
                dt.Rows.Clear();
                for (int i = 1; i < dtDatos.Rows.Count; i++)
                {
                    DataRow rowx = dtDatos.Rows[i];
                    dt.Rows.Add(rowx["fecha"],
                                rowx["codigo"],
                                rowx["nombre"],
                                rowx["descripcion"],
                                rowx["sede"],
                                rowx["area"],
                                rowx["piso"],
                                rowx["edificio"],
                                rowx["categoria"],
                                rowx["aula"],
                                rowx["serie"],
                                rowx["marca"],
                                rowx["modelo"],
                                rowx["condicion"],
                                rowx["observacion"],
                                rowx["usuAsignado"]);
                }

                gvReporte.DataSource = dt;
                gvReporte.DataBind();
                // ============================================================
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {

            //  btnSalir.Attributes.Add("onclick", "window.close();");
            Response.Write("<script>window.close();</script>");
        }

        protected void btnSalir_Click1(object sender, EventArgs e)
        {
            Response.Write("<script>window.close();</script>");
        }
    }
}