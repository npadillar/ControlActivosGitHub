using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
    public class LogisticaEn
    {
        public int IdLogistica { get; set; }
        public int IdInventario { get; set; }
        public int IdLogin { get; set; }
        public DateTime Fecha { get; set; }
        public string Codigo { get; set; }
        public string codAnterior { get; set; }
        public string Descripcion { get; set; }
        public int IdSede { get; set; }
        public string Area { get; set; }
        public string Piso { get; set; }
        public string Edificio { get; set; }
        public int IdCategoria { get; set; }

        public string Aula { get; set; }
        public string Serie { get; set; }
        //public string Numero { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int IdCondicion { get; set; }
        public string Observacion { get; set; }
        public string UsuAsignado { get; set; }
        public string ip { get; set; }
    }
}
