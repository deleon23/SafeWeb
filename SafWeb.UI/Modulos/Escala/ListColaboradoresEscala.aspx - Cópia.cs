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

namespace SafWeb.UI.Modulos.Escala
{
    public partial class ListColaboradoresEscala : FWPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.VerificarParametro();

            if (!this.Page.IsPostBack)
            {
                this.InicializaScripts();
            }
            this.Bind(Enums.TipoBind.DataBind, true);
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
        /// </history>
        private void Bind(Enums.TipoBind penmTipoBind, bool pblnBuscar)
        {
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

        protected void radGridColaboradores_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {

            if (e.CommandName.Trim() == "Editar")
            {

                if (this.IdEscalacao <= 0)
                {
                    ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('" +
                    BLEncriptacao.EncQueryStr(e.CommandArgument.ToString().Trim()) + "');</script>");
                }

                ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
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
    }
}
