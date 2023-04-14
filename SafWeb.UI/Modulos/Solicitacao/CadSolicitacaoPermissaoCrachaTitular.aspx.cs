using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Idioma;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Area;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.Model.Colaborador;
using SafWeb.Model.Solicitacao;
using Telerik.WebControls;

namespace SafWeb.UI.Modulos.Solicitacao
{
    public partial class CadSolicitacaoPermissaoCrachaTitular : FWPage
    {
        private Colaborador gobjColaborador;
        private SafWeb.Model.Solicitacao.PermissaoCrachaTitular gobjSolicitacao;
        private SafWeb.Model.Solicitacao.PermissaoCrachaTitular gobjSolicitacaoColaborador;

        protected void Page_Load(object sender, EventArgs e)
        {
            VerificarHiddenColaboradores();
            
            if (!Page.IsPostBack)
            {
                this.ControlaPaineis(Enums.TipoPainel.Listagem);
                this.VerificaPermissoes();
                this.InicializaScripts();
            }
            
        }

        #region Inicializa Scripts

        protected void InicializaScripts()
        {
            //seleciona a regional e a filial do usuário logado.
            BLColaborador objBlColaborador = new BLColaborador();
            DataTable dtt = new DataTable();

            try
            {
                dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                this.IdRegional = Convert.ToInt32(dtt.Rows[0][0].ToString());
                this.IdFilial = Convert.ToInt32(dtt.Rows[0][1].ToString());
            }
            catch (Exception)
            {
                throw;
            }


            lblMensagem.Visible = false;
            
            this.PopularRegional();
            this.PopularFilial(ref ddlRegionalCad, ref ddlFilialCad);
            this.PopularFilial(ref ddlRegional, ref ddlFilial);

            this.PopularStatus();
            this.PopularTipoSolicitacao();
            
            this.BindSolicitacao(Enums.TipoBind.DataBind);

            txtDataInicio.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtDataFim.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtObservacao.Attributes.Add("onkeypress", "return blocTexto(this.value);");

            BLUtilitarios.InseriMensagemDropDownList(ref ddlAprovadorCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            //BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            //BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
        }

        #endregion

        #region Controla Painéis

        protected void ControlaPaineis(Enums.TipoPainel pintPainel)
        {
            if (pintPainel == Enums.TipoPainel.Listagem)
            {
                pnlListagem.Visible = true;
                pnlCadastro.Visible = false;
            }
            else
            {
                pnlCadastro.Visible = true;
                pnlListagem.Visible = false;
            }

        }

        #endregion

        #region Permissões

        protected void VerificaPermissoes()
        {
            btnIncluir.Enabled = Permissoes.Inclusão();
        }

        #endregion

        #region Popular Combos

        /// <summary>
        ///     Popula os combos com as regionais
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
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

                ddlRegionalCad.DataSource = colRegional;
                ddlRegionalCad.DataTextField = "DescricaoRegional";
                ddlRegionalCad.DataValueField = "IdRegional";
                ddlRegionalCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegional, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegionalCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                BLUtilitarios.ConsultarValorCombo(ref ddlRegional, this.IdRegional.ToString());
                BLUtilitarios.ConsultarValorCombo(ref ddlRegionalCad, this.IdRegional.ToString());
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com as filiais da regional selecionada
        /// </summary>
        /// <param name="ddlRegional">Regional</param>
        /// <param name="ddlFilial">Filial</param>
        /// <history>
        ///     [mribeiro] created 01/07/2009
        /// </history>
        protected void PopularFilial(ref DropDownList ddlRegional, ref DropDownList ddlFilial)
        {
            BLFilial objBlFilial = new BLFilial();
            Collection<SafWeb.Model.Filial.Filial> colFilial;

            try
            {
                if (ddlRegional.SelectedIndex != 0)
                {
                    colFilial = objBlFilial.Listar(Convert.ToInt32(ddlRegional.SelectedItem.Value));

                    ddlFilial.DataSource = colFilial;
                    ddlFilial.DataTextField = "AliasFilial";
                    ddlFilial.DataValueField = "IdFilial";
                    ddlFilial.DataBind();

                    //ddlFilial.Enabled = true;
                }
                else
                {
                    ddlFilial.Enabled = false;
                    ddlFilial.SelectedIndex = 0;
                    lstAreaSelecionada.Items.Clear();
                    lstAreaVisita.Items.Clear();
                }

                BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                BLUtilitarios.ConsultarValorCombo(ref ddlFilial, this.IdFilial.ToString());
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula o combo com os tipos de solicitações
        /// </summary>
        /// <history>
        ///     [mribeiro] created 06/07/2009 
        ///</history>
        protected void PopularTipoSolicitacao()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colTipoSolicitacao;

            try
            {
                colTipoSolicitacao = objBlSolicitacao.ListarTipoSolicitacaoGrupo(SafWeb.BusinessLayer.Solicitacao.Enumeradores.ETipoSolicitacaoGrupo.Permissao);


                this.ddlTipoSolicitacao.DataSource = colTipoSolicitacao;
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

        /// <summary>
        ///     Popula o combo com os status da solicitação
        /// </summary>
        /// <history>
        ///     [mribeiro] created 06/07/2009 
        ///</history>
        protected void PopularStatus()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.Status> colStatus;

            try
            {
                colStatus = objBlSolicitacao.ListarStatus();

                ddlStatus.DataSource = colStatus;
                ddlStatus.DataTextField = "Descricao";
                ddlStatus.DataValueField = "Codigo";
                ddlStatus.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlStatus, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com as areas de uma filial
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
        protected void PopularArea()
        {
            BLArea objBlArea = new BLArea();
            Collection<SafWeb.Model.Area.Area> colArea;

            try
            {
                if (ddlFilialCad.SelectedIndex > 0)
                {
                    colArea = objBlArea.ListarAreaSeg(Convert.ToInt32(ddlFilialCad.SelectedItem.Value),EAreasSeguranca.Todas);

                    lstAreaVisita.DataSource = colArea;
                    lstAreaVisita.DataTextField = "Descricao";
                    lstAreaVisita.DataValueField = "Codigo";
                    lstAreaVisita.DataBind();
                }
                else
                {
                    lstAreaVisita.Items.Clear();
                }

                lstAreaSelecionada.Items.Clear();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula o combo com os possíveis aprovadores
        /// </summary>
        /// <history>
        ///     [mribeiro] 15/07/2009 created
        /// </history>
        protected void PopularAprovadores()
        {
            string strRegional = "", strFilial = "", strTipoSolicitacao = "";

            for (int i = 0; i < this.lstGrid.Count; i++)
            {
                if (i == 0)
                {
                    strRegional = this.lstGrid[i].Id_Regional.ToString();
                    strFilial = this.lstGrid[i].Id_Filial.ToString();
                    strTipoSolicitacao = this.lstGrid[i].Id_TipoSolicitacao.ToString();
                }
                else
                {
                    strRegional += "," + this.lstGrid[i].Id_Regional.ToString();
                    strFilial += "," + this.lstGrid[i].Id_Filial.ToString();
                    strTipoSolicitacao += "," + this.lstGrid[i].Id_TipoSolicitacao.ToString();
                }
            }

            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<Aprovador> colAprovador = new Collection<Aprovador>();

            try
            {
                colAprovador = objBlSolicitacao.ListarAprovadores(strRegional,
                                                                  strFilial,
                                                                  strTipoSolicitacao);

                ddlAprovadorCad.DataSource = colAprovador;
                ddlAprovadorCad.DataTextField = "NomeUsuario";
                ddlAprovadorCad.DataValueField = "IdUsuario";
                ddlAprovadorCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlAprovadorCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                txtREAprovador.Text = string.Empty;

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Listagem

        #region Bind Solicitações


        /// <history>
        ///     [no history]
        ///     [haguiar] 17/06/2011 11:42
        /// </history>
        private void BindSolicitacao(Enums.TipoBind pintTipoBind)
        {
            BLSolicitacao objBLSolicitacao;
            Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular> colSolicitacoes;

            try
            {
                objBLSolicitacao = new BLSolicitacao();
                colSolicitacoes = new Collection<SafWeb.Model.Solicitacao.PermissaoCrachaTitular>();

                colSolicitacoes = objBLSolicitacao.ListarSolicitacoesCrachaTitular(Convert.ToInt32(BLAcesso.IdUsuarioLogado()),
                                                                    (txtNumSolicitacao.Text != "" ? Convert.ToInt32(txtNumSolicitacao.Text) : 0),
                                                                      (ddlRegional.SelectedIndex > 0 ? Convert.ToInt32(ddlRegional.SelectedValue) : 0),
                                                                      (ddlFilial.SelectedIndex > 0 ? Convert.ToInt32(ddlFilial.SelectedValue) : 0),
                                                                      (ddlStatus.SelectedIndex > 0 ? Convert.ToInt32(ddlStatus.SelectedValue) : -1),
                                                                      (ddlTipoSolicitacao.SelectedIndex > 0 ? Convert.ToInt32(ddlTipoSolicitacao.SelectedValue) : 0),
                                                                      (txtDataInicio.Text != "" ? (DateTime?)Convert.ToDateTime(txtDataInicio.Text) : null),
                                                                      (txtDataFim.Text != "" ? (DateTime?)Convert.ToDateTime(txtDataFim.Text) : null),
                                                                      this.txtNomeColaborador.Text,
                                                                      txtNomeAprovador.Text
                                                                      );
                
                this.radGridSolicitacao.DataSource = colSolicitacoes;

                if (pintTipoBind == Enums.TipoBind.DataBind)
                {
                    this.radGridSolicitacao.DataBind();
                }
            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Botões

        /// <summary>
        ///     Lista as solicitações conforme filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BindSolicitacao(Enums.TipoBind.DataBind);
        }

        /// <summary>
        ///     Abre a tela de cadastro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(Enums.TipoPainel.Cadastro);
            this.BindModel(Enums.TipoTransacao.Novo);

        }

        #endregion

        #region DataGrid

        #region NeedDataSource

        protected void radGridSolicitacao_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindSolicitacao(Enums.TipoBind.SemDataBind);
            }
        }

        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridSolicitacao
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        ///     [haguiar] modified 27/04/2011
        ///     alterar área para codigo de area+flg_areati
        /// </history>        
        protected void radGridSolicitacao_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Visualizar")
            {
                Response.Redirect("../Aprovacao/ListHistoricoCrachaTitular.aspx?Id_SolicitacaoCrachaTitular=" + e.Item.Cells[2].Text
                    + "&Pag_Solicitacao=1");
            }

            if (e.CommandName.Trim() == "Ativar")
            {
                
                bool blnFlgSituacao = false;
                int intIdSolicitacao = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                try
                {

                    BLSolicitacao objBlSolicitacao = new BLSolicitacao();

                    //altera o botão
                    if (btnAtivar.ToolTip == "Ativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        blnFlgSituacao = true;
                    }
                    else if (btnAtivar.ToolTip == "Inativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        blnFlgSituacao = false;
                    }

                    objBlSolicitacao.AlterarSituacaoCrachaTitular(intIdSolicitacao,
                                                     blnFlgSituacao,
                                                     Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                    //altera o status
                    if (blnFlgSituacao)
                    {
                        radGridSolicitacao.Items[e.Item.ItemIndex]["Des_StatusAprovacao"].Text = objBlSolicitacao.ObterUltimoStatusCrachaTitular(intIdSolicitacao);
                    }
                    else
                    {
                        radGridSolicitacao.Items[e.Item.ItemIndex]["Des_StatusAprovacao"].Text = "Cancelada";
                    }
                    
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }

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

                        //digitou 0, volta para a primeira página
                        if (Convert.ToInt32(strPageIndexString) == 0)
                        {
                            ((TextBox)e.Item.FindControl("txtPagina")).Text = "1";
                            intPageIndex = 0;
                        }
                        else
                        {
                            intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
                        }
                    
                    }
                    catch
                    {
                        //process incorrect input here if needed 
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

        #region ItemDataBound

        protected void radGridSolicitacao_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnVisualizar;
                btnVisualizar = (ImageButton)e.Item.FindControl("imgVisualizar");

                Telerik.WebControls.GridDataItem dataItemEditar = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnEditar = (ImageButton)dataItemEditar["Editar"].Controls[0];

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                if ((btnAtivar != null))
                {
                    btnAtivar.Visible = false;

                    if (Permissoes.Alteração())
                    {
                        BLSolicitacao objBlSolicitacao;
                        DataTable dttAprovadores = new DataTable();

                        try
                        {
                            objBlSolicitacao = new BLSolicitacao();

                            dttAprovadores = objBlSolicitacao.ObterAprovadoresCrachaTitular(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id_SolicitacaoCrachaTitular")));

                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }

                        if (btnEditar != null)
                        {
                            btnEditar.Visible = false;
                            int intIdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                            for (int i = 0; i < dttAprovadores.Rows.Count; i++)
                            {
                                if (intIdUsuario == Convert.ToInt32(dttAprovadores.Rows[i][0]))
                                {
                                    btnAtivar.Visible = true;

                                    if (intIdUsuario == Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id_UsuarioSolicitante").ToString())) //e.Item.Cells[7].Text))
                                    {
                                        btnVisualizar.Visible = false;
                                        e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                                        e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                                        e.Item.Style["cursor"] = "hand";
                                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);

                                        // Cursor 
                                        int intCell = e.Item.Cells.Count;
                                        for (int @int = 0; @int <= intCell - 3; @int++)
                                        {
                                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnVisualizar, ""));
                                        }
                                    }

                                    if (Permissoes.Alteração())
                                    {
                                        btnEditar.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                                        btnEditar.Visible = false; //true;

                                        btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_SolicitacaoCrachaTitular").ToString();
                                    }

                                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Situacao")))
                                    {
                                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                        btnAtivar.ToolTip = "Inativar";
                                    }
                                    else
                                    {
                                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                        btnAtivar.ToolTip = "Ativar";
                                    }
                                }
                            }
                        }
                        else
                        {
                            int intIdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                            for (int i = 0; i < dttAprovadores.Rows.Count; i++)
                            {
                                if (intIdUsuario == Convert.ToInt32(dttAprovadores.Rows[i][0]))
                                {
                                    btnAtivar.Visible = true;

                                    if (intIdUsuario == Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id_UsuarioSolicitante").ToString())) //e.Item.Cells[7].Text))
                                    {
                                        btnVisualizar.Visible = false;
                                        e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                                        e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                                        e.Item.Style["cursor"] = "hand";
                                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);

                                        // Cursor 
                                        int intCell = e.Item.Cells.Count;
                                        for (int @int = 0; @int <= intCell - 2; @int++)
                                        {
                                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnVisualizar, ""));
                                        }
                                    }

                                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Situacao")))
                                    {
                                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                        btnAtivar.ToolTip = "Inativar";
                                    }
                                    else
                                    {
                                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                        btnAtivar.ToolTip = "Ativar";
                                    }
                                }
                            }
                        }


                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_SolicitacaoCrachaTitular").ToString();
                    }
                    else if (btnEditar != null)
                    {
                        btnEditar.Visible = false;
                    }
                }
                else if (btnEditar != null)
                {
                    btnEditar.Visible = false;
                }
            }
        }

        #endregion

        #endregion

        #region Eventos

        /// <summary>
        ///     Seleciona uma filial conforme Regional selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void ddlRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularFilial(ref ddlRegional, ref ddlFilial);
        }

        #endregion

        #endregion

        #region Cadastro

        #region BindModel


        /// <summary>
        ///     Bind model da solicitação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 27/04/2011 11:48
        ///     verificar area de ti
        /// </history>
        protected void BindModel(Enums.TipoTransacao pintTipoTransacao)
        {
            if (pintTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {

                gobjSolicitacaoColaborador = new PermissaoCrachaTitular();

                List<string> arrAreas = new List<string>();
                List<string> arrCodAreas = new List<string>(); ;

                bool blnAreaSeg = false;
                bool blnAreaTI = false;

                for (int i = 0; i < lstAreaSelecionada.Items.Count; i++)
                {
                    if (!arrAreas.Contains(lstAreaSelecionada.Items[i].Text))
                        arrAreas.Add(lstAreaSelecionada.Items[i].Text);

                    if (!arrCodAreas.Contains(lstAreaSelecionada.Items[i].Value.Substring(0, lstAreaSelecionada.Items[i].Value.Length - 2)))
                        arrCodAreas.Add(lstAreaSelecionada.Items[i].Value.Substring(0, lstAreaSelecionada.Items[i].Value.Length - 2));

                    if (lstAreaSelecionada.Items[i].Value.Substring(lstAreaSelecionada.Items[i].Value.Length - 2, 1) == "1")
                        blnAreaSeg = true;

                    if (lstAreaSelecionada.Items[i].Value.Substring(lstAreaSelecionada.Items[i].Value.Length - 1, 1) == "1")
                        blnAreaTI = true;
                    }

                string strAreas = string.Join(",", arrAreas.ToArray());
                string strCodAreas = string.Join(",", arrCodAreas.ToArray());

                gobjSolicitacaoColaborador.Id_Colaboradores = string.Empty;

                foreach (ListItem liColaborador in lstColaboradores.Items)
                {
                    if (gobjSolicitacaoColaborador.Id_Colaboradores == string.Empty)
                    {
                        gobjSolicitacaoColaborador.Id_Colaboradores += liColaborador.Value;
                        gobjSolicitacaoColaborador.Nom_Colaboradores += liColaborador.Text;
                    }
                    else
                    {
                        gobjSolicitacaoColaborador.Id_Colaboradores += "," + liColaborador.Value;
                        gobjSolicitacaoColaborador.Nom_Colaboradores += "," +  liColaborador.Text;
                    }
                }

                if (!blnAreaSeg && !blnAreaTI)
                gobjSolicitacaoColaborador.Id_TipoSolicitacao = 13;

                if (blnAreaSeg && !blnAreaTI)
                    gobjSolicitacaoColaborador.Id_TipoSolicitacao = 14;

                if (!blnAreaSeg && blnAreaTI)
                    gobjSolicitacaoColaborador.Id_TipoSolicitacao = 15;

                if (blnAreaSeg && blnAreaTI)
                    gobjSolicitacaoColaborador.Id_TipoSolicitacao = 16;


                gobjSolicitacaoColaborador.Id_Regional = Convert.ToInt32(ddlRegionalCad.SelectedItem.Value);
                gobjSolicitacaoColaborador.Id_Filial = Convert.ToInt32(ddlFilialCad.SelectedItem.Value);
                gobjSolicitacaoColaborador.Alias_Filial = ddlFilialCad.SelectedItem.Text;

                gobjSolicitacaoColaborador.Id_Area = strCodAreas;
                gobjSolicitacaoColaborador.Des_Area = strAreas;
                gobjSolicitacaoColaborador.Des_MotivoSolicitacao = txtObservacao.Text;
                gobjSolicitacaoColaborador.Data_Solicitacao = DateTime.Now;
            }
            else if (pintTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
            }
            else if (pintTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.HiddenColaborador.Value = string.Empty;
                this.Id_Colaborador = 0;
                this.IdSolicitacao = 0;

                this.lstColaboradores.Items.Clear();
                this.PopularArea();

                txtObservacao.Text = string.Empty;
                ddlAprovadorCad.Enabled = false;
                ddlAprovadorCad.SelectedIndex = 0;
                txtREAprovador.Text = string.Empty;
                btnAdicionar.Enabled = true;
                lblMensagem.Visible = false;

                this.lstGrid.Clear();
                this.radGridColaboradores.DataBind();
            }
        }

        #endregion

        #region Bind Colaborador

        private void BindColaborador(Enums.TipoBind pintTipoBind)
        {
            radGridColaboradores.DataSource = this.lstGrid;

            if (pintTipoBind == Enums.TipoBind.DataBind)
            {
                radGridColaboradores.DataBind();
            }
        }

        #endregion

        #region DataGrid

        #region NeedDataSource

        protected void radGridColaboradores_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindColaborador(Enums.TipoBind.SemDataBind);
            }
        }

        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridColaboradores_ItemCommand
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 14/03/2011
        /// </history>  
        protected void radGridColaboradores_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Excluir")
            {
                for (int i = 0; i < this.lstGrid.Count; i++)
                {
                    if (this.lstGrid[i].Id_Colaboradores == radGridColaboradores.Items[Convert.ToInt32(e.Item.ItemIndex)]["Id_Colaboradores"].Text)
                    {
                        this.lstGrid.RemoveAt(i);
                    }
                }

                if (this.lstGrid.Count == 0)
                {
                    ddlAprovadorCad.Enabled = false;
                    ddlAprovadorCad.SelectedIndex = 0;

                    if (this.IdSolicitacao > 0)
                    {
                        btnAdicionar.Enabled = true;
                    }
                }
                else
                {
                    this.PopularAprovadores();
                }

                radGridColaboradores.DataSource = this.lstGrid;
                radGridColaboradores.DataBind();
            }


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
                    catch
                    {
                        //process incorrect input here if needed 
                    }
                }

                if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                {
                    e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                    e.Item.OwnerTableView.Rebind();
                }
            }


