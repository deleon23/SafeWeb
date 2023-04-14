using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Model.Permissao;
using SafWeb.DataLayer.Permissao;
using FrameWork.BusinessLayer.Utilitarios;


namespace SafWeb.BusinessLayer.Permissao
{
    public class BLPermissaoRonda
    {
        public IList<PermissaoRonda> BuscaPermissaoRonda(int idPermissao)
        {
            using (DLPermissaoRonda objDLPermissao = new DLPermissaoRonda())
            {
                try
                {
                    return objDLPermissao.BuscaPermissaoRonda(idPermissao);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public IList<PermissaoRonda> BuscaPermissaoNaoUtilizadaRonda(int idPermissao)
        {
            using (DLPermissaoRonda objDLPermissao = new DLPermissaoRonda())
            {
                try
                {
                    return objDLPermissao.BuscaPermissaoNaoUtilizadaRonda(idPermissao);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        #region Insere

        public PermissaoRonda InserePermissaoRONDA(string strDesPermissao, string strIdGrupoColetores)
        {
            using (DLPermissaoRonda objDLPermissaoRonda = new DLPermissaoRonda())
            {
                try
                {
                    return objDLPermissaoRonda.InserePermissaoRONDA(strDesPermissao, strIdGrupoColetores);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        #endregion

        #region Insere

        public PermissaoRonda EditaPermissaoRONDA(int idPermissao, string strDesPermissao, string strIdGrupoColetores)
        {
            using (DLPermissaoRonda objDLPermissaoRonda = new DLPermissaoRonda())
            {
                try
                {
                    return objDLPermissaoRonda.EditaPermissaoRONDA(idPermissao, strDesPermissao, strIdGrupoColetores);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        #endregion

    }
}
