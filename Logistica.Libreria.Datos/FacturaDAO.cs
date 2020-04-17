using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using System.Configuration;

namespace Logistica.Libreria.Datos
{
   public class FacturaDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
        ConexionDAO objCn = new ConexionDAO();

        public FacturaEn buscar_proveedor_xRuc(string ruc)
        {
            try
            {
                FacturaEn emp = new FacturaEn();

                using (SqlConnection cn = new SqlConnection(objCn.conex("SISE")))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_tbSunat_buscar", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ruc", ruc);
                    SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows == true)
                        while (dr.Read())
                        {
                            emp.razonSocial = dr["razon_social"].ToString();
                            emp.direccion = dr["direccion"].ToString();
                        }
                }
                return emp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void registrar_empresa_api(FacturaEn emp)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(objCn.conex("SISE")))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_tbSunat_insertar", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ruc", emp.ruc);
                    cmd.Parameters.AddWithValue("@razonSocial", emp.razonSocial);
                    cmd.Parameters.AddWithValue("@direccion", emp.direccion);
                    cmd.Parameters.AddWithValue("@usuReg", emp.usuReg);
                    cmd.Parameters.AddWithValue("@ip", emp.pc);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void registrar_factura_guia(FacturaEn factguia)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cad_cn))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_GuiaFactura_ins", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdLogistica", factguia.IdLogistica);
                    cmd.Parameters.AddWithValue("@Ruc", factguia.Ruc);
                    cmd.Parameters.AddWithValue("@Proveedor", factguia.Proveedor);
                    cmd.Parameters.AddWithValue("@Direccion", factguia.Direccion);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void registrar_consulta_api(string sede, string usuario, string pagina, string valor, string ip)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(objCn.conex(sede)))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_NEW_auditoria_ReniecSunat", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@sistema", "Cobranzas");
                    cmd.Parameters.AddWithValue("@pagina", pagina);
                    cmd.Parameters.AddWithValue("@valor", valor);
                    cmd.Parameters.AddWithValue("@ip", ip);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
