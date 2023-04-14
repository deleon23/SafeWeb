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
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Idioma;
using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Usuarios;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Acesso;
using SafWeb.BusinessLayer.Escala;
using SafWeb.Model.Acesso;
using SafWeb.Model.Filial;
using FrameWork.Model.Usuarios;
using SafWeb.BusinessLayer.Empresa;
using Telerik.WebControls;
using SafWeb.BusinessLayer.Solicitacao;

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
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void InicializaScripts()
        {
            if (!this.divAbaContingencia.Visible)
            {
                this.btnHelp.Attributes.Add("OnClick", "AbrirHelpList();");

                if (txtHiddenColaboradores.Value != string.Empty)
                {
                    lblMensagemEscalacao.Text = txtHiddenColaboradores.Value;

                    Bind(Enums.TipoBind.DataBind);

                    txtHiddenColaboradores.Value = string.Empty;
                }
                else
                {
                    lblMensagemEscalacao.Text = string.Empty;
                }
            }
            else
            {
                //contingencia
                if (txtHiddenColaboradores.Value != string.Empty)
                {
                    ObterColaborador(Convert.ToInt32(txtHiddenColaboradores.Value));

                    if (gobjColaborador != null)
                    {
                        this.txtNomeColaborador_Conting.Text = gobjColaborador.NomeColaborador;
                        this.txtREColaborador_Conting.Text = gobjColaborador.CodigoColaborador;
                    }
                }
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

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

                this.PopulaRadGridVisAgendadas(Enums.TipoBind.DataBind);

                //Bind Escala dos Colaboradores
                this.Bind(Enums.TipoBind.DataBind);

                //Obter Filial do Usuário logado
                this.ObterFilial();
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
                divAbaColaboradores.Visible = true;
                divAbaVisitantes.Visible = false;
                divAbaContingencia.Visible = false;

                btnAbaColaboradores.CssClass = "cadbuttonAbaAtiva";
                btnAbaVisitantes.CssClass = "cadbuttonAbaInativa";
                btnAbaContingencia.CssClass = "cadbuttonAbaInativa";

                imgAbaColaboradores.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaVisitantes.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaContingencia.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
            }
            else if (pintPainel == Enums.TipoPainel.Cadastro)
            {
                divAbaVisitantes.Visible = true;
                divAbaColaboradores.Visible = false;
                divAbaContingencia.Visible = false;

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

                divAbaContingencia.Visible = true;
                divAbaVisitantes.Visible = false;
                divAbaColaboradores.Visible = false;

                btnAbaContingencia.CssClass = "cadbuttonAbaAtiva";
                btnAbaVisitantes.CssClass = "cadbuttonAbaInativa";
                btnAbaColaboradores.CssClass = "cadbuttonAbaInativa";

                imgAbaContingencia.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaColaboradores.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
                imgAbaVisitantes.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
            }

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

                IdFilial = Convert.ToInt32(dtt.Rows[0][1]);

                BLUtilitarios.ConsultarValorCombo(ref ddlRegional, dtt.Rows[0][0].ToString());
                this.PopulaComboFilial(Convert.ToInt32(this.ddlRegional.SelectedValue));
                BLUtilitarios.ConsultarValorCombo(ref ddlFilial, dtt.Rows[0][1].ToString());
            }
            catch (Exception)
            {

                throw;
            }

            this.PopulaComboTipoDocumento();
            this.PopularComboEmpresa();
            this.PopularComboEscalaDpto();
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
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void Bind(Enums.TipoBind pintTipoBind)
        {
            BLAcessoColaborador objBLAcessoColaborador = new BLAcessoColaborador();
            Collection<ListaAcessoColaborador> colListaAcessoColaborador;

            int intFilial = 0;
            string strNumeroDocumento = string.Empty;
            string strNomeColaborador = string.Empty;
            int intHoraInicio = -1;
            int intHoraFim = -1;
            int intMinutoInicio = 0;
            int intMinutoFim = 0;
            int intNumeroEscala = 0;
            int intEscalaDepto = 0;

            try
            {
                //Obtém Usuário logado
                gobjUsuario = BLAcesso.ObterUsuario();

                intFilial = gobjUsuario.CodigoFilial;
                IdFilial = intFilial;

                if (txtNumeroDocumento.Text != string.Empty)
                {
                    strNumeroDocumento = txtNumeroDocumento.Text;
                }
                if (txtNomeColaborador.Text != string.Empty)
                {
                    strNomeColaborador = txtNomeColaborador.Text;
                }
                if (txtHoraInicio.Text != string.Empty)
                {
                    intHoraInicio = Convert.ToInt32(txtHoraInicio.Text.Substring(0, 2));
                    intMinutoInicio = Convert.ToInt32(txtHoraInicio.Text.Substring(3, 2));
                }
                if (txtHoraFim.Text != string.Empty)
                {
                    intHoraFim = Convert.ToInt32(txtHoraFim.Text.Substring(0, 2));
                    intMinutoFim = Convert.ToInt32(txtHoraFim.Text.Substring(3, 2));
                }
                if (txtEscalaColaborador.Text != string.Empty)
                {
                    intNumeroEscala = Convert.ToInt32(txtEscalaColaborador.Text);
                }
                if (ddlEscalaDep.SelectedIndex > 0)
                {
                    intEscalaDepto = Convert.ToInt32(ddlEscalaDep.SelectedItem.Value);
                }


                colListaAcessoColaborador = objBLAcessoColaborador.Listar(intFilial, strNumeroDocumento, strNomeColaborador,
                                    intNumeroEscala, intHoraInicio, intHoraFim, intMinutoInicio, intMinutoFim, intEscalaDepto);

                radGridColaborador.DataSource = colListaAcessoColaborador;

                //Ordena Paginação do Grid
                this.OrdenarPaginacaoGrid(colListaAcessoColaborador);

                if (pintTipoBind == Enums.TipoBind.DataBind)
                {
                    this.radGridColaborador.DataBind();
                }
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
            this.Bind(Enums.TipoBind.DataBind);
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
                    intHoraEscala = Convert.ToInt32(objListaAcessoColaborador.HoraEscalacao.Substring(0, 2));

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
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void PopularComboEscalaDpto()
        {
            BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            DataTable dtt = new DataTable();

            //dtt = objBLEscalaDepartamental.ListarEscalaDepartamental(0, gobjUsuario.CodigoFilial, "", 0);
            dtt = objBLEscalaDepartamental.ListarEscalaDepartamental(0, this.IdFilial, "", 0, false);

            BLUtilitarios.InseriMensagemDropDownList(ref ddlEscalaDep, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            foreach (DataRow dr in dtt.Rows)
            {
                ddlEscalaDep.Items.Add(new ListItem(dr["Des_EscalaDpto"].ToString(), dr["Id_EscalaDpto"].ToString()));
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
        ///     Método para Abrir a tela de confirmação de cadastro de escala do colaborador
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 06/04/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void AbreRadWindow(int pintIdEscalacao, int pintIdColaborador, DateTime pdatDtEscalacao, string pstrNomeColaborador, bool pblnStatusEscala)
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.Close;

            rwdWindow.Width = Unit.Pixel(280);
            rwdWindow.Height = Unit.Pixel(300);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Confirmação de Entrada / Saída";

            rwdWindow.NavigateUrl = "ConfirmaEntradaSaida.aspx?IdEscalacao=" + pintIdEscalacao.ToString() + "&IdColaborador=" +
                pintIdColaborador.ToString() + "&DtEscalacao=" + pdatDtEscalacao.ToString() + "&NomeColaborador=" +
                pstrNomeColaborador + "&IdFilial=" + IdFilial.ToString() + "&StatusAcesso=" + pblnStatusEscala;

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
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void radGridColaborador_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "RegistrarAcesso")
            {
                int intIdEscalacao;
                int intIdColaborador;
                DateTime datDtEscalacao;
                string strNomeColaborador;
                bool blnStatusEscala;

                string strCommandArgumment = e.CommandArgument.ToString().Trim();
                string[] strCommandArg = new string[5];
                strCommandArg = strCommandArgumment.Split(';');

                intIdEscalacao = Convert.ToInt32(strCommandArg[0]);
                intIdColaborador = Convert.ToInt32(strCommandArg[1]);
                datDtEscalacao = Convert.ToDateTime(strCommandArg[2]);
                strNomeColaborador = strCommandArg[3].ToString();
                blnStatusEscala = Convert.ToBoolean(strCommandArg[4]);

                this.AbreRadWindow(intIdEscalacao, intIdColaborador, datDtEscalacao, strNomeColaborador, blnStatusEscala);
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
                            DataBinder.Eval(e.Item.DataItem, "FlgStatusAcesso").ToString();

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
                            btnRegistrarAcesso.ImageUrl = "~/imagens/icones/ico_alertaold.gif";
                            btnRegistrarAcesso.ToolTip = "";

                            btnRegistrarAcesso.Enabled = false;
                        }

                        //Obtém a data de último acesso do colaborador na Filial
                        Label lblUltimoAcesso = (Label)e.Item.FindControl("lblUltimoAcesso");
                        Label lblDescUltimoAcesso = (Label)e.Item.FindControl("lblDescUltimoAcesso");

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


                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }
        #endregion

        #region NeedDataSource
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método NeedDataSource do radgrid
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 07/04/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void radGridColaborador_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.Bind(Enums.TipoBind.SemDataBind);
        }
        #endregion

        #endregion

        #region Property

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
        #endregion

        #endregion

        


        #endregion
        
        
        
        #region "Contingencia"
        
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
            

            rwdWindow.NavigateUrl = "../Solicitacao/ListColaboradoresSol.aspx?Tipo=1"; //funcionarios e terceiros
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
        {
            if (ddlAprovador_Conting.SelectedIndex > 0)
            {
                BLSolicitacao objBlSolicitacao = new BLSolicitacao();

                DataTable dtt = objBlSolicitacao.ObterCodigoAprovador(Convert.ToInt32(ddlAprovador_Conting.SelectedItem.Value));
                txtREAprovador.Text = dtt.Rows[0][1].ToString();
            }
            else
            {
                txtREAprovador.Text = string.Empty;
            }
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

        #endregion
    }
}
