using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.Model.Idioma;
using SafWeb.BusinessLayer.Acesso;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Cracha;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.BusinessLayer.Veiculo;
using SafWeb.Model.Colaborador;
using SafWeb.Model.Filial;
using Telerik.WebControls;
using SafWeb.Util.Extension;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class CadEntrada : FWPage
    {
        Filial gobjFilial = new Filial();
        BLFilial gobjBLFilial = new BLFilial();

        /// <history>
        ///     [no history]
        ///     [haguiar_4] 18/01/2011
        ///     adicionar fuso horário ao campo hora
        /// </history>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InicializaScripts();
            
            if (!Page.IsPostBack)
            {
                this.CodSolicitacao = Convert.ToInt32(Request.QueryString["IdSol"].ToString());
                this.CodVisitante = Convert.ToInt32(Request.QueryString["IdVis"].ToString());

                this.DadosBusca += "NumSol=" + Request.QueryString["NumSol"].ToString();
                this.DadosBusca += "&Tipo=" + Request.QueryString["Tipo"].ToString();
                this.DadosBusca += "&NumDoc=" + Request.QueryString["NumDoc"].ToString();
                this.DadosBusca += "&NomVis=" + Request.QueryString["NomVis"].ToString();
                this.DadosBusca += "&NomApr=" + Request.QueryString["NomApr"].ToString();


                //inserir informacoes do fuso horario

                BLColaborador objBlColaborador = new BLColaborador();
                DataTable dtt = new DataTable();

                dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));
                gobjFilial = gobjBLFilial.Obter(Convert.ToInt32(dtt.Rows[0][1].ToString()));
                dtt.Dispose();

                this.Vlr_FusoHorario = gobjFilial.Vlr_FusoHorario;

                //hora da filial
                this.txtDataHoraEntrada.Text = DateTime.UtcNow.AddHours(this.Vlr_FusoHorario).ToString("dd/MM/yyyy HH:mm"); //DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                this.PopulaCombos();
                this.PopulaHistoricoSolicitacao();

            }
        }

        #region ViewState

        //valor do fuso horário da filial
        private double Vlr_FusoHorario
        {
            get
            {
                return Convert.ToDouble(ViewState["vsVlr_FusoHorario"]);
            }
            set
            {
                ViewState["vsVlr_FusoHorario"] = value;
            }
        }

        public DataSet Solicitacoes
        {
            get
            {
                if (ViewState["vsSolicitacoes"] == null)
                {
                    ViewState["vsSolicitacoes"] = null;
                }
                return (DataSet)ViewState["vsSolicitacoes"];
            }
            set
            {
                ViewState["vsSolicitacoes"] = value;
            }
        }

        public int CodSolicitacao
        {
            get
            {
                if (ViewState["vsCodSolicitacao"] == null)
                {
                    ViewState["vsCodSolicitacao"] = 0;
                }
                return (int)ViewState["vsCodSolicitacao"];
            }
            set
            {
                ViewState["vsCodSolicitacao"] = value;
            }
        }

        public int CodVisitante
        {
            get
            {
                if (ViewState["vsCodVisitante"] == null)
                {
                    ViewState["vsCodVisitante"] = 0;
                }
                return (int)ViewState["vsCodVisitante"];
            }
            set
            {
                ViewState["vsCodVisitante"] = value;
            }
        }

        public int CodVisitado
        { 
            get
            {
                if (ViewState["vsCodVisitado"] == null)
                {
                    ViewState["vsCodVisitado"] = 0;
                }
                return (int)ViewState["vsCodVisitado"];
            }
            set
            {
                ViewState["vsCodVisitado"] = value;
            }
        }

        public int CodCracha
        { 
            get
            {
                if (ViewState["vsCodCracha"] == null)
                {
                    ViewState["vsCodCracha"] = 0;
                }
                return (int)ViewState["vsCodCracha"];
            }
            set
            {
                ViewState["vsCodCracha"] = value;
            }
        }

        public int CodFilial
        {
            get
            {
                if (ViewState["vsCodFilial"] == null)
                {
                    ViewState["vsCodFilial"] = 0;
                }
                return (int)ViewState["vsCodFilial"];
            }
            set
            {
                ViewState["vsCodFilial"] = value;
            }
        }

        public int CodRegional
        {
            get
            {
                if (ViewState["vsCodRegional"] == null)
                {
                    ViewState["vsCodRegional"] = 0;
                }
                return (int)ViewState["vsCodRegional"];
            }
            set
            {
                ViewState["vsCodRegional"] = value;
            }
        }

        public string DadosBusca
        {
            get
            {
                if (ViewState["vsDadosBusca"] == null)
                {
                    ViewState["vsDadosBusca"] = string.Empty;
                }
                return (string)ViewState["vsDadosBusca"];
            }
            set
            {
                ViewState["vsDadosBusca"] = value;
            }
        }
        #endregion

        #region Métodos

        #region Inicializar Scripts

        private void InicializaScripts()
        {
            if (HiddenCracha.Value != string.Empty)
            {
                this.txtCracha.Text = HiddenCracha.Value.Split(',').GetValue(0).ToString();
                this.CodCracha = Convert.ToInt32(HiddenCracha.Value.Split(',').GetValue(1));
            }

            this.txtCracha.Attributes.Add("OnKeyPress", "return mascara_Numero(this, event.keyCode);");
            this.txtDataHoraEntrada.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            this.btnHelp.Attributes.Add("OnClick", "AbrirHelpCad();");
        }

        #endregion

        #region RadWindow

        private void AbreRadWindow(int pintCodLista, int pintCodSolicitacao, eListaTipo listTipo)
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.None;

            rwdWindow.Width = Unit.Pixel(530);
            rwdWindow.Height = Unit.Pixel(400);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            switch (listTipo)
            {
                case eListaTipo.Visitantes:
            rwdWindow.Title = "Lista de Visitantes";
            rwdWindow.NavigateUrl = "ListLista.aspx?CodSolicitacao=" + pintCodSolicitacao.ToString() + "&CodLista=" + pintCodLista.ToString();
                    break;
                case eListaTipo.Veiculos:
                    rwdWindow.Title = "Lista de Veículos";
                    rwdWindow.NavigateUrl = "../Aprovacao/ListSolicitacaoListaVeiculos.aspx?CodSolicitacao=" + pintCodSolicitacao.ToString();
                    break;
            }

            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlLista = null;

            //Tenta encontrar na master
            pnlLista = (Panel)this.FindControl("pnlLista");
            pnlLista.Controls.Add(rwmWindowManager);
        }

        private enum eListaTipo
        {
            Visitantes,
            Veiculos
        }

        private void AbreRadWindowCracha()
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.None;

            rwdWindow.Width = Unit.Pixel(525);
            rwdWindow.Height = Unit.Pixel(410);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            //rwdWindow.DestroyOn Close = true;
            rwdWindow.VisibleOnPageLoad = true;
            rwdWindow.Title = "Lista de Crachás";
            rwdWindow.NavigateUrl = "ListCracha.aspx?Reg=" + this.CodRegional.ToString() + "&Fil=" + this.CodFilial.ToString();
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlLista = null;

            //Tenta encontrar na master
            pnlLista = (Panel)this.FindControl("pnlListaCracha");
            pnlLista.Controls.Add(rwmWindowManager);
        }

        #endregion

        #region Historico

        /// <summary>
        ///     Popula data grid de históricos
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar_sdm9004] 05/08/2011 11:41
        ///     exibir placa para colaborador em lista
        ///</history>
        private void PopulaHistoricoSolicitacao()
        {
            BLSolicitacao objBLSolicitacao = null;

            try
            {
                objBLSolicitacao = new BLSolicitacao();

                this.Solicitacoes = objBLSolicitacao.ListarHistoricoSolic(this.CodSolicitacao, 
                                                                          this.CodVisitante);

                if (Solicitacoes.Tables[0].Rows.Count > 0)
                {
                    this.CodVisitado = Convert.ToInt32(Solicitacoes.Tables[0].Rows[0]["Id_Visitado"]);
                    this.txtCodSolic.Text = Solicitacoes.Tables[0].Rows[0]["Id_Solicitacao"].ToString();
                    this.txtNomeVisitado.Text = Solicitacoes.Tables[0].Rows[0]["Nom_Visitado"].ToString();
                    this.txtRE.Text = Solicitacoes.Tables[0].Rows[0]["Re_Visitado"].ToString();
                    this.txtFilial.Text = Solicitacoes.Tables[0].Rows[0]["Alias_Filial"].ToString();
                    this.CodFilial = Convert.ToInt32(Solicitacoes.Tables[0].Rows[0]["Id_Filial"]);
                    this.CodRegional = Convert.ToInt32(Solicitacoes.Tables[0].Rows[0]["Id_Regional"]);
                    this.txtObservacao.Text = Solicitacoes.Tables[0].Rows[0]["Des_ObsSolicitacao"].ToString();
                    this.txtMotivoVisita.Text = Solicitacoes.Tables[0].Rows[0]["Des_MotivoVisita"].ToString();
                    this.txtDataInicio.Text = Solicitacoes.Tables[0].Rows[0]["Dt_InicioVisita"].ToString();
                    this.txtDataFim.Text = Solicitacoes.Tables[0].Rows[0]["Dt_FimVisita"].ToString();
                    this.chkSabado.Checked = (Solicitacoes.Tables[0].Rows[0]["Flg_AcSabado"].ToString() == "1" ? true : false);
                    this.chkDomingo.Checked = (Solicitacoes.Tables[0].Rows[0]["Flg_AcDomingo"].ToString() == "1" ? true : false);
                    this.chkFeriado.Checked = (Solicitacoes.Tables[0].Rows[0]["Flg_AcFeriado"].ToString() == "1" ? true : false);
                    this.txtCargoColaborador.Text = Solicitacoes.Tables[0].Rows[0]["Des_Funcao"].ToString();
                    
                    if (this.Solicitacoes.Tables[0].Rows[0]["Id_Visitante"].ToString() == string.Empty)
                    {
                        //this.lnkVisitante.Visible = true;
                        //this.txtNomeVisitante.Visible = false;
                        //this.lnkVisitante.Text = this.Solicitacoes.Tables[0].Rows[0]["Nom_Visitante"].ToString();

                        BLAcessoVisitante objBLAcessoVisitante = new BLAcessoVisitante();
                        DataTable dttVisitante;

                        dttVisitante = objBLAcessoVisitante.ObterVisitante(Convert.ToInt32(this.CodVisitante));

                        this.lnkVisitante.Visible = false;
                        this.txtNomeVisitante.Visible = true;
                        this.txtNomeVisitante.Text = Request.QueryString["NomVis"].ToString();

                        if (dttVisitante.Rows.Count > 0)
                        {
                            this.txtTipoDocumento.Text = dttVisitante.Rows[0]["Des_DocumentoTipo"].ToString();
                            this.txtDocumento.Text = dttVisitante.Rows[0]["Num_Documento"].ToString();
                            this.txtTipoVisitante.Text = dttVisitante.Rows[0]["DES_TIPOCOLABORADOR"].ToString();

                            imgFoto.ImageUrl = "ImageFoto.aspx?ImageByte=" + this.CodVisitante + "&idTipoVisitante=" + dttVisitante.Rows[0]["ID_TIPOCOLABORADOR"].ToString();
                            imgFoto.Visible = true;
                            if (dttVisitante.Rows[0]["ID_TIPOCOLABORADOR"].ToString() == "3") // Fotografar somente visitantes que não são funcionários ou terceirizados
                                btnFoto.Visible = true;
                        }
                        
                        this.txtEmpresa.Text = Request.QueryString["NomEmp"].ToString();

                        if (this.Solicitacoes.Tables[0].Columns.IndexOf("Des_Placa") > 0)
                        {
                            if (!string.IsNullOrEmpty(this.Solicitacoes.Tables[0].Rows[0]["Des_Placa"].ToString()))
                                this.txtPlaca.Text = this.Solicitacoes.Tables[0].Rows[0]["Des_Placa"].ToString();
                        }
                    }
                    else
                    {
                        this.lnkVisitante.Visible = false;
                        this.txtNomeVisitante.Visible = true;
                        this.txtNomeVisitante.Text = this.Solicitacoes.Tables[0].Rows[0]["Nom_Visitante"].ToString();

                        this.txtTipoDocumento.Text = this.Solicitacoes.Tables[0].Rows[0]["TipDoc_Visitante"].ToString();
                        this.txtDocumento.Text = this.Solicitacoes.Tables[0].Rows[0]["Doc_Visitante"].ToString();
                        this.txtEmpresa.Text = this.Solicitacoes.Tables[0].Rows[0]["Emp_Visitante"].ToString();
                        this.txtTipoVisitante.Text = this.Solicitacoes.Tables[0].Rows[0]["Tip_Visitante"].ToString();
                        this.txtPlaca.Text = this.Solicitacoes.Tables[0].Rows[0]["Des_Placa"].ToString();

                        imgFoto.ImageUrl = "ImageFoto.aspx?ImageByte=" + this.CodVisitante + "&idTipoVisitante=" + this.Solicitacoes.Tables[0].Rows[0]["ID_TIPOCOLABORADOR"].ToString();
                        imgFoto.Visible = true;
                        if (this.Solicitacoes.Tables[0].Rows[0]["ID_TIPOCOLABORADOR"].ToString() == "3") // Fotografar somente visitantes que não são funcionários ou terceirizados
                            btnFoto.Visible = true;
                    }

                    txtPlaca.Visible = true;
                    lnkPlaca.Visible = false;
                    if (string.IsNullOrEmpty(Solicitacoes.Tables[0].Rows[0]["Des_Placa"].ToString()) && Solicitacoes.Tables[4].Rows.Count > 0)
                    {
                        txtPlaca.Visible = false;
                        lnkPlaca.Visible = true;
                        lnkPlaca.Text = Solicitacoes.Tables[4].Rows[0]["Des_VeiculoLista"].ToString();
                    }

                    if (this.Solicitacoes.Tables[0].Rows[0]["Id_Cracha"].ToString() != string.Empty)
                    {
                        this.txtCracha.Text = this.Solicitacoes.Tables[0].Rows[0]["Num_Cracha"].ToString();
                        this.CodCracha = Convert.ToInt32(this.Solicitacoes.Tables[0].Rows[0]["Id_Cracha"]);
                        this.txtCracha.Enabled = false;
                        this.btnBuscaCracha.Enabled = false;
                        this.chkSemCracha.Enabled = false;
                    }
                    else
                    {
                        BLCracha objBlCracha = new BLCracha();

                        try
                        {
                            this.txtCracha.Text = objBlCracha.ObterNumeroCracha(this.CodFilial);
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }

                    if (Solicitacoes.Tables[4].Rows.Count > 0)
                    {
                        this.ddlPlaca.Items.Clear();

                        this.ddlPlaca.DataSource = Solicitacoes.Tables[4];
                        this.ddlPlaca.DataTextField = "Des_Placa";
                        this.ddlPlaca.DataValueField = "id_veiculo";
                        this.ddlPlaca.DataBind();

                        this.ddlPlaca.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), ""));
                        this.ddlPlaca.Enabled = true;
                        this.ddlEstado.SelectedIndex = 0;
                        this.ddlEstado.Enabled = false;

                    }
                }

                if (Solicitacoes.Tables[3].Rows.Count > 0)
                {
                    this.txtObservacaoReprovacao.Text = this.Solicitacoes.Tables[3].Rows[0]["Des_MotivoReprovacao"].ToString();
                }
                this.pnlObservacao.Visible = (this.txtObservacaoReprovacao.Text != "" ? true : false);

                if (this.Solicitacoes.Tables[1].Rows.Count > 0)
                {
                    this.lstAreaVisita.DataTextField = "Des_Area";
                    this.lstAreaVisita.DataValueField = "Id_Area";
                    this.lstAreaVisita.DataSource = this.Solicitacoes.Tables[1];
                    this.lstAreaVisita.DataBind();
                }

                if (this.Solicitacoes.Tables[2].Rows.Count > 0)
                {
                this.radSolicitacoes.DataSource = this.Solicitacoes.Tables[2];
                    this.radSolicitacoes.DataBind();
            }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Popular Combo

        private void PopulaCombos()
        {
            this.PopulaComboEstado();
            this.PopulaComboPlaca(Convert.ToInt32(ddlEstado.SelectedValue));
        }

        private void PopulaComboEstado()
        {
            BLVeiculo objBlVeiculo = null;
            Collection<SafWeb.Model.Veiculo.Estado> colEstado = null;

            try
            {
                objBlVeiculo = new BLVeiculo();

                colEstado = objBlVeiculo.ListarEstado();

                this.ddlEstado.DataSource = colEstado;
                this.ddlEstado.DataTextField = "DescricaoEstado";
                this.ddlEstado.DataValueField = "Codigo";
                this.ddlEstado.DataBind();

                this.ddlEstado.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "-1"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaComboPlaca(int pintCodEstado)
        {
            BLVeiculo objBlVeiculo = null;
            Collection<SafWeb.Model.Veiculo.Veiculo> colVeiculo = null;

            try
            {
                if (pintCodEstado > 0)
                {
                    objBlVeiculo = new BLVeiculo();

                    colVeiculo = objBlVeiculo.ListarVeiculo(pintCodEstado);
                }
                else
                    colVeiculo = new Collection<SafWeb.Model.Veiculo.Veiculo>();

                this.ddlPlaca.DataSource = colVeiculo;
                this.ddlPlaca.DataTextField = "DescricaoPlaca";
                this.ddlPlaca.DataValueField = "Codigo";
                this.ddlPlaca.DataBind();

                this.ddlPlaca.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "-1"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Gravar

        private bool GravarEntrada()
        {
            bool blnRetorno = false;
            BLAcessoVisitante objBLAcessoVisitante = null;
            Model.Acesso.AcessoVisitante objAcesso = null;

            try
            {
                objBLAcessoVisitante = new BLAcessoVisitante();
                objAcesso = new Model.Acesso.AcessoVisitante();

                objAcesso.CodSolicitacao = this.CodSolicitacao;
                objAcesso.CodVisitante = this.CodVisitante;
                objAcesso.Entrada = Convert.ToDateTime(this.txtDataHoraEntrada.Text);
                objAcesso.ValidadeVisita = Convert.ToDateTime(this.txtDataFim.Text);
                objAcesso.CodAcompanhante = this.CodVisitado;
                if(!this.chkSemCracha.Checked)
                    objAcesso.CodCracha = this.CodCracha;
                if(Convert.ToInt32(this.ddlPlaca.SelectedValue) > 0)
                    objAcesso.CodVeiculo = Convert.ToInt32(this.ddlPlaca.SelectedValue);
                objAcesso.CodUsuLibEnt = BLAcesso.IdUsuarioLogado();
                objAcesso.ObsVisita = this.txtObsEntrada.Text;

                if (objBLAcessoVisitante.Inserir(objAcesso) > 0)
                    blnRetorno = true;
            }
            catch(Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            return blnRetorno;
        }

        #endregion

        #endregion

        #region Eventos

        #region RadGrid

        protected void radSolicitacoes_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                this.radSolicitacoes.DataSource = Solicitacoes.Tables[2];
            }
        }

        protected void radSolicitacoes_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
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
                    catch(Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
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

        #region LinkButton

        protected void lnkVisitante_Click(object sender, EventArgs e)
        {
            this.AbreRadWindow(Convert.ToInt32(Solicitacoes.Tables[0].Rows[0]["Id_ColaboradorLista"].ToString()), Convert.ToInt32(txtCodSolic.Text), eListaTipo.Visitantes);
        }

        protected void lnkPlaca_Click(object sender, EventArgs e)
        {
            AbreRadWindow(0, txtCodSolic.Text.ToInt32(), eListaTipo.Veiculos);
        }

        #endregion

        #region Combo

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulaComboPlaca(Convert.ToInt32(ddlEstado.SelectedValue));
        }

        #endregion 

        #region Botões

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            /*
            * No retorno não aplica filtro
            * by [abranco] 23/06/2010
            */
            //Response.Redirect("ListAgendados.aspx?" + this.DadosBusca);            
            Response.Redirect("ListAgendados.aspx");            
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            bool blnGravar = true;

            if (chkSemCracha.Checked)
            {
                if (ddlPlaca.SelectedIndex == 0)
                {
                    RadAjaxPanel1.Alert("Selecione uma placa.");
                    blnGravar = false;
                }
            }
            else
            {
                if (txtCracha.Text == string.Empty)
                {
                    RadAjaxPanel1.Alert("Insira um número de crachá.");
                    blnGravar = false;
                }
                else
                {
                    if (this.CodCracha == 0)
                    {
                        BLCracha objBlCracha;                        
                        
                        try
                        {
                            objBlCracha = new BLCracha();

                            this.CodCracha = objBlCracha.ObterIdCracha(txtCracha.Text.Trim());

                            if (this.CodCracha == 0)
                            {
                                RadAjaxPanel1.Alert("Crachá não cadastrado. Certifique-se que o número foi digitado corretamente e se for o caso, cadastre-o no menu Portaria / Crachás.");
                                blnGravar = false;
                            }
                            else
                            {
                                if (this.CodCracha < 0)
                                {
                                    BLColaborador objBlColaborador = new BLColaborador();
                                    Colaborador objColaborador = new Colaborador();

                                    try
                                    {
                                        objColaborador = objBlColaborador.Obter(this.CodCracha * -1);
                                        RadAjaxPanel1.Alert("O crachá informado está em uso pelo visitante " + objColaborador.NomeColaborador + ".");
                                    }
                                    catch (Exception ex)
                                    {
                                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                                    }
                                    
                                    blnGravar = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                            blnGravar = false;
                        }
                    }                    
                }
            }

            try
            {
                if (txtDataHoraEntrada.Text.Length == 16)
                {
                    DateTime.Parse(txtDataHoraEntrada.Text);
                }
                else
                {
                    RadAjaxPanel1.Alert("Insira Data e Hora.");
                }                         
            }
            catch (Exception ex)
            {
                blnGravar = false;
                RadAjaxPanel1.Alert("Data Inválida.");
            }

            if (blnGravar)
            {
                DateTime datEntrada = Convert.ToDateTime(txtDataHoraEntrada.Text),
                         datInicio = Convert.ToDateTime(txtDataInicio.Text),
                         datFim = Convert.ToDateTime(txtDataFim.Text);

                //verifica se a data de entrada está no período
                if (datInicio.Date <= datEntrada.Date && datFim.Date >= datEntrada.Date)
                {
                    //verifica checkbox 
                    if (this.VerificarCheckbox())
                    {
                        //se a hora entrada for é menor que a hora de inicio
                        if (datEntrada.Hour < datInicio.Hour)
                        {
                            RadAjaxPanel1.Alert("A Hora de entrada está forá do período.");
                        }
                        //se a hora entrada for é maior que a hora final
                        else if (datEntrada.Hour > datFim.Hour)
                        {
                            RadAjaxPanel1.Alert("A Hora de entrada está forá do período.");
                        }
                        //se a data de entrada for igual a hora de inicio verifica os minutos
                        else if (datEntrada.Hour == datInicio.Hour && datEntrada.Minute < datInicio.Minute)
                        {
                            RadAjaxPanel1.Alert("A Hora de entrada está forá do período.");
                        }
                        //se a data de entrada for igual a hora final verifica os minutos
                        else if (datEntrada.Hour == datFim.Hour && datEntrada.Minute > datFim.Minute)
                        {
                            RadAjaxPanel1.Alert("A Hora de entrada está forá do período.");
                        }
                        else
                        {
                            if (GravarEntrada())
                            {
                                /*
                                 * No retorno não aplica filtro
                                 * by [abranco] 23/06/2010
                                 */
                                //Response.Redirect("ListAgendados.aspx?Gravar=true&" + this.DadosBusca);
                                Response.Redirect("ListAgendados.aspx?Gravar=true");
                            }

                        }
                    }
                }
                else
                {
                    RadAjaxPanel1.Alert("A Data de entrada está forá do período.");
                }
            }
            else
            {
                this.CodCracha = 0;
            }
        }

        protected void btnBuscaCracha_Click(object sender, ImageClickEventArgs e)
        {
            this.AbreRadWindowCracha();
        }

        #endregion

        #region Verificar CheckBox

        /// <summary>
        ///     Verifica se os checkbox foram selecionados corretamente
        /// </summary>
        /// <param name="pdatInicio">Data de Início</param>
        /// <param name="pdatFim">Data de Término</param>
        /// <returns>True/False</returns>
        /// <history>
        ///     [mribeiro] created 17/07/2009
        /// </history>
        protected bool VerificarCheckbox()
        {
            DateTime datEntrada;

            datEntrada = Convert.ToDateTime(txtDataHoraEntrada.Text);

            bool blnFer = false, blnSab = false, blnDom = false, blnGravar = true;

            DataTable dttFeriados = new DataTable();

            switch (datEntrada.DayOfWeek)
            {
                case DayOfWeek.Sunday: blnDom = true; break;
                case DayOfWeek.Saturday: blnSab = true; break;
            }

            try
            {
                BLSolicitacao objBlSolicitacao = new BLSolicitacao();

                dttFeriados = objBlSolicitacao.ListarFeriado(this.CodRegional,
                                                             this.CodFilial);

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            //verifica se é feriado
            foreach (DataRow dtr in dttFeriados.Rows)
            {
                if (dtr["Flg_fixo"].ToString() == "1")
                {
                    if (Convert.ToDateTime(dtr["Dt_Data"]).ToString("dd/MM") == datEntrada.ToString("dd/MM"))
                    {
                        blnFer = true;
                        break;
                    }
                }
                else if (dtr["Flg_fixo"].ToString() == "0")
                {
                    if (Convert.ToDateTime(dtr["Dt_Data"]) == datEntrada)
                    {
                        blnFer = true;
                        break;
                    }
                }
            }


            if (blnDom)
            {
                if (chkDomingo.Checked == false)
                {
                    RadAjaxPanel1.Alert("Entrada não permitida para domingo.");
                    blnGravar = false;
                }
            }

            if (blnSab)
            {
                if (chkSabado.Checked == false)
                {
                    RadAjaxPanel1.Alert("Entrada não permitida para sábado.");
                    blnGravar = false;
                }
            }

            if (blnFer)
            {
                if (chkFeriado.Checked == false)
                {
                    RadAjaxPanel1.Alert("Entrada não permitida para feriados.");
                    blnGravar = false;
                }
            }

            return blnGravar;
        }

        #endregion

        #region CheckBox

        protected void chkSemCracha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSemCracha.Checked)
            {
                txtCracha.Enabled = false;
                txtCracha.Text = string.Empty;
                btnBuscaCracha.Enabled = false;                
            }
            else
            {
                txtCracha.Enabled = true;
                btnBuscaCracha.Enabled = true;
            }
        }

        #endregion

        protected void btnFoto_Click(object sender, EventArgs e)
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.Close;

            rwdWindow.Width = Unit.Pixel(630);
            rwdWindow.Height = Unit.Pixel(384);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Fotografar Visitante";

            rwdWindow.NavigateUrl = "CapturarFoto.aspx?idVisitante=" + this.CodVisitante + "&FusoHorario=" + this.Vlr_FusoHorario;

            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlColaborador = null;

            //Tenta encontrar na master
            pnlColaborador = (Panel)this.FindControl("pnlColaborador");
            pnlColaborador.Controls.Add(rwmWindowManager);

        }

        #endregion
    }
}

