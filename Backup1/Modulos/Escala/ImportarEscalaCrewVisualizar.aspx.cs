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

        Collection<DataHorarioColaboradores> colDataHorarioColaboradores = null;
        protected void BindModel(Enums.TipoTransacao penmTipoTransacao)
        {
            if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.PopularComboDatas();
                colDataHorarioColaboradores = this.ObterDataHorariosColaboradores(false);

                this.PopularAprovadores();

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
        ///     [haguiar_sdm9004] 31/08/2011 10:55
        /// </history>
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = string.Empty;
            lblMensagem.Visible = false;

            if (this.ddlAprovadores.SelectedIndex == 0)
            {
                this.RadAjaxPanel1.Alert("Selecione um aprovador");

                return;
            }

            if (VerificaDuplicidade())
            {
                //lblMensagem.Text = "Verifique a lista de colaboradores com horário duplicado e tente novamente!";
                //lblMensagem.Visible = true;

                this.RadAjaxPanel1.Alert("Verifique a lista de colaboradores com horário duplicado e tente novamente!"); 
                return;
            }

            BLEscala objBLEscala = new BLEscala();
            Usuario objUsuario = null; 
            objUsuario = BLAcesso.ObterUsuario();

            try
            {
                int intEscalacao = 0;
                intEscalacao = objBLEscala.ImportarEscalacaoCREW(Convert.ToInt32(BLAcesso.IdUsuarioLogado()), this.IdTipoSolicitacao, this.IdFilial, Convert.ToDateTime(this.Periodo), Convert.ToDateTime(this.PeriodoFinal), this.chkHoraExtra.Checked);

                EscalaAprovacao gobjEscalaAprovacao;
                gobjEscalaAprovacao = new EscalaAprovacao();

                gobjEscalaAprovacao.IdEscalacao = intEscalacao;
                gobjEscalaAprovacao.IdStatusSolicitacao = 1;
                gobjEscalaAprovacao.IdUsuarioAprovador = Convert.ToInt32(this.ddlAprovadores.SelectedValue);

                objBLEscala.InserirEscalaAprovacao(gobjEscalaAprovacao);

                /*
                strEscalacao = "Escala n°: " + gobjEscalaAprovacao.IdEscalacao.ToString() +
                    " - Usuário Solicitante: " + objUsuario.NomeUsuario;
                */

                //this.RadAjaxPanel1.Alert("Importação realizada com sucesso!");

                this.Response.Redirect(this.Endereco + "?mod=" +
                        BLEncriptacao.EncQueryStr(gobjEscalaAprovacao.IdEscalacao.ToString() + "," + objUsuario.NomeUsuario));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #region Verificar duplicidades

        private bool VerificaDuplicidade()
        {
            //verificar divergencia de horários duplicados
            Collection<DataHorarioColaboradores> colDataHorarioColaboradores =
                new Collection<DataHorarioColaboradores>();

            try
            {
                colDataHorarioColaboradores = this.ObterDataHorariosColaboradores(true);

                Duplicidade = false;

                if (colDataHorarioColaboradores.Count > 0)
                {
                    this.radGridHorariosColaboradores.DataSource = colDataHorarioColaboradores;
                    this.radGridHorariosColaboradores.DataBind();

                    Duplicidade = true;
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
        #endregion

        #endregion

        #region Grid Horarios Colaboradores

        #region NeedDataSource
        protected void radGridHorariosColaboradores_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.radGridHorariosColaboradores.DataSource = this.ObterDataHorariosColaboradores(Duplicidade);
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
                colDataHorarioColaboradores = objBLEscala.ObterDtHorColEscalacaoCrew(colDatas[0], null, colDatas, Convert.ToInt32(BLAcesso.IdUsuarioLogado()), pblnFiltrar, this.IdTipoSolicitacao);

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
        ///     [haguiar_sdm9004] create 31/08/2011 10:21
        /// </history>
        protected void PopularAprovadores()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<Aprovador> colAprovador = new Collection<Aprovador>();

            try
            {

                //seleciona a regional e a filial do usuário logado.
                BLColaborador objBlColaborador = new BLColaborador();
                DataTable dtt = new DataTable();

                dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                colAprovador = objBlSolicitacao.ListarAprovadores(
                    dtt.Rows[0][0].ToString(),
                    dtt.Rows[0][1].ToString(),
                    Convert.ToString(9));

                    //Remove o aprovador logado
                    foreach (Aprovador objAprovador in colAprovador)
                    {
                        if (objAprovador.IdUsuario == BLAcesso.IdUsuarioLogado())
                        {
                            colAprovador.Remove(objAprovador);
                            break;
                        }
                    }

                    //obter usuários aprovadores escalados
                    //Collection<DataHorarioColaboradores> colDataHorarioColaboradores = null;
                    Collection<Colaborador> colColaborador = null;
                    BLColaborador objBLColaborador = new BLColaborador();
                    SafWeb.BusinessLayer.Acesso.BLAcessoColaborador objBLAcessoColaborador = new BusinessLayer.Acesso.BLAcessoColaborador();

                    //colDataHorarioColaboradores = this.ObterDataHorariosColaboradores(false);

                    if (colDataHorarioColaboradores.Count > 0)
                    {
                        //erro caso a lista tenha apenas 1 registro (0)
                        //string[] parrIdColaboradores = colDataHorarioColaboradores[1].CodigosColaboradores.Split(',');
                        string[] parrIdColaboradores = colDataHorarioColaboradores[0].CodigosColaboradores.Split(',');

                        //Obtém os coódigos dos colaboradores
                        colColaborador = objBLColaborador.Obter(parrIdColaboradores);

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

                        this.ddlAprovadores.DataSource = colAprovador;
                        this.ddlAprovadores.DataTextField = "NomeUsuario";
                        this.ddlAprovadores.DataValueField = "IdUsuario";
                        this.ddlAprovadores.DataBind();
                    }
                

                BLUtilitarios.InseriMensagemDropDownList(ref this.ddlAprovadores,
                    BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

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

        #region PeriodoFinal
        /// <summary>
        ///     Propriedade Periodo Final
        /// </summary>
        /// <history>
        ///     [haguiar] created 23/03/2012 19:10
        /// </history>
        private string PeriodoFinal
        {
            get
            {
                if (this.ViewState["vsPeriodoFinal"] == null)
                {
                    this.ViewState.Add("vsPeriodoFinal", string.Empty);
                }

                return Convert.ToString(this.ViewState["vsPeriodoFinal"]);
            }

            set
            {
                this.ViewState.Add("vsPeriodoFinal", value);
            }
        }
        #endregion

        #region IdFilial
        /// <summary>
        ///     Propriedade IdFilial utilizada a IdFilial
        /// </summary>
        /// <history>
        ///     [haguiar] created 23/03/2012 19:09
        /// </history>
        private int IdFilial
        {
            get
            {
                if (this.ViewState["vsIdFilial"] == null)
                {
                    this.ViewState.Add("vsIdFilial", 0);
                }

                return Convert.ToInt32(this.ViewState["vsIdFilial"]);
            }

            set
            {
                this.ViewState.Add("vsIdFilial", value);
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
                    int intFlgHoraExtra;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;
                        this.Periodo = arrParametros[2];

                        Int32.TryParse(arrParametros[3], out intFlgHoraExtra);
                        this.PeriodoFinal = arrParametros[4];

                        this.IdFilial = Convert.ToInt32(arrParametros[6]);
                        this.IdTipoSolicitacao = Convert.ToInt32(arrParametros[5]);

                        if (this.IdTipoSolicitacao == 9)
                            this.PeriodoFinal = this.Periodo;

                        this.chkHoraExtra.Checked = Convert.ToBoolean(intFlgHoraExtra);

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