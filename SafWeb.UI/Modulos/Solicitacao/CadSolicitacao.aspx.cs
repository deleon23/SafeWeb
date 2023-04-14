using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Usuarios;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Idioma;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Area;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Empresa;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Lista;
using SafWeb.BusinessLayer.ListaVeiculos;
using SafWeb.BusinessLayer.Regional;
using SafWeb.BusinessLayer.Solicitacao;
using SafWeb.BusinessLayer.Veiculo;
using SafWeb.Model.Colaborador;
using SafWeb.Model.Empresa;
using SafWeb.Model.Solicitacao;
using SafWeb.Model.Veiculo;
using SafWeb.Util.Extension;
using Telerik.WebControls;

namespace SafWeb.UI.Modulos.Solicitacao
{
    public partial class CadSolicitacao : FWPage
    {
        private Colaborador gobjColaborador;
        private SafWeb.Model.Solicitacao.Solicitacao gobjSolicitacao;
        private SafWeb.Model.Solicitacao.SolicitacaoColaborador gobjSolicitacaoColaborador;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HiddenVisitado.Value != string.Empty)
            {
                if (this.IdVisitado != Convert.ToInt32(HiddenVisitado.Value))
                {
                    int intIdVisitadoAntigo;
                    intIdVisitadoAntigo = this.IdVisitado;
                    this.IdVisitado = Convert.ToInt32(HiddenVisitado.Value);
                    HiddenVisitado.Value = string.Empty;

                    if (this.VerificarVisitante() == false)
                    {
                        this.ObterColaborador(this.IdVisitado);
                        txtNomeVisitadoCad.Text = gobjColaborador.NomeColaborador;
                        txtREVisitado.Text = gobjColaborador.CodigoColaborador;
                    }
                    else
                    {
                        this.IdVisitado = intIdVisitadoAntigo;
                    }
                }
            }

            if (HiddenVisitante.Value != string.Empty)
            {
                this.IdVisitante = Convert.ToInt32(HiddenVisitante.Value);

                if (this.IdVisitante > 0)
                {
                    this.ObterColaborador(this.IdVisitante);
                    this.BindModel(Enums.TipoTransacao.DescarregarDados);
                    this.DesabilitarCampos();
                }
                else
                {
                    if (ddlTipoVisitante.SelectedItem.Text.Trim() == "Visitante")
                    {
                        //Se for um visitante habilita os campos para o cadastro
                        this.LimparDadosVisitante();
                        this.HabilitarCampos();
                        BLUtilitarios.ConsultarTextoCombo(ref ddlTipoVisitante, "Visitante");
                        ddlTipoVisitante.Enabled = false;
                    }
                }
            }

            if (HiddenLista.Value != string.Empty)
            {
                for (int i = 0; i < this.lstGrid.Count; i++)
                {
                    if (lstGrid[i].CodLista == Convert.ToInt32(HiddenLista.Value))
                    {
                        lstGrid[i].CodInclusos = HiddenColRetirados.Value;
                        HiddenLista.Value = string.Empty;
                        HiddenColRetirados.Value = string.Empty;
                        break;
                    }
                }
            }

