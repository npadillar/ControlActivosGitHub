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
    public class InventarioN
    {
          InventarioDAO objdatos = new InventarioDAO();

        public DataTable BuscaInventario(string xcod)
        {
            return objdatos.BuscaInventario(xcod);
        }

        public DataTable BuscaInv(string xcod)
        {
            return objdatos.BuscaInv(xcod);
        }
            public DataTable ListarInventario()
        {
            return objdatos.ListarInventario();
        }

            public string InsertarInventario(InventarioEn objE)
            {
                return objdatos.InsertarInventario(objE);
            }

            public bool EliminarInventario(string xId)
            {
                return objdatos.EliminarInventario(xId);
            }
            public string ActualizarInventario(InventarioEn ObjN)
            {
                return objdatos.ActualizarInventario(ObjN);
            }
    }
}
