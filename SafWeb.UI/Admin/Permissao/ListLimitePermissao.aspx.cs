using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SafWeb.BusinessLayer.Permissao;
using SafWeb.Model.Permissao;
using Telerik.WebControls;
using SafWeb.Util.Extension;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.Model.Util;
using System.Text.RegularExpressions;
using FrameWork.Model.Utilitarios;

namespace SafWeb.UI.Admin.Permissao
{
    public partial class ListLimitePermissao : FWPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                CarregaGridPermissao(Enums.TipoBind.DataBind);
            }
        }
        protected void btnBuscarList_Click(object sender, EventArgs e)
        {
            CarregaGridPermissao(Enums.TipoBind.DataBind);
        }

        private void CarregaGridPermissao(Enums.TipoBind pintTipoBind)
        {
            try
            {
                BLLimitePermissao objBLPermissao = new BLLimitePermissao();


                int intCodLimite = 0;
                double dblLimite = 0;
                int idCriterio = 0;
                if (txtCodLimite.Text != string.Empty)
                {
                    intCodLimite = int.Parse(txtCodLimite.Text);
                }
                if (txtLimite.Text != string.Empty)
                {
                    dblLimite = double.Parse(txtLimite.Text);
                }
                idCriterio = int.Parse(ddlcriterio.SelectedValue.ToString());

                var limites = objBLPermissao.BuscaPorFiltroLimitePermissao(intCodLimite, dblLimite, (SafWeb.Model.Util.Enum.ECriterio)idCriterio);

                foreach (var item in limites)
                {
                    item.Limite = item.Limite / 100;
                }

                radGridLimite.DataSource = limites;
                if (pintTipoBind == Enums.TipoBind.DataBind)
                    radGridLimite.DataBind();

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }


        protected void radGridLimite_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                CarregaGridPermissao(Enums.TipoBind.SemDataBind);
            }
        }

        protected void radGridLimite_ItemCommand(object source, GridCommandEventArgs e)
        {

        }

        protected void radGridLimite_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }


        private void EditarLimites(int intIdLimite)
        {
            string strParametro = string.Empty;
            strParametro = BLEncriptacao.EncQueryStr("TrocaHor," + Convert.ToInt32(intIdLimite));
            this.Response.Redirect("CadTrocaEscalaHorario.aspx?mod=" + strParametro, false);

        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync RemoverLimites(int intIdLimite)
        {
            var ret = new RetornoAssync();
            ret.codigo = intIdLimite;
            try
            {
                var bll = new BLLimitePermissao();
                ret.erro = bll.ExcluiLimitePermissao(new LimitePermissao { idLimite = intIdLimite });
            }
            catch (Exception Ex)
            {
                ret.mensagem = "Erro ao excluir!";

                Regex ER = new Regex("conflicted.*?constraint.*?fk_", RegexOptions.IgnoreCase);

                if (ER.IsMatch(Ex.Message))
                {
                    ret.mensagem = "Existem colaboradores monitorando a quantidade de permissões com este limite. \nPara excluir o limite, os vinculos com esses colaboradores devem ser removidos.";
                }

                ret.erro = true;
            }

            return ret;
        }


        /*Parte do cadastro de do listar*/
        //Adiciona Colaborador
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync SalvarLimitePermissao(SafWeb.Model.Permissao.LimitePermissao objLimitePermissao)
        {


            var ret = new RetornoAssync();
            ret.erro = false;
            ret.mensagem = "";
            ret.codigo = objLimitePermissao.idLimite;
            try
            {
                if (objLimitePermissao.Limite > 0)
                {

                    BLLimitePermissao objBLLimitePermissao = new BLLimitePermissao();
                    if (objLimitePermissao.idLimite > 0)
                        objBLLimitePermissao.EditaLimitePermissao(objLimitePermissao);
                    else
                        objBLLimitePermissao.InsereLimitePermissao(new LimitePermissao { Limite = objLimitePermissao.Limite });
                }

            }
            catch (Exception Ex)
            {
                ret.mensagem = Ex.Message;
                ret.erro = true;
            }

            return ret;
        }

    }
}
