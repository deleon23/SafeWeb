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
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Area;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Empresa;
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Usuarios;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.BusinessLayer.Colaborador;
using Telerik.WebControls;
using System.Collections.Generic;
using SafWeb.BusinessLayer.Escala;
using SafWeb.Model.Escala;
using System.Text;
using SafWeb.BusinessLayer.Relatorio;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class ImportarEscalaCrew : FWPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InicializaScripts();

            if (!Page.IsPostBack)
            {
                DataTable dtt = new DataTable();
                try
                {
                    //seleciona a regional e a filial do usuário logado.
                    BLColaborador objBlColaborador = new BLColaborador();
                    
                    dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                    this.IdRegional = Convert.ToInt32(dtt.Rows[0][0].ToString());
                    this.IdFilial = Convert.ToInt32(dtt.Rows[0][1].ToString());

                }
                catch (Exception ex)
                {
                    ex = null;
                }


                this.PopularCombos();
                /*
                if (Painel == )
                {
                    this.ControlaPaineis(TipoAba.HistoricoColb);
                }
                else
                {*/
                this.ControlaPaineis(Painel);
                //}

                this.VerificaPermissoes();

                dtt = new DataTable();
                this.radGridRelatorio.DataSource = dtt;
                this.radGridRelatorio.DataBind();
                radGridRelatorio.Enabled = false;

                //seleciona as checkbox que já estavam selecionadas
                if (Session["lstCheckEsc"] != null)
                {
                    this.ListaCheckEscalas = (List<int>)Session["lstCheckEsc"];
                    this.ListaCheckEscalasHr = (List<int>)Session["lstCheckEscHr"];

                    //seleciona as escalações
                    foreach (Telerik.WebControls.GridDataItem rdiPendentes in radEscalaImportar.Items)
                    {
                        try
                        {
                            if (rdiPendentes["TemplateColumn"].FindControl("chkeItem") != null)
                                for (int i = 0; i < this.ListaCheckEscalas.Count; i++)
                                {
                                    if (Convert.ToInt32(rdiPendentes["IdEscalacao"].Text) == this.ListaCheckEscalas[i])
                                    {
                                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkeItem")).Checked = true;
                                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Enabled = true;
                                    }
                                }

                            if (rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra") != null)
                                for (int i = 0; i < this.ListaCheckEscalasHr.Count; i++)
                                {
                                    if (Convert.ToInt32(rdiPendentes["IdEscalacao"].Text) == this.ListaCheckEscalasHr[i])
                                    {
                                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Checked = true;
                                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Enabled = true;
                                    }
                                }

                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }

                    Session["lstCheckEsc"] = null;
                    this.ListaCheckEscalas = null;

                    Session["lstCheckEscHr"] = null;
                    this.ListaCheckEscalasHr = null;

                }

                //verifica importacao
                if (!string.IsNullOrEmpty(this.Request.QueryString["mod"]) && !blnMensagem)
                {
                    blnMensagem = true;

                    string[] arrParametros = BLEncriptacao.DecQueryStr(this.Request.QueryString["mod"]).Split(',');

                    string strEscalacao = string.Empty;

                    strEscalacao = "Escala n°: " + arrParametros[0] +
                        " - Usuário Solicitante: " + arrParametros[1];

                    this.RadAjaxPanel1.Alert(strEscalacao);
                }
            }

        }

        #region Inicializar Scripts

        private void InicializaScripts()
        {

            this.txtDataInicio.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            this.txtDataFim.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            this.txtNumSolicitacaoColab.Attributes.Add("OnKeyPress", "return mascara_Numero(this, event.keyCode);");

            this.btnHelp.Attributes.Add("OnClick", "AbrirHelp();");
        }

        #endregion

        #region Controla Paineis

        public enum TipoAba
        {
            Pendentes = 0,
            HistoricoColb = 2,
        }

        /// <history>
        ///     [no history]
        ///     [haguiar] 17/03/2011 16:22
        ///     popular apenas dados da aba pertinente
        /// </history>
        private void ControlaPaineis(TipoAba pintPainel)
        {
            Painel = pintPainel;
            
            if (pintPainel == TipoAba.Pendentes)
            {
                this.PopularRadGridEscalaImportar(true);

                divAbaImportar.Visible = true;
                divAbaHistorico.Visible = false;

                btnAbaHistorico.CssClass = "cadbuttonAbaInativa";
                btnAbaImportar.CssClass = "cadbuttonAbaAtiva";

                imgAbaImportar.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaHistorico.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
            }
            else
            {
                this.PopularRadGridHistoricoColab(true);

                divAbaImportar.Visible = false;
                divAbaHistorico.Visible = true;

                btnAbaHistorico.CssClass = "cadbuttonAbaAtiva";
                btnAbaImportar.CssClass = "cadbuttonAbaInativa";

                imgAbaImportar.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaHistorico.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
            }
        }

        #endregion

        #region Verificar Permissõe

        private void VerificaPermissoes()
        {
            this.btnImportar.Enabled = Permissoes.Inclusão();
        }

        #endregion

        #region Pendentes

        #region ViewState

        #region dtEscalacao
        /// <summary>
        ///     Propriedade dtEscalacao utilizada para a data da escalacao
        /// </summary>
        /// <history>
        ///     [haguiar] created 12/09/2011 09:14
        /// </history>
        private DateTime dtEscalacao
        {
            get
            {
                if (this.ViewState["vsdtEscalacao"] == null)
                {
                    this.ViewState.Add("vsdtEscalacao", DateTime.Now);
                }

                return Convert.ToDateTime(this.ViewState["vsdtEscalacao"]);
            }

            set
            {
                this.ViewState.Add("vsdtEscalacao", value);
            }
        }
        #endregion
        #region IdEscalacao
        /// <summary>
        ///     Propriedade Id_Escalacao utilizada para a Escalacao
        /// </summary>
        /// <history>
        ///     [haguiar] created 12/09/2011 08:51
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

        #region IdTipoSolicitacao
        /// <summary>
        ///     Propriedade IdTipoSolicitacao utilizada o tipo da solicitacao
        /// </summary>
        /// <history>
        ///     [haguiar] created 23/03/2012 19:09
        /// </history>
        private int IdTipoSolicitacao
        {
            get
            {
                if (this.ViewState["vsIdTipoSolicitacao"] == null)
                {
                    this.ViewState.Add("vsIdTipoSolicitacao", 0);
                }

                return Convert.ToInt32(this.ViewState["vsIdTipoSolicitacao"]);
            }

            set
            {
                this.ViewState.Add("vsIdTipoSolicitacao", value);
            }
        }
        #endregion

        #region blnFeriado
        /// <summary>
        ///     Propriedade blnFeriado utilizada para feriado
        /// </summary>
        /// <history>
        ///     [haguiar] created 19/10/2011 11:07
        /// </history>
        private bool blnFeriado
        {
            get
            {
                if (this.ViewState["vsblnFeriado"] == null)
                {
                    this.ViewState.Add("vsblnFeriado", 0);
                }

                return Convert.ToBoolean(this.ViewState["vsblnFeriado"]);
            }

            set
            {
                this.ViewState.Add("vsblnFeriado", value);
            }
        }
        #endregion

        public TipoAba Painel
        {
            get
            {
                if (ViewState["vsPainel"] == null)
                {
                    ViewState["vsPainel"] = TipoAba.Pendentes;
                }
                return (TipoAba)ViewState["vsPainel"];
            }
            set
            {
                ViewState["vsPainel"] = value;
            }
        }

        public List<int> lstCheck
        {
            get
            {
                if (ViewState["vsList"] == null)
                {
                    ViewState["vsList"] = new List<int>();
                }
                return (List<int>)ViewState["vsList"];
            }
            set
            {
                ViewState["vsList"] = value;
            }
        }

        public List<int> lstCheckHr
        {
            get
            {
                if (ViewState["vsListHr"] == null)
                {
                    ViewState["vsListHr"] = new List<int>();
                }
                return (List<int>)ViewState["vsListHr"];
            }
            set
            {
                ViewState["vsListHr"] = value;
            }
        }
        #endregion

        #region Métodos

        #endregion

        #region Eventos

        #region Botões

        #region Voltar
        /// <summary>
        /// Botão de voltar da divergencia de horários.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] created 12/09/2011 10:19
        /// </history>
        protected void btnVoltarDiv_Click(object sender, EventArgs e)
        {
            this.divAbaImportar.Visible = true;
            this.divDivergenciaHorarios.Visible = false;

            this.IdEscalacao = 0;

            DataTable dtt = new DataTable();
            this.radGridHorariosColaboradores.DataSource = dtt;
            this.radGridHorariosColaboradores.DataBind();
        }
        #endregion

        /// <history>
        ///     [haguiar_sdm9004] 31/08/2011 10:55
        /// </history>
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            BLEscala objBLEscala = new BLEscala();

            bool blnImportaEscala = false;
            int intId_TipoSolicitacao = 0;

            StringBuilder strIdEscalacao = new StringBuilder();
            DateTime datInicio = new DateTime();
            DateTime datFim = new DateTime();

            try
            {
                bool blnFlg_horaExtra = false;

                //Aprova as escalas
                foreach (Telerik.WebControls.GridDataItem rdiPendente in radEscalaImportar.Items)
                {
                    if (((CheckBox)rdiPendente.Cells[0].FindControl("chkeItem")).Checked)
                    {
                        strIdEscalacao.Append(rdiPendente.Cells[9].Text + ",");
                        datInicio = Convert.ToDateTime(rdiPendente.Cells[3].Text.Substring(6, 10));
                        this.dtEscalacao = datInicio;

                        intId_TipoSolicitacao = Convert.ToInt32(rdiPendente.Cells[5].Text);
                        this.IdTipoSolicitacao = intId_TipoSolicitacao;
 
                        if (intId_TipoSolicitacao.Equals(7))
                        {
                            datFim = Convert.ToDateTime(rdiPendente.Cells[3].Text.Substring(19, 10));                            
                        }

                        blnFlg_horaExtra = ((CheckBox)rdiPendente.Cells[4].FindControl("chkHoraExtra")).Checked;
                    }
                }

                if (strIdEscalacao.Length > 0)
                {
                    strIdEscalacao.Remove(strIdEscalacao.Length - 1, 1);

                    this.IdEscalacao = Convert.ToInt32(strIdEscalacao.ToString());

                    if (VerificaDuplicidade())
                        return;

                    int intRetorno = objBLEscala.ImportarEscalacaoCREW(Convert.ToInt32(BLAcesso.IdUsuarioLogado()), intId_TipoSolicitacao,  this.IdFilial, datInicio, datFim, blnFlg_horaExtra);

                    if (intRetorno > 0)
                    {
                    blnImportaEscala = true;
                }
                    else
                    {
                        this.lblMensagem.Visible = true;
                        this.lblMensagem.Text = "Não existem dados para o período selecionado!";
                        this.lblMensagem.ForeColor = System.Drawing.Color.FromName("#154E7A");

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

                if (blnImportaEscala)
                {
                    this.lblMensagem.ForeColor = System.Drawing.Color.Empty;
                    this.lblMensagem.Visible = true;
                    this.lblMensagem.Text = "Importação realizada com sucesso!";
                    this.lblMensagem.ForeColor = System.Drawing.Color.Red;

                    Session["lstCheckEsc"] = null;
                    this.ListaCheckEscalas = null;

                    Session["lstCheckEscHr"] = null;
                    this.ListaCheckEscalasHr = null;

                    this.PopularRadGridEscalaImportar(true);

                    this.RadAjaxPanel1.Alert("Importação realizada com sucesso!");
                }
                else
                {
                    this.lblMensagem.Visible = true;
                    this.lblMensagem.Text = "Por favor, selecione uma ou mais escalas ou solicitações!";
                    this.lblMensagem.ForeColor = System.Drawing.Color.FromName("#154E7A");
                }
            }

        protected void btnAbaImportar_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(TipoAba.Pendentes);
        }

        protected void btnAbaHistorico_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(TipoAba.HistoricoColb);
        }
        
        #endregion
        
        #region CheckBox

        protected void chkHoraExtra_CheckedChanged(object sender, EventArgs e)
        {
            blnFeriado = true;

            //seleciona apenas um item
            foreach (Telerik.WebControls.GridDataItem rdiPendentes in radEscalaImportar.Items)
            {
                try
                {
                    if (rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra") != null)
                    {
                        if (((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Text == ((CheckBox)sender).Text)
                            ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Checked = ((CheckBox)sender).Checked;
                        else
                            ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Checked = false;
                    }

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }


        protected void chkItem_CheckedChanged(object sender, EventArgs e)
        {
            //seleciona apenas um item
            foreach (Telerik.WebControls.GridDataItem rdiPendentes in radEscalaImportar.Items)
            {
                try
                {
                    if (rdiPendentes["TemplateColumn"].FindControl("chkeItem") != null)
                    {
                        if (((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkeItem")).Text == ((CheckBox)sender).Text)
                        {
                            ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkeItem")).Checked = ((CheckBox)sender).Checked;
                            ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Enabled = ((CheckBox)sender).Checked;
                            ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Checked = false;
                        }
                        else
                        {
                            ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkeItem")).Checked = false;
                            ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Enabled = false;
                            ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Checked = false;
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

        #region Escala

        #region Eventos

        #region CheckBox Escala
        /// <summary>
        /// Checa os check box selecionar todos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 27/1/2010
        /// </history>
        protected void chkItemEscala_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Telerik.WebControls.GridDataItem rdiPendentes in radEscalaImportar.Items)
            {
                try
                {
                    if (rdiPendentes["TemplateColumn"].FindControl("chkeItem") != null)
                        ((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkeItem")).Checked = ((CheckBox)sender).Checked;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        #endregion

        #region RadGrid

        /// <summary>
        /// radGridEscalas_ItemCommand
        /// </summary>
        /// <history>
        ///     [haguiar_2] modify 07/12/2010
        ///     permitir visualizacao de troca de horario
        /// </history>
        protected void radEscalaImportar_ItemCommand(object source, GridCommandEventArgs e)
        {
            bool blnFlg_HoraExtra = false;
            try
            {
                if (e.CommandName.Trim() == "Visualizar")
                {

                    DateTime datInicio;
                    string datFim = string.Empty;
                    int intId_TipoSolicitacao = 0;

                    //armazena o codigo das solicitações que foram selecionadas.
                    foreach (Telerik.WebControls.GridDataItem rdiPendentes in radEscalaImportar.Items)
                    {
                        try
                        {
                            if (rdiPendentes["TemplateColumn"].FindControl("chkeItem") != null)
                                if (((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkeItem")).Checked)
                                {
                                    this.ListaCheckEscalas.Add(Convert.ToInt32(rdiPendentes["IdEscalacao"].Text));
                                }

                            if (((CheckBox)rdiPendentes["TemplateColumn"].FindControl("chkHoraExtra")).Checked)
                            {
                                this.ListaCheckEscalasHr.Add(Convert.ToInt32(rdiPendentes["IdEscalacao"].Text));
                                blnFlg_HoraExtra = true;
                            }

                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }

                    datInicio = Convert.ToDateTime(e.Item.Cells[3].Text.Substring(6, 10));
                    intId_TipoSolicitacao = Convert.ToInt32(e.Item.Cells[5].Text);
                    if (intId_TipoSolicitacao.Equals(7))
                    {
                        datFim = e.Item.Cells[3].Text.Substring(19, 10);
                    }

                    Session["lstCheckEsc"] = this.ListaCheckEscalas;
                    Session["lstCheckEscHr"] = this.ListaCheckEscalasHr;

                    string strParametros = "CadCaixa," + e.Item.Cells[9].Text + "," + e.Item.Cells[3].Text.Substring(6, 10) + "," + Convert.ToInt32(blnFlg_HoraExtra) + "," + datFim + "," + intId_TipoSolicitacao.ToString() + "," + this.IdFilial.ToString();
                        
                    Response.Redirect("../Escala/ImportarEscalaCrewVisualizar.aspx?mod=" +
                        BLEncriptacao.EncQueryStr(strParametros));
                }

                if (e.CommandName.Trim() == "IrPagina")
                {
                    string strPageIndexString = ((TextBox)e.Item.FindControl("txtPagina")).Text;
                    int intPageIndex = e.Item.OwnerTableView.CurrentPageIndex;
                    if ((!object.ReferenceEquals(strPageIndexString.Trim(), string.Empty)))
                    {
                        if (e.Item.OwnerTableView.PageCount < Convert.ToInt32(strPageIndexString))
                        {
                            strPageIndexString = (e.Item.OwnerTableView.PageCount).ToString();
                            ((TextBox)e.Item.FindControl("txtPagina")).Text = strPageIndexString;
                        }
                        intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
                    }

                    if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                    {
                        e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                        e.Item.OwnerTableView.Rebind();
                    }

                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void radEscalaImportar_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnVisualizar;
                btnVisualizar = (ImageButton)e.Item.FindControl("btnVisualizar");

                BLEscala objBLEscala = new BLEscala();
                string strNomeUltimoAprov = string.Empty;

                if (btnVisualizar != null)
                {
                    btnVisualizar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    // Cursor 
                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 1; @int++)
                    {
                        if (@int != 2 && @int != 4)
                        {
                            e.Item.Cells[@int].Attributes.Add("onclick",
                                Page.ClientScript.GetPostBackEventReference(btnVisualizar, ""));
                        }
                    }

                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                }
            }
        }

        protected void radEscalaImportar_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.PopularRadGridEscalaImportar(false);
            }
        }

        #endregion

        #endregion

        #region Métodos

        #region Verificar duplicidades

        private bool VerificaDuplicidade()
        {
            //verificar divergencia de horários duplicados
            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                new Collection<DataHorarioColaboradores>();

            //lblMensagemDiv_1.Visible = false;
            //lblMensagemDiv_2.Visible = false;

            try
            {
                colDataHorarioColaboradores = this.ObterDataHorariosColaboradores(false);

                if (colDataHorarioColaboradores.Count > 0)
                {
                    this.radGridHorariosColaboradores.DataSource = colDataHorarioColaboradores;
                    this.radGridHorariosColaboradores.DataBind();

                    this.divAbaImportar.Visible = false;
                    this.divDivergenciaHorarios.Visible = true;


                    //lblMensagemDiv_1.Visible = true;
                    //lblMensagemDiv_2.Visible = true;

                    this.RadAjaxPanel1.Alert("Verifique a lista de colaboradores com horário duplicado e tente novamente!");

                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            return false;
        }

        #endregion

        #region Popular RadGridEscalaPendentes
        /// <summary>
        /// Popula a radgrid escalação.
        /// </summary>
        /// <param name="pblnBind">True - Faz o bind, False - não faz o bind</param>
        /// <history>
        ///     [cmarchi] created 26/1/2010
        /// </history>
        private void PopularRadGridEscalaImportar(bool pblnBind)
        {
            BLEscala objBLEscala = new BLEscala();
            Collection<EscalaImportarCrew> colEscalacoes = new Collection<EscalaImportarCrew>();

            try
            {
                colEscalacoes = objBLEscala.ListarEscalasImportarCREW(BLAcesso.IdUsuarioLogado());

                this.radEscalaImportar.DataSource = colEscalacoes;

                if (pblnBind)
                    this.radEscalaImportar.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #endregion

        #region Propriedades
        /// <summary>
        /// Propriedade Lista de checados das Escalas.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 27/1/2010
        /// </history>
        public List<int> ListaCheckEscalas
        {
            get
            {
                if (ViewState["vsListEscala"] == null)
                {
                    ViewState["vsListEscala"] = new List<int>();
                }
                return (List<int>)ViewState["vsListEscala"];
            }
            set
            {
                ViewState["vsListEscala"] = value;
            }
        }

        /// <summary>
        /// Propriedade Lista de checados de hora extra.
        /// </summary>
        /// <history>
        ///     [haguiar] created 19/10/2011 11:14
        /// </history>
        public List<int> ListaCheckEscalasHr
        {
            get
            {
                if (ViewState["vsListEscalaHr"] == null)
                {
                    ViewState["vsListEscalaHr"] = new List<int>();
                }
                return (List<int>)ViewState["vsListEscalaHr"];
            }
            set
            {
                ViewState["vsListEscalaHr"] = value;
            }
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade blnMensagem
        /// </summary> 
        /// <history> 
        ///     [haguiar] 31/08/2011 11:59
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool blnMensagem
        {
            get
            {
                if ((this.ViewState["vsblnMensagem"] != null))
                {
                    return Convert.ToBoolean(this.ViewState["vsblnMensagem"]);
                }
                else
                {
                    return false;
                }
            }
            set
            {
                this.ViewState.Add("vsblnMensagem", value);
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

        #endregion

        #endregion

        #region Historico

        #region Métodos



        private void PopularCombos()
        {
            this.PopularComboTipoSolicitacaoHistColab();
            this.PopularComboRegionalHistColab();
            this.PopularComboFilialHistColab();

            this.PopularComboStatusHistColab();

            this.PopularComboEscalaDepartamental();
        }
        
        #region PopularComboEscalaDepto
        /// <summary>
        ///     Popula o combo com a Escala Departamental crew
        /// </summary>
        /// <history>
        ///     [haguiar] created 25/08/2011 15:57
        ///</history>
        protected void PopularComboEscalaDepartamental()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            //Collection<EscalaDepartamental> colEscalaDepartamental;
            DataTable colEscalaDepartamental;

            try
            {
                colEscalaDepartamental = objBLEscalaDepartamental.ListarEscalaDepartamental(IdRegional,IdFilial,string.Empty,0,false,true);

                //preenche escala departamental da parte de cadastro
                this.ddlEscalaHistColab.DataSource = colEscalaDepartamental;
                this.ddlEscalaHistColab.DataTextField = "Des_EscalaDpto";
                this.ddlEscalaHistColab.DataValueField = "Id_EscalaDpto";
                this.ddlEscalaHistColab.DataBind();

                this.ddlEscalaHistColab.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #endregion

        #region Eventos
        
        #region Combo

        protected void ddlRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularComboFilialHistColab();
        }

        #endregion
        
        #endregion

        #endregion

        #region Histórico Colaboradores

        #region Métodos

        private void PopularComboTipoSolicitacaoHistColab()
        {
            BLSolicitacao objBLSolicitacao = null;
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colTipoSolicitacao = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();

                colTipoSolicitacao = objBLSolicitacao.ListarTipoSolicitacao(true);

                this.ddlEscalaDep.DataSource = colTipoSolicitacao;
                this.ddlEscalaDep.DataTextField = "Descricao";
                this.ddlEscalaDep.DataValueField = "Codigo";
                this.ddlEscalaDep.DataBind();

                this.ddlEscalaDep.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularComboRegionalHistColab()
        {
            BLRegional objBlRegional = null;
            Collection<SafWeb.Model.Regional.Regional> colRegional = null;

            try
            {
                objBlRegional = new BLRegional();

                colRegional = objBlRegional.Listar();

                this.ddlRegional.DataSource = colRegional;
                this.ddlRegional.DataTextField = "DescricaoRegional";
                this.ddlRegional.DataValueField = "IdRegional";
                this.ddlRegional.DataBind();

                this.ddlRegional.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));

                ListItem ltmRegional = this.ddlRegional.Items.FindByValue(
                    Convert.ToString(IdRegional));

                if (ltmRegional != null)
                {
                    ltmRegional.Selected = true;
                }

                ddlRegional.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularComboFilialHistColab()
        {
            BLFilial objBlFilial = null;
            Collection<SafWeb.Model.Filial.Filial> colFilial = null;

            try
            {
                objBlFilial = new BLFilial();

                if (ddlRegional.SelectedIndex > 0)
                {
                    colFilial = objBlFilial.Listar(Convert.ToInt32(ddlRegional.SelectedItem.Value));
                    ddlFilial.Enabled = true;
                }
                else
                {
                    ddlFilial.Enabled = false;
                }

                this.ddlFilial.Items.Clear();

                this.ddlFilial.DataSource = colFilial;
                this.ddlFilial.DataTextField = "AliasFilial";
                this.ddlFilial.DataValueField = "IdFilial";
                this.ddlFilial.DataBind();

                this.ddlFilial.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));

                ListItem ltmFilial = this.ddlFilial.Items.FindByValue(
                    Convert.ToString(IdFilial));

                if (ltmFilial != null)
                {
                    ltmFilial.Selected = true;
                }

                ddlFilial.Enabled = false;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        
        private void PopularComboStatusHistColab()
        {
            BLSolicitacao objBLSolicitacao = null;
            Collection<SafWeb.Model.Solicitacao.Status> colStatus = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();

                colStatus = objBLSolicitacao.ListarStatus();

                this.ddlStatus.DataSource = colStatus;
                this.ddlStatus.DataTextField = "Descricao";
                this.ddlStatus.DataValueField = "Codigo";
                this.ddlStatus.DataBind();

                this.ddlStatus.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopularRadGridHistoricoColab(bool pblnBind)
        {
            if (string.IsNullOrEmpty(txtDataInicio.Text) || string.IsNullOrEmpty(txtDataFim.Text))
                return;

            BLRelatorio objBLRelatorio;
            Collection<SafWeb.Model.Relatorio.RelatorioEscalas> colRelatorio;

            try
            {
                objBLRelatorio = new BLRelatorio();
                colRelatorio = new Collection<SafWeb.Model.Relatorio.RelatorioEscalas>();

                int intNumeroEscala = 0;
                int intEscalaDepartamental = 0;
                int intRegional = 0;
                int intFilial = 0;
                int intStatus = 0;
                int intTipoSolicitacao = 0;

                DateTime? datDataInicio = null;
                DateTime? datDataFinal = null;

                DateTime datIni;
                DateTime datFim;

                try
                {
                    //verifica os parâmetros da consulta
                    if (DateTime.TryParse(this.txtDataInicio.Text.Trim(), out datIni))
                        datDataInicio = datIni;

                    if (DateTime.TryParse(this.txtDataFim.Text.Trim(), out datFim))
                        datDataFinal = datFim;

                    if (!string.IsNullOrEmpty(this.txtNumSolicitacaoColab.Text))
                    {
                        int intResultado;
                        if (Int32.TryParse(this.txtNumSolicitacaoColab.Text.Trim(), out intResultado))
                            intNumeroEscala = intResultado;
                    }

                    if (this.ddlEscalaHistColab.SelectedIndex > 0)
                        intEscalaDepartamental = Convert.ToInt32(
                                             this.ddlEscalaHistColab.SelectedValue);

                    if (this.ddlEscalaDep.SelectedIndex > 0)
                        intTipoSolicitacao = Convert.ToInt32(
                                             this.ddlEscalaDep.SelectedValue);

                    //intRegional = IdRegional;
                   // intFilial = IdFilial;

                    //status nao pode ter o valor 0
                    if (this.ddlStatus.SelectedIndex > 0)
                    {
                        intStatus = Convert.ToInt32(
                                             this.ddlStatus.SelectedValue);
                    }

                    this.radGridRelatorio.DataSource = objBLRelatorio.ListarEscalasImportadas(intNumeroEscala, intEscalaDepartamental,
                        intRegional, intFilial, this.txtSolicitanteHistColab.Text.Trim(),
                        this.txtColabEscalado.Text.Trim(),
                        this.txtAprovadorHistColab.Text.Trim(),
                        intStatus, intTipoSolicitacao, datDataInicio, datDataFinal,
                        Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                    //this.radGridRelatorio.DataSource = colRelatorio;
                    radGridRelatorio.Enabled = true;

                    if (pblnBind)
                    {
                        this.radGridRelatorio.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }

            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #region ObterDataHorariosColaboradores
        /// <summary>
        /// Obtém a datas e horários dos colaboradores  duplicados para importacao CREW.
        /// </summary>
        /// <param name="pblnFiltrar">True - Filtra as datas, False - retorna todas as datas</param>
        /// <returns>Lista de datas e horários dos colaboradores</returns>
        /// <history>
        ///     [haguiar] create 12/09/2011 09:17
        /// </history>
        private Collection<DataHorarioColaboradores> ObterDataHorariosColaboradores(bool pblnFiltrar)
        {
            BLEscala objBLEscala = new BLEscala();

            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                new Collection<DataHorarioColaboradores>();

            try
            {
                Collection<DateTime> colDatas = new Collection<DateTime>();
                colDatas.Add(dtEscalacao);

                colDataHorarioColaboradores = objBLEscala.ObterDtHorColEscalacaoCrew(dtEscalacao, null, colDatas, Convert.ToInt32(BLAcesso.IdUsuarioLogado()), true, this.IdTipoSolicitacao);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            return colDataHorarioColaboradores;
        }

        #endregion
        #endregion

        #region Eventos
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.PopularRadGridHistoricoColab(true);
        }

        /// <summary>
        ///     Exibe no Word os visitantes agendados conforme filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [tgerevini] created 14/6/2010
        /// </history>
        protected void btnExportWord_Click(object sender, EventArgs e)
        {
            this.radGridRelatorio.ExportSettings.IgnorePaging = true;
            this.radGridRelatorio.ExportSettings.ExportOnlyData = true;
            this.radGridRelatorio.MasterTableView.ExportToWord();
            this.radGridRelatorio.ExportSettings.OpenInNewWindow = true;
        }

        /// <summary>
        ///     Exibe no Excel os visitantes agendados conforme filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [tgerevini] created 14/6/2010
        /// </history>
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            this.radGridRelatorio.ExportSettings.IgnorePaging = true;
            this.radGridRelatorio.ExportSettings.ExportOnlyData = true;
            this.radGridRelatorio.MasterTableView.ExportToExcel();
            this.radGridRelatorio.ExportSettings.OpenInNewWindow = true;
        }

        #region RadGrid Importar

        protected void radGridRelatorio_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.PopularRadGridHistoricoColab(false);
            }
        }

        /// <summary>
        /// radGridRelatorio_ItemCommand
        /// </summary>
        /// <history>
        ///     [haguiar_2] modify 07/12/2010
        ///     permitir visualizacao de troca de horario
        /// </history>
        protected void radGridRelatorio_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Trim() == "IrPagina")
                {
                    string strPageIndexString = ((TextBox)e.Item.FindControl("txtPagina")).Text;
                    int intPageIndex = e.Item.OwnerTableView.CurrentPageIndex;
                    if ((!object.ReferenceEquals(strPageIndexString.Trim(), string.Empty)))
                    {
                        if (e.Item.OwnerTableView.PageCount < Convert.ToInt32(strPageIndexString))
                        {
                            strPageIndexString = (e.Item.OwnerTableView.PageCount).ToString();
                            ((TextBox)e.Item.FindControl("txtPagina")).Text = strPageIndexString;
                        }
                        intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
                    }

                    if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                    {
                        e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                        e.Item.OwnerTableView.Rebind();
                    }

                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Grid Horarios Colaboradores

        #region NeedDataSource
        protected void radGridHorariosColaboradores_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.radGridHorariosColaboradores.DataSource = this.ObterDataHorariosColaboradores(false);
            }
        }
        #endregion

        #region ItemDataBound

        /// <summary>
        /// ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] create 12/09/2011 09:20
        /// </history>
        protected void radGridHorariosColaboradores_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                Label lblDataHora = (Label)e.Item.FindControl("lblDataHora");

                string strCompensado = DataBinder.Eval(e.Item.DataItem, "Compensado").ToString();
                string strFolga = DataBinder.Eval(e.Item.DataItem, "Folga").ToString();
                string strLicenca = DataBinder.Eval(e.Item.DataItem, "Licenca").ToString();
                string strHorarioFlex = DataBinder.Eval(e.Item.DataItem, "HorarioFlex").ToString();
                string strHoraExtra = DataBinder.Eval(e.Item.DataItem, "HoraExtra").ToString();

                DateTime dtDataHora = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "DataColaboradores").ToString());

                if (strCompensado != string.Empty)
                {
                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy}", dtDataHora).Substring(0, 10) + " - Compensado";
                }
                else if (strFolga != string.Empty)
                {
                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy}", dtDataHora).Substring(0, 10) + " - Folga/DSR";
                }
                else if (strLicenca != string.Empty)
                {
                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy}", dtDataHora).Substring(0, 10) + " - Férias/Licença";
                }
                else if (strHorarioFlex != string.Empty)
                {
                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy}", dtDataHora).Substring(0, 10) + " - 08 às 09 flex";
                }
                else if (strHoraExtra != string.Empty)
                {
                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy HH:mm}", dtDataHora) + " - Hora Extra";
                }
                else
                {
                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy HH:mm}", dtDataHora);
                }

            }
        }
        #endregion
        #endregion

        #endregion

        #endregion
    }
}
