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
    public class GuiaTemporalN
    {
        GuiaTemporalDAO objguiaT  = new GuiaTemporalDAO();

        public string InsertarGuiaTemporal(GuiaTemporalEn ObjG)
        {
            return objguiaT.InsertarGuiaTemporal(ObjG);
        }

        public DataTable ListarGuiaTemporal()
        {
            return objguiaT.ListarGuiaTemporal();
        }

        public DataTable EliminarGuiaTemporal()
        {
            return objguiaT.EliminarGuiaTemporal();
        }
        
    }
}
