using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Datos;

namespace Logistica.Libreria.Negocio
{
    public class UsuarioN
    {
        UsuarioDAO objdatos = new UsuarioDAO();
        public DataTable ListarUsuario()
        {
            return objdatos.ListarUsuario();
        }

        public DataTable fun_ListarUsuario_Auditoria()
        {
            return objdatos.ListarUsuario_Auditoria();
        }

        public DataTable fun_ListarUsuario_bajas()
        {
            return objdatos.ListarUsuario_bajas();
        }
    }
}
