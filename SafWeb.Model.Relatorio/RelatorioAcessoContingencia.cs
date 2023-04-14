using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Relatorio
{
    public class RelatorioAcessoContingencia
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Relatorio
        /// Class : Model.Relatorio.RelatorioAcessoContingencia
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tela de Relatorio de Acesso em contingencia
        /// </summary> 
        /// <history> 
        ///     [haguiar] 08/04/2011 14:47
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private string strNomeColaborador;
        private string strNomeUsuarioLibAcesso;
        private string strAprovador;
        private string strDes_Motivo;
        private DateTime datInclusao;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo strDes_Motivo</summary>
        public string Des_Motivo
        {
            get { return strDes_Motivo; }
            set { strDes_Motivo = value; }
        }

        /// <summary> Armazena o conteúdo do campo strNomeColaborador  </summary>
        public string Nom_Colaborador
        {
            get { return strNomeColaborador; }
            set { strNomeColaborador = value; }
        }

        /// <summary> Armazena o conteúdo do campo strNomeUsuarioLibAcesso  </summary>
        public string NomeUsuarioLibAcesso
        {
            get { return strNomeUsuarioLibAcesso; }
            set { strNomeUsuarioLibAcesso = value; }
        }


        /// <summary> Armazena o conteúdo do campo strAprovador  </summary>
        public string Aprovador
        {
            get { return strAprovador; }
            set { strAprovador = value; }
        }

        /// <summary> Armazena o conteúdo do campo Dt_Inclusao  </summary>
        public DateTime Dt_Inclusao
        {
            get { return datInclusao; }
            set { datInclusao = value; }
        }
        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Nom_Colaborador"] != System.DBNull.Value)
                this.Nom_Colaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Colaborador"));

            if (pobjIDataReader["NomeUsuarioLibAcesso"] != System.DBNull.Value)
                this.NomeUsuarioLibAcesso = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("NomeUsuarioLibAcesso"));

            if (pobjIDataReader["Aprovador"] != System.DBNull.Value)
                this.Aprovador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Aprovador"));

            if (pobjIDataReader["Des_Motivo"] != System.DBNull.Value)
                this.Des_Motivo = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Motivo"));

            if (pobjIDataReader["Dt_Inclusao"] != System.DBNull.Value)
                this.Dt_Inclusao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Inclusao"));
        }
    }
}
