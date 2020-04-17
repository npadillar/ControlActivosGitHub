using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logistica.Libreria.Entidad;
using Logistica.Libreria.Datos;

namespace Logistica.Libreria.Negocio
{
    public class DisposicionBajaActivosN
    {
        DisposicionBajaActivosDAO objdatos = new DisposicionBajaActivosDAO();

        public DataTable ListarDisposicionBajaActivos()
        {

            return objdatos.ListarDisposicionBajaActivos();
        }
    }
}
