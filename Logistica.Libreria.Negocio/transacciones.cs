using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Logistica.Libreria.Datos;
using Logistica.Libreria.Entidad;
using System.Data;

namespace Logistica.Libreria.Negocio
{
    public class transacciones
    {
        LogisticaN objLogis = new LogisticaN();
        BajaActivosN objBaja = new BajaActivosN();
        ModificarLogisticaN objModLog = new ModificarLogisticaN();

        public bool fun_registrar_activos_masivo(List<LogisticaEn> listLogis, List<FacturaEn> listFact)
        {
            try
            {
                bool b = false;
                using (TransactionScope ProcesoTransaccional = new TransactionScope())
                {
                    for (int i = 0; i < listLogis.Count; i++)
                    {
                        objLogis.InsertarLogistica(listLogis[i], listFact[i]);
                    }
                    ProcesoTransaccional.Complete();
                    b = true;
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool fun_registrar_baja(BajaActivosCabeceraEn bajaCabecera, List<BajaActivosDetalleEn> listDetBaja)
        {
            try
            {
                bool b = false;
                using (TransactionScope ProcesoTransaccional = new TransactionScope())
                {
                    string cod = "";
                    cod = objBaja.InsertarBajaActivosCabecera(bajaCabecera);

                    for (int i = 0; i < listDetBaja.Count; i++)
                    {
                        listDetBaja[i].IdBajaCabecera = int.Parse(cod);
                        objBaja.InsertarBajaActivosDetalle(listDetBaja[i]);
                    }
                    ProcesoTransaccional.Complete();
                    b = true;
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool fun_modificar_activos_masivo(LogisticaEn logis, List<string> listaCodigos, List<FacturaEn> listaFact)
        {
            try
            {
                bool b = false;
                using (TransactionScope ProcesoTransaccional = new TransactionScope())
                {
                    for (int i = 0; i < listaCodigos.Count; i++)
                    {
                        logis.Codigo = listaCodigos[i];
                        objModLog.fun_InsertarModificarLog_masivo(logis, listaFact[i]); 
                        objLogis.pr_ActualizarLogistica_masivo(listaCodigos[i], logis, listaFact[i]);                        
                    }
                    ProcesoTransaccional.Complete();
                    b = true;
                }
                return b;  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool fun_registrar_reparacion(ReparacionEn rep, List<ReparacionDetEn> det, GuiaCabeceraEn guia)
        {
            try
            {
                bool b = false;
                using (TransactionScope ProcesoTransaccional = new TransactionScope())
                {
                    ReparacionesDAO objRep = new ReparacionesDAO();
                    GuiaCabeceraN objGuia = new GuiaCabeceraN();

                    int idReparacion = objRep.registrar_reparacion_cab(rep);

                    for (int i = 0; i < det.Count; i++)
                    {
                        det[i].idReparacion = idReparacion;
                        objRep.registrar_reparacion_det(det[i]);
                    }

                    objGuia.ActualizarGuiaCabecera(guia);

                    ProcesoTransaccional.Complete();
                    b = true;
                }
                return b;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
