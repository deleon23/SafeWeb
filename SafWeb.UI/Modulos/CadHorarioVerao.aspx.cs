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
using SafWeb.BusinessLayer.Filial;
using System.Collections.Generic;

using SafWeb.Model.Filial;
using SafWeb.Model.Regional;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.Model.Colaborador;

using System.Text;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class CadHorarioVerao : FWPage
    {
        private HorarioVerao gobjHorarioVerao;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //Inicializa();

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
        ///     [haguiar] modify 27/10/2010
        /// </history>
        protected void BindCadastro(Enums.TipoTransacao penmTipoTransacao)
        {
            //atribuir as informações na tela para os objetos
            if (penmTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                this.gobjHorarioVerao = new HorarioVerao();

                this.gobjHorarioVerao.IdHorarioVerao = this.IdHorarioVerao;

                this.gobjHorarioVerao.InicioPeriodo = Convert.ToDateTime(this.txtDataInicio.Text);
                this.gobjHorarioVerao.FinalPeriodo = Convert.ToDateTime(this.txtDataFim.Text);

                StringBuilder strCodigoFilial = new StringBuilder();

                //seleciona filiais
                foreach (ListItem FilialItem in this.lstFiliaisSelecionadas.Items)
                {
                    strCodigoFilial.Append(FilialItem.Value + ",");
                }

                //remove ultimo separador
                strCodigoFilial.Remove(strCodigoFilial.Length - 1, 1);

                gobjHorarioVerao.IdFiliais = strCodigoFilial.ToString();
            }
            //atribuir as informações dos objetos para tela
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.txtDataInicio.Text = this.gobjHorarioVerao.InicioPeriodo.ToShortDateString();
                this.txtDataFim.Text = this.gobjHorarioVerao.FinalPeriodo.ToShortDateString();

                int intTamanho = lstFiliais.Items.Count -1;

                if (this.gobjHorarioVerao.IdFiliais != null)
                {
                    foreach (string strId in this.gobjHorarioVerao.IdFiliais.Split(','))
                    {
                        if (strId.Trim().Equals(""))
                            break;

                        for (int i = 0; i < intTamanho; i++)
                        {
                            if (Convert.ToInt32(strId) == Convert.ToInt32(lstFiliais.Items[i].Value))
                            {
                                lstFiliaisSelecionadas.Items.Add(new ListItem(lstFiliais.Items[i].Text,
                                                                                  lstFiliais.Items[i].Value));

                                lstFiliais.Items.RemoveAt(i);

                                //reinicia contador
                                intTamanho = lstFiliais.Items.Count - 1;
                                break;
                            }
                        }
                    }
                }
            }
            //inicializa as informações na tela
            else if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.IdHorarioVerao = 0;
                this.txtDataInicio.Text = string.Empty;
                this.txtDataFim.Text = string.Empty;

                this.lstFiliaisSelecionadas.Items.Clear();
                this.InicializaScripts();
            }
        }
        #endregion

        #region BindHorarioVerao
        /// <summary>
        /// Bind HorarioVerao
        /// </summary>
        ///<param name="penmTipoBind">Tipo de Bind</param>
        /// <history>
        ///     [haguiar] modify 27/10/2010
        /// </history>
        private void BindHorarioVerao(Enums.TipoBind penmTipoBind)
        {

            BLHorarioVerao objBlHorarioVerao = new BLHorarioVerao();

            try
            {
                radGridHorarioVerao.DataSource = objBlHorarioVerao.Listar();

                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    radGridHorarioVerao.DataBind();
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
                //this.txtDescricaoList.Text = string.Empty;

                this.txtDataInicio.Text = string.Empty;
                this.txtDataFim.Text = string.Empty;

                this.lstFiliaisSelecionadas.Items.Clear();
            }

            this.BindHorarioVerao(Enums.TipoBind.DataBind);
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
                Inicializa();

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
            
            if (lstFiliais.SelectedIndex > -1)
            {
                int intCount = lstFiliais.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstFiliais.Items[i].Selected)
                    {
                        lstFiliaisSelecionadas.Items.Add(new ListItem(lstFiliais.Items[i].Text,
                                                                          lstFiliais.Items[i].Value));
                        lstFiliais.Items.RemoveAt(i);
                    }
                }
            }
            
        }

        #endregion
        
        #region Botão Buscar
        /// <summary>
        ///     Pesquisa de Horário de Verao
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] 17/12/2009 created
        ///     [cmarchi] 4/1/2010 modify
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BindHorarioVerao(Enums.TipoBind.DataBind);
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

            if (this.lstFiliaisSelecionadas.SelectedIndex > -1)
            {
                int intCount = lstFiliaisSelecionadas.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstFiliaisSelecionadas.Items[i].Selected)
                    {
                        this.lstFiliais.Items.Add(new ListItem(lstFiliaisSelecionadas.Items[i].Text,
                                                             lstFiliaisSelecionadas.Items[i].Value));
                        lstFiliaisSelecionadas.Items.RemoveAt(i);
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

        #region Editar
        /// <summary>
        /// Editar um Horario de verao
        /// </summary>
        /// <param name="pintIdHorarioVerao">Id do horário de verao</param>
        /// <history>
        ///     [cmarchi] created 17/01/2010
        /// </history>
        private void Editar(int pintIdHorarioVerao)
        {
            BLHorarioVerao objBLHorarioVerao = new BLHorarioVerao();
            
            try
            {
                lblMensagem.Visible = false;
                lblMensagem.Text = string.Empty;

                this.BlnEditar = true;

                this.IdHorarioVerao = pintIdHorarioVerao;
                this.gobjHorarioVerao = objBLHorarioVerao.Obter(pintIdHorarioVerao);

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
        ///     Grava o Horário de Verao
        /// </summary>
        /// <history>
        ///     [haguiar_4] created 13/01/2011
        /// </history>
        private void Gravar()
        {
            BLHorarioVerao objBLHorarioVerao = new BLHorarioVerao();
            int intRetorno = -1;
            try
            {
                if (this.ValidarCampos())
                {
                    this.BindCadastro(Enums.TipoTransacao.CarregarDados);

                    if (!BlnEditar)
                    {
                        intRetorno = objBLHorarioVerao.Inserir(gobjHorarioVerao);
                    }
                    else if (this.IdHorarioVerao > 0)
                    {
                        gobjHorarioVerao.IdHorarioVerao = this.IdHorarioVerao;
                        intRetorno = objBLHorarioVerao.Alterar(gobjHorarioVerao);

                        BlnEditar = false;
                        this.IdHorarioVerao = 0;
                    }

                    lblMensagem.Visible = true;

                    if (intRetorno > 0)
                    {
                        this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
                        this.RadAjaxPanel1.Alert("Horário de verão gravado com sucesso.");
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

        #region Grid HorarioVerao Listagem

        #region NeedDataSource
        protected void radGridHorarioVerao_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindHorarioVerao(Enums.TipoBind.SemDataBind);
            }
        }        
        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridHorarioVerao
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        /// </history>        
        protected void radGridHorarioVerao_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Ativar")
            {
                bool blnSituacao = true;

                int intIdHorarioVerao = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                try
                {
                    BLHorarioVerao objBLHorarioVerao = new BLHorarioVerao();

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

                    objBLHorarioVerao.AlterarSituacao(intIdHorarioVerao, blnSituacao);
                    
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
        protected void radGridHorarioVerao_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];
                                
                if (Permissoes.Alteração() && btnEditar != null)
                {
                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_HorarioVerao").ToString();
                    //btnEditar.Visible = true;
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                }


                if (DataBinder.Eval(e.Item.DataItem, "Flg_Situacao") == DBNull.Value)
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                    btnAtivar.ToolTip = "Inativar";
                    btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_HorarioVerao").ToString();
                }
                else
                {

                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Situacao")))
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_HorarioVerao").ToString();
                    }
                    else
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_HorarioVerao").ToString();
                    }
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
        /// </history>
        private void Inicializa()
        {
            lblMensagem.Visible = false;
            lblMensagem.Text = string.Empty;

            try
            {
                //painel de listagem
                this.PopularFilial();
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
        ///     [haguiar_4] created 12/01/2011
        ///     [haguiar_4] modify 14/01/2011
        /// </history>
        protected void InicializaScripts()
        {

            txtDataInicio.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtDataFim.Attributes.Add("OnKeyPress", "return FormataData(event,this);");

            lblMensagem.Visible = false;
            lblMensagem.Text = string.Empty;
        }
        
        #endregion        

        #region Popular Combos

        #region Filial
        /// <summary>
        ///     Popula a lista de filiais
        /// </summary>
        /// <param name="ddlFilial">Filial</param>
        /// <history>
        ///     [haguiar_4] created 17/01/2011
        /// </history>
        /// 
        
        protected void PopularFilial()
        {
            BLFilial objBlFilial = new BLFilial();
            Collection<SafWeb.Model.Filial.Filial> colFilial;

            try
            {
                colFilial = objBlFilial.Listar();
                
                this.lstFiliais.DataSource = colFilial;
                this.lstFiliais.DataTextField = "AliasFilial";
                this.lstFiliais.DataValueField = "IdFilial";
                this.lstFiliais.DataBind();

                this.lstFiliais.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
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

        #region IdHorarioVerao
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdHorarioVerao
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdHorarioVerao
        {
            get
            {
                if ((this.ViewState["vsIdHorarioVerao"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdHorarioVerao"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdHorarioVerao", value);
            }
        }

        #endregion

        #region IdRegional
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdRegional
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

        #endregion

        #region Validações
        
        #region cvrFilialSelecionada_ServerValidate
        /// <summary>
        /// Valida o a lista de horários selecionados na parte de cadastro.
        /// </summary>
        /// <history>
        ///     [cmarchi] Created 5/01/2010
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cvrFilialSelecionada_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (this.lstFiliaisSelecionadas.Items.Count > 0)
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

            /*
            if (this.ddlRegionalCad.SelectedItem == null)
            {
                RadAjaxPanel1.Alert("Selecione uma Regional.");
                blnRetorno = false;
            }
            */

            /*
            if (this.ddlFilialCad.SelectedItem == null)
            {
                RadAjaxPanel1.Alert("Selecione uma Filial.");
                blnRetorno = false;
            }
            */

            /*
            if (this.ddlPeriodicidadeCad.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Periodicidade.");
                blnRetorno = false;
            }
            */
            /*
            if (!string.IsNullOrEmpty(this.txtFilial.Text))
            {
                if (this.txtFilial.Text.Trim().Length == 0)
                {
                    RadAjaxPanel1.Alert("Campo Obrigatório: Filial.");
                    blnRetorno = false;
                }
            }
            else
            {
                RadAjaxPanel1.Alert("Campo Obrigatório: Filial.");
                blnRetorno = false;
            }
            */
            /*
            if (!string.IsNullOrEmpty(this.txtSiglaFilial.Text))
            {
                if (this.txtSiglaFilial.Text.Trim().Length == 0)
                {
                    RadAjaxPanel1.Alert("Campo Obrigatório: Sigla Filial.");
                    blnRetorno = false;
                }
            }
            else
            {
                RadAjaxPanel1.Alert("Campo Obrigatório: Sigla Filial.");
                blnRetorno = false;
            }
            */

            return blnRetorno;
        }

        #endregion

        #endregion
    }
}