using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Model.Permissao;
using SafWeb.DataLayer.Permissao;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Permissao
{
    public class BLLimitePrmColaborador
    {
        public LimitePrmColaborador InsereLimitePrmColaborador(LimitePrmColaborador objLimitePrmColaborador)
        {
            using (DLLimitePrmColaborador objDLLimitePrmColaborador = new DLLimitePrmColaborador())
            {
                try
                {
                    return objDLLimitePrmColaborador.InsereLimitePrmColaborador(objLimitePrmColaborador);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public bool ExcluiLimitePrmColaborador(LimitePrmColaborador objLimitePrmColaborador)
        {
            using (DLLimitePrmColaborador objDLLimitePrmColaborador = new DLLimitePrmColaborador())
            {
                try
                {
                    return objDLLimitePrmColaborador.ExcluiLimitePrmColaborador(objLimitePrmColaborador);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public LimitePrmColaborador EditaLimitePrmColaborador(LimitePrmColaborador objLimitePrmColaborador)
        {
            using (DLLimitePrmColaborador objDLLimitePrmColaborador = new DLLimitePrmColaborador())
            {
                try
                {
                    return objDLLimitePrmColaborador.EditaLimitePrmColaborador(objLimitePrmColaborador);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public IList<LimitePrmColaborador> BuscaTodosLimitePrmColaborador()
        {
            using (DLLimitePrmColaborador objDLLimitePrmColaborador = new DLLimitePrmColaborador())
            {
                try
                {
                    return objDLLimitePrmColaborador.BuscaLimitePrmColaborador(0,null,0,0);
                }
                catch (Exception ex)
                {
                    BLFuncoes.GravaLog("Classe: " + ex.TargetSite.ReflectedType.Name.ToString() + " Método: " + ex.TargetSite.GetMethodBody().ToString(), ref ex, 0);
                    throw ex;
                }
            }
        }

        public LimitePrmColaborador BuscaPorIdLimitePrmColaborador(int idLimiteColaborador)
        {
            using (DLLimitePrmColaborador objDLLimitePrmColaborador = new DLLimitePrmColaborador())
            {
                try
                {
                    var objRetorno = objDLLimitePrmColaborador.BuscaLimitePrmColaborador(idLimiteColaborador, null, 0,0);

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

        public IList<LimitePrmColaborador> BuscaPorFiltroLimitePrmColaborador(int idLimiteColaborador, string nmColaborador, decimal Limite, SafWeb.Model.Util.Enum.ECriterio Criterio)
        {
            using (DLLimitePrmColaborador objDLLimitePrmColaborador = new DLLimitePrmColaborador())
            {
                try
                {
                    var objRetorno = objDLLimitePrmColaborador.BuscaLimitePrmColaborador(idLimiteColaborador, nmColaborador, Limite, Criterio);
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
