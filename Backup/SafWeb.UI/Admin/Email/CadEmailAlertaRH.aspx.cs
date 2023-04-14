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

using SafWeb.Model.Filial;
using SafWeb.Model.Regional;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.Model.Colaborador;

using SafWeb.Model.Area;
using SafWeb.BusinessLayer.Area;
using SafWeb.BusinessLayer.Email;

namespace SafWeb.UI.Modulos.Email
{
    public partial class CadEmailAlertaRH : FWPage
    {
        //private Filial gobjFilial;
        //private Collection<SafWeb.Model.Area.Area> objcolArea;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.InicializaScripts();
                this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
            }
        }
                
        #region Bind

        #region BindAlerta
        /// <summary>
        /// Bind Alerta
        /// </summary>
        ///<param name="penmTipoBind">Tipo de Bind</param>
        /// <history>
        ///     [haguiar] create 21/11/2011 17:33
        /// </history>
        private void BindAlerta(Enums.TipoBind penmTipoBind)
        {
            BLEmail objBLEmail = new BLEmail();

            this.IdRegraEmailAlertaRH = 0;
            this.BlnEditar = false;

            try
            {
                radGridFilial.DataSource = objBLEmail.ListarAlertaRH();

                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    radGridFilial.DataBind();
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
                //this.gobjFilial = new Filial();
            }
            //atribuir as informações dos objetos para tela
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                BLEmail objBLEmail = new BLEmail();

                DataTable dtt;
                dtt = objBLEmail.ObterAlertaRH(this.IdRegraEmailAlertaRH);

                if (dtt.Rows.Count >= 0)
                {
                    this.txtNomeColaborador.Text = dtt.Rows[0][1].ToString();
                    this.txtEmailColaborador.Text = dtt.Rows[0][2].ToString();
                }

                dtt.Clear();
            }
            //inicializa as informações na tela
            else if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.IdRegraEmailAlertaRH = 0;
                this.txtEmailColaborador.Text = string.Empty;
                this.txtNomeColaborador.Text = string.Empty;

                this.InicializaScripts();
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
            this.BindAlerta(Enums.TipoBind.DataBind);
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
        /// <param name="pintIdRegraEmailAlertaRH">Id do cadastro de alerta</param>
        /// <history>
        ///     [haguiar] created 21/11/2011 16:06
        /// </history>
        private void Editar(int pintIdRegraEmailAlertaRH)
        {
            BLEmail objBLEmail = new BLEmail();

            try
            {
                lblMensagem.Visible = false;
                lblMensagem.Text = string.Empty;

                this.BlnEditar = true;

                this.IdRegraEmailAlertaRH = pintIdRegraEmailAlertaRH;
                //this.gobjFilial = objBLEmail.ObterAlertaRH(pintIdRegraEmailAlertaRH);

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
            BLEmail objBLEmail = new BLEmail();
            int intRetorno = -1;

            try
            {                
                //this.BindCadastro(Enums.TipoTransacao.CarregarDados);

                if (!BlnEditar)
                {
                    intRetorno = objBLEmail.InserirEmailAlertaRH(this.txtNomeColaborador.Text,this.txtEmailColaborador.Text);
                }
                else if (this.IdRegraEmailAlertaRH > 0)
                {
                    intRetorno = objBLEmail.AlterarEmailAlertaRH(this.IdRegraEmailAlertaRH, this.txtNomeColaborador.Text, this.txtEmailColaborador.Text);

                    BlnEditar = false;
                    this.IdRegraEmailAlertaRH = 0;
                }

                lblMensagem.Visible = true;

                if (intRetorno > 0)
                {
                    this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
                    this.RadAjaxPanel1.Alert("Cadastro efetuado com sucesso.");
                }
                else
                {
                    lblMensagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR_ERRO));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion
        
        #region Grid Filial Listagem

        #region NeedDataSource
        protected void radGridFilial_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                //this.BindFilial(Enums.TipoBind.SemDataBind);
            }
        }        
        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridFilial
        /// </summary>
        /// <history>
        ///     [haguiar] created 21/11/2011 17:15
        /// </history>        
        protected void radGridFilial_ItemCommand(object source, GridCommandEventArgs e)
        {
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
            
            if (e.CommandName.Trim() == "Excluir")
            {
                if (e.CommandArgument.ToString().Trim() != string.Empty)
                {
                    if (Permissoes.Exclusão())
                    {
                        BLEmail objBLEmail = new BLEmail();

                        objBLEmail.ExcluirEmailAlertaRH(Convert.ToInt32(e.CommandArgument.ToString().Trim()));

                        this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
                        this.RadAjaxPanel1.Alert("Cadastro excluído com sucesso.");
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
        protected void radGridFilial_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");

                //Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                //ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];


                if (Permissoes.Alteração() && btnEditar != null)
                {
                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_RegraEmailAlertaRH").ToString();
                    //btnEditar.Visible = true;
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
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

        #region IdRegraEmailAlertaRH
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade Id_RegraEmailAlertaRH
        /// </summary> 
        /// <history> 
        ///     [haguiar] 21/11/211 16:03
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdRegraEmailAlertaRH
        {
            get
            {
                if ((this.ViewState["vsIdRegraEmailAlertaRH"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdRegraEmailAlertaRH"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdRegraEmailAlertaRH", value);
            }
        }

        #endregion

        #endregion
    }
}