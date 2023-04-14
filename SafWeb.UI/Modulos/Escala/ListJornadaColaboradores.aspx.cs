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
using SafWeb.BusinessLayer.Escala;
using SafWeb.Model.Escala;
using SafWeb.BusinessLayer.Colaborador;
using System.Collections.ObjectModel;
using SafWeb.Model.Colaborador;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Utilitarios;
using FrameWork.Model.Idioma;
using FrameWork.BusinessLayer.Idioma;
using System.Text;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class ListJornadaColaboradores : System.Web.UI.Page
    {
        JornadaColaboradores gobjJornadaColaboradores;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.VerificarQuery(this.Request.QueryString["open"]);
            }
        }

        #region Bind
        /// <summary>
        /// Bind.
        /// </summary>
        /// <param name="pobjJornadaColaboradores">Objeto Jornada Colaboradores</param>
        /// <history>
        ///     [cmarchi] created 14/1/2010
        /// </history>
        private void Bind(Enums.TipoTransacao penmTipoTransacao)
        {
            if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.lblRespJornada.Text = this.gobjJornadaColaboradores.DescricaoJornada.Trim();

                this.PopularListaColaboradores();
            }
        }
        #endregion

        #region Botões

        #region Inserir
        /// <summary>
        /// Botão Inserir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 14/1/2010
        /// </history>
        protected void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid && this.ValidarCampos())
            {
                //Chama o javaScript da página Pai para retornar o código dos Colaboradores selecionados.
                string strColaboradores = this.ObterColaboradoresSelecionados();

                string strParamentros = this.LinhaGrid.ToString() + "#" + this.IdJornada + "#" + strColaboradores;

                ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('" +
                BLEncriptacao.EncQueryStr(strParamentros) + "');</script>");

                ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
            }
        }
        
        #endregion

        #endregion

        #region ObterColaboradoresSelecionados
        /// <summary>
        /// Obtém os Colaboradores selecionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 14/1/2010
        ///     [cmarchi] cmodify 15/1/2010
        /// </history>
        private string ObterColaboradoresSelecionados()
        {
            int[] arrIndices = this.lstColaboradores.GetSelectedIndices();
            StringBuilder strIdColaboradores = new StringBuilder();

            foreach (int i in arrIndices)
            {
                strIdColaboradores.Append(
                    this.lstColaboradores.Items[i].Value + ",");
            }

            strIdColaboradores.Remove(strIdColaboradores.Length - 1, 1);

            return strIdColaboradores.ToString();
        }
        #endregion

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
            BLColaborador objBLColaborador = new BLColaborador();
            Collection<Colaborador> colColaboradores = null;

            try
            {
                colColaboradores = objBLColaborador.Obter(
                                                this.gobjJornadaColaboradores.CodigosColaboradores.Split(','));

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

        #region Propriedades

        #region Id Jornada
        /// <summary>
        ///     Propriedade IdJornada que contem o id da Jornada.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 18/1/2010 
        /// </history>
        private int IdJornada
        {
            get
            {
                if (this.ViewState["vsJornada"] == null)
                {
                    this.ViewState.Add("vsJornada", -1);
                }

                return Convert.ToInt32(this.ViewState["vsJornada"]);
            }

            set
            {
                this.ViewState.Add("vsJornada", value);
            }
        }
        #endregion

        #region Linha Grid
        /// <summary>
        ///     Propriedade LinhaGrid que contem o índice a linha no grid.
        /// </summary>
        /// <history>
        ///     [cmarchi] created 14/1/2010 
        /// </history>
        private int LinhaGrid
        {
            get
            {
                if (this.ViewState["vsLinha"] == null)
                {
                    this.ViewState.Add("vsLinha", -1);
                }

                return Convert.ToInt32(this.ViewState["vsLinha"]);
            }

            set
            {
                this.ViewState.Add("vsLinha", value);
            }
        }
        #endregion

        #endregion

        #region Validações

        #region Validar
        /// <summary>
        /// Validar.
        /// </summary>
        ///<param name="objJornadaColaboradores">Objeto Jornada dos Colaboradores</param>
        /// <returns>True - Validação OK, False - Erro</returns>
        /// <history>
        ///     [cmarchi] created 14/1/2010
        /// </history>
        private bool Validar(JornadaColaboradores pobjJornadaColaboradores)
        {
            string[] arrCodigoColaboradores = pobjJornadaColaboradores.CodigosColaboradores.Split(',');

            if (arrCodigoColaboradores == null || arrCodigoColaboradores.Length == 0)
            {
                return false;
            }

            if (this.LinhaGrid < 0)
            {
                return false;
            }

            if (pobjJornadaColaboradores.IdJornada <= 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(pobjJornadaColaboradores.DescricaoJornada))
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Validar Campos
        /// <summary>
        /// Validar os Campos
        /// </summary>
        /// <returns>True - Validação OK, False - Erro</returns>
        /// <history>
        ///     [cmarchi] created 14/1/2010
        /// </history>
        private bool ValidarCampos()
        {
            if (this.lstColaboradores.GetSelectedIndices().Length == 0)
            {
                ClientScript.RegisterStartupScript(String.Empty.GetType(), "Erro", "<script>window.alert('Selecione um Colaborador');</script>");
                
                return false;
            }

            return true;
        }
        #endregion

        #endregion

        #region VerificarQuery
        /// <summary>
        /// Verifica a query.
        /// </summary>
        ///<param name="pstrQuery">Query</param>
        /// <history>
        ///     [cmarchi] created 14/1/2010
        /// </history>
        private void VerificarQuery(string pstrQuery)
        {
            if (!string.IsNullOrEmpty(pstrQuery))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split('#');

                if (arrParametros != null && arrParametros.Length == 5)
                {
                    //verifica se está editando uma escala
                    if (arrParametros[0] == "Sim")
                    {
                        this.gobjJornadaColaboradores = new JornadaColaboradores();

                        int intPosicaoLinha;
                        int intIdJornada;

                        this.gobjJornadaColaboradores.DescricaoJornada     = arrParametros[3];

                        if (Session["CodigosColaboradores"] != null)
                        {
                            arrParametros[4] = (string)Session["CodigosColaboradores"];
                            Session["CodigosColaboradores"] = null;
                            Session.Remove("CodigosColaboradores");

                        }

                        this.gobjJornadaColaboradores.CodigosColaboradores = arrParametros[4];

                        //verifica a linha grid
                        if (Int32.TryParse(arrParametros[1], out intPosicaoLinha))
                        {
                            this.LinhaGrid = intPosicaoLinha;
                        }                        

                        //verifica o id da jornada
                        if (Int32.TryParse(arrParametros[2], out intIdJornada))
                        {
                            this.gobjJornadaColaboradores.IdJornada = intIdJornada;
                            this.IdJornada = intIdJornada;
                        }

                        //faz a validação dos campos recebidos
                        if (this.Validar(this.gobjJornadaColaboradores))
                        {
                            this.Bind(Enums.TipoTransacao.DescarregarDados);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(String.Empty.GetType(),
                                "CloseRad", "<script>CloseWin();</script>");
                        }

                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(String.Empty.GetType(),
                        "CloseRad", "<script>CloseWin();</script>");
                }
            }
        }        
        #endregion       
    }
}
