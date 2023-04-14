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

namespace SafWeb.UI.Modulos.Portaria
{
    public partial class ConfirmaVariosEntrada : FWPage
    {
        BLAcessoColaborador gobjBLAcessoColaborador = new BLAcessoColaborador();
        List<AcessoColaborador> gobjAcessoColaborador;
        List<AcessoColaborador> gobjAcessoColaboradorAtrasado;

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
        private void InicializaScripts(bool bRecarregaGrid, bool bDataBind)
        {
            gobjAcessoColaborador = new List<AcessoColaborador>();
            gobjAcessoColaboradorAtrasado = new List<AcessoColaborador>();

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
                DataTable dtFuncionariosAtrasados = new DataTable();
                DataColumn dcFuncionario;
                DataColumn dcFuncionariosAtrasados;

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "CodColaborador";
                dtFuncionarios.Columns.Add(dcFuncionario);
                dcFuncionariosAtrasados = new DataColumn();
                dcFuncionariosAtrasados.DataType = Type.GetType("System.String");
                dcFuncionariosAtrasados.ColumnName = "CodColaborador";
                dtFuncionariosAtrasados.Columns.Add(dcFuncionariosAtrasados);

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "HoraEntrada";
                dtFuncionarios.Columns.Add(dcFuncionario);
                dcFuncionariosAtrasados = new DataColumn();
                dcFuncionariosAtrasados.DataType = Type.GetType("System.String");
                dcFuncionariosAtrasados.ColumnName = "HoraEntrada";
                dtFuncionariosAtrasados.Columns.Add(dcFuncionariosAtrasados);

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "NomeColaborador";
                dtFuncionarios.Columns.Add(dcFuncionario);
                dcFuncionariosAtrasados = new DataColumn();
                dcFuncionariosAtrasados.DataType = Type.GetType("System.String");
                dcFuncionariosAtrasados.ColumnName = "NomeColaborador";
                dtFuncionariosAtrasados.Columns.Add(dcFuncionariosAtrasados);

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "RE";
                dtFuncionarios.Columns.Add(dcFuncionario);
                dcFuncionariosAtrasados = new DataColumn();
                dcFuncionariosAtrasados.DataType = Type.GetType("System.String");
                dcFuncionariosAtrasados.ColumnName = "RE";
                dtFuncionariosAtrasados.Columns.Add(dcFuncionariosAtrasados);

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "NumeroEscalado";
                dtFuncionarios.Columns.Add(dcFuncionario);
                dcFuncionariosAtrasados = new DataColumn();
                dcFuncionariosAtrasados.DataType = Type.GetType("System.String");
                dcFuncionariosAtrasados.ColumnName = "NumeroEscalado";
                dtFuncionariosAtrasados.Columns.Add(dcFuncionariosAtrasados);

                dcFuncionario = new DataColumn();
                dcFuncionario.DataType = Type.GetType("System.String");
                dcFuncionario.ColumnName = "EscalaDepartamental";
                dtFuncionarios.Columns.Add(dcFuncionario);
                dcFuncionariosAtrasados = new DataColumn();
                dcFuncionariosAtrasados.DataType = Type.GetType("System.String");
                dcFuncionariosAtrasados.ColumnName = "EscalaDepartamental";
                dtFuncionariosAtrasados.Columns.Add(dcFuncionariosAtrasados);

                DataRow linha;
                DataRow linhaAtrasado;
                for (int i = 0; i < lstIdColaborador.Length; i++)
                {
                    string s = hdProcessado.Value.Replace("<" + lstIdColaborador[i].ToString() + ">", "");

                    if (hdProcessado.Value == s) // Se forem iguais, lstIdColaborador[i] não está no hdProcessado
                    {
                        linha = dtFuncionarios.NewRow();
                        linhaAtrasado = dtFuncionariosAtrasados.NewRow();

                        linha["CodColaborador"] = lstIdColaborador[i].ToString();
                        linha["HoraEntrada"] = lstHoraEntrada[i].ToString();
                        linha["NomeColaborador"] = lstNomeColaborador[i].ToString();
                        linha["RE"] = lstCodigoColaborador[i].ToString();
                        linha["NumeroEscalado"] = lstIdEscalacao[i].ToString();
                        linha["EscalaDepartamental"] = lstDescricaoEscalaDepto[i].ToString();

                        linhaAtrasado["CodColaborador"] = lstIdColaborador[i].ToString();
                        linhaAtrasado["HoraEntrada"] = lstHoraEntrada[i].ToString();
                        linhaAtrasado["NomeColaborador"] = lstNomeColaborador[i].ToString();
                        linhaAtrasado["RE"] = lstCodigoColaborador[i].ToString();
                        linhaAtrasado["NumeroEscalado"] = lstIdEscalacao[i].ToString();
                        linhaAtrasado["EscalaDepartamental"] = lstDescricaoEscalaDepto[i].ToString();

                        AcessoColaborador objAcessoColaborador = new AcessoColaborador();
                        objAcessoColaborador.CodFilial = Convert.ToInt32(sIdFilial);
                        objAcessoColaborador.CodColaborador = Convert.ToInt32(lstIdColaborador[i]);
                        objAcessoColaborador.CodEscalacao = Convert.ToInt32(lstIdEscalacao[i]);
                        objAcessoColaborador.DataEscalacao = Convert.ToDateTime(lstDtEscalacao[i]);

                        //obtem dados da filial / fuso horario e verifica se está atrasado
                        gobjFilial = gobjBLFilial.Obter(objAcessoColaborador.CodFilial);
                        DateTime datAtual = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;

                        bool Atrasado = VerificaAtrasado(datAtual, Convert.ToDateTime(lstHoraEntrada[i].ToString()).Hour, Convert.ToDateTime(lstHoraEntrada[i].ToString()).Minute);

                        if (!Atrasado)
                        {

                            gobjAcessoColaborador.Add(objAcessoColaborador);
                            dtFuncionarios.Rows.Add(linha);
                        }
                        else
                        {
                            gobjAcessoColaboradorAtrasado.Add(objAcessoColaborador);
                            dtFuncionariosAtrasados.Rows.Add(linhaAtrasado);                            
                        }
                    }
                }

                if (bRecarregaGrid)
                {
                    this.gridFuncionarios.DataSource = dtFuncionarios;
                    
                    if(bDataBind)
                        this.gridFuncionarios.DataBind();

                    if (dtFuncionarios.Rows.Count == 0)
                    {
                        btnConfirmar.Enabled = false;
                    }

                    this.gridFuncionariosAtrasados.DataSource = dtFuncionariosAtrasados;
                    
                    if (bDataBind)
                        this.gridFuncionariosAtrasados.DataBind();

                    if (dtFuncionariosAtrasados.Rows.Count == 0)
                    {
                        btnConfirmarAtrasados.Enabled = false;
                    }

                    if ((dtFuncionarios.Rows.Count == 0) && (dtFuncionariosAtrasados.Rows.Count == 0))
                    {
                        hdSaindo.Value = "false"; // Preenche a Hidden pois, se o usuário entrar nessa tela novamente, isso evita que recarregue a tela de fundo.
                        Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('Entrada efetuada com sucesso');</script>");

                        //Fecha RadWindow e volta para tela anterior
                        Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "fechar", "<script>CloseWin()</script>");
                    }
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
        ///     [no history]
        ///     [haguiar_4] 18/01/2011
        ///     adicionar fuso horário ao campo hora
        ///     [aoliveira] 18/01/2011 
        ///     limpa sessões e coloca os valores na view state
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

                InicializaScripts(true, true);

                //Hora atual
                DateTime datEscala = new DateTime();

                if (gobjAcessoColaborador.Count > 0)
                {
                    //obtem dados da filial / fuso horario
                    gobjFilial = gobjBLFilial.Obter(gobjAcessoColaborador[0].CodFilial);

                    datEscala = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;
                }
                else
                {
                    if (gobjAcessoColaboradorAtrasado.Count > 0)
                    {
                        //obtem dados da filial / fuso horario
                        gobjFilial = gobjBLFilial.Obter(gobjAcessoColaboradorAtrasado[0].CodFilial);

                        datEscala = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;
                    }
                }

                CultureInfo ci = CultureInfo.InvariantCulture;

                lblHora.Text = datEscala.ToString("HH:mm", ci);
                hdHora.Value = datEscala.ToString("HH:mm", ci);
            }
            else
            {
                InicializaScripts(false, false);               
            }
            hdSaindo.Value = "";
        }

