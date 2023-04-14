using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Model.HelpDesk;
using SafWeb.DataLayer.HelpDesk;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Usuarios;

namespace SafWeb.BusinessLayer.HelpDesk
{
    public class BLHelpDeskSolCrachaTitular
    {
        public IList<HelpDeskSolCrachaTitular> ListarCrachaTitularPendentes(int piniIdSolicitacao, int pintIdRegional, int pintIdFilial, string pstrNomeColaborador, int pintIdTipoSolicitacao)
        {
            using (DLHelpDeskSolCrachaTitular objDLLimitePermissao = new DLHelpDeskSolCrachaTitular())
            {
                try
                {
                    return objDLLimitePermissao.ListarCrachaTitularPendentes(Convert.ToInt32(BLAcesso.IdUsuarioLogado()),piniIdSolicitacao,pintIdRegional, pintIdFilial, pstrNomeColaborador, pintIdTipoSolicitacao);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public bool AdicionarPermissaoCrachaTitularColaborador(int idSolicitacaoCrachaTitular, int idPermissao)
        {
            using (DLHelpDeskSolCrachaTitular objDLLimitePermissao = new DLHelpDeskSolCrachaTitular())
            {
                try
                {
                    return objDLLimitePermissao.AdicionarPermissaoCrachaTitularColaborador(Convert.ToInt32(BLAcesso.IdUsuarioLogado()), idSolicitacaoCrachaTitular, idPermissao);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }
    }
}
