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
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Utilitarios;
using System.Collections.ObjectModel;
using SafWeb.Model.Colaborador;
using SafWeb.BusinessLayer.Escala;
using SafWeb.Model.Escala;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using System.Text;
using SafWeb.BusinessLayer.Colaborador;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class CadJornadaEscala : FWPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.VerificarHiddenColaboradores();

            if (!this.Page.IsPostBack)
            {
                this.VerificarQueryTela(this.Request.QueryString["mod"]); 
            }
        }

        #region AdicionarColaboradorLista
        /// <summary>
        /// Adiciona os colaboradores na lista de colaboradores.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        private void AdicionarColaboradorLista(string[] arrIdColaboradores)
        {
            BLColaborador objBLColaborador = new BLColaborador();
            Collection<Colaborador> colColaborador = null;

            //obtém os colaboradores
            colColaborador = objBLColaborador.Obter(arrIdColaboradores);

            foreach (Colaborador objColaborador in colColaborador)
            {
                if (this.lstColaboradores.Items.FindByValue(objColaborador.IdColaborador.ToString()) == null)
                {
                    ListItem ltmColaborador = new ListItem(objColaborador.NomeColaborador + " - " +
                                                           objColaborador.CodigoColaborador,
                                                           objColaborador.IdColaborador.ToString());

                    this.lstColaboradores.Items.Add(ltmColaborador);
                }
            }
        }
        #endregion

        #region Avancar
        /// <summary>
        /// Grava as Jornadas dos Colaboradores e avança para a tela de horários dos colaboradores.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 15/1/2010
        /// </history>
        private void Avancar()
        {
            BLJornada objBLJornada = new BLJornada();

            try
            {                
                if (this.ValidarCampos())
                {
                    objBLJornada.Inserir(this.JornadaColaboradores);

                    string strParametro = BLEncriptacao.EncQueryStr("CadJor," + this.IdEscalacao.ToString());                        
                    this.Response.Redirect("CadEscalaHorarioColaborador.aspx?mod=" + strParametro,false);
                }                
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion

        #region Bind

        #region BindCadastro
        /// <summary>
        /// Bind Cadastro
        /// </summary>
        /// <param name="pblnSomenteRadGrid">Bind somente na radgrid</param>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        ///     [cmarchi] modify  20/1/2010
        /// </history>
        protected void BindCadastro(bool pblnSomenteRadGrid)
        {
            bool blnExcluirJornadas = false;

            if (!pblnSomenteRadGrid)
            {
                BLJornada objJornada = new BLJornada();                

                this.PopularListaJornadas();
                this.PopularListaColaboradores();
                /*
                if (lstColaboradores.Items.Count == 0)
                {
                    this.btnInserir.Enabled = false;
                */
                    //obtém as jornadas dos colaboradores já cadastrados
                    if (this.JornadaColaboradores.Count == 0)
                    {
                        try
                        {
                            this.JornadaColaboradores =
                                objJornada.ObterJornadaColaboradores(this.IdEscalacao);

                            //this.radGridJornadaColaboradores.Enabled = false;

                            //blnExcluirJornadas = true;
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                        }
                    }/*
                }
                else
                {
                    this.btnInserir.Enabled = true;
                }*/
            }

            this.radGridJornadaColaboradores.DataSource = this.JornadaColaboradores;
            this.radGridJornadaColaboradores.DataBind();

            if (blnExcluirJornadas)
            {
                this.JornadaColaboradores = null;
            }
        }
        #endregion

        #endregion

        #region Botões

        #region Avançar
        /// <summary>
        /// Botão Avançar - Tela de Definição de Horários dos Colaboradores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 29/11/2009
        /// </history>
        protected void btnAvancar_Click(object sender, EventArgs e)
        {
            this.Avancar();
        }        
        #endregion

        #region Inserir
        /// <summary>
        /// Insere dos colaboradores na grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 29/11/2009
        /// </history>
        protected void btnInserir_Click(object sender, EventArgs e)
        {
            if (this.ValidarInserir())
            {
                ListItem ltmJornada = this.lstJornadas.SelectedItem;

                JornadaColaboradores objJornadaColaboradores = new JornadaColaboradores();
                objJornadaColaboradores.IdJornada        = Convert.ToInt32(ltmJornada.Value);
                objJornadaColaboradores.DescricaoJornada = ltmJornada.Text;
                objJornadaColaboradores.IdEscalacao      = this.IdEscalacao;                

                //insere a jornada
                this.Inserir(ref objJornadaColaboradores);
            }
        }        
        #endregion

        #region Voltar
        /// <summary>
        /// Botão Voltar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] 29/11/2009
        /// </history>
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            string strParametro = BLEncriptacao.EncQueryStr("CadJor," + this.IdEscalacao.ToString());
            this.Response.Redirect("CadSelecaoEscalaColaborador.aspx?mod=" + strParametro); 
        }
        #endregion

        #endregion        

        #region EditarJornadaColaboradores
        /// <summary>
        ///    Editar Jornadas dos Colaboradores.
        /// </summary>
        /// <param name="pintLinhaGrid">Linha da Grid</param>
        /// <param name="pintIdEscalacao">Id da Escalação</param>
        /// <param name="pintIdJornada">Id da Jornada</param>
        /// <param name="pstrIdColaboradores">Id dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        ///     [haguiar] modify 16/11/2011 09:12
        ///     guardar codigos de colaboradores em sessao. valor muito grande para utilizar qs.
        /// </history>
        private void EditarJornadaColaboradores(int pintLinhaGrid, int pintIdJornada,
            string pstrDescricaoJornada, string pstrIdColaboradores)
        {
            string strParametro = string.Empty;

            strParametro = BLEncriptacao.EncQueryStr("Sim#" + pintLinhaGrid.ToString() + "#" +
               pintIdJornada + "#" + pstrDescricaoJornada + "#0"); //+ pstrIdColaboradores);

            Session.Add("CodigosColaboradores", pstrIdColaboradores);

            this.RadWindowCadastroEscala(strParametro);
        }
        #endregion        

        #region Grid Jornada Colaboradores

        #region NeedDataSource
        protected void radGridJornadaColaboradores_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Page.IsPostBack)
            {
                this.radGridJornadaColaboradores.DataSource = this.JornadaColaboradores;
            }
        }
        #endregion

        #region ItemCommand
        protected void radGridJornadaColaboradores_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName.Trim() == "Editar" && this.radGridJornadaColaboradores.Enabled)
            {
                if (e.CommandArgument.ToString().Trim() != string.Empty)
                {                    
                    this.EditarJornadaColaboradores(e.Item.ItemIndex,
                        Convert.ToInt32(e.Item.Cells[2].Text),
                        e.Item.Cells[3].Text,
                        e.Item.Cells[4].Text);                    
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
        protected void radGridJornadaColaboradores_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem | e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {                
                ImageButton btnEditar = (ImageButton)e.Item.FindControl("Editar");                

                if (btnEditar != null)
                {              
                    e.Item.Attributes.Add("onMouseOver", "selecionaGrid(this)");
                    e.Item.Attributes.Add("onMouseOut", "deSelecionaGrid(this)");
                    e.Item.Style["cursor"] = "hand";
                    e.Item.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);

                    // Cursor 
                    int intCell = e.Item.Cells.Count;
                    for (int @int = 0; @int <= intCell - 1; @int++)
                    {
                        e.Item.Cells[@int].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(btnEditar, ""));
                    }
                    
                    btnEditar.ToolTip = BLIdiomas.TraduzirMensagens(Mensagens.EDITAR);
                    btnEditar.CommandArgument = DataBinder.Eval(e.Item.DataItem, "IdEscalacao").ToString();
                    btnEditar.Visible = true;
                    
                }
            }
        }        
        #endregion

        #endregion                 

        #region Inserir Jornada Colaboradores
        /// <summary>
        ///    Editar Jornadas dos Colaboradores.
        /// </summary>
        /// <param name="pobjJornadaColaboradores">Objeto Jornada Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        private void Inserir(ref JornadaColaboradores pobjJornadaColaboradores)
        {
            if (this.ValidarInserir())
            {
                Collection<ListItem> colColaboradores = new Collection<ListItem>();
                BLJornada objBLJornada = new BLJornada();

                Collection<JornadaColaboradores> colJornadaCol = null;

                StringBuilder strCodigoColaboradores = new StringBuilder();
                StringBuilder strNomeColaboradores = new StringBuilder();

                foreach (ListItem var in this.lstColaboradores.Items)
                {
                    if (var.Selected)
                    {
                        colColaboradores.Add(var);

                        colJornadaCol = this.JornadaColaboradores;

                        strCodigoColaboradores.Append(var.Value + ",");
                        strNomeColaboradores.Append(var.Text + ",");
                    }
                }

                strCodigoColaboradores.Remove(strCodigoColaboradores.Length - 1, 1);
                strNomeColaboradores.Remove(strNomeColaboradores.Length - 1, 1);

                pobjJornadaColaboradores.CodigosColaboradores = strCodigoColaboradores.ToString();
                pobjJornadaColaboradores.NomesColaboradores = strNomeColaboradores.ToString();

                int intPosicaoJornada = BLJornada.IndiceJornadaColaborador(this.JornadaColaboradores,
                                                        pobjJornadaColaboradores.IdJornada);

                objBLJornada.InserirJornadaColaborador(ref colJornadaCol,
                    intPosicaoJornada, pobjJornadaColaboradores);

                //remove da lista
                foreach (ListItem var in colColaboradores)
                {
                    this.lstColaboradores.Items.Remove(var);
                }

                //insere na grid
                this.radGridJornadaColaboradores.DataSource = this.JornadaColaboradores;
                this.radGridJornadaColaboradores.DataBind();
            }
        }
        #endregion

        #region Popular Listas
        
        #region PopularListaColaboradores
        /// <summary>
        /// Preenche a Lista de Colaboradores.
        /// </summary>
        /// <param name="parrColaboradores">Array com os Id dos Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        private void PopularListaColaboradores()
        {
            BLJornada objBLJornada = new BLJornada();
            Collection<Colaborador> colColaboradores = null;

            try
            {
                colColaboradores = objBLJornada.ObterColaboradoresNaoContemJornada(
                                                this.IdEscalacao);

                this.lstColaboradores.Items.Clear(); 

                foreach (Colaborador objColaborador in colColaboradores)
                {
                    ListItem limColaborador = new ListItem();
                    limColaborador.Text = objColaborador.NomeColaborador + " - " + objColaborador.CodigoColaborador;
                    limColaborador.Value = objColaborador.IdColaborador.ToString();

                    this.lstColaboradores.Items.Add(limColaborador);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }            
        }
        #endregion

        #region PopularListaJornadas
        /// <summary>
        /// Preenche a Lista de Jornadas.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        private void PopularListaJornadas()
        {
            BLJornada objBLJornada = new BLJornada();
            Collection<Jornada> colJornadas = null;

            try
            {
                colJornadas = objBLJornada.ListarJornadas();

                this.lstJornadas.DataTextField = "DescricaoJornada";
                this.lstJornadas.DataValueField = "IdJornada";

                this.lstJornadas.DataSource = colJornadas;
                this.lstJornadas.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }            
        }
        #endregion

        #endregion

        #region Propriedades

        #region IdEscalacao
        /// <summary>
        ///     Propriedade Id_Escalacao utilizada para Editar uma Escalacao
        /// </summary>
        /// <history>
        ///     [cmarchi] created 13/1/2010 
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

        #region JornadaColaboradores
        /// <summary>
        ///     Propriedade JornadaColaboradores que contem as jornada dos colaboradores.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 13/1/2010 
        /// </history>
        private Collection<JornadaColaboradores> JornadaColaboradores
        {
            get
            {
                if (this.ViewState["vsJornadaCol"] == null)
                {
                    this.ViewState.Add("vsJornadaCol", new Collection<JornadaColaboradores>());
                }

                return (Collection<JornadaColaboradores>)this.ViewState["vsJornadaCol"];
            }

            set
            {
                this.ViewState.Add("vsJornadaCol", value);
            }
        }
        #endregion
        
        #endregion

        #region RadWindow

        /// <summary>
        ///     Abre a RadWindow com a tela de Alteração de jornada dos colaboradores.
        /// </summary>
        /// <param name="pstrParamentro">Parâmetro da query string</param>
        /// <history>
        ///   [cmarchi] created 29/11/2009
        ///   [cmarchi] created 13/1/2010
        ///   [haguiar] modify 16/11/2011 09:13
        ///   utilizar sessao para armazenar codigo de colaboradores. 
        /// </history>
        protected void RadWindowCadastroEscala(string pstrParamentro)
        {
            //cria radwindow
            RadWindow rwdWindow = new RadWindow();
            RadWindowManager rwmWindowManager = new RadWindowManager();

            rwmWindowManager.Skin = "Office2007";
            //Não exibe nenhum botão na janela
            rwmWindowManager.Behavior = Telerik.WebControls.RadWindowBehaviorFlags.Close;
            rwmWindowManager.ShowContentDuringLoad = false;
            rwmWindowManager.VisibleStatusbar = false;

            rwdWindow.Width = Unit.Pixel(543);
            rwdWindow.Height = Unit.Pixel(385);
            rwdWindow.Modal = true;
            rwdWindow.ReloadOnShow = true;
            rwdWindow.DestroyOnClose = true;
            rwdWindow.VisibleOnPageLoad = true;

            rwdWindow.Title = "Edição de Jornadas de Colaboradores";

            rwdWindow.NavigateUrl = "ListJornadaColaboradores.aspx?open=" + pstrParamentro;
            rwmWindowManager.Windows.Add(rwdWindow);

            Panel pnlEscala = null;

            //Tenta encontrar na master
            pnlEscala = (Panel)this.FindControl("pnlJanHorarioColaboradores");
            pnlJornadaColaborador.Controls.Add(rwmWindowManager);
        }
        #endregion

        #region Validações

        #region Validar Campos
        /// <summary>
        /// Valida os Campos
        /// </summary>
        /// <returns>True - Validação OK, False - Erro</returns>
        /// <history>
        ///     [cmarchi] created 15/1/2010
            ///     [cmarchi] modify 8/2/2010
        /// </history>
        private bool ValidarCampos()
        {
            if (this.lstColaboradores.Items.Count > 0)
            {
                this.RadAjaxPanel1.Alert("Defina uma jornada para cada colaborador.");
                return false;
            }

            return true;
        }
        #endregion

        #region Validar no Inserir

        /// <summary>
        ///     Verifica se os alguma jornada e colaboradores foram selecionados.
        /// </summary>
        /// <returns>True - Válidos, False - Erros</returns>
        /// <history>
        ///     [cmarchi] created 14/01/2010
        ///     [cmarchi] modify 8/2/2010
        /// </history>
        private bool ValidarInserir()
        {
            bool blnRetorno = true;

            if (this.lstJornadas.SelectedIndex == -1)
            {
                RadAjaxPanel1.Alert("Selecione uma Jornada.");
                blnRetorno = false;
            }

            if (this.lstColaboradores.Items.Count > 0 && this.lstColaboradores.SelectedIndex == -1)
            {
                RadAjaxPanel1.Alert("Defina uma jornada para cada colaborador.");
                blnRetorno = false;
            }

            if (this.lstColaboradores.Items.Count == 0 && this.lstColaboradores.SelectedIndex == -1)
            {               
                blnRetorno = false;
            }

            return blnRetorno;
        }
        #endregion

        #endregion

        #region VerificarQueryTela
        /// <summary>
        /// Verifica a query a tela que a chamou.
        /// </summary>
        ///<param name="pstrQuery">Query</param>
        /// <history>
        ///     [cmarchi] created 13/1/2010
        /// </history>
        private void VerificarQueryTela(string pstrQuery)
        {
            if(!string.IsNullOrEmpty(pstrQuery))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split(',');

                //verifica a tela que a chamou
                if (arrParametros[0] == "CadSel" || arrParametros[0] == "CadHor")
                {
                    int intIdEscalacao;

                    if (Int32.TryParse(arrParametros[1], out intIdEscalacao))
                    {
                        this.IdEscalacao = intIdEscalacao;
                        this.BindCadastro(false);

                        return;
                    }
                }                                
            }

            this.Response.Redirect("CadSelecaoEscalaColaborador.aspx");
        }
        #endregion

        #region VerificarHiddenColaboradores
        /// <summary>
        /// Verifica o valor da query string.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 6/1/2010
        /// </history>
        private void VerificarHiddenColaboradores()
        {
            if (!string.IsNullOrEmpty(this.txtHiddenColaboradores.Value))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(this.txtHiddenColaboradores.Value).Split('#');
                this.txtHiddenColaboradores.Value = string.Empty;

                int intIdJornada;
                int intLinhaPosicao;

                if (Int32.TryParse(arrParametros[0], out intLinhaPosicao))
                {
                    if (Int32.TryParse(arrParametros[1], out intIdJornada))
                    {
                        string[] arrIdColaboradores = arrParametros[2].Split(',');

                        if (arrIdColaboradores != null && arrIdColaboradores.Length > 0)
                        {
                            Collection<JornadaColaboradores> colJornadaColaboradores = this.JornadaColaboradores;

                            this.AdicionarColaboradorLista(arrIdColaboradores);
                            BLJornada.ExcluirColaborador(ref colJornadaColaboradores,
                                 intLinhaPosicao, intIdJornada, arrIdColaboradores);

                            this.BindCadastro(true);
                        }
                    }
                }
            }
        }        
        #endregion
    }
}
