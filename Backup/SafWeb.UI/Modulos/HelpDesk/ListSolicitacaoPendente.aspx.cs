using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Idioma;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Permissao;
using SafWeb.BusinessLayer.Regional;
using SafWeb.Model.Permissao;
using SafWeb.Model.Util;
using SafWeb.Util.Extension;
using SafWeb.Model.Colaborador;
using System.Text.RegularExpressions;
using SafWeb.BusinessLayer.Solicitacao;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.HelpDesk;
using SafWeb.Util.Extension;


namespace SafWeb.UI.Modulos.HelpDesk
{
    public partial class ListSolicitacaoPendente : FWPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                PopularRegional();
                PopularTipoSolicitacao();
                CarregaGridSolicitacao(Enums.TipoBind.DataBind);
            }
        }

        private void CarregaGridSolicitacao(Enums.TipoBind pintTipoBind)
        {
            try
            {
                int piniIdSolicitacao = txtNumSolicitacao.Text.ToInt32();
                int pintIdRegiona = ddlRegional.SelectedValue.ToInt32();
                int pintIdFilial = ddlFilial.SelectedValue.ToInt32();
                string pstrNomeColaborador = txtColaborador.Text.Trim();
                int pintIdTipoSolicitacao = ddlTipoSolicitacao.SelectedValue.ToInt32();
                BLHelpDeskSolCrachaTitular objBLHelpDesk = new BLHelpDeskSolCrachaTitular();

                radGridSolicitacao.DataSource = objBLHelpDesk.ListarCrachaTitularPendentes(piniIdSolicitacao, pintIdRegiona, pintIdFilial, pstrNomeColaborador, pintIdTipoSolicitacao);

                //radGridSolicitacao.DataSource = objBLHelpDesk.BuscaPorFiltroLimitePrmColaborador(0, nmColaborador, dblLimite, (SafWeb.Model.Util.Enum.ECriterio)idCriterio);
                if (pintTipoBind == Enums.TipoBind.DataBind)
                    radGridSolicitacao.DataBind();

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        //Popula a ddl de Regional
        protected void PopularRegional()
        {
            BLRegional objBlRegional = new BLRegional();
            Collection<SafWeb.Model.Regional.Regional> colRegional;

            try
            {
                colRegional = objBlRegional.Listar();

                ddlRegional.DataSource = colRegional;
                ddlRegional.DataTextField = "DescricaoRegional";
                ddlRegional.DataValueField = "IdRegional";
                ddlRegional.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegional, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
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

        //Popula a ddl de Regional
        protected void PopularTipoSolicitacao()
        {

            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colTipoSolicitacao;

            try
            {

                colTipoSolicitacao = objBlSolicitacao.ListarTipoSolicitacaoGrupo(BusinessLayer.Solicitacao.Enumeradores.ETipoSolicitacaoGrupo.Permissao);

                ddlTipoSolicitacao.DataSource = colTipoSolicitacao;
                ddlTipoSolicitacao.DataTextField = "Descricao";
                ddlTipoSolicitacao.DataValueField = "Codigo";
                ddlTipoSolicitacao.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoSolicitacao, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CarregaGridSolicitacao(Enums.TipoBind.DataBind);
        }

        //Popula a dll de Filial
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync CriptografarParametro(string strParametros)
        {
            var objret = new RetornoAssync();
            objret.codigo = 0;
            objret.erro = false;
            objret.mensagem = "";
            try
            {
                objret.lista = BLEncriptacao.EncQueryStr(strParametros);
            }
            catch (Exception ex)
            {

            }
            return objret;
        }

    }
}

