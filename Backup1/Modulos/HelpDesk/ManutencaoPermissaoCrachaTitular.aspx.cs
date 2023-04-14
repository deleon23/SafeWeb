using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.BusinessLayer.Permissao;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.Model.Util;
using System.Collections.Generic;
using SafWeb.BusinessLayer.HelpDesk;

namespace SafWeb.UI.Modulos.HelpDesk
{
    public partial class ManutencaoPermissaoCrachaTitular : FWPage
    {
        bool b;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                this.Id_SolicitacaoCrachaTitular = Convert.ToInt32(BLEncriptacao.DecQueryStr(Request.QueryString["mod"].ToString()));
                PopulaListPermissaoRonda();
                PopulaListPermissaoNaoUtilizadaRonda();
                this.PopulaHistoricoSolicitacao();
            }
        }

        public DataSet Solicitacoes
        {
            get
            {
                if (ViewState["vsSolicitacoes"] == null)
                {
                    ViewState["vsSolicitacoes"] = null;
                }
                return (DataSet)ViewState["vsSolicitacoes"];
            }
            set
            {
                ViewState["vsSolicitacoes"] = value;
            }
        }

        public int Id_SolicitacaoCrachaTitular
        {
            get
            {
                if (ViewState["vsId_SolicitacaoCrachaTitular"] == null)
                {
                    ViewState["vsId_SolicitacaoCrachaTitular"] = 0;
                }
                return (int)ViewState["vsId_SolicitacaoCrachaTitular"];
            }
            set
            {
                ViewState["vsId_SolicitacaoCrachaTitular"] = value;
            }
        }

        #region Botões

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Pag_Solicitacao"]))
            {
                if (Convert.ToBoolean(Convert.ToInt32(Request.QueryString["Pag_Solicitacao"])))
                    Response.Redirect("../Solicitacao/CadSolicitacaoPermissaoCrachaTitular.aspx");
            }

            else if (Convert.ToString(Request.QueryString["Hist"]) == "true")
            {
                Response.Redirect("CadCaixaEntrada.aspx?Hist=true");
            }
            else
            {
                Response.Redirect("CadCaixaEntrada.aspx");
            }
        }

        #endregion

        #region Métodos

        private void PopulaHistoricoSolicitacao()
        {
            BLSolicitacao objBLSolicitacao = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();

                Solicitacoes = objBLSolicitacao.ListarHistoricoCrachaTitular(Id_SolicitacaoCrachaTitular);

                if (Solicitacoes.Tables[0].Rows.Count > 0)
                {
                    txtCodSolic.Text = Solicitacoes.Tables[0].Rows[0]["Id_SolicitacaoCrachaTitular"].ToString();
                    txtNomeVisitado.Text = Solicitacoes.Tables[0].Rows[0]["NOM_COLABORADOR"].ToString();
                    txtRE.Text = Solicitacoes.Tables[0].Rows[0]["COD_COLABORADOR"].ToString();

                    txtFilial.Text = Solicitacoes.Tables[0].Rows[0]["Alias_Filial"].ToString();
                    txtObservacao.Text = Solicitacoes.Tables[0].Rows[0]["DES_MOTIVOSOLICITACAO"].ToString();

                    txtDataInicio.Text = Solicitacoes.Tables[0].Rows[0]["DT_SOLICITACAO"].ToString();
                }
                if (Solicitacoes.Tables[3].Rows.Count > 0)
                {
                    txtObservacaoReprovacao.Text = Solicitacoes.Tables[3].Rows[0]["Des_MotivoReprovacao"].ToString();
                }
                //pnlObservacao.Visible = (txtObservacaoReprovacao.Text != "" ? true : false);

                if (!string.IsNullOrEmpty(txtObservacaoReprovacao.Text))
                {
                    lblObservacaoReprovacao.Visible = true;
                    txtObservacaoReprovacao.Visible = true;
                }

                lstAreaVisita.DataTextField = "Des_Area";
                lstAreaVisita.DataValueField = "Id_Area";
                lstAreaVisita.DataSource = Solicitacoes.Tables[1];
                lstAreaVisita.DataBind();

                radSolicitacoes.DataSource = Solicitacoes.Tables[2];
                radSolicitacoes.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region RadGrid

        protected void radSolicitacoes_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                radSolicitacoes.DataSource = Solicitacoes.Tables[2];
            }
        }

        protected void radSolicitacoes_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "IrPagina")
            {
                string strPageIndexString = ((TextBox)e.Item.FindControl("txtPagina")).Text;
                int intPageIndex = e.Item.OwnerTableView.CurrentPageIndex;
                if ((!object.ReferenceEquals(strPageIndexString.Trim(), string.Empty)))
                {
                    try
                    {
                        if (e.Item.OwnerTableView.PageCount < Convert.ToInt32(strPageIndexString))
                        {
                            strPageIndexString = (e.Item.OwnerTableView.PageCount).ToString();
                            ((TextBox)e.Item.FindControl("txtPagina")).Text = strPageIndexString;
                        }
                        intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                    }
                }

                if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                {
                    e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                    e.Item.OwnerTableView.Rebind();
                }
            }
        }

        #endregion

        #region ListPermissaoRonda

        protected void PopulaListPermissaoNaoUtilizadaRonda()
        {
            try
            {
                BLPermissaoRonda objBLPermissaoRonda = new BLPermissaoRonda();
                lstPermissaoNaoUtilizadas.DataTextField = "desPermissao";
                lstPermissaoNaoUtilizadas.DataValueField = "idPermissao";
                lstPermissaoNaoUtilizadas.DataSource = objBLPermissaoRonda.BuscaPermissaoNaoUtilizadaRonda(0);
                lstPermissaoNaoUtilizadas.DataBind();

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void PopulaListPermissaoRonda()
        {
            try
            {
                BLPermissaoRonda objBLPermissaoRonda = new BLPermissaoRonda();
                lstPermissao.DataTextField = "desPermissao";
                lstPermissao.DataValueField = "idPermissao";
                lstPermissao.DataSource = objBLPermissaoRonda.BuscaPermissaoRonda(0);
                lstPermissao.DataBind();

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region ListGrupoColetores

        //Popula a dll de Filial
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync PopularGrupoCorretoresRonda(int IdPermissao)
        {
            BLGrupoColetoresRonda objBLGrupoColetoresRonda = new BLGrupoColetoresRonda();
            var objret = new RetornoAssync();
            objret.codigo = IdPermissao;
            objret.erro = false;
            objret.mensagem = "";
            try
            {
                objret.lista = (object)objBLGrupoColetoresRonda.BuscaGrupoColetores(IdPermissao);
            }
            catch (Exception ex)
            {
                objret.erro = true;
                objret.mensagem = ex.Message;

            }
            return objret;
        }

        #endregion

        #region BtnSalvar
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync SalvarGrupoColetor(int idPermissao, string strDesPermissao, string strIdGrupoColetores)
        {
            //EditaPermissaoRONDA

            BLPermissaoRonda objBLPermissaoRonda = new BLPermissaoRonda();
            var objret = new RetornoAssync();
            objret.codigo = 0;
            objret.erro = false;
            objret.mensagem = "";
            try
            {
                if (idPermissao > 0)
                {
                    var objPermissao = objBLPermissaoRonda.EditaPermissaoRONDA(idPermissao, strDesPermissao, strIdGrupoColetores);
                    objret.codigo = objPermissao.idPermissao;
                    objret.lista = objBLPermissaoRonda.BuscaPermissaoRonda(objPermissao.idPermissao);
                }
                else
                {
                    var objPermissao = objBLPermissaoRonda.InserePermissaoRONDA(strDesPermissao, strIdGrupoColetores);
                    objret.codigo = objPermissao.idPermissao;
                    objret.lista = objBLPermissaoRonda.BuscaPermissaoRonda(objPermissao.idPermissao);
                }
            }
            catch (Exception ex)
            {
                objret.erro = true;
                objret.mensagem = ex.Message;

            }
            return objret;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static RetornoAssync SalvarSolicitacao(int idSolicitacao, int idPermissao)
        {
            //EditaPermissaoRONDA

            BLHelpDeskSolCrachaTitular objBLHelpDeskSolCrachaTitular = new BLHelpDeskSolCrachaTitular();
            var objret = new RetornoAssync();
            objret.codigo = idSolicitacao;
            objret.erro = false;
            objret.mensagem = "Permissão executada!";
            try
            {
                if (objBLHelpDeskSolCrachaTitular.AdicionarPermissaoCrachaTitularColaborador(idSolicitacao, idPermissao))
                {
                    objret.erro = true;
                    objret.mensagem = "Não foi possível executar a liberação.";
                }

            }
            catch (Exception ex)
            {
                objret.erro = true;
                objret.mensagem = ex.Message;

            }
            return objret;
        }

        #endregion
    }
}
