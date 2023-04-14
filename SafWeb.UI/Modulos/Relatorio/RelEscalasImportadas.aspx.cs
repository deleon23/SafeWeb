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
using SafWeb.BusinessLayer.Regional;
using System.Collections.ObjectModel;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.Model.Idioma;
using FrameWork.Model.Utilitarios;
using FrameWork.Model.Usuarios;
using SafWeb.Model.Escala;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Empresa;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.BusinessLayer.Area;
using SafWeb.BusinessLayer.Relatorio;
using SafWeb.BusinessLayer.Escala;
using SafWeb.Model.Solicitacao;

namespace SafWeb.UI.Modulos.Relatorio
{

    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : RelEscalasImportadas
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    ///     Implementação da classe RelEscalasImportadas
    /// </summary> 
    /// <history> 
    ///     [haguiar SDM 9004] 01/08/2011 18:04 Created
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public partial class RelEscalasImportadas : FWPage
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.InicializaScripts();
            }
        }

        #region Inicializa Scripts
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Método InicializaScripts
        /// </summary> 
        /// <history> 
        ///     tgerevini 14/6/2010 Created 
        ///     [haguiar_2] 14/12/2010 modify
        ///     carregar tipos de solicitaçao
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void InicializaScripts()
        {
            //this.PopularStatus();

            //ddlStatus.SelectedIndex = 2;
            //ddlStatus.Enabled = false;
            
            this.PopularRegional();
            this.PopularEscalaDepartamental();
            
            this.PopularTipoEscala();

             //this.PopularEscalaDepartamental();

            txtDataInicio.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtDataFim.Attributes.Add("OnKeyPress", "return FormataData(event,this);");

            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            DataTable dtt = new DataTable();
            this.radGridRelatorio.DataSource = dtt;
            this.radGridRelatorio.DataBind();
            radGridRelatorio.Enabled = false;
        }

        #endregion

        #region Popular Combos

        #region Regional
        /// <summary>
        ///     Popula os combos com as regionais
        /// </summary>
        /// <history>
        ///     [tgerevini] created 14/6/2010 
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

                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegional, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Filial
        /// <summary>
        ///     Popula os combos com as filiais da regional selecionada
        /// </summary>
        /// <history>
        ///     [tgerevini] created 15/06/2010
        /// </history>
        private void PopularFilial()
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
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region EscalaDepto
        /// <summary>
        ///     Popula o combo com a Escala Departamental
        /// </summary>
        /// <history>
        ///     [tgerevini] created 16/6/2010 
        ///     [haguiar_3] modify 07/01/2011
        ///</history>
        protected void PopularEscalaDepartamental()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<EscalaDepartamental> colEscalaDepartamental;

            try
            {
                //colEscalaDepartamental = objBLEscalaDepartamental.ListarEscalaDepartamental(false);
                //lista todas as escalas
                colEscalaDepartamental = objBLEscalaDepartamental.ListarEscalaDepartamental(true);

                //preenche escala departamental da parte de cadastro
                this.ddlEscalaHistColab.DataSource = colEscalaDepartamental;
                this.ddlEscalaHistColab.DataTextField = "DescricaoEscalaDpto";
                this.ddlEscalaHistColab.DataValueField = "IdEscalaDpto";
                this.ddlEscalaHistColab.DataBind();

                this.ddlEscalaHistColab.Items.Insert(0, BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)));

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        /*
        #region Status
        /// <summary>
        ///     Popula o combo com os status da solicitação
        /// </summary>
        /// <history>
        ///     [tgerevini] created 15/06/2010 
        ///</history>
        protected void PopularStatus()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.Status> colStatus;

            try
            {
                colStatus = objBlSolicitacao.ListarStatus();

                ddlStatus.DataSource = colStatus;
                ddlStatus.DataTextField = "Descricao";
                ddlStatus.DataValueField = "Codigo";
                ddlStatus.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlStatus, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion
        */

        #region Escala Depto

        /// <summary>
        ///     Popula o combo TipoEscala com os tipos de escala
        /// </summary>
        /// <history>
        /// [no history]    
        /// [haguiar_2] modify 14/12/2010
        ///</history>
        private void PopularTipoEscala()
        {
            BLEscala objBLEscala = new BLEscala();
            Collection<TipoSolicitacao> colSolicitacao = null;

            try
            {
                colSolicitacao = objBLEscala.ListarTipoSolicitacao();

                this.ddlEscalaDep.DataTextField = "Descricao";
                this.ddlEscalaDep.DataValueField = "Codigo";

                this.ddlEscalaDep.DataSource = colSolicitacao;
                this.ddlEscalaDep.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlEscalaDep, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #endregion

        #region Bind

        private void Bind(Enums.TipoBind pintTipoBind)
        {
            radGridRelatorio.Enabled = true;
            BLRelatorio objBLRelatorio;
            Collection<SafWeb.Model.Relatorio.RelatorioEscalas> colRelatorio;

            try
            {
                objBLRelatorio = new BLRelatorio();
                colRelatorio = new Collection<SafWeb.Model.Relatorio.RelatorioEscalas>();

                DateTime datInicio, datFim;

                datInicio = Convert.ToDateTime(txtDataInicio.Text);
                datFim = Convert.ToDateTime(txtDataFim.Text);

                if (datFim > datInicio.AddMonths(1) || datFim.Year != datInicio.Year)
                {
                    RadAjaxPanel1.Alert("O intervalo entre as datas deve ser menor ou igual a 1 (um) mês.");
                }
                else
                {

                    colRelatorio = objBLRelatorio.ListarEscalasImportadas(BLAcesso.IdUsuarioLogado(),
                                                                       (this.txtNumSolicitacaoColab.Text != "" ? Convert.ToInt32(this.txtNumSolicitacaoColab.Text) : 0),
                                                                       (this.ddlRegional.SelectedIndex > 0 ? Convert.ToInt32(this.ddlRegional.SelectedValue) : 0),
                                                                       (this.ddlFilial.SelectedIndex > 0 ? Convert.ToInt32(this.ddlFilial.SelectedValue) : 0),
                                                                       (this.ddlEscalaDep.SelectedIndex > 0 ? Convert.ToInt32(this.ddlEscalaDep.SelectedValue) : 0),
                                                                       (this.ddlEscalaHistColab.SelectedIndex > 0 ? Convert.ToInt32(this.ddlEscalaHistColab.SelectedValue) : 0),
                                                                       this.txtSolicitanteHistColab.Text,
                                                                       this.txtColabEscalado.Text,
                                                                       (this.txtDataInicio.Text != "" ? (DateTime?)Convert.ToDateTime(this.txtDataInicio.Text) : null),
                                                                       (this.txtDataFim.Text != "" ? (DateTime?)Convert.ToDateTime(this.txtDataFim.Text) : null));

                    this.radGridRelatorio.DataSource = colRelatorio;

                    if (pintTipoBind == Enums.TipoBind.DataBind)
                    {
                        this.radGridRelatorio.DataBind();
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Eventos

        protected void ddlRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularFilial();
        }
        #endregion

        #region Botões

        /// <summary>
        ///     Exibe na tela os visitantes agendados conforme filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [tgerevini] created 14/6/2010
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Bind(Enums.TipoBind.DataBind);
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

        #endregion

        #region DataGrid

        #region NeedDataSource

        protected void radGridRelatorio_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.Bind(Enums.TipoBind.SemDataBind);
            }
        }

        #endregion

        #region ItemCommand

        protected void radGridRelatorio_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
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
        /*
        #region ItemDataBound

        protected void radGridRelatorio_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                BLEscala objBLEscala = new BLEscala();
                string strNomeUltimoAprov = string.Empty;

                //Obtém o Nome do Ultimo Aprovador
                strNomeUltimoAprov = objBLEscala.ObterUltimoAprovador(Convert.ToInt32(e.Item.Cells[2].Text));

                Label lblUsuarioAprov = (Label)(e.Item.FindControl("lblUsuarioAprov"));
                lblUsuarioAprov.Text = strNomeUltimoAprov;
            }
        }
        #endregion
        */
        #endregion

    }
}