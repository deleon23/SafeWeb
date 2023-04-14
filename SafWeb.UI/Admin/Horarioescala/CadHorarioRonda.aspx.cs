using System;
using System.Web.UI;
using Telerik.WebControls;

using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;

using System.Collections.ObjectModel;

using System.Web.UI.WebControls;
using System.Collections.Generic;

using SafWeb.Model.Escala;
using SafWeb.BusinessLayer.Escala;
using System.Globalization;
using SafWeb.Util.Extension;

namespace SafWeb.UI.Admin.Horarios
{
    public partial class CadHorarioRonda: FWPage
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
                Collection<HorarioLegadoRonda> colEscalaLegadoRonda = null;
                colEscalaLegadoRonda = objBLEscala.ListarHorarioRonda();

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

            int intIdJornada;

            intIdJornada = ddlJornada.SelectedValue.ToInt32();

            try
            {
                Collection<HorarioLegado> colHorarioEscalas = null;
                colHorarioEscalas = objBLEscala.ListarRegistroHorarios(0, this.txtDescricaoList.Text, intCodLegado, intIdJornada);
                
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

                gobjHorarioEscala.DesEscala = this.txtDescricaoCad.Text;
                gobjHorarioEscala.HrEntrada = this.txtHorarioCad.Text;

                gobjHorarioEscala.CodLegado = Convert.ToInt32(this.txtCodEscalaCad.Text);

                gobjHorarioEscala.IdJornada = ddlJornadaCad.SelectedValue.ToInt32();
            }
            //atribuir as informações dos objetos para tela
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.radGridHorarioLegadoRonda.Visible = false;

                if (gobjHorarioEscala != null)
                {
                    PopularComboJornadas(ddlJornadaCad);
                    ddlJornadaCad.SelectedIndex = 0;
                    ddlJornadaCad.Enabled = true;

                    this.txtDescricaoCad.Text = gobjHorarioEscala.DesEscala;
                    this.txtHorarioCad.Text = gobjHorarioEscala.HrEntrada;
                    this.txtCodEscalaCad.Text = gobjHorarioEscala.CodLegado.ToString();

                    this.SelecionaValorCompoJornada(this.ddlJornadaCad, gobjHorarioEscala.CodLegado, gobjHorarioEscala.DesEscala);
                    //this.ddlJornadaCad.SelectedValue = gobjHorarioEscala.IdJornada.ToString();

                    this.txtHorarioCad.Enabled = true;
                }

            }
            //inicializa as informações na tela
            else if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.IdEscala = 0;

                this.txtDescricaoCad.Text = string.Empty;
                this.txtHorarioCad.Text = string.Empty;
                this.txtCodEscalaCad.Text = string.Empty;

                this.txtHorarioCad.Enabled = false;

                PopularComboJornadas(ddlJornadaCad);
                ddlJornadaCad.SelectedIndex = 0;
                ddlJornadaCad.Enabled = true;

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
                this.gobjHorarioEscala = objEscala.ObterRegistroHorario(pintIdEscala);

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
                    intRetorno = objBLEscala.InsereCodigoHorario(gobjHorarioEscala);
                }
                else if (this.IdEscala > 0)
                {
                    intRetorno = objBLEscala.AlterarRegistroHorario(gobjHorarioEscala);

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

        protected void SelecionaValorCompoJornada(DropDownList objCombo, int codEscala, string strDescricao)
        {
            objCombo.Enabled = true;
            objCombo.SelectedIndex = 0;

            if (codEscala.IsBetween(3001, 3288) || strDescricao.ContainsRegex(@"\(\s?0?7:20\s?\)"))
            {
                objCombo.SelectedValue = "1";
                objCombo.Enabled = false;
            }
            else if (codEscala.IsBetween(3301, 3588) || strDescricao.ContainsRegex(@"\(\s?0?8:48\s?\)"))
            {
                objCombo.SelectedValue = "2";
                objCombo.Enabled = false;
            }
        }

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
                        this.txtDescricaoCad.Text = radGridHorarioLegadoRonda.Items[e.Item.ItemIndex]["DesHor"].Text;
                        this.txtCodEscalaCad.Text = radGridHorarioLegadoRonda.Items[e.Item.ItemIndex]["CodHor"].Text;

                        DateTime dtHorario;
                        if (DateTime.TryParse(radGridHorarioLegadoRonda.Items[e.Item.ItemIndex]["DesHor"].Text.Substring(0, 5), out dtHorario))
                        {
                            this.txtHorarioCad.Text = dtHorario.ToString("HH:mm:ss.f", CultureInfo.InvariantCulture).Substring(0, 5);
                            this.txtHorarioCad.Enabled = false;
                        }
                        else
                        {
                            this.txtHorarioCad.Enabled = true;
                            this.txtHorarioCad.Text = string.Empty;
                        }

                        this.SelecionaValorCompoJornada(ddlJornadaCad, this.txtCodEscalaCad.Text.ToInt32(), this.txtDescricaoCad.Text);

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
                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "CodHor").ToString();
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

                    objBLEscala.AlterarSituacaoCodHorario(intIdEscala, blnSituacao);
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

            PopularComboJornadas(ddlJornada);
            ddlJornada.SelectedIndex = 0;
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

        #region PopularListaJornadas
        /// <summary>
        /// Preenche a Lista de Jornadas.
        /// </summary>
        /// <history>
        ///     [cfrancisco] created 13/1/2010
        /// </history>
        private void PopularComboJornadas(DropDownList objCombo)
        {
            BLJornada objBLJornada = new BLJornada();
            try
            {
                var ListJornadas = new List<Jornada>(objBLJornada.ListarJornadas());

                ListJornadas.Insert(0, new Jornada { DescricaoJornada = "Selecione", IdJornada = 0 });
                objCombo.DataSource = ListJornadas;
                objCombo.DataTextField = "DescricaoJornada";
                objCombo.DataValueField = "IdJornada";
                objCombo.DataBind();
    }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion
    }
}