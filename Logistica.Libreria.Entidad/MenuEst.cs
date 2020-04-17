using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistica.Libreria.Entidad
{
    public class MenuEst
    {
        public struct EST_AREA
        {
            public int idAre;
            public string descrip;
            public string estado;
            public int alcance;
            public string usuReg;
        }

        public struct EST_SISTEMA
        {
            public int idSis;
            public string descrip;
            public int tipo;
            public string usuReg;
        }

        public struct EST_MODULO
        {
            public int idMod;
            public string descrip;
            public int idSis;
            public string usuReg;
        }

        public struct EST_PAGINA
        {
            public int idPag;
            public string descrip;
            public string link;
            public EST_SISTEMA sistema;
            public EST_MODULO modulo;
            public EST_PAGINA_PERMISOS pagper;
            public string usuReg;
        }

        public struct EST_PAGINA_PERMISOS
        {
            public EST_CARGO cargo;
            public int idPag;
            public int idPermiso;
            public string permiso;
            public string estado;
            public string usuReg;
        }

        public struct EST_CARGO
        {
            public int idCar;
            public string descrip;
            public string estado;
            public string usuReg;
        }
    }
}
