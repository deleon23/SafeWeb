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
using Telerik.WebControls;
using System.Collections.ObjectModel;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Escala;
using FrameWork.BusinessLayer.Utilitarios;
using SafWeb.Model.Escala;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using System.Text;
using System.IO;
using SafWeb.Model.Solicitacao;
using SafWeb.BusinessLayer.Solicitacao;
using FrameWork.BusinessLayer.Usuarios;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.Model.Colaborador;
using FrameWork.Model.Usuarios;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class ImportarEscalaRondaVisualizar : FWPage
    {
        //EscalaAprovacao gobjEscalaAprovacao;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.VerificarQueryTela(this.Request.QueryString["mod"]);
            }
        }

        #region BindModel
        /// <summary>
        /// Bind Model 
        /// </summary>
        /// <param name="penmTipoTransacao">Tipo de Transação</param>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        ///     [cmarchi] modify 26/1/2010
        /// </history>

        Collection<DataHorarioColaboradores> colDataHorarioColaboradores = null;
        protected void BindModel(Enums.TipoTransacao penmTipoTransacao)
        {
            if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.PopularComboDatas();
                colDataHorarioColaboradores = this.ObterDataHorariosColaboradores();

                this.radGridHorariosColaboradores.DataSource = colDataHorarioColaboradores;
                this.radGridHorariosColaboradores.DataBind();

                this.lblMensagem.Text = string.Empty;

                btnEnviarAprovacao.Enabled = (colDataHorarioColaboradores.Count > 0);

                if (this.Visualizar)
                {
                    this.btnVoltar.Enabled = true;
                    this.btnVoltar.Visible = true;
                }
            }
        }
        #endregion

        #region Botões

        #region Exportar Excel
        /// <summary>
        /// Botão Exporta para o Arquivo Excel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 21/12/2010
        ///     [cmarchi] modify 22/1/2010
        /// </history>
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            this.radGridHorariosColaboradores.ExportSettings.IgnorePaging = true;
            this.radGridHorariosColaboradores.ExportSettings.ExportOnlyData = true;
            this.radGridHorariosColaboradores.MasterTableView.ExportToExcel();
            this.radGridHorariosColaboradores.ExportSettings.OpenInNewWindow = true;
        }

        private void ObterEscalasExportação(int p)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region Filtrar Data
        /// <summary>
        /// Botão de filtrar data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        /// </history>
        protected void btnFiltrarData_Click(object sender, EventArgs e)
        {
            Collection<DataHorarioColaboradores> colDataHorarioColaboradores = null;

            colDataHorarioColaboradores = this.ObterDataHorariosColaboradores();

            this.radGridHorariosColaboradores.DataSource = colDataHorarioColaboradores;
            this.radGridHorariosColaboradores.DataBind();
        }
        #endregion

        #region Voltar
        /// <summary>
        /// Botão de voltar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(this.Endereco);
        }
        #endregion

        #region Importar
        /// <history>
        ///     [haguiar] 04/11/2011 16:06
        /// </history>
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            
            lblMensagem.Text = string.Empty;
            lblMensagem.Visible = false;
            /*
            if (this.ddlAprovadores.SelectedIndex == 0)
            {
                this.RadAjaxPanel1.Alert("Selecione um aprovador");

                return;
            }
            */

            BLEscala objBLEscala = new BLEscala();
            Usuario objUsuario = null; 
            objUsuario = BLAcesso.ObterUsuario();

            DateTime pdtDe_Periodo_ini;
            DateTime pdtDe_Periodo_Fim;
            DateTime pdtPara_Periodo_ini;
            DateTime pdtPara_Periodo_Fim;

            try
            {

                if (this.IdTipoSolicitacao == 7)
                {
                    pdtDe_Periodo_ini = Convert.ToDateTime(Periodo.ToString().Substring(0, 10));
                    pdtDe_Periodo_Fim = Convert.ToDateTime(Periodo.ToString().Substring(13, 10));

                    pdtPara_Periodo_ini = Convert.ToDateTime(PeriodoImportar.ToString().Substring(0, 10));
                    pdtPara_Periodo_Fim = Convert.ToDateTime(PeriodoImportar.ToString().Substring(13, 10));
                }
                else
                {
                    pdtDe_Periodo_ini = Convert.ToDateTime(Periodo.ToString().Substring(0, 10));
                    pdtDe_Periodo_Fim = Convert.ToDateTime(Periodo.ToString().Substring(0, 10));

                    pdtPara_Periodo_ini = Convert.ToDateTime(PeriodoImportar.ToString().Substring(0, 10));
                    pdtPara_Periodo_Fim = Convert.ToDateTime(PeriodoImportar.ToString().Substring(0, 10));
                }
                
                int intEscalacao;
                intEscalacao = objBLEscala.ImportarEscalacaoRonda(
                    this.IdEscalaDpto,
                    pdtDe_Periodo_ini,
                    pdtDe_Periodo_Fim,
                    pdtPara_Periodo_ini,
                    pdtPara_Periodo_Fim,
                    this.IdTipoSolicitacao,
                    Convert.ToInt32(BLAcesso.IdUsuarioLogado()), 
                    this.chkHoraExtra.Checked);

                /*
                EscalaAprovacao gobjEscalaAprovacao;
                gobjEscalaAprovacao = new EscalaAprovacao();

                gobjEscalaAprovacao.IdEscalacao = intEscalacao;
                gobjEscalaAprovacao.IdStatusSolicitacao = 1;
                gobjEscalaAprovacao.IdUsuarioAprovador = Convert.ToInt32(this.ddlAprovadores.SelectedValue);

                objBLEscala.InserirEscalaAprovacao(gobjEscalaAprovacao);
                */

                if (intEscalacao > 0)
                {
                    //atualiza escala departamental
                    string strCodigoColaboradores;
                    if (Session["CodigosColaboradores"] != null)
                    {
                        strCodigoColaboradores = (string)Session["CodigosColaboradores"];
                        string[] parrIdColaboradores = strCodigoColaboradores.Split(',');

                        Collection<Colaborador> gcolColaboradores = new Collection<Colaborador>();

                        foreach (string strId in parrIdColaboradores)
                        {
                            Colaborador objColaborador = new Colaborador();
                            objColaborador.IdColaborador = Convert.ToInt32(strId);

                            gcolColaboradores.Add(objColaborador);
                        }

                         Escalacao gobjEscalacao = new Escalacao();
                         gobjEscalacao.IdEscalaDepartamental = this.IdEscalaDpto;
                         gobjEscalacao.IdUsuarioSolicitante = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                         gobjEscalacao.DataInicioPeriodo = pdtPara_Periodo_ini;
                         gobjEscalacao.DataFinalPeriodo = pdtPara_Periodo_Fim;

                         gobjEscalacao.IdEscalacao = intEscalacao;
                         objBLEscala.Editar(gobjEscalacao, gcolColaboradores);

                         Session["CodigosColaboradores"] = null;
                         Session.Remove("CodigosColaboradores");
                    }


                    this.Response.Redirect(@"../Escala/CadEscalaHorario.aspx?mod=" +
                            BLEncriptacao.EncQueryStr("CadHor," + intEscalacao.ToString()));

                    //this.Response.Redirect(@"../Escala/CadJornadaEscala.aspx?mod=" +
                    //        BLEncriptacao.EncQueryStr("CadSel," + intEscalacao.ToString()));
                }
                else
                {
                    this.RadAjaxPanel1.Alert("Problemas na importação. Tente novamente.");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
            
        }
        #endregion

        #endregion

        #region Grid Horarios Colaboradores

        #region NeedDataSource
        protected void radGridHorariosColaboradores_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.radGridHorariosColaboradores.DataSource = this.ObterDataHorariosColaboradores();
            }
        }
        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridEscala_ItemCommand
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] create 06/12/2011 10:34
        /// </history>
        protected void radGridHorariosColaboradores_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "OrdenarData")
            {
                blnFiltrar = !blnFiltrar;

                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = "DataColaboradores";
                sortExpr.SortOrder = (!blnFiltrar ? GridSortOrder.Ascending : GridSortOrder.Descending);
                radGridHorariosColaboradores.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

                this.radGridHorariosColaboradores.Rebind();                
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

            
        }
        #endregion

        #region ItemDataBound

        /// <summary>
        /// ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] create 07/11/2011 16:05
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

        #region ObterDataHorariosColaboradores
        /// <summary>
        /// Obtém a datas e horários dos colaboradores.
        /// </summary>
        /// <param name="pblnFiltrar">True - Filtra as datas, False - retorna todas as datas</param>
        /// <returns>Lista de datas e horários dos colaboradores</returns>
        /// <history>
        ///     [haguiar] modify 07/11/2011 16:00
        ///     filtrar por nome do colaborador
        /// </history>
        private Collection<DataHorarioColaboradores> ObterDataHorariosColaboradores()
        {
            BLEscala objBLEscala = new BLEscala();

            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                new Collection<DataHorarioColaboradores>();

            try
            {
                PopularComboDatas();

                colDataHorarioColaboradores = objBLEscala.ObterDtHorColEscalacaoRonda(this.IdEscalaDpto, this.IdTipoSolicitacao, colDatas);

                //modifica a lista de datas, filtrando por nome do colaborador
                ObterDataHorariosColaboradoresNome(ref colDataHorarioColaboradores, this.txtNomeColaborador.Text.ToString().Trim().ToUpper());
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            return colDataHorarioColaboradores;
        }

        /// <summary>
        /// Obtém a datas e horários dos colaboradores de acordo com o nome.
        /// </summary>
        /// <param name="colDataHorarioColaboradores">Coleçao de datas/horários</param>
        /// <param name="pstrColaborador">Filtra as datas por nome</param>
        /// <returns>Altera lista por referencia de datas e horários dos colaboradores de acordo com o nome</returns>
        /// <history>
        ///     [haguiar_2] created 01/12/2010
        /// </history>
        private void ObterDataHorariosColaboradoresNome(ref Collection<DataHorarioColaboradores> colDataHorarioColaboradores, string pstrColaborador)
        {
            Collection<DataHorarioColaboradores> colDataHorarioColaboradoresFiltrados = new Collection<DataHorarioColaboradores>();

            foreach (DataHorarioColaboradores datHorColab in colDataHorarioColaboradores)
            {
                if (datHorColab.NomesColaboradores.ToUpper().IndexOf(pstrColaborador.Trim().ToUpper()) != -1)
                {
                    colDataHorarioColaboradoresFiltrados.Add(datHorColab);
                }
            }

            colDataHorarioColaboradores = colDataHorarioColaboradoresFiltrados;
        }

        #endregion

        #region Popular Combos

        #region PopularComboDatas
        /// <summary>
        /// Preenche o combo de Datas da Escalação.
        /// </summary>
        /// <history>
        ///     [haguiar] modified 07/11/2011 15:55
        /// </history>
        private void PopularComboDatas()
        {
            //BLEscala objBLEscala = new BLEscala();

            try
            {
                //armazena as datas do período
                colDatas = new Collection<DateTime>();

                //DateTime dtPeriodoInicial = Convert.ToDateTime(Periodo.ToString().Substring(0, 10));
                //colDatas.Add(dtPeriodoInicial);

                this.ddlDatas.Items.Clear();

                ListItem limData = new ListItem();
                
                if (this.IdTipoSolicitacao == 7)
                {
                    //escala
                    limData.Text = Periodo.ToString();

                    colDatas.Add(Convert.ToDateTime(Periodo.ToString().Substring(0, 10)));
                    colDatas.Add(Convert.ToDateTime(Periodo.ToString().Substring(13, 10)));
                }
                else
                {
                    colDatas.Add(Convert.ToDateTime(Periodo.ToString().Substring(0, 10)));

                    //troca de horário
                    limData.Text = Periodo.ToString().Substring(0, 10);
                }

                limData.Value = string.Empty;

                this.ddlDatas.Items.Add(limData);
                this.ddlDatas.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #endregion

        #region Propriedades

        #region Duplicidade
        /// <summary>
        ///     Propriedade Duplicidade
        /// </summary>
        /// <history>
        ///     [haguiar] created 12/09/2011 15:40
        /// </history>
        private bool Duplicidade
        {
            get
            {
                if (this.ViewState["vsDuplicidade"] == null)
                {
                    this.ViewState.Add("vsDuplicidade", false);
                }

                return Convert.ToBoolean(this.ViewState["vsDuplicidade"]);
            }

            set
            {
                this.ViewState.Add("vsDuplicidade", value);
            }
        }
        #endregion

        #region Endereco
        /// <summary>
        ///     Propriedade Endereço de retorno da página que a chamou.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 10/2/2010 
        /// </history>
        private string Endereco
        {
            get
            {
                if (this.ViewState["vsEndereco"] == null)
                {
                    this.ViewState.Add("vsEndereco", string.Empty);
                }

                return this.ViewState["vsEndereco"].ToString();
            }

            set
            {
                this.ViewState.Add("vsEndereco", value);
            }
        }
        #endregion

        #region IdTipoSolicitacao
        /// <summary>
        ///     Propriedade IdTipoSolicitacao utilizada para o tipo da Escalacao
        /// </summary>
        /// <history>
        ///     [haguiar] created 07/11/2011 15:54
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

        #region IdEscalaDpto
        /// <summary>
        ///     Propriedade IdEscalaDpto utilizada para escala departamental
        /// </summary>
        /// <history>
        ///     [haguiar] created 07/11/2011 15:54
        /// </history>
        private int IdEscalaDpto
        {
            get
            {
                if (this.ViewState["vsIdEscalaDpto"] == null)
                {
                    this.ViewState.Add("vsIdEscalaDpto", 0);
                }

                return Convert.ToInt32(this.ViewState["vsIdEscalaDpto"]);
            }

            set
            {
                this.ViewState.Add("vsIdEscalaDpto", value);
            }
        }
        #endregion

        #region Visualizar
        /// <summary>
        ///     Propriedade Visualizar
        /// </summary>
        /// <history>
        ///     [cmarchi] created 27/1/2010 
        /// </history>
        private bool Visualizar
        {
            get
            {
                if (this.ViewState["vsVisualizar"] == null)
                {
                    this.ViewState.Add("vsVisualizar", false);
                }

                return Convert.ToBoolean(this.ViewState["vsVisualizar"]);
            }

            set
            {
                this.ViewState.Add("vsVisualizar", value);
            }
        }
        #endregion

        #region PeriodoImportar
        /// <summary>
        ///     Propriedade PeriodoImportar
        /// </summary>
        /// <history>
        ///     [haguiar] created 10/11/2011 15:51
        /// </history>
        private string PeriodoImportar
        {
            get
            {
                if (this.ViewState["vsPeriodoImportar"] == null)
                {
                    this.ViewState.Add("vsPeriodoImportar", string.Empty);
                }

                return Convert.ToString(this.ViewState["vsPeriodoImportar"]);
            }

            set
            {
                this.ViewState.Add("vsPeriodoImportar", value);
            }
        }
        #endregion

        #region Periodo
        /// <summary>
        ///     Propriedade Periodo
        /// </summary>
        /// <history>
        ///     [haguiar SDM 9004] created 11/08/2011 09:51
        /// </history>
        private string Periodo
        {
            get
            {
                if (this.ViewState["vsPeriodo"] == null)
                {
                    this.ViewState.Add("vsPeriodo", string.Empty);
                }

                return Convert.ToString(this.ViewState["vsPeriodo"]);
            }

            set
            {
                this.ViewState.Add("vsPeriodo", value);
            }
        }
        #endregion
        #endregion

        #region VerificarQueryTela
        /// <summary>
        /// Verifica a query da Tela que a chamou.
        /// </summary>
        ///<param name="pstrQuery">Query</param>
        /// <history>
        ///     [haguiar] modify  07/11/2011
        /// </history>
        private void VerificarQueryTela(string pstrQuery)
        {
            if (!string.IsNullOrEmpty(pstrQuery))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split(',');
            
                int intIdTipoSolicitacao;

                if (Int32.TryParse(arrParametros[0], out intIdTipoSolicitacao))
                {
                    this.IdTipoSolicitacao = intIdTipoSolicitacao;

                    //habilitado apenas em trocas de horário
                    chkHoraExtra.Enabled = (this.IdTipoSolicitacao == 9);

                    this.Periodo = arrParametros[1];

                    this.PeriodoImportar = arrParametros[2];
                    this.ddlPeriodoImportar.Items.Add(new ListItem(arrParametros[2], arrParametros[2]));

                    int pintIdEscalaDpto;
                    Int32.TryParse(arrParametros[3], out pintIdEscalaDpto);
                    this.IdEscalaDpto = pintIdEscalaDpto;

                    this.Visualizar = true;

                    this.Endereco = @"../Escala/ImportarEscalaRonda.aspx";

                    this.BindModel(Enums.TipoTransacao.DescarregarDados);

                    return;
                }
            }
        }

        #region colDatas
        /// <summary>
        ///     Propriedade colDatas utilizada para armazenar datas da escala/troca de horario
        /// </summary>
        /// <history>
        ///     [haguiar_9004] created 11/08/2011 15:13
        /// </history>
        public Collection<DateTime> colDatas
        {
            get
            {
                if (ViewState["vscolDatas"] == null)
                {
                    ViewState["vscolDatas"] = new Collection<DateTime>();
                }
                return (Collection<DateTime>)ViewState["vscolDatas"];
            }
            set
            {
                ViewState["vscolDatas"] = value;
            }
        }
        #endregion


        #region blnFiltrar
        /// <summary>
        ///     Propriedade blnFiltrar para filtrar datas ASC ou DESC
        /// </summary>
        /// <history>
        ///     [haguiar] created 06/12/2011 11:33
        /// </history>
        public bool blnFiltrar
        {
            get
            {
                if (ViewState["vsblnFiltrar"] == null)
                {
                    ViewState["vsblnFiltrar"] = false;
                }
                return (bool)ViewState["vsblnFiltrar"];
            }
            set
            {
                ViewState["vsblnFiltrar"] = value;
            }
        }
        #endregion
        #endregion
    }
}