using System;
using System.Collections.ObjectModel;
using System.Data;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.DataLayer.Area;

namespace SafWeb.BusinessLayer.Area
{
    public enum EAreasSeguranca
    {
        Todas = -1,
        Acesso = 0,
        ColetoresPonto = 1
    }

    public class BLArea
    {
        #region Listar Area Segurança

        /// <summary>
        ///      Lista as areas (Codigo + Flag Area Segurança)
        /// </summary>
        /// <history>
        ///      [mribeiro] created 07/07/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Area.Area> ListarAreaSeg(int pintFilial, EAreasSeguranca paseTipo)
        {
            DLArea objDLArea = new DLArea();

            try
            {
                return objDLArea.ListarAreaSeg(pintFilial, (int)paseTipo);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLArea.Finalizar();
            }
        }

        #endregion

        #region Listar Area

        /// <summary>
        ///      Lista as areas
        /// </summary>
        /// <history>
        ///      [mribeiro] created 07/07/2009
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Area.Area> ListarArea(int pintFilial)
        {
            DLArea objDLArea = new DLArea();

            try
            {
                return objDLArea.ListarArea(pintFilial);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLArea.Finalizar();
            }
        }


        /// <summary>
        ///      Lista as areas
        /// </summary>
        /// <history>
        ///      [haguiar] created 26/01/2011
        /// </history>
        /// <returns>DataTable Area</returns>
        public DataTable ListarArea_DataTable(int pintFilial)
        {
            DLArea objDLArea = new DLArea();

            try
            {
                return objDLArea.ListarArea_DataTable(pintFilial);
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLArea.Finalizar();
            }
        }

        #endregion

        #region Listar GrupoColetores

        /// <summary>
        ///      Lista os grupos de coletores
        /// </summary>
        /// <history>
        ///      [haguiar_4] created 26/01/2011
        /// </history>
        /// <returns></returns>
        public Collection<SafWeb.Model.Area.GrupoColetores> ListarGrupoColetores()
        {
            DLArea objDLArea = new DLArea();

            try
            {
                return objDLArea.ListarGrupoColetores();
            }
            catch (Exception ex)
            {
                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLArea.Finalizar();
            }
        }

        #endregion


        #region Inserir Areas

        /// <summary>
        ///      Inserir Area
        /// </summary>
        /// <history>
        ///      [haguiar_4] created 27/01/2011
        ///      [haguiar_4] modified 02/02/2011
        /// </history>
        /// <returns></returns>
        public void InserirAlterarArea(Collection<SafWeb.Model.Area.Area> pobjColArea)
        {
            DLArea objDLArea = new DLArea();

            try
            {
                if (pobjColArea.Count > 0)
                {
                    objDLArea.BeginTransaction();

                    //Exclui áreas da filial
                    //objDLArea.ExcluirAreas(pintIdFilial);

                    foreach (SafWeb.Model.Area.Area objArea in pobjColArea)
                    {
                        if (Convert.ToInt32(objArea.Codigo) == 0)
                        {
                            objDLArea.Inserir(objArea);
                        }
                        else
                        {
                            objDLArea.Alterar(objArea);
                        }
                    }

                    objDLArea.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                objDLArea.RollBackTransaction();

                BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                throw;
            }
            finally
            {
                objDLArea.Finalizar();
            }
        }

        #endregion
    }
}
