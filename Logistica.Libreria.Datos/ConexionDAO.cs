using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Configuration;

namespace Logistica.Libreria.Datos
{
    public class ConexionDAO
    {
        public string conex(string bd)
        {
            string cadena = "";

            if (bd == "activos")
            {
                cadena = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;
            }
            else
            {
                // Data Source=srv08.local;database=BDSISE;uid=u_sise;pwd=sis3*2018
                cadena = ConfigurationManager.ConnectionStrings["conexSise"].ConnectionString;
            }

            return cadena;
        }
    }
}
