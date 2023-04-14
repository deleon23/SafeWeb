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
using Telerik.Web.UI;
using System.Text;

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class ConfirmaVariosSaida : FWPage
    {
        BLAcessoColaborador gobjBLAcessoColaborador = new BLAcessoColaborador();
        List<AcessoColaborador> gobjAcessoColaborador;

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
        ///     [tgerevini] 07/04/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        private void InicializaScripts(bool bRecarregaGrid)
        {
            gobjAcessoColaborador = new List<AcessoColaborador>();

            //Nome do Colaborador
            string sNomeColaborador;
            string sIdFilial;
            string sIdColaborador;
            string sIdEscalacao;
            string sDtEscalacao;
            string sHoraEntrada;
            string sCodigoColaborador;
            string sDescricaoEscalaDepto;

            string[] lstNomeColaborador;
            string[] lstIdColaborador;
            string[] lstIdEscalacao;
            string[] lstDtEscalacao;
            string[] lstHoraEntrada;
            string[] lstCodigoColaborador;
            string[] lstDescricaoEscalaDepto;

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
                sNomeColaborador = Server.UrlDecode(ViewState["NomeColaborador"].ToString());
                lstNomeColaborador = sNomeColaborador.Split(',');

                sIdFilial = Server.UrlDecode(ViewState["IdFilial"].ToString());

                sIdColaborador = Server.UrlDecode(ViewState["IdColaborador"].ToString());
                lstIdColaborador = sIdColaborador.Split(',');

                sIdEscalacao = Server.UrlDecode(ViewState["IdEscalacao"].ToString());
                lstIdEscalacao = sIdEscalacao.Split(',');

                sDtEscalacao = Server.UrlDecode(ViewState["DtEscalacao"].ToString());
                lstDtEscalacao = sDtEscalacao.Split(',');

                sHoraEntrada = Server.UrlDecode(ViewState["HoraEntrada"].ToString());
                lstHoraEntrada = sHoraEntrada.Split(',');

                sCodigoColaborador = Server.UrlDecode(ViewState["CodigoColaborador"].ToString());
                lstCodigoColaborador = sCodigoColaborador.Split(',');

                sDescricaoEscalaDepto = Server.UrlDecode(ViewState["DescricaoEscalaDepto"].ToString());
                lstDescricaoEscalaDepto = sDescricaoEscalaDepto.Split(',');

                DataTable dtFuncionarios = new DataTable();
                DataColumn dcFuncionario;

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "CodColaborador";
                dtFuncionarios.Columns.Add(dcFuncionario);

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "HoraEntrada";
                dtFuncionarios.Columns.Add(dcFuncionario);
            
                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "NomeColaborador";
                dtFuncionarios.Columns.Add(dcFuncionario);

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "RE";
                dtFuncionarios.Columns.Add(dcFuncionario);

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "NumeroEscalado";
                dtFuncionarios.Columns.Add(dcFuncionario);

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "EscalaDepartamental";
                dtFuncionarios.Columns.Add(dcFuncionario);

                DataRow linha;
                for (int i = 0; i < lstIdColaborador.Length; i++)
                {
                    string s = hdProcessado.Value.Replace("<" + lstIdColaborador[i].ToString() + ">", "");

                    if (hdProcessado.Value == s) // Se forem iguais, lstIdColaborador[i] não está no hdProcessado
                    {
                        linha = dtFuncionarios.NewRow();

                        linha["CodColaborador"] = lstIdColaborador[i].ToString();
                        linha["HoraEntrada"] = lstHoraEntrada[i].ToString();
                        linha["NomeColaborador"] = lstNomeColaborador[i].ToString();
                        linha["RE"] = lstCodigoColaborador[i].ToString();
                        linha["NumeroEscalado"] = lstIdEscalacao[i].ToString();
                        linha["EscalaDepartamental"] = lstDescricaoEscalaDepto[i].ToString();

                        AcessoColaborador objAcessoColaborador = new AcessoColaborador();
                        objAcessoColaborador.CodFilial = Convert.ToInt32(sIdFilial);
                        objAcessoColaborador.CodColaborador = Convert.ToInt32(lstIdColaborador[i]);
                        objAcessoColaborador.CodEscalacao = Convert.ToInt32(lstIdEscalacao[i]);
                        objAcessoColaborador.DataEscalacao = Convert.ToDateTime(lstDtEscalacao[i]);

                        //obtem dados da filial / fuso horario e verifica se está atrasado
                        gobjFilial = gobjBLFilial.Obter(objAcessoColaborador.CodFilial);
                        DateTime datAtual = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;

                        DateTime dtHoraEntrada = datAtual.Date;
                        dtHoraEntrada = dtHoraEntrada.AddHours(Convert.ToDateTime(lstHoraEntrada[i].ToString()).Hour);
                        dtHoraEntrada = dtHoraEntrada.AddMinutes(Convert.ToDateTime(lstHoraEntrada[i].ToString()).Minute);

                        gobjAcessoColaborador.Add(objAcessoColaborador);
                        dtFuncionarios.Rows.Add(linha);
                    }
                }

                this.gridFuncionarios.DataSource = dtFuncionarios;
                if (dtFuncionarios.Rows.Count == 0)
                {
                    btnConfirmar.Enabled = false;
                }
            }

            if (bRecarregaGrid)
            {
                this.gridFuncionarios.DataBind();
            }

        }
        #endregion

        /// <history>
        ///     [no history]
        ///     [haguiar_4] 18/01/2011
        ///     adicionar fuso horário ao campo hora
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

                InicializaScripts(true);

                //Hora atual
                DateTime datEscala = new DateTime();

                if (gobjAcessoColaborador.Count > 0)
                {
                    //obtem dados da filial / fuso horario
                    gobjFilial = gobjBLFilial.Obter(gobjAcessoColaborador[0].CodFilial);

                    datEscala = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;
                }
               
                CultureInfo ci = CultureInfo.InvariantCulture;

                lblHora.Text = datEscala.ToString("HH:mm", ci);
                hdHora.Value = datEscala.ToString("HH:mm", ci);
            }
            else
            {
                InicializaScripts(false);               
            }
        }

        #region Botão

        /// <history>
        ///     [no history]
        ///     [haguiar_4] 18/01/2011
        ///     calcular entrada com fuso horário
        /// </history>

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < gridFuncionarios.Items.Count; i++)
            {
                string CodColaborador = gridFuncionarios.Items[i].Cells[1].Text;


                //obtem dados da filial / fuso horario
                gobjFilial = gobjBLFilial.Obter(gobjAcessoColaborador[i].CodFilial);

                DateTime datAtual = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;
                DateTime datHorarioEntradaSaida = Convert.ToDateTime(datAtual.ToShortDateString() + " " + hdHora.Value + ":00");

                try
                {
                    gobjUsuario = BLAcesso.ObterUsuario();

                    gobjAcessoColaborador[i].CodUsuarioLiberaEntrada = Convert.ToInt32(gobjUsuario.Codigo);
                    gobjAcessoColaborador[i].DataSaida = datHorarioEntradaSaida;

                    //Insere Escalação do Colaborador                           
                    if (gobjBLAcessoColaborador.Alterar(gobjAcessoColaborador[i]))
                    {

                        //Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('Entrada efetuada com sucesso');</script>");

                    }
                    else
                    {
                        lblMensagem.Text = "Erro ao efetuar cadastro!";
                    }

                    hdProcessado.Value += "<" + gobjAcessoColaborador[i].CodColaborador + ">";
                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }                
            }

            if (lblMensagem.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('Saída efetuada com sucesso');</script>");

                //Fecha RadWindow e volta para tela anterior
                Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "fechar", "<script>CloseWin()</script>");
            }

        }
        
        #endregion
    }
}
