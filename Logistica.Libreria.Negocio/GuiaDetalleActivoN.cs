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
    public class GuiaDetalleActivoN
    {
        GuiaDetalleActivosDAO objguia = new GuiaDetalleActivosDAO();

        public string InsertarGuiaDetalleActivos(GuiaDetalleActivosEn objE)
        {
            return objguia.InsertarGuiaDetalleActivos(objE);
        }

        public DataTable BuscarGuiaActivos(string xcod)
        {

            return objguia.BuscarGuiaActivos(xcod);
        }
    }
}
