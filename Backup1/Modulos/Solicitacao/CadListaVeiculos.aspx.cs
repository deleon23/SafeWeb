using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Idioma;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Empresa;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.ListaVeiculos;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Veiculo;
using SafWeb.Model.Empresa;
using SafWeb.Model.ListaVeiculos;
using Telerik.WebControls;

namespace SafWeb.UI.Modulos.Solicitacao
{
    public partial class CadListaVeiculos : FWPage
    {
        private ListaVeiculos gobjListaVeiculos;
        private SafWeb.Model.Veiculo.Veiculo gobjVeiculo;

        protected void Page_Load(object sender, EventArgs e)
        {
            //verifica se o Hidden está preenchido
            /*if (HiddenField.Value != string.Empty)
            {
                this.IdVeiculo = Convert.ToInt32(HiddenField.Value);

                if (this.IdVeiculo > 0)
                {
                    this.ObterVeiculo();
                }
            }*/

            if (!Page.IsPostBack)
            {
                this.IdVeiculo = Convert.ToInt32(Session["idVeiculo"]);
                this.ControlaPaineis(Enums.TipoPainel.Listagem);
                this.VerificaPermissoes();
                this.InicializaScripts();
            }
        }

        #region Inicializa Scripts

        protected void InicializaScripts()
        {
            lblMensagem.Visible = false;
            this.PopularEmpresa();
            this.PopularRegional();
            this.PopularEstado();

            btnHelpCad.Attributes.Add("OnClick", "AbrirHelpCad();");
            btnHelpList.Attributes.Add("OnClick", "AbrirHelpList();");

            //txtPlacaCad.Attributes.Add("OnKeyPress", "return FormataPlaca(event,this);");

            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlPlacaCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            this.BindLista(Enums.TipoBind.DataBind);
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
                pnlListagem.Visible = false;
                pnlCadastro.Visible = true;
            }

        }

        #endregion

        #region Permissões

        protected void VerificaPermissoes()
        {
            btnIncluir.Enabled = Permissoes.Inclusão();
            btnGravar.Visible = Permissoes.Inclusão();
            btnGravarSair.Visible = Permissoes.Inclusão();
        }

        #endregion

        #region Popular Combos

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
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void PopularEmpresa()
        {
            BLEmpresa objBlEmpresa = new BLEmpresa();
            Collection<SafWeb.Model.Empresa.Empresa> colEmpresa;

            ddlEmpresaCad.Items.Clear();

            try
            {
                colEmpresa = objBlEmpresa.Listar();

                ddlEmpresaCad.DataSource = colEmpresa;
                ddlEmpresaCad.DataTextField = "DescricaoEmpresa";
                ddlEmpresaCad.DataValueField = "IdEmpresa";
                ddlEmpresaCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlEmpresaCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, -1);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlEmpresaCad, "Outra...", true, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

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

                    ddlFilial.Enabled = true;
                }
                else
                {
                    ddlFilial.Enabled = false;
                    ddlFilial.SelectedIndex = 0;
                }

                BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, -1);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void PopularPlaca()
        {
            BLVeiculo objBlVeiculo = new BLVeiculo();
            Collection<SafWeb.Model.Veiculo.Veiculo> colVeiculo;

            try
            {
                colVeiculo = objBlVeiculo.ListarVeiculo(Convert.ToInt32(ddlEstadoCad.SelectedItem.Value));

                ddlPlacaCad.DataSource = colVeiculo;
                ddlPlacaCad.DataTextField = "DescricaoPlaca";
                ddlPlacaCad.DataValueField = "Codigo";
                ddlPlacaCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlPlacaCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                ddlPlacaCad.Items.Add(new ListItem("Outra..."));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void PopularEstado()
        {
            BLVeiculo objBlVeiculo = new BLVeiculo();
            Collection<SafWeb.Model.Veiculo.Estado> colEstado;

            try
            {
                colEstado = objBlVeiculo.ListarEstado();

                ddlEstadoCad.DataSource = colEstado;
                ddlEstadoCad.DataTextField = "DescricaoEstado";
                ddlEstadoCad.DataValueField = "Codigo";
                ddlEstadoCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlEstadoCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Listagem

        #region Bind

        protected void BindLista(Enums.TipoBind pintTipoBind)
        {

            BLListaVeiculos objBlListaVeiculos = new BLListaVeiculos();
            Collection<SafWeb.Model.ListaVeiculos.ListaVeiculos> colListaVeiculos;
            gobjListaVeiculos = new ListaVeiculos();

            gobjListaVeiculos.IdLista = 0;

            if (txtCodLista.Text != "")
            {
                gobjListaVeiculos.IdLista = Convert.ToInt32(txtCodLista.Text);
            }

            try
            {
                gobjListaVeiculos.IdRegional = Convert.ToInt32(ddlRegional.SelectedItem.Value);
                gobjListaVeiculos.IdFilial = Convert.ToInt32(ddlFilial.SelectedItem.Value);
                gobjListaVeiculos.DescricaoLista = txtNomeLista.Text;

                colListaVeiculos = objBlListaVeiculos.ListarListaVeiculos(gobjListaVeiculos);

                radGridLista.DataSource = colListaVeiculos;
                radGridLista.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(this.radGridLista.CurrentPageIndex,
                                                                                  this.radGridLista.PageCount,
                                                                                  colListaVeiculos.Count,
                                                                                  this.radGridLista.PageSize);

                if (pintTipoBind == Enums.TipoBind.DataBind)
                {
                    this.radGridLista.DataBind();
                }
            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region DataGrid

        #region NeedDataSource


        protected void radGridLista_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindLista(Enums.TipoBind.SemDataBind);
            }
        }

        #endregion

        #region ItemCommand

        protected void radGridLista_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim() == "Editar")
                {
                    if (Permissoes.Alteração() == false)
                    {
                        txtCodListaCad.Enabled = false;
                        txtNomeListaCad.Enabled = false;
                        ddlFilialCad.Enabled = false;
                        //TODO Caio
                        ddlEmpresaCad.Enabled = true;
                        btnAdicionarCad.Enabled = false;
                        //btnListarVisitante.Enabled = false;
                        btnGravar.Enabled = false;
                        btnGravarSair.Enabled = false;
                    }

                    this.BindModelVeiculo(Enums.TipoTransacao.Novo);
                    this.BindModelListaVeiculos(Enums.TipoTransacao.Novo);
                    this.IdLista = Convert.ToInt32(e.CommandArgument.ToString().Trim());
                    this.ObterLista();
                    this.ControlaPaineis(Enums.TipoPainel.Cadastro);
                }

                if (e.CommandName.Trim() == "Ativar")
                {
                    gobjListaVeiculos = new ListaVeiculos();

                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    if (btnAtivar.ToolTip == "Ativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        gobjListaVeiculos.Situacao = 1;
                    }
                    else if (btnAtivar.ToolTip == "Inativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        gobjListaVeiculos.Situacao = 0;
                    }

                    BLListaVeiculos objBlListaVeiculos = new BLListaVeiculos();

                    gobjListaVeiculos.IdLista = Convert.ToInt32(e.CommandArgument.ToString().Trim());
                    gobjListaVeiculos.IdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                    objBlListaVeiculos.AlterarSituacao(gobjListaVeiculos);

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
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region ItemDataBound

        protected void radGridLista_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                try
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
                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                        }

                        if (Permissoes.Alteração())
                        {
                            e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                        }
                        else
                        {
                            e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                        }

                        btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdLista").ToString();

                        Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                        ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                        if ((btnAtivar != null))
                        {
                            if (Permissoes.Alteração())
                            {
                                if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Situacao")))
                                {
                                    btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                    btnAtivar.ToolTip = "Inativar";
                                }
                                else
                                {
                                    btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                    btnAtivar.ToolTip = "Ativar";
                                }
                                btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdLista").ToString();
                            }
                            else
                            {
                                btnAtivar.Enabled = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        #endregion

        #endregion

        #region Eventos

        /// <summary>
        ///     Popula os combos com as filiais
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
        protected void ddlRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularFilial(ref ddlRegional, ref ddlFilial);
        }

        protected void ddlPlaca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlacaCad.SelectedItem.Text.Equals("Outra..."))
            {
                txtPlacaCad.Visible = true;
                ddlEmpresaCad.Enabled = true;
                //ddlPlacaCad.Visible = false;
            }
            else
            {
                txtPlacaCad.Visible = false;
                txtPlacaCad.Text = string.Empty;
                ddlEmpresaCad.Enabled = false;
                this.IdVeiculo = Convert.ToInt32(ddlPlacaCad.SelectedValue);
                this.ObterVeiculo();
            }
        }

        #endregion

        #region Botões

        /// <summary>
        ///     Exibe as listas conforme os filtros preenchidos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] 30/06/2009 created
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BindLista(Enums.TipoBind.DataBind);
        }

        /// <summary>
        ///     Abre a tela de cadastro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] 30/06/2009 created
        /// </history>
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            this.BindModelVeiculo(Enums.TipoTransacao.Novo);
            this.BindModelListaVeiculos(Enums.TipoTransacao.Novo);
        }

        #endregion

        #endregion

        #region Cadastro

        #region Bind Colaborador

        /// <summary>
        ///     Preenche o RadGrid com os colaboradores
        /// </summary>
        /// <history>
        ///     [mribeiro] created 01/07/2009
        /// </history>
        /// <param name="pintTipoBind"></param>
        protected void BindVeiculo(Enums.TipoBind pintTipoBind)
        {
            //TODO Caio
            BLListaVeiculos objBlListaVeiculos = new BLListaVeiculos();
            Collection<SafWeb.Model.Veiculo.Veiculo> colVeiculos;

            try
            {

                colVeiculos = objBlListaVeiculos.ListarVeiculosDaLista(this.IdLista);

                radGridVeiculos.DataSource = colVeiculos;
                radGridVeiculos.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(this.radGridVeiculos.CurrentPageIndex,
                                                                                       this.radGridVeiculos.PageCount,
                                                                                       colVeiculos.Count,
                                                                                       this.radGridVeiculos.PageSize);

                if (colVeiculos.Count > 0)
                {
                    this.lstGrid = new List<SafWeb.Model.Veiculo.Veiculo>();

                    for (int i = 0; i < colVeiculos.Count; i++)
                    {
                        this.lstGrid.Add(colVeiculos[i]);
                        //gobjVeiculo = new SafWeb.Model.Veiculo.Veiculo();

                        //gobjVeiculo.Codigo = colVeiculos[i].Codigo;
                        //gobjVeiculo.DescricaoPlaca = colVeiculos[i].DescricaoPlaca;
                        //gobjVeiculo.IdEmpresa = colVeiculos[i].IdEmpresa;
                        //gobjVeiculo.DescricaoEmpresa = colVeiculos[i].DescricaoEmpresa;
                        //gobjVeiculo.IdEstado = colVeiculos[i].IdEstado;
                        //gobjVeiculo.DescricaoEstado = colVeiculos[i].DescricaoEstado;
                        //gobjVeiculo.IdModelo = colVeiculos[i].IdModelo;
                        //gobjVeiculo.Prefixo = colVeiculos[i].Prefixo;
                        //gobjVeiculo.Situacao = colVeiculos[i].Situacao;

                        //this.lstGrid.Add(gobjVeiculo);
                    }
                }

                if (pintTipoBind == Enums.TipoBind.DataBind)
                {
                    this.radGridVeiculos.DataBind();
                }
            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Bind Model Filial

        protected void BindModelListaVeiculos(Enums.TipoTransacao pintTipoTransacao)
        {
            switch (pintTipoTransacao)
            {
                case Enums.TipoTransacao.CarregarDados:
                    gobjListaVeiculos = new ListaVeiculos();

                    gobjListaVeiculos.DescricaoLista = txtNomeListaCad.Text;
                    gobjListaVeiculos.IdFilial = Convert.ToInt32(ddlFilialCad.SelectedItem.Value);
                    gobjListaVeiculos.IdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());
                    break;
                case Enums.TipoTransacao.DescarregarDados:
                    txtCodListaCad.Text = gobjListaVeiculos.IdLista.ToString();
                    txtNomeListaCad.Text = gobjListaVeiculos.DescricaoLista;
                    BLUtilitarios.ConsultarValorCombo(ref ddlRegionalCad, gobjListaVeiculos.IdRegional.ToString());
                    ddlFilialCad.Enabled = true;
                    this.PopularFilial(ref ddlRegionalCad, ref ddlFilialCad);
                    BLUtilitarios.ConsultarValorCombo(ref ddlFilialCad, gobjListaVeiculos.IdFilial.ToString());
                    break;
                case Enums.TipoTransacao.Novo:
                    this.IdLista = 0;
                    txtCodListaCad.Text = "-----";
                    pnlCadastro.Visible = true;
                    pnlListagem.Visible = false;
                    txtNomeListaCad.Text = string.Empty;
                    ddlRegionalCad.SelectedIndex = 0;
                    ddlFilialCad.Enabled = false;
                    ddlFilialCad.Items.Add(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
                    ddlFilialCad.SelectedIndex = 0;
                    ddlEmpresaCad.SelectedIndex = 0;
                    this.lstGrid.Clear();
                    radGridVeiculos.DataSource = this.lstGrid;
                    radGridVeiculos.DataBind();
                    lblMensagem.Visible = false;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Bind Model Colaborador

        protected void BindModelVeiculo(Enums.TipoTransacao pintTipoTransacao)
        {
            switch (pintTipoTransacao)
            {
                case Enums.TipoTransacao.CarregarDados:
                    gobjVeiculo = new Model.Veiculo.Veiculo
                    {
                        Codigo = 0,
                        IdEmpresa = 0,
                        IdEstado = Convert.ToInt32(ddlEstadoCad.SelectedItem.Value),
                        DescricaoEstado = ddlEstadoCad.SelectedItem.Text
                    };


                    if (txtPlacaCad.Visible)
                        gobjVeiculo.DescricaoPlaca = txtPlacaCad.Text;
                    else
                    {
                        gobjVeiculo.Codigo = this.IdVeiculo;
                        gobjVeiculo.DescricaoPlaca = ddlPlacaCad.SelectedItem.Text;
                    }

                    if (txtOutraEmpresaCad.Visible)
                        gobjVeiculo.DescricaoEmpresa = txtOutraEmpresaCad.Text;
                    else
                    {
                        gobjVeiculo.IdEmpresa = Convert.ToInt32(ddlEmpresaCad.SelectedItem.Value);
                        gobjVeiculo.DescricaoEmpresa = ddlEmpresaCad.SelectedItem.Text;
                    }

                    break;
                case Enums.TipoTransacao.DescarregarDados:
                    //txtNomeVisitante.Text = gobjVeiculo.DescricaoPlaca;
                    BLUtilitarios.ConsultarValorCombo(ref ddlEmpresaCad, gobjVeiculo.IdEmpresa.ToString());
                    break;
                case Enums.TipoTransacao.Novo:
                    HiddenField.Value = string.Empty;
                    this.IdVeiculo = 0;

                    if (ddlEstadoCad.Items.Count > 0)
                        ddlEstadoCad.SelectedIndex = 0;

                    if (ddlPlacaCad.Items.Count > 0)
                        ddlPlacaCad.SelectedIndex = 0;

                    txtPlacaCad.Text = string.Empty;
                    txtPlacaCad.Visible = false;

                    if (ddlEmpresaCad.Items.Count > 0)
                        ddlEmpresaCad.SelectedIndex = 0;

                    txtOutraEmpresaCad.Text = string.Empty;
                    txtOutraEmpresaCad.Visible = false;

                    ddlEmpresaCad.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Desabilitar Campos

        /// <summary>
        ///     Desabilita os campos do "Dados do Visitante"
        /// </summary>
        /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history>
        protected void DesabilitarCampos()
        {
            ddlEmpresaCad.Enabled = false;
            ddlPlacaCad.Enabled = false;
        }

        #endregion

        #region Habilitar Campos

        protected void HabilitarCampos()
        {
            ddlEmpresaCad.Enabled = true;
            ddlPlacaCad.Enabled = true;
        }

        #endregion

        #region DataGrid

        #region NeedDataSource

        protected void radGridVeiculos_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                radGridVeiculos.DataSource = this.lstGrid;
            }
        }

        #endregion

        #region ItemCommand

        protected void radGridVeiculos_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Excluir")
            {
                var veiculoList = this.lstGrid.Find(
                    delegate(Model.Veiculo.Veiculo _veiculo)
                    {
                        return _veiculo.DescricaoPlaca == radGridVeiculos.Items[Convert.ToInt32(e.Item.ItemIndex)]["DescricaoPlaca"].Text;
                    });

                if (veiculoList != null)
                    this.lstGrid.RemoveAt(this.lstGrid.IndexOf(veiculoList));

                radGridVeiculos.DataSource = this.lstGrid;
                radGridVeiculos.DataBind();
            }

            if (e.CommandName.Trim() == "Ativar")
            {
                gobjVeiculo = new Model.Veiculo.Veiculo();

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                if (btnAtivar.ToolTip == "Ativar")
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                    btnAtivar.ToolTip = "Inativar";
                    gobjVeiculo.Situacao = 1;
                }
                else if (btnAtivar.ToolTip == "Inativar")
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                    btnAtivar.ToolTip = "Ativar";
                    gobjVeiculo.Situacao = 0;
                }

                BLListaVeiculos objBlLista = new BLListaVeiculos();
                gobjVeiculo.Codigo = Convert.ToInt32(e.CommandArgument.ToString());
                this.lstGrid.Find(delegate(Model.Veiculo.Veiculo Veic) { return Veic.Codigo == gobjVeiculo.Codigo; }).Situacao = gobjVeiculo.Situacao;
                objBlLista.AlterarSituacaoVeiculoDaLista(gobjVeiculo, this.IdLista, Convert.ToInt32(BLAcesso.IdUsuarioLogado()));
            }
        }

        #endregion

        #region ItemDataBound

        protected void radGridVeiculos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                try
                {
                    if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
                    {
                        if (Permissoes.Alteração())
                        {
                            e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                        }
                        else
                        {
                            e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                        }

                        //btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdLista").ToString();

                        Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                        ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                        if ((btnAtivar != null))
                        {
                            if (Permissoes.Alteração())
                            {
                                if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Situacao")))
                                {
                                    btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                    btnAtivar.ToolTip = "Inativar";
                                }
                                else
                                {
                                    btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                    btnAtivar.ToolTip = "Ativar";
                                }
                                btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Codigo").ToString();
                            }
                            else
                            {
                                btnAtivar.Enabled = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        #endregion

        #endregion

        #region Eventos

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPlacaCad.Enabled = false;
            txtPlacaCad.Text = string.Empty;
            txtPlacaCad.Visible = false;

            ddlEmpresaCad.SelectedIndex = 0;
            ddlEmpresaCad.Enabled = false;
            txtOutraEmpresaCad.Text = string.Empty;
            txtOutraEmpresaCad.Visible = false;

            if (ddlEstadoCad.SelectedIndex > 0)
            {
                this.PopularPlaca();
                ddlPlacaCad.Enabled = true;
            }

            if (ddlPlacaCad.Items.Count > 0)
                ddlPlacaCad.SelectedIndex = 0;
        }

        /// <summary>
        ///     Se for selecionada a opção "Outra", exibe um textbox para inserir uma nova empresa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 24/07/2009 
        ///</history>
        protected void ddlEmpresaCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpresaCad.SelectedItem.Text.Equals("Outra..."))
            {
                //lblOutraEmpresa.Visible = true;
                txtOutraEmpresaCad.Visible = true;
            }
            else
            {
                //lblOutraEmpresa.Visible = false;
                txtOutraEmpresaCad.Visible = false;
            }
        }

        /// <summary>
        ///     Popula os combos com as filiais
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
        protected void ddlRegionalCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularFilial(ref ddlRegionalCad, ref ddlFilialCad);
        }

        #endregion

        #region Botões

        /// <summary>
        ///     Volta para o painel de listagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            ControlaPaineis(Enums.TipoPainel.Listagem);
            this.BindLista(Enums.TipoBind.DataBind);
        }

        /// <summary>
        ///     Adiciona os dados do visitante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history>
        protected void btnAdicionarCad_Click(object sender, EventArgs e)
        {
            bool blnAdicionar = true;

            if (this.lstGrid.Find(delegate(Model.Veiculo.Veiculo _veiculo) { return _veiculo.Codigo == this.IdVeiculo; }) != null && this.IdVeiculo > 0)
            {
                blnAdicionar = false;
                RadAjaxPanel2.Alert("Esse veículo já foi adicionado na lista.");
            }

            if ((ddlEmpresaCad.SelectedItem.Text == "Outra...") && (string.IsNullOrEmpty(txtOutraEmpresaCad.Text)))
            {
                blnAdicionar = false;
                RadAjaxPanel2.Alert("Insira o nome da empresa.");
            }

            if ((ddlPlacaCad.SelectedItem.Text == "Outra...") && (string.IsNullOrEmpty(txtPlacaCad.Text)))
            {
                blnAdicionar = false;
                RadAjaxPanel2.Alert("Insira a placa do veículo.");
            }

            if (blnAdicionar)
            {
                this.BindModelVeiculo(Enums.TipoTransacao.CarregarDados);

                //verifica se é uma nova empresa
                if (gobjVeiculo.IdEmpresa == 0)
                {
                    BLEmpresa objBlEmpresa = new BLEmpresa();
                    Empresa objEmpresa = objBlEmpresa.Obter(new Empresa
                        {
                            DescricaoEmpresa = gobjVeiculo.DescricaoEmpresa
                        }
                    );

                    if (objEmpresa.IdEmpresa > 0)
                    {
                        gobjVeiculo.IdEmpresa = objEmpresa.IdEmpresa;
                        gobjVeiculo.DescricaoEmpresa = objEmpresa.DescricaoEmpresa;
                    }
                }

                //verifica se a placa já existe
                if (gobjVeiculo.Codigo == 0)
                {
                    BLVeiculo objBlVeiculo = new BLVeiculo();
                    Model.Veiculo.Veiculo objVeiculo = objBlVeiculo.Obter(new Model.Veiculo.Veiculo
                        {
                            DescricaoPlaca = gobjVeiculo.DescricaoPlaca
                        }
                    );

                    if (objVeiculo.Codigo > 0)
                    {
                        RadAjaxPanel2.Alert("A placa " + objVeiculo.DescricaoPlaca + " já existe");
                        blnAdicionar = false;
                    }
                }


                if (blnAdicionar)
                {
                    gobjVeiculo.Situacao = 1;
                    //adiciona o novo registro
                    this.lstGrid.Add(gobjVeiculo);

                    radGridVeiculos.DataSource = this.lstGrid;
                    radGridVeiculos.DataBind();

                    this.BindModelVeiculo(Enums.TipoTransacao.Novo);
                    this.DesabilitarCampos();
                }
                else
                {
                    this.DesabilitarCampos();
                }
            }
        }

        /// <summary>
        ///     Grava os dados e volta para o painel de listagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history> 
        protected void btnGravarSair_Click(object sender, EventArgs e)
        {
            if (this.Gravar())
            {
                this.ControlaPaineis(Enums.TipoPainel.Listagem);
                this.BindLista(Enums.TipoBind.DataBind);
            }
        }

        /// <summary>
        ///     Grava os dados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            if (this.Gravar())
            {
                this.BindModelListaVeiculos(Enums.TipoTransacao.Novo);
                lblMensagem.Visible = true;
                lblMensagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));
            }
        }

        /// <summary>
        ///     Abre a RadWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history>
        protected void btnListarVisitante_Click(object sender, ImageClickEventArgs e)
        {
            this.AbrirRadWindow();
        }

        /// <summary>
        ///     Limpa os campos do dado do visitante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history>
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            this.BindModelVeiculo(Enums.TipoTransacao.Novo);
            this.DesabilitarCampos();
        }

        #endregion

        #region Gravar

        /// <summary>
        ///     Insere/Altera uma nova lista
        /// </summary>
        /// <history>
        ///     [mribeiro] created 02/07/2009
        /// </history>
        protected bool Gravar()
        {
            bool blnGravar = true;

            if (!string.IsNullOrEmpty(txtNomeListaCad.Text) && ddlRegionalCad.SelectedIndex > 0 && ddlFilialCad.SelectedIndex > 0)
            {
                if (this.lstGrid.Count > 1)
                {
                    BLListaVeiculos objBlListaVeiculos = new BLListaVeiculos();
                    this.BindModelListaVeiculos(Enums.TipoTransacao.CarregarDados);

                    try
                    {
                        for (int i = 0; i < this.lstGrid.Count; i++)
                        {
                            //verifica se a empresa já existe
                            if (this.lstGrid[i].IdEmpresa == 0)
                            {
                                BLEmpresa objBlempresa = new BLEmpresa();
                                this.lstGrid[i].IdEmpresa = objBlempresa.Inserir(
                                    new Empresa
                                    {
                                        DescricaoEmpresa = this.lstGrid[i].DescricaoEmpresa
                                    }
                                );
                            }

                            //verifica se o veículo já existe
                            if (this.lstGrid[i].Codigo == 0)
                            {
                                BLVeiculo objBlVeiculo = new BLVeiculo();
                                this.lstGrid[i].Codigo = objBlVeiculo.Inserir(this.lstGrid[i]);
                            }

                            this.lstGrid[i].IdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());
                        }

                        if (blnGravar)
                        {
                            if (this.IdLista == 0)
                            {
                                gobjListaVeiculos.IdLista = objBlListaVeiculos.Inserir(gobjListaVeiculos, this.lstGrid);
                            }
                            else
                            {
                                //TODO Caio
                                gobjListaVeiculos.IdLista = this.IdLista;
                                gobjListaVeiculos.IdLista = objBlListaVeiculos.Alterar(gobjListaVeiculos, this.lstGrid);
                            }

                            if (gobjListaVeiculos.IdLista > 0)
                            {
                                if (this.IdLista == 0)
                                {
                                    this.IdLista = gobjListaVeiculos.IdLista;
                                }

                                RadAjaxPanel1.Alert("Lista " + gobjListaVeiculos.DescricaoLista + " gravada com sucesso! Código da lista: " + this.IdLista.ToString() + ".");
                                blnGravar = true;
                                this.PopularEmpresa();
                            }
                            else
                            {
                                RadAjaxPanel1.Alert("Já existe uma lista com o nome: " + gobjListaVeiculos.DescricaoLista + ".");
                                blnGravar = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        blnGravar = false;
                    }
                }
                else
                {
                    RadAjaxPanel1.Alert("A Lista deve conter pelo menos 2 (dois) veículos.");
                    blnGravar = false;
                }
            }
            return blnGravar;
        }

        #endregion

        #region Obter Lista

        /// <summary>
        ///     Obtem as informações da lista selecionada
        /// </summary>
        /// <history>
        ///     [mribeiro] created 01/07/2009
        /// </history>
        protected void ObterLista()
        {
            BLListaVeiculos objBlListaVeiculos = new BLListaVeiculos();

            try
            {
                gobjListaVeiculos = objBlListaVeiculos.Obter(this.IdLista);
                this.BindModelListaVeiculos(Enums.TipoTransacao.DescarregarDados);
                this.BindVeiculo(Enums.TipoBind.DataBind);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Obter Veiculos

        protected void ObterVeiculo()
        {
            BLVeiculo objBlVeiculo = new BLVeiculo();

            try
            {
                gobjVeiculo = objBlVeiculo.Obter(new Model.Veiculo.Veiculo { Codigo = this.IdVeiculo });
                this.BindModelVeiculo(Enums.TipoTransacao.DescarregarDados);
                this.DesabilitarCampos();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #endregion

        #region Property

        public int IdVeiculo
        {
            get
            {
                if ((this.ViewState["vsVeiculo"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsVeiculo"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsVeiculo", value);
            }
        }

        public int IdLista
        {
            get
            {
                if ((this.ViewState["vsLista"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsLista"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsLista", value);
            }
        }

        /// <summary> 
        ///     Armazena os veiculos adicionados na lista
        /// </summary> 
        /// <value>Colaborador</value> 
        /// <history> 
        ///     [mribeiro] 01/07/2009 Created 
        /// </history> 
        public List<SafWeb.Model.Veiculo.Veiculo> lstGrid
        {
            get
            {
                if (ViewState["vsList"] == null)
                {
                    ViewState["vsList"] = new List<SafWeb.Model.Veiculo.Veiculo>();
                }
                return (List<SafWeb.Model.Veiculo.Veiculo>)ViewState["vsList"];
            }
            set
            {
                ViewState["vsList"] = value;
            }
        }

        #endregion

        #region RadWindow

        /// <summary> 
        ///     Abre a RadWindow com a tela de Listagem de Colaboradores 
        /// </summary> 
        /// <history> 
        ///     [mribeiro] created 04/07/2009 
        /// </history> 
        private void AbrirRadWindow()
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.None;

            rwdWindow.Width = Unit.Pixel(530);
            rwdWindow.Height = Unit.Pixel(480);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Selecione um colaborador ou clique em fechar para sair da tela.";
            //TODO Caio
            //rwdWindow.NavigateUrl = "ListColaboradoresVis.aspx?Tipo=" + ddlTipoVisitante.SelectedItem.Value.ToString();
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlExclusao = null;

            //Tenta encontrar na master
            pnlExclusao = (Panel)this.FindControl("pnlLista");
            pnlExclusao.Controls.Add(rwmWindowManager);
        }

        #endregion

    }
}