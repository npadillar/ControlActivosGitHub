using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
    public class InventarioEn
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdSede { get; set; }
        public string SubSede { get; set; }
        public string Marca{ get; set; }
        public string Modelo { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        public string Area { get; set; }
        public string Piso { get; set; }
        public string Edificio { get; set; }
        public int IdCategoria { get; set; }
        public int Estado { get; set; }

    }
}