            if (!Page.IsPostBack)
            {
                this.ControlaPaineis(Enums.TipoPainel.Listagem);
                this.VerificaPermissoes();
                this.InicializaScripts();
            }

        }

        #region Inicializa Scripts

        protected void InicializaScripts()
        {
            lblMensagem.Visible = false;

            this.PopularEmpresa();
            this.PopularRegional();
            this.PopularTipoDocumento();
            this.PopularTipoVisitante();
            this.PopularMotivoVisita();
            this.PopularStatus();
            this.PopularTipoSolicitacao();
            this.PopularEstado();

            this.BindSolicitacao(Enums.TipoBind.DataBind);

            txtPlaca.Attributes.Add("OnKeyPress", "return FormataPlaca(event,this);");
            txtDataInicio.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtDataFim.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtDataInicioCad.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtDataFimCad.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            txtPlaca.Attributes.Add("OnKeyPress", "return FormataPlaca(event,this);");
            btnHelpCad.Attributes.Add("OnClick", "AbrirHelpCad();");
            btnHelpList.Attributes.Add("OnClick", "AbrirHelpList();");
            txtObservacao.Attributes.Add("onkeypress", "return blocTexto(this.value);");

            BLUtilitarios.InseriMensagemDropDownList(ref ddlLista, "<-- Selecione uma Filial -->", false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlListaVeiculos, "<-- Selecione uma Filial -->", false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlAprovadorCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlPlaca, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
        }

        #endregion

        #region Controla Painéis

        protected void ControlaPaineis(Enums.TipoPainel pintPainel)
        {
            if (pintPainel == Enums.TipoPainel.Listagem)
            {
                pnlListagem.Visible = true;
                pnlCadastro.Visible = false;
            }
            else
            {
                pnlCadastro.Visible = true;
                pnlListagem.Visible = false;
            }

        }

        #endregion

        #region Permissões

        protected void VerificaPermissoes()
        {
            btnIncluir.Enabled = Permissoes.Inclusão();
        }

        #endregion

        #region Popular Combos

        /// <summary>
        ///     Popula os combos com as regionais
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
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

                ddlRegionalCad.DataSource = colRegional;
                ddlRegionalCad.DataTextField = "DescricaoRegional";
                ddlRegionalCad.DataValueField = "IdRegional";
                ddlRegionalCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegional, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegionalCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
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
        ///     [mribeiro] created 30/06/2009 
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
        ///     Popula os combos com os tipos de documentos
        /// </summary>
        /// <history>
        ///     [mribeiro] created 30/06/2009 
        ///</history>
        protected void PopularTipoDocumento()
        {
            BLColaborador objBlColaborador = new BLColaborador();
            Collection<SafWeb.Model.Colaborador.TipoDocumento> colTipoDocumento;

            try
            {
                colTipoDocumento = objBlColaborador.ListarTipoDocumento();

                ddlTipoDocumento.DataSource = colTipoDocumento;
                ddlTipoDocumento.DataTextField = "DescricaoDocumento";
                ddlTipoDocumento.DataValueField = "IdTipoDocumento";
                ddlTipoDocumento.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoDocumento, "RE", true, -1);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoDocumento, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
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
        ///     [mribeiro] created 30/06/2009 
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

                ddlEmpresaCad.DataSource = colEmpresa;
                ddlEmpresaCad.DataTextField = "DescricaoEmpresa";
                ddlEmpresaCad.DataValueField = "IdEmpresa";
                ddlEmpresaCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlEmpresa, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlEmpresaCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                ddlEmpresaCad.Items.Add(new ListItem("Outra..."));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula os combos com as filiais da regional selecionada
        /// </summary>
        /// <param name="ddlRegional">Regional</param>
        /// <param name="ddlFilial">Filial</param>
        /// <history>
        ///     [mribeiro] created 01/07/2009
        /// </history>
        protected void PopularFilial(ref DropDownList ddlRegional, ref DropDownList ddlFilial)
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
                    lstAreaSelecionada.Items.Clear();
                    lstAreaVisita.Items.Clear();
                }

                BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula o combo com os tipos de solicitações
        /// </summary>
        /// <history>
        ///     [mribeiro] created 06/07/2009 
        ///</history>
        protected void PopularTipoSolicitacao()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.TipoSolicitacao> colTipoSolicitacao;

            try
            {
                colTipoSolicitacao = objBlSolicitacao.ListarTipoSolicitacao();


                ddlTipoSolicitacao.DataSource = colTipoSolicitacao;
                ddlTipoSolicitacao.DataTextField = "Descricao";
                ddlTipoSolicitacao.DataValueField = "Codigo";
                ddlTipoSolicitacao.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlTipoSolicitacao, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula o combo com os status da solicitação
        /// </summary>
        /// <history>
        ///     [mribeiro] created 06/07/2009 
        ///</history>
        protected void PopularStatus()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.Status> colStatus;

            try
            {
                colStatus = objBlSolicitacao.ListarStatus();

                ddlStatus.DataSource = colStatus;
                ddlStatus.DataTextField = "Descricao";
                ddlStatus.DataValueField = "Codigo";
                ddlStatus.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlStatus, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula o combo com os motivos da visita
        /// </summary>
        /// <history>
        ///     [mribeiro] created 06/07/2009 
        ///</history>
        protected void PopularMotivoVisita()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.MotivoSolicitacao> colMotivoSolicitacao;

            try
            {
                colMotivoSolicitacao = objBlSolicitacao.ListarMotivoSolicitacao();

                ddlMotivoVisita.DataSource = colMotivoSolicitacao;
                ddlMotivoVisita.DataTextField = "Descricao";
                ddlMotivoVisita.DataValueField = "Codigo";
                ddlMotivoVisita.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlMotivoVisita, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        /// <summary>
        ///     Popula o combo com as listas cadastradas
        /// </summary>
        /// <history>
        ///     [mribeiro] created 06/07/2009 
        ///</history>
        protected void PopularLista()
        {
            BLLista objBlLista = new BLLista();
            Collection<SafWeb.Model.Lista.Lista> colLista;

            try
            {
                if (ddlFilialCad.SelectedIndex > 0)
                {
                    colLista = objBlLista.ListarListasAtivas(Convert.ToInt32(ddlRegionalCad.SelectedItem.Value),
                                                             Convert.ToInt32(ddlFilialCad.SelectedItem.Value));

                    ddlLista.DataSource = colLista;
                    ddlLista.DataTextField = "DescricaoLista";
                    ddlLista.DataValueField = "IdLista";
                    ddlLista.DataBind();

                    BLUtilitarios.InseriMensagemDropDownList(ref ddlLista, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, -1);
                    ddlLista.Enabled = true;
                }
                else
                {
                    ddlLista.Enabled = false;
                    ddlLista.Items.Clear();
                    ddlLista.SelectedIndex = 0;
                    BLUtilitarios.InseriMensagemDropDownList(ref ddlLista, "<-- Selecione uma Filial -->", false, 0);
                }
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
                if (ddlFilialCad.SelectedIndex > 0)
                {
                    colArea = objBlArea.ListarAreaSeg(Convert.ToInt32(ddlFilialCad.SelectedItem.Value), EAreasSeguranca.Acesso);

                    lstAreaVisita.DataSource = colArea;
                    lstAreaVisita.DataTextField = "Descricao";
                    lstAreaVisita.DataValueField = "Codigo";
                    lstAreaVisita.DataBind();
                }
                else
                {
                    lstAreaVisita.Items.Clear();
                }

                lstAreaSelecionada.Items.Clear();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        protected void PopulaListaVeiculos()
        {
            BLListaVeiculos objBlListaVeiculos = new BLListaVeiculos();

            try
            {
                ddlListaVeiculos.DataSource = objBlListaVeiculos.ListarListaVeiculos(new Model.ListaVeiculos.ListaVeiculos
                {
                    IdFilial = ddlFilialCad.SelectedValue.ToInt32(),
                    IdRegional = ddlRegionalCad.SelectedValue.ToInt32(),
                    Situacao = 1
                });
                ddlListaVeiculos.DataTextField = "DescricaoLista";
                ddlListaVeiculos.DataValueField = "IdLista";
                ddlListaVeiculos.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlListaVeiculos, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
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
        ///     [mribeiro] created 13/07/2009 
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
                ddlPlaca.Items.Add(new ListItem("Outra..."));
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
        ///     [mribeiro] created 13/07/2009 
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

        /// <summary>
        ///     Popula o combo com os possíveis aprovadores
        /// </summary>
        /// <history>
        ///     [mribeiro] 15/07/2009 created
        /// </history>
        protected void PopularAprovadores()
        {
            string strRegional = "", strFilial = "", strTipoSolicitacao = "";

            for (int i = 0; i < this.lstGrid.Count; i++)
            {
                if (i == 0)
                {
                    strRegional = this.lstGrid[i].CodRegional.ToString();
                    strFilial = this.lstGrid[i].CodFilial.ToString();
                    strTipoSolicitacao = this.lstGrid[i].CodTipoSolicitacao.ToString();
                }
                else
                {
                    strRegional += "," + this.lstGrid[i].CodRegional.ToString();
                    strFilial += "," + this.lstGrid[i].CodFilial.ToString();
                    strTipoSolicitacao += "," + this.lstGrid[i].CodTipoSolicitacao.ToString();
                }
            }

            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<Aprovador> colAprovador = new Collection<Aprovador>();

            try
            {
                colAprovador = objBlSolicitacao.ListarAprovadores(strRegional,
                                                                  strFilial,
                                                                  strTipoSolicitacao);

                ddlAprovadorCad.DataSource = colAprovador;
                ddlAprovadorCad.DataTextField = "NomeUsuario";
                ddlAprovadorCad.DataValueField = "IdUsuario";
                ddlAprovadorCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlAprovadorCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                txtREAprovador.Text = string.Empty;

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Listagem

        #region Bind Solicitações


        /// <history>
        ///     [no history]
        ///     [haguiar] 17/06/2011 11:42
        /// </history>
        private void BindSolicitacao(Enums.TipoBind pintTipoBind)
        {
            BLSolicitacao objBLSolicitacao;
            Collection<SafWeb.Model.Solicitacao.Solicitacao> colSolicitacoes;

            try
            {
                objBLSolicitacao = new BLSolicitacao();
                colSolicitacoes = new Collection<SafWeb.Model.Solicitacao.Solicitacao>();

                colSolicitacoes = objBLSolicitacao.ListarSolicitacoes((txtNumSolicitacao.Text != "" ? Convert.ToInt32(txtNumSolicitacao.Text) : 0),
                                                                      (ddlEmpresa.SelectedIndex > 0 ? Convert.ToInt32(ddlEmpresa.SelectedValue) : 0),
                                                                      (ddlRegional.SelectedIndex > 0 ? Convert.ToInt32(ddlRegional.SelectedValue) : 0),
                                                                      (ddlFilial.SelectedIndex > 0 ? Convert.ToInt32(ddlFilial.SelectedValue) : 0),
                                                                      (ddlStatus.SelectedIndex > 0 ? Convert.ToInt32(ddlStatus.SelectedValue) : -1),
                                                                      (ddlTipoSolicitacao.SelectedIndex > 0 ? Convert.ToInt32(ddlTipoSolicitacao.SelectedValue) : 0),
                                                                      (txtDataInicio.Text != "" ? (DateTime?)Convert.ToDateTime(txtDataInicio.Text) : null),
                                                                      (txtDataFim.Text != "" ? (DateTime?)Convert.ToDateTime(txtDataFim.Text) : null),
                                                                      txtNomeVisitado.Text,
                                                                      txtNomeVisitante.Text,
                                                                      txtNomeSolicitante.Text,
                                                                      txtNomeAprovador.Text,
                                                                      Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                this.radGridSolicitacao.DataSource = colSolicitacoes;

                if (pintTipoBind == Enums.TipoBind.DataBind)
                {
                    this.radGridSolicitacao.DataBind();
                }
            }

            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Botões

        /// <summary>
        ///     Lista as solicitações conforme filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BindSolicitacao(Enums.TipoBind.DataBind);
        }

        /// <summary>
        ///     Abre a tela de cadastro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(Enums.TipoPainel.Cadastro);
            this.BindModel(Enums.TipoTransacao.Novo);

        }

        #endregion

        #region DataGrid

        #region NeedDataSource

        protected void radGridSolicitacao_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindSolicitacao(Enums.TipoBind.SemDataBind);
            }
        }

        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridSolicitacao
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        ///     [haguiar] modified 27/04/2011
        ///     alterar área para codigo de area+flg_areati
        /// </history>        
        protected void radGridSolicitacao_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Visualizar")
            {
                Response.Redirect("../Aprovacao/ListHistorico.aspx?Id_Solicitacao=" + e.Item.Cells[2].Text
                    + "&Pag_Solicitacao=1");
            }

            if (e.CommandName.Trim() == "Editar")
            {
                if (e.CommandArgument.ToString().Trim() != string.Empty)
                {
                    if (Permissoes.Alteração() == false)
                    {
                        txtNomeVisitadoCad.Enabled = false;
                        txtREVisitado.Enabled = false;
                        ddlAprovadorCad.Enabled = false;
                        txtREAprovador.Enabled = false;
                        ddlFilialCad.Enabled = false;
                        ddlMotivoVisita.Enabled = false;
                        lstAreaVisita.Enabled = false;
                        lstAreaSelecionada.Enabled = false;
                        txtObservacao.Enabled = false;
                        txtDataInicioCad.Enabled = false;
                        txtDataFimCad.Enabled = false;
                        ddlTipoVisitante.Enabled = false;
                        ddlLista.Enabled = false;
                        txtNomeVisitanteCad.Enabled = false;
                        ddlTipoDocumento.Enabled = false;
                        ddlEmpresaCad.Enabled = false;
                        txtOutraEmpresa.Enabled = false;
                        ddlPlaca.Enabled = false;
                        ddlEstado.Enabled = false;
                        txtDocumento.Enabled = false;
                        chkDomingo.Enabled = false;
                        chkSabado.Enabled = false;
                        chkFeriado.Enabled = false;
                        btnAddUm.Enabled = false;
                        btnAddTodos.Enabled = false;
                        btnRemoverUm.Enabled = false;
                        btnRemoverTodos.Enabled = false;
                        ddlAprovadorCad.Enabled = false;
                        txtREAprovador.Enabled = false;
                        btnAdicionar.Enabled = false;
                        btnListarVisitado.Enabled = false;
                        btnListarVisitante.Enabled = false;
                        btnGravar.Enabled = false;
                    }

                    this.BindModel(Enums.TipoTransacao.Novo);
                    this.IdSolicitacao = Convert.ToInt32(e.CommandArgument.ToString().Trim());
                    this.ControlaPaineis(Enums.TipoPainel.Cadastro);
                    //obtem os dados da solicitação
                    this.ObterSolicitacao();
                    BLUtilitarios.ConsultarValorCombo(ref ddlRegionalCad, gobjSolicitacaoColaborador.CodRegional.ToString());
                    this.PopularFilial(ref ddlRegionalCad, ref ddlFilialCad);
                    BLUtilitarios.ConsultarValorCombo(ref ddlFilialCad, gobjSolicitacaoColaborador.CodFilial.ToString());
                    ddlFilialCad.Enabled = true;
                    BLUtilitarios.ConsultarValorCombo(ref ddlMotivoVisita, gobjSolicitacaoColaborador.CodMotivoVisita.ToString());

                    this.PopularArea();

                    string[] strCodArea;

                    strCodArea = gobjSolicitacaoColaborador.CodArea.Split(',');
                    for (int intCont = lstAreaVisita.Items.Count - 1; intCont >= 0; intCont--)
                    {
                        foreach (string strItem in strCodArea)
                        {
                            if (strItem == lstAreaVisita.Items[intCont].Value.Substring(0, lstAreaVisita.Items[intCont].Value.Length - 2))
                            {
                                lstAreaSelecionada.Items.Add(new ListItem(lstAreaVisita.Items[intCont].Text, lstAreaVisita.Items[intCont].Value));
                                lstAreaVisita.Items.RemoveAt(intCont);
                                break;
                            }
                        }
                    }

                    //for (int intCont = lstAreaVisita.Items.Count - 1; intCont >= 0; intCont--)
                    //{
                    //    for (int intCont2 = 0; intCont2 < strCodArea.Length; intCont2++)
                    //    {
                    //        if (strCodArea[intCont2] == lstAreaVisita.Items[intCont].Value.Substring(0, lstAreaVisita.Items[intCont].Value.Length - 1))
                    //        {
                    //            lstAreaSelecionada.Items.Add(new ListItem(lstAreaVisita.Items[intCont].Text, lstAreaVisita.Items[intCont].Value));
                    //            lstAreaVisita.Items.RemoveAt(intCont);
                    //        }
                    //    }
                    //}

                    txtObservacao.Text = gobjSolicitacaoColaborador.Observacao;
                    txtDataInicioCad.Text = gobjSolicitacaoColaborador.InicioVisita.ToString("dd/MM/yyy HH:mm");
                    txtDataFimCad.Text = gobjSolicitacaoColaborador.FimVisita.ToString("dd/MM/yyy HH:mm");
                    chkSabado.Checked = gobjSolicitacaoColaborador.AcSabado;
                    chkDomingo.Checked = gobjSolicitacaoColaborador.AcDomingo;
                    chkFeriado.Checked = gobjSolicitacaoColaborador.AcFeriado;

                    //Obtem o aprovador
                    try
                    {
                        BLSolicitacao objBlSolicitacao = new BLSolicitacao();
                        Collection<SafWeb.Model.Solicitacao.Aprovador> colAprovador = new Collection<Aprovador>();

                        colAprovador = objBlSolicitacao.ListarAprovadores(gobjSolicitacaoColaborador.CodRegional.ToString(),
                                                                          gobjSolicitacaoColaborador.CodFilial.ToString(),
                                                                          gobjSolicitacaoColaborador.CodTipoSolicitacao.ToString());

                        ddlAprovadorCad.DataSource = colAprovador;
                        ddlAprovadorCad.DataTextField = "NomeUsuario";
                        ddlAprovadorCad.DataValueField = "IdUsuario";
                        ddlAprovadorCad.DataBind();

                        BLUtilitarios.InseriMensagemDropDownList(ref ddlAprovadorCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                        BLUtilitarios.ConsultarValorCombo(ref ddlAprovadorCad, gobjSolicitacaoColaborador.CodAprovador.ToString());
                        DataTable dtt = objBlSolicitacao.ObterCodigoAprovador(gobjSolicitacaoColaborador.CodAprovador);



                        if (dtt.Rows.Count > 0)
                        {
                            if (dtt.Rows[0][1] != null)
                                txtREAprovador.Text = dtt.Rows[0][1].ToString();
                            else
                                txtREAprovador.Text = string.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                    }

                    //obtem o veículo
                    if (gobjSolicitacaoColaborador.CodVeiculo > 0)
                    {
                        BLUtilitarios.ConsultarValorCombo(ref ddlEstado, gobjSolicitacaoColaborador.CodEstadoVeiculo.ToString());
                        this.PopularPlaca();
                        BLUtilitarios.ConsultarValorCombo(ref ddlPlaca, gobjSolicitacaoColaborador.CodVeiculo.ToString());
                        ddlPlaca.Enabled = true;
                    }
                    else if (gobjSolicitacaoColaborador.CodListaVeiculos > 0)
                    {
                        ddlEstado.Enabled = false;
                        this.PopulaListaVeiculos();
                        ddlListaVeiculos.SelectedValue = gobjSolicitacaoColaborador.CodListaVeiculos.ToString();
                        //BLUtilitarios.ConsultarValorCombo(ref ddlListaVeiculos, gobjSolicitacaoColaborador.CodListaVeiculos.ToString());
                        ddlListaVeiculos.Enabled = true;
                    }

                    //obtem o visitado
                    this.ObterColaborador(gobjSolicitacaoColaborador.CodVisitado);
                    this.IdVisitado = gobjSolicitacaoColaborador.CodVisitado;
                    txtNomeVisitadoCad.Text = gobjColaborador.NomeColaborador;
                    txtREVisitado.Text = gobjColaborador.CodigoColaborador;
                    //obtem o visitante
                    if (gobjSolicitacaoColaborador.CodVisitante == 0)
                    {
                        BLUtilitarios.ConsultarTextoCombo(ref ddlTipoVisitante, "Lista");
                        this.PopularLista();
                        BLUtilitarios.ConsultarTextoCombo(ref ddlLista, gobjSolicitacaoColaborador.NomeVisitante);
                        IdListaAnterior = Convert.ToInt32(ddlLista.SelectedItem.Value);
                        ddlLista.Visible = true;
                        lblLista.Visible = true;
                    }
                    else
                    {
                        this.ObterColaborador(gobjSolicitacaoColaborador.CodVisitante);
                        this.IdVisitante = gobjSolicitacaoColaborador.CodVisitante;
                        this.BindModel(Enums.TipoTransacao.DescarregarDados);
                        this.DesabilitarCampos();
                    }

                    string strRetirados = gobjSolicitacaoColaborador.CodInclusos;

                    this.BindModel(Enums.TipoTransacao.CarregarDados);

                    //adiciona o novo registro
                    this.lstGrid.Add(gobjSolicitacaoColaborador);
                    this.BindVisitante(Enums.TipoBind.DataBind);

                    lstGrid[0].CodInclusos = strRetirados;

                    //this.PopularAprovadores();
                    ddlAprovadorCad.Enabled = true;

                    this.LimparDadosVisitante();

                    txtNomeVisitanteCad.Enabled = false;
                    ddlTipoDocumento.Enabled = false;
                    txtDocumento.Enabled = false;
                    ddlEmpresaCad.Enabled = false;

                    btnAdicionar.Enabled = false;
                }
            }

            if (e.CommandName.Trim() == "Ativar")
            {

                int intSituacao = 0;
                int intIdSolicitacao = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                try
                {

                    BLSolicitacao objBlSolicitacao = new BLSolicitacao();

                    //altera o botão
                    if (btnAtivar.ToolTip == "Ativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        intSituacao = 1;
                    }
                    else if (btnAtivar.ToolTip == "Inativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        intSituacao = 0;
                    }

                    objBlSolicitacao.AlterarSituacao(intIdSolicitacao,
                                                     intSituacao,
                                                     Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                    //altera o status
                    if (intSituacao == 1)
                    {
                        radGridSolicitacao.Items[e.Item.ItemIndex]["Status"].Text = objBlSolicitacao.ObterUltimoStatus(intIdSolicitacao);
                    }
                    else
                    {
                        radGridSolicitacao.Items[e.Item.ItemIndex]["Status"].Text = "Cancelada";
                    }

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
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

        protected void radGridSolicitacao_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnVisualizar;
                btnVisualizar = (ImageButton)e.Item.FindControl("imgVisualizar");

                Telerik.WebControls.GridDataItem dataItemEditar = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnEditar = (ImageButton)dataItemEditar["Editar"].Controls[0];

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                if ((btnAtivar != null))
                {
                    btnAtivar.Visible = false;

                    if (Permissoes.Alteração())
                    {
                        BLSolicitacao objBlSolicitacao;
                        DataTable dttAprovadores = new DataTable();

                        try
                        {
                            objBlSolicitacao = new BLSolicitacao();

                            dttAprovadores = objBlSolicitacao.ObterAprovadores(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Codigo")));

                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }

                        if (btnEditar != null)
                        {
                            btnEditar.Visible = false;
                            int intIdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                            for (int i = 0; i < dttAprovadores.Rows.Count; i++)
                            {
                                if (intIdUsuario == Convert.ToInt32(dttAprovadores.Rows[i][0]))
                                {
                                    btnAtivar.Visible = true;

                                    if (intIdUsuario == Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "CodUsuSolic").ToString())) //e.Item.Cells[7].Text))
                                    {
                                        btnVisualizar.Visible = false;
                                        e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                                        e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                                        e.Item.Style["cursor"] = "hand";
                                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);

                                        // Cursor 
                                        int intCell = e.Item.Cells.Count;
                                        for (int @int = 0; @int <= intCell - 3; @int++)
                                        {
                                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnVisualizar, ""));
                                        }
                                    }

                                    if (Permissoes.Alteração())
                                    {
                                        btnEditar.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                                        btnEditar.Visible = true;

                                        btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Codigo").ToString();
                                    }

                                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Situacao")))
                                    {
                                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                        btnAtivar.ToolTip = "Inativar";
                                    }
                                    else
                                    {
                                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                        btnAtivar.ToolTip = "Ativar";
                                    }
                                }
                            }
                        }
                        else
                        {
                            int intIdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                            for (int i = 0; i < dttAprovadores.Rows.Count; i++)
                            {
                                if (intIdUsuario == Convert.ToInt32(dttAprovadores.Rows[i][0]))
                                {
                                    btnAtivar.Visible = true;

                                    if (intIdUsuario == Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "CodUsuSolic").ToString())) //e.Item.Cells[7].Text))
                                    {
                                        btnVisualizar.Visible = false;
                                        e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                                        e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                                        e.Item.Style["cursor"] = "hand";
                                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.VISUALIZAR);

                                        // Cursor 
                                        int intCell = e.Item.Cells.Count;
                                        for (int @int = 0; @int <= intCell - 2; @int++)
                                        {
                                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnVisualizar, ""));
                                        }
                                    }

                                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Situacao")))
                                    {
                                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                                        btnAtivar.ToolTip = "Inativar";
                                    }
                                    else
                                    {
                                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                                        btnAtivar.ToolTip = "Ativar";
                                    }
                                }
                            }
                        }


                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Codigo").ToString();
                    }
                    else if (btnEditar != null)
                    {
                        btnEditar.Visible = false;
                    }
                }
                else if (btnEditar != null)
                {
                    btnEditar.Visible = false;
                }
            }
        }

        #endregion

        #endregion

        #region Eventos

        /// <summary>
        ///     Seleciona uma filial conforme Regional selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void ddlRegional_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularFilial(ref ddlRegional, ref ddlFilial);
        }

        #endregion

        #endregion

        #region Cadastro

        #region BindModel


        /// <summary>
        ///     Bind model da solicitação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 27/04/2011 11:48
        ///     verificar area de ti
        ///     [aoliveira] modified 28/02/2013 09:02
        ///     verificar se colaborador estará de férias ou de licença médica na 
        ///     data da solicitação e exigir o mesmo tratamento de acesso emergencial 
        ///     para esses casos
        /// </history>
        protected void BindModel(Enums.TipoTransacao pintTipoTransacao)
        {
            if (pintTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                DateTime datInicio = Convert.ToDateTime(txtDataInicioCad.Text);
                DateTime datFim = Convert.ToDateTime(txtDataFimCad.Text);
                BLColaborador objBLColaborador = new BLColaborador();

                gobjSolicitacaoColaborador = new SolicitacaoColaborador();

                string strAreas = "", strCodAreas = "";
                bool blnAreaSeg = false;
                bool blnAreaTI = false;

                for (int i = 0; i < lstAreaSelecionada.Items.Count; i++)
                {
                    if (i == 0)
                    {
                        strAreas += lstAreaSelecionada.Items[i].Text;
                        strCodAreas += lstAreaSelecionada.Items[i].Value.Substring(0, lstAreaSelecionada.Items[i].Value.Length - 2);

                        if (lstAreaSelecionada.Items[i].Value.Substring(lstAreaSelecionada.Items[i].Value.Length - 2, 1) == "1")
                        {
                            blnAreaSeg = true;
                        }

                        if (lstAreaSelecionada.Items[i].Value.Substring(lstAreaSelecionada.Items[i].Value.Length - 1, 1) == "1")
                        {
                            blnAreaTI = true;
                        }
                    }
                    else
                    {
                        strAreas += "," + lstAreaSelecionada.Items[i].Text;
                        strCodAreas += "," + lstAreaSelecionada.Items[i].Value.Substring(0, lstAreaSelecionada.Items[i].Value.Length - 2);

                        if (lstAreaSelecionada.Items[i].Value.Substring(lstAreaSelecionada.Items[i].Value.Length - 2, 1) == "1")
                        {
                            blnAreaSeg = true;
                        }

                        if (lstAreaSelecionada.Items[i].Value.Substring(lstAreaSelecionada.Items[i].Value.Length - 1, 1) == "1")
                        {
                            blnAreaTI = true;
                        }
                    }
                }

                gobjSolicitacaoColaborador.CodRegional = Convert.ToInt32(ddlRegionalCad.SelectedItem.Value);
                gobjSolicitacaoColaborador.CodFilial = Convert.ToInt32(ddlFilialCad.SelectedItem.Value);
                gobjSolicitacaoColaborador.AliasFilial = ddlFilialCad.SelectedItem.Text;
                gobjSolicitacaoColaborador.MotivoVisita = ddlMotivoVisita.SelectedItem.Text;
                gobjSolicitacaoColaborador.CodMotivoVisita = Convert.ToInt32(ddlMotivoVisita.SelectedItem.Value);
                gobjSolicitacaoColaborador.CodArea = strCodAreas;
                gobjSolicitacaoColaborador.NomeArea = strAreas;
                gobjSolicitacaoColaborador.Observacao = txtObservacao.Text;
                gobjSolicitacaoColaborador.InicioVisita = Convert.ToDateTime(txtDataInicioCad.Text);
                gobjSolicitacaoColaborador.FimVisita = Convert.ToDateTime(txtDataFimCad.Text);
                gobjSolicitacaoColaborador.AcSabado = chkSabado.Checked;
                gobjSolicitacaoColaborador.AcDomingo = chkDomingo.Checked;
                gobjSolicitacaoColaborador.AcFeriado = chkFeriado.Checked;
                gobjSolicitacaoColaborador.CodVisitado = this.IdVisitado;
                //gobjSolicitacaoColaborador.CodRetirados;


                if (ddlLista.Visible)
                {
                    gobjSolicitacaoColaborador.CodVisitante = -1;
                    gobjSolicitacaoColaborador.CodLista = Convert.ToInt32(ddlLista.SelectedItem.Value);
                    gobjSolicitacaoColaborador.NomeVisitante = ddlLista.SelectedItem.Text;
                }
                else
                {
                    gobjSolicitacaoColaborador.CodVisitante = this.IdVisitante;
                    gobjSolicitacaoColaborador.NomeVisitante = txtNomeVisitanteCad.Text;
                    gobjSolicitacaoColaborador.NumeroDocumento = txtDocumento.Text;
                    gobjSolicitacaoColaborador.CodTipoColaborador = Convert.ToInt32(ddlTipoVisitante.SelectedItem.Value);

                    if (ddlTipoVisitante.SelectedItem.Text == "Visitante")
                    {
                        gobjSolicitacaoColaborador.CodTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value);
                    }
                    else
                    {
                        gobjSolicitacaoColaborador.CodTipoDocumento = -1;
                    }
                }

                if (txtOutraEmpresa.Visible)
                {
                    gobjSolicitacaoColaborador.CodEmpresa = 0;
                    gobjSolicitacaoColaborador.Empresa = txtOutraEmpresa.Text;
                }
                else
                {
                    if (Convert.ToInt32(ddlEmpresaCad.SelectedItem.Value) > 0)
                    {
                        gobjSolicitacaoColaborador.CodEmpresa = Convert.ToInt32(ddlEmpresaCad.SelectedItem.Value);
                    }
                    else
                    {
                        gobjSolicitacaoColaborador.CodEmpresa = -1;
                    }
                }

                if (ddlListaVeiculos.Visible && ddlListaVeiculos.SelectedIndex > 0)
                {
                    gobjSolicitacaoColaborador.PlacaVeiculo = ddlListaVeiculos.SelectedItem.Text;
                    gobjSolicitacaoColaborador.CodVeiculo = -1;

                    gobjSolicitacaoColaborador.CodListaVeiculos = ddlListaVeiculos.SelectedValue.ToInt32();
                }
                else
                {
                    if (txtPlaca.Visible)
                    {
                        gobjSolicitacaoColaborador.PlacaVeiculo = txtPlaca.Text;
                        gobjSolicitacaoColaborador.CodVeiculo = 0;
                    }
                    else
                    {
                        if (ddlPlaca.SelectedIndex > 0)
                        {
                            gobjSolicitacaoColaborador.PlacaVeiculo = ddlPlaca.SelectedItem.Text;
                            gobjSolicitacaoColaborador.CodVeiculo = Convert.ToInt32(ddlPlaca.SelectedItem.Value);
                        }
                        else
                        {
                            gobjSolicitacaoColaborador.CodVeiculo = -1;
                        }
                    }

                    gobjSolicitacaoColaborador.CodEstadoVeiculo = Convert.ToInt32(ddlEstado.SelectedItem.Value);
                }
                //verifica qual o tipo de solicitação

                //verifica se é fora do horário comercial, ou inclui sabado, domingo ou feriado
                if (datInicio.Hour >= 18 || datInicio.Hour < 8 || datFim.Hour > 18 || datFim.Hour <= 8 || chkDomingo.Checked || chkFeriado.Checked || chkSabado.Checked)
                {
                    if (blnAreaTI)
                    {
                        //Área de TI - Condição de Segurança Nivel 3
                        gobjSolicitacaoColaborador.CodTipoSolicitacao = 12;
                    }
                    else
                    {
                        //verifica se é area de seg.
                        if (blnAreaSeg)
                        {
                            //Condição de Segurança Nivel 3 - Area de Segurança
                            gobjSolicitacaoColaborador.CodTipoSolicitacao = 6;
                        }
                        else
                        {
                            //Condição de Segurança Nivel 3
                            gobjSolicitacaoColaborador.CodTipoSolicitacao = 3;
                        }
                    }
                }
                //verifica se é para hoje, ou se o funcionário está de férias ou licença médica
                else if ((datInicio.Date == DateTime.Now.Date) || (objBLColaborador.ColaboradorEmFeriasLicenca(gobjSolicitacaoColaborador.CodVisitante, datInicio, datFim)))
                {
                    if (blnAreaTI)
                    {
                        //Área de TI Emergencial
                        gobjSolicitacaoColaborador.CodTipoSolicitacao = 11;
                        
                    }
                    else
                    {
                        //verifica se é area de seg.
                        if (blnAreaSeg)
                        {
                            //Emergencial - Area de Segurança
                            gobjSolicitacaoColaborador.CodTipoSolicitacao = 5;
                        }
                        else
                        {
                            //Emergencial
                            gobjSolicitacaoColaborador.CodTipoSolicitacao = 2;
                        }
                    }
                }
                else
                {

                    if (blnAreaTI)
                    {
                        //Normal - Área de TI
                        gobjSolicitacaoColaborador.CodTipoSolicitacao = 10;
                    }
                    else
                    {
                        //verifica se é area de seg.
                        if (blnAreaSeg)
                        {
                            //Normal - Area de Segurança
                            gobjSolicitacaoColaborador.CodTipoSolicitacao = 4;
                        }
                        else
                        {
                            //Normal
                            gobjSolicitacaoColaborador.CodTipoSolicitacao = 1;
                        }
                    }
                }
            }
            else if (pintTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                BLUtilitarios.ConsultarValorCombo(ref ddlTipoVisitante, gobjColaborador.IdTipoColaborador.ToString());
                txtNomeVisitanteCad.Text = gobjColaborador.NomeColaborador;

                if (gobjColaborador.CodigoColaborador == null)
                {
                    BLUtilitarios.ConsultarValorCombo(ref ddlTipoDocumento, gobjColaborador.IdTipoDocumento.ToString());
                    txtDocumento.Text = gobjColaborador.NumeroDocumento;
                }
                else
                {
                    BLUtilitarios.ConsultarTextoCombo(ref ddlTipoDocumento, "RE");
                    txtDocumento.Text = gobjColaborador.CodigoColaborador;
                }
                BLUtilitarios.ConsultarValorCombo(ref ddlEmpresaCad, gobjColaborador.IdEmpresa.ToString());
            }
            else if (pintTipoTransacao == Enums.TipoTransacao.Novo)
            {
                HiddenVisitado.Value = string.Empty;
                HiddenVisitante.Value = string.Empty;
                this.IdVisitante = 0;
                this.IdVisitado = 0;
                this.IdSolicitacao = 0;
                txtNomeVisitadoCad.Text = string.Empty;
                txtREVisitado.Text = string.Empty;
                ddlRegionalCad.SelectedIndex = 0;
                ddlFilialCad.SelectedIndex = 0;
                ddlFilialCad.Enabled = false;
                lstAreaSelecionada.Items.Clear();
                lstAreaVisita.Items.Clear();
                ddlMotivoVisita.SelectedIndex = 0;
                txtObservacao.Text = string.Empty;
                txtDataInicioCad.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy 08:00");
                txtDataFimCad.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy 18:00");
                chkDomingo.Checked = false;
                chkSabado.Checked = false;
                chkFeriado.Checked = false;
                ddlTipoVisitante.Enabled = true;
                ddlTipoVisitante.SelectedIndex = 0;
                ddlLista.SelectedIndex = 0;
                ddlLista.Visible = false;
                lblLista.Visible = false;
                txtNomeVisitanteCad.Text = string.Empty;
                ddlTipoDocumento.SelectedIndex = 0;
                txtDocumento.Text = string.Empty;
                ddlEmpresaCad.SelectedIndex = 0;
                txtOutraEmpresa.Text = string.Empty;
                lblOutraEmpresa.Visible = false;
                txtOutraEmpresa.Visible = false;
                ddlEstado.SelectedIndex = 0;
                ddlPlaca.SelectedIndex = 0;
                ddlPlaca.Enabled = false;
                txtPlaca.Text = string.Empty;
                txtPlaca.Visible = false;
                ddlAprovadorCad.Enabled = false;
                ddlAprovadorCad.SelectedIndex = 0;
                txtREAprovador.Text = string.Empty;
                btnAdicionar.Enabled = true;
                lblMensagem.Visible = false;

                this.lstGrid.Clear();
                this.radGridVisitantes.DataSource = this.lstGrid;
                this.radGridVisitantes.DataBind();
            }
        }

        #endregion

        #region Bind Visitante

        private void BindVisitante(Enums.TipoBind pintTipoBind)
        {
            radGridVisitantes.DataSource = this.lstGrid;

            if (pintTipoBind == Enums.TipoBind.DataBind)
            {
                radGridVisitantes.DataBind();
            }
        }

        #endregion

        #region DataGrid

        #region NeedDataSource

        protected void radGridVisitantes_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindVisitante(Enums.TipoBind.SemDataBind);
            }
        }

        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridVisitantes_ItemCommand
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 14/03/2011
        ///     quando ediçao de funcionário, ddlLista nao deve estar visivel.
        /// </history>  
        protected void radGridVisitantes_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Excluir")
            {
                for (int i = 0; i < this.lstGrid.Count; i++)
                {
                    if (this.lstGrid[i].NomeVisitante == radGridVisitantes.Items[Convert.ToInt32(e.Item.ItemIndex)]["NomeVisitante"].Text &&
                        (this.lstGrid[i].NumeroDocumento == null ? "&nbsp;" : this.lstGrid[i].NumeroDocumento) == radGridVisitantes.Items[Convert.ToInt32(e.Item.ItemIndex)]["NumeroDocumento"].Text)
                    {
                        this.lstGrid.RemoveAt(i);
                    }
                }

                if (this.lstGrid.Count == 0)
                {
                    ddlAprovadorCad.Enabled = false;
                    ddlAprovadorCad.SelectedIndex = 0;

                    if (this.IdSolicitacao > 0)
                    {
                        btnAdicionar.Enabled = true;
                    }
                }
                else
                {
                    this.PopularAprovadores();
                }

                radGridVisitantes.DataSource = this.lstGrid;
                radGridVisitantes.DataBind();
            }

            if (e.CommandName.Trim() == "Lista")
            {
                this.RadWindowColaboradoresLista(Convert.ToInt32(e.Item.Cells[3].Text));
            }

            if (e.CommandName.Trim() == "Editar")
            {
                int i = Convert.ToInt32(e.Item.ItemIndex);

                this.IdVisitante = this.lstGrid[i].CodVisitante;
                BLUtilitarios.ConsultarValorCombo(ref ddlRegionalCad, this.lstGrid[i].CodRegional.ToString());
                BLUtilitarios.ConsultarValorCombo(ref ddlFilialCad, this.lstGrid[i].CodFilial.ToString());
                this.IdMotivoVisita = this.lstGrid[i].CodMotivoVisita;
                lstAreaSelecionada.Items.Clear();
                lstAreaVisita.Items.Clear();

                this.PopularArea();

                string[] strCodArea;

                strCodArea = this.lstGrid[i].CodArea.Split(new string[] { "," }, StringSplitOptions.None);

                for (int intCont = lstAreaVisita.Items.Count - 1; intCont >= 0; intCont--)
                {
                    for (int intCont2 = 0; intCont2 < strCodArea.Length; intCont2++)
                    {
                        if (strCodArea[intCont2] == lstAreaVisita.Items[intCont].Value.Substring(0, lstAreaVisita.Items[intCont].Value.Length - 1))
                        {
                            lstAreaSelecionada.Items.Add(new ListItem(lstAreaVisita.Items[intCont].Text, lstAreaVisita.Items[intCont].Value));
                            lstAreaVisita.Items.RemoveAt(intCont);
                        }
                    }
                }

                if (this.lstGrid[i].Observacao != string.Empty)
                {
                    txtObservacao.Text = this.lstGrid[i].Observacao;
                }
                txtDataInicioCad.Text = this.lstGrid[i].InicioVisita.ToString("dd/MM/yyy HH:mm");
                txtDataFimCad.Text = this.lstGrid[i].FimVisita.ToString("dd/MM/yyy HH:mm");
                chkDomingo.Checked = this.lstGrid[i].AcDomingo;
                chkSabado.Checked = this.lstGrid[i].AcSabado;
                chkFeriado.Checked = this.lstGrid[i].AcFeriado;

                if (Convert.ToString(this.lstGrid[i].CodVisitante) == "-1")
                {
                    BLUtilitarios.ConsultarTextoCombo(ref ddlTipoVisitante, "Lista");

                    ddlLista.Visible = true;
                    lblLista.Visible = true;

                    BLUtilitarios.ConsultarValorCombo(ref ddlLista, this.lstGrid[i].CodLista.ToString());
                }
                else
                {

                    ddlLista.Visible = false;
                    lblLista.Visible = false;

                    BLUtilitarios.ConsultarValorCombo(ref ddlTipoVisitante, this.lstGrid[i].CodTipoColaborador.ToString());
                    BLUtilitarios.ConsultarValorCombo(ref ddlTipoDocumento, this.lstGrid[i].CodTipoDocumento.ToString());
                    txtNomeVisitanteCad.Text = this.lstGrid[i].NomeVisitante;

                    if (ddlTipoVisitante.SelectedItem.Text != "Visitante")
                    {
                        if (ddlTipoDocumento.Items[ddlTipoDocumento.Items.Count - 1].Text != "RE")
                        {
                            ddlTipoDocumento.Items.Add(new ListItem("RE"));
                        }

                        BLUtilitarios.ConsultarTextoCombo(ref ddlTipoDocumento, "RE");
                    }
                    else
                    {
                        BLUtilitarios.ConsultarValorCombo(ref ddlTipoDocumento, this.lstGrid[i].CodTipoDocumento.ToString());
                    }

                    txtDocumento.Text = this.lstGrid[i].NumeroDocumento;

                    if (this.lstGrid[i].CodTipoColaborador == 1 && this.lstGrid[i].CodVisitante == 0)
                    {
                        txtNomeVisitanteCad.Enabled = true;
                        ddlTipoDocumento.Enabled = true;
                        txtDocumento.Enabled = true;
                        ddlEmpresaCad.Enabled = true;
                    }

                    if (this.lstGrid[i].CodEmpresa == 0)
                    {
                        BLUtilitarios.ConsultarTextoCombo(ref ddlEmpresaCad, "Outra...");
                        lblOutraEmpresa.Visible = true;
                        txtOutraEmpresa.Visible = true;
                        txtOutraEmpresa.Text = this.lstGrid[i].Empresa;
                    }
                    else
                    {
                        BLUtilitarios.ConsultarValorCombo(ref ddlEmpresaCad, this.lstGrid[i].CodEmpresa.ToString());
                    }
                }

                BLUtilitarios.ConsultarValorCombo(ref ddlEstado, this.lstGrid[i].CodEstadoVeiculo.ToString());
                BLUtilitarios.ConsultarValorCombo(ref ddlMotivoVisita, this.IdMotivoVisita.ToString());

                if (this.lstGrid[i].CodVeiculo == 0)
                {
                    BLUtilitarios.ConsultarTextoCombo(ref ddlPlaca, "Outra...");
                    txtPlaca.Visible = true;
                    ddlPlaca.Enabled = true;
                    txtPlaca.Text = this.lstGrid[i].PlacaVeiculo;
                }
                else
                {
                    if (this.lstGrid[i].CodVeiculo != -1)
                    {
                        BLUtilitarios.ConsultarValorCombo(ref ddlPlaca, this.lstGrid[i].CodVeiculo.ToString());
                        ddlPlaca.Enabled = true;
                    }
                }

                this.lstGrid.RemoveAt(i);

                if (this.lstGrid.Count == 0)
                {
                    ddlAprovadorCad.Enabled = false;
                    ddlAprovadorCad.SelectedIndex = 0;

                    if (this.IdSolicitacao > 0)
                    {
                        btnAdicionar.Enabled = true;
                    }
                }
                else
                {
                    this.PopularAprovadores();
                }

                radGridVisitantes.DataSource = this.lstGrid;
                radGridVisitantes.DataBind();

                BLUtilitarios.ConsultarValorCombo(ref ddlMotivoVisita, this.IdMotivoVisita.ToString());
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

        #region ItemDataBound

        protected void radGridVisitantes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                LinkButton lnkVisitante;
                lnkVisitante = (LinkButton)(e.Item.FindControl("lnkVisitante"));
                lnkVisitante.Text = e.Item.Cells[5].Text;
                lnkVisitante.Enabled = false;
                if (e.Item.Cells[2].Text == "-1")
                    lnkVisitante.Enabled = true;

                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");
                btnEditar.Visible = false;
                e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                e.Item.Style["cursor"] = "hand";

                // Cursor 
                int intCell = e.Item.Cells.Count;
                for (int @int = 0; @int <= intCell - 2; @int++)
                {
                    if (@int != 4)
                    {
                        e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                    }
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
        }

        #endregion

        #endregion

        #region Botões

        /// <summary>
        ///     Abre a RadWindow com os colaboradores que podem ser visitados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnListarVisitado_Click(object sender, ImageClickEventArgs e)
        {
            this.RadWindowVisitados();
        }

        /// <summary>
        ///     Abre a RadWindow com os colaboradores que podem visitar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnListarVisitante_Click1(object sender, ImageClickEventArgs e)
        {
            this.RadWindowVisitantes();
        }

        /// <summary>
        ///     Volta para a tela de Listagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.ControlaPaineis(Enums.TipoPainel.Listagem);
            lblMensagem.Visible = false;
            this.BindSolicitacao(Enums.TipoBind.DataBind);
        }

        /// <summary>
        ///     Grava a soliciação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        ///     [haguiar] modify 05/11/2010
        ///     [haguiar_3] modify 11/01/2011
        /// </history>
        protected void btnGravar_Click(object sender, EventArgs e)
        {
            bool blnGravar = false;

            if (this.IdVisitado > 0)
            {
                if (ddlAprovadorCad.SelectedIndex > 0 && this.lstGrid.Count > 0)
                {
                    if (VerificarAprovador())
                    {
                        blnGravar = true;
                    }
                }
                else
                {
                    blnGravar = false;
                    RadAjaxPanel1.Alert("Insira pelo menos uma solicitação.");
                }
            }

            if (blnGravar)
                this.Gravar();
        }

        /// <summary>
        ///     Adiciona um visitante na lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (txtNomeVisitadoCad.Text != string.Empty)
            {
                if (lstAreaSelecionada.Items.Count > 0)
                {
                    if (this.ValidarCampos())
                    {
                        if (this.VerificarVisitante() == false)
                        {
                            if (this.VerificarCheckbox(Convert.ToDateTime(txtDataInicioCad.Text), Convert.ToDateTime(txtDataFimCad.Text)))
                            {
                                bool blnAdicionar = true;
                                this.BindModel(Enums.TipoTransacao.CarregarDados);

                                //verifica se é uma nova empresa
                                if (gobjSolicitacaoColaborador.CodEmpresa == 0)
                                {
                                    BLEmpresa objBlEmpresa = new BLEmpresa();
                                    Empresa objEmpresa = new Empresa();

                                    objEmpresa.DescricaoEmpresa = gobjSolicitacaoColaborador.Empresa;

                                    gobjSolicitacaoColaborador.CodEmpresa = objBlEmpresa.Inserir(objEmpresa);

                                    //verifica se a empresa já existe
                                    if (gobjSolicitacaoColaborador.CodEmpresa == -1)
                                    {
                                        RadAjaxPanelVisitante.Alert("A empresa " + objEmpresa.DescricaoEmpresa + " já existe");
                                        blnAdicionar = false;
                                    }
                                    else
                                    {
                                        this.PopularEmpresa();
                                    }
                                }

                                //verifica se foi inserida alguma placa
                                if (gobjSolicitacaoColaborador.CodVeiculo != -1)
                                {
                                    //verifica se a placa já existe
                                    if (gobjSolicitacaoColaborador.CodVeiculo == 0)
                                    {
                                        BLVeiculo objBlVeiculo = new BLVeiculo();
                                        Veiculo objVeiculo = new Veiculo();

                                        objVeiculo.DescricaoPlaca = gobjSolicitacaoColaborador.PlacaVeiculo;
                                        objVeiculo.IdEmpresa = gobjSolicitacaoColaborador.CodEmpresa;
                                        objVeiculo.IdUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());
                                        objVeiculo.IdEstado = Convert.ToInt32(gobjSolicitacaoColaborador.CodEstadoVeiculo);

                                        gobjSolicitacaoColaborador.CodVeiculo = objBlVeiculo.Inserir(objVeiculo);

                                        if (gobjSolicitacaoColaborador.CodVeiculo == -1)
                                        {
                                            RadAjaxPanelVisitante.Alert("A placa " + objVeiculo.DescricaoPlaca + " já existe");
                                            blnAdicionar = false;
                                        }
                                    }
                                }
                                /*
                                else
                                {
                                    gobjSolicitacaoColaborador.CodVeiculo = 0;
                                }
                                */

                                if (blnAdicionar)
                                {
                                    //adiciona o novo registro
                                    this.lstGrid.Add(gobjSolicitacaoColaborador);
                                    this.BindVisitante(Enums.TipoBind.DataBind);

                                    this.PopularAprovadores();
                                    ddlAprovadorCad.Enabled = true;

                                    this.LimparDadosVisitante();

                                    txtNomeVisitanteCad.Enabled = false;
                                    ddlTipoDocumento.Enabled = false;
                                    txtDocumento.Enabled = false;
                                    ddlEmpresaCad.Enabled = false;
                                }
                            }

                            //se for alteração desabilita o botão
                            if (this.IdSolicitacao > 0)
                            {
                                btnAdicionar.Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    RadAjaxPanelVisitante.Alert("Selecione pelo menos um área.");
                }
            }
            else
            {
                RadAjaxPanelVisitante.Alert("Selecione o visitado.");
            }
        }

        /// <summary>
        ///     Adiciona todas as areas no listBox AreasSelecionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnAddTodos_Click(object sender, EventArgs e)
        {
            for (int i = lstAreaVisita.Items.Count; i > 0; i--)
            {
                lstAreaSelecionada.Items.Add(new ListItem(lstAreaVisita.Items[lstAreaVisita.Items.Count - 1].Text, lstAreaVisita.Items[lstAreaVisita.Items.Count - 1].Value));
                lstAreaVisita.Items.RemoveAt(lstAreaVisita.Items.Count - 1);
            }
        }

        /// <summary>
        ///     Adiciona a area selecionada no listBox AreasSelecionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnAddUm_Click(object sender, EventArgs e)
        {
            if (lstAreaVisita.SelectedIndex > -1)
            {
                int intCount = lstAreaVisita.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstAreaVisita.Items[i].Selected)
                    {
                        lstAreaSelecionada.Items.Add(new ListItem(lstAreaVisita.Items[i].Text, lstAreaVisita.Items[i].Value));
                        lstAreaVisita.Items.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        ///     Remove a area selecionada no listBox AreasSelecionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnRemoverUm_Click(object sender, EventArgs e)
        {
            if (lstAreaSelecionada.SelectedIndex > -1)
            {
                int intCount = lstAreaSelecionada.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstAreaSelecionada.Items[i].Selected)
                    {
                        lstAreaVisita.Items.Add(new ListItem(lstAreaSelecionada.Items[i].Text, lstAreaSelecionada.Items[i].Value));
                        lstAreaSelecionada.Items.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        ///     Remove todas as areas no listBox AreasSelecionadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void btnRemoverTodos_Click(object sender, EventArgs e)
        {
            for (int i = lstAreaSelecionada.Items.Count; i > 0; i--)
            {
                lstAreaVisita.Items.Add(new ListItem(lstAreaSelecionada.Items[lstAreaSelecionada.Items.Count - 1].Text, lstAreaSelecionada.Items[lstAreaSelecionada.Items.Count - 1].Value));
                lstAreaSelecionada.Items.RemoveAt(lstAreaSelecionada.Items.Count - 1);
            }
        }

        /// <summary>
        ///     Limpa os campos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 08/07/2009
        /// </history>
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            this.LimparDadosVisitante();
            this.DesabilitarCampos();
            ddlTipoVisitante.Enabled = true;
        }

        #endregion

        #region Verificar Visitante

        /// <summary>
        ///     Verifica se o Visitado é diferente do Visitante e se o Visitante ainda
        ///     não foi adicionado na lista.
        /// </summary>
        /// <history>
        ///     [mribeiro] created 21/07/2009
        /// </history> 
        /// <returns>True/False</returns>
        protected bool VerificarVisitante()
        {
            bool blnExiste = false;

            if (this.lstGrid.Count == 0)
            {
                if (ddlLista.Visible == false)
                {
                    if (this.IdVisitante == this.IdVisitado)
                    {
                        blnExiste = true;
                        RadAjaxPanelVisitante.Alert("O Visitante não pode ser o mesmo que o Visitado.");
                        RadAjaxPanelVisitado.Alert("O Visitante não pode ser o mesmo que o Visitado.");
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.lstGrid.Count; i++)
                {
                    //verifica se a lista já foi adicionada
                    if (ddlLista.Visible)
                    {
                        if (this.lstGrid[i].CodLista == Convert.ToInt32(ddlLista.SelectedItem.Value))
                        {
                            blnExiste = true;
                            RadAjaxPanelVisitante.Alert("Essa lista já foi adicionado.");
                            break;
                        }
                    }
                    else
                    {
                        //verifica se o visitante é o mesmo que o visitado
                        if (this.IdVisitante == this.IdVisitado)
                        {
                            blnExiste = true;
                            RadAjaxPanelVisitante.Alert("O Visitante não pode ser o mesmo que o Visitado.");
                            RadAjaxPanelVisitado.Alert("O Visitante não pode ser o mesmo que o Visitado.");
                            break;

                        }

                        //verifica se o visitante já foi adicionado
                        if (this.lstGrid[i].CodVisitante == this.IdVisitante)
                        {
                            //verifica se não é um novo cadastro
                            if (this.IdVisitante > 0)
                            {
                                blnExiste = true;
                                RadAjaxPanelVisitante.Alert("Esse visitante já foi adicionado.");
                                RadAjaxPanelVisitado.Alert("Esse visitante já foi adicionado.");
                                this.LimparDadosVisitante();
                                break;
                            }
                        }
                    }
                }
            }

            return blnExiste;
        }

        #endregion

        #region Validar Campos

        /// <summary>
        ///     Verifica se os campos foram preenchidos corretamente
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     [mribeiro] created 20/07/2009
        /// </history>
        protected bool ValidarCampos()
        {
            bool blnRetorno = true;

            if (ddlTipoVisitante.SelectedIndex > 0)
            {
                if (ddlTipoVisitante.SelectedItem.Text == "Lista")
                {
                    if (ddlLista.SelectedIndex == 0)
                    {
                        RadAjaxPanelVisitante.Alert("Selecione uma Lista.");
                        blnRetorno = false;
                    }
                    else if (ddlPlaca.SelectedItem.Text == "Outra...")
                    {
                        if (txtPlaca.Text == string.Empty)
                        {
                            RadAjaxPanelVisitante.Alert("Insira o número da placa.");
                            blnRetorno = false;
                        }
                    }
                }
                else
                {
                    if (txtNomeVisitanteCad.Text == string.Empty)
                    {
                        RadAjaxPanelVisitante.Alert("Insira um visitante.");
                        blnRetorno = false;
                    }
                    else if (ddlTipoDocumento.SelectedIndex == 0)
                    {
                        RadAjaxPanelVisitante.Alert("Selecione um tipo de documento.");
                        blnRetorno = false;
                    }
                    else if (txtDocumento.Text == string.Empty)
                    {
                        RadAjaxPanelVisitante.Alert("Insira o número do documento.");
                        blnRetorno = false;
                    }
                    else if (ddlPlaca.SelectedItem.Text == "Outra...")
                    {
                        if (txtPlaca.Text == string.Empty)
                        {
                            RadAjaxPanelVisitante.Alert("Insira o número da placa.");
                            blnRetorno = false;
                        }
                    }
                    else if (ddlEmpresaCad.SelectedIndex > 0)
                    {
                        if (ddlEmpresaCad.SelectedItem.Text == "Outra...")
                        {
                            if (txtOutraEmpresa.Text == string.Empty)
                            {
                                RadAjaxPanelVisitante.Alert("Insira o nome da empresa.");
                                blnRetorno = false;
                            }
                        }
                    }
                    else
                    {
                        RadAjaxPanelVisitante.Alert("Selecione uma empresa.");
                        blnRetorno = false;
                    }
                }
            }
            else
            {
                RadAjaxPanelVisitante.Alert("Selecione um tipo de visitante.");
                blnRetorno = false;
            }

            if (blnRetorno)
            {
                try
                {
                    if (txtDataInicioCad.Text.Length == 16)
                    {
                        if (txtDataFimCad.Text.Length == 16)
                        {
                            DateTime datInicio, datFim;

                            datInicio = DateTime.Parse(txtDataInicioCad.Text);
                            datFim = DateTime.Parse(txtDataFimCad.Text);

                            if (datInicio.Date < DateTime.Today.Date)
                            {
                                RadAjaxPanelVisitante.Alert("A Data Início deve ser maior ou igual a de hoje.");
                                blnRetorno = false;
                            }
                            else if (datFim.Date < datInicio.Date)
                            {
                                RadAjaxPanelVisitante.Alert("A Data Fim deve ser maior ou igual a Data Inicial.");
                                blnRetorno = false;
                            }
                        }
                        else
                        {
                            RadAjaxPanelVisitante.Alert("Deve ser informada Data e Hora do Final da visita.");
                            blnRetorno = false;
                        }
                    }
                    else
                    {
                        RadAjaxPanelVisitante.Alert("Deve ser informada Data e Hora de início da visita.");
                        blnRetorno = false;
                    }


                    if (this.txtDataInicioCad.Text != string.Empty && this.txtDataFimCad.Text != string.Empty)
                    {
                        try
                        {
                            //verifica o intervalo da solicitação - nao deve ultrapassar 31 dias.
                            DateTime dtInicio = Convert.ToDateTime(this.txtDataInicioCad.Text.ToString());
                            DateTime dtFinal = Convert.ToDateTime(this.txtDataFimCad.Text.ToString());

                            //inicio
                            TimeSpan ts = dtFinal - dtInicio;

                            if (ts.Days > 31)
                            {
                                //passou o limite

                                blnRetorno = false;
                                RadAjaxPanelVisitante.Alert("Intervalo da solicitação não deve ultrapassar 31 dias.");
                            }
                        }
                        catch (Exception ex)
                        {
                            ex = null;
                            blnRetorno = false;

                            RadAjaxPanelVisitante.Alert("Verifique o intervalo da solicitação.");
                        }

                    }

                }
                catch (Exception ex)
                {
                    blnRetorno = false;
                    RadAjaxPanelVisitante.Alert("Data Inválida.");
                }
            }

            return blnRetorno;
        }

        #endregion

        #region Limpar Dados Visitante

        /// <summary>
        ///     Limpa os campos dos Dados do Visitante
        /// </summary>
        /// <history>
        ///     [mribeiro] 16/07/2009 created
        /// </history>
        protected void LimparDadosVisitante()
        {
            HiddenVisitante.Value = string.Empty;
            this.IdVisitante = 0;
            ddlTipoVisitante.Enabled = true;
            ddlLista.SelectedIndex = 0;

            if (ddlTipoVisitante.Text == "Lista" && this.IdSolicitacao == 0)
            {
                ddlLista.Visible = true;
                lblLista.Visible = true;
            }
            else
            {
                ddlLista.Visible = false;
                lblLista.Visible = false;
            }

            if (this.IdSolicitacao > 0)
            {
                ddlTipoVisitante.SelectedIndex = 0;
            }

            if (this.ddlListaVeiculos.Items.Count > 0)
                ddlListaVeiculos.SelectedIndex = 0;

            ddlListaVeiculos.Enabled = true;
            txtNomeVisitanteCad.Text = string.Empty;
            ddlTipoDocumento.SelectedIndex = 0;
            txtDocumento.Text = string.Empty;
            ddlEmpresaCad.SelectedIndex = 0;
            txtOutraEmpresa.Text = string.Empty;
            lblOutraEmpresa.Visible = false;
            txtOutraEmpresa.Visible = false;
            ddlEstado.SelectedIndex = 0;
            ddlPlaca.SelectedIndex = 0;
            ddlPlaca.Enabled = false;
            txtPlaca.Text = string.Empty;
            txtPlaca.Visible = false;
        }

        #endregion

        #region RadWindow

        /// <summary>
        ///     Abre a RadWindow com a tela de listagem de colaboradores
        /// </summary>
        /// <history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void RadWindowVisitantes()
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

            if (ddlTipoVisitante.SelectedItem.Text.Trim() == "Visitante")
            {
                rwdWindow.Title = "Selecione um colaborador ou clique em fechar para adicionar um novo.";
            }
            else
            {
                rwdWindow.Title = "Selecione um colaborador ou clique em fechar para sair da tela.";
            }

            rwdWindow.NavigateUrl = "ListColaboradoresSol.aspx?Tipo=" + ddlTipoVisitante.SelectedItem.Value.ToString();
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlVisitante = null;

            //Tenta encontrar na master
            pnlVisitante = (Panel)this.FindControl("pnlListaVisitante");
            pnlVisitante.Controls.Add(rwmWindowManager);
        }

        /// <summary>
        ///     Abre a RadWindow com a tela de listagem de colaboradores 
        /// </summary>
        /// <history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void RadWindowVisitados()
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

            rwdWindow.NavigateUrl = "ListColaboradoresSol.aspx";
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlVisitado = null;

            //Tenta encontrar na master
            pnlVisitado = (Panel)this.FindControl("pnlListaVisitado");
            pnlVisitado.Controls.Add(rwmWindowManager);
        }

        /// <summary>
        ///     Abre a RadWindow com a tela de listagem de colaboradores 
        /// </summary>
        /// <history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void RadWindowColaboradoresLista(int pintIdLista)
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

            rwdWindow.Title = "Listagem de Colaboradores da Lista.";

            HiddenLista.Value = pintIdLista.ToString();

            for (int i = 0; i < this.lstGrid.Count; i++)
            {
                if (lstGrid[i].CodLista == pintIdLista)
                {
                    rwdWindow.NavigateUrl = "ListColaboradoresLista.aspx?CodLista=" + pintIdLista.ToString() + "&Inclusos=" + lstGrid[i].CodInclusos;
                    rwmWindowManager.Windows.Add(rwdWindow);
                    break;
                }
            }

            Panel pnlVisitante = null;

            //Tenta encontrar na master
            pnlVisitante = (Panel)this.FindControl("pnlLista");
            pnlVisitante.Controls.Add(rwmWindowManager);
        }

        #endregion

        #region Eventos

        /// <summary>
        ///     Se a opção "Outra" for selecionada, exibe um textbox para cadastro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void ddlEmpresaCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpresaCad.SelectedItem.Text.Equals("Outra..."))
            {
                lblOutraEmpresa.Visible = true;
                txtOutraEmpresa.Visible = true;
            }
            else
            {
                lblOutraEmpresa.Visible = false;
                txtOutraEmpresa.Visible = false;
            }
        }

        /// <summary>
        ///     Se a opção "Lista" for selecionada, exibe o combo com as listas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void ddlTipoVisitanteCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoVisitante.SelectedItem.Text.Trim() == "Lista")
            {
                lblLista.Visible = true;
                ddlLista.Visible = true;
                btnListarVisitante.Enabled = false;
            }
            else
            {
                lblLista.Visible = false;
                ddlLista.Visible = false;
                btnListarVisitante.Enabled = true;

                if (ddlTipoVisitante.SelectedItem.Text == "Visitante")
                {
                    if (ddlTipoDocumento.Items[ddlTipoDocumento.Items.Count - 1].Text == "RE")
                    {
                        ddlTipoDocumento.Items.RemoveAt(ddlTipoDocumento.Items.Count - 1);
                    }
                }
                else
                {
                    if (ddlTipoDocumento.Items[ddlTipoDocumento.Items.Count - 1].Text != "RE")
                    {
                        ddlTipoDocumento.Items.Add(new ListItem("RE"));
                    }
                }
            }
        }

        /// <summary>
        ///     Popula o combo Filial conforme Regional selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// history>
        ///     [mribeiro] created 06/07/2009
        /// </history>
        protected void ddlRegionalCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAreaVisita.Items.Count > 0)
            {
                lstAreaSelecionada.Items.Clear();
                lstAreaVisita.Items.Clear();
            }

            if (ddlLista.Enabled)
            {
                ddlLista.Items.Clear();
                BLUtilitarios.InseriMensagemDropDownList(ref ddlLista, "<-- Selecione uma Filial -->", false, 0);
                ddlLista.SelectedIndex = 0;
                ddlLista.Enabled = false;
            }

            this.PopularFilial(ref ddlRegionalCad, ref ddlFilialCad);
        }

        /// <summary>
        ///     Popula Combo com as áreas da filial selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 07/07/2009
        /// </history>
        protected void ddlFilialCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularLista();
            this.PopularArea();
            this.PopulaListaVeiculos();
        }

        /// <summary>
        ///     Formata o campo Documento conforme Tipo de Documento selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] created 08/07/2009
        /// </history>
        protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoDocumento.SelectedItem.Text.Trim() == "RG" || ddlTipoDocumento.SelectedItem.Text.Trim() == "Passaporte")
            {
                txtDocumento.Enabled = true;
                txtDocumento.MaxLength = 15;
                txtDocumento.Mascara = global::FrameWork.WebControl.FWMascara.TipoMascara.NENHUMA;
            }
            else
            {
                txtDocumento.Enabled = false;
            }
        }

        /// <summary>
        ///     Preenche o campo Placa com conforme o estado selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] 13/07/2009 created 
        /// </history>
        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstado.SelectedIndex > 0)
            {
                this.PopularPlaca();
                ddlPlaca.Enabled = true;
                ddlListaVeiculos.Enabled = false;
                if (ddlListaVeiculos.SelectedIndex > 0)
                    ddlListaVeiculos.SelectedIndex = 0;
            }
            else
            {
                ddlPlaca.SelectedIndex = 0;
                ddlPlaca.Enabled = false;
                ddlListaVeiculos.Enabled = true;
            }

        }

        protected void ddlListaVeiculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlListaVeiculos.SelectedIndex > 0)
            {
                ddlEstado.Enabled = false;
                if (ddlEstado.SelectedIndex > 0)
                    ddlEstado.SelectedIndex = 0;
            }
            else
            {
                ddlEstado.Enabled = true;
            }

        }

        /// <summary>
        ///     Se a opção "Outra" for selecionada, exibe um textbox para cadastro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] 13/07/2009 created 
        /// </history>
        protected void ddlPlaca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlaca.SelectedItem.Text.Equals("Outra..."))
            {
                txtPlaca.Visible = true;
            }
            else
            {
                txtPlaca.Visible = false;
            }
        }

        /// <summary>
        ///     Preenche o RE do aprovador selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] 16/07/2009 created 
        /// </history>
        protected void ddlAprovadorCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAprovadorCad.SelectedIndex > 0)
            {
                BLSolicitacao objBlSolicitacao = new BLSolicitacao();

                DataTable dtt = objBlSolicitacao.ObterCodigoAprovador(Convert.ToInt32(ddlAprovadorCad.SelectedItem.Value));
                txtREAprovador.Text = dtt.Rows[0][1].ToString();
            }
            else
            {
                txtREAprovador.Text = string.Empty;
            }
        }

        /// <summary>
        ///     Preenche com o motivo da visita selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [mribeiro] 20/07/2009 created 
        /// </history>
        protected void ddlMotivoVisita_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IdMotivoVisita = Convert.ToInt32(ddlMotivoVisita.SelectedItem.Value);
        }

        #endregion

        #region Obter Colaborador

        /// <summary>
        ///     Obtem as informações do colaborador selecionado
        /// </summary>
        /// <history>
        ///     [mribeiro] created 08/07/2009
        /// </history>
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

        #region Obter Solicitacao

        /// <summary>
        ///     Obtem as informações da solicitação selecionado
        /// </summary>
        /// <history>
        ///     [mribeiro] created 20/07/2009
        /// </history>
        protected void ObterSolicitacao()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();

            try
            {
                gobjSolicitacaoColaborador = new SolicitacaoColaborador();
                gobjSolicitacaoColaborador = objBlSolicitacao.ObterSolicitacao(this.IdSolicitacao);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Desabilitar Campos

        /// <summary>
        ///     Desabilita os campos
        /// </summary>
        /// <history>
        ///     [mribeiro] created 08/07/2009
        /// </history>
        protected void DesabilitarCampos()
        {
            ddlTipoVisitante.Enabled = false;
            txtNomeVisitanteCad.Enabled = false;
            ddlTipoDocumento.Enabled = false;
            txtDocumento.Enabled = false;
            ddlEmpresaCad.Enabled = false;
        }

        #endregion

        #region Habilitar Campos

        /// <summary>
        ///     Habilita os campos
        /// </summary>
        /// <history>
        ///     [mribeiro] created 08/07/2009
        /// </history>
        protected void HabilitarCampos()
        {
            ddlTipoVisitante.Enabled = true;
            txtNomeVisitanteCad.Enabled = true;
            ddlTipoDocumento.Enabled = true;
            txtDocumento.Enabled = true;
            ddlEmpresaCad.Enabled = true;
        }

        #endregion

        #region Gravar

        protected void Gravar()
        {
            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            gobjSolicitacao = new SafWeb.Model.Solicitacao.Solicitacao();
            bool blnGravar = true;

            try
            {
                for (int i = 0; i < this.lstGrid.Count; i++)
                {
                    //verifica se o colaborador já existe
                    if (this.lstGrid[i].CodVisitante == 0)
                    {

                        BLColaborador objBlColaborador = new BLColaborador();
                        gobjColaborador = new Colaborador();

                        gobjColaborador.IdTipoColaborador = this.lstGrid[i].CodTipoColaborador;
                        gobjColaborador.NomeColaborador = this.lstGrid[i].NomeVisitante;
                        gobjColaborador.IdEmpresa = this.lstGrid[i].CodEmpresa;
                        gobjColaborador.NumeroDocumento = this.lstGrid[i].NumeroDocumento;
                        gobjColaborador.IdTipoDocumento = this.lstGrid[i].CodTipoDocumento;

                        //insere o colaborador
                        gobjColaborador.IdColaborador = objBlColaborador.Inserir(gobjColaborador);
                        //insere o documento do colaborador
                        objBlColaborador.InserirDocumentoVisitante(gobjColaborador);

                        this.lstGrid[i].CodVisitante = gobjColaborador.IdColaborador;

                    }
                }

                bool blnAlterar = true;
                string strSolicitacao = "";

                if (blnGravar)
                {
                    for (int i = 0; i < this.lstGrid.Count; i++)
                    {

                        //preenche a model solicitação
                        gobjSolicitacao.CodUsuSolic = Convert.ToInt32(BLAcesso.IdUsuarioLogado());
                        gobjSolicitacao.CodVisitado = this.lstGrid[i].CodVisitado;
                        gobjSolicitacao.CodVisitante = this.lstGrid[i].CodVisitante;
                        gobjSolicitacao.InicioVisita = this.lstGrid[i].InicioVisita;
                        gobjSolicitacao.FimVisita = this.lstGrid[i].FimVisita;
                        gobjSolicitacao.AcDomingo = this.lstGrid[i].AcDomingo;
                        gobjSolicitacao.AcSabado = this.lstGrid[i].AcSabado;
                        gobjSolicitacao.AcFeriado = this.lstGrid[i].AcFeriado;
                        gobjSolicitacao.CodArea = this.lstGrid[i].CodArea;
                        gobjSolicitacao.CodTipoSolicitacao = this.lstGrid[i].CodTipoSolicitacao;
                        gobjSolicitacao.CodMotivoVisita = this.lstGrid[i].CodMotivoVisita;
                        gobjSolicitacao.Observacao = this.lstGrid[i].Observacao;
                        gobjSolicitacao.CodVeiculo = this.lstGrid[i].CodVeiculo;

                        if (this.IdSolicitacao == 0)
                        {
                            //insere a solicitação.
                            gobjSolicitacao.Codigo = objBlSolicitacao.Inserir(gobjSolicitacao);
                        }
                        else
                        {
                            gobjSolicitacao.Codigo = this.IdSolicitacao;

                            //altera a solicitação.
                            //Solicitação Reprovada não altera
                            BLSolicitacao gobjBLSolicitacao = new BLSolicitacao();
                            string strStatus;
                            strStatus = gobjBLSolicitacao.ObterUltimoStatus(gobjSolicitacao.Codigo);
                            if (strStatus != "Reprovada")
                            {
                                blnAlterar = objBlSolicitacao.AlterarSolicitacao(gobjSolicitacao,
                                                                                 this.IdListaAnterior);
                            }
                            else
                            {
                                blnAlterar = false;
                            }

                        }

                        if (blnAlterar)
                        {
                            //se for lista
                            if (this.lstGrid[i].CodLista > 0)
                            {
                                //insere as solicitacoes dos colaboradores da lista.
                                objBlSolicitacao.InserirSolicitacaoLista(gobjSolicitacao.Codigo,
                                                                         this.lstGrid[i].CodLista,
                                                                         this.lstGrid[i].CodInclusos);
                            }


                            //TODO Caio
                            //se for lista de veiculos
                            if (this.lstGrid[i].CodListaVeiculos > 0)
                            {
                                objBlSolicitacao.InserirSolicitacaoListaVeiculos(gobjSolicitacao.Codigo, this.lstGrid[i].CodListaVeiculos);
                            }

                            //insere o status da solicitação
                            objBlSolicitacao.InserirStatusSolicitacao(gobjSolicitacao.Codigo,
                                                                      Convert.ToInt32(ddlAprovadorCad.SelectedItem.Value),
                                                                      this.lstGrid[i].CodTipoSolicitacao);


                            strSolicitacao += "Solicitação nº: " + gobjSolicitacao.Codigo.ToString() + " - Visitante: " + this.lstGrid[i].NomeVisitante + " \n";
                        }
                        else
                        {
                            RadAjaxPanel1.Alert("A solicitação " + gobjSolicitacao.Codigo.ToString() + " não pode ser alterada, pois já foi Aprovada/Reprovada.");
                        }
                    }

                    this.BindModel(Enums.TipoTransacao.Novo);

                    if (blnAlterar)
                    {
                        RadAjaxPanel1.Alert(strSolicitacao);
                        lblMensagem.Visible = true;
                        lblMensagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

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
        protected bool VerificarCheckbox(DateTime pdatInicio,
                                         DateTime pdatFim)
        {
            bool blnFer = false, blnSab = false, blnDom = false, blnGravar = true;
            int intUtil = 0, intFeriadoUtil = 0;
            DataTable dttFeriados = new DataTable();

            do
            {
                switch (pdatInicio.DayOfWeek)
                {
                    case DayOfWeek.Sunday: blnDom = true; break;
                    case DayOfWeek.Saturday: blnSab = true; break;
                    case DayOfWeek.Monday: intUtil += 1; break;
                    case DayOfWeek.Thursday: intUtil += 1; break;
                    case DayOfWeek.Tuesday: intUtil += 1; break;
                    case DayOfWeek.Wednesday: intUtil += 1; break;
                    case DayOfWeek.Friday: intUtil += 1; break;
                }

                try
                {
                    BLSolicitacao objBlSolicitacao = new BLSolicitacao();

                    dttFeriados = objBlSolicitacao.ListarFeriado(Convert.ToInt32(ddlRegionalCad.SelectedItem.Value),
                                                                 Convert.ToInt32(ddlFilialCad.SelectedItem.Value));

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }

                //verifica se ainda não foi encontrado nenhum feriado
                if (blnFer == false)
                {
                    foreach (DataRow dtr in dttFeriados.Rows)
                    {
                        if (dtr["Flg_fixo"].ToString() == "1")
                        {
                            if (Convert.ToDateTime(dtr["Dt_Data"]).ToString("dd/MM") == pdatInicio.ToString("dd/MM"))
                            {
                                if (Convert.ToDateTime(dtr["Dt_Data"]).DayOfWeek != DayOfWeek.Saturday &&
                                    Convert.ToDateTime(dtr["Dt_Data"]).DayOfWeek != DayOfWeek.Sunday)
                                {
                                    intFeriadoUtil += 1;
                                }

                                blnFer = true;
                            }
                        }
                        else if (dtr["Flg_fixo"].ToString() == "0")
                        {
                            if (Convert.ToDateTime(dtr["Dt_Data"]) == pdatInicio)
                            {
                                blnFer = true;
                            }
                        }
                    }
                }

                pdatInicio = pdatInicio.AddDays(1);

            } while (pdatInicio <= pdatFim);

            intUtil -= intFeriadoUtil;

            if (blnDom == false)
            {
                if (chkDomingo.Checked)
                {
                    chkDomingo.Checked = false;
                }
            }

            if (blnSab == false)
            {
                if (chkSabado.Checked)
                {
                    chkSabado.Checked = false;
                }
            }

            if (blnFer == false)
            {
                if (chkFeriado.Checked)
                {
                    chkFeriado.Checked = false;
                }
            }

            //verifica se existe algum dia util
            if (intUtil == 0)
            {
                if (chkFeriado.Checked == false && chkSabado.Checked == false && chkDomingo.Checked == false)
                {
                    if (blnFer)
                    {
                        chkFeriado.Checked = true;
                    }

                    if (blnSab)
                    {
                        chkSabado.Checked = true;
                    }

                    if (blnDom)
                    {
                        chkDomingo.Checked = true;
                    }
                }
            }

            return blnGravar;
        }

        #endregion

        #region Verificar Aprovador

        /// <summary>
        ///     Verificar o aprovador 
        /// </summary>
        /// <history>
        ///     [mribeiro] 30/07/2009 created
        /// </history>
        /// <returns></returns>
        protected bool VerificarAprovador()
        {
            bool blnRetorno = true;

            if (BLAcesso.IdUsuarioLogado() == Convert.ToInt32(this.ddlAprovadorCad.SelectedItem.Value))
            {
                RadAjaxPanel1.Alert("O Aprovador não pode ser o mesmo que o Usuário Logado.");
                blnRetorno = false;
            }
            else
            {
                BLSolicitacao objBlSolicitacao = new BLSolicitacao();
                DataTable dtt = new DataTable();

                try
                {
                    dtt = objBlSolicitacao.ObterCodigoAprovador(Convert.ToInt32(this.ddlAprovadorCad.SelectedItem.Value));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }

                string strLista = "";

                for (int i = 0; i < this.lstGrid.Count; i++)
                {
                    if (this.lstGrid[i].CodVisitante == Convert.ToInt32(dtt.Rows[0][0]))
                    {
                        RadAjaxPanel1.Alert("O Aprovador não pode ser o mesmo que o Visitante.");
                        blnRetorno = false;
                        break;
                    }
                    else
                    {
                        if (this.lstGrid[i].CodLista > 0)
                        {
                            if (this.lstGrid[i].CodInclusos == null)
                            {
                                if (strLista == "")
                                    strLista = this.lstGrid[i].CodLista.ToString();
                                else
                                    strLista += "," + this.lstGrid[i].CodLista.ToString();
                            }
                            else
                            {

                                string[] strInclusos = lstGrid[i].CodInclusos.Split(new char[] { ',' });

                                for (int j = 0; j < strInclusos.Length; j++)
                                {
                                    if (dtt.Rows[0][0].ToString() == strInclusos[j])
                                    {
                                        RadAjaxPanel1.Alert("O Aprovador já existe em uma lista selecionada.");
                                        blnRetorno = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }


                    if (blnRetorno)
                    {
                        BLLista objBlLIsta = new BLLista();

                        try
                        {
                            if (objBlLIsta.VerificarColaboradorLista(strLista, Convert.ToInt32(dtt.Rows[0][0])))
                            {
                                RadAjaxPanel1.Alert("O Aprovador já existe em uma lista selecionada.");
                                blnRetorno = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }
                }
            }

            return blnRetorno;

        }

        #endregion

        #endregion

        #region Property

        /// <summary> 
        ///     Armazena os dados da solicitação e dos colaboradores adicionados na lista
        /// </summary> 
        /// <value>SolicitacaoColaborador</value> 
        /// <history> 
        ///     [mribeiro] 08/07/2009 Created 
        /// </history> 
        public List<SafWeb.Model.Solicitacao.SolicitacaoColaborador> lstGrid
        {
            get
            {
                if (ViewState["vsList"] == null)
                {
                    ViewState["vsList"] = new List<SafWeb.Model.Solicitacao.SolicitacaoColaborador>();
                }
                return (List<SafWeb.Model.Solicitacao.SolicitacaoColaborador>)ViewState["vsList"];
            }
            set
            {
                ViewState["vsList"] = value;
            }
        }

        /// <summary> 
        ///     Codigo da Solicitação
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 10/07/2009 Created 
        /// </history> 
        public int IdSolicitacao
        {
            get
            {
                if ((this.ViewState["vsSolicitacao"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsSolicitacao"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsSolicitacao", value);
            }
        }

        /// <summary> 
        ///     Codigo do motivo da visita
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 15/07/2009 Created 
        /// </history> 
        public int IdMotivoVisita
        {
            get
            {
                if ((this.ViewState["vsMotivoVisita"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsMotivoVisita"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsMotivoVisita", value);
            }
        }

        /// <summary> 
        ///     Codigo do Visitante
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 17/07/2009 Created 
        /// </history> 
        public int IdVisitante
        {
            get
            {
                if ((this.ViewState["vsIdVisitante"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdVisitante"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdVisitante", value);
            }
        }

        /// <summary> 
        ///     Codigo do Visitado
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 21/07/2009 Created 
        /// </history> 
        public int IdVisitado
        {
            get
            {
                if ((this.ViewState["vsIdVisitado"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdVisitado"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdVisitado", value);
            }
        }

        /// <summary> 
        ///     Codigo do Visitado
        /// </summary> 
        /// <value>int</value> 
        /// <history> 
        ///     [mribeiro] 21/07/2009 Created 
        /// </history> 
        public int IdListaAnterior
        {
            get
            {
                if ((this.ViewState["vsIdLista"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdLista"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdLista", value);
            }
        }

        #endregion

    }
}
