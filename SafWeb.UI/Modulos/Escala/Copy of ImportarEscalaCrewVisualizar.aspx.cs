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
    public partial class ImportarEscalaCrewVisualizar : FWPage
    {
        EscalaAprovacao gobjEscalaAprovacao;

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
            if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                Collection<DataHorarioColaboradores> colDataHorarioColaboradores = null;

                this.PopularComboDatas();
                colDataHorarioColaboradores = this.ObterDataHorariosColaboradores(false);

                this.radGridHorariosColaboradores.DataSource = colDataHorarioColaboradores;
                this.radGridHorariosColaboradores.DataBind();

                this.lblMensagem.Text = string.Empty;

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
                    
                    //lblDataHora.Text = DataBinder.Eval(e.Item.DataItem, "DataColaboradores").ToString().Substring(0, 9) + " - Compensado";
                }
                else if (strFolga != string.Empty)
                {

                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy}", dtDataHora).Substring(0, 10) + " - Folga/DSR";

                    //lblDataHora.Text = DataBinder.Eval(e.Item.DataItem, "DataColaboradores").ToString().Substring(0, 9) + " - Folga";
                }
                else if (strLicenca != string.Empty)
                {

                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy}", dtDataHora).Substring(0, 10) + " - Férias/Licença";

                    //lblDataHora.Text = DataBinder.Eval(e.Item.DataItem, "DataColaboradores").ToString().Substring(0, 9) + " - Licença";

                }
                else if (strHorarioFlex != string.Empty)
                {

                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy}", dtDataHora).Substring(0, 10) + " - 08 às 09 flex";

                    //lblDataHora.Text = DataBinder.Eval(e.Item.DataItem, "DataColaboradores").ToString().Substring(0, 9) + " - Licença";

                }
                else if (strHoraExtra != string.Empty)
                {

                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy HH:mm}", dtDataHora) + " - Hora Extra";

                }
                else
                {

                    lblDataHora.Text = String.Format("{0:dd/MM/yyyy HH:mm}", dtDataHora);

                    //lblDataHora.Text = DataBinder.Eval(e.Item.DataItem, "DataColaboradores").ToString();
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

            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                new Collection<DataHorarioColaboradores>();

            try
            {
                colDataHorarioColaboradores = objBLEscala.ObterDtHorColEscalacaoCrew(this.IdEscalacao, colDatas[0], null, colDatas, Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

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
        ///     [cmarchi] created 20/1/2010
        ///     [haguiar] modified 21/10/2010
        /// </history>
        private void PopularComboDatas()
        {
            BLEscala objBLEscala = new BLEscala();

            try
            {
                colDatas = new Collection<DateTime>();

                DateTime dtPeriodoInicial = Convert.ToDateTime(Periodo.ToString().Substring(0, 10));
                colDatas.Add(dtPeriodoInicial);

                //[haguiar]
                //limpa o combo de datas
                this.ddlDatas.Items.Clear();

                foreach (DateTime datData in colDatas)
                {
                    ListItem limData = new ListItem();
                    limData.Text = String.Format("{0:dd/MM/yyyy}", datData);
                    limData.Value = String.Format("{0:dd/MM/yyyy}", datData);

                    this.ddlDatas.Items.Add(limData);
                }

                //armazena somente primeira data do período
                colDatas = new Collection<DateTime>();
                colDatas.Add(Convert.ToDateTime(Periodo.ToString().Substring(0, 10)));

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
            
                if (arrParametros[0] == "CadCaixa")
                {
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;
                        this.Periodo = arrParametros[2];

                        this.Visualizar = true;

                        if (arrParametros[0] == "CadCaixa")
                            this.Endereco = @"../Escala/ImportarEscalaCrew.aspx";

                        this.BindModel(Enums.TipoTransacao.DescarregarDados);

                        return;
                    }
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

        #endregion
    }
}