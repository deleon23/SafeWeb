using System;
using System.Collections.Generic;
using SafWeb.BusinessLayer.Permissao;
using SafWeb.Util.Extension;
using SafWeb.Model.Permissao;




namespace SafWeb.UI.Modulos.HelpDesk
{
    public partial class CadPermissaoCrachaTitular : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                BLGrupoColetoresRonda objBLGrupoColetoresRonda = new BLGrupoColetoresRonda();
                List<GrupoColetoresRonda> ListColetores = new List<GrupoColetoresRonda>();
                ListColetores = objBLGrupoColetoresRonda.BuscaGrupoColetores(0);

               

                if (Request["idPermissao"] != null)
                {
                    idPermissao.Value = Request["idPermissao"];
                    if (idPermissao.Value.ToInt32() > 0)
                    {

                        BLPermissaoRonda objBLPermissaoRonda = new BLPermissaoRonda();
                        var ListPermissaoRonda = objBLPermissaoRonda.BuscaPermissaoRonda(idPermissao.Value.ToInt32());

                        if (ListPermissaoRonda.Count > 0)
                        {
                            txtNome.Text = ListPermissaoRonda[0].desPermissao;
                            List<GrupoColetoresRonda> ListGrupos = new List<GrupoColetoresRonda>();
                            ListGrupos = objBLGrupoColetoresRonda.BuscaGrupoColetores(ListPermissaoRonda[0].idPermissao);

                            foreach (var item in ListGrupos)
                            {
                                //chkGrupoColetores
                                var iiTem = ListColetores.Find(ee => ee.idGrupoColetores == item.idGrupoColetores);

                                if (iiTem != null)
                                {
                                    iiTem.Selecionado = true;
                                    //chkGrupoColetores.Items[ListColetores.IndexOf(iiTem)].Selected = true;
                                }
                            }

                        }
                    }
                }

                PopularGrupoCorretoresRonda(ListColetores);


            }
        }

        #region ListGrupoColetores

        public void PopularGrupoCorretoresRonda(List<GrupoColetoresRonda> ListColetores)
        {
            try
            {
                rptGrupoColetores.DataSource = ListColetores;
                rptGrupoColetores.DataBind();

            }
            catch (Exception ex)
            {
                ExceptionHandler.TrataErro(ex, TipoMensagem.Adm, this.Page);
            }
        }
        #endregion


    }
}