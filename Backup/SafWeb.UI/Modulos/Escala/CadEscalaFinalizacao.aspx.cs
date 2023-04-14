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
    public partial class CadEscalaFinalizacao : FWPage
    {
        EscalaAprovacao gobjEscalaAprovacao;

        private Escalacao objEscalacao
        {
            get
            {
                if (ViewState["vsobjEscalacao"] == null)
                    ViewState["vsobjEscalacao"] = new Escalacao();
                return (Escalacao)ViewState["vsobjEscalacao"];
            }
            set { ViewState["vsobjEscalacao"] = value; }
        }

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
        protected void BindModel(Enums.TipoTransacao penmTipoTransacao)
        {
            if (penmTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                this.gobjEscalaAprovacao = new EscalaAprovacao();

                this.gobjEscalaAprovacao.IdEscalacao = this.IdEscalacao;
                this.gobjEscalaAprovacao.IdStatusSolicitacao = 1;
                this.gobjEscalaAprovacao.IdUsuarioAprovador =
                    Convert.ToInt32(this.ddlAprovadores.SelectedValue);
            }
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                Collection<DataHorarioColaboradores> colDataHorarioColaboradores = null;

                BLEscala objBLEscala = new BLEscala();
                objEscalacao = objBLEscala.Obter(this.IdEscalacao);

                txtPeriodo.Text = string.Format("{0:dd/MM/yyyy} à {1:dd/MM/yyyy}", objEscalacao.DataInicioPeriodo, objEscalacao.DataFinalPeriodo);

                this.PopularComboDatas();
                colDataHorarioColaboradores = this.ObterDataHorariosColaboradores(false);

                this.radGridHorariosColaboradores.DataSource = colDataHorarioColaboradores;
                this.radGridHorariosColaboradores.DataBind();

                this.PopularAprovadores();

                this.lblMensagem.Text = string.Empty;

                if (this.Visualizar)
                {
                    this.ddlAprovadores.Enabled = false;
                    this.btnEnviarAprovacao.Enabled = false;
                    //this.btnEditarData.Enabled = false;

                    this.btnVoltar.Enabled = true;
                    this.btnVoltar.Visible = true;
                }
            }
        }
        #endregion

        #region Botões

        //#region Editar Data
        ///// <summary>
        ///// Botão de editar data.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        ///// <history>
        /////     [cmarchi] created 22/1/2010
        /////     [haguiar] modify 08/11/2010
        /////     [haguiar_2] modify 24/11/2010
        /////     Enviar parametro de troca de horário para a ediçao de data
        /////     [haguiar] modify 03/01/2012 13:47
        /////     adicionar codlegado
        ///// </history>
        //protected void btnEditarData_Click(object sender, EventArgs e)
        //{
        //    string strIdColaboradores = string.Empty;
        //    string strData = string.Empty;
        //    string strParametro = string.Empty;
        //    string strCodLegado = string.Empty;

        //    //procura por uma data selecionada
        //    foreach (GridDataItem dataItem in radGridHorariosColaboradores.MasterTableView.Items)
        //    {
        //        if (dataItem.Selected)
        //        {
        //            strIdColaboradores = dataItem["CodigosColaboradores"].Text.Trim();
        //            strCodLegado = dataItem["CodLegado"].Text.Trim();

        //            //formato dd/mm/yyyy
        //            DateTime dtDateTime = Convert.ToDateTime(dataItem["DataColaboradores"].Text.Trim());
                    
        //            strData = String.Format("{0:dd/MM/yyyy HH:mm}", dtDateTime);

        //            break;
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(strData) && !string.IsNullOrEmpty(strIdColaboradores))
        //    {
        //        //cria o parametro
        //        Session.Add("CodigosColaboradores", strIdColaboradores);

        //        if (!this.blnTrocaHorario)
        //        {
        //            strParametro = BLEncriptacao.EncQueryStr("CadHor," + this.IdEscalacao);
        //            this.Response.Redirect("CadEscalaHorario.aspx?mod=" + strParametro, false);
        //        }
        //        else
        //        {
        //            strParametro = BLEncriptacao.EncQueryStr("TrocaHor," + this.IdEscalacao);
        //            this.Response.Redirect("CadTrocaEscalaHorario.aspx?mod=" + strParametro, false);
        //        }

        //    }
        //    else
        //    {
        //        this.RadAjaxPanel1.Alert("Selecione uma data.");
        //    }

        //}
        //#endregion

        #region Enviar Aprovação
        /// <summary>
        /// Botão Enviar Aprovação de Escalas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 26/11/2009
        /// </history>
        protected void btnEnviarAprovacao_Click(object sender, EventArgs e)
        {
            BLEscala objBLEscala = new BLEscala();

            Usuario objUsuario = null;

            string strEscalacao = string.Empty;


            try
            {
                if (this.Page.IsValid && this.Validar())
                {
                    this.BindModel(Enums.TipoTransacao.CarregarDados);

                    objUsuario = BLAcesso.ObterUsuario();

                    if (objUsuario != null && !string.IsNullOrEmpty(objUsuario.NomeUsuario))
                    {
                        objBLEscala.InserirEscalaAprovacao(this.gobjEscalaAprovacao);

                        strEscalacao = "Escala n°: " + this.gobjEscalaAprovacao.IdEscalacao.ToString() +
                            " - Usuário Solicitante: " + objUsuario.NomeUsuario;

                        //this.RadAjaxPanel1.Alert(strEscalacao);
                        //Response.Redirect("CadSelecaoEscalaColaborador.aspx");

                        //lblMensagem.Visible = true;
                        //lblMensagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));

                        /*
                         * REDIRECIONA PARA A PAGINA DE LISTAGEM  COM A MENSAGEM DE OK! *strEscalacao
                         * 23/06/2010 by [abranco] 
                         */

                        Response.Redirect("CadSelecaoEscalaColaborador.aspx?navid=170&MsgCadOk=" + strEscalacao);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Exportar Excel
        /// <summary>
        /// Botão Exporta para o Arquivo Excel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 21/12/2010
        ///     [cmarchi] modify 22/1/2010
        ///     [haguiar] modify 03/01/2012 11:08
        /// </history>
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {/*
            BLEscala objBLEscala = new BLEscala();
            DataTable dttDataHorarioColaboradores = null;

            dttDataHorarioColaboradores = objBLEscala.ObterEscalasExportação(this.IdEscalacao);

            if (dttDataHorarioColaboradores != null &&
               dttDataHorarioColaboradores.Rows.Count > 0)
            {
                DataGrid dtgExportacao = new DataGrid();
                dtgExportacao.DataSource = dttDataHorarioColaboradores;
                dtgExportacao.DataBind();

                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.Charset = "UTF-8";
                this.Response.Charset = "";
                this.Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
                this.Response.AddHeader("content-disposition", "attachment;filename=Escalas_" + DateTime.Now.ToShortDateString() +
                    "_" + DateTime.Now.ToShortTimeString() + ".xls");

                this.EnableViewState = false;

                StringWriter oStringWriter = new StringWriter();
                Html32TextWriter oHtmlWriter = new Html32TextWriter(oStringWriter);

                dtgExportacao.RenderControl(oHtmlWriter);
                this.Response.Write(oStringWriter.ToString());
                this.Response.End();
            }
            else
            {
                this.RadAjaxPanel1.Alert("Impossível exportar o formato Excel. Não há colaboradores sem escala.");
            }*/

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

            colDataHorarioColaboradores = this.ObterDataHorariosColaboradores(true);

            this.radGridHorariosColaboradores.DataSource = colDataHorarioColaboradores;
            this.radGridHorariosColaboradores.DataBind();
        }
        #endregion

        #region Filtrar Data
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
        ///     [no history]
        ///     [haguiar] modify 08/11/2010
        ///     [haguiar_2] modify 04/12/2010
        ///     adicionar horário flex
        ///     [haguiar_8829] modify 06/07/2011 16:24
        ///     adicionar hora extra
        ///     [haguiar] modify 03/01/2012 11:12
        ///     adicionar codlegado
        /// </history>
        protected void radGridHorariosColaboradores_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton imgbEditar = (ImageButton)e.Item.FindControl("Editar");
                Image imbStatus = (Image)e.Item.FindControl("IMGStatus");

                if (imgbEditar != null)
                {
                    imgbEditar.Visible = !this.Visualizar;
                }

                if (imbStatus != null)
                {
                    imbStatus.Visible = this.Visualizar;
                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "flgSituacao").ToString()))
                    {
                        imbStatus.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        imbStatus.ToolTip = "Ativo(s)";
                    }
                    else
                    {
                        imbStatus.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        imbStatus.ToolTip = "Inativo(s)";
                    }
                }

                Label lblDataHora = (Label)e.Item.FindControl("lblDataHora");

                string strCompensado = DataBinder.Eval(e.Item.DataItem, "Compensado").ToString();
                string strFolga = DataBinder.Eval(e.Item.DataItem, "Folga").ToString();
                string strLicenca = DataBinder.Eval(e.Item.DataItem, "Licenca").ToString();
                string strHorarioFlex = DataBinder.Eval(e.Item.DataItem, "HorarioFlex").ToString();
                string strHoraExtra = DataBinder.Eval(e.Item.DataItem, "HoraExtra").ToString();
                string strCodLegado = DataBinder.Eval(e.Item.DataItem, "CodLegado").ToString();
                bool strFlgIniciaFolgando = Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "FlgIniciaFolgando").ToString());
                int intIdJornada = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IdJornada"));
                //string strDescricaoHorario = DataBinder.Eval(e.Item.DataItem, "HorarioColaborador").ToString();
                DateTime dtDataHora = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "DataColaboradores").ToString());

                string strDiaSemana;

                switch (dtDataHora.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        strDiaSemana = "Domingo ";
                        break;
                    case DayOfWeek.Monday:
                        strDiaSemana = "Segunda ";
                        break;
                    case DayOfWeek.Tuesday:
                        strDiaSemana = "Terça ";
                        break;
                    case DayOfWeek.Wednesday:
                        strDiaSemana = "Quarta ";
                        break;
                    case DayOfWeek.Thursday:
                        strDiaSemana = "Quinta ";
                        break;
                    case DayOfWeek.Friday:
                        strDiaSemana = "Sexta ";
                        break;
                    case DayOfWeek.Saturday:
                        strDiaSemana = "Sábado ";
                        break;
                    default:
                        strDiaSemana = "";
                        break;
                }



                if (strCompensado != string.Empty)
                {
                    lblDataHora.Text = String.Format("{2}{0:dd/MM/yyyy} - {1}", dtDataHora, "Compensado (Funcionários não autorizados neste dia.)", strDiaSemana);
                }
                else if (strFolga != string.Empty)
                {
                    if (strCodLegado.Equals("9997"))
                        lblDataHora.Text = String.Format("{2}{0:dd/MM/yyyy} - {1}", dtDataHora, "Feriado (Funcionários não autorizados neste dia, para autorizar favor fazer uma troca de horário)", strDiaSemana);
                    else
                        lblDataHora.Text = String.Format("{2}{0:dd/MM/yyyy} - {1}", dtDataHora, "Folga/DSR (Funcionários não autorizados neste dia.)", strDiaSemana);
                }
                else if (strLicenca != string.Empty)
                {
                    if (dtDataHora.DayOfWeek != DayOfWeek.Sunday && dtDataHora.DayOfWeek != DayOfWeek.Saturday)
                        lblDataHora.Text = "Férias/Licença";
                    else
                        lblDataHora.Text = string.Format("{0}{1:dd/MM/yyyy} {2}", strDiaSemana, dtDataHora, "Férias/Licença (Funcionários não autorizados neste dia.)");
                }
                else if (strHorarioFlex != string.Empty)
                {
                    if (dtDataHora.DayOfWeek != DayOfWeek.Sunday && dtDataHora.DayOfWeek != DayOfWeek.Saturday)
                        lblDataHora.Text = "08 às 09 flex";
                    else
                        lblDataHora.Text = String.Format("{2}{0:dd/MM/yyyy} - {1}", dtDataHora, "08 às 09 flex", strDiaSemana);
                }
                else if (strHoraExtra != string.Empty)
                {
                    string strDescricaoHorario = DataBinder.Eval(e.Item.DataItem, "HorarioColaborador").ToString();
                    lblDataHora.Text = String.Format("{2}{0:dd/MM/yyyy} {3} - {1}", dtDataHora, "Hora Extra", strDiaSemana, strDescricaoHorario);
                }
                else
                {

                    string strDescricaoHorario = DataBinder.Eval(e.Item.DataItem, "HorarioColaborador").ToString();
                    if (dtDataHora.DayOfWeek != DayOfWeek.Sunday && dtDataHora.DayOfWeek != DayOfWeek.Saturday)
                        lblDataHora.Text = strDescricaoHorario;
                    else
                        lblDataHora.Text = string.Format("{0}{1:dd/MM/yyyy} {2}", strDiaSemana, dtDataHora, strDescricaoHorario);
                }

                if (intIdJornada.Equals(3))
                {
                    if (strFlgIniciaFolgando)
                        lblDataHora.Text += " - Inicia em folga";

            }

        }
        }
        #endregion

        #region ItemCommand
        
        /// <summary>
        ///     ItemCommand
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] created 08/11/2010
        /// </history>        
        protected void radGridHorariosColaboradores_ItemCommand(object source, GridCommandEventArgs e)
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

            if (e.CommandName.Trim().Equals("Editar"))
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                string strIdColaboradores = string.Empty;
                string strData = string.Empty;
                string strParametro = string.Empty;
                string strCodLegado = string.Empty;

                strIdColaboradores = dataItem["CodigosColaboradores"].Text.Trim();
                strCodLegado = dataItem["CodLegado"].Text.Trim();

                //formato dd/mm/yyyy
                DateTime dtDateTime = Convert.ToDateTime(dataItem["DataColaboradores"].Text.Trim());

                strData = String.Format("{0:dd/MM/yyyy HH:mm}", dtDateTime);

                if (!string.IsNullOrEmpty(strData) && !string.IsNullOrEmpty(strIdColaboradores))
                {
                    //cria o parametro
                    Session.Add("CodigosColaboradores", strIdColaboradores);

                    if (!this.blnTrocaHorario)
                    {
                        strParametro = BLEncriptacao.EncQueryStr("CadHor," + this.IdEscalacao);
                        this.Response.Redirect("CadEscalaHorario.aspx?mod=" + strParametro, false);
        }
                    else
                    {
                        strParametro = BLEncriptacao.EncQueryStr("TrocaHor," + this.IdEscalacao);
                        this.Response.Redirect("CadTrocaEscalaHorario.aspx?mod=" + strParametro, false);
                    }
        
                }
                else
                {
                    this.RadAjaxPanel1.Alert("Selecione uma data.");
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
        ///     [cmarchi] created 20/1/2010
        ///     [haguiar_2] modify 01/12/2010
        ///     filtrar por nome do colaborador
        /// </history>
        private Collection<DataHorarioColaboradores> ObterDataHorariosColaboradores(bool pblnFiltrar)
        {
            BLEscala objBLEscala = new BLEscala();
            //BLJornada objBLJornada = new BLJornada();

            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                new Collection<DataHorarioColaboradores>();
            
            //Collection<JornadaColaboradores> colJornadaColaboradores = new Collection<JornadaColaboradores>();



            try
            {
                if (pblnFiltrar && this.ddlDatas.SelectedIndex > 0)
                {
                    DateTime datDataSelecionada = Convert.ToDateTime(this.ddlDatas.SelectedValue);
                    colDataHorarioColaboradores = objBLEscala.ObterDtHorColEscalacao(this.IdEscalacao,
                                                                                     datDataSelecionada, false,null);
                }
                else
                {
                    colDataHorarioColaboradores = objBLEscala.ObterDtHorColEscalacao(this.IdEscalacao, null, null,null);
                    //colJornadaColaboradores = objBLJornada.ObterJornadaColaboradores(this.IdEscalacao);


                }

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

        #region Combo Aprovadores
        /// <summary>
        ///     Popula o combo com os possíveis aprovadores
        /// </summary>
        /// <history>
        ///     [mribeiro] 26/01/2010 created
        ///     [haguiar] 25/10/2010 modify
        ///     [haguiar_2] modify 24/11/2010
        ///     [haguiar] modify 03/02/2011
        ///     erro caso nao exista aprovador para o usuario que criou a escala.
        /// </history>
        protected void PopularAprovadores()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            BLEscala objBLEscala = new BLEscala();

            Escalacao objEscalacao = null;
            EscalaDepartamental objEscalaDepartamental = null;

            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<Aprovador> colAprovador = new Collection<Aprovador>();

            try
            {
                //obtém a escalação
                objEscalacao = objBLEscala.Obter(this.IdEscalacao);

                //verifica se é nullo
                if (objEscalacao != null && objEscalacao.IdEscalaDepartamental > 0)
                {
                    //obtém a regional e filial
                    objEscalaDepartamental = objBLEscalaDepartamental.Obter(objEscalacao.IdEscalaDepartamental, false,null);

                    colAprovador = objBlSolicitacao.ListarAprovadores(
                        objEscalaDepartamental.ObjRegional.IdRegional.ToString(),
                        objEscalaDepartamental.ObjFilial.IdFilial.ToString(),
                        objEscalacao.IdTipoSolicitacao.ToString());

                    //se nao estiver apenas visualizando, remove o aprovador logado
                    if (!this.Visualizar)
                    {
                        //Remove o aprovador logado
                        foreach (Aprovador objAprovador in colAprovador)
                        {
                            if (objAprovador.IdUsuario == BLAcesso.IdUsuarioLogado())
                            {
                                colAprovador.Remove(objAprovador);
                                break;
                            }
                        }
                    }

                    //obter usuários aprovadores escalados
                    Collection<DataHorarioColaboradores> colDataHorarioColaboradores = null;
                    Collection<Colaborador> colColaborador = null;                 
                    BLColaborador objBLColaborador = new BLColaborador();
                    SafWeb.BusinessLayer.Acesso.BLAcessoColaborador objBLAcessoColaborador = new BusinessLayer.Acesso.BLAcessoColaborador();

                    this.PopularComboDatas();
                    colDataHorarioColaboradores = this.ObterDataHorariosColaboradores(false);

                    if (colDataHorarioColaboradores.Count > 0)
                    {
                        //erro caso a lista tenha apenas 1 registro (0)
                        //string[] parrIdColaboradores = colDataHorarioColaboradores[1].CodigosColaboradores.Split(',');
                        string[] parrIdColaboradores = colDataHorarioColaboradores[0].CodigosColaboradores.Split(',');
                        
                        //Obtém os coódigos dos colaboradores
                        colColaborador = objBLColaborador.Obter(parrIdColaboradores);

                        //se nao estiver apenas visualizando, remove o aprovador logado
                        if (!this.Visualizar)
                        {

                            //remove operador escalado
                            foreach (Colaborador objColaborador in colColaborador)
                            {
                                int intIdUsuario = objBLAcessoColaborador.ObterIdUsuario(objColaborador.IdColaborador);

                                foreach (Aprovador objAprovador in colAprovador)
                                {
                                    if (objAprovador.IdUsuario == intIdUsuario)
                                    {
                                        colAprovador.Remove(objAprovador);
                                        break;
                                    }
                                }
                            }
                        }

                        this.ddlAprovadores.DataSource = colAprovador;
                        this.ddlAprovadores.DataTextField = "NomeUsuario";
                        this.ddlAprovadores.DataValueField = "IdUsuario";
                        this.ddlAprovadores.DataBind();
                    }
                }

                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlAprovadores,
                    BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                if (this.Visualizar)
                {

                    DataTable dttAprovadores = new DataTable();

                    try
                    {
                        dttAprovadores = objBLEscala.ObterAprovadores(
                            Convert.ToInt32(this.IdEscalacao));
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                    }

                    //ListItem ltmAprovador = this.ddlAprovadores.Items.FindByValue(
                    //    BLAcesso.IdUsuarioLogado().ToString());

                    if (dttAprovadores != null)
                    {

                        if (dttAprovadores.Rows.Count > 1)
                        {
                            ListItem ltmAprovador = this.ddlAprovadores.Items.FindByValue(
                                Convert.ToString(dttAprovadores.Rows[0][0]));

                            if (ltmAprovador != null)
                            {
                                ltmAprovador.Selected = true;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region PopularComboDatas
        /// <summary>
        /// Preenche o combo de Datas da Escalação.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 20/1/2010
        ///     [haguiar] modified 21/10/2010
        /// </history>
        private void PopularComboDatas()
        {
            BLEscala objBLEscala = new BLEscala();
            Collection<DateTime> colDatas = null;

            try
            {
                colDatas = objBLEscala.ObterDatas(this.IdEscalacao);

                //[haguiar]
                //limpa o combo de datas
                this.ddlDatas.Items.Clear();

                foreach (DateTime datData in colDatas)
                {
                    //Console.WriteLine(DateTime.ParseExact(datData.ToShortDateString(), "{0:dd/MM/yyyy}", System.Globalization.CultureInfo.InvariantCulture));

                    ListItem limData = new ListItem();
                    limData.Text = String.Format("{0:dd/MM/yyyy}", datData); //Convert.ToString(DateTime.ParseExact(datData.ToShortDateString().ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)); //datData.ToShortDateString(); //String.Format("{0:dd/MM/yyyy}", datData); //datData.ToShortDateString();
                    limData.Value = String.Format("{0:dd/MM/yyyy}", datData);  //datData.ToShortDateString();

                    this.ddlDatas.Items.Add(limData);
                }

                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlDatas, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #endregion

        #region Propriedades

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

        #region IdEscalacao
        /// <summary>
        ///     Propriedade Id_Escalacao utilizada para a Escalacao
        /// </summary>
        /// <history>
        ///     [cmarchi] created 20/1/2010 
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

        #endregion

        #region Validar
        /// <summary>
        /// Valida os campos da tela.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 26/11/2009
        /// </history>
        private bool Validar()
        {
            if (this.ddlAprovadores.SelectedIndex == 0)
            {
                this.RadAjaxPanel1.Alert("Selecione um aprovador");

                return false;
            }

            return true;
        }
        #endregion

        #region VerificarQueryTela
        /// <summary>
        /// Verifica a query da Tela que a chamou.
        /// </summary>
        ///<param name="pstrQuery">Query</param>
        /// <history>
        ///     [cmarchi]   created 20/1/2010
        ///     [cmarchi]   modify  10/2/2010
        ///     [haguiar_2] modify  24/11/2010
        ///     Parametro troca de horário
        /// </history>
        private void VerificarQueryTela(string pstrQuery)
        {
            if (!string.IsNullOrEmpty(pstrQuery))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split(',');

                //verifica se a tela
                if (arrParametros[0] == "CadHor" | arrParametros[0] == "TrocaHor")
                {
                    int intIdEscalacao;

                    if (arrParametros[0] == "TrocaHor")
                    {
                        this.blnTrocaHorario = true;
                        this.Label2.Text = "Troca de horário - Finalização";
                    }

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;

                        this.BindModel(Enums.TipoTransacao.DescarregarDados);

                        return;
                    }
                }

                else if (arrParametros[0] == "CadCaixa" || arrParametros[0] == "CadSelEscList")
                {
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;

                        this.Visualizar = true;

                        if (arrParametros[0] == "CadCaixa")
                            this.Endereco = @"../Aprovacao/CadCaixaEntrada.aspx";
                        else
                            this.Endereco = @"CadSelecaoEscalaColaborador.aspx";

                        if (Convert.ToInt32(arrParametros[2]) == 9)
                        {
                            this.Label2.Text = "Troca de horário - Finalização";
                        }

                        this.BindModel(Enums.TipoTransacao.DescarregarDados);

                        return;
                    }
                }
            }

            this.Response.Redirect("CadSelecaoEscalaColaborador.aspx");
        }

        #region blnTrocaHorario
        /// <summary>
        ///     Propriedade blnTrocaHorario utilizada para Troca de horário
        /// </summary>
        /// <history>
        ///     [haguiar_2] created 24/11/2010 
        /// </history>
        private bool blnTrocaHorario
        {
            get
            {
                if (this.ViewState["vsblnTrocaHorario"] == null)
                {
                    this.ViewState.Add("vsblnTrocaHorario", false);
                }

                return Convert.ToBoolean(this.ViewState["vsblnTrocaHorario"]);
            }

            set
            {
                this.ViewState.Add("vsblnTrocaHorario", value);
            }
        }
        #endregion
        #endregion
    }
}