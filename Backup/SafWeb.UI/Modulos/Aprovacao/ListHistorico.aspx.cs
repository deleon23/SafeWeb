using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SafWeb.BusinessLayer.Solicitacao;
using Telerik.WebControls;
using SafWeb.Util.Extension;

namespace SafWeb.UI.Modulos.Aprovacao
{
    public partial class ListHistorico : FWPage
    {
        bool b;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.CodSolicitacao = Convert.ToInt32(Request.QueryString["Id_Solicitacao"].ToString());

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

        public int CodSolicitacao
        {
            get
            {
                if (ViewState["vsCodSolicitacao"] == null)
                {
                    ViewState["vsCodSolicitacao"] = 0;
                }
                return (int)ViewState["vsCodSolicitacao"];
            }
            set
            {
                ViewState["vsCodSolicitacao"] = value;
            }
        }

        #region Botões

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Pag_Solicitacao"]))
            {
                if (Convert.ToBoolean(Convert.ToInt32(Request.QueryString["Pag_Solicitacao"])))
                    Response.Redirect("../Solicitacao/CadSolicitacao.aspx");
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

                Solicitacoes = objBLSolicitacao.ListarHistoricoSolic(CodSolicitacao, 0);

                if (Solicitacoes.Tables[0].Rows.Count > 0)
                {
                    txtCodSolic.Text = Solicitacoes.Tables[0].Rows[0]["Id_Solicitacao"].ToString();
                    txtNomeVisitado.Text = Solicitacoes.Tables[0].Rows[0]["Nom_Visitado"].ToString();//"Augusto de Oliveira";
                    txtRE.Text = Solicitacoes.Tables[0].Rows[0]["Re_Visitado"].ToString();//"";
                    txtFilial.Text = Solicitacoes.Tables[0].Rows[0]["Alias_Filial"].ToString();//"SOA";
                    txtObservacao.Text = Solicitacoes.Tables[0].Rows[0]["Des_ObsSolicitacao"].ToString();//"";
                    txtMotivoVisita.Text = Solicitacoes.Tables[0].Rows[0]["Des_MotivoVisita"].ToString();//"Prestação de Serviço";
                    txtDataInicio.Text = Solicitacoes.Tables[0].Rows[0]["Dt_InicioVisita"].ToString();//"20/05/2009 8:00";
                    txtDataFim.Text = Solicitacoes.Tables[0].Rows[0]["Dt_FimVisita"].ToString();//"31/05/2009 18:00";
                    chkSabado.Checked = (Solicitacoes.Tables[0].Rows[0]["Flg_AcSabado"].ToString() == "1" ? true : false);
                    chkDomingo.Checked = (Solicitacoes.Tables[0].Rows[0]["Flg_AcDomingo"].ToString() == "1" ? true : false);
                    chkFeriado.Checked = (Solicitacoes.Tables[0].Rows[0]["Flg_AcFeriado"].ToString() == "1" ? true : false);

                    if (Solicitacoes.Tables[0].Rows[0]["Id_Visitante"].ToString() == string.Empty)
                    {
                        lnkVisitante.Visible = true;
                        txtNomeVisitante.Visible = false;
                        lnkVisitante.Text = Solicitacoes.Tables[0].Rows[0]["Nom_Visitante"].ToString();
                    }
                    else
                    {
                        lnkVisitante.Visible = false;
                        txtNomeVisitante.Visible = true;
                        txtNomeVisitante.Text = Solicitacoes.Tables[0].Rows[0]["Nom_Visitante"].ToString();//"";
                    }
                    txtTipoDocumento.Text = Solicitacoes.Tables[0].Rows[0]["TipDoc_Visitante"].ToString();//"RG";
                    txtDocumento.Text = Solicitacoes.Tables[0].Rows[0]["Doc_Visitante"].ToString();//"40.256.148-1";
                    txtEmpresa.Text = Solicitacoes.Tables[0].Rows[0]["Emp_Visitante"].ToString();//"Makesys Fábrica de Software";
                    txtTipoVisitante.Text = Solicitacoes.Tables[0].Rows[0]["Tip_Visitante"].ToString();//"Visitante";
                    txtPlaca.Text = Solicitacoes.Tables[0].Rows[0]["Des_Placa"].ToString();
                    txtPlaca.Visible = true;
                    lnkPlaca.Visible = false;
                    if (string.IsNullOrEmpty(Solicitacoes.Tables[0].Rows[0]["Des_Placa"].ToString()) && Solicitacoes.Tables[4].Rows.Count > 0)
                    {
                        txtPlaca.Visible = false;
                        lnkPlaca.Visible = true;
                        lnkPlaca.Text = Solicitacoes.Tables[4].Rows[0]["Des_VeiculoLista"].ToString();
                }
                }
                if (Solicitacoes.Tables[3].Rows.Count > 0)
                {
                    txtObservacaoReprovacao.Text = Solicitacoes.Tables[3].Rows[0]["Des_MotivoReprovacao"].ToString();
                }
                pnlObservacao.Visible = (txtObservacaoReprovacao.Text != "" ? true : false);

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

        private void AbreRadWindow(int pintCodLista, int pintCodSolicitacao, eListaTipo listTipo)
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.None;

            rwdWindow.Width = Unit.Pixel(530);
            rwdWindow.Height = Unit.Pixel(400);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            switch (listTipo)
            {
                case eListaTipo.Visitantes:
            rwdWindow.Title = "Lista de Visitantes";
            rwdWindow.NavigateUrl = "ListSolicitacaoLista.aspx?CodSolicitacao=" + pintCodSolicitacao.ToString() + "&CodLista=" + pintCodLista.ToString();
                    break;
                case eListaTipo.Veiculos:
                    rwdWindow.Title = "Lista de Veículos";
                    rwdWindow.NavigateUrl = "ListSolicitacaoListaVeiculos.aspx?CodSolicitacao=" + pintCodSolicitacao.ToString() ;
                    break;
            }


            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlLista = null;

            //Tenta encontrar na master
            pnlLista = (Panel)this.FindControl("pnlLista");
            pnlLista.Controls.Add(rwmWindowManager);
        }


        private enum eListaTipo
        {
            Visitantes,
            Veiculos
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

        #region LinkButton

        protected void lnkVisitante_Click(object sender, EventArgs e)
        {
            AbreRadWindow(Convert.ToInt32(Solicitacoes.Tables[0].Rows[0]["Id_ColaboradorLista"].ToString()), Convert.ToInt32(txtCodSolic.Text), eListaTipo.Visitantes);
        }

        protected void lnkPlaca_Click(object sender, EventArgs e)
        {
            AbreRadWindow(0, txtCodSolic.Text.ToInt32(), eListaTipo.Veiculos);
        }


        #endregion
    }
}
