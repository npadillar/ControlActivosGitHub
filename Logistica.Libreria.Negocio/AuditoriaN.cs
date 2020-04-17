using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistica.Libreria.Datos;
using System.Data;
using est = Logistica.Libreria.Entidad.AuditoriaEst;

namespace Logistica.Libreria.Negocio
{
    public class AuditoriaN
    {
        AuditoriaDAO objAudi = new AuditoriaDAO();

        public DataTable fun_listar_paginas()
        {
            try
            {
                return objAudi.listar_paginas();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable fun_listar_auditoria(est.EST_AUDITORIA audi)
        {
            try
            {
                return objAudi.listar_auditoria(audi);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
