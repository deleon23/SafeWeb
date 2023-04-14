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
using SafWeb.BusinessLayer.Relatorio;
using System.Collections.ObjectModel;
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Idioma;
using FrameWork.BusinessLayer.Idioma;
using SafWeb.BusinessLayer.Area;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Empresa;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Veiculo;

namespace SafWeb.UI.Modulos.Relatorio
{
    public partial class RelEntrada : FWPage
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
            this.PopularEmpresa();
            this.PopularRegional();
            this.PopularTipoVisitante();
            this.PopularEstado();

            txtDataInicio.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtDataFim.Attributes.Add("OnKeyPress", "return FormataData(event,this);");

            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlArea, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlPlaca, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            DataTable dtt = new DataTable();
            this.radGridRelatorio.DataSource = dtt;
            this.radGridRelatorio.DataBind();
            radGridRelatorio.Enabled = false;
        }

        #endregion

        #region Popular Combos

        /// <summary>
        ///     Popula os combos com as regionais
        /// </summary>
        /// <history>
        ///     [mribeiro] created 23/07/2009 
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

        /// <summary>
        ///     Popula os combos com os tipos de colaboradores
        /// </summary>
        /// <history>
        ///     [mribeiro] created 23/07/2009 
        ///</history>
        protected void PopularTipoVisitante()
        {
            BLColaborador objBlColaborador = new BLColaborador();
            Collection<SafWeb.Model.Colaborador.TipoColaborador> colTipoColaborador;

            try
            {
                colTipoColaborador = objBlColaborador.ListarTipoColaborador();

                ddlTipoVisitante.DataSource = colTipoColaborador;
                ddlTipoVisitante.DataTextField = "DescricaoColaborador";
                ddlTipoVisitante.DataValueField = "IdTipoColaborador";
                ddlTipoVisitante.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoVisitante, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                ddlTipoVisitante.Items.Add(new ListItem("Lista"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com as empresas
        /// </summary>
        /// <history>
        ///     [mribeiro] created 23/07/2009 
        ///</history>
        protected void PopularEmpresa()
        {
            BLEmpresa objBlEmpresa = new BLEmpresa();
            Collection<SafWeb.Model.Empresa.Empresa> colEmpresa;

            try
            {
                colEmpresa = objBlEmpresa.Listar();

                ddlEmpresa.DataSource = colEmpresa;
                ddlEmpresa.DataTextField = "DescricaoEmpresa";
                ddlEmpresa.DataValueField = "IdEmpresa";
                ddlEmpresa.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlEmpresa, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com as filiais da regional selecionada
        /// </summary>
        /// <history>
        ///     [mribeiro] created 23/07/2009
        /// </history>
        protected void PopularFilial()
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
                    ddlArea.SelectedIndex = 0;
                    ddlArea.Enabled = false;
                }

                BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com as areas de uma filial
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
        protected void PopularArea()
        {
            BLArea objBlArea = new BLArea();
            Collection<SafWeb.Model.Area.Area> colArea;

            try
            {
                if (ddlFilial.SelectedIndex > 0)
                {
                    colArea = objBlArea.ListarArea(Convert.ToInt32(ddlFilial.SelectedItem.Value));

                    ddlArea.DataSource = colArea;
                    ddlArea.DataTextField = "Descricao";
                    ddlArea.DataValueField = "Codigo";
                    ddlArea.DataBind();

                    ddlArea.Enabled = true;
                }
                else
                {
                    ddlArea.Enabled = false;
                    ddlArea.SelectedIndex = 0;
                }

                BLUtilitarios.InseriMensagemDropDownList(ref ddlArea, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com as placas cadastradas
        /// </summary>
        /// <history>
        ///     [mribeiro] created 27/07/2009 
        ///</history>
        protected void PopularPlaca()
        {
            BLVeiculo objBlVeiculo = new BLVeiculo();
            Collection<SafWeb.Model.Veiculo.Veiculo> colVeiculo;

            try
            {
                colVeiculo = objBlVeiculo.ListarVeiculo(Convert.ToInt32(ddlEstado.SelectedItem.Value));

                ddlPlaca.DataSource = colVeiculo;
                ddlPlaca.DataTextField = "DescricaoPlaca";
                ddlPlaca.DataValueField = "Codigo";
                ddlPlaca.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlPlaca, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com estados
        /// </summary>
        /// <history>
        ///     [mribeiro] created 27/07/2009 
        ///</history>
        protected void PopularEstado()
        {
            BLVeiculo objBlVeiculo = new BLVeiculo();
            Collection<SafWeb.Model.Veiculo.Estado> colEstado;

            try
            {
                colEstado = objBlVeiculo.ListarEstado();

                ddlEstado.DataSource = colEstado;
                ddlEstado.DataTextField = "DescricaoEstado";
                ddlEstado.DataValueField = "Codigo";
                ddlEstado.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlEstado, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Bind

        private void Bind(Enums.TipoBind pintTipoBind)
        {
            radGridRelatorio.Enabled = true;
            BLRelatorio objBLRelatorio;
            Collection<SafWeb.Model.Relatorio.RelatorioEntradas> colRelatorio;

            try
            {
                objBLRelatorio = new BLRelatorio();
                colRelatorio = new Collection<SafWeb.Model.Relatorio.RelatorioEntradas>();

                DateTime datInicio, datFim;

                datInicio = Convert.ToDateTime(txtDataInicio.Text);
                datFim = Convert.ToDateTime(txtDataFim.Text);

                if (datFim > datInicio.AddMonths(1) || datFim.Year != datInicio.Year)
                {
                    RadAjaxPanel1.Alert("O intervalo entre as datas deve ser menor ou igual a 1 (um) mês.");
                }
                else
                {

                    int intNumSolicitacao = 0;

                    if (txtIdSolicitacao.Text != string.Empty)
                    {
                        intNumSolicitacao = Convert.ToInt32(txtIdSolicitacao.Text);
                    }

                    colRelatorio = objBLRelatorio.ListarEntradas(intNumSolicitacao,
                                                                 (ddlEmpresa.SelectedIndex > 0 ? Convert.ToInt32(ddlEmpresa.SelectedValue) : 0),
                                                                 (ddlArea.SelectedIndex > 0 ? Convert.ToInt32(ddlArea.SelectedValue) : 0),
                                                                 (ddlRegional.SelectedIndex > 0 ? Convert.ToInt32(ddlRegional.SelectedValue) : 0),
                                                                 (ddlFilial.SelectedIndex > 0 ? Convert.ToInt32(ddlFilial.SelectedValue) : 0),
                                                                 (ddlTipoVisitante.SelectedIndex > 0 ? (ddlTipoVisitante.SelectedItem.Text != "Lista" ? Convert.ToInt32(ddlTipoVisitante.SelectedValue) : 0) : 0),
                                                                 datInicio,
                                                                 datFim,
                                                                 txtVisitado.Text,
                                                                 txtVisitante.Text,
                                                                 (ddlTipoVisitante.SelectedItem.Text == "Lista" ? 1 : 0),
                                                                 txtCracha.Text,
                                                                 (ddlEstado.SelectedIndex > 0 ? Convert.ToInt32(ddlEstado.SelectedValue) : 0),
                                                                 (ddlPlaca.SelectedIndex > 0 ? Convert.ToInt32(ddlPlaca.SelectedValue) : 0));

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

        protected void ddlFilial_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularArea();
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstado.SelectedIndex > 0)
            {
                this.PopularPlaca();
                ddlPlaca.Enabled = true;
            }
            else
            {
                ddlPlaca.SelectedIndex = 0;
                ddlPlaca.Enabled = false;
            }
        }

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

        #endregion

        
    }
}
