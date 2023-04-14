using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SafWeb.Model.Regional;
using SafWeb.DataLayer.Regional;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Regional
{
    public class BLRegional
    {
        #region Listar

       /// <summary>
       ///      Lita as regionais 
       /// </summary>
       /// <history>
       ///      [mribeiro] created 22/06/2009
       /// </history>
       /// <returns></returns>
        public Collection<SafWeb.Model.Regional.Regional> Listar()
        {
            DLRegional objDLRegional = new DLRegional();

            try
            {
                return objDLRegional.Listar();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
               objDLRegional.Finalizar();
            }
        }

        #endregion
    }
}
