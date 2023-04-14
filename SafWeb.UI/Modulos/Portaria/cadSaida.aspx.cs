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
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.Model.Utilitarios;
using FrameWork.Model.Idioma;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Acesso;
using SafWeb.Model.Acesso;
using SafWeb.Model.Colaborador;
using Telerik.WebControls;

using SafWeb.BusinessLayer.Filial;
using SafWeb.Model.Filial;
using FrameWork.Model.Usuarios;
using SafWeb.BusinessLayer.Escala;
using System.Globalization;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class cadSaida : FWPage
    {
        Filial gobjFilial = new Filial();
        BLFilial gobjBLFilial = new BLFilial();

        /// <history>
        ///     [no history]
        ///     [haguiar_4] 21/01/2011 09:50
        ///     adicionar fuso horário ao campo hora
        /// </history>

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InicializaScript();
            
            if (!Page.IsPostBack)
            {
                this.PopulaCombos();
                this.ControlaPaineis(Enums.TipoPainel.Cadastro);
                this.VerificaPermissoes();

                //inserir informacoes do fuso horario
                BLColaborador objBlColaborador = new BLColaborador();
                DataTable dtt = new DataTable();

                dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));
                gobjFilial = gobjBLFilial.Obter(Convert.ToInt32(dtt.Rows[0][1].ToString()));
                dtt.Dispose();

                this.Vlr_FusoHorario = gobjFilial.Vlr_FusoHorario;

                //hora da filial
                this.txtDataSaida.Text = DateTime.UtcNow.AddHours(this.Vlr_FusoHorario).ToString("dd/MM/yyyy HH:mm"); //DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                //this.txtDataSaida.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }

            this.lblMensagem.Visible = false;
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
            //seleciona a regional e a filial odo usuário logado.
            BLColaborador objBlColaborador = new BLColaborador();
            DataTable dtt = new DataTable();

            try
            {
                dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                this.IdFilial = Convert.ToInt32(dtt.Rows[0][1]);
                this.IdRegional = Convert.ToInt32(dtt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            this.PopulaComboTipoDocumento();
            this.PopularComboEscalaDpto();	
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

        private DateTime DataEntradaVisitante
        {
            get
            {
                return Convert.ToDateTime(ViewState["vsDataEntradaVisitante"]);
            }
            set
            {
                ViewState["vsDataEntradaVisitante"] = value;
            }
        }

        private int CodAcessoVisitante
        {
            get
            {
                if (ViewState["vsCodAcessoVisitante"] == null)
                    ViewState["vsCodAcessoVisitante"] = 0;

                return Convert.ToInt32(ViewState["vsCodAcessoVisitante"]);
            }
            set
            {
                ViewState["vsCodAcessoVisitante"] = value;
            }
        }

        #endregion

        #region Controla Painéis

        private void ControlaPaineis(Enums.TipoPainel pintPainel)
        {
            if (pintPainel == Enums.TipoPainel.Listagem)
            {
                this.pnlListagemVis.Visible = false;
                this.pnlCadastro.Visible = false;

                divAbaColaboradores.Visible = true;
                divAbaVisitantes.Visible = false;

                btnAbaColaboradores.CssClass = "cadbuttonAbaAtiva";
                btnAbaVisitantes.CssClass = "cadbuttonAbaInativa";

                imgAbaColaboradores.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaVisitantes.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";

                this.Bind();
                LimpaControles();
            }
            else if (pintPainel == Enums.TipoPainel.Cadastro)
            {
                this.pnlCadastro.Visible = true;
                this.pnlListagemVis.Visible = false;

                divAbaVisitantes.Visible = true;
                divAbaColaboradores.Visible = false;

                btnAbaVisitantes.CssClass = "cadbuttonAbaAtiva";
                btnAbaColaboradores.CssClass = "cadbuttonAbaInativa";

                imgAbaVisitantes.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaColaboradores.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
            }
            else
            {
                this.pnlCadastro.Visible = false;
                this.pnlListagemVis.Visible = true;

                divAbaVisitantes.Visible = true;
                divAbaColaboradores.Visible = false;

                btnAbaVisitantes.CssClass = "cadbuttonAbaAtiva";
                btnAbaColaboradores.CssClass = "cadbuttonAbaInativa";

                imgAbaVisitantes.ImageUrl = "%%PATH%%/Imagens/right-abaAtiva.gif";
                imgAbaColaboradores.ImageUrl = "%%PATH%%/Imagens/right-abaInativa.gif";
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

        #region Permissões

        private void VerificaPermissoes()
        {
            this.btnBaixar.Enabled = Permissoes.Inclusão();
        }

        #endregion

        #region InicializaScript

        private void InicializaScript()
        {
            if (hdEscalaDep.Text != string.Empty)
            {
                ddlEscalaDep.SelectedValue = hdEscalaDep.Text;
            }

            this.txtCracha.Attributes.Add("OnKeyPress", "return mascara_Numero(this, event.keyCode);");
            this.txtDataSaida.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            this.btnHelpCad.Attributes.Add("OnClick", "AbrirHelpCad();");
            this.btnHelpList.Attributes.Add("OnClick", "AbrirHelpList();");

            //Obtém Usuário logado
            gobjUsuario = BLAcesso.ObterUsuario();

            //Obter Filial do Usuário logado
            this.ObterFilial();

            if (txtHiddenColaboradores.Value != string.Empty)
            {
                lblMensagemEscalacao.Text = txtHiddenColaboradores.Value;

                Bind();

                txtHiddenColaboradores.Value = string.Empty;
            }
            else
            {
                lblMensagemEscalacao.Text = string.Empty;
            }
        }

        #endregion

        #region Abras
        protected void btnAbaVisitantes_Click(object sender, EventArgs e)
        {
            ControlaPaineis(Enums.TipoPainel.Cadastro);
        }

        protected void btnAbaColaboradores_Click(object sender, EventArgs e)
        {
            ControlaPaineis(Enums.TipoPainel.Listagem);
        }

        #endregion

        #region Primeira Aba

        #region Listagem

        #region Métodos

        private void PopulaComboTipoDocumento()
        {
            BLColaborador objBlColaborador = null;
            Collection<TipoDocumento> colTipoDocumento;

            try
            {
                objBlColaborador = new BLColaborador();

                colTipoDocumento = objBlColaborador.ListarTipoDocumento();

                this.ddlTipoDocumento.DataSource = colTipoDocumento;
                this.ddlTipoDocumento.DataTextField = "DescricaoDocumento";
                this.ddlTipoDocumento.DataValueField = "IdTipoDocumento";
                this.ddlTipoDocumento.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoDocumento, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }


        /// <summary>
        ///     Lista as saidas em aberto conforme filtros
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 25/10/2010
        /// </history>
        private void PopulaRadGridSaida(Enums.TipoBind pintTipoBind)
        {
            BLAcessoVisitante objBLAcessoVisitante = null;
            AcessoVisitante objAcessoVisitante = null;
            Collection<AcessoVisitante> colAcessoVisitante = null;

            try
            {
                objBLAcessoVisitante = new BLAcessoVisitante();
                objAcessoVisitante = new AcessoVisitante();

                objAcessoVisitante.NomVisitante = this.txtNomeVisitante.Text;

                //verifica se o campo cracha/placa está preenchido e busca por crachá selecionado
                if (this.txtCrachaPlaca.Text != string.Empty && this.rdbCracha.Checked)
                {
                    objAcessoVisitante.NumCracha = this.txtCrachaPlaca.Text;
                }
                else
                {
                    objAcessoVisitante.NumCracha = string.Empty;
                }

                //verifica se o campo cracha/placa está preenchido e busca por placa selecionado
                if (this.txtCrachaPlaca.Text != string.Empty && this.rdbPlaca.Checked)
                {
                    objAcessoVisitante.Placa = this.txtCrachaPlaca.Text;
                }
                else
                {
                    objAcessoVisitante.Placa = string.Empty;
                }

                if (this.txtCracha.Text != string.Empty)
                    objAcessoVisitante.NumCracha = this.txtCracha.Text;

                if (this.txtPlaca.Text != string.Empty)
                    objAcessoVisitante.Placa = this.txtPlaca.Text;

                //objAcessoVisitante.NumCracha = this.txtCracha.Text;
                //objAcessoVisitante.Placa = this.txtPlaca.Text;

                objAcessoVisitante.CodTipoDocumento = Convert.ToInt32(this.ddlTipoDocumento.SelectedValue);
                objAcessoVisitante.NumDocumento = this.txtDocumento.Text;
                objAcessoVisitante.CodUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                colAcessoVisitante = objBLAcessoVisitante.Listar(objAcessoVisitante, this.rdbCracha.Checked);

                this.radSaida.DataSource = colAcessoVisitante;

                if (pintTipoBind == Enums.TipoBind.DataBind)
                    this.radSaida.DataBind();
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
        ///     Chama a funcao PopulaRadGridSaida
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 25/10/2010
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //limpa o conteúdo do campo cracha/placa. o campo específico será utilizado.
            this.txtCrachaPlaca.Text = string.Empty;

            this.PopulaRadGridSaida(Enums.TipoBind.DataBind);
        }

        /// <summary>
        ///     Volta para o estado inicial da página
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 01/11/2010
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(Enums.TipoPainel.Cadastro);

            //verifica radiobuttons
            VerificaRadioButtons();
        }

        #endregion

        #region Combo

        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            else
            {
                this.txtDocumento.Enabled = false;
            }
        }

        #endregion

        #region RadGrid

        protected void radSaida_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
        {
            this.PopulaRadGridSaida(Enums.TipoBind.SemDataBind);
        }


        /// <summary>
        ///     radSaida
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        /// </history>
        protected void radSaida_ItemCommand(object source, Telerik.WebControls.GridCommandEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
            {
                if (e.CommandName.Trim() == "Editar")
                {
                    try
                    {

                        if (this.rdbCracha.Checked)
                            this.txtCrachaPlaca.Text = e.Item.Cells[2].Text;

                        if (this.rdbPlaca.Checked)
                            this.txtCrachaPlaca.Text = e.Item.Cells[3].Text;

                        //armazena id e data de entrada para a baixa do cracha/placa
                        this.CodAcessoVisitante = Convert.ToInt32(e.Item.Cells[4].Text);
                        this.DataEntradaVisitante = Convert.ToDateTime(e.Item.Cells[8].Text);
                        
                        this.ControlaPaineis(Enums.TipoPainel.Cadastro);

                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                    }
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
                    catch (Exception ex)
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

            */
        }

        protected void radSaida_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
        {
            ImageButton imgEditar;

            if (e.Item.ItemType == Telerik.WebControls.GridItemType.Item || e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem)
            {
                try
                {
                    imgEditar = (ImageButton)(e.Item.FindControl("imgEditar"));
                    imgEditar.Visible = false;
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";

                    // Cursor 
                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 2; @int++)
                    {
                        e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(imgEditar, ""));
                    }

                    if (Permissoes.Alteração())
                    {
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                    }
                    else
                    {
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);
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

        #endregion

        #region Cadastro

        #region Métodos


        /// <summary>
        ///     Efetua a baixa de acesso por crachá/placa
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 25/10/2010
        ///     [haguiar_4] modified 21/01/2011
        ///     implementar fuso horário
        /// </history>
        private bool GravarSaida()
        {
            bool blnRetorno = false;
            BLAcessoVisitante objBLAcessoVisitante = null;
            AcessoVisitante objAcessoVisitante = null;
            //DataTable dtt;

            try
            {
                objBLAcessoVisitante = new BLAcessoVisitante();
                objAcessoVisitante = new AcessoVisitante();
                //dtt = new DataTable();

                //dtt = objBLAcessoVisitante.IdAcessoVisitante(txtCrachaPlaca.Text,
                //                                             Convert.ToInt32(BLAcesso.IdUsuarioLogado()),
                //                                             (rdbCracha.Checked? true : false));

               
                objAcessoVisitante.Saida = Convert.ToDateTime(this.txtDataSaida.Text);
                
                if (objAcessoVisitante.Saida < Convert.ToDateTime(DataEntradaVisitante))
                {
                    RadAjaxPanel1.Alert("A data de saida deve ser maior que a data de entrada.");
                }
                else if (objAcessoVisitante.Saida > DateTime.UtcNow.AddHours(this.Vlr_FusoHorario))
                {
                    RadAjaxPanel1.Alert("A data de saida deve ser menor/igual a data atual.");
                }
                else
                {
                    objAcessoVisitante.CodAcessoVisitante = this.CodAcessoVisitante;
                    objAcessoVisitante.CodUsuLibSai = BLAcesso.IdUsuarioLogado();

                    blnRetorno = objBLAcessoVisitante.Alterar(objAcessoVisitante);
                }
                
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            return blnRetorno;
        }

        #endregion

        #region Eventos

        #region Botões

        /// <summary>
        ///     Abre o painal de busca
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 25/10/2010
        /// </history>
        protected void btnListar_Click(object sender, ImageClickEventArgs e)
        {
            //limpa os campos de busca
            this.txtCracha.Text = string.Empty;
            this.txtPlaca.Text = string.Empty;

            this.ControlaPaineis(Enums.TipoPainel.Cadastro + 2);
            this.PopulaRadGridSaida(Enums.TipoBind.DataBind);
        }

        /// <summary>
        ///     Efetua a baixa de acesso por crachá/placa
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 25/10/2010
        ///     [haguiar_4] modified 21/01/2011
        ///     implementa fuso horário
        /// </history>
        protected void btnBaixar_Click(object sender, EventArgs e)
        {
            bool blnGravar = true;

            if (txtCrachaPlaca.Text == string.Empty)
            {
                RadAjaxPanel1.Alert("Selecione um crachá/placa para dar saída.");
                blnGravar = false;
            }

            try
            {
                if (txtDataSaida.Text.Length == 16)
                {
                    DateTime.Parse(txtDataSaida.Text);
                }
                else
                {
                    RadAjaxPanel1.Alert("Insira Data e Hora.");  
                }
            }
            catch (Exception ex)
            {
                blnGravar = false;
                RadAjaxPanel1.Alert("Data/Hora Inválida.");   
            }

            //verifica se já buscou saidas
            if (this.CodAcessoVisitante == 0)
            {
                //nao efetuou busca de saidas
                this.PopulaRadGridSaida(Enums.TipoBind.DataBind);
                
                //this.radSaida.DataSource

                //existe mais de uma saida em aberto para o crachá/placa selecionada
                if (radSaida.Items.Count > 0)
                {
                    //verifica se existe mais de uma saída
                    if (radSaida.Items.Count > 1)
                    {
                        //exibe as saidas para a seleçao do usuário
                        this.ControlaPaineis(Enums.TipoPainel.Cadastro + 2);
                        blnGravar = false;
                    }
                    else
                    {
                        //efetua a baixa
                        blnGravar = true;
                        this.CodAcessoVisitante = Convert.ToInt32(radSaida.Items[0].Cells[4].Text.ToString());
                    }
                }
                else
                {
                    blnGravar = false;

                    if (rdbCracha.Checked)
                    {
                        RadAjaxPanel1.Alert("Não existe nenhuma entrada para esse crachá.");
                    }
                    else if (rdbPlaca.Checked)
                    {
                        RadAjaxPanel1.Alert("Não existe nenhuma entrada para essa placa.");
                    }
                }
            }
            
            if (blnGravar)
            {
                if (this.GravarSaida())
                {
                    this.lblMensagem.Visible = true;
                    this.lblMensagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_BAIXA));
                    this.txtCrachaPlaca.Text = string.Empty;
                    this.CodAcessoVisitante = 0;
                    this.txtDataSaida.Text = DateTime.UtcNow.AddHours(this.Vlr_FusoHorario).ToString("dd/MM/yyyy HH:mm"); //DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                }
            }
        }

        #endregion

        #region CheckedChanged

        /// <summary>
        ///     Verifica o estado dos radiobuttons Crachá/Placa para a busca
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] created 01/11/2010
        /// </history>
        private void VerificaRadioButtons()
        {
            if (rdbCracha.Checked)
            {
                txtCrachaPlaca.Text = string.Empty;
                txtCrachaPlaca.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NUMERO;
                txtCrachaPlaca.MaxLength = 12;
                this.txtCrachaPlaca.Attributes.Remove("OnKeyPress");

                this.CodAcessoVisitante = 0;

                txtCrachaPlaca.Focus();
            }
            else
            {
                txtCrachaPlaca.Text = string.Empty;
                txtCrachaPlaca.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NENHUMA;
                txtCrachaPlaca.MaxLength = 7;
                this.txtCrachaPlaca.Attributes.Add("OnKeyPress", "return FormataPlaca(event,this);");

                this.CodAcessoVisitante = 0;

                txtCrachaPlaca.Focus();  
            }

        }

        /// <summary>
        ///     Mudança de estado rdbCracha
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modify 01/11/2010
        /// </history>
        protected void rdbCracha_CheckedChanged(object sender, EventArgs e)
        {
            //txtCrachaPlaca.Text = string.Empty;
            //txtCrachaPlaca.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NUMERO;
            //txtCrachaPlaca.MaxLength = 12;
            //this.txtCrachaPlaca.Attributes.Remove("OnKeyPress");

            //this.CodAcessoVisitante = 0;

            //txtCrachaPlaca.Focus();

            VerificaRadioButtons();
        }

        /// <summary>
        ///     Mudança de estado rdbPlaca
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modify 01/11/2010
        /// </history>
        protected void rdbPlaca_CheckedChanged(object sender, EventArgs e)
        {
            //txtCrachaPlaca.Text = string.Empty;
            //txtCrachaPlaca.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NENHUMA;
            //txtCrachaPlaca.MaxLength = 7;
            //this.txtCrachaPlaca.Attributes.Add("OnKeyPress", "return FormataPlaca(event,this);");
            
            //this.CodAcessoVisitante = 0; 
            
            //txtCrachaPlaca.Focus();      
            VerificaRadioButtons();
        }

        #endregion

        #endregion

        #endregion

        #endregion
        
        #region Segunda Aba

            Usuario gobjUsuario = new Usuario();
            BLAcesso gobjBLAcesso = new BLAcesso();

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
                        intHoraEscala = Convert.ToInt32(objListaAcessoColaborador.HoraEscalacao.Substring(0, 2).Replace(":", ""));

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

                DropDownList ddlAux = new DropDownList();
                BLUtilitarios.InseriMensagemDropDownList(ref ddlAux, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                
                for(int i = 0; i<ddlAux.Items.Count; i++)
                    ddlEscalaDep.Items.Add(ddlAux.Items[i]);

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
            private void AbreVariosRadWindow(string pintIdEscalacao, string pintIdColaborador, string pdatDtEscalacao, string pstrNomeColaborador, string pblnStatusEscala, string sHoraEntrada, string sCodigoColaborador, string sDescricaoEscalaDepto)
            {
                //cria radwindow
                RadWindow rwdWindow = new RadWindow();
                RadWindowManager rwmWindowManager = new RadWindowManager();

                rwmWindowManager.Skin = "Office2007";
                //Não exibe nenhum botão na janela
                rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.Close;

                rwdWindow.Width = Unit.Pixel(620);
                rwdWindow.Height = Unit.Pixel(300);
                rwdWindow.Modal = true;
                rwdWindow.ReloadOnShow = true;
                rwdWindow.DestroyOnClose = true;
                rwdWindow.VisibleOnPageLoad = true;

                rwdWindow.Title = "Confirmação de Saída";

                Session["IdEscalacao"] = Server.UrlEncode(pintIdEscalacao.ToString());
                Session["IdColaborador"] = Server.UrlEncode(pintIdColaborador.ToString());
                Session["DtEscalacao"] = Server.UrlEncode(pdatDtEscalacao.ToString());
                Session["NomeColaborador"] = Server.UrlEncode(pstrNomeColaborador.ToString());
                Session["IdFilial"] = Server.UrlEncode(IdFilial.ToString());
                Session["HoraEntrada"] = Server.UrlEncode(sHoraEntrada.ToString());
                Session["CodigoColaborador"] = Server.UrlEncode(sCodigoColaborador.ToString());
                Session["DescricaoEscalaDepto"] = Server.UrlEncode(sDescricaoEscalaDepto.ToString());

                rwdWindow.NavigateUrl = "ConfirmaVariosSaida.aspx?";

                rwmWindowManager.Windows.Add(rwdWindow);

                Panel pnlColaborador = null;

                //Tenta encontrar na master
                pnlColaborador = (Panel)this.FindControl("pnlListagem");
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

                    this.AbreVariosRadWindow(intIdEscalacao, intIdColaborador, datDtEscalacao, strNomeColaborador, blnStatusEscala, sHoraEntrada, sCodigoColaborador, sDescricaoEscalaDepto);
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

                            bool blnAfastamento = false;
                            switch (DataBinder.Eval(e.Item.DataItem, "HoraEscalacao").ToString())
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

                                btnRegistrarAcesso.Enabled = false;
                            }

                            //Obtém a data de último acesso do colaborador na Filial
                            Label lblUltimoAcesso = (Label)e.Item.FindControl("lblUltimoAcesso");
                            Label lblDescUltimoAcesso = (Label)e.Item.FindControl("lblDescUltimoAcesso");

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
            
            #endregion

            #endregion

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

            /// <summary>
            ///     Abre a RadWindow com os colaboradores que podem visitar
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// <history>
            ///     [aoliveira] created 13/11/2012 15:33
            /// </history>
            protected void btnInserirSaida_Click(object sender, EventArgs e)
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

                if(intIdEscalacao.Length > 0)
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

        #endregion
    }
}