            if (e.CommandName.Trim() == "Editar")
            {
                int i = Convert.ToInt32(e.Item.ItemIndex);

                lstAreaSelecionada.Items.Clear();
                lstAreaVisita.Items.Clear();

                this.PopularArea();

                string[] strCodArea;
                strCodArea = this.lstGrid[i].Id_Area.Split(new string[] { "," }, StringSplitOptions.None);


                for (int intCont2 = 0; intCont2 < strCodArea.Length; intCont2++)
                {
                    for (int intCont = 0; intCont <= lstAreaVisita.Items.Count - 1; intCont++)
                    {
                        if (strCodArea[intCont2] == lstAreaVisita.Items[intCont].Value)
                        {
                            lstAreaSelecionada.Items.Add(new ListItem(lstAreaVisita.Items[intCont].Text, lstAreaVisita.Items[intCont].Value));
                            lstAreaVisita.Items.RemoveAt(intCont);
                        }
                    }
                }

                

                this.lstColaboradores.Items.Clear();
                this.PopularListaColaboradores(this.lstGrid[i].Id_Colaboradores.Split(new string[] { "," }, StringSplitOptions.None));

                if (this.lstGrid[i].Des_MotivoSolicitacao != string.Empty)
                {
                    txtObservacao.Text = this.lstGrid[i].Des_MotivoSolicitacao;
                }

                this.lstGrid.RemoveAt(i);

                if (this.lstGrid.Count == 0)
                {
                    ddlAprovadorCad.Enabled = false;
                    ddlAprovadorCad.SelectedIndex = 0;
                }
                else
                {
                    this.PopularAprovadores();
                }

                radGridColaboradores.DataSource = this.lstGrid;
                radGridColaboradores.DataBind();
            }
        }

