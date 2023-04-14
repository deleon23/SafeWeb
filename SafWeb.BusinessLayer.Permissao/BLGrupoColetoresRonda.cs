using System;
using System.Collections.Generic;
using System.Text;
using SafWeb.Model.Permissao;
using SafWeb.DataLayer.Permissao;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.BusinessLayer.Permissao
{
    public class BLGrupoColetoresRonda
    {

        public List<GrupoColetoresRonda> BuscaGrupoColetores(int idPermissao)
        {
            using (DLGrupoColetoresRonda objDLPermissao = new DLGrupoColetoresRonda())
            {
                try
                {
                    return objDLPermissao.BuscaGrupoColaboradoresRonda(idPermissao);
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
