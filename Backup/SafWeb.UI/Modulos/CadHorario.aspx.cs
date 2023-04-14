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
using SafWeb.BusinessLayer.Email;
using FrameWork.BusinessLayer.Usuarios;
using System.Collections.Generic;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.Model.Idioma;
using SafWeb.BusinessLayer.Solicitacao;
using System.Collections.ObjectModel;
using SafWeb.Model.Solicitacao;
using FrameWork.BusinessLayer.Utilitarios;

namespace SafWeb.UI.Modulos
{
    public partial class CadHorarrio : FWPage
    {
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [no_history]
        /// [haguiar_5] modify 15/02/2011 15:55
        /// </history>

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblMensagemCad.Visible = false;
                this.PopularAprovadores();

                BLEmail objBlEmail;
                DataTable dttHorarios = new DataTable();
                DataTable dttHoraCol = new DataTable();

                int i = 0;

                try
                {
                    objBlEmail = new BLEmail();

                    dttHorarios = objBlEmail.ListarHorarios();
                    lstIdAlertas = new Dictionary<int, int>();

                    foreach (DataRow dtr in dttHorarios.Rows)
                    {
                        i++;

                        if (i == 1)
                        {
                            lblDesc1.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora1.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(1,Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox1.Visible = true;
                        }
                        if (i == 2)
                        {
                            lblDesc2.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora2.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(2,Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox2.Visible = true;
                        }
                        if (i == 3)
                        {
                            lblDesc3.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora3.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(3,Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox3.Visible = true;
                        } 
                        if (i == 4)
                        {
                            lblDesc4.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora4.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(4,Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox4.Visible = true;
                        }
                        if (i == 5)
                        {
                            lblDesc5.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora5.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(5,Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox5.Visible = true;
                        }
                        if (i == 6)
                        {
                            lblDesc6.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora6.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(6,Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox6.Visible = true;
                        }
                        if (i == 7)
                        {
                            lblDesc7.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora7.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(7, Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox7.Visible = true;
                        }
                        if (i == 8)
                        {
                            lblDesc8.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora8.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(8, Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox8.Visible = true;
                        }
                        if (i == 9)
                        {
                            lblDesc9.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora9.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(9, Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox9.Visible = true;
                        }
                        if (i == 10)
                        {
                            lblDesc10.Text = dtr["Des_HorarioAlerta"].ToString();
                            if (dtr["Hor_Alerta"].ToString() != string.Empty)
                                lblHora10.Text = dtr["Hor_Alerta"].ToString() + ":00";
                            lstIdAlertas.Add(10,Convert.ToInt32(dtr["Id_HorarioAlerta"]));
                            CheckBox10.Visible = true;
                        }
                    }

                    dttHoraCol = objBlEmail.ObterHorarios(Convert.ToInt32(BLAcesso.IdUsuarioLogado()));

                    foreach (DataRow dtr in dttHoraCol.Rows)
                    {
                        if (lstIdAlertas[1] == Convert.ToInt32(dtr["Id_Alerta"]))
                            CheckBox1.Checked = true;
                        else if (lstIdAlertas[2] == Convert.ToInt32(dtr["Id_Alerta"]))
                            CheckBox2.Checked = true;
                        else if (lstIdAlertas[3] == Convert.ToInt32(dtr["Id_Alerta"]))
                            CheckBox3.Checked = true;
                        else if (lstIdAlertas[4] == Convert.ToInt32(dtr["Id_Alerta"]))
                            CheckBox4.Checked = true;
                        else if (lstIdAlertas[5] == Convert.ToInt32(dtr["Id_Alerta"]))
                            CheckBox5.Checked = true;
                        else if (lstIdAlertas[6] == Convert.ToInt32(dtr["Id_Alerta"]))
                            CheckBox6.Checked = true;
                        else if (lstIdAlertas[7] == Convert.ToInt32(dtr["Id_Alerta"]))
                            CheckBox7.Checked = true;
                        else if (lstIdAlertas[8] == Convert.ToInt32(dtr["Id_Alerta"]))
                            CheckBox8.Checked = true;
                    }

                    if (dttHoraCol.Rows.Count > 0)
                    {
                        BLUtilitarios.ConsultarValorCombo(ref ddlAprovadores, dttHoraCol.Rows[0]["Id_AprovaSegNivel"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }
            }
        }

        #region Property

        /// <summary>
        /// Armazena o checkbox e o id do alerta
        /// </summary>
        /// <history>
        /// [mribeiro] created 25/10/2009
        /// </history>
        public Dictionary<int, int> lstIdAlertas
        {
            get
            {
                if (ViewState["vsIdAlerta"] == null)
                {
                    ViewState["vsIdAlerta"] = null;
                }
                return (Dictionary<int, int>)(ViewState["vsCodCracha"]);
            }
            set
            {
                ViewState["vsCodCracha"] = value;
            }
        }

        #endregion

        #region Botão

        /// <summary>
        /// Exlclui os registros do usuário e adiciona novamente conforme seleção na tela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [mribeiro] cretaed 25/10/2009
        /// [haguiar_5] modify 15/02/2011 15:55
        /// </history>
        protected void btnSalvarCadastro_Click(object sender, EventArgs e)
        {
            BLEmail objBlEmail;
            BLSolicitacao objBlSolicitacao;
            int intUsuario;

            try
            {
                objBlEmail = new BLEmail();

                intUsuario = Convert.ToInt32(BLAcesso.IdUsuarioLogado());

                objBlEmail.Excluir(intUsuario);

                if(CheckBox1.Checked)
                    objBlEmail.Inserir(intUsuario,lstIdAlertas[1]);
                if (CheckBox2.Checked)
                    objBlEmail.Inserir(intUsuario, lstIdAlertas[2]);
                if (CheckBox3.Checked)
                    objBlEmail.Inserir(intUsuario, lstIdAlertas[3]);
                if (CheckBox4.Checked)
                    objBlEmail.Inserir(intUsuario, lstIdAlertas[4]);
                if (CheckBox5.Checked)
                    objBlEmail.Inserir(intUsuario, lstIdAlertas[5]);
                if (CheckBox6.Checked)
                    objBlEmail.Inserir(intUsuario, lstIdAlertas[6]);
                if (CheckBox7.Checked)
                    objBlEmail.Inserir(intUsuario, lstIdAlertas[7]);

                if (CheckBox8.Checked)
                    objBlEmail.Inserir(intUsuario, lstIdAlertas[8]);

                if(ddlAprovadores.SelectedIndex > 0)
                {
                    objBlSolicitacao = new BLSolicitacao();

                    objBlSolicitacao.InserirAprovadorSegNiveil(Convert.ToInt32(BLAcesso.IdUsuarioLogado()),
                                                               Convert.ToInt32(ddlAprovadores.SelectedValue));
                }

                lblMensagemCad.Visible = true;
                lblMensagemCad.Text = BLIdiomas.TraduzirMensagens(Convert.ToInt32(Mensagens.REGISTRO_GRAVAR));

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion

        #region Popular Combo

        protected void PopularAprovadores()
        {

            BLSolicitacao objBlSolicitacao = new BLSolicitacao();
            Collection<Aprovador> colAprovador = new Collection<Aprovador>();

            try
            {
                colAprovador = objBlSolicitacao.ListarTodosAprovadores();

                ddlAprovadores.DataSource = colAprovador;
                ddlAprovadores.DataTextField = "NomeUsuario";
                ddlAprovadores.DataValueField = "IdUsuario";
                ddlAprovadores.DataBind();

                BLUtilitarios.InseriMensagemDropDownList(ref ddlAprovadores, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }

        #endregion
    }
}
