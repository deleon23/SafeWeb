using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SafWeb.Model.Escala;
using SafWeb.DataLayer.Escala;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : BLPeriodicidade
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe BLPeriodicidade
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 30/12/2009 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class BLPeriodicidade
    {
        #region Listar Periodicidade

        /// <summary>
        ///      Lista Periodicidade
        /// </summary>
        /// <history>
        ///      [cmarchi] created 30/12/2009
        /// </history>
        /// <returns></returns>
        public Collection<Periodicidade> Listar()
        {
            DLPeriodicidade objDLPeriodicidade = new DLPeriodicidade();

            try
            {
                return objDLPeriodicidade.Listar();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLPeriodicidade.Finalizar();
            }
        }

        #endregion
    }
}
