using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.WebControls;

using FrameWork.Model.Utilitarios;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using FrameWork.BusinessLayer.Usuarios;

using SafWeb.BusinessLayer.Regional;
using System.Collections.ObjectModel;

using System.Web.UI.WebControls;
using SafWeb.BusinessLayer.Filial;
using System.Collections.Generic;

using SafWeb.Model.Filial;
using SafWeb.Model.Regional;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.Model.Colaborador;

using SafWeb.Model.Area;
using SafWeb.BusinessLayer.Area;
using SafWeb.BusinessLayer.Solicitacao;

namespace SafWeb.UI.Admin
{
    public partial class CadAtributosAprovador : FWPage
    {
        private AprovadorFilial gobjBLAprovadorFilial;
        private Collection<SafWeb.Model.Filial.AprovadorFilial> objcolAprovadorFilial;

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
        ///     [haguiar] modify 26/04/2011 14:26
        ///     limpar chkAprovaAreaTI
        /// </history>
        protected void BindCadastro(Enums.TipoTransacao penmTipoTransacao)
        {
            //atribuir as informações na tela para os objetos
            if (penmTipoTransacao == Enums.TipoTransacao.CarregarDados)
            {
                //this.gobjBLAprovadorFilial = new Filial();

                //this.gobjBLAprovadorFilial.IdRegional = Convert.ToInt32(this.ddlRegionalCad.SelectedValue);
                //this.gobjBLAprovadorFilial.IdFilial = this.IdFilial;

                //this.gobjFilial.DescricaoFilial = this.txtFilial.Text.Trim();
                //this.gobjFilial.AliasFilial = this.txtSiglaFilial.Text.Trim();

                //this.gobjFilial.FlgContrAcessoOnline = this.chkColetorOnline.Checked;
                //this.gobjFilial.FlgPortValAcesso = this.chkPortariaValAcesso.Checked;

                //this.gobjFilial.IdFusoHorario = Convert.ToInt32(this.ddlFusoHorario.SelectedValue);
                
                //BLFilial objBlFilial = new BLFilial();
                //this.gobjFilial.Vlr_FusoHorario = objBlFilial.ValorFusoHorario(this.gobjFilial.IdFusoHorario);

                //this.gobjFilial.IdCidade = Convert.ToInt32(this.ddlCidade.SelectedValue);
                //this.gobjFilial.CodFilial = Convert.ToInt32(this.txtCodigoFilialCorporate.Text.Trim());

                //this.gobjFilial.QtdToleranciaAcesso = Convert.ToInt32(this.txtQtdToleranciaAcesso.Text.Trim());
            }
            //atribuir as informações dos objetos para tela
            else if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.chkAprovaAreaSeg.Checked = false;
                this.chkAprovaCont.Checked = false;
                this.chkAprovaAreaTI.Checked = false;
                this.chkAprovaCracha.Checked = false;

                this.txtJustificativa.Text = string.Empty;

                this.txtNSolicitacao.Text = string.Empty;

                this.txtInicio.Text = string.Empty;
                this.txtFim.Text = string.Empty;

                rdbVigencia_Definitiva.Checked = true;

                this.ddlRegionalCad.SelectedIndex = 0;
                this.ddlFilialCad.Items.Clear();
                this.ddlFilialCad.Enabled = false;

                BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                this.ddlNivelAprovacao.SelectedIndex = 0;
                this.ddlSuperHierarquico.SelectedIndex = 0;
                this.ddlOrigemSol.SelectedIndex = 0;

                ColAtributosAprovador = null;
                radGridAtributosAprovador.DataSource = null;
                radGridAtributosAprovador.DataBind();

                this.BindAtributosAprovador(Enums.TipoBind.DataBind);
            }
            //inicializa as informações na tela
            else if (penmTipoTransacao == Enums.TipoTransacao.Novo)
            {
                this.IdFilial = 0;
                this.IdRegional = 0;
                ColAtributosAprovador = null;

                this.InicializaScripts();
            }
        }
        #endregion

