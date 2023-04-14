using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.Filial;
using SafWeb.Model.Filial;
using System.Data;

namespace SafWeb.BusinessLayer.Filial
{
    public class BLEstado
    {

        #region Listar Estado

        /// <summary>
        ///      Lita os Estados 
        /// </summary>
        /// <history>
        ///      [haguiar_4] created 14/01/2011
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Filial.Estado> ListarEstado()
        {
            DLEstado objDLEstado = new DLEstado();

            try
            {
                return objDLEstado.ListarEstado();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEstado.Finalizar();
            }
        }

        #endregion

    }
}