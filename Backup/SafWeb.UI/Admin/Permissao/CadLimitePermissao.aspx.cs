using System;
using System.Collections.Generic;
using SafWeb.Model.Permissao;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SafWeb.BusinessLayer.Permissao;
using System.Text.RegularExpressions;
using SafWeb.Model.Util;
using SafWeb.Util.Extension;


namespace SafWeb.UI.Admin.Permissao
{
    public partial class CadLimitePermissao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (Request["idLimitePermissao"] != null)
                {
                    idLimitePermissao.Value = Request["idLimitePermissao"];
                    var blLimitePermissao= new BLLimitePermissao();
                    var LimitePermissao = blLimitePermissao.BuscaPorIdLimitePermissao(Request["idLimitePermissao"].ToInt32());

                    txtLimite.Value = LimitePermissao.Limite.ToString("##0.00");
                }
            }
        }
   
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
           
        }
    }
}