        #region Botão

        /// <history>
        ///     [no history]
        ///     [haguiar_4] 18/01/2011
        ///     calcular entrada com fuso horário
        /// </history>

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            lblMensagemAtrasado.Text = "";

            for (int i = 0; i < gridFuncionarios.Items.Count; i++)
            {
                string CodColaborador = gridFuncionarios.Items[i].Cells[1].Text;
                string HoraEntrada = gridFuncionarios.Items[i].Cells[3].Text;
                                
                //obtem dados da filial / fuso horario
                gobjFilial = gobjBLFilial.Obter(gobjAcessoColaborador[i].CodFilial);
                
                DateTime datAtual = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;
                try
                {
                    int hora = Convert.ToInt32(HoraEntrada[0].ToString() + HoraEntrada[1].ToString());
                    int minuto = Convert.ToInt32(HoraEntrada[3].ToString() + HoraEntrada[4].ToString());

                    if (!AdiantadoForaTolerancia(datAtual, hora, minuto))
                    {
                        DateTime datHorarioEntradaSaida = Convert.ToDateTime(datAtual.ToShortDateString() + " " + hdHora.Value + ":00");

                        gobjUsuario = BLAcesso.ObterUsuario();

                        gobjAcessoColaborador[i].DataEntrada = datHorarioEntradaSaida;
                        gobjAcessoColaborador[i].CodUsuarioLiberaEntrada = Convert.ToInt32(gobjUsuario.Codigo);

                        //Insere Escalação do Colaborador                           
                        if (gobjBLAcessoColaborador.Inserir(gobjAcessoColaborador[i]) > 0)
                        {

                            //Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('Entrada efetuada com sucesso');</script>");

                        }
                        else
                        {
                            lblMensagem.Text = "Erro ao efetuar cadastro!";
                        }

                        hdProcessado.Value += "<" + gobjAcessoColaborador[i].CodColaborador + ">";
                    }
                    else
                    {
                        lblMensagem.Text = "Existem colaboradores adiantados que não foram registradas as entradas";
                    }

                }
                catch (Exception ex)
                {
                    ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                }                
            }
            InicializaScripts(true, true);
        }

        protected void btnConfirmarAtrasados_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            lblMensagemAtrasado.Text = "";

            bool existemSelecionados = false;
            for (int i = 0; i < gridFuncionariosAtrasados.Items.Count; i++)
            {
                //string CodColaborador = gridFuncionariosAtrasados.Rows[i].Cells[1].Text;
                CheckBox c = (CheckBox)gridFuncionariosAtrasados.Items[i]["CheckAcesso"].FindControl("checkClientes");
                if (c.Checked)
                {
                    existemSelecionados = true;
                    //obtem dados da filial / fuso horario
                    gobjFilial = gobjBLFilial.Obter(gobjAcessoColaboradorAtrasado[i].CodFilial);

                    DateTime datAtual = DateTime.UtcNow.AddHours(gobjFilial.Vlr_FusoHorario); //DateTime.Now;
                    DateTime datHorarioEntradaSaida = Convert.ToDateTime(datAtual.ToShortDateString() + " " + hdHora.Value + ":00");
                    
                    try
                    {

                        gobjUsuario = BLAcesso.ObterUsuario();

                        gobjAcessoColaboradorAtrasado[i].DataEntrada = datHorarioEntradaSaida;
                        gobjAcessoColaboradorAtrasado[i].CodUsuarioLiberaEntrada = Convert.ToInt32(gobjUsuario.Codigo);

                        //Insere Escalação do Colaborador
                        if (gobjBLAcessoColaborador.Inserir(gobjAcessoColaboradorAtrasado[i]) > 0)
                        {

                            //Page.ClientScript.RegisterStartupScript(String.Empty.GetType(), "Colaboradores", "<script>window.parent.PreencherHiddenColaboradores('Entrada efetuada com sucesso');</script>");

                        }
                        else
                        {
                            lblMensagemAtrasado.Text = "Erro ao efetuar cadastro!";
                        }

                        hdProcessado.Value += "<" + gobjAcessoColaboradorAtrasado[i].CodColaborador + ">";
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
                    }
                }
            }
            if (!existemSelecionados)
            {
                lblMensagemAtrasado.Text = "Selecione os Colaboradores";
            }
            InicializaScripts(true, true);
        }
        #endregion

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método NeedDataSource do radgrid
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 07/04/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void gridFuncionarios_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            InicializaScripts(true, false);
        }

        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Método NeedDataSource do radgrid
        /// </summary> 
        /// <history> 
        ///     [tgerevini] 07/04/2010 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        protected void gridFuncionariosAtrasados_OnNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            InicializaScripts(true, false);
        }

        protected void gridFuncionariosAtrasados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem item = (GridHeaderItem)e.Item;

                item.Attributes.Add("onclick", "CancelaAtualizacaoPai();");
                //LinkButton c = (LinkButton)item.FindControl("NomeColaborador");
            }
        }

        protected void gridFuncionarios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem item = (GridHeaderItem)e.Item;

                item.Attributes.Add("onclick", "CancelaAtualizacaoPai();");
                //LinkButton c = (LinkButton)item.FindControl("NomeColaborador");
            }
        }
    }
}
