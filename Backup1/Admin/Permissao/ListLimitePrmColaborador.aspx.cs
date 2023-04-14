using System;
using System.Collections.Generic;
using SafWeb.BusinessLayer.Permissao;
using SafWeb.Model.Permissao;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.WebControls;
using SafWeb.Util.Extension;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.Model.Util;
using System.Text.RegularExpressions;
using SafWeb.BusinessLayer.Filial;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Colaborador;

namespace SafWeb.UI.Admin.Permissao
{
    public partial class ListLimitePrmColaborador : FWPage
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
        protected void btnMontarNovaLimite_Click(object sender, EventArgs e)
        {

        }

        private void CarregaGridPermissao(Enums.TipoBind pintTipoBind)
        {
            try
            {
                BLLimitePrmColaborador objBLPermissao = new BLLimitePrmColaborador();

                DataTable dtt = new DataTable();

                string nmColaborador = string.Empty;
                decimal dblLimite = 0;
                int idCriterio = 0;
                if (txtColaborador.Text != string.Empty)
                {
                    nmColaborador = txtColaborador.Text;
                }
                if (txtLimite.Text != string.Empty)
                {
                    dblLimite = decimal.Parse(txtLimite.Text);
                }
                idCriterio = int.Parse(ddlcriterio.SelectedValue.ToString());
                var ListaLimite = objBLPermissao.BuscaPorFiltroLimitePrmColaborador(0, nmColaborador, dblLimite, (SafWeb.Model.Util.Enum.ECriterio)idCriterio);

                foreach (var item in ListaLimite)
                {
                    item.Limite = item.Limite / 100;
                }
                radGridLimite.DataSource = ListaLimite;
                if (pintTipoBind == Enums.TipoBind.DataBind)
                    radGridLimite.DataBind();

                //dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));
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
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync RemoverPrmColaborador(int intIdColaborador)
        {
            var ret = new RetornoAssync();
            ret.codigo = intIdColaborador;
            try
            {
                var bll = new BLLimitePrmColaborador();
                ret.erro = bll.ExcluiLimitePrmColaborador(new LimitePrmColaborador { idLimiteColaborador = intIdColaborador });
            }
            catch (Exception Ex)
            {
                ret.mensagem = "Erro ao excluir!";

                Regex ER = new Regex("conflicted.*?constraint.*?fk_", RegexOptions.IgnoreCase);

                if (ER.IsMatch(Ex.Message))
                {
                    ret.mensagem = "Existem colaboradores monitorando a quantidade de permissões com este limite. \nPara excluir o limite, os viculos com esses colaboradores devem ser removidos.";
                }

                ret.erro = true;
            }

            return ret;
        }


        /*Parte do cadastro de do listar*/
        //Adiciona Colaborador
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync SalvarPrmColaborador(SafWeb.Model.Permissao.LimitePrmColaborador objColaborador)
        {


            var ret = new RetornoAssync();
            ret.erro = false;
            ret.mensagem = "";
            ret.codigo = objColaborador.idColaborador;
            try
            {
                if ((objColaborador.idColaborador > 0) && (objColaborador.idLimite > 0))
                {

                    BLLimitePrmColaborador objBLLimitePrmColaborador = new BLLimitePrmColaborador();
                    if (objColaborador.idLimiteColaborador > 0)
                        objBLLimitePrmColaborador.EditaLimitePrmColaborador(objColaborador);
                    else
                        objBLLimitePrmColaborador.InsereLimitePrmColaborador(new LimitePrmColaborador { idColaborador = objColaborador.idColaborador, idLimite = objColaborador.idLimite });
                }

            }
            catch (Exception Ex)
            {
                ret.mensagem = "Erro ao Adicionar Colaborador!";

                Regex ER = new Regex(@"UNIQUE\sKEY.*?constraint.*?UQ_", RegexOptions.IgnoreCase);

                if (ER.IsMatch(Ex.Message))
                {
                    ret.mensagem = "Já existe um limite cadastrado para este colaborador.";
                    ret.erro = true;
                }
                else
                {

                    ret.mensagem = Ex.Message;
                    ret.erro = true;
                }
            }

            return ret;
        }

        //Popula a dll de Filial
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync PopularFilial(int IdRegional)
        {
            BLFilial objBlFilial = new BLFilial();
            var objret = new RetornoAssync();
            objret.codigo = IdRegional;
            objret.erro = false;
            objret.mensagem = "";
            try
            {
                objret.lista = (object)objBlFilial.Listar(IdRegional);
            }
            catch (Exception ex)
            {
                objret.erro = true;
                objret.mensagem = ex.Message;

            }
            return objret;
        }

        //Popula a dll de Colaborador
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync PopularColaborador(int IdFilial)
        {

            BLColaborador objBlColaborador = new BLColaborador();
            var objret = new RetornoAssync();
            objret.codigo = IdFilial;
            objret.erro = false;
            objret.mensagem = "";
            try
            {
                objret.lista = (object)objBlColaborador.BuscaPorFilialColoborador(IdFilial);
            }
            catch (Exception ex)
            {
                objret.erro = true;
                objret.mensagem = ex.Message;

            }
            return objret;
        }
    }
}