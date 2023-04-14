using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Model.Permissao;
using SafWeb.DataLayer.Permissao;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Permissao
{
    public class BLLimitePermissao
    {
        public LimitePermissao InsereLimitePermissao(LimitePermissao objLimitePermissao)
        {
            using (DLLimitePermissao objDLLimitePermissao = new DLLimitePermissao())
            {
                try
                {
                    return objDLLimitePermissao.InsereLimitePermissao(objLimitePermissao);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public bool ExcluiLimitePermissao(LimitePermissao objLimitePermissao)
        {
            using (DLLimitePermissao objDLLimitePermissao = new DLLimitePermissao())
            {
                try
                {
                    return objDLLimitePermissao.ExcluiLimitePermissao(objLimitePermissao);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public LimitePermissao EditaLimitePermissao(LimitePermissao objLimitePermissao)
        {
            using (DLLimitePermissao objDLLimitePermissao = new DLLimitePermissao())
            {
                try
                {
                    return objDLLimitePermissao.EditaLimitePermissao(objLimitePermissao);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public IList<LimitePermissao> BuscaTodosLimitePermissao()
        {
            using (DLLimitePermissao objDLLimitePermissao = new DLLimitePermissao())
            {
                try
                {
                    return objDLLimitePermissao.BuscaLimitePermissao(0, 0, 0);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public LimitePermissao BuscaPorIdLimitePermissao(int idLimite)
        {
            using (DLLimitePermissao objDLLimitePermissao = new DLLimitePermissao())
            {
                try
                {
                    var objRetorno = objDLLimitePermissao.BuscaLimitePermissao(idLimite, 0, 0);

                    if (objRetorno.Count > 0)
                        return objRetorno[0];

                    return null;
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public IList<LimitePermissao> BuscaPorFiltroLimitePermissao(int idLimite, double dblLimite, SafWeb.Model.Util.Enum.ECriterio Criterio)
        {
            using (DLLimitePermissao objDLLimitePermissao = new DLLimitePermissao())
            {
                try
                {
                    var objRetorno = objDLLimitePermissao.BuscaLimitePermissao(idLimite, dblLimite, Criterio);
                    return objRetorno;
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
