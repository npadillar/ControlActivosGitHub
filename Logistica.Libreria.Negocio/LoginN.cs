using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistica.Libreria.Datos;
using Logistica.Libreria.Entidad;
using System.Data;
using System.Collections;

namespace Logistica.Libreria.Negocio
{
    public class LoginN 
    {
        ArrayList _arrOpciones = new ArrayList();

        LoginDAO objdatos = new LoginDAO();
        public int ValidarAcceso(string Usuario, string Clave)
        {
            return objdatos.ValidarAcceso(Usuario, Clave);
        }


        public int ObtenerPerfil(int xIdUsuario)
        {
            return objdatos.ObtenerPerfil(xIdUsuario);
        }

        // new 11-01-19

        public DataTable fun_ListarSedes()
        {
            try
            {
                return objdatos.ListarSedes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool fun_login(string usuario, string clave)
        {
            try
            {
                return objdatos.login(usuario, clave);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool fun_verificar_estadoUsu(string usuario)
        {
            try
            {
                return objdatos.verificar_estadoUsu(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool fun_verificar_AlcanceTotal(string usuario)
        {
            try
            {
                return objdatos.verificar_AlcanceTotal(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable fun_listarCargos_usuario_xSede(string usuario, string idLocal)
        {
            try
            {
                return objdatos.listarCargos_usuario_xSede(usuario, idLocal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool fun_MostrarControl_xCargo(int control, int cargo)
        {
            try
            {
                return objdatos.mostrarControl_xCargo(control, cargo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string fun_traer_nombre_persona(string usuario)
        {
            try
            {
                return objdatos.traer_nombre_persona(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Int32 fun_traer_idTra_persona(string usuario)
        {
            try
            {
                return objdatos.traer_idTra_persona(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void pr_Guardar_usuario(int idTrabajador, string nombre, string usuario)
        {
            try
            {
                objdatos.Guardar_usuario(idTrabajador, nombre, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}