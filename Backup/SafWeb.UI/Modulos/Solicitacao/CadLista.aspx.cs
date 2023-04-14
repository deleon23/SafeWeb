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
using System.Collections.ObjectModel;
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using FrameWork.BusinessLayer.Utilitarios;
using Telerik.WebControls;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Empresa;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Lista;
using SafWeb.Model.Lista;
using FrameWork.BusinessLayer.Usuarios;
using System.Collections.Generic;
using SafWeb.Model.Colaborador;
using SafWeb.Model.Empresa;

namespace SafWeb.UI.Modulos.Solicitacao
{
    public partial class CadLista : FWPage
    {
        private Lista gobjLista;
        private Colaborador gobjColaborador;

        protected void Page_Load(object sender, EventArgs e)
        {
            //verifica se o Hidden está preenchido
            if (HiddenField.Value != string.Empty)
            {
                this.IdColaborador = Convert.ToInt32(HiddenField.Value);

                if (this.IdColaborador > 0)
                {
                    //Obtem os dados do colaborador selecionado na radWindow
                    this.ObterColaborador();
                }
                else
                {
                    if (ddlTipoVisitante.SelectedItem.Text.Trim() == "Visitante")
                    {
                        //Se for um visitante habilita os campos para o cadastro
                        this.BindModelColaborador(Enums.TipoTransacao.Novo);
                        this.HabilitarCampos();
                        BLUtilitarios.ConsultarTextoCombo(ref ddlTipoVisitante, "Visitante");
                        ddlTipoVisitante.Enabled = false;
                    }
                }
            }

            if (!Page.IsPostBack)
            {
                this.IdColaborador = Convert.ToInt32(Session["idColaborador"]);
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
            this.PopularTipoDocumento();
            this.PopularTipoVisitante();

            btnHelpCad.Attributes.Add("OnClick", "AbrirHelpCad();");
            btnHelpList.Attributes.Add("OnClick", "AbrirHelpList();");

            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

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
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com os tipos de colaboradores
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
        protected void PopularTipoVisitante()
        {
            BLColaborador objBlColaborador = new BLColaborador();
            Collection<SafWeb.Model.Colaborador.TipoColaborador> colTipoColaborador;

            try
            {
                colTipoColaborador = objBlColaborador.ListarTipoColaborador();

                ddlTipoVisitante.DataSource = colTipoColaborador;
                ddlTipoVisitante.DataTextField = "DescricaoColaborador";
                ddlTipoVisitante.DataValueField = "IdTipoColaborador";
                ddlTipoVisitante.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoVisitante, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, -1);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com os tipos de documentos
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
        protected void PopularTipoDocumento()
        {
            BLColaborador objBlColaborador = new BLColaborador();
            Collection<SafWeb.Model.Colaborador.TipoDocumento> colTipoDocumento;

            try
            {
                colTipoDocumento = objBlColaborador.ListarTipoDocumento();

                ddlTipoDocumento.DataSource = colTipoDocumento;
                ddlTipoDocumento.DataTextField = "DescricaoDocumento";
                ddlTipoDocumento.DataValueField = "IdTipoDocumento";
                ddlTipoDocumento.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoDocumento, "RE", true, -1);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoDocumento, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com as empresas
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
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

        #endregion

        #region Listagem

        #region Bind

        /// <summary>
        ///     Preenche o RadGrid com as listas
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009
        /// </history>
        /// <param name="pintTipoBind"></param>
        protected void BindLista(Enums.TipoBind pintTipoBind)
        {

            BLLista objBlLista = new BLLista();
            Collection<SafWeb.Model.Lista.Lista> colLista;
            gobjLista = new Lista();

            gobjLista.IdLista = 0;

            if (txtCodLista.Text != "")
            {
                gobjLista.IdLista = Convert.ToInt32(txtCodLista.Text);
            }
            
            try
            {

                gobjLista.IdRegional = Convert.ToInt32(ddlRegional.SelectedItem.Value);
                gobjLista.IdFilial = Convert.ToInt32(ddlFilial.SelectedItem.Value);
                gobjLista.DescricaoLista = txtNomeLista.Text;

                colLista = objBlLista.ListarLista(gobjLista);

                radGridLista.DataSource = colLista;
                radGridLista.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(this.radGridLista.CurrentPageIndex,
                                                                                  this.radGridLista.PageCount,
                                                                                  colLista.Count,
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

        /// <summary>
        ///     radGridLista
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        /// </history>        
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
                        ddlTipoVisitante.Enabled = false;
                        txtNomeVisitante.Enabled = false;
                        ddlTipoDocumento.Enabled = false;
                        txtDocumento.Enabled = false;
                        ddlEmpresaCad.Enabled = false;
                        btnAdicionarCad.Enabled = false;
                        btnListarVisitante.Enabled = false;
                        btnGravar.Enabled = false;
                        btnGravarSair.Enabled = false;
                    }

                    this.BindModelColaborador(Enums.TipoTransacao.Novo);
                    this.BindModelFilial(Enums.TipoTransacao.Novo);
                    this.IdLista = Convert.ToInt32(e.CommandArgument.ToString().Trim());
                    this.ObterLista();
                    this.ControlaPaineis(Enums.TipoPainel.Cadastro);
                }

                if (e.CommandName.Trim() == "Ativar")
                {
                    gobjLista = new Lista();

                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    if (btnAtivar.ToolTip == "Ativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        gobjLista.Situacao = 1;
                    }
                    else if (btnAtivar.ToolTip == "Inativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        gobjLista.Situacao = 0;
                    }

                    BLLista objBlLista = new BLLista();

                    gobjLista.IdLista = Convert.ToInt32(e.CommandArgument.ToString().Trim());
                    gobjLista.IdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                    objBlLista.AlterarSituacao(gobjLista);

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
            this.BindModelColaborador(Enums.TipoTransacao.Novo);
            this.BindModelFilial(Enums.TipoTransacao.Novo);
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
        protected void BindColaborador(Enums.TipoBind pintTipoBind)
        {

            BLLista objBlLista = new BLLista();
            Collection<SafWeb.Model.Colaborador.Colaborador> colColaboradores;

            try
            {

                colColaboradores = objBlLista.ListarColaboradorDaLista(this.IdLista);

                radGridVisitantes.DataSource = colColaboradores;
                radGridVisitantes.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(this.radGridVisitantes.CurrentPageIndex,
                                                                                       this.radGridVisitantes.PageCount,
                                                                                       colColaboradores.Count,
                                                                                       this.radGridVisitantes.PageSize);

                if (colColaboradores.Count > 0)
                {
                    this.lstGrid = new List<Colaborador>();

                    for (int i = 0; i < colColaboradores.Count;i++ )
                    {
                        gobjColaborador = new Colaborador();

                        gobjColaborador.IdColaborador = colColaboradores[i].IdColaborador;
                        gobjColaborador.TipoDocumento = colColaboradores[i].TipoDocumento;
                        gobjColaborador.NomeColaborador = colColaboradores[i].NomeColaborador;
                        gobjColaborador.IdEmpresa = colColaboradores[i].IdEmpresa;
                        gobjColaborador.DescricaoEmpresa = colColaboradores[i].DescricaoEmpresa;
                        gobjColaborador.NumeroDocumento = colColaboradores[i].NumeroDocumento;
                        gobjColaborador.Situacao = colColaboradores[i].Situacao;

                        this.lstGrid.Add(gobjColaborador);
                    }
                }

                if (pintTipoBind == Enums.TipoBind.DataBind)
                {
                    this.radGridVisitantes.DataBind();
                }
            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Bind Model Filial

        protected void BindModelFilial(Enums.TipoTransacao pintTipoTransacao)
        {
            if (pintTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                gobjLista = new Lista();

                gobjLista.DescricaoLista = txtNomeListaCad.Text;
                gobjLista.IdFilial = Convert.ToInt32(ddlFilialCad.SelectedItem.Value);
                gobjLista.IdUsuario = Convert.ToInt32( BLAcesso.IdUsuarioLogado());
           
            }
            else if (pintTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                txtCodListaCad.Text = gobjLista.IdLista.ToString();
                txtNomeListaCad.Text = gobjLista.DescricaoFilial;
                BLUtilitarios.ConsultarValorCombo(ref ddlRegionalCad, gobjLista.IdRegional.ToString());
                ddlFilialCad.Enabled = true;
                this.PopularFilial(ref ddlRegionalCad, ref ddlFilialCad);
                BLUtilitarios.ConsultarValorCombo(ref ddlFilialCad, gobjLista.IdFilial.ToString());
                                
            }
            else if (pintTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.IdLista = 0;
                txtCodListaCad.Text = "-----";
                pnlCadastro.Visible = true;
                pnlListagem.Visible = false;
                txtNomeListaCad.Text = string.Empty;
                ddlRegionalCad.SelectedIndex = 0;
                ddlFilialCad.Enabled = false;
                ddlFilialCad.Items.Add(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
                ddlFilialCad.SelectedIndex = 0;
                txtOutraEmpresa.Visible = false;
                lblOutraEmpresa.Visible = false;
                ddlEmpresaCad.SelectedIndex = 0;
                this.lstGrid.Clear();
                radGridVisitantes.DataSource = this.lstGrid;
                radGridVisitantes.DataBind();
                lblMensagem.Visible = false;
            }
        }

        #endregion

        #region Bind Model Colaborador

        protected void BindModelColaborador(Enums.TipoTransacao pintTipoTransacao)
        {
            if (pintTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                gobjColaborador = new Colaborador();

                gobjColaborador.IdColaborador = this.IdColaborador;
                gobjColaborador.TipoDocumento = ddlTipoDocumento.SelectedItem.Text;

                if (ddlTipoVisitante.SelectedItem.Text == "Visitante")
                {
                    gobjColaborador.IdTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value);
                }
                else
                {
                    gobjColaborador.IdTipoDocumento = -1;
                }

                gobjColaborador.IdTipoColaborador = Convert.ToInt32(ddlTipoVisitante.SelectedItem.Value);
                gobjColaborador.NomeColaborador = txtNomeVisitante.Text;

                if (txtOutraEmpresa.Visible)
                {
                    gobjColaborador.IdEmpresa = 0;
                    gobjColaborador.DescricaoEmpresa = txtOutraEmpresa.Text;
                }
                else
                {
                    gobjColaborador.IdEmpresa = Convert.ToInt32(ddlEmpresaCad.SelectedItem.Value);
                    gobjColaborador.DescricaoEmpresa = ddlEmpresaCad.SelectedItem.Text;
                }
                gobjColaborador.NumeroDocumento = txtDocumento.Text;
            }
            else if (pintTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                BLUtilitarios.ConsultarValorCombo(ref ddlTipoVisitante, gobjColaborador.IdTipoColaborador.ToString());  
                txtNomeVisitante.Text = gobjColaborador.NomeColaborador;
                if (gobjColaborador.CodigoColaborador == null)
                {
                    BLUtilitarios.ConsultarValorCombo(ref ddlTipoDocumento, gobjColaborador.IdTipoDocumento.ToString());
                    txtDocumento.Text = gobjColaborador.NumeroDocumento;
                }
                else
                {
                    BLUtilitarios.ConsultarTextoCombo(ref ddlTipoDocumento, "RE");
                    txtDocumento.Text = gobjColaborador.CodigoColaborador;
                }
                BLUtilitarios.ConsultarValorCombo(ref ddlEmpresaCad, gobjColaborador.IdEmpresa.ToString());  
            }
            else if (pintTipoTransacao == Enums.TipoTransacao.Novo)
            {
                HiddenField.Value = string.Empty;
                this.IdColaborador = 0;
                ddlTipoVisitante.Enabled = true;
                //ddlTipoVisitante.SelectedIndex = 0;                
                txtNomeVisitante.Text = string.Empty;                
                ddlTipoDocumento.SelectedIndex = 0;                
                txtDocumento.Text = string.Empty;                
                ddlEmpresaCad.SelectedIndex = 0;
                txtOutraEmpresa.Text = string.Empty;
                lblOutraEmpresa.Visible = false;
                txtOutraEmpresa.Visible = false;
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
            ddlTipoVisitante.Enabled = false;
            txtNomeVisitante.Enabled = false;
            ddlTipoDocumento.Enabled = false;
            txtDocumento.Enabled = false;
            ddlEmpresaCad.Enabled = false;
        }

        #endregion

        #region Habilitar Campos

        /// <summary>
        ///     Habilitar os campos do "Dados do Visitante"
        /// </summary>
        /// <history>
        ///     [mribeiro] created 04/07/2009
        /// </history>
        protected void HabilitarCampos()
        {
            ddlTipoVisitante.Enabled = true;
            txtNomeVisitante.Enabled = true;
            ddlTipoDocumento.Enabled = true;
            txtDocumento.Enabled = true;
            ddlEmpresaCad.Enabled = true;
        }

        #endregion

        #region DataGrid

        #region NeedDataSource

        protected void radGridVisitantes_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                radGridVisitantes.DataSource = this.lstGrid;
            }
        }

        #endregion

        #region ItemCommand

        protected void radGridVisitantes_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Excluir")
            {
                
                for (int i = 0; i < this.lstGrid.Count; i++)
                {
                    if (this.lstGrid[i].NomeColaborador == radGridVisitantes.Items[Convert.ToInt32(e.Item.ItemIndex)]["NomeColaborador"].Text &&
                       this.lstGrid[i].DescricaoEmpresa == radGridVisitantes.Items[Convert.ToInt32(e.Item.ItemIndex)]["DescricaoEmpresa"].Text &&
                       this.lstGrid[i].TipoDocumento == radGridVisitantes.Items[Convert.ToInt32(e.Item.ItemIndex)]["TipoDocumento"].Text &&
                       this.lstGrid[i].NumeroDocumento == radGridVisitantes.Items[Convert.ToInt32(e.Item.ItemIndex)]["NumeroDocumento"].Text)
                    {
                        this.lstGrid.RemoveAt(i);
                    }
                }

                radGridVisitantes.DataSource = this.lstGrid;
                radGridVisitantes.DataBind();
            }

            if (e.CommandName.Trim() == "Ativar")
            {
                gobjColaborador = new Colaborador();

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                if (btnAtivar.ToolTip == "Ativar")
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                    btnAtivar.ToolTip = "Inativar";
                    gobjColaborador.Situacao = true;
        }
                else if (btnAtivar.ToolTip == "Inativar")
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                    btnAtivar.ToolTip = "Ativar";
                    gobjColaborador.Situacao = false;
                }

                BLLista objBlLista = new BLLista();
                gobjColaborador.IdColaborador = Convert.ToInt32(e.CommandArgument.ToString());
                this.lstGrid.Find(delegate(Colaborador col) { return col.IdColaborador == gobjColaborador.IdColaborador; }).Situacao = gobjColaborador.Situacao;
                objBlLista.AlterarSituacaoColaboradorDaLista(gobjColaborador, this.IdLista, Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

            }
        }

        #endregion

        #region ItemDataBound

        protected void radGridVisitantes_ItemDataBound(object sender, GridItemEventArgs e)
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
                                btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdColaborador").ToString();
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
                lblOutraEmpresa.Visible = true;
                txtOutraEmpresa.Visible = true;
            }
            else
            {
                lblOutraEmpresa.Visible = false;
                txtOutraEmpresa.Visible = false;
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

        /// <summary>
        ///     Formata o campo Documento conforme tipo selecionado
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDocumento.SelectedItem.Text.Trim() == "RG" || ddlTipoDocumento.SelectedItem.Text.Trim() == "Passaporte")
            {
                txtDocumento.Enabled = true;
                txtDocumento.MaxLength = 10;
                txtDocumento.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NENHUMA;
            }
            else
            {
                txtDocumento.Enabled = false;
            }
        }

        /// <summary>
        ///     Se for visitante retira a opção de RE.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 24/07/2009
        /// </history>
        protected void ddlTipoVisitante_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoVisitante.SelectedItem.Text == "Visitante")
            {
                if (ddlTipoDocumento.Items[ddlTipoDocumento.Items.Count - 1].Text == "RE")
                {
                    ddlTipoDocumento.Items.RemoveAt(ddlTipoDocumento.Items.Count - 1);
                }
            }
            else
            {
                if (ddlTipoDocumento.Items[ddlTipoDocumento.Items.Count - 1].Text != "RE")
                {
                    ddlTipoDocumento.Items.Add(new ListItem("RE"));
                }
            }
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

            for (int i = 0; i < this.lstGrid.Count; i++)
            {
                //verifica se não é um novo cadastro
                if (this.IdColaborador > 0)
                {
                    //verifica se o colaborador já não existe na lista
                    if (this.lstGrid[i].IdColaborador == this.IdColaborador)
                    {
                        blnAdicionar = false;
                        RadAjaxPanel2.Alert("Esse visitante já foi adicionado na lista.");
                        break;
                    }
                }
            }

            if (ddlEmpresaCad.SelectedItem.Text == "Outra...")
            {
                if (txtOutraEmpresa.Text == string.Empty)
                {
                    blnAdicionar = false;
                    RadAjaxPanel2.Alert("Insira o nome da empresa.");
                }
            }

            if (blnAdicionar)
            {
                this.BindModelColaborador(Enums.TipoTransacao.CarregarDados);

                //verifica se é uma nova empresa
                if (gobjColaborador.IdEmpresa == 0)
                {
                    BLEmpresa objBlEmpresa = new BLEmpresa();
                    Empresa objEmpresa = new Empresa();

                    objEmpresa.DescricaoEmpresa = txtOutraEmpresa.Text;

                    gobjColaborador.IdEmpresa = objBlEmpresa.Inserir(objEmpresa);

                    //verifica se a empresa já existe
                    if (gobjColaborador.IdEmpresa == -1)
                    {
                        RadAjaxPanel2.Alert("A empresa " + objEmpresa.DescricaoEmpresa + " já existe");
                        blnAdicionar = false;
                    }
                    else
                    {
                        this.PopularEmpresa();
                    }
                }

                if (blnAdicionar)
                {
                    gobjColaborador.Situacao = true;
                    //adiciona o novo registro
                    this.lstGrid.Add(gobjColaborador);

                    radGridVisitantes.DataSource = this.lstGrid;
                    radGridVisitantes.DataBind();

                    this.BindModelColaborador(Enums.TipoTransacao.Novo);
                    this.DesabilitarCampos();
                    ddlTipoVisitante.Enabled = true;
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
                this.BindModelFilial(Enums.TipoTransacao.Novo);
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
            this.BindModelColaborador(Enums.TipoTransacao.Novo);
            this.DesabilitarCampos();
            ddlTipoVisitante.Enabled = true;
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

            if (txtNomeListaCad.Text != string.Empty && ddlRegionalCad.SelectedIndex > 0 && ddlFilialCad.SelectedIndex > 0)
            {
                if (this.lstGrid.Count > 1)
                {
                    BLLista objBlLista = new BLLista();
                    this.BindModelFilial(Enums.TipoTransacao.CarregarDados);

                    try
                    {
                        for (int i = 0; i < this.lstGrid.Count; i++)
                        {
                            //verifica se o colaborador já existe
                            if (this.lstGrid[i].IdColaborador == 0)
                            {
                                BLColaborador objBlColaborador = new BLColaborador();
                                gobjColaborador = new Colaborador();

                                gobjColaborador.IdTipoColaborador = this.lstGrid[i].IdTipoColaborador;
                                gobjColaborador.NomeColaborador = this.lstGrid[i].NomeColaborador;
                                gobjColaborador.IdEmpresa = this.lstGrid[i].IdEmpresa;
                                gobjColaborador.NumeroDocumento = this.lstGrid[i].NumeroDocumento;
                                gobjColaborador.TipoDocumento = this.lstGrid[i].TipoDocumento;
                                gobjColaborador.IdTipoDocumento = this.lstGrid[i].IdTipoDocumento;


                                //insere o colaborador
                                gobjColaborador.IdColaborador = objBlColaborador.Inserir(gobjColaborador);
                                //insere o documento do colaborador
                                objBlColaborador.InserirDocumentoVisitante(gobjColaborador);

                                this.lstGrid[i].IdColaborador = gobjColaborador.IdColaborador;

                            }
                        }

                        if (blnGravar)
                        {
                            if (this.IdLista == 0)
                            {
                                gobjLista.IdLista = objBlLista.Inserir(gobjLista, this.lstGrid);
                            }
                            else
                            {
                                gobjLista.IdLista = this.IdLista;
                                gobjLista.IdLista = objBlLista.Alterar(gobjLista, this.lstGrid);
                            }

                            if (gobjLista.IdLista != -1)
                            {
                                if(this.IdLista == 0)
                                {
                                    this.IdLista = gobjLista.IdLista;
                                }

                                RadAjaxPanel1.Alert("Lista " + gobjLista.DescricaoLista+ " gravada com sucesso! Código da lista: " +  this.IdLista.ToString()  + ".");
                                blnGravar = true;
                                this.PopularEmpresa();
                            }
                            else
                            {
                                RadAjaxPanel1.Alert("Já existe uma lista com o nome: " + gobjLista.DescricaoLista + ".");
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
                    RadAjaxPanel1.Alert("A Lista deve conter pelo menos 2 (dois) visitantes.");
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
            BLLista objBlLista = new BLLista();

            try
            {
                gobjLista = objBlLista.Obter(this.IdLista);
                this.BindModelFilial(Enums.TipoTransacao.DescarregarDados);
                this.BindColaborador(Enums.TipoBind.DataBind);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Obter Colaborador

        /// <summary>
        ///     Obtem as informações do colaborador selecionado
        /// </summary>
        /// <history>
        ///     [mribeiro] created 03/07/2009
        /// </history>
        protected void ObterColaborador()
        {
            BLColaborador objBlColaborador = new BLColaborador();

            try
            {
                gobjColaborador= objBlColaborador.Obter(this.IdColaborador);
                this.BindModelColaborador(Enums.TipoTransacao.DescarregarDados);
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

        /// <summary> 
        ///     Codigo do colaborador
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 01/07/2009 Created 
        /// </history> 
        public int IdColaborador
        {
            get
            {
                if ((this.ViewState["vsColaborador"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsColaborador"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsColaborador", value);
            }
        }

        /// <summary> 
        ///     Codigo da Lista
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 01/07/2009 Created 
        /// </history> 
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
        ///     Armazena os colaboradores adicionados na lista
        /// </summary> 
        /// <value>Colaborador</value> 
        /// <history> 
        ///     [mribeiro] 01/07/2009 Created 
        /// </history> 
        public List<Colaborador> lstGrid
        {
            get
            {
                if (ViewState["vsList"] == null)
                {
                    ViewState["vsList"] = new List<Colaborador>();
                }
                return (List<Colaborador>)ViewState["vsList"];
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

            if (ddlTipoVisitante.SelectedItem.Text.Trim() == "Visitante")
            {
                rwdWindow.Title = "Selecione um colaborador ou clique em fechar para adicionar um novo.";
            }
            else
            {
                rwdWindow.Title = "Selecione um colaborador ou clique em fechar para sair da tela.";
            }

            rwdWindow.NavigateUrl = "ListColaboradoresVis.aspx?Tipo=" + ddlTipoVisitante.SelectedItem.Value.ToString();
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlExclusao = null;

            //Tenta encontrar na master
            pnlExclusao = (Panel)this.FindControl("pnlLista");
            pnlExclusao.Controls.Add(rwmWindowManager);
        }

        #endregion

    }
}
