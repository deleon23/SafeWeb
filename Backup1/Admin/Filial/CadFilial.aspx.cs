using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Idioma;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Area;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Regional;
using SafWeb.Model.Area;
using SafWeb.Model.Filial;
using Telerik.WebControls;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class CadFilial : FWPage
    {
        private Filial gobjFilial;
        private Collection<SafWeb.Model.Area.Area> objcolArea;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.InicializaScripts();
                this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
            }
        }
                
        #region Bind

        #region BindCadastro
        /// <summary>
        /// Bind Cadastro
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo da Transação</param>
        /// <history>
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar] modify 26/04/2011 11:52
        ///     adicionar chkAreaTI
        /// </history>
        protected void BindCadastro(Enums.TipoTransacao penmTipoTransacao)
        {
            //atribuir as informações na tela para os objetos
            if (penmTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                this.gobjFilial = new Filial();

                this.gobjFilial.IdRegional = Convert.ToInt32(this.ddlRegionalCad.SelectedValue);
                this.gobjFilial.IdFilial = this.IdFilial;

                this.gobjFilial.DescricaoFilial = this.txtFilial.Text.Trim();
                this.gobjFilial.AliasFilial = this.txtSiglaFilial.Text.Trim();

                this.gobjFilial.FlgContrAcessoOnline = this.chkColetorOnline.Checked;
                this.gobjFilial.FlgPortValAcesso = this.chkPortariaValAcesso.Checked;

                this.gobjFilial.IdFusoHorario = Convert.ToInt32(this.ddlFusoHorario.SelectedValue);
                
                //BLFilial objBlFilial = new BLFilial();
                //this.gobjFilial.Vlr_FusoHorario = objBlFilial.ValorFusoHorario(this.gobjFilial.IdFusoHorario);

                this.gobjFilial.IdCidade = Convert.ToInt32(this.ddlCidade.SelectedValue);
                this.gobjFilial.CodFilial = Convert.ToInt32(this.txtCodigoFilialCorporate.Text.Trim());

                this.gobjFilial.QtdToleranciaAcesso = Convert.ToInt32(this.txtQtdToleranciaAcesso.Text.Trim());
            }
            //atribuir as informações dos objetos para tela
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.txtFilial.Text = this.gobjFilial.DescricaoFilial;
                this.txtSiglaFilial.Text = this.gobjFilial.AliasFilial;

                this.chkColetorOnline.Checked = this.gobjFilial.FlgContrAcessoOnline;
                this.chkPortariaValAcesso.Checked = this.gobjFilial.FlgPortValAcesso;

                this.txtCodigoFilialCorporate.Text = this.gobjFilial.CodFilial.ToString();
                this.txtQtdToleranciaAcesso.Text = this.gobjFilial.QtdToleranciaAcesso.ToString();

                BLUtilitarios.ConsultarValorCombo(ref this.ddlRegionalCad, this.gobjFilial.IdRegional.ToString());
                BLUtilitarios.ConsultarValorCombo(ref this.ddlFusoHorario, this.gobjFilial.IdFusoHorario.ToString());
                BLUtilitarios.ConsultarValorCombo(ref this.ddlEstado, this.gobjFilial.IdEstado.ToString());

                this.PopulaComboCidade();

                BLUtilitarios.ConsultarValorCombo(ref this.ddlCidade, this.gobjFilial.IdCidade.ToString());

                ColArea = null;
                radGridPerfilAcesso.DataSource = null;
                radGridPerfilAcesso.DataBind();

                this.BindArea(Enums.TipoBind.DataBind);
            }
            //inicializa as informações na tela
            else if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.IdFilial = 0;
                this.IdRegional = 0;
                this.txtFilial.Text = string.Empty;
                this.txtSiglaFilial.Text = string.Empty;

                this.txtCodigoFilialCorporate.Text = string.Empty;
                this.txtQtdToleranciaAcesso.Text = string.Empty;

                this.txtDes_Area.Text = string.Empty;

                chkAreaSeg.Checked = false;
                chkAreaTI.Checked = false;
                chkColetoresPonto.Checked = false;

                this.ddlGrupoColetores.SelectedIndex = 0;

                ColArea = null;

                this.InicializaScripts();
            }
        }
        #endregion

        #region BindArea
        /// <summary>
        /// Bind Area
        /// </summary>
        ///<param name="penmTipoBind">Tipo de Bind</param>
        /// <history>
        ///     [haguiar_4] create 26/01/2011
        /// </history>
        private void BindArea(Enums.TipoBind penmTipoBind)
        {
            if (ColArea != null)
            {
                if (this.IdFilial == 0 || ColArea.Count != 0)
                    return;
            }

            BLArea objBLArea = new BLArea();

            try
            {
                objcolArea = objBLArea.ListarArea(this.IdFilial);

                //guarda coleçao de areas
                ColArea = objcolArea;

                this.radGridPerfilAcesso.DataSource = objcolArea;

                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    radGridPerfilAcesso.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region BindFilial
        /// <summary>
        /// Bind Filial
        /// </summary>
        ///<param name="penmTipoBind">Tipo de Bind</param>
        /// <history>
        ///     [haguiar] modify 27/10/2010
        /// </history>
        private void BindFilial(Enums.TipoBind penmTipoBind)
        {
            BLFilial objBlFilial = new BLFilial();

            try
            {
                if (Convert.ToInt32(this.ddlRegionalList.SelectedValue) !=0)
                {
                    radGridFilial.DataSource = objBlFilial.Listar_DataTable(Convert.ToInt32(this.ddlRegionalList.SelectedValue), Convert.ToInt32(this.ddlFilialList.SelectedValue), txtDescricaoList.Text);
                }
                else
                {
                    radGridFilial.DataSource = objBlFilial.Listar_DataTable(0, 0, txtDescricaoList.Text);
                }

                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    radGridFilial.DataBind();
                }
                
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region BindListagem
        /// <summary>
        /// Bind Listagem
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo da Transação</param>        
        /// <history>
        ///     [cmarchi] created 29/12/2009
        ///     [cmarchi] modify 4/1/2009
        /// </history>
        protected void BindListagem(Enums.TipoTransacao penmTipoTransacao)                                    
        {            
            //atribuir as informações dos objetos para tela
            if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                //BLEscalaDepartamental objEscala
                this.txtDescricaoList.Text = string.Empty;
            }

            this.BindFilial(Enums.TipoBind.DataBind);
        }
        #endregion

        #region BindModel
        /// <summary>
        /// Bind Model 
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo de Transação</param>
        /// <param name="penmTipoPainel">Tipo do Painel</param>
        /// <history>
        ///     [cmarchi] created 17/12/2009
        ///     [cmarchi] modify 29/12/2009
        /// </history>
        protected void BindModel(Enums.TipoTransacao penmTipoTransacao,
                                 Enums.TipoPainel penmTipoPainel)
        {
            //verifica se  listagem ou cadastro na página
            if (penmTipoPainel == Enums.TipoPainel.Cadastro)
            {
                this.ControlaPaineis(Enums.TipoPainel.Cadastro);
                this.BindCadastro(penmTipoTransacao);
            }
            else
            {
                this.ControlaPaineis(Enums.TipoPainel.Listagem);
                this.BindListagem(penmTipoTransacao);                
            }
        }
        #endregion

        #endregion

        #region Botões

        #region Botão Adicionar Horários Selecionados
        /// <summary>
        ///     Botão Adicionar Horários Selecionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] 4/01/2010 created
        /// </history>
        protected void btnAddUmCad_Click(object sender, EventArgs e)
        {
            /*
            if (lstHorariosCad.SelectedIndex > -1)
            {
                int intCount = lstHorariosCad.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstHorariosCad.Items[i].Selected)
                    {                        
                        lstHorariosSelecionadosCad.Items.Add(new ListItem(lstHorariosCad.Items[i].Text,
                                                                          lstHorariosCad.Items[i].Value));
                        lstHorariosCad.Items.RemoveAt(i);
                    }
                }
            }
            */
        }

        #endregion
        
        #region Botão Buscar
        /// <summary>
        ///     Pesquisa de escalas Departamentais
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] 17/12/2009 created
        ///     [cmarchi] 4/1/2010 modify
        /// </history>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BindFilial(Enums.TipoBind.DataBind);
        }
        #endregion

        #region Botão Gravar
        /// <summary>
        ///     Grava os dados da tela de cadastro 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 30/12/2009 
        /// </history>
        protected void btnGravar_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                this.Gravar();
            }
        }
        #endregion
                
        #region Botão Incluir
        /// <summary>
        ///     Abre a tela de cadastro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 17/12/2009 
        ///     [cmarchi] modify 29/12/2009
        /// </history>
        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            this.BindModel(Enums.TipoTransacao.Novo, Enums.TipoPainel.Cadastro);
        }
        #endregion

        #region Botão Voltar
        /// <summary>
        ///     Volta para a parte de Listagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 19/11/2009 
        ///     [cmarchi] modify 17/12/2009 
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {            
            this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
        }
        #endregion

        #region Botão Remover Horários Selecionados
        /// <summary>
        ///     Botão Remover Horários Selecionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] 4/01/2010 created
        /// </history>
        protected void btnRemoverUmCad_Click(object sender, EventArgs e)
        {
            /*
            if (lstHorariosSelecionadosCad.SelectedIndex > -1)
            {
                int intCount = lstHorariosSelecionadosCad.Items.Count - 1;

                for (int i = intCount; i >= 0; i--)
                {
                    if (lstHorariosSelecionadosCad.Items[i].Selected)
                    {
                        lstHorariosCad.Items.Add(new ListItem(lstHorariosSelecionadosCad.Items[i].Text,
                                                             lstHorariosSelecionadosCad.Items[i].Value));
                        lstHorariosSelecionadosCad.Items.RemoveAt(i);
                    }
                }
            }
           */
        }
        #endregion

        #endregion

        #region Controla Painels
        /// <summary>
        /// Controla os Painels de Listagem e Cadastro de Escalas Departamentais.
        /// </summary>
        /// <param name="penmPainel">Painel a ser exibido</param>
        /// <history>
        ///     [cmarchi] created 17/12/2009
        /// </history>
        protected void ControlaPaineis(Enums.TipoPainel penmPainel)
        {
            if (penmPainel == Enums.TipoPainel.Listagem)
            {
                pnlCadastro.Visible = false;
                pnlListagem.Visible = true;
            }
            else
            {
                pnlCadastro.Visible = true;
                pnlListagem.Visible = false;
            }

        }
        #endregion

        #region Editar
        /// <summary>
        /// Editar uma Escala Departamental
        /// </summary>
        /// <param name="pintIdEscalaDepartamental">Id da Escala Departamental</param>
        /// <history>
        ///     [cmarchi] created 5/1/2010
        /// </history>
        private void Editar(int pintIdFilial)
        {
            BLFilial objBLFilial = new BLFilial();

            try
            {
                lblMensagem.Visible = false;
                lblMensagem.Text = string.Empty;

                this.BlnEditar = true;

                this.IdFilial = pintIdFilial;
                this.gobjFilial = objBLFilial.Obter(pintIdFilial);

                this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Cadastro);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Eventos Regionais

        #region Regional Cadastro
        /// <summary>
        ///     Seleciona uma filial conforme Regional selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [haguiar] modify 27/10/2010
        /// </history>
        protected void ddlRegionalCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.PopularFilial(ref this.ddlRegionalCad, ref ddlFilialCad);
        }
        #endregion

        #region Regional Listagem
        /// <summary>
        ///     Seleciona uma filial conforme Regional selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [haguiar] modify 27/10/2010 
        /// </history>
        protected void ddlRegionalList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularFilial(ref this.ddlRegionalList, ref ddlFilialList);
        }
        #endregion

        #endregion

        #region Gravar
        /// <summary>
        ///     Grava a Filial
        /// </summary>
        /// <history>
        ///     [haguiar_4] created 13/01/2011
        /// </history>
        private void Gravar()
        {
            BLFilial objBLFilial = new BLFilial();
            int intRetorno = -1;
            try
            {
                
                if (this.ValidarCampos())
                {
                    this.BindCadastro(Enums.TipoTransacao.CarregarDados);

                    gobjFilial.ColArea = ColArea;

                    if (!BlnEditar)
                    {
                        intRetorno = objBLFilial.Inserir(gobjFilial);
                    }
                    else if (this.IdFilial > 0)
                    {
                        gobjFilial.IdFilial = this.IdFilial;
                        intRetorno = objBLFilial.Alterar(gobjFilial);

                        BlnEditar = false;
                        this.IdFilial = 0;
                    }

                    lblMensagem.Visible = true;

                    if (intRetorno > 0)
                    {
                        this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
                        this.RadAjaxPanel1.Alert("Filial gravada com sucesso.");
                    }
                    else
                    {
                        lblMensagem.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR_ERRO));
                    }
                }
                
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Adicionar Área
        /// <summary>
        /// Adiciona áreas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar] created 19/02/2011
        ///     [haguiar] modify 26/04/2011 11:07
        ///     adicionar flg_areati
        /// </history>
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            Page.Validate("GrupoColetor");

            if (!Page.IsValid)
                return;
           
            if (this.ColArea == null)
            {
                objcolArea = new Collection<Area>();
            }
            else
            {
                objcolArea = ColArea;
            }

            bool blnValidar = true;


            //nao pode incluir uma descrição já existente
            foreach (Area gobjArea in objcolArea)
            {
                /*
                if (Convert.ToInt32(ddlGrupoColetores.SelectedValue) == gobjArea.IdGrupoColetores)
                {
                    this.RadAjaxPanel2.Alert("Grupo de coletores já existe em " + gobjArea.Descricao + ".");

                    blnValidar = false;

                    break;
                }
                */

                int intIdAreaEditada = 0;

                if (AreaEditada != null)
                {
                    intIdAreaEditada = Convert.ToInt32(AreaEditada.Codigo);
                }

                if (this.txtDes_Area.Text.ToString().Trim().ToUpper() == gobjArea.Descricao.ToString().Trim().ToUpper() && (Convert.ToInt32(gobjArea.Codigo) != intIdAreaEditada))
                {
                    RadAjaxPanel2.Alert("Descrição da Área já existe na lista!");

                    blnValidar = false;

                    break;
                }            
            }

            if (blnValidar)
            {
                Area objArea;

                if (AreaEditada == null)
                {
                    objArea = new Area();
                    objArea.Codigo = "0";
                    objArea.Flg_Situacao = true;
                }
                else
                {
                    objArea = AreaEditada;
                }

                objArea.IdFilial = this.IdFilial;

                if (this.chkAreaSeg.Checked)
                {
                    objArea.Flg_AreaSeg = "SIM";
                }
                else
                {
                    objArea.Flg_AreaSeg = "NÃO";
                }

                objArea.Flg_AreaTI = this.chkAreaTI.Checked;

                objArea.flg_ColetoresPonto = this.chkColetoresPonto.Checked;

                objArea.Descricao = this.txtDes_Area.Text;

                if (ddlGrupoColetores.SelectedIndex > 0)
                {
                    objArea.IdGrupoColetores = Convert.ToInt32(ddlGrupoColetores.SelectedValue);
                    objArea.Des_GrupoColetores = ddlGrupoColetores.SelectedItem.Text;
                }

                if (AreaEditada != null)
                {
                    //remove perfil selecionado
                    foreach (Area gobjArea in objcolArea)
                    {
                        if (Convert.ToInt32(gobjArea.Codigo) == Convert.ToInt32(objArea.Codigo))
                        {
                            objcolArea.Remove(gobjArea);
                            break;
                        }
                    }

                    AreaEditada = null;
                }

                objcolArea.Add(objArea);

                //atualiza vs
                ColArea = objcolArea;

                this.radGridPerfilAcesso.DataSource = objcolArea;
                this.radGridPerfilAcesso.DataBind();

                //limpa os campos
                this.txtDes_Area.Text = string.Empty;
                this.ddlGrupoColetores.SelectedIndex = 0;
                this.chkAreaSeg.Checked = false;
                this.chkAreaTI.Checked = false;
                this.chkColetoresPonto.Checked = false;
            }
        }
        #endregion

        #region Grid PerfilAcesso Listagem

            #region NeedDataSource
            protected void radGridPerfilAcesso_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
            {
                if (Page.IsPostBack)
                {
                    //this.BindArea(Enums.TipoBind.SemDataBind);

                    if (ColArea != null && ColArea.Count != 0)
                    {
                        radGridPerfilAcesso.DataSource = ColArea;
                    }
                }
            }
            #endregion        
        
            #region ItemCommand

            /// <summary>
            ///     radGridPerfilAcesso
            /// </summary>
            /// <history>
            ///     [haguiar] created 26/01/2011
            ///     [haguiar_4] Modify 02/02/2011
            ///     [haguiar] Modify 26/04/2011 11:24
            ///     inserir flg_areati
            /// </history>
            protected void radGridPerfilAcesso_ItemCommand(object source, GridCommandEventArgs e)
            {
                if (e.CommandName.Trim() == "Ativar")
                {
                    bool blnSituacao = true;

                    int intIdArea = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    try
                    {
                        //BLFilial objBLFilial = new BLFilial();

                        //altera o botão
                        if (btnAtivar.ToolTip == "Ativar")
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                            btnAtivar.ToolTip = "Inativar";
                            blnSituacao = true;
                        }
                        else if (btnAtivar.ToolTip == "Inativar")
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                            btnAtivar.ToolTip = "Ativar";
                            blnSituacao = false;
                        }

                        //remove perfil de acesso
                        if (this.ColArea != null)
                        {

                            foreach (Area gobjArea in ColArea)
                            {
                                if (Convert.ToInt32(gobjArea.Codigo) == Convert.ToInt32(e.CommandArgument.ToString()))
                                {
                                    gobjArea.Flg_Situacao = blnSituacao;

                                    break;
                                }
                            }

                        }

                        //objBLFilial.AlterarSituacao(intIdFilial, blnSituacao);

                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                    }
                }


                if (e.CommandName.Trim() == "Editar")
                {
                    if (e.CommandArgument.ToString().Trim() != string.Empty)
                    {
                        if (Permissoes.Alteração())
                        {
                            //edita perfil de acesso
                            if (this.ColArea != null)
                            {
                                objcolArea = ColArea;

                                //edita perfil selecionado
                                foreach (Area gobjArea in objcolArea)
                                {
                                    if (Convert.ToInt32(gobjArea.Codigo) == Convert.ToInt32(e.CommandArgument.ToString()))
                                    {
                                        txtDes_Area.Text = gobjArea.Descricao;

                                        if (gobjArea.Flg_AreaSeg.Equals("SIM"))
                                        {
                                            chkAreaSeg.Checked = true;
                                        }
                                        else
                                        {
                                            chkAreaSeg.Checked = false;
                                        }

                                        chkAreaTI.Checked = gobjArea.Flg_AreaTI;

                                        chkColetoresPonto.Checked = gobjArea.flg_ColetoresPonto;

                                        BLUtilitarios.ConsultarValorCombo(ref this.ddlGrupoColetores, gobjArea.IdGrupoColetores.ToString());

                                        this.AreaEditada = gobjArea;

                                        break;
                                    }
                                }

                            }
                        }
                    }
                }



                if (e.CommandName.Trim() == "Remover")
                {
                    if (e.CommandArgument.ToString().Trim() != string.Empty)
                    {
                        if (Permissoes.Alteração())
                        {
                            //remove perfil de acesso
                            if (this.ColArea != null)
                            {
                                objcolArea = ColArea;

                                //remove perfil selecionado
                                foreach (Area gobjArea in objcolArea)
                                {
                                    if (Convert.ToInt32(gobjArea.Codigo) == Convert.ToInt32(e.CommandArgument.ToString()))
                                    {
                                        objcolArea.Remove(gobjArea);
                                        break;
                                    }
                                }

                                ColArea = objcolArea;
                            }

                            radGridPerfilAcesso.DataSource = objcolArea;
                            radGridPerfilAcesso.DataBind();
                        }
                    }
                }

                if (e.CommandName.Trim() == "IrPagina")
                {
                    string strPageIndexString = ((TextBox)e.Item.FindControl("txtPagina_Perfil")).Text;
                    int intPageIndex = e.Item.OwnerTableView.CurrentPageIndex;
                    if ((!object.ReferenceEquals(strPageIndexString.Trim(), string.Empty)))
                    {
                        try
                        {
                            if (e.Item.OwnerTableView.PageCount < Convert.ToInt32(strPageIndexString))
                            {
                                strPageIndexString = (e.Item.OwnerTableView.PageCount).ToString();
                                ((TextBox)e.Item.FindControl("txtPagina_Perfil")).Text = strPageIndexString;
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

                /*
                //[haguiar]
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
                */
            }

            #endregion

            #region ItemDataBound
            /// <summary>
            ///     radGridPerfilAcesso_ItemDataBound
            /// </summary>
            /// <history>
            ///     [haguiar_4] Modify 02/02/2011     
            /// </history> 
            protected void radGridPerfilAcesso_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
                {
                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    ImageButton btnRemover = (ImageButton)dataItem["Remover"].Controls[0];
                    ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    ImageButton btnEditar;
                    btnEditar = (ImageButton)e.Item.FindControl("btnEditar");
                    if (Permissoes.Alteração() && btnEditar != null)
                    {
                        btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Codigo").ToString();
                        //btnEditar.Visible = true;
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);

                        btnEditar.Visible = false;
                        e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                        e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                        e.Item.Style["cursor"] = "hand";

                        // Cursor 
                        int intCell = e.Item.Cells.Count;
                        for (int @int = 0; @int <= intCell - 2; @int++)
                        {
                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                        }

                    }

                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_AreaTI")) == false)
                    {
                        e.Item.Cells[5].Text = "NÃO";

                    }
                    else
                    {
                        e.Item.Cells[5].Text = "SIM";
                    }

                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "flg_ColetoresPonto")) == false)
                    {
                        e.Item.Cells[6].Text = "NÃO";

                    }
                    else
                    {
                        e.Item.Cells[6].Text = "SIM";
                    }

                    if (Permissoes.Alteração() && btnRemover != null)
                    {
                        btnRemover.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Codigo").ToString();

                        if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Codigo").ToString()) != 0)
                            btnRemover.Visible = false;
                    }
                    else
                    {
                        btnRemover.Visible = false;
                    }

                    if (DataBinder.Eval(e.Item.DataItem, "Flg_Situacao") == DBNull.Value)
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Codigo").ToString();
                    }
                    else
                    {

                        if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Situacao")))
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                            btnAtivar.ToolTip = "Inativar";
                            btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Codigo").ToString();
                        }
                        else
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                            btnAtivar.ToolTip = "Ativar";
                            btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Codigo").ToString();
                        }
                    }

                }

                e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                e.Item.Style["cursor"] = "hand";
            }
            #endregion
        #endregion

        #region Grid Filial Listagem

        #region NeedDataSource
        protected void radGridFilial_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindFilial(Enums.TipoBind.SemDataBind);
            }
        }        
        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridFilial
        /// </summary>
        /// <history>
        ///     [no history]
        ///     [haguiar] modified 05/11/2010
        /// </history>        
        protected void radGridFilial_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Ativar")
            {
                bool blnSituacao = true;

                int intIdFilial = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                try
                {
                    BLFilial objBLFilial = new BLFilial();

                    //altera o botão
                    if (btnAtivar.ToolTip == "Ativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        blnSituacao = true;
                    }
                    else if (btnAtivar.ToolTip == "Inativar")
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        blnSituacao = false;
                    }

                    objBLFilial.AlterarSituacao(intIdFilial, blnSituacao);
                    
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }

            if (e.CommandName.Trim() == "Editar")
            {
                if (e.CommandArgument.ToString().Trim() != string.Empty)
                {
                    if (Permissoes.Alteração())
                    {
                        this.Editar(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                    }
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

            /*
            //[haguiar]
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
            */
        }

        #endregion

        #region ItemDataBound
        protected void radGridFilial_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];
                                
                if (Permissoes.Alteração() && btnEditar != null)
                {
                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Filial").ToString();
                    //btnEditar.Visible = true;
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                }

                if (DataBinder.Eval(e.Item.DataItem, "Flg_Situacao") == DBNull.Value)
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                    btnAtivar.ToolTip = "Inativar";
                    btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Filial").ToString();
                }
                else
                {

                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Situacao")))
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Filial").ToString();
                    }
                    else
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Filial").ToString();
                    }
                }

                btnEditar.Visible = false;
                e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                e.Item.Style["cursor"] = "hand";

                // Cursor 
                int intCell = e.Item.Cells.Count;
                for (int @int = 0; @int <= intCell - 2; @int++)
                {
                    e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                }
            }
        }
        #endregion

        #endregion

        /*
        #region Inicializa
        /// <summary>
        /// Inicializa alguns campos da pagina
        /// </summary>
        /// <history>
        ///     [cmarchi] created 9/2/2010
        /// </history>
        private void Inicializa()
        {
            lblMensagem.Visible = false;
            lblMensagem.Text = string.Empty;

            //seleciona a regional e a filial do usuário logado.
            //BLColaborador objBlColaborador = new BLColaborador();
            //DataTable dtt = new DataTable();

            try
            {
                //dtt = objBlColaborador.ObterRegFilUsuario(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                //this.IdRegional = Convert.ToInt32(dtt.Rows[0][0].ToString());
                //this.IdFilial = Convert.ToInt32(dtt.Rows[0][1].ToString());

                //painel de listagem
                //this.PopularRegional();
                //this.PopularFilial();

                //painel de cadastro
                //this.PopularRegional();
                //this.PopularFilial(ref this.ddlFilialCad);

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        */

        #region Inicializa Scripts
        /// <summary>
        /// Inicializa os scripts 
        /// </summary>        
        /// <history>
        ///     [haguiar_4] created 12/01/2011
        ///     [haguiar_4] modify 14/01/2011
        /// </history>
        protected void InicializaScripts()
        {
            BlnEditar = false;

            this.txtCodigoFilialCorporate.MaxLength = 4;
            this.txtCodigoFilialCorporate.Attributes.Add("OnKeyPress", "return FormataCodigo(event,this);");

            this.txtQtdToleranciaAcesso.MaxLength = 4;
            this.txtQtdToleranciaAcesso.Attributes.Add("OnKeyPress", "return FormataCodigo(event,this);");

            lblMensagem.Visible = false;
            lblMensagem.Text = string.Empty;

            this.PopularRegional();
            this.PopularFusoHorario();

            this.PopulaComboEstado();
            
            ddlCidade.Items.Clear();
            ddlCidade.Enabled = false;

            this.PopulaGrupoColetores();

            radGridPerfilAcesso.DataSource = ColArea;
            radGridPerfilAcesso.DataBind();

            BLUtilitarios.InseriMensagemDropDownList(ref ddlFusoHorario, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            BLUtilitarios.InseriMensagemDropDownList(ref ddlEstado, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlCidade, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            BLUtilitarios.InseriMensagemDropDownList(ref ddlGrupoColetores, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
        }
        
        #endregion

        #region Popular Combos

        #region Filial
        /// <summary>
        ///     Popula os combos com a filial do logado
        /// </summary>
        /// <param name="ddlRegional">Regional</param>
        /// <param name="ddlFilial">Filial</param>
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [haguiar] modify 27/10/2010
        ///     [haguiar] modify 03/02/2011
        ///     popular todas as filiais
        /// </history>
        /// 
        
        protected void PopularFilial(ref DropDownList ddlRegional, ref DropDownList ddlFilial)
        {
            BLFilial objBlFilial = new BLFilial();
            //Collection<SafWeb.Model.Filial.Filial> colFilial;
            
            DataTable dttFilial = new DataTable();

            try
            {
                if (ddlRegional.SelectedIndex != 0)
                {
                    dttFilial = objBlFilial.Listar_DataTable(Convert.ToInt32(ddlRegional.SelectedItem.Value),0,string.Empty);

                    ddlFilial.DataSource = dttFilial;
                    ddlFilial.DataTextField = "Alias_Filial";
                    ddlFilial.DataValueField = "Id_Filial";
                    ddlFilial.DataBind();

                    ddlFilial.Enabled = true;
                }
                else
                {
                    ddlFilial.Enabled = false;
                    ddlFilial.SelectedIndex = 0;                    
                }

                BLUtilitarios.InseriMensagemDropDownList(ref ddlFilial, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Horários
        /// <summary>
        /// Popula uma Lista com os horários 
        /// </summary>        
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [cmarchi] modify 5/1/2010
        /// </history>
        private void PopularHorarios()
        {
            //BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            //Collection<HorarioEscala> colHorarios = objBLEscalaDepartamental.GerarHorarios();
            /*
            this.lstHorariosCad.DataTextField  = "IdHorario";
            this.lstHorariosCad.DataValueField = "IdHorario";

            this.lstHorariosCad.DataSource = colHorarios;
            this.lstHorariosCad.DataBind();
            */
        }
        #endregion

        #region Horários Alteração
        /// <summary>
        /// Popula uma Lista com os horários para alteração
        /// </summary>        
        /// <history>
        ///     [cmarchi] created 5/1/2010
        ///     [haguiar] modify 21/10/2010
        /// </history>
        private void PopularHorariosAlteracao()
        {
            int intHorarioPosicao = -1;
            int intTamanho = 0;

            //BLEscalaDepartamental objBLEscalaDepartamental = new BLEscalaDepartamental();
            //Collection<HorarioEscala> colHorarios = objBLEscalaDepartamental.GerarHorarios();
           
            /*
            this.lstHorariosCad.DataTextField = "IdHorario";
            this.lstHorariosCad.DataValueField = "IdHorario";

            this.lstHorariosSelecionadosCad.DataTextField  = "IdHorario";
            this.lstHorariosSelecionadosCad.DataValueField = "IdHorario";
            */
            //intTamanho = colHorarios.Count;
            /*
            //exclui os horários da lstHorariosCad que estejam na lista de horários da Escala
            foreach (HorarioEscala h in this.gobjEscalaDepartamental.HorariosEscala)
            {
                for (int i = 0; i < intTamanho; i++)
			    {

                    if (h.IdHorario == colHorarios[i].IdHorario)
                    {                        
                        colHorarios.RemoveAt(i);
                            
                        //reinicia contador
                        intTamanho = colHorarios.Count;
                        break;
                    }  
                    
			    }              
            }
            */
            //this.lstHorariosCad.DataSource = colHorarios;
            //this.lstHorariosCad.DataBind();
            
            //this.lstHorariosSelecionadosCad.DataSource = this.gobjEscalaDepartamental.HorariosEscala;
            //this.lstHorariosSelecionadosCad.DataBind();            
        }
        #endregion

        #region Periodicidade
        /// <summary>
        ///     Popula o combo com a periodicidade
        /// </summary>
        /// <history>
        ///     [cmarchi] created 30/12/2009 
        ///</history>
        protected void PopularPeriodicidade()
        {
            
           // BLPeriodicidade objBlPeriodicidade = new BLPeriodicidade();
            //Collection<Periodicidade> colPeriodicidade;

            try
            {
                /*
                colPeriodicidade = objBlPeriodicidade.Listar();

                //preenche periodicidade da parte de listagem
                ddlPeriodicidadeList.DataSource = colPeriodicidade;
                ddlPeriodicidadeList.DataTextField = "DescricaoPeriodicidade";
                ddlPeriodicidadeList.DataValueField = "IdPeriodicidade";
                ddlPeriodicidadeList.DataBind();

                //preenche periodicidade da parte de cadastro
                //ddlPeriodicidadeCad.DataSource = colPeriodicidade;
                //ddlPeriodicidadeCad.DataTextField = "DescricaoPeriodicidade";
                //ddlPeriodicidadeCad.DataValueField = "IdPeriodicidade";
                //ddlPeriodicidadeCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlPeriodicidadeList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                //BLUtilitarios.InseriMensagemDropDownList(ref ddlPeriodicidadeCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            */
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Regional
        /// <summary>
        ///     Popula o combo com a regional do logado
        /// </summary>
        /// <history>
        ///     [cmarchi] created 29/12/2009 
        ///     [haguiar] 27/10/2010
        ///</history>

        protected void PopularRegional()
        {
            BLRegional objBlRegional = new BLRegional();
            Collection<SafWeb.Model.Regional.Regional> colRegional;

            try
            {
                colRegional = objBlRegional.Listar();

                //preenche regionais de parte de listagem
                ddlRegionalList.DataSource = colRegional;
                ddlRegionalList.DataTextField = "DescricaoRegional";
                ddlRegionalList.DataValueField = "IdRegional";
                ddlRegionalList.DataBind();

                //preenche regionais de parte de cadastro
                ddlRegionalCad.DataSource = colRegional;
                ddlRegionalCad.DataTextField = "DescricaoRegional";
                ddlRegionalCad.DataValueField = "IdRegional";
                ddlRegionalCad.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegionalList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegionalCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
 
        #endregion

        #region Estado
        /// <summary>
        ///     Preenche o combo cidades, de acordo com o estado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 30/12/2009
        ///     [haguiar] modify 27/10/2010
        /// </history>
        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopulaComboCidade();
        }
        private void PopulaComboEstado()
        {
            BLEstado objBLEstado = null;
            Collection<SafWeb.Model.Filial.Estado> colEstado = null;

            try
            {
                objBLEstado = new BLEstado();

                colEstado = objBLEstado.ListarEstado();

                this.ddlEstado.DataSource = colEstado;
                this.ddlEstado.DataTextField = "DescricaoEstado";
                this.ddlEstado.DataValueField = "Codigo";
                this.ddlEstado.DataBind();

                //this.ddlEstado.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "-1"));
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion        

        #region Fuso Horário
        /// <summary>
        ///     Popula o combo com fuso horário
        /// </summary>
        /// <history>
        ///     [haguiar] created 13/11/2011
        ///</history>

        protected void PopularFusoHorario()
        {
            BLFusoHorario objBLFusoHorario = new BLFusoHorario();

            ddlFusoHorario.DataSource = objBLFusoHorario.ListarFusoHorario();
            ddlFusoHorario.DataTextField = "DescricaoFusoHorario";
            ddlFusoHorario.DataValueField = "IdFusoHorario";
            ddlFusoHorario.DataBind();            
        }
        #endregion

        #region Cidade
        private void PopulaComboCidade()
        {
            BLCidade objBLCidade = null;
            Collection<SafWeb.Model.Filial.Cidade> colCidade = null;

            try
            {
                if (ddlEstado.SelectedIndex != 0)
                {
                    objBLCidade = new BLCidade();

                    colCidade = objBLCidade.ListarCidade(Convert.ToInt32(ddlEstado.SelectedValue));

                    this.ddlCidade.DataSource = colCidade;
                    this.ddlCidade.DataTextField = "DescricaoCidade";
                    this.ddlCidade.DataValueField = "Id_Cidade";
                    this.ddlCidade.DataBind();

                    if (ddlCidade.Items.Count > 0)
                    {
                        ddlCidade.Enabled = true;
                    }
                    else
                    {
                        ddlCidade.Enabled = false;
                    }

                    BLUtilitarios.InseriMensagemDropDownList(ref ddlCidade, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                }
                else
                {
                    ddlCidade.Enabled = false;
                    BLUtilitarios.InseriMensagemDropDownList(ref ddlCidade, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region GrupoColetores
        private void PopulaGrupoColetores()
        {
            BLArea objBLArea = new BLArea();
            Collection<SafWeb.Model.Area.GrupoColetores> colGrupoColetores = null;

            try
            {
                colGrupoColetores = objBLArea.ListarGrupoColetores();

                this.ddlGrupoColetores.DataSource = colGrupoColetores;
                this.ddlGrupoColetores.DataTextField = "Des_GrupoColetores";
                this.ddlGrupoColetores.DataValueField = "Id_GrupoColetores";
                this.ddlGrupoColetores.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlCidade, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
               
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion
        #endregion
      

        #region Propriedades

        #region AreaEditada
        /// <summary>
        ///     Propriedade AreaEditada que armazena área editada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar_4] created 03/02/2011
        /// </history>
        private Area AreaEditada
        {
            get
            {
                /*
                if (this.ViewState["vsAreaEditada"] == null)
                {
                    this.ViewState.Add("vsAreaEditada", new Area());
                }
                */

                return (Area)this.ViewState["vsAreaEditada"];
            }

            set
            {
                this.ViewState.Add("vsAreaEditada", value);
            }
        }
        #endregion

        #region Editar
        /// <summary>
        ///     Propriedade Editar que verifica se está ou não editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 5/1/2009 
        /// </history>
        private bool BlnEditar
        {
            get
            {
                if (this.ViewState["vsEditar"] == null)
                {
                    this.ViewState.Add("vsEditar", false);
                }

                return Convert.ToBoolean(this.ViewState["vsEditar"]);
            }

            set 
            {
                this.ViewState.Add("vsEditar", value);
            }
        }
        #endregion

        #region IdFilial
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdFilial
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
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

        #region IdRegional
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdRegional
        /// </summary> 
        /// <history> 
        ///     [haguiar] 26/10/2010 Created
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

        #region ColArea
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade ColArea
        /// </summary> 
        /// <history> 
        ///     [haguiar_4] 26/01/2011 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Area.Area> ColArea
        {
            get
            {
                if (this.ViewState["vsColArea"] == null)
                {
                    this.ViewState.Add("vsColArea", new Collection<Area>());
                }

                return (Collection<Area>)this.ViewState["vsColArea"];
                
            }
            set
            {
                this.ViewState.Add("vsColArea", value);
            }
        }
        #endregion
        #endregion

        #region Validações
        /*
        #region cvrHorariosSelecionadosCad_ServerValidate
        /// <summary>
        /// Valida o a lista de horários selecionados na parte de cadastro.
        /// </summary>
        /// <history>
        ///     [cmarchi] Created 5/01/2010
        /// </history>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cvrHorariosSelecionadosCad_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (this.lstHorariosSelecionadosCad.Items.Count > 0)
                e.IsValid = true;
            else
                e.IsValid = false;
        }
        #endregion
        */


        #region Validar Campos

        /// <summary>
        ///     Verifica se os campos foram preenchidos corretamente
        /// </summary>
        /// <returns>True - Válidos, False - Erros</returns>
        /// <history>
        ///     [cmarchi] created 7/01/2009
        ///     [haguiar] modify 27/10/2010
        /// </history>
        protected bool ValidarCampos()
        {
            bool blnRetorno = true;

            /*
            if (this.ddlRegionalCad.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Regional.");
                blnRetorno = false;
            }

            if (this.ddlFilialCad.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Filial.");
                blnRetorno = false;
            }
            */

            if (this.ddlRegionalCad.SelectedItem == null)
            {
                RadAjaxPanel1.Alert("Selecione uma Regional.");
                blnRetorno = false;
            }

            /*
            if (this.ddlFilialCad.SelectedItem == null)
            {
                RadAjaxPanel1.Alert("Selecione uma Filial.");
                blnRetorno = false;
            }
            */

            /*
            if (this.ddlPeriodicidadeCad.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Periodicidade.");
                blnRetorno = false;
            }
            */

            if (!string.IsNullOrEmpty(this.txtFilial.Text))
            {
                if (this.txtFilial.Text.Trim().Length == 0)
                {
                    RadAjaxPanel1.Alert("Campo Obrigatório: Filial.");
                    blnRetorno = false;
                }
            }
            else
            {
                RadAjaxPanel1.Alert("Campo Obrigatório: Filial.");
                blnRetorno = false;
            }

            if (!string.IsNullOrEmpty(this.txtSiglaFilial.Text))
            {
                if (this.txtSiglaFilial.Text.Trim().Length == 0)
                {
                    RadAjaxPanel1.Alert("Campo Obrigatório: Sigla Filial.");
                    blnRetorno = false;
                }
            }
            else
            {
                RadAjaxPanel1.Alert("Campo Obrigatório: Sigla Filial.");
                blnRetorno = false;
            }

            if (!string.IsNullOrEmpty(this.txtCodigoFilialCorporate.Text))
            {
                if (this.txtCodigoFilialCorporate.Text.Trim().Length == 0)
                {
                    RadAjaxPanel1.Alert("Campo Obrigatório: Código Filial Corporate.");
                    blnRetorno = false;
                }
            }
            else
            {
                RadAjaxPanel1.Alert("Campo Obrigatório: Código Filial Corporate.");
                blnRetorno = false;
            }



            return blnRetorno;
        }

        #endregion

        #endregion
    }
}