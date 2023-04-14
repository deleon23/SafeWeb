using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.Cracha;

namespace SafWeb.BusinessLayer.Cracha
{
    public class BLCracha
    {
        #region Inserir

        /// <summary>
        ///     Insere os registros na base
        /// </summary>
        /// <param name="pobjCracha">Model Cracha</param>
        /// <returns>C�digo do Crach�</returns>
        /// <history>
        ///     [vsantos] 15/07/2009 Created 
        /// </history>
        public int Inserir(SafWeb.Model.Cracha.Cracha pobjCracha)
        {
            DLCracha objDLCracha = null;

            try
            {
                objDLCracha = new DLCracha();

                return objDLCracha.Inserir(pobjCracha);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLCracha.Finalizar();
            }
        }

        #endregion

        #region Alterar

        /// <summary>
        ///     Altera registros na base
        /// </summary>
        /// <param name="pobjCracha">Model Cracha</param>
        /// <returns>Boeleano indicando sucesso da opera��o</returns>
        /// <history>
        ///     [vsantos] created 15/07/2009
        /// </history>
        public bool Alterar(SafWeb.Model.Cracha.Cracha pobjCracha)
        {
            DLCracha objDLCracha = null;

            try
            {
                objDLCracha = new DLCracha();

                return objDLCracha.Alterar(pobjCracha);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLCracha.Finalizar();
            }
        }

        #endregion

        #region Listar

        /// <summary>
        ///     Lista os registros da base
        /// </summary>
        /// <param name="pobjCracha">Model Cracha</param>
        /// <returns>Cole��o de objetos tipo Cracha</returns>
        /// <history>
        ///     [vsantos] created 15/07/2009
        /// </history>
        public Collection<Model.Cracha.Cracha> Listar(Model.Cracha.Cracha pobjCracha,
                                                      bool pblnEntrada)
        {
            DLCracha objDLCracha = null;

            try
            {
                objDLCracha = new DLCracha();

                return objDLCracha.Listar(pobjCracha,
                                          pblnEntrada);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLCracha.Finalizar();
            }
        }

        #endregion

        #region Listar Cracha Tipo

        /// <summary>
        ///     Lista registros da base
        /// </summary>
        /// <returns>Cole��o de objetos tipo CrachaTipo</returns>
        /// <history>
        ///     [vsantos] created 15/07/2009
        /// </history>
        public Collection<Model.Cracha.CrachaTipo> ListarCrachaTipo()
        {
            DLCracha objDLCracha = null;

            try
            {
                objDLCracha = new DLCracha();

                return objDLCracha.ListarCrachaTipo();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLCracha.Finalizar();
            }
        }

        #endregion

        #region Obter Id do Crach�

        /// <summary>
        ///     Obtem o Id do crach�
        /// </summary>
        /// <param name="strNumero">N�mero do Crach�</param>
        /// <returns>Id do crach�/returns>
        /// <history>
        ///     [mribeiro] created 04/08/2009
        /// </history>
        public int ObterIdCracha(string pstrNumero)
        {
            DLCracha objDLCracha = null;

            try
            {
                objDLCracha = new DLCracha();

                return objDLCracha.ObterIdCracha(pstrNumero);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLCracha.Finalizar();
            }
        }

        #endregion

        #region Obter N�mero Crach�

        /// <summary>
        ///     Obtem os 7 primeiros d�gitos do crach� de acordo com a filial
        /// </summary>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <returns>7 primeiros digitos do crach�</returns>
        /// <history>
        ///     [mribeiro] created 15/10/2009
        /// </history>
        public string ObterNumeroCracha(int pintIdFilial)
        {
            DLCracha objDLCracha = null;

            try
            {
                objDLCracha = new DLCracha();

                return objDLCracha.ObterNumeroCracha(pintIdFilial);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " M�todo: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLCracha.Finalizar();
            }
        }

        #endregion
    }
}
