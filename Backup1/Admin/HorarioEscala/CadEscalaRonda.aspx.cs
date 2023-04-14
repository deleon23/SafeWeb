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
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using FrameWork.BusinessLayer.Usuarios;

using SafWeb.BusinessLayer.Regional;
using System.Collections.ObjectModel;

using System.Web.UI.WebControls;
using SafWeb.BusinessLayer.Email;
using System.Collections.Generic;

using SafWeb.Model.Escala;
using SafWeb.BusinessLayer.Escala;
using System.Globalization;



namespace SafWeb.UI.Admin.Horarios
{
    public partial class CadEscalaRonda: FWPage
    {
        private HorarioLegado gobjHorarioEscala;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.InicializaScripts();
                this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
            }
        }

        #region Eventos

        #region Jornada
        /// <summary>
        ///     Seleciona uma jornada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] created 06/01/2012 15:52
        /// </history>
        protected void ddlJornadaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.PopularFilial(ref this.ddlRegionalCad, ref ddlFilialCad);
        }
        #endregion
        #endregion

        #region Popular Combos

        #region PopularListaJornadas
        /// <summary>
        /// Preenche a Lista de Jornadas.
        /// </summary>
        /// <history>
        ///     [haguiar] created 06/01/2012 15:45
        /// </history>
        private void PopularListaJornadas(ref DropDownList ddlJornadaRef)
        {
            BLJornada objBLJornada = new BLJornada();
            Collection<Jornada> colJornadas = null;

            try
            {
                colJornadas = objBLJornada.ListarJornadas();

                ddlJornadaRef.DataTextField = "DescricaoJornada";
                ddlJornadaRef.DataValueField = "IdJornada";

                ddlJornadaRef.DataSource = colJornadas;
                ddlJornadaRef.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlJornadaRef, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                //ddlJornadaRef.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion


        #endregion

        #region Bind

        #region Bind Legado Ronda
        /// <summary>
        /// Bind Legado Ronda
        /// </summary>
        ///<param name="penmTipoBind">Tipo de Bind</param>
        /// <history>
        ///     [haguiar] create 11/01/2012 14:53
        /// </history>
        private void BindLegadoRonda(Enums.TipoBind penmTipoBind)
        {
            BLEscala objBLEscala = new BLEscala();

            try
            {
                Collection<EscalaLegadoRonda> colEscalaLegadoRonda = null;
                colEscalaLegadoRonda = objBLEscala.ListarEscalasRonda();

                radGridHorarioLegadoRonda.DataSource = colEscalaLegadoRonda;

                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    radGridHorarioLegadoRonda.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region BindHorario
        /// <summary>
        /// Bind Horários
        /// </summary>
        ///<param name="penmTipoBind">Tipo de Bind</param>
        /// <history>
        ///     [haguiar] create 21/11/2011 17:33
        /// </history>
        private void BindHorarios(Enums.TipoBind penmTipoBind)
        {
            BLEscala objBLEscala = new BLEscala();

            this.IdEscala = 0;
            this.BlnEditar = false;

            int intCodLegado;
            int.TryParse(this.txtCodEscalaList.Text, out intCodLegado);

            try
            {
                Collection<HorarioLegado> colHorarioEscalas = null;
                colHorarioEscalas = objBLEscala.ListarHorarioEscalas(0, Convert.ToInt32(this.ddlJornadalist.SelectedValue), this.txtDescricaoList.Text, intCodLegado);
                
                radGridHorarios.DataSource = colHorarioEscalas;

                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    radGridHorarios.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region BindCadastro
        /// <summary>
        /// Bind Cadastro
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo da Transação</param>
        /// <history>
        ///     [haguiar] create 21/11/2011 18:03
        /// </history>
        protected void BindCadastro(Enums.TipoTransacao penmTipoTransacao)
        {
            //atribuir as informações na tela para os objetos
            if (penmTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                this.radGridHorarioLegadoRonda.Visible = false;

                this.gobjHorarioEscala = new HorarioLegado();
                this.gobjHorarioEscala.IdEscala = this.IdEscala;

                this.gobjHorarioEscala.IdJornada = Convert.ToInt32(this.ddlJornadaCad.SelectedValue);

                gobjHorarioEscala.DesEscala = this.txtDescricaoCad.Text;
                gobjHorarioEscala.HrEntrada = this.txtHorarioCad.Text;
                gobjHorarioEscala.DurRefeicao =this.txtDurRefeicaoCad.Text;

                gobjHorarioEscala.CodLegado = Convert.ToInt32(this.txtCodEscalaCad.Text);
            }
            //atribuir as informações dos objetos para tela
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.radGridHorarioLegadoRonda.Visible = false;
                this.txtHorarioCad.Enabled = true;

                if (gobjHorarioEscala != null)
                {
                    BLUtilitarios.ConsultarValorCombo(ref ddlJornadaCad, gobjHorarioEscala.IdJornada.ToString());

                    this.txtDescricaoCad.Text = gobjHorarioEscala.DesEscala;
                    this.txtHorarioCad.Text = gobjHorarioEscala.HrEntrada;
                    this.txtDurRefeicaoCad.Text = gobjHorarioEscala.DurRefeicao;
                    this.txtCodEscalaCad.Text = gobjHorarioEscala.CodLegado.ToString();
                }
            }
            //inicializa as informações na tela
            else if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.IdEscala = 0;

                this.txtDescricaoCad.Text = string.Empty;
                this.txtHorarioCad.Text = string.Empty;
                this.txtDurRefeicaoCad.Text = string.Empty;
                this.txtCodEscalaCad.Text = string.Empty;
                this.txtHorarioCad.Enabled = false;

                this.ddlJornadaCad.SelectedIndex = 0;

                this.radGridHorarioLegadoRonda.Visible = true;
                this.BindLegadoRonda(Enums.TipoBind.DataBind);
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
            this.BindHorarios(Enums.TipoBind.DataBind);            
        }
        #endregion

        #region BindModel
        /// <summary>
        /// Bind Model 
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo de Transação</param>
        /// <param name="penmTipoPainel">Tipo do Painel</param>
        /// <history>
        ///     [haguiar] created 21/11/2011 18:10
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
            Page.Validate("Cadastro");
            
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

        #region Botão Buscar
        /// <summary>
        ///     Pesquisa de horários
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] 06/01/2012 15:53
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BindHorarios(Enums.TipoBind.DataBind);
        }
        #endregion

        #endregion

        #region Controla Painels
        /// <summary>
        /// Controla os Painels de Listagem e Cadastro
        /// </summary>
        /// <param name="penmPainel">Painel a ser exibido</param>
        /// <history>
        ///     [haguiar] created 21/11/2011 16:05
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

        #region Editar
        /// <summary>
        /// Editar um cadastro
        /// </summary>
        /// <param name="pintIdEscala">Id do horario da escala</param>
        /// <history>
        ///     [haguiar] created 06/01/2012 16:30
        /// </history>
        private void Editar(int pintIdEscala)
        {
            BLEscala objEscala = new BLEscala();

            try
            {
                lblMensagem.Visible = false;
                lblMensagem.Text = string.Empty;

                this.BlnEditar = true;

                this.IdEscala = pintIdEscala;
                this.gobjHorarioEscala = objEscala.ObterHorarioEscala(pintIdEscala);

                this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Cadastro);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Gravar
        /// <summary>
        ///     Grava o cadastro do alerta
        /// </summary>
        /// <history>
        ///     [haguiar] created 21/11/2011 16:10
        /// </history>
        private void Gravar()
        {
            BLEscala objBLEscala = new BLEscala();
            int intRetorno = -1;

            try
            {                
                this.BindCadastro(Enums.TipoTransacao.CarregarDados);

                if (!BlnEditar)
                {
                    intRetorno = objBLEscala.InsereCodigoEscala(gobjHorarioEscala);
                }
                else if (this.IdEscala > 0)
                {
                    intRetorno = objBLEscala.AlterarHorarioEscala(gobjHorarioEscala);

                    BlnEditar = false;
                    this.IdEscala = 0;
                }

                if (intRetorno > 0)
                {
                    this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
                    lblMensagem.Visible = false;

                    this.RadAjaxPanel1.Alert("Cadastro efetuado com sucesso.");
                }
                else
                {
                    lblMensagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR_ERRO));
                    lblMensagem.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Grid Escala Legado Ronda

        #region NeedDataSource
        protected void radGridHorarioLegadoRonda_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindLegadoRonda(Enums.TipoBind.SemDataBind);
            }
        }
        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridHorarios
        /// </summary>
        /// <history>
        ///     [haguiar] created 21/11/2011 17:15
        /// </history>        
        protected void radGridHorarioLegadoRonda_ItemCommand(object source, GridCommandEventArgs e)
        {

            if (e.CommandName.Trim() == "Editar")
            {
                if (e.CommandArgument.ToString().Trim() != string.Empty)
                {
                    if (Permissoes.Alteração())
                    {
                        //preenche os campos de cadastro com as informações da escala
                        this.txtDescricaoCad.Text = radGridHorarioLegadoRonda.Items[e.Item.ItemIndex]["NomEsc"].Text;
                        this.txtCodEscalaCad.Text = radGridHorarioLegadoRonda.Items[e.Item.ItemIndex]["CodEsc"].Text;
                        this.txtDurRefeicaoCad.Text = "01:00";

                        DateTime dtHorario;
                        if (DateTime.TryParse(radGridHorarioLegadoRonda.Items[e.Item.ItemIndex]["NomEsc"].Text.Substring(0, 5), out dtHorario))
                        {
                            this.txtHorarioCad.Text = dtHorario.ToString("HH:mm:ss.f", CultureInfo.InvariantCulture).Substring(0, 5);
                            this.txtHorarioCad.Enabled = false;
                        }
                        else
                        {
                            this.txtHorarioCad.Enabled = true;
                            this.txtHorarioCad.Text = string.Empty;
                        }
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
        }

        #endregion

        #region ItemDataBound
        protected void radGridHorarioLegadoRonda_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;

                if (Permissoes.Alteração() && btnEditar != null)
                {
                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "CodEsc").ToString();
                    e.Item.ToolTip = "Selecionar";
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

        #region Grid Horarios Listagem

        #region NeedDataSource
        protected void radGridHorarios_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindHorarios(Enums.TipoBind.SemDataBind);
            }
        }        
        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridHorarios
        /// </summary>
        /// <history>
        ///     [haguiar] created 21/11/2011 17:15
        /// </history>        
        protected void radGridHorarios_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Ativar")
            {
                bool blnSituacao = true;

                int intIdEscala = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                try
                {
                    BLEscala objBLEscala = new BLEscala();

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

                    objBLEscala.AlterarSituacaoCodEscala(intIdEscala, blnSituacao);
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
        }

        #endregion

        #region ItemDataBound
        protected void radGridHorarios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                if (Permissoes.Alteração() && btnEditar != null)
                {
                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdEscala").ToString();
                    //btnEditar.Visible = true;
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                }

                if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "FlgSituacao")))
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                    btnAtivar.ToolTip = "Inativar";
                    btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdEscala").ToString();
                }
                else
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                    btnAtivar.ToolTip = "Ativar";
                    btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdEscala").ToString();
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
        
        #region Inicializa Scripts
        /// <summary>
        /// Inicializa os scripts 
        /// </summary>        
        /// <history>
        ///     [haguiar] created 21/11/2011 16:05
        /// </history>
        protected void InicializaScripts()
        {
            //BlnEditar = false;

            lblMensagem.Visible = false;
            lblMensagem.Text = string.Empty;

            PopularListaJornadas(ref this.ddlJornadalist);
            PopularListaJornadas(ref this.ddlJornadaCad);
        }
        
        #endregion
      
        #region Propriedades

        #region Editar
        /// <summary>
        ///     Propriedade Editar que verifica se está ou não editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] created 21/11/2011 16:03
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

        #region IdEscala
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdEscala
        /// </summary> 
        /// <history> 
        ///     [haguiar] 06/01/2012 11:33
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdEscala
        {
            get
            {
                if ((this.ViewState["vsIdEscala"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdEscala"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdEscala", value);
            }
        }

        #endregion

        #endregion
    }
}