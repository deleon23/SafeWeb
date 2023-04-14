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

using FrameWork.BusinessLayer.Usuarios;
using FrameWork.Model.Usuarios;

using SafWeb.BusinessLayer.Acesso;
using SafWeb.BusinessLayer.Filial;
using SafWeb.Model.Acesso;
using SafWeb.Model.Filial;
using System.Globalization;
using System.Collections.Generic;
using Telerik.WebControls;
using SafWeb.BusinessLayer.Colaborador;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class ConfirmaEntrada : FWPage
    {
        BLAcessoColaborador gobjBLAcessoColaborador = new BLAcessoColaborador();
        AcessoColaborador gobjAcessoColaborador;

        BLFilial gobjBLFilial = new BLFilial();
        Filial gobjFilial = new Filial();

        Usuario gobjUsuario = new Usuario();
        BLAcesso gobjBLAcesso = new BLAcesso();

        #region InicializaScripts
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método InicializaScripts
        /// </summary> 
        /// <history> 
        ///     [aoliveira] 04/01/2013 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void InicializaScripts()
        {
            gobjAcessoColaborador = new AcessoColaborador();

            //Nome do Colaborador
            //string sIdFilial;
            string sIdColaborador;
            //string sDtEscalacao;

            if (ViewState["NomeColaborador"] != null
               &&
               ViewState["IdFilial"] != null
               &&
               ViewState["IdColaborador"] != null
               &&
               ViewState["IdEscalacao"] != null
               &&
               ViewState["DtEscalacao"] != null
               &&
               ViewState["HoraEntrada"] != null
               &&
               ViewState["CodigoColaborador"] != null
               &&
               ViewState["DescricaoEscalaDepto"] != null
               )
            {
                lbl_NomeColaborador.Text = Server.UrlDecode(ViewState["NomeColaborador"].ToString());
                //sIdFilial = Server.UrlDecode(ViewState["IdFilial"].ToString());
                sIdColaborador = Server.UrlDecode(ViewState["IdColaborador"].ToString());
                lbl_NumEscala.Text = Server.UrlDecode(ViewState["IdEscalacao"].ToString());
                //sDtEscalacao = Server.UrlDecode(ViewState["DtEscalacao"].ToString());
                lbl_HoraEntrada.Text = Server.UrlDecode(ViewState["HoraEntrada"].ToString());
                lbl_RE.Text = Server.UrlDecode(ViewState["CodigoColaborador"].ToString());
                lbl_EscalaDpto.Text = Server.UrlDecode(ViewState["DescricaoEscalaDepto"].ToString());

                BLColaborador objBlColaborador = new BLColaborador();
                Model.Colaborador.Colaborador gobjColaborador = objBlColaborador.Obter(Convert.ToInt32(sIdColaborador));
                lbl_Cargo.Text = gobjColaborador.Des_Funcao;

                gobjAcessoColaborador.CodFilial = Convert.ToInt32(Server.UrlDecode(ViewState["IdFilial"].ToString()));
                gobjAcessoColaborador.CodColaborador = Convert.ToInt32(sIdColaborador);
                gobjAcessoColaborador.CodEscalacao = Convert.ToInt32(Server.UrlDecode(ViewState["IdEscalacao"].ToString()));
                gobjAcessoColaborador.DataEscalacao = Convert.ToDateTime(Server.UrlDecode(ViewState["DtEscalacao"].ToString()));

                imgFoto.ImageUrl = "ImageFoto.aspx?ImageByte=" + sIdColaborador;
                imgFoto.Visible = true;

                //obtem dados da filial / fuso horario e verifica se está atrasado
                gobjFilial = gobjBLFilial.Obter(gobjAcessoColaborador.CodFilial);
                DateTime datAtual = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;

                bool Atrasado = VerificaAtrasado(datAtual, Convert.ToDateTime(lbl_HoraEntrada.Text.ToString()).Hour, Convert.ToDateTime(lbl_HoraEntrada.Text.ToString()).Minute);

                if (Atrasado)
                {
                    lbl_HoraEntrada.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private bool VerificaAtrasado(DateTime datAtual, int hora, int minuto)
        {
            DateTime dtHoraEntrada = datAtual.Date;
            dtHoraEntrada = dtHoraEntrada.AddHours(hora);
            dtHoraEntrada = dtHoraEntrada.AddMinutes(minuto);

            int diferencaHoras = 12 - datAtual.Hour;
            int diferencaMinutos = 0;
            if(datAtual.Minute > 0)
                diferencaMinutos = datAtual.Minute * -1;

            // Leva a hora atual para 12h00(meio dia)
            datAtual = datAtual.AddHours(diferencaHoras);
            datAtual = datAtual.AddMinutes(diferencaMinutos);

            // Leva a dtHoraEntrada junto com a hora atual, mantendo a diferença entre elas
            // Leva a hora atual para 12h00(meio dia)
            DateTime dtHoraEntradaAux = dtHoraEntrada;
            dtHoraEntradaAux = dtHoraEntradaAux.AddHours(diferencaHoras);
            dtHoraEntradaAux = dtHoraEntradaAux.AddMinutes(diferencaMinutos);
            dtHoraEntrada = dtHoraEntrada.AddHours(dtHoraEntrada.Hour * -1);
            dtHoraEntrada = dtHoraEntrada.AddMinutes(dtHoraEntrada.Minute * -1);
            dtHoraEntrada = dtHoraEntrada.AddHours(dtHoraEntradaAux.Hour);
            dtHoraEntrada = dtHoraEntrada.AddMinutes(dtHoraEntradaAux.Minute);

            int result = DateTime.Compare(datAtual, dtHoraEntrada);
            
            if (result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool AdiantadoForaTolerancia(DateTime datAtual, int hora, int minuto)
        {
            DateTime dtHoraEntrada = datAtual.Date;
            dtHoraEntrada = dtHoraEntrada.AddHours(hora);
            dtHoraEntrada = dtHoraEntrada.AddMinutes(minuto);

            int diferencaHoras = 12 - datAtual.Hour;
            int diferencaMinutos = 0;
            if (datAtual.Minute > 0)
                diferencaMinutos = datAtual.Minute * -1;

            // Leva a hora atual para 12h00(meio dia)
            datAtual = datAtual.AddHours(diferencaHoras);
            datAtual = datAtual.AddMinutes(diferencaMinutos);

            // Leva a dtHoraEntrada junto com a hora atual, mantendo a diferença entre elas
            // Leva a hora atual para 12h00(meio dia)
            DateTime dtHoraEntradaAux = dtHoraEntrada;
            dtHoraEntradaAux = dtHoraEntradaAux.AddHours(diferencaHoras);
            dtHoraEntradaAux = dtHoraEntradaAux.AddMinutes(diferencaMinutos);
            dtHoraEntrada = dtHoraEntrada.AddHours(dtHoraEntrada.Hour * -1);
            dtHoraEntrada = dtHoraEntrada.AddMinutes(dtHoraEntrada.Minute * -1);
            dtHoraEntrada = dtHoraEntrada.AddHours(dtHoraEntradaAux.Hour);
            dtHoraEntrada = dtHoraEntrada.AddMinutes(dtHoraEntradaAux.Minute);

            int result = DateTime.Compare(datAtual, dtHoraEntrada);

            if (result > 0)
            {
                return false;
            }
            else
            {
                datAtual = datAtual.AddMinutes(20);
                int result2 = DateTime.Compare(datAtual, dtHoraEntrada);
                
                if (result2 >= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion

        /// <history>
        ///     [aoliveira] 18/01/2011 Created
        ///     adicionar fuso horário ao campo hora, limpa sessões e coloca os valores na view state
        /// </history>

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Page.IsPostBack))
            {
                ViewState["NomeColaborador"] = Session["NomeColaborador"];
                ViewState["IdFilial"] = Session["IdFilial"];
                ViewState["IdColaborador"] = Session["IdColaborador"];
                ViewState["IdEscalacao"] = Session["IdEscalacao"];
                ViewState["DtEscalacao"] = Session["DtEscalacao"];
                ViewState["HoraEntrada"] = Session["HoraEntrada"];
                ViewState["CodigoColaborador"] = Session["CodigoColaborador"];
                ViewState["DescricaoEscalaDepto"] = Session["DescricaoEscalaDepto"];

                Session.Remove("IdEscalacao");
                Session.Remove("IdColaborador");
                Session.Remove("DtEscalacao");
                Session.Remove("NomeColaborador");
                Session.Remove("IdFilial");
                Session.Remove("HoraEntrada");
                Session.Remove("CodigoColaborador");
                Session.Remove("DescricaoEscalaDepto");

                InicializaScripts();

                //Hora atual
                DateTime datEscala = new DateTime();

               //obtem dados da filial / fuso horario
                gobjFilial = gobjBLFilial.Obter(gobjAcessoColaborador.CodFilial);

                datEscala = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;
               
                CultureInfo ci = CultureInfo.InvariantCulture;

                lblHora.Text = datEscala.ToString("HH:mm", ci);
                hdHora.Value = datEscala.ToString("HH:mm", ci);
            }
            else
            {
                InicializaScripts();               
            }
        }

        #region Botão

        /// <history>
        ///     [aoliveira] 04/02/2013 Created
        ///     registra a entrada do colaborador
        /// </history>

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";

            string CodColaborador = lbl_RE.Text;
            string HoraEntrada = lbl_HoraEntrada.Text;
                                
            //obtem dados da filial / fuso horario
            gobjFilial = gobjBLFilial.Obter(Convert.ToInt32(Server.UrlDecode(ViewState["IdFilial"].ToString())));
                
            DateTime datAtual = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;
            try
            {
                int hora = Convert.ToInt32(HoraEntrada[0].ToString() + HoraEntrada[1].ToString());
                int minuto = Convert.ToInt32(HoraEntrada[3].ToString() + HoraEntrada[4].ToString());

                if (!AdiantadoForaTolerancia(datAtual, hora, minuto))
                {
                    DateTime datHorarioEntradaSaida = Convert.ToDateTime(datAtual.ToShortDateString() + " " + hdHora.Value + ":00");

                    gobjUsuario = BLAcesso.ObterUsuario();

                    gobjAcessoColaborador.DataEntrada = datHorarioEntradaSaida;
                    gobjAcessoColaborador.CodUsuarioLiberaEntrada = Convert.ToInt32(gobjUsuario.Codigo);

                    //Insere Escalação do Colaborador                           
                    if (gobjBLAcessoColaborador.Inserir(gobjAcessoColaborador) > 0)
                    {

                        //Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('Entrada efetuada com sucesso');</script>");

                    }
                    else
                    {
                        lblMensagem.Text = "Erro ao efetuar cadastro!";
                    }
                }
                else
                {
                    lblMensagem.Text = "O colaborador está adiantado demais para efetuar entrada.";
                }

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }

            if (lblMensagem.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('Entrada efetuada com sucesso');</script>");

                //Fecha RadWindow e volta para tela anterior
                Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "fechar", "<script>CloseWin()</script>");
            }
        }

        #endregion

    }
}
