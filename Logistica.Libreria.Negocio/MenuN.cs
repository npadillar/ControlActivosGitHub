using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistica.Libreria.Datos;
using est = Logistica.Libreria.Entidad.MenuEst;

namespace Logistica.Libreria.Negocio
{
    public class MenuN
    {
        MenuDAO objMenu = new MenuDAO();

        public List<est.EST_MODULO> fun_listarModulos_xSistema_xCargo(int cargo)
        {
            try
            {
                return objMenu.listarModulos_xSistema_xCargo(cargo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<est.EST_PAGINA> fun_listarPaginas_xModulo_xCargo(int modulo, int cargo)
        {
            try
            {
                return objMenu.listarPaginas_xModulo_xCargo(modulo, cargo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool fun_MostrarControl_xCargo(int control, int cargo)
        {
            try
            {
                return objMenu.mostrarControl_xCargo(control, cargo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
