using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using FrameWork.BusinessLayer.Idioma;
using FrameWork.BusinessLayer.Utilitarios;
using FrameWork.Model.Idioma;
using SafWeb.BusinessLayer.Colaborador;
using SafWeb.BusinessLayer.Filial;
using SafWeb.BusinessLayer.Permissao;
using SafWeb.BusinessLayer.Regional;
using SafWeb.Model.Permissao;
using SafWeb.Model.Util;
using SafWeb.Util.Extension;
using SafWeb.Model.Colaborador;
using System.Text.RegularExpressions;

namespace SafWeb.UI.Admin.Permissao
{
    public partial class CadLimitePrmColaborador : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                CarregaDdlLimite();
                PopularRegional();

                if (Request["idLimitePrmColaborador"] != null)
                {

                    idLimitePrmColaborador.Value = Request["idLimitePrmColaborador"];

                    var blLimitePrmColaborador = new BLLimitePrmColaborador();
                    var LimitePrmColaborador = blLimitePrmColaborador.BuscaPorIdLimitePrmColaborador(Request["idLimitePrmColaborador"].ToInt32());

                    ddlLimite.SelectedValue = LimitePrmColaborador.idLimite.ToString();

                    BLColaborador objBlColaborador = new BLColaborador();
                    IList<FWCUsuario> iColaborador = new List<FWCUsuario>();
                    iColaborador = objBlColaborador.BuscaPorIdColoborador(LimitePrmColaborador.idColaborador);
                    ddlColaborador.DataSource = iColaborador;
                    ddlColaborador.DataTextField = "UsuCNome";
                    ddlColaborador.DataValueField = "UsuNCodigo";
                    ddlColaborador.DataBind();

                    ddlFilial.Enabled = false;
                    ddlRegional.Enabled = false;
                    dvRegional.Visible = false;
                    dvFilial.Visible = false;
                }
            }

        }

        //Popula a ddl de Regional
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

                BLUtilitarios.InseriMensagemDropDownList(ref ddlRegional, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);
            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }


        //Carrega a ddl de Limite
        private void CarregaDdlLimite()
        {
            try
            {
                BLLimitePermissao objLimitePermissao = new BLLimitePermissao();
                IList<LimitePermissao> iListPermissao = new List<LimitePermissao>();

                iListPermissao = objLimitePermissao.BuscaTodosLimitePermissao();

                foreach (var item in iListPermissao)
                {
                    item.Limite = item.Limite / 100;
                }

                ddlLimite.DataSource = iListPermissao;


                ddlLimite.DataValueField = "idLimite";
                ddlLimite.DataTextField = "Limite";
                ddlLimite.DataTextFormatString = "{0:##0.00%}";
                ddlLimite.DataBind();
                BLUtilitarios.InseriMensagemDropDownList(ref ddlLimite, BLIdiomas.TraduzirMensagens(Mensagens.SELECIONE), false, 0);

            }
            catch
            {

            }

        }
    }
}