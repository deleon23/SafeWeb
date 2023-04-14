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
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.Model.Colaborador;
using FrameWork.Model.Utilitarios;
using Telerik.WebControls;
using System.Text;
using SafWeb.BusinessLayer.Escala;
using SafWeb.BusinessLayer.Filial;
using SafWeb.Model.Filial;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
//using SafWeb.BusinessLayer.Escala;
using SafWeb.Model.Escala;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class ListColaboradoresEscala : FWPage
    {
        /// <summary>
        ///     Page Load
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modify 05/11/2010
        /// </history>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.VerificarParametro();

            if (!this.Page.IsPostBack)
            {
                this.InicializaScripts();
            }
            this.Bind(Enums.TipoBind.DataBind, true);
            
            /*
            [haguiar]
            
            this.VerificarParametro();

            if (!this.Page.IsPostBack)
            {
                this.InicializaScripts();

                this.Bind(Enums.TipoBind.DataBind, true);
            }
            else
            {
                this.Bind(Enums.TipoBind.SemDataBind, true);
            }
            */
        }

        #region Bind

        /// <summary>
        ///     Bind
        /// </summary>
        /// <param name="penmTipoBind">Tipo Bind</param>
        /// <param name="pblnBuscar">True - Faz a busca de colaborador, False Lista todos</param>
        /// <history>
        ///     [cmarchi] created 6/1/2010
        ///     [cmarchi] modify 8/2/2010
        ///     [haguiar] modify 14/03/2011
        ///     nao mostrar labels de aviso
        /// </history>
        private void Bind(Enums.TipoBind penmTipoBind, bool pblnBuscar)
        {

            this.btnIncluir.Visible = false;
            this.lblMensagem.Visible = false;
            this.lblMensagem_2.Visible = false;


            BLColaborador objBLColaborador = new BLColaborador();
            BLEscala objBLEscala = new BLEscala();

            Collection<Colaborador> colColaborador = null;

            string strColaborador = string.Empty;
            string strDocumento = string.Empty;
            int intIdFilial = 0;

            if (pblnBuscar)
            {
                strColaborador = this.txtNome.Text.Trim();
                strDocumento = this.txtDocumento.Text.Trim();

                if (this.ddlFilial.SelectedIndex > 0)
                    Int32.TryParse(this.ddlFilial.SelectedValue, out intIdFilial);
            }

            try
            {
                if (this.IdEscalacao <= 0)
                {
                    colColaborador = objBLColaborador.ListarColaboradorFuncionarioTerceiro(strColaborador, strDocumento,
                            intIdFilial);
                }
                else
                {
                    //this.btnSelecionar.Visible = false;
                    this.btnBuscar.Visible = false;
                    this.ddlFilial.Visible = false;
                    this.txtDocumento.Visible = false;
                    this.txtNome.Visible = false;

                    this.lblFilialList.Visible = false;
                    this.lblDocumento.Visible = false;
                    this.lblNome.Visible = false;

                    colColaborador = objBLEscala.ObterColaboradores(this.IdEscalacao);
                }

                this.radGridColaboradores.DataSource = colColaborador;

                if (penmTipoBind == Enums.TipoBind.DataBind)
                    this.radGridColaboradores.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Botão

        #region AdicionaColaborador
        /// <summary>
        ///     Remove colaborador da escala atual e adiciona na escala editada.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] created 14/03/2011
        /// </history>
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            //Permite a inserção do colaborador em uma escala
            ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('" +
            BLEncriptacao.EncQueryStr(this.IdColaborador.ToString()) + "');</script>");

            ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
        }
        #endregion

        #region Buscar
        /// <summary>
        ///     Filtra os colaboradores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 18/12/2009
        ///     [cmarchi] modify 6/1/2010
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Bind(Enums.TipoBind.DataBind, true);
        }
        #endregion

        #region Fechar
        /// <summary>
        ///    Fechar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 11/2/2010
        /// </history>
        protected void btnFechar_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
        }
        #endregion

        #endregion

        #region DataGrid

        protected void radGridColaboradores_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.Bind(Enums.TipoBind.SemDataBind, true);
            }
        }

        /// <summary>
        ///     Seleciona o colaborador para inserir na escala departamental
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modify 14/03/2011
        ///     retornar nome da escala departamental para o usuário, habilita botão 'continuar'
        /// </history>
        protected void radGridColaboradores_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {

            if (e.CommandName.Trim() == "Editar")
            {

                if (this.IdEscalacao <= 0)
                {
                    BLEscala objBLEscala = new BLEscala();
                    int intEscala = objBLEscala.ObterEscalaColaborador(Convert.ToInt32(e.CommandArgument));
                   
                    //verificar se o colaborador já está em uma escala
                    if (intEscala > 0)
                    {
                        //Escalacao objEscalacao = objBLEscala.Obter(intEscala);

                        BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
                        EscalaDepartamental objEscalaDepartamental = objBLEscalaDepartamental.Obter(intEscala, false, null);

                        //Já está em uma escala
                        //lblMensagem.Text = "Não é possível inserir colaborador na lista. Ele já está na escala '" + objEscalaDepartamental.DescricaoEscalaDpto + "'.";

                        lblMensagem.Text   = "Colaborador já cadastrado na escala '" + objEscalaDepartamental.DescricaoEscalaDpto + "'.";
                        lblMensagem_2.Text = "Clique no botão 'Continuar' para removê-lo da escala atual e adicioná-lo na escala editada."; //intEscala.ToString();

                        this.btnIncluir.Visible = true;
                        this.lblMensagem.Visible = true;
                        this.lblMensagem_2.Visible = true;

                        IdColaborador = Convert.ToInt32(e.CommandArgument);
                    }
                    else 
                    {
                        this.btnIncluir.Visible = false;
                        this.lblMensagem.Visible = false;
                        this.lblMensagem_2.Visible = false;

                        //Permite a inserção do colaborador em uma escala
                        ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('" +
                        BLEncriptacao.EncQueryStr(e.CommandArgument.ToString().Trim()) + "');</script>");

                        ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
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

            */
        }

        protected void radGridColaboradores_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
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

                e.Item.ToolTip = "Selecione";

            }
        }

        #endregion

        #region IdEscalacao
        /// <summary>
        ///     Propriedade Id_Escalacao
        /// </summary>
        /// <history>
        ///     [cmarchi] created 27/1/2010
        /// </history>
        private int IdEscalacao
        {
            get
            {
                if (this.ViewState["vsIdEscalacao"] == null)
                {
                    this.ViewState.Add("vsIdEscalacao", 0);
                }

                return Convert.ToInt32(this.ViewState["vsIdEscalacao"]);
            }

            set
            {
                this.ViewState.Add("vsIdEscalacao", value);
            }
        }
        #endregion

        #region Inicializa Scripts
        /// <summary>
        /// Inicializa os scripts 
        /// </summary>        
        /// <history>
        ///     [cmarchi] created 6/1/2010
        ///     [cmarchi] modify 11/2/2010
        /// </history>
        protected void InicializaScripts()
        {
            BLFilial objBLFilial = new BLFilial();
            Collection<Filial> colFiliais = new Collection<Filial>();

            this.txtNome.Text = string.Empty;
            this.txtDocumento.Text = string.Empty;

            txtDocumento.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NUMERO;
            txtDocumento.MaxLength = 12;

            try
            {
                colFiliais = objBLFilial.Listar();

                this.ddlFilial.Items.Clear();

                this.ddlFilial.DataTextField = "AliasFilial";
                this.ddlFilial.DataValueField = "IdFilial";

                this.ddlFilial.DataSource = colFiliais;
                this.ddlFilial.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            this.Bind(Enums.TipoBind.DataBind, false);
        }
        #endregion

        #region VerificarParametro
        /// <summary>
        /// Verifica o valor da query string.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 6/1/2010
        ///     [cmarchi] modify 27/01/2010
        /// </history>
        private void VerificarParametro()
        {
            string strQuery = this.Request.QueryString["open"];

            if (!string.IsNullOrEmpty(strQuery))
            {
                string strParametro = BLEncriptacao.DecQueryStr(strQuery);

                if (strParametro != "sim")
                {
                    int intIdEscalacao;

                    string[] arrParametros = strParametro.Split('#');

                    if (arrParametros[0] == "CadCaixa" && Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;
                    }
                    else
                        ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
            }
        }
        #endregion

        #region Property
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdColaborador do colaborador selecionado
        /// </summary> 
        /// <history> 
        ///     [haguiar] 14/03/2011 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdColaborador
        {
            get
            {
                if ((this.ViewState["vsIdColaborador"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdColaborador"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdColaborador", value);
            }
        }
        #endregion
    }
}