        #region Bind Atributos Aprovador
        /// <summary>
        /// Bind Atributos Aprovador
        /// </summary>
        ///<param name="penmTipoBind">Tipo de Bind</param>
        /// <history>
        ///     [haguiar_5] created 17/02/2011
        /// </history>
        private void BindAtributosAprovador(Enums.TipoBind penmTipoBind)
        {
            if (this.ColAtributosAprovador != null)
            {
                if (this.IdUsuario == 0 || ColAtributosAprovador.Count != 0)
                    return;
            }

            BLAprovadorFilial objBLAprovadorFilial = new BLAprovadorFilial();

            try
            {
                this.objcolAprovadorFilial = objBLAprovadorFilial.ListarAtributos(this.IdUsuario);

                //guarda coleçao de atributos
                ColAtributosAprovador = objcolAprovadorFilial;

                this.radGridAtributosAprovador.DataSource = objcolAprovadorFilial;

                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    radGridAtributosAprovador.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region BindUsuario
        /// <summary>
        /// Bind Filial
        /// </summary>
        ///<param name="penmTipoBind">Tipo de Bind</param>
        /// <history>
        ///     [haguiar_5] create 16/02/2011
        /// </history>
        private void BindUsuario(Enums.TipoBind penmTipoBind)
        {
            BLAprovadorFilial objBLAprovadorFilial = new BLAprovadorFilial();

            try
            {
                if (Convert.ToInt32(this.ddlRegionalList.SelectedValue) !=0)
                {
                    radGridUsuario.DataSource = objBLAprovadorFilial.Listar(Convert.ToInt32(this.ddlRegionalList.SelectedValue), Convert.ToInt32(this.ddlFilialList.SelectedValue), Convert.ToInt32(this.ddlSuperiorHierarquicoList.SelectedValue), this.txtNomeUsuarioList.Text);
                }
                else
                {
                    radGridUsuario.DataSource = objBLAprovadorFilial.Listar(0, 0, Convert.ToInt32(this.ddlSuperiorHierarquicoList.SelectedValue), this.txtNomeUsuarioList.Text);
                }

                if (penmTipoBind == Enums.TipoBind.DataBind)
                {
                    radGridUsuario.DataBind();
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
        ///     [haguiar_5] created 16/02/2011
        /// </history>
        protected void BindListagem(Enums.TipoTransacao penmTipoTransacao)                                    
        {            
            //atribuir as informações dos objetos para tela
            if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.txtNomeUsuarioList.Text = string.Empty;
            }

            this.BindUsuario(Enums.TipoBind.DataBind);
        }
        #endregion

        #region BindModel
        /// <summary>
        /// Bind Model 
        /// </summary>
        /// <param name="pintTipoTransacao">Tipo de Transação</param>
        /// <param name="penmTipoPainel">Tipo do Painel</param>
        /// <history>
        ///     [haguiar_5] modify 17/02/2011
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
            this.BindUsuario(Enums.TipoBind.DataBind);
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
        /// Editar atributos de um aprovador
        /// </summary>
        /// <param name="pintId_Usuario">Id do usuário</param>
        /// <history>
        ///     [haguiar_5] created 17/02/2011
        /// </history>
        private void Editar(int pintId_Usuario)
        {
            BLAprovadorFilial objBLAprovadorFilial = new BLAprovadorFilial();
            UsuarioAprovadorFilial gobjUsuarioAprovadorFilial;
            
            try
            {
                lblMensagem.Visible = false;
                lblMensagem.Text = string.Empty;
                
                this.AtributoAprovadorEditado = null;
                this.ColAtributosAprovador = null;
                this.objcolAprovadorFilial = null;

                this.BlnEditar = true;

                this.IdUsuario = pintId_Usuario;

                //obtem dados do usuario
                gobjUsuarioAprovadorFilial = objBLAprovadorFilial.ObterUsuario(pintId_Usuario);
                this.lblNomeUsuario.Text = gobjUsuarioAprovadorFilial.USU_C_NOME;

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
        ///     [haguiar_5] create 17/02/2011
        /// </history>
        protected void ddlRegionalCad_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PopularFilial(ref this.ddlRegionalCad, ref ddlFilialCad);
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
            BLAprovadorFilial objBLAprovadorFilial = new BLAprovadorFilial();
            int intRetorno = -1;
            try
            {
                
                if (this.ValidarCampos())
                {
                    this.BindCadastro(Enums.TipoTransacao.CarregarDados);

                    //if (!BlnEditar)
                    //{
                        intRetorno = objBLAprovadorFilial.InserirAlterarAtributosAprovador(ColAtributosAprovador);
                    //}

                    lblMensagem.Visible = true;

                    if (intRetorno != -1)
                    {
                        this.BindModel(Enums.TipoTransacao.DescarregarDados, Enums.TipoPainel.Listagem);
                        this.RadAjaxPanel1.Alert("Atributos de aprovador gravados com sucesso.");
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

        #region Adicionar Perfil Acesso
        /// <summary>
        /// Chama a tela de cadastro de escala.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar_5] created 18/02/2011
        ///     [haguiar] modify 26/04/2011 14:31
        ///     adicionar Flg_AprovaAreaTI
        /// </history>
        protected void btnAdicionar_Click(object sender, ImageClickEventArgs e)
        {
            
            if (!rdbVigencia_Definitiva.Checked)
            {
                Page.Validate("ValidarData");

                if (!Page.IsValid)
                    return;
            }
            
            Page.Validate("Todos");
            if (!Page.IsValid)
                return;  
         
            if (this.ColAtributosAprovador == null)
            {
                objcolAprovadorFilial = new Collection<AprovadorFilial>();
            }
            else
            {
                objcolAprovadorFilial = ColAtributosAprovador;
            }

            bool blnValidar = true;

            int intIdAprovadorFilial = 0;

            if (AtributoAprovadorEditado != null)
            {
                intIdAprovadorFilial = Convert.ToInt32(AtributoAprovadorEditado.Id_AprovadorFilial);
            }

            //nao pode incluir uma descrição já existente
            foreach (AprovadorFilial gobjAprovadorFilial in objcolAprovadorFilial)
            {
                if (
                    (Convert.ToInt32(this.ddlRegionalCad.SelectedValue) == gobjAprovadorFilial.Id_Regional) && 
                    (Convert.ToInt32(this.ddlFilialCad.SelectedValue) == gobjAprovadorFilial.Id_Filial) &&
                    (gobjAprovadorFilial.Flg_Situacao == true)
                   )
                {
                    RadAjaxPanel2.Alert("Regional e Filial já existem para este usuário.");

                    blnValidar = false;

                    break;
                }
            }

            if (blnValidar)
            {
                AprovadorFilial objAprovadorFilial;

                if (AtributoAprovadorEditado == null)
                {
                    objAprovadorFilial = new AprovadorFilial();
                    objAprovadorFilial.Id_AprovadorFilial = 0;
                    objAprovadorFilial.Flg_Situacao = true;
                }
                else
                {
                    objAprovadorFilial = AtributoAprovadorEditado;

                    //copia o registro caso esteja inativo e novo cadastro
                    if (!objAprovadorFilial.Flg_Situacao && objAprovadorFilial.Id_AprovadorFilial != 0)
                    {
                        objAprovadorFilial.Id_AprovadorFilial = 0;
                        objAprovadorFilial.Flg_Situacao = true;
                    }
                }

                //objAprovadorFilial.Id_AprovadorFilial = this.Id_AprovadorFilial;

                objAprovadorFilial.Id_Usuario = this.IdUsuario;

                objAprovadorFilial.Flg_AprovaAreaSeg = this.chkAprovaAreaSeg.Checked;
                objAprovadorFilial.Flg_AprovaContingencia = this.chkAprovaCont.Checked;
                objAprovadorFilial.Flg_AprovaAreaTI = this.chkAprovaAreaTI.Checked;
                objAprovadorFilial.Flg_AprovaCracha = this.chkAprovaCracha.Checked;

                objAprovadorFilial.Des_Justificativa = this.txtJustificativa.Text;

                objAprovadorFilial.Id_Filial = Convert.ToInt32(this.ddlFilialCad.SelectedValue.ToString());
                objAprovadorFilial.Id_Regional = Convert.ToInt32(this.ddlRegionalCad.SelectedValue.ToString());
                objAprovadorFilial.Id_OrigemSol = Convert.ToInt32(ddlOrigemSol.SelectedValue.ToString());

                objAprovadorFilial.Id_NivelAprovacao = Convert.ToInt32(this.ddlNivelAprovacao.SelectedValue.ToString());
                objAprovadorFilial.Id_AprovaSegNivel = Convert.ToInt32(this.ddlSuperHierarquico.SelectedValue.ToString());

                objAprovadorFilial.Id_UsuarioAlteracao = Convert.ToInt32(FrameWork.BusinessLayer.Usuarios.BLAcesso.IdUsuarioLogado());

                objAprovadorFilial.Des_Filial = this.ddlFilialCad.SelectedItem.ToString();
                objAprovadorFilial.Des_Regional = this.ddlRegionalCad.SelectedItem.ToString();
                objAprovadorFilial.Des_NivelAprovacao = this.ddlNivelAprovacao.SelectedItem.ToString();

                objAprovadorFilial.Des_NumeroSol = this.txtNSolicitacao.Text;


                if (rdbVigencia_Temporaria.Checked)
                {
                    objAprovadorFilial.Des_Vigencia = this.txtInicio.Text + " à " + this.txtFim.Text;

                    objAprovadorFilial.InicioPeriodo = Convert.ToDateTime(this.txtInicio.Text);
                    objAprovadorFilial.FimPeriodo = Convert.ToDateTime(this.txtFim.Text);
                }
                else
                {
                    objAprovadorFilial.Des_Vigencia = "Definitiva";

                    objAprovadorFilial.InicioPeriodo = new DateTime();
                    objAprovadorFilial.FimPeriodo = new DateTime();
                }

                if (this.ddlOrigemSol.SelectedIndex > 0)
                {
                    if (!string.IsNullOrEmpty(txtNSolicitacao.Text))
                    {
                        objAprovadorFilial.Des_OrigemSol = this.ddlOrigemSol.SelectedItem.ToString() + " | " + txtNSolicitacao.Text;
                    }
                    else
                    {
                        objAprovadorFilial.Des_OrigemSol = this.ddlOrigemSol.SelectedItem.ToString();
                    }                                       
                }
                else
                {
                    objAprovadorFilial.Des_OrigemSol = string.Empty;
                }

                if (this.ddlSuperHierarquico.SelectedIndex > 0)
                {
                    objAprovadorFilial.Nom_Superior = this.ddlSuperHierarquico.SelectedItem.ToString();
                }
                else
                {
                    objAprovadorFilial.Nom_Superior = string.Empty;
                }

                if (AtributoAprovadorEditado != null)
                {
                    //remove perfil selecionado
                    foreach (AprovadorFilial gobjAprovadorFilial in objcolAprovadorFilial)
                    {
                        if (Convert.ToInt32(gobjAprovadorFilial.Id_AprovadorFilial) == Convert.ToInt32(objAprovadorFilial.Id_AprovadorFilial))
                        {
                            objcolAprovadorFilial.Remove(gobjAprovadorFilial);
                            break;
                        }                        
                    }
                    AtributoAprovadorEditado = null;
                }

                objcolAprovadorFilial.Add(objAprovadorFilial);

                //atualiza vs
                ColAtributosAprovador = objcolAprovadorFilial;

                this.radGridAtributosAprovador.DataSource = objcolAprovadorFilial;
                this.radGridAtributosAprovador.DataBind();

                //limpa os campos
                this.chkAprovaAreaSeg.Checked = false;
                this.chkAprovaCont.Checked = false;
                this.chkAprovaAreaTI.Checked = false;
                this.chkAprovaCracha.Checked = false;

                this.txtJustificativa.Text = string.Empty;

                this.ddlRegionalCad.SelectedIndex = 0;
                this.ddlFilialCad.Items.Clear();
                this.ddlFilialCad.Enabled = false;
                BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialCad, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

                this.ddlNivelAprovacao.SelectedIndex = 0;
                this.ddlSuperHierarquico.SelectedIndex = 0;
                this.ddlOrigemSol.SelectedIndex = 0;

                this.txtNSolicitacao.Text = string.Empty;

                this.txtInicio.Text = string.Empty;
                this.txtFim.Text = string.Empty;

                rdbVigencia_Definitiva.Checked = true;
            }
        }
        #endregion

        #region "Vigencia"

            protected void rdbVigencia_Definitiva_CheckedChanged(object sender, EventArgs e)
            {
                /*
                this.lblInicio.Visible = false;
                this.lblFim.Visible = false;
                this.lblJustificativa.Visible = false;
                */


                this.txtJustificativa.Enabled = !rdbVigencia_Definitiva.Checked;
                this.txtInicio.Enabled = !rdbVigencia_Definitiva.Checked;
                this.txtFim.Enabled = !rdbVigencia_Definitiva.Checked;
            }

            protected void rdbVigencia_Temporaria_CheckedChanged(object sender, EventArgs e)
            {
                /*
                this.lblInicio.Visible = true;
                this.lblFim.Visible = true;
                this.lblJustificativa.Visible = true;
                */

                this.txtJustificativa.Enabled = rdbVigencia_Temporaria.Checked;
                this.txtInicio.Enabled = rdbVigencia_Temporaria.Checked;
                this.txtFim.Enabled = rdbVigencia_Temporaria.Checked;
            }

        #endregion


        #region Grid Atributos Aprovador Listagem

            #region NeedDataSource
            /// <summary>
            ///     radGridAtributosAprovador
            /// </summary>
            /// <history>
            ///     [haguiar_5] created 17/02/2011
            /// </history>
            protected void radGridAtributosAprovador_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
            {
                if (Page.IsPostBack)
                {
                    //this.BindArea(Enums.TipoBind.SemDataBind);

                    if (this.ColAtributosAprovador != null && ColAtributosAprovador.Count != 0)
                    {
                        radGridAtributosAprovador.DataSource = ColAtributosAprovador;
                    }
                }
            }
            #endregion        
        
            #region ItemCommand

            /// <summary>
            ///     radGridAtributosAprovador
            /// </summary>
            /// <history>
            ///     [haguiar_5] created 17/02/2011
            ///     [haguiar] modify 26/04/2011 14:27
            ///     adicionar chkAprovaAreaTI
            /// </history>
            protected void radGridAtributosAprovador_ItemCommand(object source, GridCommandEventArgs e)
            {
                if (e.CommandName.Trim() == "Ativar")
                {
                    bool blnSituacao = true;

                    int intIdArea = Convert.ToInt32(e.CommandArgument.ToString().Trim());

                    Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                    ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];

                    try
                    {
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
                        if (this.ColAtributosAprovador != null)
                        {
                            foreach (AprovadorFilial gobjAprovadorFilial in ColAtributosAprovador)
                            {
                                if (Convert.ToInt32(gobjAprovadorFilial.Id_AprovadorFilial) == Convert.ToInt32(e.CommandArgument.ToString()))
                                {
                                    gobjAprovadorFilial.Flg_Situacao = blnSituacao;

                                    break;
                                }
                            }
                        }
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
                            if (this.ColAtributosAprovador != null)
                            {
                                objcolAprovadorFilial = ColAtributosAprovador;

                                //edita perfil selecionado
                                foreach (AprovadorFilial gobjAprovadorFilial in objcolAprovadorFilial)
                                {
                                    if (Convert.ToInt32(gobjAprovadorFilial.Id_AprovadorFilial) == Convert.ToInt32(e.CommandArgument.ToString()))
                                    {
                                        //objAprovadorFilial.Id_AprovadorFilial = this.Id_AprovadorFilial;

                                        this.chkAprovaAreaSeg.Checked = gobjAprovadorFilial.Flg_AprovaAreaSeg;
                                        this.chkAprovaCont.Checked=gobjAprovadorFilial.Flg_AprovaContingencia;
                                        this.chkAprovaAreaTI.Checked = gobjAprovadorFilial.Flg_AprovaAreaTI;
                                        this.chkAprovaCracha.Checked = gobjAprovadorFilial.Flg_AprovaCracha;

                                        this.txtNSolicitacao.Text = gobjAprovadorFilial.Des_NumeroSol;
                                        
                                        this.txtJustificativa.Text=gobjAprovadorFilial.Des_Justificativa;

                                        this.txtInicio.Text = gobjAprovadorFilial.InicioPeriodo.ToShortDateString();
                                        this.txtFim.Text = gobjAprovadorFilial.FimPeriodo.ToShortDateString();

                                        BLUtilitarios.ConsultarValorCombo(ref this.ddlRegionalCad, gobjAprovadorFilial.Id_Regional.ToString());

                                        this.PopularFilial(ref this.ddlRegionalCad, ref ddlFilialCad);

                                        BLUtilitarios.ConsultarValorCombo(ref this.ddlFilialCad, gobjAprovadorFilial.Id_Filial.ToString());
                                        BLUtilitarios.ConsultarValorCombo(ref this.ddlNivelAprovacao, gobjAprovadorFilial.Id_NivelAprovacao.ToString());
                                        BLUtilitarios.ConsultarValorCombo(ref this.ddlSuperHierarquico, gobjAprovadorFilial.Id_AprovaSegNivel.ToString());
                                        BLUtilitarios.ConsultarValorCombo(ref this.ddlOrigemSol, gobjAprovadorFilial.Id_OrigemSol.ToString());

                                        if (gobjAprovadorFilial.InicioPeriodo.ToString() == new DateTime().ToString())
                                        {
                                            rdbVigencia_Definitiva.Checked = true;
                                            rdbVigencia_Temporaria.Checked = false;

                                            this.txtInicio.Text = string.Empty;
                                            this.txtFim.Text = string.Empty;

                                            /*
                                            this.lblInicio.Visible = false;
                                            this.lblFim.Visible = false;
                                            this.lblJustificativa.Visible = false;
                                            */

                                            this.txtJustificativa.Enabled = false;
                                            this.txtInicio.Enabled = false;
                                            this.txtFim.Enabled = false;
                                        }
                                        else
                                        {
                                            rdbVigencia_Temporaria.Checked = true;
                                            rdbVigencia_Definitiva.Checked = false;

                                            this.txtInicio.Text = gobjAprovadorFilial.InicioPeriodo.ToShortDateString();
                                            this.txtFim.Text = gobjAprovadorFilial.FimPeriodo.ToShortDateString();

                                            /*
                                            this.lblInicio.Visible = true;
                                            this.lblFim.Visible = true;
                                            this.lblJustificativa.Visible = true;
                                            */

                                            this.txtJustificativa.Enabled = true;
                                            this.txtInicio.Enabled = true;
                                            this.txtFim.Enabled = true;
                                        }

                                        if ((AtributoAprovadorEditado != null) && (AtributoAprovadorEditado.Flg_Situacao))
                                        {
                                            objcolAprovadorFilial.Add(AtributoAprovadorEditado);
                                        }

                                        this.AtributoAprovadorEditado = gobjAprovadorFilial;

                                        //remove da lista para editar somente objetos ativos
                                        if (gobjAprovadorFilial.Flg_Situacao)
                                        {
                                            objcolAprovadorFilial.Remove(gobjAprovadorFilial);

                                            //atualiza grid
                                            radGridAtributosAprovador.DataSource = objcolAprovadorFilial;
                                            radGridAtributosAprovador.DataBind();
                                        }
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
                            if (this.ColAtributosAprovador != null)
                            {
                                objcolAprovadorFilial = ColAtributosAprovador;

                                //remove perfil selecionado
                                foreach (AprovadorFilial gobjAprovadorFilial in objcolAprovadorFilial)
                                {
                                    if (Convert.ToInt32(gobjAprovadorFilial.Id_AprovadorFilial) == Convert.ToInt32(e.CommandArgument.ToString()))
                                    {
                                        objcolAprovadorFilial.Remove(gobjAprovadorFilial);

                                        break;
                                    }
                                }
                                ColAtributosAprovador = objcolAprovadorFilial;
                            }

                            radGridAtributosAprovador.DataSource = objcolAprovadorFilial;
                            radGridAtributosAprovador.DataBind();
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
            ///     radGridAtributosAprovador_ItemDataBound
            /// </summary>
            /// <history>
            ///     [haguiar_5] create 17/02/2011     
            ///     [haguiar] modify 26/04/2011 14:29
            ///     inserir Flg_AprovaAreaTI
            /// </history> 
            protected void radGridAtributosAprovador_ItemDataBound(object sender, GridItemEventArgs e)
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
                        btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_AprovadorFilial").ToString();
                        //btnEditar.Visible = true;
                        e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);

                        btnEditar.Visible = false;
                        e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                        e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                        e.Item.Style["cursor"] = "hand";

                        // Cursor 
                        int intCell = e.Item.Cells.Count;
                        for (int @int = 0; @int <= intCell - 3; @int++)
                        {
                            e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                        }

                    }

                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_AprovaAreaSeg")) == false)
                    {
                        e.Item.Cells[5].Text = "Não";
                    }
                    else
                    {                      
                        e.Item.Cells[5].Text = "Sim";
                    }

                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_AprovaContingencia")) == false)
                    {
                        e.Item.Cells[6].Text = "Não";
                    }
                    else
                    {
                        e.Item.Cells[6].Text = "Sim";
                    }

                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_AprovaAreaTI")) == false)
                    {
                        e.Item.Cells[7].Text = "Não";
                    }
                    else
                    {
                        e.Item.Cells[7].Text = "Sim";
                    }

                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_AprovaCracha")) == false)
                    {
                        e.Item.Cells[8].Text = "Não";
                    }
                    else
                    {
                        e.Item.Cells[8].Text = "Sim";
                    }

                    if (Permissoes.Alteração() && btnRemover != null)
                    {
                        btnRemover.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_AprovadorFilial").ToString();

                        if (Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id_AprovadorFilial").ToString()) != 0)
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
                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_AprovadorFilial").ToString();
                    }
                    else
                    {

                        if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Situacao")))
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                            btnAtivar.ToolTip = "Inativar";
                            btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_AprovadorFilial").ToString();
                        }
                        else
                        {
                            btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                            btnAtivar.Enabled = false;
                            btnAtivar.ToolTip = "Registro inativo";

                            //btnAtivar.ToolTip = "Ativar";
                            //btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_AprovadorFilial").ToString();
                        }
                    }

                }

                e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                e.Item.Style["cursor"] = "hand";
            }
            #endregion
        #endregion

        #region Grid Usuario Listagem

        #region NeedDataSource
        protected void radGridUsuario_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.BindUsuario(Enums.TipoBind.SemDataBind);
            }
        }        
        #endregion

        #region ItemCommand

        /// <summary>
        ///     radGridUsuario
        /// </summary>
        /// <history>
        ///     [haguiar_5] created 16/02/2011
        /// </history>        
        protected void radGridUsuario_ItemCommand(object source, GridCommandEventArgs e)
        {

            /*
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
            */

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
        protected void radGridUsuario_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                ImageButton btnEditar;
                btnEditar = (ImageButton)e.Item.FindControl("btnEditar");

                Telerik.WebControls.GridDataItem dataItem = (Telerik.WebControls.GridDataItem)e.Item;
                //ImageButton btnAtivar = (ImageButton)dataItem["Ativar"].Controls[0];
                                
                if (Permissoes.Alteração() && btnEditar != null)
                {
                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Usuario").ToString();
                    //btnEditar.Visible = true;
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                }

                /*

                if (DataBinder.Eval(e.Item.DataItem, "Flg_Situacao") == DBNull.Value)
                {
                    btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                    btnAtivar.ToolTip = "Inativar";
                    btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Usuario").ToString();
                }
                else
                {

                    if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Flg_Situacao")))
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_ativo.gif";
                        btnAtivar.ToolTip = "Inativar";
                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Usuario").ToString();
                    }
                    else
                    {
                        btnAtivar.ImageUrl = "~/imagens/icones/ico_inativo.gif";
                        btnAtivar.ToolTip = "Ativar";
                        btnAtivar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "Id_Filial").ToString();
                    }
                }
                */

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

            this.txtInicio.Attributes.Add("OnKeyPress", "return FormataData(event,this);");
            this.txtFim.Attributes.Add("OnKeyPress", "return FormataData(event,this);");

            this.txtNSolicitacao.Attributes.Add("OnKeyPress", "return FormataCodigo(event,this);");
            this.txtNSolicitacao.MaxLength = 10;
            
            this.txtJustificativa.MaxLength = 50;

            //this.txtCodigoFilialCorporate.MaxLength = 4;
            //this.txtCodigoFilialCorporate.Attributes.Add("OnKeyPress", "return FormataCodigo(event,this);");

            //this.txtQtdToleranciaAcesso.MaxLength = 4;
            //this.txtQtdToleranciaAcesso.Attributes.Add("OnKeyPress", "return FormataCodigo(event,this);");

            lblMensagem.Visible = false;
            lblMensagem.Text = string.Empty;

            this.PopularRegional();
            //this.PopularFusoHorario();

            this.PopulaComboSuperior();
            this.PopulaComboNivel();

            PopulaOrigemSolicitacao();

            //ddlCidade.Items.Clear();
            //ddlCidade.Enabled = false;

            //this.PopulaGrupoColetores();

            radGridAtributosAprovador.DataSource = this.ColAtributosAprovador;
            radGridAtributosAprovador.DataBind();

            //BLUtilitarios.InseriMensagemDropDownList(ref ddlFusoHorario, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            BLUtilitarios.InseriMensagemDropDownList(ref ddlFilialList, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            //BLUtilitarios.InseriMensagemDropDownList(ref ddlEstado, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            //BLUtilitarios.InseriMensagemDropDownList(ref ddlCidade, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            //BLUtilitarios.InseriMensagemDropDownList(ref ddlGrupoColetores, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
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
        ///     [haguiar] modify 14/03/2011
        ///     listar apenas filiais com área
        /// </history>
        /// 
        
        protected void PopularFilial(ref DropDownList ddlRegional, ref DropDownList ddlFilial)
        {
            BLFilial objBlFilial = new BLFilial();
            Collection<SafWeb.Model.Filial.Filial> colFilial;
            
            //DataTable dttFilial = new DataTable();

            try
            {
                if (ddlRegional.SelectedIndex != 0)
                {
                    //dttFilial = objBlFilial.Listar_DataTable(Convert.ToInt32(ddlRegional.SelectedItem.Value),0,string.Empty);

                    //listar apenas filiais com área
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

        #region Nível aprovacao
        private void PopulaComboNivel()
        {
            BLSolicitacao objBLSolicitacao = new BLSolicitacao();
            Collection<SafWeb.Model.Solicitacao.NivelAprovacao> colNivelAprovacao;

            try
            {
                colNivelAprovacao = objBLSolicitacao.ListarNivelAprovacao();

                ddlNivelAprovacao.DataSource = colNivelAprovacao;
                ddlNivelAprovacao.DataTextField = "Descricao";
                ddlNivelAprovacao.DataValueField = "Codigo";
                ddlNivelAprovacao.DataBind();

                ddlNivelAprovacao.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "-1"));

                ddlNivelAprovacao.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Origem Solicitacao
        private void PopulaOrigemSolicitacao()
        {
            BLAprovadorFilial objBLAprovadorFilial = new BLAprovadorFilial();

            try
            {
                ddlOrigemSol.DataSource = objBLAprovadorFilial.ListarOrigemChamado();
                ddlOrigemSol.DataTextField = "Des_SistOrigemChamado";
                ddlOrigemSol.DataValueField = "Id_SistOrigemChamado";
                ddlOrigemSol.DataBind();

                ddlOrigemSol.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "-1"));

                ddlOrigemSol.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Superior Hierarquico
        /// <summary>
        ///     Preenche os combos de superior hierárquico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar_5] create 16/02/2011
        /// </history>
        private void PopulaComboSuperior()
        {
            BLAprovadorFilial objBLAprovadorFilial = null;

            try
            {
                objBLAprovadorFilial = new BLAprovadorFilial();

                this.ddlSuperiorHierarquicoList.DataSource = objBLAprovadorFilial.Listar(0,0,0,string.Empty);
                this.ddlSuperiorHierarquicoList.DataTextField = "USU_C_NOME";
                this.ddlSuperiorHierarquicoList.DataValueField = "Id_Usuario";
                this.ddlSuperiorHierarquicoList.DataBind();

                this.ddlSuperHierarquico.DataSource = objBLAprovadorFilial.Listar(0, 0, 0, string.Empty);
                this.ddlSuperHierarquico.DataTextField = "USU_C_NOME";
                this.ddlSuperHierarquico.DataValueField = "Id_Usuario";
                this.ddlSuperHierarquico.DataBind();

                this.ddlSuperHierarquico.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "-1"));
                this.ddlSuperiorHierarquicoList.Items.Insert(0, new ListItem(BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.SELECIONE)), "-1"));

                ddlSuperHierarquico.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion        

        /*
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
        */

        /*
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
        */

        /*
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
        */

        #endregion

        #region Propriedades

        #region AtributoAprovadorEditado
        /// <summary>
        ///     Propriedade AtributoAprovadorEditado que armazena o atributo editado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [haguiar_5] created 18/02/2011
        /// </history>
        private AprovadorFilial AtributoAprovadorEditado
        {
            get
            {
                /*
                if (this.ViewState["vsAreaEditada"] == null)
                {
                    this.ViewState.Add("vsAreaEditada", new Area());
                }
                */

                return (AprovadorFilial)this.ViewState["vsAtributoAprovadorEditado"];
            }

            set
            {
                this.ViewState.Add("vsAtributoAprovadorEditado", value);
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
        

        #region Id_AprovadorFilial
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade Id_AprovadorFilial
        /// </summary> 
        /// <history> 
        ///     [haguiar_5] 18/02/2010 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int Id_AprovadorFilial
        {
            get
            {
                if ((this.ViewState["vsId_AprovadorFilial"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsId_AprovadorFilial"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsId_AprovadorFilial", value);
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

        #region IdUsuario
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade IdUsuario
        /// </summary> 
        /// <history> 
        ///     [haguiar] 17/02/2011 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public int IdUsuario
        {
            get
            {
                if ((this.ViewState["vsIdUsuario"] != null))
                {
                    return Convert.ToInt32(this.ViewState["vsIdUsuario"]);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState.Add("vsIdUsuario", value);
            }
        }

        #endregion

        #region ColAtributosAprovador
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Propriedade ColAtributosAprovador
        /// </summary> 
        /// <history> 
        ///     [haguiar_5] 17/02/2011 Created
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public Collection<SafWeb.Model.Filial.AprovadorFilial> ColAtributosAprovador
        {
            get
            {
                if (this.ViewState["vsColAtributosAprovador"] == null)
                {
                    this.ViewState.Add("vsColAtributosAprovador", new Collection<AprovadorFilial>());
                }

                return (Collection<AprovadorFilial>)this.ViewState["vsColAtributosAprovador"];
                
            }
            set
            {
                this.ViewState.Add("vsColAtributosAprovador", value);
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
        ///     [haguiar_9004] modify 12/08/2011 16:46
        ///     incluir Id_UsuarioAlteracao = usuariologado quando Id_UsuarioAlteracao = 0
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

            
            if (this.ddlFilialCad.SelectedItem == null)
            {
                RadAjaxPanel1.Alert("Selecione uma Filial.");
                blnRetorno = false;
            }

            if (ColAtributosAprovador != null)
            {
                foreach (AprovadorFilial gobjAprovadorFilial in ColAtributosAprovador)
                {

                    if (gobjAprovadorFilial.Id_AprovaSegNivel < 1)
                    {
                        RadAjaxPanel1.Alert("Selecione um superior hierárquico para todos os atributos de aprovador.");

                        blnRetorno = false;
                        break;
                    }
                    
                    if (gobjAprovadorFilial.Id_UsuarioAlteracao == 0)
                        gobjAprovadorFilial.Id_UsuarioAlteracao = Convert.ToInt32(FrameWork.BusinessLayer.Usuarios.BLAcesso.IdUsuarioLogado());
                }
            }



            /*
            if (this.ddlPeriodicidadeCad.SelectedIndex == 0)
            {
                RadAjaxPanel1.Alert("Selecione uma Periodicidade.");
                blnRetorno = false;
            }
            */
            /*
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
            */
            /*
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
            */


            return blnRetorno;
        }

        #endregion

        #endregion
    }
}