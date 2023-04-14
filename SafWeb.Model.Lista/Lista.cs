using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Lista
{
    public class Lista
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Lista
        /// Class : Model.Lista.Lista
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de Lista
        /// </summary> 
        /// <history> 
        ///     [mribeiro] 30/06/2009 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intIdLista;
        private string strDescricao;
        private int intRegional;
        private int intFilial;
        private string strFilial;
        private byte bytSituacao;
        private int intUsuario;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_ColaboradorLista  </summary> 
        public int IdLista
        {
            get { return intIdLista; }
            set { intIdLista = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_ColaboradorLista  </summary> 
        public string DescricaoLista
        {
            get { return strDescricao; }
            set { strDescricao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Regional  </summary> 
        public int IdRegional
        {
            get { return intRegional; }
            set { intRegional = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Filial  </summary> 
        public int IdFilial
        {
            get { return intFilial; }
            set { intFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Filial  </summary> 
        public string DescricaoFilial
        {
            get { return strFilial; }
            set { strFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Situacao  </summary> 
        public byte Situacao
        {
            get { return bytSituacao; }
            set { bytSituacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_UsuarioInclusao/Id_UsuarioAlteracao </summary> 
        public int IdUsuario
        {
            get { return intUsuario; }
            set { intUsuario = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_ColaboradorLista"] != System.DBNull.Value)
                this.IdLista = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_ColaboradorLista"));

            if (pobjIDataReader["Des_ColaboradorLista"] != System.DBNull.Value)
                this.DescricaoLista = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_ColaboradorLista"));

            if (pobjIDataReader["Id_Regional"] != System.DBNull.Value)
                this.IdRegional = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Regional"));

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.IdFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Des_Filial"] != System.DBNull.Value)
                this.DescricaoFilial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Filial"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Situacao = pobjIDataReader.GetByte(pobjIDataReader.GetOrdinal("Flg_Situacao"));

            if (pobjIDataReader["Id_Usuario"] != System.DBNull.Value)
                this.IdUsuario = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Usuario"));

        }
    }
}
