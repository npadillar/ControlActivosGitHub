using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;

using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace Logistica.Libreria.Conexion
{
    public static class DbBaseConex
    {

        public static DbConnection ObtenerConexion()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["CosapiSoft.Adryan"];
            object crip = new object();
            string strNombreProveedor = settings.ProviderName;
            DbProviderFactory factoriaProveedor = DbProviderFactories.GetFactory(strNombreProveedor);

            DbConnection cnn = null;
            string strCnn = String.Empty;

            strCnn = settings.ConnectionString;

            try
            {
                cnn = factoriaProveedor.CreateConnection();
                cnn.ConnectionString = strCnn;
                cnn.Open();
            }
            catch (Exception ex)
            {
                ex.ToString() ; 
            }

            return cnn;
        }


        public static void CerrarConexion(ref DbConnection _conexion)
        {
            if (!(_conexion == null))
            {
                if ((_conexion.State == ConnectionState.Open))
                {
                    _conexion.Close();
                    _conexion = null;
                }

            }

        }
    }
}
