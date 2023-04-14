using System;
using System.Collections.Generic;
using System.Text;
using FrameWork.DataLayer.Utilitarios;

namespace SafWeb.DataLayer.Usuarios
{
    public class DLUsuario : FrameWork.DataLayer.Usuarios.DALUsuario
    {
         #region Construtor
        /// ----------------------------------------------------------------------------- 
        /// <summary> 
        ///     Constructor 
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 22/6/2009 Created 
        /// </history> 
        /// ----------------------------------------------------------------------------- 
        public DLUsuario()
        {
            strConnection = DALUtilitarios.GetStringConnection(DALUtilitarios.ConexoesDisponiveis.ConnectionFW);
            conProvider = new DALProvider(strConnection);
            intCommandTimeOut = DALUtilitarios.ObterCommandTimeOut();
        }
        #endregion


    }
}
