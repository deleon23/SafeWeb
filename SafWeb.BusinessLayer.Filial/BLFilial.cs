using System;
using System.Collections.ObjectModel;
using System.Data;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.BusinessLayer.Area;
using SafWeb.DataLayer.Filial;

namespace SafWeb.BusinessLayer.Filial
{
    public class BLFilial
    {
        #region Inserir
        /// <summary>
        /// Insere uma filial
        /// </summary>
        /// <returns>Id_Filial ou Erro</returns>
        /// <history>
        ///     [haguiar_4] created 13/01/2011
        /// </history>
        public int Inserir(SafWeb.Model.Filial.Filial pobjFilial)
        {
            DLFilial objDLFilial = new DLFilial();
            
            try
            {
                int intIdFilial;
                intIdFilial = objDLFilial.Inserir(pobjFilial);

                //verifica id da filial das áreas
                foreach (SafWeb.Model.Area.Area objArea in pobjFilial.ColArea)
                {
                    objArea.IdFilial = intIdFilial;
                }

                BLArea objBLArea = new BLArea();
                objBLArea.InserirAlterarArea(pobjFilial.ColArea);

                return intIdFilial;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFilial.Finalizar();
            }
        }
        #endregion

        #region AlterarFilial
        /// <summary>
        /// Altera uma Filial
        /// </summary>
        /// <param name="gobjFilial">Objeto Filial</param>
        /// <returns>Valor do Id da Filial ou Erro</returns>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        /// </history>
        public int Alterar(SafWeb.Model.Filial.Filial pobjFilial)
        {
            DLFilial objDLFilial = new DLFilial();

            try
            {
                int intIdFilial;

                intIdFilial = objDLFilial.Alterar(pobjFilial); 

                BLArea objBLArea = new BLArea();
                objBLArea.InserirAlterarArea(pobjFilial.ColArea);

                return intIdFilial;
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFilial.Finalizar();
            }
        }
        #endregion

        
        #region Listar

        /// <summary>
        ///      Lita as filiais
        /// </summary>
        /// <history>
        ///      [mribeiro] created 23/06/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Filial.Filial> Listar(int pintRegional)
        {
            DLFilial objDLFilial = new DLFilial();

            try
            {
                return objDLFilial.Listar(pintRegional);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFilial.Finalizar();
            }
        }

        /// <summary>
        ///      Lita todas as filiais
        /// </summary>
        /// <history>
        ///      [cmarchi] created 11/2/2010
        /// </history>
        /// <returns>Filiais</returns>
        public Collection<SafWeb.Model.Filial.Filial> Listar()
        {
            DLFilial objDLFilial = new DLFilial();

            try
            {
                return objDLFilial.Listar();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFilial.Finalizar();
            }
        }

        /// <summary>
        ///      Lista as filiais
        /// </summary>
        /// <history>
        ///      [haguiar_4] created 12/01/2011
        /// </history>
        /// <returns>Retorna datatable de filiais</returns>
        public DataTable Listar_DataTable(int pintRegional, int pintIdFilial, string pstrDes_Filial)
        {
            DLFilial objDLFilial = new DLFilial();

            try
            {
                return objDLFilial.Listar_DataTable(pintRegional, pintIdFilial, pstrDes_Filial);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFilial.Finalizar();
            }
        }
        
        #endregion

        #region AlterarSituacao
        /// <summary>
        /// Altera o status da Situação.
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Filial</param>
        /// <param name="pblSituacao">Flag da Situação</param>
        /// <history>
        ///     [haguiar_4] created 13/01/2011
        /// </history>
        public void AlterarSituacao(int pintIdFilial, bool pblnSituacao)
        {
            DLFilial objDLFilial= new DLFilial();

            try
            {
                objDLFilial.AlterarSituacao(pintIdFilial, pblnSituacao);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFilial.Finalizar();
            }
        }
        #endregion

        #region Obter

        /// <summary>
        ///      Obtem os registros de uma filial
        /// </summary>
        /// <param name="pintIdColaborador">Id da Filial</param>
        /// <returns>Objeto Filial</returns>
        /// <history>
        ///     [tgerevini] created 06/04/2010
        /// </history>
        public SafWeb.Model.Filial.Filial Obter(int pintIdFilial)
        {
            DLFilial objDLFilial = new DLFilial();

            try
            {
                return objDLFilial.Obter(pintIdFilial);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLFilial.Finalizar();
            }
        }

        #endregion

        

        
    }
}