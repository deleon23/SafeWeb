using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.WebControls;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Escala;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using SafWeb.Model.Escala;
using System.Web.UI.WebControls;
using SafWeb.BusinessLayer.Filial;
using System.Collections.Generic;
using FrameWork.BusinessLayer.Usuarios;
using SafWeb.Model.Filial;
using SafWeb.Model.Regional;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.Model.Colaborador;
using System.Text;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class CadEscalaDepartamental : FWPage
    {
        private EscalaDepartamental gobjEscalaDepartamental;
        private Collection<HorarioEscala> gcolHorarios;
        private Collection<Colaborador> gcolColaboradores;

        #region Property
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


        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdPeriodicidade
        /// </summary> 
        /// <history> 
        ///     [haguiar_5] 14/02/2011 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdPeriodicidade
        {
            get
            {
                if ((this.ViewState["vsIdPeriodicidade"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdPeriodicidade"]);
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                this.ViewState.Add("vsIdPeriodicidade", value);
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade Situacao
        /// </summary> 
        /// <history> 
        ///     [haguiar_4] 14/02/2011 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool Situacao
        {
            get
            {
                if ((this.ViewState["vsSituacao"] != null))
                {
                    return Convert.ToBoolean(this.ViewState["vsSituacao"]);
                }
                else
                {
                    return false;
                }
            }
            set
            {
                this.ViewState.Add("vsSituacao", value);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Inicializa();
            VerificarHiddenColaboradores();

            if (!this.Page.IsPostBack)
            {
                this.InicializaScripts();
                this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
            }
        }
                
        #region Bind

        #region BindCadastro
        /// <summary>
        /// Bind Cadastro
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo da Transação</param>
        /// <history>
        ///     [cmarchi] created 29/12/2009
        ///     [cmarchi] modify 4/01/2010
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar_5] modify 14/02/2011
        ///     [haguiar] modify 22/03/2011
        ///     IdEscalaDepartamental = 0 e blnEditar=false em novo cadastro de escala departamental.
        ///     [haguiar] modify 28/03/2011
        ///     incluir campo replicaRH
        /// </history>
        protected void BindCadastro(Enums.TipoTransacao penmTipoTransacao)
        {
            //atribuir as informações na tela para os objetos
            if (penmTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                this.gobjEscalaDepartamental = new EscalaDepartamental();

                this.gobjEscalaDepartamental.ObjRegional = new Regional();
                this.gobjEscalaDepartamental.ObjRegional.IdRegional = Convert.ToInt32(
                    this.ddlRegionalCad.SelectedValue);

                this.gobjEscalaDepartamental.ObjFilial = new Filial();
                this.gobjEscalaDepartamental.ObjFilial.IdFilial = Convert.ToInt32(
                    this.ddlFilialCad.SelectedValue);

                this.gobjEscalaDepartamental.ObjPeriodicidade = new Periodicidade();
                this.gobjEscalaDepartamental.ObjPeriodicidade.IdPeriodicidade = Convert.ToInt32(
                    this.ddlPeriodicidadeCad.SelectedValue);

                this.gobjEscalaDepartamental.DescricaoEscalaDpto = this.txtDescricaoCad.Text.Trim();

                this.gobjEscalaDepartamental.IdUsuarioAlteracao = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                this.gobjEscalaDepartamental.Situacao = this.Situacao;
                this.gobjEscalaDepartamental.Flg_ReplicaRH = this.chkReplicaRH.Checked;

                this.ObterHorariosSelecionados();
                this.ObterListaColaboradores();
            }
            //atribuir as informações dos objetos para tela
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.txtDescricaoCad.Text = this.gobjEscalaDepartamental.DescricaoEscalaDpto;

                BLUtilitarios.ConsultarValorCombo(ref this.ddlRegionalCad, this.gobjEscalaDepartamental.ObjRegional.IdRegional.ToString());
                
                //this.PopularFilial(ref this.ddlRegionalCad, ref this.ddlFilialCad);
                BLUtilitarios.ConsultarValorCombo(ref this.ddlFilialCad, this.gobjEscalaDepartamental.ObjFilial.IdFilial.ToString());
                //this.ddlFilialCad.Enabled = true;

                BLUtilitarios.ConsultarValorCombo(ref this.ddlPeriodicidadeCad, this.gobjEscalaDepartamental.ObjPeriodicidade.IdPeriodicidade.ToString());

                //this.IdPeriodicidade = this.gobjEscalaDepartamental.ObjPeriodicidade.IdPeriodicidade;
                this.Situacao = this.gobjEscalaDepartamental.Situacao;
                this.chkReplicaRH.Checked = this.gobjEscalaDepartamental.Flg_ReplicaRH;

                this.PopularHorariosAlteracao();
                this.PopularColaboradores();
            }
            //inicializa as informações na tela
            else if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.IdEscalaDepartamental = 0;
                this.txtDescricaoCad.Text = string.Empty;
                this.InicializaScripts();

                this.chkReplicaRH.Checked = true;
                this.Situacao = false;
                this.gcolColaboradores = null;
                this.lstColaboradores.Items.Clear();
                this.gobjEscalaDepartamental = null;

                BlnEditar = false;
            }
        }
        #endregion

        #region BindEscalaDepartamental
        /// <summary>
        /// Bind Escala Departamental
        /// </summary>
        ///<param name="penmTipoBind">Tipo de Bind</param>
        /// <history>
        ///     [cmarchi] created 4/1/2009
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar] modify 28/08/2011 16:11
        ///     nao listar escalas crew
        /// </history>
        private void BindEscalaDepartamental(Enums.TipoBind penmTipoBind)
        {
            int intRegional = 0;
            int intFilial = 0;
            int intPeriodicidade = 0;

            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();

            /*
            intRegional = Convert.ToInt32(
                this.ddlRegionalList.SelectedValue);

            intFilial = Convert.ToInt32(
                this.ddlFilialList.SelectedValue);
            */

            intPeriodicidade = Convert.ToInt32(
                this.ddlPeriodicidadeList.SelectedValue);

            intRegional = IdRegional;
            intFilial = IdFilial;

            try
            {
                radGridEscalaDepartamental.DataSource = objBLEscalaDepartamental.ListarEscalaDepartamental(intRegional,
                    intFilial,
                    this.txtDescricaoList.Text.Trim(),
                    intPeriodicidade, false, false);

                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    radGridEscalaDepartamental.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region BindListagem
        /// <summary>
        /// Bind Listagem
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo da Transação</param>        
        /// <history>
        ///     [cmarchi] created 29/12/2009
        ///     [cmarchi] modify 4/1/2009
        /// </history>
        protected void BindListagem(Enums.TipoTransacao penmTipoTransacao)                                    
        {            
            //atribuir as informações dos objetos para tela
            if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                //BLEscalaDepartamental objEscala
                this.txtDescricaoList.Text = string.Empty;
            }

            this.BindEscalaDepartamental(Enums.TipoBind.DataBind);
        }
        #endregion

        #region BindModel
        /// <summary>
        /// Bind Model 
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo de Transação</param>
        /// <param name="penmTipoPainel">Tipo do Painel</param>
        /// <history>
        ///     [cmarchi] created 17/12/2009
        ///     [cmarchi] modify 29/12/2009
        /// </history>
        protected void BindModel(Enums.TipoTransacao penmTipoTransacao,
                                 Enums.TipoPainel penmTipoPainel)
        {
            //verifica se  listagem ou cadastro na página
            if (penmTipoPainel == Enums.TipoPainel.Cadastro)
            {
                this.ControlaPaineis(Enums.TipoPainel.Cadastro);
                this.BindCadastro(penmTipoTransacao);
            }
            else
            {
                this.ControlaPaineis(Enums.TipoPainel.Listagem);
                this.BindListagem(penmTipoTransacao);                
            }
        }
        #endregion

        #endregion

        #region Botões

        #region Botão Adicionar Horários Selecionados
        /// <summary>
        ///     Botão Adicionar Horários Selecionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] 4/01/2010 created
        /// </history>
        protected void btnAddUmCad_Click(object sender, EventArgs e)
        {
            if (lstHorariosCad.SelectedIndex > -1)
            {
                int intCount = lstHorariosCad.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstHorariosCad.Items[i].Selected)
                    {                        
                        lstHorariosSelecionadosCad.Items.Add(new ListItem(lstHorariosCad.Items[i].Text,
                                                                          lstHorariosCad.Items[i].Value));
                        lstHorariosCad.Items.RemoveAt(i);
                    }
                }
            }
        }

        #endregion
        
        #region Botão Buscar
        /// <summary>
        ///     Pesquisa de escalas Departamentais
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] 17/12/2009 created
        ///     [cmarchi] 4/1/2010 modify
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BindEscalaDepartamental(Enums.TipoBind.DataBind);
        }
        #endregion

        #region Botão Gravar
        /// <summary>
        ///     Grava os dados da tela de cadastro 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 30/12/2009 
        /// </history>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                this.Gravar();
            }
        }
        #endregion

        #region Botão Incluir
        /// <summary>
        ///     Abre a tela de cadastro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 17/12/2009 
        ///     [cmarchi] modify 29/12/2009
        /// </history>
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            this.BindModel(Enums.TipoTransacao.Novo, Enums.TipoPainel.Cadastro);
        }
        #endregion

        #region Botão Voltar
        /// <summary>
        ///     Volta para a parte de Listagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 19/11/2009 
        ///     [cmarchi] modify 17/12/2009 
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {            
            this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
        }
        #endregion

        #region Botão Remover Horários Selecionados
        /// <summary>
        ///     Botão Remover Horários Selecionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] 4/01/2010 created
        /// </history>
        protected void btnRemoverUmCad_Click(object sender, EventArgs e)
        {
            if (lstHorariosSelecionadosCad.SelectedIndex > -1)
            {
                int intCount = lstHorariosSelecionadosCad.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstHorariosSelecionadosCad.Items[i].Selected)
                    {
                        lstHorariosCad.Items.Add(new ListItem(lstHorariosSelecionadosCad.Items[i].Text,
                                                             lstHorariosSelecionadosCad.Items[i].Value));
                        lstHorariosSelecionadosCad.Items.RemoveAt(i);
                    }
                }
            }
        }
        #endregion

        #endregion

        #region Controla Painels
        /// <summary>
        /// Controla os Painels de Listagem e Cadastro de Escalas Departamentais.
        /// </summary>
        /// <param name="penmPainel">Painel a ser exibido</param>
        /// <history>
        ///     [cmarchi] created 17/12/2009
        /// </history>
        protected void ControlaPaineis(Enums.TipoPainel penmPainel)
        {
            if (penmPainel == Enums.TipoPainel.Listagem)
            {
                pnlCadastro.Visible = false;
                pnlListagem.Visible = true;
            }
            else
            {
                pnlCadastro.Visible = true;
                pnlListagem.Visible = false;
            }

        }
        #endregion

        /*
        #region Periodicidade
        /// <summary>
        ///     Armazena a periodicidade selecionada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar_5] created 14/02/2011
        /// </history>
        protected void ddlPeriodicidadeCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IdPeriodicidade = Convert.ToInt32(ddlPeriodicidadeCad.SelectedValue.ToString());
        }
        #endregion
        */

        #region Editar
        /// <summary>
        /// Editar uma Escala Departamental
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Escala Departamental</param>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        /// </history>
        private void Editar(int pintIdEscalaDepartamental)
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();

            try
            {
                this.BlnEditar = true;
                this.IdEscalaDepartamental = pintIdEscalaDepartamental;
                this.gobjEscalaDepartamental = objBLEscalaDepartamental.Obter(pintIdEscalaDepartamental, false, null);
                this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Cadastro);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Eventos Regionais

        #region Regional Cadastro
        /// <summary>
        ///     Seleciona uma filial conforme Regional selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [haguiar] modify 27/10/2010
        /// </history>
        protected void ddlRegionalCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.PopularFilial(ref this.ddlRegionalCad, ref ddlFilialCad);
        }
        #endregion

        #region Regional Listagem
        /// <summary>
        ///     Seleciona uma filial conforme Regional selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [haguiar] modify 27/10/2010 
        /// </history>
        protected void ddlRegionalList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.PopularFilial(ref this.ddlRegionalList, ref ddlFilialList);
        }
        #endregion

        #endregion

        #region Gravar
        /// <summary>
        ///     Grava a Escala Departamental
        /// </summary>
        /// <history>
        ///     [cmarchi] created 17/12/2009 
        ///     [haguiar_5] modify 11/02/2011
        /// </history>
        private void Gravar()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            int intRetorno = -1;
            try
            {
                if (this.ValidarCampos())
                {
                    this.BindCadastro(Enums.TipoTransacao.CarregarDados);

                    if (!BlnEditar)
                    {
                        intRetorno = objBLEscalaDepartamental.Inserir(gobjEscalaDepartamental, gcolHorarios, this.gcolColaboradores);
                    }
                    else if (this.IdEscalaDepartamental > 0)
                    {
                        gobjEscalaDepartamental.IdEscalaDpto = this.IdEscalaDepartamental;
                        intRetorno = objBLEscalaDepartamental.Alterar(gobjEscalaDepartamental, gcolHorarios, this.gcolColaboradores);

                        BlnEditar = false;
                        this.IdEscalaDepartamental = 0;
                    }

                    lblMensagem.Visible = true;

                    if (intRetorno > 0)
                    {
                        this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
                        this.RadAjaxPanel1.Alert("Escala departamental gravada com sucesso.");
                    }
                    else
                    {
                        lblMensagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR_ERRO));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Grid Escala Listagem

        #region NeedDataSource
        protected void radGridEscalaDepartamental_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindEscalaDepartamental(Enums.TipoBind.SemDataBind);
            }
        }        
        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridEscalaDepartamental
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        /// </history>        
        protected void radGridEscalaDepartamental_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Ativar")
            {
                bool blnSituacao = true;

                int intIdEscalaDepartamental = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                try
                {
                    BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();

                    //altera o botão
                    if (btnAtivar.ToolTip == "Ativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        blnSituacao = true;
                    }
                    else if (btnAtivar.ToolTip == "Inativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        blnSituacao = false;
                    }

                    objBLEscalaDepartamental.AlterarSituacao(intIdEscalaDepartamental, blnSituacao, Convert.ToInt32(BLAcesso.IdUsuarioLogado()));
                    
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }

            if (e.CommandName.Trim() == "Editar")
            {
                if (e.CommandArgument.ToString().Trim() != string.Empty)
                {
                    if (Permissoes.Alteração())
                    {
                        this.Editar(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                    }
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

            /*
            //[haguiar]
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
            */
        }

        #endregion

        #region ItemDataBound
        protected void radGridEscalaDepartamental_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];
                                
                if (Permissoes.Alteração() && btnEditar != null)
                {
                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_EscalaDpto").ToString();
                    //btnEditar.Visible = true;
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                }

                if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Situacao")))
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                    btnAtivar.ToolTip = "Inativar";
                    btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_EscalaDpto").ToString();
                }
                else
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                    btnAtivar.ToolTip = "Ativar";
                    btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_EscalaDpto").ToString();
                }

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
            }
        }
        #endregion

        #endregion

        #region Inicializa
        /// <summary>
        /// Inicializa alguns campos da pagina
        /// </summary>
        /// <history>
        ///     [cmarchi] created 9/2/2010
        ///     [haguiar_5] modify 14/02/2011
        /// </history>
        private void Inicializa()
        {
            lblMensagem.Visible = false;
            lblMensagem.Text = string.Empty;

            if (IdPeriodicidade >= 0)
            {
                //periodicidade selecionada
                BLUtilitarios.ConsultarValorCombo(ref this.ddlPeriodicidadeCad, this.IdPeriodicidade.ToString());

                IdPeriodicidade = -1;
            }

            //seleciona a regional e a filial do usuário logado.
            BLColaborador objBlColaborador = new BLColaborador();
            DataTable dtt = new DataTable();

            try
            {
                dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                this.IdRegional = Convert.ToInt32(dtt.Rows[0][0].ToString());
                this.IdFilial = Convert.ToInt32(dtt.Rows[0][1].ToString());

                //painel de listagem
                this.PopularRegional(ref this.ddlRegionalList);
                this.PopularFilial(ref this.ddlFilialList);

                //painel de cadastro
                this.PopularRegional(ref this.ddlRegionalCad);
                this.PopularFilial(ref this.ddlFilialCad);

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Inicializa Scripts
        /// <summary>
        /// Inicializa os scripts 
        /// </summary>        
        /// <history>
        ///     [cmarchi] created 29/12/2009
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar_5] modify 15/02/2011
        /// </history>
        protected void InicializaScripts()
        {
            lblMensagem.Visible = false;
            lblMensagem.Text = string.Empty;
            this.lstHorariosSelecionadosCad.Items.Clear();

            this.PopularPeriodicidade();
            //this.PopularRegional();
            this.PopularHorarios();

            //BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            //BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            //this.ddlFilialCad.Enabled = false;
        }
        
        #endregion

        #region ObterHorariosSelecionados
        /// <summary>
        /// Obtém os horários selecionados pelo usuário.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 4/01/2010
        /// </history>
        private void ObterHorariosSelecionados()
        {
            if (this.gcolHorarios == null)
                this.gcolHorarios = new Collection<HorarioEscala>();
            else
                this.gcolHorarios.Clear();

            foreach (ListItem h in this.lstHorariosSelecionadosCad.Items)
            {
                HorarioEscala objHorarioEscala = new HorarioEscala();
                objHorarioEscala.IdHorario = h.Value;

                this.gcolHorarios.Add(objHorarioEscala);
            }
        }
        #endregion

        #region Popular Combos

        #region Filial
        /// <summary>
        ///     Popula os combos com a filial do logado
        /// </summary>
        /// <param name="ddlRegional">Regional</param>
        /// <param name="ddlFilial">Filial</param>
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [haguiar] modify 27/10/2010
        /// </history>
        /// 
        #region Filial
            protected void PopularFilial(ref DropDownList ddlFilialTemp)
        {
            BLFilial objBlFilial = new BLFilial();
            SafWeb.Model.Filial.Filial gobjFilial;

            //limpa a lista
            ddlFilialTemp.Items.Clear();

            try
            {
                gobjFilial = objBlFilial.Obter(IdFilial);

                if (gobjFilial != null)
                {
                    ddlFilialTemp.Items.Add(new ListItem(gobjFilial.DescricaoFilial,
                                                        Convert.ToString(gobjFilial.IdFilial)));
                    ddlFilialTemp.SelectedIndex = 0;
                }
                else
                {
                    BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialTemp, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                    ddlFilialTemp.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        /*protected void PopularFilial(ref DropDownList ddlRegional, ref DropDownList ddlFilial)
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

                BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }*/
        #endregion

        #region Horários
        /// <summary>
        /// Popula uma Lista com os horários 
        /// </summary>        
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [cmarchi] modify 5/1/2010
        /// </history>
        private void PopularHorarios()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<HorarioEscala> colHorarios = objBLEscalaDepartamental.GerarHorarios();

            this.lstHorariosCad.DataTextField  = "IdHorario";
            this.lstHorariosCad.DataValueField = "IdHorario";

            this.lstHorariosCad.DataSource = colHorarios;
            this.lstHorariosCad.DataBind();
        }
        #endregion

        #region Horários Alteração
        /// <summary>
        /// Popula uma Lista com os horários para alteração
        /// </summary>        
        /// <history>
        ///     [cmarchi] created 5/1/2010
        ///     [haguiar] modify 21/10/2010
        /// </history>
        private void PopularHorariosAlteracao()
        {
            int intHorarioPosicao = -1;
            int intTamanho = 0;

            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<HorarioEscala> colHorarios = objBLEscalaDepartamental.GerarHorarios();
           
            this.lstHorariosCad.DataTextField = "IdHorario";
            this.lstHorariosCad.DataValueField = "IdHorario";

            this.lstHorariosSelecionadosCad.DataTextField  = "IdHorario";
            this.lstHorariosSelecionadosCad.DataValueField = "IdHorario";

            intTamanho = colHorarios.Count;

            //exclui os horários da lstHorariosCad que estejam na lista de horários da Escala
            foreach (HorarioEscala h in this.gobjEscalaDepartamental.HorariosEscala)
            {
                for (int i = 0; i < intTamanho; i++)
			    {

                    if (h.IdHorario == colHorarios[i].IdHorario)
                    {                        
                        colHorarios.RemoveAt(i);
                            
                        //reinicia contador
                        intTamanho = colHorarios.Count;
                        break;
                    }  
                    
			    }              
            }
          
            this.lstHorariosCad.DataSource = colHorarios;
            this.lstHorariosCad.DataBind();
            
            this.lstHorariosSelecionadosCad.DataSource = this.gobjEscalaDepartamental.HorariosEscala;
            this.lstHorariosSelecionadosCad.DataBind();            
        }
        #endregion

        #region Periodicidade
        /// <summary>
        ///     Popula o combo com a periodicidade
        /// </summary>
        /// <history>
        ///     [cmarchi] created 30/12/2009 
        ///</history>
        protected void PopularPeriodicidade()
        {
            BLPeriodicidade objBlPeriodicidade = new BLPeriodicidade();
            Collection<Periodicidade> colPeriodicidade;

            try
            {
                colPeriodicidade = objBlPeriodicidade.Listar();

                //preenche periodicidade da parte de listagem
                ddlPeriodicidadeList.DataSource = colPeriodicidade;
                ddlPeriodicidadeList.DataTextField = "DescricaoPeriodicidade";
                ddlPeriodicidadeList.DataValueField = "IdPeriodicidade";
                ddlPeriodicidadeList.DataBind();

                //preenche periodicidade da parte de cadastro
                ddlPeriodicidadeCad.DataSource = colPeriodicidade;
                ddlPeriodicidadeCad.DataTextField = "DescricaoPeriodicidade";
                ddlPeriodicidadeCad.DataValueField = "IdPeriodicidade";
                ddlPeriodicidadeCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlPeriodicidadeList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlPeriodicidadeCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Regional
        /// <summary>
        ///     Popula o combo com a regional do logado
        /// </summary>
        /// <history>
        ///     [cmarchi] created 29/12/2009 
        ///     [haguiar] 27/10/2010
        ///</history>

        protected void PopularRegional(ref DropDownList ddlRegionalTemp)
        {
            BLRegional objBlRegional = new BLRegional();
            Collection<SafWeb.Model.Regional.Regional> colRegional;

            try
            {
                //limpa a lista
                ddlRegionalTemp.Items.Clear();
                colRegional = objBlRegional.Listar();

                //preenche reginal do usuário logado

                foreach (Regional gobjRegional in colRegional)
                {
                    if (gobjRegional.IdRegional == this.IdRegional)
                    {
                        ddlRegionalTemp.Items.Add(new ListItem(gobjRegional.DescricaoRegional,
                                                             Convert.ToString(gobjRegional.IdRegional)));
                        ddlRegionalTemp.SelectedIndex = 0;

                        break;
                    }
                }

                if (colRegional == null | colRegional.Count == 0)
                    BLUtilitarios.InseriMensagemDropDownList(ref ddlRegionalTemp, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /*        protected void PopularRegional()
        {
            BLRegional objBlRegional = new BLRegional();
            Collection<SafWeb.Model.Regional.Regional> colRegional;

            try
            {
                colRegional = objBlRegional.Listar();

                //preenche regionais de parte de listagem
                ddlRegionalList.DataSource = colRegional;
                ddlRegionalList.DataTextField = "DescricaoRegional";
                ddlRegionalList.DataValueField = "IdRegional";
                ddlRegionalList.DataBind();

                //preenche regionais de parte de cadastro
                ddlRegionalCad.DataSource = colRegional;
                ddlRegionalCad.DataTextField = "DescricaoRegional";
                ddlRegionalCad.DataValueField = "IdRegional";
                ddlRegionalCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegionalList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegionalCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
 */
        #endregion

        #endregion

        #region Propriedades

        #region Editar
        /// <summary>
        ///     Propriedade Editar que verifica se está ou não editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 5/1/2009 
        /// </history>
        private bool BlnEditar
        {
            get
            {
                if (this.ViewState["vsEditar"] == null)
                {
                    this.ViewState.Add("vsEditar", false);
                }

                return Convert.ToBoolean(this.ViewState["vsEditar"]);
            }

            set 
            {
                this.ViewState.Add("vsEditar", value);
            }
        }
        #endregion

        #region IdEscalaDepartamental
        /// <summary>
        ///     Propriedade Id_EscalaDepartamental utilizada para Editar uma Escala Departamental
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 5/1/2009 
        /// </history>
        private int IdEscalaDepartamental
        {
            get
            {
                if (this.ViewState["vsIdEscalaDepartamental"] == null)
                {
                    this.ViewState.Add("vsIdEscalaDepartamental", 0);
                }

                return Convert.ToInt32(this.ViewState["vsIdEscalaDepartamental"]);
            }

            set
            {
                this.ViewState.Add("vsIdEscalaDepartamental", value);
            }
        }
        #endregion

        #endregion

        #region Validações

        #region cvrHorariosSelecionadosCad_ServerValidate
        /// <summary>
        /// Valida o a lista de horários selecionados na parte de cadastro.
        /// </summary>
        /// <history>
        ///     [cmarchi] Created 5/01/2010
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cvrHorariosSelecionadosCad_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (this.lstHorariosSelecionadosCad.Items.Count > 0)
                e.IsValid = true;
            else
                e.IsValid = false;
        }
        #endregion

        #region Validar Campos

        /// <summary>
        ///     Verifica se os campos foram preenchidos corretamente
        /// </summary>
        /// <returns>True - Válidos, False - Erros</returns>
        /// <history>
        ///     [cmarchi] created 7/01/2009
        ///     [haguiar] modify 27/10/2010
        /// </history>
        protected bool ValidarCampos()
        {
            bool blnRetorno = true;

            /*
            if (this.ddlRegionalCad.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Regional.");
                blnRetorno = false;
            }

            if (this.ddlFilialCad.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Filial.");
                blnRetorno = false;
            }
            */

            if (this.ddlRegionalCad.SelectedItem == null)
            {
                RadAjaxPanel1.Alert("Selecione uma Regional.");
                blnRetorno = false;
            }

            if (this.ddlFilialCad.SelectedItem == null)
            {
                RadAjaxPanel1.Alert("Selecione uma Filial.");
                blnRetorno = false;
            }

            if (this.ddlPeriodicidadeCad.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Periodicidade.");
                blnRetorno = false;
            }

            if (!string.IsNullOrEmpty(this.txtDescricaoCad.Text))
            {
                if (this.txtDescricaoCad.Text.Trim().Length == 0)
                {
                    RadAjaxPanel1.Alert("Campo Obrigatório: Descrição.");
                    blnRetorno = false;
                }
            }
            else
            {
                RadAjaxPanel1.Alert("Campo Obrigatório: Descrição.");
                blnRetorno = false;
            }

            return blnRetorno;
        }

        #endregion

        #endregion

        #region cvrListaColaboradores_ServerValidate
        /// <summary>
        /// Valida o a lista de Colaboradores.
        /// </summary>
        /// <history>
        ///     [haguiar_5] Created 11/02/2011
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cvrListaColaboradores_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (this.lstColaboradores.Items.Count > 0)
                e.IsValid = true;
            else
                e.IsValid = false;
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
        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            this.IdPeriodicidade = Convert.ToInt32(ddlPeriodicidadeCad.SelectedValue.ToString());
            
            this.RadWindowCadastroEscala();
        }

        #region RadWindow

        /// <summary>
        ///     Abre a RadWindow com a tela de listagem de colaboradores
        /// </summary>
        /// <history>
        ///     [haguiar_5] created 11/02/2011
        /// </history>
        protected void RadWindowCadastroEscala()
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

            rwdWindow.NavigateUrl = "ListColaboradoresEscala.aspx?open=" + BLEncriptacao.EncQueryStr("sim");
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlEscala = null;

            //Tenta encontrar na master
            pnlEscala = (Panel)this.FindControl("pnlCadColab");
            pnlEscala.Controls.Add(rwmWindowManager);
        }
        #endregion
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
        protected void btnRemover_Click(object sender, ImageClickEventArgs e)
        {
            //this.IdPeriodicidade = Convert.ToInt32(ddlPeriodicidadeCad.SelectedValue.ToString());

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

            if (!string.IsNullOrEmpty(this.txtHiddenColaboradores.Value))
                arrColaboradores = BLEncriptacao.DecQueryStr(this.txtHiddenColaboradores.Value).Split(',');

            this.PopularListaColaboradores(arrColaboradores);
        }

        #endregion

        #region PopularColaboradores
        /// <summary>
        ///     Preenche lista de colaboradores cadastrados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar_5] created 14/02/2011
        /// </history>
        protected void PopularColaboradores()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<Colaborador> colColaboradores = null;

            try
            {
                colColaboradores = objBLEscalaDepartamental.ObterColaboradores(
                                                   this.IdEscalaDepartamental);

                this.PopularListaColaboradores(colColaboradores);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Lista Colaboradores
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
                    this.txtHiddenColaboradores.Value = string.Empty;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        /// <summary>
        /// Preenche a Lista de Colaboradores.
        /// </summary>
        /// <param name="parrColaboradores">Array com os Id dos Colaboradores</param>
        /// <history>
        ///     [haguiar_5] created 14/02/2011
        /// </history>
        private void PopularListaColaboradores(Collection<Colaborador> pcolColaboradores)
        {
            ListItem limColaborador = null;

            this.lstColaboradores.Items.Clear();

            foreach (Colaborador objColaborador in pcolColaboradores)
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
    #endregion

        #region ObterListaColaboradores
        /// <summary>
        /// Obteém os Colaboradores da lista de Colaboradores
        /// </summary>
        /// <history>
        ///     [haguiar_5] created 11/02/2011
        /// </history>
        private void ObterListaColaboradores()
        {
            this.gcolColaboradores = new Collection<Colaborador>();

            foreach (ListItem var in this.lstColaboradores.Items)
            {
                Colaborador objColaborador = new Colaborador();
                objColaborador.IdColaborador = Convert.ToInt32(var.Value);

                this.gcolColaboradores.Add(objColaborador);
            }
        }
        #endregion
    }
}