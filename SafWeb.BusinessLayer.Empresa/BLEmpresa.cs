using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.Empresa;

namespace SafWeb.BusinessLayer.Empresa
{
    public class BLEmpresa
    {
        #region Inserir

        /// <summary>
        ///      Insere uma Empresa
        /// </summary>
        /// <history>
        ///      [mribeiro] created 01/07/2009
        /// </history>
        /// <returns></returns>
        public int Inserir(SafWeb.Model.Empresa.Empresa pobjEmpresa)
        {
            DLEmpresa objDLEmpresa = new DLEmpresa();

            try
            {
                return objDLEmpresa.Inserir(pobjEmpresa);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEmpresa.Finalizar();
            }
        }

        #endregion

        #region Listar

        /// <summary>
        ///      Lita as Empresas 
        /// </summary>
        /// <history>
        ///      [mribeiro] created 24/06/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Empresa.Empresa> Listar()
        {
            DLEmpresa objDLEmpresa = new DLEmpresa();

            try
            {
                return objDLEmpresa.Listar();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEmpresa.Finalizar();
            }
        }
        #endregion

        #region Obter

        /// <summary>
        ///      Retorna o objeto Lista 
        /// </summary>
        /// <param name="pintIdLista">Id da lista</param>
        /// <history>
        ///      [cfrancisco] created 24/04/2012
        /// </history>
        /// <returns>Objeto Lista</returns>
        public SafWeb.Model.Empresa.Empresa Obter(SafWeb.Model.Empresa.Empresa pobjEmpresa)
        {
            DLEmpresa objDLEmpresa = new DLEmpresa();

            try
            {
                return objDLEmpresa.Obter(pobjEmpresa);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLEmpresa.Finalizar();
            }
        }

        
        #endregion
    }
}
