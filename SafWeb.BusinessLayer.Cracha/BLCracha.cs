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
        /// <returns>Código do Crachá</returns>
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
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
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
        /// <returns>Boeleano indicando sucesso da operação</returns>
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
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
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
        /// <returns>Coleção de objetos tipo Cracha</returns>
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
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
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
        /// <returns>Coleção de objetos tipo CrachaTipo</returns>
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
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLCracha.Finalizar();
            }
        }

        #endregion

        #region Obter Id do Crachá

        /// <summary>
        ///     Obtem o Id do crachá
        /// </summary>
        /// <param name="strNumero">Número do Crachá</param>
        /// <returns>Id do crachá/returns>
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
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLCracha.Finalizar();
            }
        }

        #endregion

        #region Obter Número Crachá

        /// <summary>
        ///     Obtem os 7 primeiros dígitos do crachá de acordo com a filial
        /// </summary>
        /// <param name="pintIdFilial">Id da Filial</param>
        /// <returns>7 primeiros digitos do crachá</returns>
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
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
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