        #endregion

        #region ItemDataBound

        protected void radGridColaboradores_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");

                btnEditar.Visible = false;

                e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                e.Item.Style["cursor"] = "hand";

                // Cursor 
                int intCell = e.Item.Cells.Count;
                for (int @int = 0; @int <= intCell - 2; @int++)
                {
                    if (@int != 4)
                    {
                        e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                    }
                }
                if (Permissoes.Alteração())
                {
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                }
            }
        }

        #endregion

        #endregion

        #region Botões
        
        /// <summary>
        ///     Volta para a tela de Listagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(Enums.TipoPainel.Listagem);
            lblMensagem.Visible = false;
            this.BindSolicitacao(Enums.TipoBind.DataBind);
        }

        /// <summary>
        ///     Grava a soliciação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] created 25/02/2012 14:52
        /// </history>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            bool blnGravar = false;

        if (ddlAprovadorCad.SelectedIndex > 0 && this.lstGrid.Count > 0)
            {
                if (VerificarAprovador())
                {
                    blnGravar = true;
                }
            }
            else
            {
                blnGravar = false;
                RadAjaxPanel1.Alert("Insira pelo menos uma solicitação.");
            }            

            if (blnGravar)
                this.Gravar();
        }

        /// <summary>
        ///     Adiciona um visitante na lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            Page.Validate("Cadastro");

            if (!Page.IsValid)
                return;

            if (this.lstColaboradores.Items.Count > 0)
            {
                if (lstAreaSelecionada.Items.Count > 0)
                {                    
                        if (this.VerificarColaborador() == false)
                        {
                            this.BindModel(Enums.TipoTransacao.CarregarDados);

                            //adiciona o novo registro
                            this.lstGrid.Add(gobjSolicitacaoColaborador);
                            this.BindColaborador(Enums.TipoBind.DataBind);

                            this.PopularAprovadores();
                            ddlAprovadorCad.Enabled = true;

                            this.LimparDadosColaborador();                               
                        }                    
                }
                else
                {
                    RadAjaxPanelColaborador.Alert("Selecione pelo menos um área.");
                }
            }
            else
            {
                RadAjaxPanelColaborador.Alert("Selecione um ou mais colaboradores.");
            }
        }

        /// <summary>
        ///     Adiciona todas as areas no listBox AreasSelecionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnAddTodos_Click(object sender, EventArgs e)
        {
            for (int i = lstAreaVisita.Items.Count; i > 0; i--)
            {
                lstAreaSelecionada.Items.Add(new ListItem(lstAreaVisita.Items[lstAreaVisita.Items.Count - 1].Text, lstAreaVisita.Items[lstAreaVisita.Items.Count - 1].Value));
                lstAreaVisita.Items.RemoveAt(lstAreaVisita.Items.Count - 1);
            }
        }

        /// <summary>
        ///     Adiciona a area selecionada no listBox AreasSelecionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnAddUm_Click(object sender, EventArgs e)
        {
            if (lstAreaVisita.SelectedIndex > -1)
            {
                int intCount = lstAreaVisita.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstAreaVisita.Items[i].Selected)
                    {
                        lstAreaSelecionada.Items.Add(new ListItem(lstAreaVisita.Items[i].Text, lstAreaVisita.Items[i].Value));
                        lstAreaVisita.Items.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        ///     Remove a area selecionada no listBox AreasSelecionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnRemoverUm_Click(object sender, EventArgs e)
        {
            if (lstAreaSelecionada.SelectedIndex > -1)
            {
                int intCount = lstAreaSelecionada.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstAreaSelecionada.Items[i].Selected)
                    {
                        lstAreaVisita.Items.Add(new ListItem(lstAreaSelecionada.Items[i].Text, lstAreaSelecionada.Items[i].Value));
                        lstAreaSelecionada.Items.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        ///     Remove todas as areas no listBox AreasSelecionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnRemoverTodos_Click(object sender, EventArgs e)
        {
            for (int i = lstAreaSelecionada.Items.Count; i > 0; i--)
            {
                lstAreaVisita.Items.Add(new ListItem(lstAreaSelecionada.Items[lstAreaSelecionada.Items.Count - 1].Text, lstAreaSelecionada.Items[lstAreaSelecionada.Items.Count - 1].Value));
                lstAreaSelecionada.Items.RemoveAt(lstAreaSelecionada.Items.Count - 1);
            }
        }

        /// <summary>
        ///     Limpa os campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 08/07/2009
        /// </history>
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            this.LimparDadosColaborador();
            this.DesabilitarCampos();
            //ddlTipoVisitante.Enabled = true;
        }

        #endregion

        #region Verificar Visitante

        /// <summary>
        ///     Verifica se o Visitado é diferente do Visitante e se o Visitante ainda
        ///     não foi adicionado na lista.
        /// </summary>
        /// <history>
        ///     [mribeiro] created 21/07/2009
        /// </history> 
        /// <returns>True/False</returns>
        protected bool VerificarColaborador()
        {
            bool blnExiste = false;

                for (int i = 0; i < this.lstGrid.Count; i++)
                {
                    //verifica se o visitante já foi adicionado
                    if (this.lstGrid[i].Id_Colaborador == this.Id_Colaborador)
                    {
                        //verifica se não é um novo cadastro
                        if (this.Id_Colaborador > 0)
                        {
                            blnExiste = true;
                            RadAjaxPanelColaborador.Alert("Este colaborador já foi adicionado.");
                            this.LimparDadosColaborador();
                            break;
                        }
                    }                    
                }
            
            return blnExiste;
        }

        #endregion

        #region Limpar Dados Visitante

        /// <summary>
        ///     Limpa os campos dos Dados do Visitante
        /// </summary>
        /// <history>
        ///     [mribeiro] 16/07/2009 created
        /// </history>
        protected void LimparDadosColaborador()
        {
            HiddenColaborador.Value = string.Empty;
            this.Id_Colaborador = 0;

            gobjSolicitacaoColaborador = null;

            this.lstColaboradores.Items.Clear();

            //this.ddlRegionalCad.SelectedIndex = 0;
            //this.ddlFilialCad.SelectedIndex = 0;

            this.PopularArea(); 
            
            this.txtObservacao.Text = string.Empty;
        }

        #endregion

        #region Adicionar Colaborador
        /// <summary>
        /// Chama a tela de listagem de colaboradores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar_5] created 11/02/2011
        ///     [haguiar_5] modify 14/02/2011
        /// </history>
        protected void btnAdicionarColaborador_Click(object sender, ImageClickEventArgs e)
        {
            this.RadWindowColaborador();
        }

        #region RadWindow

        /// <summary>
        ///     Abre a RadWindow com a tela de listagem de colaboradores
        /// </summary>
        /// <history>
        ///     [haguiar_5] created 11/02/2011
        /// </history>
        protected void RadWindowColaborador()
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.None;
            rwmWindowManager.ShowContentDuringLoad = false;
            rwmWindowManager.VisibleStatusbar = false;

            rwdWindow.Width = Unit.Pixel(530);
            rwdWindow.Height = Unit.Pixel(460);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Listagem de Colaboradores";

            rwdWindow.NavigateUrl = "ListColaboradoresSol.aspx?Tipo=13";
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlEscala = null;
            
            //Tenta encontrar na master
            pnlEscala = (Panel)this.FindControl("pnlCadColaborador");
            pnlEscala.Controls.Add(rwmWindowManager);
        }
        #endregion
        #endregion

        #region VerificarHiddenColaboradores
        /// <summary>
        /// Verifica o valor da query string.
        /// </summary>
        /// <history>
        ///     [haguiar_5] created 11/02/2011
        /// </history>
        private void VerificarHiddenColaboradores()
        {
            string[] arrColaboradores = null;

            if (!string.IsNullOrEmpty(this.HiddenColaborador.Value))
                //arrColaboradores = BLEncriptacao.DecQueryStr(this.HiddenColaborador.Value).Split(',');

                arrColaboradores = this.HiddenColaborador.Value.Split(',');

            this.PopularListaColaboradores(arrColaboradores);
        }

        /// <summary>
        /// Preenche a Lista de Colaboradores.
        /// </summary>
        /// <param name="parrColaboradores">Array com os Id dos Colaboradores</param>
        /// <history>
        ///     [haguiar_5] created 11/02/2011
        /// </history>
        private void PopularListaColaboradores(string[] parrColaboradores)
        {
            if (parrColaboradores != null)
            {
                BLColaborador objBLColaborador = new BLColaborador();
                Collection<Colaborador> colColaborador = null;

                try
                {
                    colColaborador = objBLColaborador.Obter(parrColaboradores);
                    ListItem limColaborador = null;

                    if (colColaborador != null)
                    {
                        foreach (Colaborador objColaborador in colColaborador)
                        {
                            limColaborador = this.lstColaboradores.Items.FindByValue(objColaborador.IdColaborador.ToString());

                            if (limColaborador == null)
                            {
                                limColaborador = new ListItem();
                                limColaborador.Text = objColaborador.NomeColaborador + " - " + objColaborador.CodigoColaborador;
                                limColaborador.Value = objColaborador.IdColaborador.ToString();

                                this.lstColaboradores.Items.Add(limColaborador);
                            }
                            limColaborador = null;
                        }
                    }
                    this.HiddenColaborador.Value = string.Empty;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }
        #endregion

        #region Remover Colaborador
        /// <summary>
        /// remove os colaboradores da lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar_5] created 11/02/2011
        /// </history>
        protected void btnRemoverColaborador_Click(object sender, ImageClickEventArgs e)
        {
            int intTotal = this.lstColaboradores.Items.Count;
            StringBuilder strColaboradores = new StringBuilder();

            for (int i = 0; i < intTotal; i++)
            {
                if (this.lstColaboradores.Items[i].Selected)
                {
                    strColaboradores.Append(this.lstColaboradores.Items[i].Value + ",");
                }
            }

            if (strColaboradores.Length > 0)
            {
                //removendo o caracter ,
                strColaboradores.Remove(strColaboradores.Length - 1, 1);

                string[] arrColaboradores = strColaboradores.ToString().Split(',');
                intTotal = arrColaboradores.Length;

                for (int i = 0; i < intTotal; i++)
                {
                    ListItem ltmColaborador = this.lstColaboradores.Items.FindByValue(arrColaboradores[i]);

                    if (ltmColaborador != null)
                    {
                        this.lstColaboradores.Items.Remove(ltmColaborador);
                    }
                }
            }
        }
        #endregion

        #region Eventos
        

        /// <summary>
        ///     Popula o combo Filial conforme Regional selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void ddlRegionalCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAreaVisita.Items.Count > 0)
            {
                lstAreaSelecionada.Items.Clear();
                lstAreaVisita.Items.Clear();
            }

            this.PopularFilial(ref ddlRegionalCad, ref ddlFilialCad);
        }

        /// <summary>
        ///     Popula Combo com as áreas da filial selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void ddlFilialCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.PopularLista();
            this.PopularArea();
        }

        /// <summary>
        ///     Preenche o RE do aprovador selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] 16/07/2009 created 
        /// </history>
        protected void ddlAprovadorCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAprovadorCad.SelectedIndex > 0)
            {
                BLSolicitacao objBlSolicitacao = new BLSolicitacao();

                DataTable dtt = objBlSolicitacao.ObterCodigoAprovador(Convert.ToInt32(ddlAprovadorCad.SelectedItem.Value));
                txtREAprovador.Text = dtt.Rows[0][1].ToString();
            }
            else
            {
                txtREAprovador.Text = string.Empty;
            }
        }

        #endregion
        
        #region Desabilitar Campos

        /// <summary>
        ///     Desabilita os campos
        /// </summary>
        /// <history>
        ///     [mribeiro] created 08/07/2009
        /// </history>
        protected void DesabilitarCampos()
        {/*
            ddlTipoVisitante.Enabled = false;
            txtNomeVisitanteCad.Enabled = false;
            ddlTipoDocumento.Enabled = false;
            txtDocumento.Enabled = false;
            ddlEmpresaCad.Enabled = false;*/
        }

        #endregion

        #region Habilitar Campos

        /// <summary>
        ///     Habilita os campos
        /// </summary>
        /// <history>
        ///     [mribeiro] created 08/07/2009
        /// </history>
        protected void HabilitarCampos()
        {/*
            ddlTipoVisitante.Enabled = true;
            txtNomeVisitanteCad.Enabled = true;
            ddlTipoDocumento.Enabled = true;
            txtDocumento.Enabled = true;
            ddlEmpresaCad.Enabled = true;*/
        }

        #endregion

        #region Gravar

        protected void Gravar()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            string strSolicitacao = "";

            try
            {
                for (int i = 0; i < this.lstGrid.Count; i++)
                {

                    string[] strId_Colaborador;
                    strId_Colaborador = this.lstGrid[i].Id_Colaboradores.Split(new string[] { "," }, StringSplitOptions.None);

                    string[] strNom_Colaborador;
                    strNom_Colaborador = this.lstGrid[i].Nom_Colaboradores.Split(new string[] { "," }, StringSplitOptions.None);

                    for (int j = 0; j < strId_Colaborador.Length; j++)
                    {
                        gobjSolicitacao = new SafWeb.Model.Solicitacao.PermissaoCrachaTitular();

                        //preenche a model solicitação
                        gobjSolicitacao.Id_UsuarioSolicitante = Convert.ToInt32(BLAcesso.IdUsuarioLogado());
                        gobjSolicitacao.Id_Colaborador = Convert.ToInt32(strId_Colaborador[j]);
                        gobjSolicitacao.Data_Solicitacao = DateTime.Now;
                        gobjSolicitacao.Id_Area = this.lstGrid[i].Id_Area;
                        gobjSolicitacao.Id_TipoSolicitacao = this.lstGrid[i].Id_TipoSolicitacao;
                        gobjSolicitacao.Des_MotivoSolicitacao = this.lstGrid[i].Des_MotivoSolicitacao;
                        
                        //insere a solicitação.
                        gobjSolicitacao.Id_SolicitacaoCrachaTitular = objBlSolicitacao.InserirPermissaoCrachaTitular(gobjSolicitacao);

                        //insere o status da solicitação
                        
                        objBlSolicitacao.InserirStatusPermissaoCrachaTitular(gobjSolicitacao.Id_SolicitacaoCrachaTitular,
                                                                                Convert.ToInt32(ddlAprovadorCad.SelectedItem.Value),
                                                                                this.lstGrid[i].Id_TipoSolicitacao);

                        strSolicitacao += "Solicitação nº: " + gobjSolicitacao.Id_SolicitacaoCrachaTitular.ToString() + " - Colaborador: " + strNom_Colaborador[j] + " \n";
                    }
                }

                this.BindModel(Enums.TipoTransacao.Novo);

                this.RadAjaxPanel1.Alert(strSolicitacao);

                lblMensagem.Visible = true;
                lblMensagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
            
        }

        #endregion


        #region Verificar Aprovador

        /// <summary>
        ///     Verificar o aprovador 
        /// </summary>
        /// <history>
        ///     [haguiar] 25/02/2012 created 14:56
        /// </history>
        /// <returns></returns>
        protected bool VerificarAprovador()
        {
            bool blnRetorno = true;

            if (BLAcesso.IdUsuarioLogado() == Convert.ToInt32(this.ddlAprovadorCad.SelectedItem.Value))
            {
                RadAjaxPanel1.Alert("O Aprovador não pode ser o mesmo que o Usuário Logado.");
                blnRetorno = false;
            }
            else
            {
                BLSolicitacao objBlSolicitacao = new BLSolicitacao();
                DataTable dtt = new DataTable();

                try
                {
                    dtt = objBlSolicitacao.ObterCodigoAprovador(Convert.ToInt32(this.ddlAprovadorCad.SelectedItem.Value));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }

                for (int i = 0; i < this.lstGrid.Count; i++)
                {
                    if (this.lstGrid[i].Id_Colaborador == Convert.ToInt32(dtt.Rows[0][0]))
                    {
                        RadAjaxPanel1.Alert("O Aprovador não pode ser o mesmo que o colaborador que irá receber a permissão.");
                        blnRetorno = false;
                        break;
                    }                    
                }
            }

            return blnRetorno;

        }

        #endregion

        #endregion

        #region Property

        /// <summary> 
        ///     Armazena os dados da solicitação e dos colaboradores adicionados na lista
        /// </summary> 
        /// <value>SolicitacaoColaborador</value> 
        /// <history> 
        ///     [mribeiro] 08/07/2009 Created 
        /// </history> 
        public List<SafWeb.Model.Solicitacao.PermissaoCrachaTitular> lstGrid
        {
            get
            {
                if (ViewState["vsList"] == null)
                {
                    ViewState["vsList"] = new List<SafWeb.Model.Solicitacao.PermissaoCrachaTitular>();
                }
                return (List<SafWeb.Model.Solicitacao.PermissaoCrachaTitular>)ViewState["vsList"];
            }
            set
            {
                ViewState["vsList"] = value;
            }
        }

        /// <summary> 
        ///     Codigo da Solicitação
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 10/07/2009 Created 
        /// </history> 
        public int IdSolicitacao
        {
            get
            {
                if ((this.ViewState["vsSolicitacao"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsSolicitacao"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsSolicitacao", value);
            }
        }

        /// <summary> 
        ///     Codigo do motivo da visita
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 15/07/2009 Created 
        /// </history> 
        public int IdMotivoVisita
        {
            get
            {
                if ((this.ViewState["vsMotivoVisita"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsMotivoVisita"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsMotivoVisita", value);
            }
        }

        /// <summary> 
        ///     Codigo do Visitante
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [haguiar] 25/02/2012 23:27 Created 
        /// </history> 
        public int Id_Colaborador
        {
            get
            {
                if ((this.ViewState["vsId_Colaborador"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsId_Colaborador"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsId_Colaborador", value);
            }
        }

        /// <summary> 
        ///     Codigo do Visitado
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 21/07/2009 Created 
        /// </history> 
        public int IdVisitado
        {
            get
            {
                if ((this.ViewState["vsIdVisitado"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdVisitado"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdVisitado", value);
            }
        }

        /// <summary> 
        ///     Codigo do Visitado
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 21/07/2009 Created 
        /// </history> 
        public int IdListaAnterior
        {
            get
            {
                if ((this.ViewState["vsIdLista"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdLista"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdLista", value);
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdFilial do Usuario Logado
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdFilial
        {
            get
            {
                if ((this.ViewState["vsIdFilial"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdFilial"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdFilial", value);
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdRegional do Usuario Logado
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdRegional
        {
            get
            {
                if ((this.ViewState["vsIdRegional"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdRegional"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdRegional", value);
            }
        }
        #endregion

    }
}
