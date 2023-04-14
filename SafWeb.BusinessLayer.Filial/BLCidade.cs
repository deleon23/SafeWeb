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
    public class BLCidade
    {

        #region Listar Cidade

        /// <summary>
        ///      Lita as Cidades 
        /// </summary>
        /// <history>
        ///      [haguiar_4] created 14/01/2011
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Filial.Cidade> ListarCidade(int pintId_Estado)
        {
            DLCidade objDLCidade = new DLCidade();

            try
            {
                return objDLCidade.ListarCidade(pintId_Estado);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLCidade.Finalizar();
            }
        }

        #endregion


    }
}