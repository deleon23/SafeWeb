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
using FrameWork.Model.Idioma;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Empresa;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.BusinessLayer.Area;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Relatorio;
using SafWeb.Model.Solicitacao;
using FrameWork.Model.Usuarios;
using FrameWork.BusinessLayer.Usuarios;

namespace SafWeb.UI.Modulos.Relatorio
{
    public partial class RelAcessoContingencia : FWPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.InicializaScripts();
            }
        }

        #region Inicializa Scripts

        protected void InicializaScripts()
        {
            try
            {
                //Obtém Usuário logado
                Usuario gobjUsuario = new Usuario();
                BLAcesso gobjBLAcesso = new BLAcesso(); 
                gobjUsuario = BLAcesso.ObterUsuario();

                PopularAprovadoresContingencia(gobjUsuario.CodigoRegional, gobjUsuario.CodigoFilial);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            txtDataInicio.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtDataFim.Attributes.Add("OnKeyPress", "return FormataData(event,this);");

            DataTable dtt = new DataTable();
            this.radGridRelatorio.DataSource = dtt;
            this.radGridRelatorio.DataBind();

            radGridRelatorio.Enabled = false;
        }

        #endregion

        #region Popular Combos
        /// <summary>
        ///     Popula o combo com os possíveis aprovadores para contingencia
        /// </summary>
        /// <history>
        ///     [haguiar] 08/04/2011 created 14:38
        /// </history>
        protected void PopularAprovadoresContingencia(int pintId_Regional, int pintId_Filial)
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<Aprovador> colAprovador = new Collection<Aprovador>();

            try
            {
                colAprovador = objBlSolicitacao.ListarAprovadoresConting(pintId_Regional, pintId_Filial);

                ddlAprovador_Conting.DataSource = colAprovador;
                ddlAprovador_Conting.DataTextField = "NomeUsuario";
                ddlAprovador_Conting.DataValueField = "IdUsuario";
                ddlAprovador_Conting.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlAprovador_Conting, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Bind
        /// <history>
        ///     [haguiar] no history
        ///     [haguiar] modify 06/04/2011
        ///     verifica Page.IsValid apenas quando nao tem bind
        ///</history>        
        private void Bind(Enums.TipoBind pintTipoBind)
        {
            if (pintTipoBind == Enums.TipoBind.DataBind)
            {
                if (!Page.IsValid)
                    return;
            }

            radGridRelatorio.Enabled = true;

            BLRelatorio objBLRelatorio;
            Collection<SafWeb.Model.Relatorio.RelatorioAcessoContingencia> colRelatorio;

            try
            {
                objBLRelatorio = new BLRelatorio();
                colRelatorio = new Collection<SafWeb.Model.Relatorio.RelatorioAcessoContingencia>();

                DateTime datInicio, datFim;

                datInicio = Convert.ToDateTime(txtDataInicio.Text);
                datFim = Convert.ToDateTime(txtDataFim.Text);

                if (datFim > datInicio.AddMonths(1) || datFim.Year != datInicio.Year)
                {
                    RadAjaxPanel1.Alert("O intervalo entre as datas deve ser menor ou igual a 1 (um) mês.");
                }
                else
                {


                    colRelatorio = objBLRelatorio.ListarAcessoContingencia(this.txtNomeColaborador_Conting.Text,
                                                                            (this.ddlAprovador_Conting.SelectedIndex > 0 ? Convert.ToInt32(ddlAprovador_Conting.SelectedValue) : 0),
                                                                            datInicio,
                                                                            datFim);

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

        #region cpvDataFim_ServerValidate
        /// <summary>
        /// Valida os campos de data início e fim do período
        /// </summary>
        /// <history>
        ///     [haguiar] Created 18/03/2011
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cpvDataFim_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (!Page.IsValid)
                return;

            DateTime dt;

            if (this.txtDataInicio.Text.Trim().Length == 10)
            {
                this.txtDataInicio.Text += " 00:00";
            }

            if (this.txtDataFim.Text.Trim().Length == 10)
            {
                this.txtDataFim.Text += " 23:59";
            }

            if (!DateTime.TryParse(this.txtDataInicio.Text.ToString(), out dt))
                e.IsValid = false;

            if (!DateTime.TryParse(this.txtDataFim.Text.ToString(), out dt))
                e.IsValid = false;

            if (!e.IsValid)
                return;

            try
            {
                if (DateTime.Compare(Convert.ToDateTime(this.txtDataInicio.Text), Convert.ToDateTime(this.txtDataFim.Text)) < 0)
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                }
            }
            catch (Exception ex)
            {
                e.IsValid = false;
                return;
            }
            
        }
        #endregion


        #endregion

        #region Botões

        /// <summary>
        ///     Exibe na tela os visitantes agendados conforme filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 27/07/2009
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
        ///     [mribeiro] created 27/07/2009
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
        ///     [mribeiro] created 27/07/2009
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

        /// <summary>
        ///     radGridRelatorio
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        /// </history>
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

        #endregion
    }
}
