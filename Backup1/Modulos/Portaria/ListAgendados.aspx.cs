using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Idioma;
using FrameWork.Model.Usuarios;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Acesso;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Empresa;
using SafWeb.BusinessLayer.Escala;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.Model.Acesso;
using SafWeb.Model.Colaborador;
using SafWeb.Model.Filial;
using SafWeb.Model.Solicitacao;
using Telerik.WebControls;
using System.Globalization;
using System.Web;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class ListAgendados : FWPage
    {
        Usuario gobjUsuario = new Usuario();
        BLAcesso gobjBLAcesso = new BLAcesso();

        Filial gobjFilial = new Filial();
        BLFilial gobjBLFilial = new BLFilial();

        #region InicializaScripts
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método Inicializa Scripts
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 05/04/2010 Created 
        ///     [haguiar] 06/04/2011 16:50 modify
        ///     modificar para verificar colaborador em contingencia
        ///     [haguiar] modify 27/04/2011 10:49
        ///     nao exibir aba de contingencia quando houver controle de acesso para filial.
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void InicializaScripts()
        {
            if (hdEscalaDep.Text != string.Empty)
            {
                ddlEscalaDep.SelectedValue = hdEscalaDep.Text;
            }

            //Obtém Usuário logado
            //gobjUsuario = BLAcesso.ObterUsuario();

            //Obter Filial do Usuário logado
            this.ObterFilial();

            if (gobjFilial != null)
            {
                if (!gobjFilial.FlgContrAcessoOnline)
                {
                    btnAbaContingencia.Visible = false;
                    imgAbaContingencia.Visible = false;
                }
            }

            
            if (!CadastroContingencia)
            {
                this.btnHelp.Attributes.Add("OnClick", "AbrirHelpList();");

                if (txtHiddenColaboradoresMensagem.Value != string.Empty)
                {
                    lblMensagemEscalacao.Text = txtHiddenColaboradoresMensagem.Value;

                    Bind();

                    txtHiddenColaboradoresMensagem.Value = string.Empty;
                }
                else
                {
                    lblMensagemEscalacao.Text = string.Empty;
                }
            }
            else
            {
                btnGravarContingencia.Enabled = true;

                //contingencia
                if (txtHiddenColaboradores.Value != string.Empty)
                {
                    ObterColaborador(Convert.ToInt32(txtHiddenColaboradores.Value));

                    if (gobjColaborador != null)
                    {
                        this.txtNomeColaborador_Conting.Text = gobjColaborador.NomeColaborador;
                        this.txtREColaborador_Conting.Text = gobjColaborador.CodigoColaborador;

                        imgFoto.ImageUrl = "ImageFoto.aspx?ImageByte=" + gobjColaborador.IdColaborador;
                        imgFoto.Visible = true;

                        this.txtCargoColaborador.Text = gobjColaborador.Des_Funcao;

                        Id_Colaborador = gobjColaborador.IdColaborador;

                        this.lstColaboradores_Conting.Enabled = false;
                        this.chkSelTodos_Conting.Enabled = false;
                        this.chkSelTodos_Conting.Checked = false;
                        this.ddlEscalaDpto_Conting.Enabled = false;
                        this.ddlEscalaDpto_Conting.SelectedIndex = 0;
                        this.lstColaboradores_Conting.Items.Clear();

                        btnLimparColaborador_Conting.Visible = true;
                    }
                }
            }
        }
        #endregion

        ///     [haguiar] modify 26/04/2011
        ///     obter filial logado em inicializarscripts
        protected void Page_Load(object sender, EventArgs e)
        {
            //Console.WriteLine(BLAcesso.IdUsuarioLogado());

            InicializaScripts();

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Gravar"] != null)
                {
                    if (Request.QueryString["Gravar"].ToString() == "true")
                    {
                        RadAjaxPanel1.Alert("Entrada efetuada com sucesso.");
                    }
                }
                if (Request.QueryString["NumSol"] != null)
                {
                    txtNumSolicitacao.Text = Request.QueryString["NumSol"].ToString();
                }
                if (Request.QueryString["Tipo"] != null)
                {
                    if (Request.QueryString["Tipo"].ToString() != "-1")
                    {
                        BLUtilitarios.ConsultarValorCombo(ref ddlTipoDocumento, Request.QueryString["Tipo"].ToString());
                    }
                }
                if (Request.QueryString["NumDoc"] != null)
                {
                    txtDocumento.Text = Request.QueryString["NumDoc"].ToString();
                }
                if (Request.QueryString["NomVis"] != null)
                {
                    txtVisitante.Text = Request.QueryString["NomVis"].ToString();
                }
                if (Request.QueryString["NomApr"] != null)
                {
                    txtAprovador.Text = Request.QueryString["NomApr"].ToString();
                }

                this.PopulaCombos();

                this.ControlaPaineis(Enums.TipoPainel.Cadastro);

                //Bind Escala dos Colaboradores
                //this.Bind(Enums.TipoBind.DataBind);

            }
            else
            {

            }
        }

        #region Controla Paineis

        /// <summary>
        ///     Controlar painel para terceira aba
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modify 06/04/2011 09:42
        ///</history> 
        private void ControlaPaineis(Enums.TipoPainel pintPainel)
        {
            if (pintPainel == Enums.TipoPainel.Listagem)
            {
                this.CadastroContingencia = false;

                divAbaColaboradores.Visible = true;
                divAbaVisitantes.Visible = false;
                divAbaContingencia.Visible = false;
                txtHiddenColaboradores.Value = string.Empty;

                btnAbaColaboradores.CssClass = "cadbuttonAbaAtiva";
                btnAbaVisitantes.CssClass = "cadbuttonAbaInativa";
                btnAbaContingencia.CssClass = "cadbuttonAbaInativa";

                imgAbaColaboradores.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaVisitantes.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaContingencia.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";

                this.Bind();
                LimpaControles();
            }
            else if (pintPainel == Enums.TipoPainel.Cadastro)
            {
                this.PopulaRadGridVisAgendadas(Enums.TipoBind.DataBind);
                this.CadastroContingencia = false;

                divAbaVisitantes.Visible = true;
                divAbaColaboradores.Visible = false;
                divAbaContingencia.Visible = false;
                txtHiddenColaboradores.Value = string.Empty;

                btnAbaVisitantes.CssClass = "cadbuttonAbaAtiva";
                btnAbaColaboradores.CssClass = "cadbuttonAbaInativa";
                btnAbaContingencia.CssClass = "cadbuttonAbaInativa";

                imgAbaVisitantes.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaColaboradores.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaContingencia.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
            }
            else
            {
                // painel de contingencia
                this.CadastroContingencia = true;
                
                divAbaContingencia.Visible = true;
                divAbaVisitantes.Visible = false;
                divAbaColaboradores.Visible = false;
                txtHiddenColaboradores.Value = string.Empty;

                btnAbaContingencia.CssClass = "cadbuttonAbaAtiva";
                btnAbaVisitantes.CssClass = "cadbuttonAbaInativa";
                btnAbaColaboradores.CssClass = "cadbuttonAbaInativa";

                imgAbaContingencia.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaColaboradores.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaVisitantes.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";

                //limpa campos
                this.Id_Colaborador = 0;
                this.txtREColaborador_Conting.Text = string.Empty;
                //this.txtREAprovador_Conting.Text = string.Empty;
                this.txtMotivoLiberacao_Conting.Text = string.Empty;

                this.txtNomeColaborador_Conting.Text = string.Empty;
                this.txtCargoColaborador.Text = string.Empty;

                imgFoto.ImageUrl = "";
                imgFoto.Visible = false;

                ddlAprovador_Conting.SelectedIndex = 0;
            }

        }

        private void LimpaControles()
        {
            txtNumeroDocumento.Text = string.Empty;
            //txtFilialColaborador.Text = string.Empty;
            txtNomeColaborador.Text = string.Empty;
            txtEscalaColaborador.Text = string.Empty;
            txtHoraInicio.Text = string.Empty;
            txtHoraFim.Text = string.Empty;
            ddlEscalaDep.SelectedIndex = 0;
            hdEscalaDep.Text = ddlEscalaDep.SelectedValue;
        }

        #endregion

        #region Escala Visitantes

        #region Métodos

        private void PopulaRadGridVisAgendadas(Enums.TipoBind pintTipoBind)
        {
            BLAcessoVisitante objBLAcesso = null;

            try
            {
                objBLAcesso = new BLAcessoVisitante();

                this.radVisAgendados.DataSource = objBLAcesso.ListarVisitasAgendadas(Convert.ToInt32(this.ddlTipoDocumento.SelectedValue),
                                                                                     this.txtDocumento.Text,
                                                                                     this.txtVisitante.Text,
                                                                                     this.txtAprovador.Text,
                                                                                     Convert.ToInt32(BLAcesso.IdUsuarioLogado()),
                                                                                     (txtNumSolicitacao.Text == string.Empty ? 0 : Convert.ToInt32(txtNumSolicitacao.Text)),
                                                                                     Convert.ToInt32(this.ddlEmpresa.SelectedValue));
                if (pintTipoBind == Enums.TipoBind.DataBind)
                    this.radVisAgendados.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar] modify 08/04/2011
        ///     armazena idRegional do usuário logado.
        ///     popular combo de aprovadores de contingencia
        ///</history>
        private void PopulaCombos()
        {
            this.PopulaComboRegional();

            //seleciona a regional e a filial odo usuário logado.
            BLColaborador objBlColaborador = new BLColaborador();
            DataTable dtt = new DataTable();

            try
            {
                dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                this.IdFilial = Convert.ToInt32(dtt.Rows[0][1]);
                this.IdRegional = Convert.ToInt32(dtt.Rows[0][0]);

                BLUtilitarios.ConsultarValorCombo(ref ddlRegional, dtt.Rows[0][0].ToString());
                this.PopulaComboFilial(Convert.ToInt32(this.ddlRegional.SelectedValue));
                BLUtilitarios.ConsultarValorCombo(ref ddlFilial, dtt.Rows[0][1].ToString());
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            this.PopulaComboTipoDocumento();
            this.PopularComboEmpresa();
            this.PopularComboEscalaDpto();
            this.PopularAprovadoresContingencia();
        }

        private void PopulaComboRegional()
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

                this.ddlRegional.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaComboFilial(int pintCodRegional)
        {
            BLFilial objBlFilial = null;
            Collection<SafWeb.Model.Filial.Filial> colFilial = null;

            try
            {
                objBlFilial = new BLFilial();

                if (pintCodRegional > 0)
                {
                    colFilial = objBlFilial.Listar(pintCodRegional);
                }

                this.ddlFilial.Items.Clear();

                this.ddlFilial.DataSource = colFilial;
                this.ddlFilial.DataTextField = "AliasFilial";
                this.ddlFilial.DataValueField = "IdFilial";
                this.ddlFilial.DataBind();

                this.ddlFilial.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "0"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        private void PopulaComboTipoDocumento()
        {
            BLColaborador objBlColaborador = null;
            Collection<SafWeb.Model.Colaborador.TipoDocumento> colTipoDocumento = null;

            try
            {
                objBlColaborador = new BLColaborador();

                colTipoDocumento = objBlColaborador.ListarTipoDocumento();

                this.ddlTipoDocumento.DataSource = colTipoDocumento;
                this.ddlTipoDocumento.DataTextField = "DescricaoDocumento";
                this.ddlTipoDocumento.DataValueField = "IdTipoDocumento";
                this.ddlTipoDocumento.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoDocumento, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, -1);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoDocumento, "RE", true, 0);
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
        ///     [mribeiro] created 15/10/2009 
        ///</history>
        protected void PopularComboEmpresa()
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

        #endregion

        #region Eventos

        #region Botões

        /// <summary>
        ///     Remover colaborador
        /// </summary>
        /// <history>
        ///     [haguiar] created 30/08/2011 18:34
        ///</history>        
        protected void btnLimparColaborador_Conting_Click(object sender, EventArgs e)
        {
            this.btnLimparColaborador_Conting.Visible = false;

            this.txtNomeColaborador_Conting.Text = string.Empty;
            this.txtREColaborador_Conting.Text = string.Empty;
            this.txtCargoColaborador.Text = string.Empty;

            imgFoto.ImageUrl = "";
            imgFoto.Visible = false;

            this.Id_Colaborador = 0;
            txtHiddenColaboradores.Value = string.Empty;

            this.lstColaboradores_Conting.Enabled = true;
            this.chkSelTodos_Conting.Enabled = true;
            this.ddlEscalaDpto_Conting.Enabled = true;

            imgFoto.ImageUrl = "";
            imgFoto.Visible = false; 
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.PopulaRadGridVisAgendadas(Enums.TipoBind.DataBind);
        }
        protected void btnAbaVisitantes_Click(object sender, EventArgs e)
        {
            ControlaPaineis(Enums.TipoPainel.Cadastro);
        }

        protected void btnAbaColaboradores_Click(object sender, EventArgs e)
        {
            ControlaPaineis(Enums.TipoPainel.Listagem);
        }


        #endregion





        #region Combo

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtDocumento.Text = string.Empty;

            if (this.ddlTipoDocumento.SelectedItem.Text.Trim() == "Passaporte")
            {
                this.txtDocumento.Enabled = true;
                this.txtDocumento.MaxLength = 9;
                this.txtDocumento.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NENHUMA;
            }
            else if (this.ddlTipoDocumento.SelectedItem.Text.Trim() == "RG")
            {
                this.txtDocumento.Enabled = true;
                this.txtDocumento.MaxLength = 9;
                this.txtDocumento.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NUMERO;
            }
            else if (this.ddlTipoDocumento.SelectedItem.Text.Trim() == "RE")
            {
                this.txtDocumento.Enabled = true;
                this.txtDocumento.MaxLength = 12;
                this.txtDocumento.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NUMERO;
            }
            else
            {
                this.txtDocumento.Enabled = false;
            }
        }

        #endregion

        #region RadGrid

        protected void radVisAgendados_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            if (this.Page.IsPostBack)
                this.PopulaRadGridVisAgendadas(Enums.TipoBind.SemDataBind);
        }

        /// <summary>
        ///     Item selecionado
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modify 01/11/2010
        ///     [haguiar_4] modify 18/01/2010
        ///     adicionar fuso horário
        ///</history>
        protected void radVisAgendados_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            try
            {
                TimeSpan ts;

                if (e.CommandName.Trim() == "Selecionar")
                {
                    bool blnHorarioVisita;


                    //if (!Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Situacao")))
                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    if (((ImageButton)dataItem["Ativar"].Controls[0]).ToolTip.Equals("Ativar"))
                    {
                        RadAjaxPanel1.Alert("Visitante removido da lista ou inativado, acesso bloqueado!");
                    }
                    else
                    {
                    /*
                    //Obter Filial do Usuário logado
                    BLColaborador objBlColaborador = new BLColaborador();
                    DataTable dtt = new DataTable();

                    dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));
                    gobjFilial = gobjBLFilial.Obter(Convert.ToInt32(dtt.Rows[0][1].ToString()));
                    dtt.Dispose();
                    */

                    //DateTime datHorarioEntradaSaida = Convert.ToDateTime(e.Item.Cells[7].Text);

                    DateTime dtInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(e.Item.Cells[7].Text.Substring(11, 2)), Convert.ToInt32(e.Item.Cells[7].Text.Substring(14, 2)), 0);
                    //DateTime dtFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(e.Item.Cells[8].Text.Substring(11, 2)), Convert.ToInt32(e.Item.Cells[8].Text.Substring(14, 2)), 0);

                    DateTime dtFinal = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second).AddHours(this.Vlr_FusoHorario);
                    
                    //inicio
                    ts = dtFinal - dtInicio;

                    if (ts.Hours >= 0 && ts.Minutes >= 0 && ts.Seconds >= 0)
                    //if (!(datHorarioEntradaSaida > DateTime.UtcNow.AddHours(this.Vlr_FusoHorario)))
                    {
                        //já passou do horário

                        blnHorarioVisita = true;
                    }
                    else
                    {
                        //ainda não chegou no horário

                        blnHorarioVisita = false;
                        RadAjaxPanel1.Alert("O horário de liberação para esta visita é a partir de: " + dtInicio.ToShortTimeString() + " horas.");
                    }

                    if (blnHorarioVisita)
                    {
                        //verifica se a solicitação já não possui uma entrada sem saída
                        BLAcessoVisitante objBlAcessoVisitante = new BLAcessoVisitante();

                        if (objBlAcessoVisitante.VerificarEntrada(Convert.ToInt32(e.Item.Cells[3].Text)))
                        {
                            RadAjaxPanel1.Alert("Esse visitante já possui uma entrada em aberto.");
                        }
                        else
                        {
                            Response.Redirect("CadEntrada.aspx?IdSol=" + e.Item.Cells[2].Text + "&IdVis=" + e.Item.Cells[3].Text + "&NumSol=" + txtNumSolicitacao.Text + "&Tipo=" + ddlTipoDocumento.SelectedValue.ToString() + "&NumDoc=" + txtDocumento.Text + "&NomVis=" + e.Item.Cells[4].Text + "&NomApr=" + txtAprovador.Text + "&NomEmp=" + e.Item.Cells[5].Text);
                        }
                    }
                }
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
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void radVisAgendados_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            ImageButton imgSelecionar;

            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
            {
                try
                {
                    imgSelecionar = (ImageButton)(e.Item.FindControl("imgSelecionar"));
                    imgSelecionar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    // Cursor 
                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 2; @int++)
                    {
                        e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(imgSelecionar, ""));
                    }

                    e.Item.ToolTip = "Selecionar";

                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    Image btnAtivar = (Image)dataItem["Ativar"].Controls[0];

                    if ((btnAtivar != null))
                    {
                        if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Situacao")))
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                            //btnAtivar.ToolTip = "Inativar";
                }
                        else
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                            //btnAtivar.ToolTip = "Ativar";
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

        protected void btnAbaPendente_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

        #region Escala Colaboradores

        #region Bind
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método Bind para carregar o radgrid dos colaboradores
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 05/04/2010 Created 
        ///     [haguiar] modify 28/10/2010
        ///     [haguiar] modify 26/04/2011
        ///     obter usuario logado em InicializarScripts
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void Bind()
        {
            try
            {
                    this.radGridColaborador.DataBind();
                }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

        }

        #endregion

        #region Botões
        protected void btnBuscarCol_Click(object sender, EventArgs e)
        {
            this.Bind();
        }
        #endregion

        #region Ordenar Paginação Grid
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Ordena a paginação do RadGrid de acordo com a hora do sistema
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 9/04/2010 Created 
        ///     [haguiar_2] 14/12/2010 modify
        ///     verificar horarios e nao afastamentos
        ///     [haguiar_4] 21/01/2011 modify
        ///     implementar fuso horário
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void OrdenarPaginacaoGrid(Collection<ListaAcessoColaborador> pcolListaAcessoColaborador)
        {
            int intIndex = 0;
            int intHoraEscala = 0;
            int intCurrentPageIndex = 0;

            //Para cada Escala, verifica se a hora de entrada é menor ou igual à hora atual
            foreach (ListaAcessoColaborador objListaAcessoColaborador in pcolListaAcessoColaborador)
            {

                bool blnAfastamento = false;
                switch (objListaAcessoColaborador.HoraEscalacao)
                {
                    case "COMPENSADO":
                        blnAfastamento = true;
                        break;

                    case "FOLGA/DSR":
                        blnAfastamento = true;
                        break;

                    case "FÉRIAS/LICENÇA":
                        blnAfastamento = true;
                        break;
                }


                if (!blnAfastamento)
                {
                    intHoraEscala = Convert.ToInt32(objListaAcessoColaborador.HoraEscalacao.Substring(0, 2).Replace(":",""));

                    //if (DateTime.Now.Hour >= intHoraEscala)
                    if (DateTime.UtcNow.AddHours(this.Vlr_FusoHorario).Hour >= intHoraEscala)
                    {
                        intIndex += 1;
                    }
                    
                    else
                    {
                        //Obtém o nº da página em que o grid estará setado 
                        intCurrentPageIndex = Convert.ToInt32(intIndex / this.radGridColaborador.PageSize);
                        break;
                    }
                    
                }
            }

            if (Page.IsPostBack)
            {
                intCurrentPageIndex = this.radGridColaborador.CurrentPageIndex;
            }

            radGridColaborador.CurrentPageIndex = BLUtilitarios.ObterPageIndiceGrid(intCurrentPageIndex,
                                                                               this.radGridColaborador.PageCount,
                                                                               pcolListaAcessoColaborador.Count,
                                                                               this.radGridColaborador.PageSize);
        }
        #endregion

        #region Popula Combo Escala Departamental
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método para popular o combo de escalas departamentais
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 06/04/2010 Created 
        ///     [haguiar] modify 28/10/2010
        ///     [haguiar_3] modify 07/01/2011
        ///     [haguiar_SDM9004] 28/08/2011 17:45
        ///     listar escalas crew
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void PopularComboEscalaDpto()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            DataTable dtt = new DataTable();

            //dtt = objBLEscalaDepartamental.ListarEscalaDepartamental(0, gobjUsuario.CodigoFilial, "", 0);
            dtt = objBLEscalaDepartamental.ListarEscalaDepartamental(0, this.IdFilial, "", 0, false, true);

            BLUtilitarios.InseriMensagemDropDownList(ref ddlEscalaDep, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlEscalaDpto_Conting, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            foreach (DataRow dr in dtt.Rows)
            {
                ddlEscalaDep.Items.Add(new ListItem(dr["Des_EscalaDpto"].ToString(), dr["Id_EscalaDpto"].ToString()));
                ddlEscalaDpto_Conting.Items.Add(new ListItem(dr["Des_EscalaDpto"].ToString(), dr["Id_EscalaDpto"].ToString()));
            }

        }
        #endregion

        #region Obter Filial do Colaborador
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método para Obter a filial do colaborador Logado
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 06/04/2010 Created 
        ///     [haguiar_4] 21/01/2011 Modify
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void ObterFilial()
        {
            try
            {
                gobjFilial = gobjBLFilial.Obter(gobjUsuario.CodigoFilial);

                if (gobjFilial != null)
                {
                    txtFilialColaborador.Text = gobjFilial.DescricaoFilial;
                    IdFilial = gobjFilial.IdFilial;

                    this.Vlr_FusoHorario = gobjFilial.Vlr_FusoHorario;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Abrir RadWindow Confirmação de Escala
        
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método para Abrir a tela de confirmação entrada de vários colaboradores
        /// </summary> 
        /// <history> 
        ///     [aoliveira] 03/01/2013 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void AbreVariosRadWindow(string pintIdEscalacao, string pintIdColaborador, string pdatDtEscalacao, string pstrNomeColaborador, string pblnStatusEscala, string sHoraEntrada, string sCodigoColaborador, string sDescricaoEscalaDepto)
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.Close;

            rwdWindow.Width = Unit.Pixel(620);
            rwdWindow.Height = Unit.Pixel(500);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Confirmação de Entrada";

            Session["IdEscalacao"] = Server.UrlEncode(pintIdEscalacao.ToString());
            Session["IdColaborador"] = Server.UrlEncode(pintIdColaborador.ToString());
            Session["DtEscalacao"] = Server.UrlEncode(pdatDtEscalacao.ToString());
            Session["NomeColaborador"] = Server.UrlEncode(pstrNomeColaborador.ToString());
            Session["IdFilial"] = Server.UrlEncode(IdFilial.ToString());
            Session["HoraEntrada"] = Server.UrlEncode(sHoraEntrada.ToString());
            Session["CodigoColaborador"] = Server.UrlEncode(sCodigoColaborador.ToString());
            Session["DescricaoEscalaDepto"] = Server.UrlEncode(sDescricaoEscalaDepto.ToString());

            rwdWindow.NavigateUrl = "ConfirmaVariosEntrada.aspx";
            
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlColaborador = null;

            //Tenta encontrar na master
            pnlColaborador = (Panel)this.FindControl("pnlColaborador");
            pnlColaborador.Controls.Add(rwmWindowManager);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método para Abrir a tela de confirmação de entrada de um colaborador
        /// </summary> 
        /// <history> 
        ///     [aoliveira] 04/01/2013 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void AbreRadWindow(string pintIdEscalacao, string pintIdColaborador, string pdatDtEscalacao, string pstrNomeColaborador, string pblnStatusEscala, string sHoraEntrada, string sCodigoColaborador, string sDescricaoEscalaDepto)
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.Close;

            rwdWindow.Width = Unit.Pixel(420);
            rwdWindow.Height = Unit.Pixel(355);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Confirmação de Entrada";

            Session["IdEscalacao"] = Server.UrlEncode(pintIdEscalacao.ToString());
            Session["IdColaborador"] = Server.UrlEncode(pintIdColaborador.ToString());
            Session["DtEscalacao"] = Server.UrlEncode(pdatDtEscalacao.ToString());
            Session["NomeColaborador"] = Server.UrlEncode(pstrNomeColaborador.ToString());
            Session["IdFilial"] = Server.UrlEncode(IdFilial.ToString());
            Session["HoraEntrada"] = Server.UrlEncode(sHoraEntrada.ToString());
            Session["CodigoColaborador"] = Server.UrlEncode(sCodigoColaborador.ToString());
            Session["DescricaoEscalaDepto"] = Server.UrlEncode(sDescricaoEscalaDepto.ToString());

            rwdWindow.NavigateUrl = "ConfirmaEntrada.aspx";

            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlColaborador = null;

            //Tenta encontrar na master
            pnlColaborador = (Panel)this.FindControl("pnlColaborador");
            pnlColaborador.Controls.Add(rwmWindowManager);
        }

        #region DataGrid

        #region ItemCommand
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método ItemCommand do radgrid
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 06/04/2010 Created 
        ///     [aoliveira] 04/02/2013 Modify
        ///     Abertura da janela de confirmação
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void radGridColaborador_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            foreach (GridDataItem item in radGridColaborador.Items)
            {
                CheckBox chkbx = (CheckBox)item["CheckAcesso"].FindControl("chkActive");

                string strSelecionados = string.Empty;
                if (ViewState["ChecksSelecionados"] != null)
                {
                    strSelecionados = ViewState["ChecksSelecionados"].ToString();
                }

                GridDataItem objGridDataItem = (GridDataItem)chkbx.NamingContainer;
                if (chkbx.Checked == true)
                {
                    //analisa necessidade de inserir                    
                    string aux = strSelecionados.Replace("<" + objGridDataItem["CodigoColaborador"].Text + ">", "");
                    if (aux == strSelecionados)
                    {
                        // Se for igual, significa que o Replace não achou ninguém, logo deve-se Incluir
                        strSelecionados += "<" + item["CodigoColaborador"].Text + ">";
                    }
                }
                else
                {
                    //remove da lista
                    strSelecionados = strSelecionados.Replace("<" + objGridDataItem["CodigoColaborador"].Text + ">", "");
                }

                ViewState["ChecksSelecionados"] = strSelecionados;
            }

            if (e.CommandName.Trim() == "RegistrarAcesso")
            {
                string intIdEscalacao = string.Empty;
                string intIdColaborador = string.Empty;
                string datDtEscalacao = string.Empty;
                string strNomeColaborador = string.Empty;
                string blnStatusEscala = string.Empty;
                string sHoraEntrada = string.Empty;
                string sCodigoColaborador = string.Empty;
                string sDescricaoEscalaDepto = string.Empty;

                string strCommandArgumment = e.CommandArgument.ToString().Trim();
                string[] strCommandArg = new string[5];
                strCommandArg = strCommandArgumment.Split(';');

                intIdEscalacao += Convert.ToInt32(strCommandArg[0]);
                intIdColaborador += Convert.ToInt32(strCommandArg[1]);
                datDtEscalacao += Convert.ToDateTime(strCommandArg[2]);
                strNomeColaborador += strCommandArg[3].ToString();
                blnStatusEscala += Convert.ToBoolean(strCommandArg[4]);
                sHoraEntrada += strCommandArg[5].ToString();
                sCodigoColaborador += strCommandArg[6].ToString();
                sDescricaoEscalaDepto += strCommandArg[7].ToString();
                this.AbreRadWindow(intIdEscalacao, intIdColaborador, datDtEscalacao, strNomeColaborador, blnStatusEscala, sHoraEntrada, sCodigoColaborador, sDescricaoEscalaDepto);
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
                    else
                    {
                        if (Convert.ToInt32(strPageIndexString) < 1)
                        {
                            strPageIndexString = "1";
                            ((TextBox)e.Item.FindControl("txtPagina")).Text = strPageIndexString;
                        }
                    }
                    intPageIndex = Convert.ToInt32(strPageIndexString) - 1;
                }

                if (!(e.Item.OwnerTableView.CurrentPageIndex == intPageIndex))
                {
                    e.Item.OwnerTableView.CurrentPageIndex = intPageIndex;
                    e.Item.OwnerTableView.Rebind();
                }
                ViewState["ChecksSelecionados"] = "";
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
            */
        }
        #endregion

        #region ItemDataBound
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método ItemDataBound do radgrid
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 06/04/2010 Created 
        ///     [haguiar_2] 14/12/2010 modify
        ///     desabilitar botao de acesso em caso de afastamento
        ///     [haguiar_3] modify 07/01/2011
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void radGridColaborador_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                CheckBox chkbx = (CheckBox)item["CheckAcesso"].FindControl("chkActive");

                if (ViewState["ChecksSelecionados"] != null)
                {
                    string strSelecionados = ViewState["ChecksSelecionados"].ToString().Replace("><", ",");
                    strSelecionados = strSelecionados.Replace("<", "");
                    strSelecionados = strSelecionados.Replace(">", "");
                    string[] listSelecionados = strSelecionados.Split(',');

                    bool marcou = false;
                    for (int i = 0; i < listSelecionados.Length && (!marcou); i++)
                    {
                        try
                        {
                            if (item["CodigoColaborador"].Text == listSelecionados[i])
                            {
                                chkbx.Checked = true;
                                marcou = true;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                }

            }

            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                try
                {
                    if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
                    {
                        Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                        ImageButton btnRegistrarAcesso = (ImageButton)dataItem["RegistrarAcesso"].Controls[0];

                        btnRegistrarAcesso.CommandArgument = DataBinder.Eval(e.Item.DataItem, "CodigoEscalacao").ToString() + ";" +
                            DataBinder.Eval(e.Item.DataItem, "IdColaborador").ToString() + ";" +
                            DataBinder.Eval(e.Item.DataItem, "DtEscalacao").ToString() + ";" +
                            DataBinder.Eval(e.Item.DataItem, "NomeColaborador").ToString() + ";" +
                            DataBinder.Eval(e.Item.DataItem, "FlgStatusAcesso").ToString() + ";" +
                            DataBinder.Eval(e.Item.DataItem, "HoraEscalacao").ToString() + ";" +
                            DataBinder.Eval(e.Item.DataItem, "CodigoColaborador").ToString() + ";" +
                            DataBinder.Eval(e.Item.DataItem, "DescricaoEscalaDepto").ToString();


                        bool blnAfastamento=false;
                        switch (DataBinder.Eval(e.Item.DataItem, "HoraEscalacao").ToString())
                        {
                            case "COMPENSADO": 
                                blnAfastamento=true;

                                break;

                            case "FOLGA/DSR":

                                blnAfastamento=true;
                                break;

                            case "FÉRIAS/LICENÇA":

                                blnAfastamento=true;
                                break;
                        }

                        if (!blnAfastamento)
                        {
                            if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "FlgStatusAcesso")))
                            {
                                btnRegistrarAcesso.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                btnRegistrarAcesso.ToolTip = "Registrar Saída";
                            }
                            else
                            {
                                btnRegistrarAcesso.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                btnRegistrarAcesso.ToolTip = "Registrar Entrada";

                            }
                        }
                        else
                        {
                            //em afastamento, desabilita o botao de entrada
                            btnRegistrarAcesso.ImageUrl = "~/imagens/icones/ico_alerta.gif";
                            btnRegistrarAcesso.ToolTip = "";

                            GridDataItem item = (GridDataItem)e.Item;
                            CheckBox chkbx = (CheckBox)item["CheckAcesso"].FindControl("chkActive");
                            chkbx.Visible = false;

                            btnRegistrarAcesso.Enabled = false;
                        }

                        //Obtém a data de último acesso do colaborador na Filial
                        Label lblUltimoAcesso = (Label)e.Item.FindControl("lblUltimoAcesso");

                        DateTime Dt_Entrada = (Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "DataEntrada")));
                        DateTime Dt_Saida = (Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "DataSaida")));

                        if (Dt_Saida == DateTime.MinValue)
                        {
                            if (Dt_Entrada == DateTime.MinValue)
                            {
                                lblUltimoAcesso.Text = "-";
                            }
                            else
                            {
                                lblUltimoAcesso.Text = String.Format("{0:dd/MM/yyyy HH:mm}", Dt_Entrada);
                            }
                        }
                        else
                        {
                            lblUltimoAcesso.Text = String.Format("{0:dd/MM/yyyy HH:mm}", Dt_Saida);
                        }

                        /*
                        AcessoColaborador objAcessoColaborador = new AcessoColaborador();
                        BLAcessoColaborador objBLAcessoColaborador = new BLAcessoColaborador();

                       objAcessoColaborador = objBLAcessoColaborador.ObterUltimoAcesso(gobjUsuario.CodigoFilial,
                            Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "CodigoEscalacao")),
                            Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IdColaborador")));

                       if (objAcessoColaborador.CodAcessoColEscalado != 0)
                       {
                           if (objAcessoColaborador.DataSaida == DateTime.MinValue)
                           {
                               lblUltimoAcesso.Text = objAcessoColaborador.DataEntrada.ToString().Replace(":00", "");
                           }
                           else
                           {
                               lblUltimoAcesso.Text = objAcessoColaborador.DataSaida.ToString().Replace(":00", "");
                           }
                       }
                       else {
                           lblUltimoAcesso.Text = "-";
                       }
                        */

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

        #region Property


        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade Id_Colaborador para liberacao do acesso em contingencia
        /// </summary> 
        /// <history> 
        ///     [haguiar] 08/04/2011 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int Id_Colaborador
        {
            get
            {
                if ((this.ViewState["vsId_Colaborador"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsId_Colaborador"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsId_Colaborador", value);
            }
        }


        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade Vlr_FusoHorario da filial do usuário logado
        /// </summary> 
        /// <history> 
        ///     [haguiar_4] 21/01/2011 Created
        /// </history> 
        /// -----------------------------------------------------------------------------
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

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdFilial do Usuario Logado
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 07/04/2010 Created 
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
        ///     [haguiar] 08/04/2011 Created 08:38
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

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade blnCadastroContingencia
        /// </summary> 
        /// <history> 
        ///     [haguiar] 08/04/2011 Created 08:44
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public bool CadastroContingencia
        {
            get
            {
                if ((this.ViewState["vsblnCadastroContingencia"] != null))
                {
                    return Convert.ToBoolean(this.ViewState["vsblnCadastroContingencia"]);
                }
                else
                {
                    return false;
                }
            }
            set
            {
                this.ViewState.Add("vsblnCadastroContingencia", value);
            }
        }
        #endregion

        #endregion

        


        #endregion
        
        
        
        #region "Contingencia"

        /// <summary>
        ///     Insere acesso de contingencia para colaborador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] created 08/04/2011 09:45
        ///     [haguiar] modify 30/08/2011 19:37
        /// </history>
        protected void btnGravarContingencia_Click(object sender, EventArgs e)
        {
            Page.Validate("Contingencia");

            if (!Page.IsValid)
            {
                return;
            }

            int[] intSel;

            intSel = lstColaboradores_Conting.GetSelectedIndices();
            if (intSel.Length == 0)
            {
                if (string.IsNullOrEmpty(txtNomeColaborador_Conting.Text))
                {
                    Page.Validate("Contingencia2");

                    if (!Page.IsValid)
                        return;
                }
            }

            btnGravarContingencia.Enabled = false;

            BLAcessoColaborador gobjBLAcessoColaborador = new BLAcessoColaborador();
            AcessoContingencia  gobjAcessoContingencia = new AcessoContingencia();
            StringBuilder strCodigoColaboradores = new StringBuilder();

            gobjAcessoContingencia.Des_Motivo = this.txtMotivoLiberacao_Conting.Text;
            gobjAcessoContingencia.Id_UsuarioLibAcesso = Convert.ToInt32(BLAcesso.IdUsuarioLogado());
            gobjAcessoContingencia.Id_Aprovador = Convert.ToInt32(ddlAprovador_Conting.SelectedValue);

            int intRetorno = 0;

            if (!string.IsNullOrEmpty(txtNomeColaborador_Conting.Text))
            {
                gobjAcessoContingencia.Id_Colaborador = this.Id_Colaborador;
                gobjAcessoContingencia.Cod_Colaborador = this.txtREColaborador_Conting.Text;

                intRetorno = gobjBLAcessoColaborador.InserirAcessoContingencia(gobjAcessoContingencia);
            }
            else
            {
                foreach (ListItem var in this.lstColaboradores_Conting.Items)
                {
                    if (var.Selected)
                    {
                        gobjAcessoContingencia.Id_Colaborador = Convert.ToInt32(var.Value);
                        gobjAcessoContingencia.Cod_Colaborador = var.Text.Substring(var.Text.Length-12,12);

                        intRetorno = gobjBLAcessoColaborador.InserirAcessoContingencia(gobjAcessoContingencia);
                    }
                }
            }

            if (intRetorno > 0)
            {
                RadAjaxPanel1.Alert("Acesso de contingência inserido com sucesso!");

                //limpa campos
                this.txtHiddenColaboradores.Value = string.Empty;
                this.Id_Colaborador = 0;
                this.txtREColaborador_Conting.Text = string.Empty;
                //this.txtREAprovador_Conting.Text = string.Empty;
                this.txtMotivoLiberacao_Conting.Text = string.Empty;

                this.txtNomeColaborador_Conting.Text = string.Empty;
                
                ddlAprovador_Conting.SelectedIndex = 0;


                this.lstColaboradores_Conting.Enabled = true;
                
                this.chkSelTodos_Conting.Enabled = true;
                this.chkSelTodos_Conting.Checked = false;

                this.ddlEscalaDpto_Conting.Enabled = true;
                this.ddlEscalaDpto_Conting.SelectedIndex = 0;
                this.lstColaboradores_Conting.Items.Clear();

                btnLimparColaborador_Conting.Visible = false;
            }

            btnGravarContingencia.Enabled = true;
        }


        /// <summary>
        ///     Abre a RadWindow com os colaboradores que podem visitar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] created 06/04/2011 15:43
        /// </history>
        protected void btnListarColaborador_Conting_Click(object sender, ImageClickEventArgs e)
        {
            this.RadWindowColaboradores();
        }

        /// <summary>
        ///     Abre a RadWindow com os colaboradores que podem visitar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [aoliveira] created 13/11/2012 15:33
        /// </history>
        protected void btnInserirEntrada_Click(object sender, EventArgs e)
        {

            string intIdEscalacao = string.Empty;
            string intIdColaborador = string.Empty;
            string datDtEscalacao = string.Empty;
            string strNomeColaborador = string.Empty;
            string blnStatusEscala = string.Empty;
            string sHoraEntrada = string.Empty;
            string sCodigoColaborador = string.Empty;
            string sDescricaoEscalaDepto = string.Empty;
            string strSelecionados = string.Empty;

            foreach (GridDataItem item in radGridColaborador.Items)
            {
                CheckBox chkbx = (CheckBox)item["CheckAcesso"].FindControl("chkActive");


                if (chkbx.Checked == true)
                {
                    ImageButton imgBtm = (ImageButton)item["RegistrarAcesso"].Controls[0];

                    string strCommandArgumment = imgBtm.CommandArgument.ToString().Trim();
                    string[] strCommandArg = new string[5];
                    strCommandArg = strCommandArgumment.Split(';');

                    intIdEscalacao += Convert.ToInt32(strCommandArg[0]) + ",";
                    intIdColaborador += Convert.ToInt32(strCommandArg[1]) + ",";
                    datDtEscalacao += Convert.ToDateTime(strCommandArg[2]) + ",";
                    strNomeColaborador += strCommandArg[3].ToString() + ",";
                    blnStatusEscala += Convert.ToBoolean(strCommandArg[4]) + ",";
                    sHoraEntrada += strCommandArg[5].ToString() + ",";
                    sCodigoColaborador += strCommandArg[6].ToString() + ",";
                    sDescricaoEscalaDepto += strCommandArg[7].ToString() + ",";
                    strSelecionados += "<" + item["CodigoColaborador"].Text + ">";
                }

            }
            ViewState["ChecksSelecionados"] = strSelecionados;

            if (intIdEscalacao.Length > 0)
            {
                lblMensagemEscalacao.Text = string.Empty;
                intIdEscalacao = intIdEscalacao.Remove(intIdEscalacao.Length - 1);
                intIdColaborador = intIdColaborador.Remove(intIdColaborador.Length - 1);
                datDtEscalacao = datDtEscalacao.Remove(datDtEscalacao.Length - 1);
                strNomeColaborador = strNomeColaborador.Remove(strNomeColaborador.Length - 1);
                blnStatusEscala = blnStatusEscala.Remove(blnStatusEscala.Length - 1);
                sHoraEntrada = sHoraEntrada.Remove(sHoraEntrada.Length - 1);
                sCodigoColaborador = sCodigoColaborador.Remove(sCodigoColaborador.Length - 1);
                sDescricaoEscalaDepto = sDescricaoEscalaDepto.Remove(sDescricaoEscalaDepto.Length - 1);

                this.AbreVariosRadWindow(intIdEscalacao, intIdColaborador, datDtEscalacao, strNomeColaborador, blnStatusEscala, sHoraEntrada, sCodigoColaborador, sDescricaoEscalaDepto);
            }
            else
            {
                lblMensagemEscalacao.Text = "Selecione os colaboradores";
            }
            
        }

        /// <summary>
        ///     Abre a RadWindow com a tela de listagem de colaboradores
        /// </summary>
        /// <history>
        ///     [haguiar] created 06/04/2011 15:34
        /// </history>
        protected void RadWindowColaboradores()
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.None;

            rwdWindow.Width = Unit.Pixel(530);
            rwdWindow.Height = Unit.Pixel(480);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Selecione um colaborador ou clique em fechar para sair da tela.";


            rwdWindow.NavigateUrl = "../Solicitacao/ListColaboradoresSol.aspx?Tipo=1&IgnorarEmFerias=1"; //funcionarios e terceiros
            rwmWindowManager.Windows.Add(rwdWindow);
            

            Panel pnlVisitante = null;

            //Tenta encontrar na master
            pnlVisitante = (Panel)this.FindControl("pnlListagem");
            pnlVisitante.Controls.Add(rwmWindowManager);
        }

        /// <summary>
        ///     Botão para a terceira aba
        /// </summary>
        /// <history>
        ///     [haguiar] created 06/04/2011 09:40
        ///</history>        
        protected void btnAbaContingencia_Click(object sender, EventArgs e)
        {
            ControlaPaineis(Enums.TipoPainel.Listagem + 1);
        }

        /// <summary>
        ///     Preenche o RE do aprovador selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] 06/04/2011 created 
        /// </history>
        protected void ddlAprovador_Conting_SelectedIndexChanged(object sender, EventArgs e)
        {/*
            if (ddlAprovador_Conting.SelectedIndex > 0)
            {
                BLSolicitacao objBlSolicitacao = new BLSolicitacao();

                DataTable dtt = objBlSolicitacao.ObterCodigoAprovador(Convert.ToInt32(ddlAprovador_Conting.SelectedItem.Value));
                //txtREAprovador_Conting.Text = dtt.Rows[0][1].ToString();
            }
            else
            {
                //txtREAprovador_Conting.Text = string.Empty;
            }*/
        }


        #region Obter Colaborador

        /// <summary>
        ///     Obtem as informações do colaborador selecionado
        /// </summary>
        /// <history>
        ///     [haguiar] created 06/04/2011 17:08
        /// </history>
        Model.Colaborador.Colaborador gobjColaborador;
        protected void ObterColaborador(int pintIdColaborador)
        {
            BLColaborador objBlColaborador = new BLColaborador();

            try
            {
                gobjColaborador = objBlColaborador.Obter(pintIdColaborador);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        /// <summary>
        ///     Popula o combo com os possíveis aprovadores para contingencia
        /// </summary>
        /// <history>
        ///     [haguiar] 08/04/2011 created 08:35
        ///     [haguiar_SDM9004] 30/08/2011 15:03
        ///     listar apenas usuário logado como aprovador 
        /// </history>
        protected void PopularAprovadoresContingencia()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<Aprovador> colAprovador = new Collection<Aprovador>();

            try
            {
                //colAprovador = objBlSolicitacao.ListarAprovadoresConting(this.IdRegional, this.IdFilial);

                Aprovador gobjAprovador = new Aprovador();
                gobjAprovador.IdUsuario = Convert.ToInt32(gobjUsuario.Codigo);
                gobjAprovador.NomeUsuario = gobjUsuario.NomeUsuario;

                colAprovador.Add(gobjAprovador);

                ddlAprovador_Conting.DataSource = colAprovador;
                ddlAprovador_Conting.DataTextField = "NomeUsuario";
                ddlAprovador_Conting.DataValueField = "IdUsuario";
                ddlAprovador_Conting.DataBind();

                //BLUtilitarios.InseriMensagemDropDownList(ref ddlAprovador_Conting, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                //txtREAprovador_Conting.Text = Convert.ToString(gobjUsuario.Codigo); //string.Empty;

                ddlAprovador_Conting.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #region Escala Departamental e Colaboradores
        /// <summary>
        ///     Seleciona colaboradores da escala departamental
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar_SDM9004] created 30/08/2011 18:08
        /// </history>
        protected void ddlEscalaDpto_Conting_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.ddlEscalaDpto_Conting.SelectedIndex > 0)
            {
                this.txtHiddenColaboradores.Value = string.Empty;
                this.Id_Colaborador = 0;
            }

            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            Collection<Colaborador> colColaboradores = null;

            try
            {
                colColaboradores = objBLEscalaDepartamental.ObterColaboradores(Convert.ToInt32(this.ddlEscalaDpto_Conting.SelectedValue));

                this.PopularListaColaboradores(colColaboradores);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        /// Preenche a Lista de Colaboradores.
        /// </summary>
        /// <param name="parrColaboradores">Array com os Id dos Colaboradores</param>
        /// <history>
        ///     [haguiar_sdm9004] created 30/08/2011 18:14
        /// </history>
        private void PopularListaColaboradores(Collection<Colaborador> pcolColaboradores)
        {


            ListItem limColaborador = null;

            this.lstColaboradores_Conting.Items.Clear();

            foreach (Colaborador objColaborador in pcolColaboradores)
            {
                limColaborador = this.lstColaboradores_Conting.Items.FindByValue(objColaborador.IdColaborador.ToString());

                if (limColaborador == null)
                {
                    limColaborador = new ListItem();
                    limColaborador.Text = objColaborador.NomeColaborador + " - " + objColaborador.CodigoColaborador;
                    limColaborador.Value = objColaborador.IdColaborador.ToString();

                    this.lstColaboradores_Conting.Items.Add(limColaborador);
                }

                limColaborador = null;
            }
        }

        protected void chkSelTodos_Conting_CheckedChanged(object sender, EventArgs e)
        {
            //seleciona apenas um item
            foreach (ListItem liColaborador in this.lstColaboradores_Conting.Items)
            {
                try
                {
                    liColaborador.Selected = ((CheckBox)sender).Checked;                        
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        #endregion
        #endregion
    }
}
