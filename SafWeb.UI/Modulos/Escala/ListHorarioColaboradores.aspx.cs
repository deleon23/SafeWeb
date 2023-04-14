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
using SafWeb.Model.Escala;
using FrameWork.Model.Utilitarios;
using SafWeb.BusinessLayer.Colaborador;
using System.Collections.ObjectModel;
using SafWeb.Model.Colaborador;
using FrameWork.BusinessLayer.Utilitarios;
using System.Text;

namespace SafWeb.UI.Modulos.Escala
{
    public partial class ListHorarioColaboradores : System.Web.UI.Page
    {
        HorarioColaboradores gobjHorarioColaboradores;

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
        /// <param name="penmTipoTransacao">Tipo de transa��o</param>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        /// </history>
        private void Bind(Enums.TipoTransacao penmTipoTransacao)
        {
            if (penmTipoTransacao == Enums.TipoTransacao.DescarregarDados)
            {
                this.lblRespHorario.Text = this.gobjHorarioColaboradores.HorarioColaborador;

                this.PopularListaColaboradores();
            }
        }
        #endregion

        #region Bot�es

        #region Selecionar
        /// <summary>
        /// Bot�o Selecionar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        /// </history>
        protected void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid && this.ValidarCampos())
            {
                //Chama o javaScript da p�gina Pai para retornar o c�digo dos Colaboradores selecionados.
                string strColaboradores = this.ObterColaboradoresSelecionados();

                string strParamentros = this.LinhaGrid.ToString() + "#" + this.lblRespHorario.Text + "#" + strColaboradores;

                ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('" +
                BLEncriptacao.EncQueryStr(strParamentros) + "');</script>");

                ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
            }
        }
        #endregion

        #endregion

        #region Linha Grid
        /// <summary>
        ///     Propriedade LinhaGrid que contem o �ndice a linha no grid.
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

        #region ObterColaboradoresSelecionados
        /// <summary>
        /// Obt�m os Colaboradores selecionados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 18/1/2010
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
        ///     [cmarchi] created 18/1/2010
        /// </history>
        private void PopularListaColaboradores()
        {
            BLColaborador objBLColaborador = new BLColaborador();
            Collection<Colaborador> colColaboradores = null;

            try
            {
                colColaboradores = objBLColaborador.Obter(
                                                this.gobjHorarioColaboradores.CodigosColaboradores.Split(','));

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

        #region Valida��es

        #region Validar
        /// <summary>
        /// Validar.
        /// </summary>
        ///<param name="objHorariosColaboradores">Objeto Hor�rio dos Colaboradores</param>
        /// <returns>True - Valida��o OK, False - Erro</returns>
        /// <history>
        ///     [cmarchi] created 18/1/2010
        /// </history>
        private bool Validar(HorarioColaboradores pobjHorariosColaboradores)
        {
            string[] arrCodigoColaboradores = pobjHorariosColaboradores.CodigosColaboradores.Split(',');

            if (arrCodigoColaboradores == null || arrCodigoColaboradores.Length == 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(pobjHorariosColaboradores.HorarioColaborador))
            {
                return false;
            }

            if (this.LinhaGrid < 0)
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
        /// <returns>True - Valida��o OK, False - Erro</returns>
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
        ///     [cmarchi] created 18/1/2010
        /// </history>
        private void VerificarQuery(string pstrQuery)
        {
            if (!string.IsNullOrEmpty(pstrQuery))
            {
                string[] arrParametros = BLEncriptacao.DecQueryStr(pstrQuery).Split('#');

                if (arrParametros != null && arrParametros.Length == 4)
                {
                    //verifica se est� editando uma escala
                    if (arrParametros[0] == "Sim")
                    {
                        this.gobjHorarioColaboradores = new HorarioColaboradores();

                        int intPosicaoLinha;                     

                        //verifica a linha grid
                        if (Int32.TryParse(arrParametros[1], out intPosicaoLinha))
                        {
                            this.LinhaGrid = intPosicaoLinha;
                        }

                        
                        this.gobjHorarioColaboradores.HorarioColaborador   = arrParametros[2];

                        if (Session["CodigosColaboradores"] != null)
                        {
                            arrParametros[3] = (string)Session["CodigosColaboradores"];
                            Session["CodigosColaboradores"] = null;
                            Session.Remove("CodigosColaboradores");
                        }
                        
                        this.gobjHorarioColaboradores.CodigosColaboradores = arrParametros[3];
                        

                        //faz a valida��o dos campos recebidos
                        if (this.Validar(this.gobjHorarioColaboradores))
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
