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
    public class SedeN
    {
        SedeDAO objdatos = new SedeDAO();
        public DataTable ListarSede(string pag)
        {
            return objdatos.ListarSede(pag);
        }
    }
}
