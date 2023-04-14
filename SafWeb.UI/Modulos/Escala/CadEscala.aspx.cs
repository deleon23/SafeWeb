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

namespace SafWeb.UI.Modulos.Escala
{
    public partial class CadEscala : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Botões

        #region Botão Fechar
        /// <summary>
        ///     Fecha a RadWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [cmarchi] created 19/11/2009
        /// </history>
        protected void btnFechar_Click(object sender, EventArgs e)
        {            
            ClientScript.RegisterStartupScript(String.Empty.GetType(), "CloseRad", "<script>CloseWin();</script>");
        }
        #endregion

        #endregion
    }
}
