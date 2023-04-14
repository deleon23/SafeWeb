using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Filial
{
    [Serializable]
    public class AprovadorFilial
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Filial
        /// Class : Model.Filial.AprovadorFilial
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de AprovadorFilial
        /// </summary> 
        /// <history> 
        ///     [haguiar_5] 16/02/2011 Created 
        ///     [haguiar] 26/04/2011 modified
        ///     incluir Flg_AprovaAreaTI
        ///     [haguiar] 24/02/2012 modified
        ///     incluir Flg_AprovaCracha
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intId_Usuario;
        private int intId_Regional;
        private int intId_Filial;

        private int intId_AprovadorFilial;

        private int intId_NivelAprovacao;
        private int intId_AprovaSegNivel;

        private bool blnFlg_AprovaAreaSeg;
        private bool blnFlg_AprovaContingencia;
        private bool blnFlg_AprovaAreaTI;
        private bool blnFlg_AprovaCracha;

        private DateTime dtInicioPeriodo;
        private DateTime dtFimPeriodo;

        private string strDes_Justificativa;

        private int intId_SistOrigemChamado;

        private string strNum_Chamado;

        bool blnFlg_Situacao;

        private DateTime dtInclusao;
        private DateTime dtAlteracao;

        private int intId_UsuarioAlteracao;

        private string strDes_Filial;
        private string strDes_Regional;

        private string strNom_Superior;
        private string strDes_NivelAprovacao;
        private string strDes_Vigencia;
        private string strDes_SistOrigemSol;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo intId_AprovadorFilial  </summary> 
        public int Id_AprovadorFilial
        {
            get { return intId_AprovadorFilial; }
            set { intId_AprovadorFilial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Usuario  </summary> 
        public int Id_Usuario
        {
            get { return intId_Usuario; }
            set { intId_Usuario = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Regional </summary> 
        public int Id_Regional
        {
            get { return intId_Regional; }
            set { intId_Regional = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Filial </summary> 
        public int Id_Filial
        {
            get { return intId_Filial; }
            set { intId_Filial = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_NivelAprovacao </summary> 
        public int Id_NivelAprovacao
        {
            get { return intId_NivelAprovacao; }
            set { intId_NivelAprovacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_AprovaSegNivel </summary> 
        public int Id_AprovaSegNivel
        {
            get { return intId_AprovaSegNivel; }
            set { intId_AprovaSegNivel = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_AprovaAreaSeg </summary> 
        public bool Flg_AprovaAreaSeg
        {
            get { return blnFlg_AprovaAreaSeg; }
            set { blnFlg_AprovaAreaSeg = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_AprovaContingencia </summary> 
        public bool Flg_AprovaContingencia
        {
            get { return blnFlg_AprovaContingencia; }
            set { blnFlg_AprovaContingencia = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_AprovaCracha </summary> 
        public bool Flg_AprovaCracha
        {
            get { return blnFlg_AprovaCracha; }
            set { blnFlg_AprovaCracha = value; }
        }

        /// <summary> Armazena o conteúdo do campo dtInicioPeriodo </summary> 
        public DateTime InicioPeriodo
        {
            get { return dtInicioPeriodo; }
            set { dtInicioPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo dtFimPeriodo </summary> 
        public DateTime FimPeriodo 
        {
            get { return dtFimPeriodo; }
            set { dtFimPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo do campo strDes_Justificativa </summary> 
        public string Des_Justificativa
        {
            get { return strDes_Justificativa; }
            set { strDes_Justificativa = value; }
        }

        /// <summary> Armazena o conteúdo do campo intId_OrigemSol </summary> 
        public int Id_OrigemSol
        {
            get { return intId_SistOrigemChamado; }
            set { intId_SistOrigemChamado = value; }
        }

        /// <summary> Armazena o conteúdo do campo strDes_NumeroSol </summary> 
        public string Des_NumeroSol
        {
            get { return strNum_Chamado; }
            set { strNum_Chamado = value; }
        }

        /// <summary> Armazena o conteúdo do campo Flg_Situacao  </summary> 
        public bool Flg_Situacao
        {
            get { return blnFlg_Situacao; }
            set { blnFlg_Situacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo dtInclusao </summary> 
        public DateTime DataInclusao
        {
            get { return dtInclusao; }
            set { dtInclusao = value; }
        }

        /// <summary> Armazena o conteúdo do campo dtAlteracao </summary> 
        public DateTime DataAlteracao
        {
            get { return dtAlteracao; }
            set { dtAlteracao = value; }
        }

        /// <summary> Armazena o conteúdo do campo intId_UsuarioAlteracao </summary> 
        public int Id_UsuarioAlteracao
        {
            get { return intId_UsuarioAlteracao; }
            set { intId_UsuarioAlteracao = value; }
        }

        /// <summary> Armazena o conteúdo do campo strDes_Filial </summary> 
        public string Des_Filial
        {
            get { return strDes_Filial; }
            set { strDes_Filial = value; }
        }

        /// <summary> Armazena o conteúdo do campo strDes_Regional </summary> 
        public string Des_Regional
        {
            get { return strDes_Regional; }
            set { strDes_Regional = value; }
        }

        /// <summary> Armazena o conteúdo do campo strDes_Vigencia </summary> 
        public string Des_Vigencia
        {
            get { return strDes_Vigencia; }
            set { strDes_Vigencia = value; }
        }

        /// <summary> Armazena o conteúdo do campo strNom_Superior </summary> 
        public string Nom_Superior
        {
            get { return strNom_Superior; }
            set { strNom_Superior = value; }
        }

        /// <summary> Armazena o conteúdo do campo strDes_NivelAprovacao </summary> 
        public string Des_NivelAprovacao
        {
            get { return strDes_NivelAprovacao; }
            set { strDes_NivelAprovacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo strDes_OrigemSol </summary> 
        public string Des_OrigemSol
        {
            get { return strDes_SistOrigemSol; }
            set { strDes_SistOrigemSol = value; }
        }

        //[haguiar] created 26/04/2011
        /// <summary> Armazena o conteúdo do campo Flg_AprovaAreaTI  </summary> 
        public bool Flg_AprovaAreaTI
        {
            get { return blnFlg_AprovaAreaTI; }
            set { blnFlg_AprovaAreaTI = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_AprovadorFilial"] != System.DBNull.Value)
                this.Id_AprovadorFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_AprovadorFilial"));

            if (pobjIDataReader["Id_Usuario"] != System.DBNull.Value)
                this.Id_Usuario = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Usuario"));

            if (pobjIDataReader["Id_Regional"] != System.DBNull.Value)
                this.Id_Regional = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Regional"));

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.Id_Filial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Id_NivelAprovacao"] != System.DBNull.Value)
                this.Id_NivelAprovacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_NivelAprovacao"));

            if (pobjIDataReader["Id_AprovaSegNivel"] != System.DBNull.Value)
                this.Id_AprovaSegNivel = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_AprovaSegNivel"));

            if (pobjIDataReader["Flg_AprovaAreaSeg"] != System.DBNull.Value)
                this.Flg_AprovaAreaSeg = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AprovaAreaSeg")));

            if (pobjIDataReader["Flg_AprovaContingencia"] != System.DBNull.Value)
                this.Flg_AprovaContingencia = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AprovaContingencia"))); 

            if (pobjIDataReader["Dt_InicioPeriodo"] != System.DBNull.Value)
                this.InicioPeriodo = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_InicioPeriodo"));

            if (pobjIDataReader["Dt_FinalPeriodo"] != System.DBNull.Value)
                this.FimPeriodo = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_FinalPeriodo"));

            if (pobjIDataReader["Des_Justificativa"] != System.DBNull.Value)
                this.Des_Justificativa = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Justificativa"));

            if (pobjIDataReader["Id_SistOrigemChamado"] != System.DBNull.Value)
                this.Id_OrigemSol = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_SistOrigemChamado"));

            if (pobjIDataReader["Num_Chamado"] != System.DBNull.Value)
                this.Des_NumeroSol = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Num_Chamado"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Flg_Situacao = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_Situacao"))); 

            if (pobjIDataReader["Dt_Inclusao"] != System.DBNull.Value)
                this.DataInclusao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Inclusao"));

            if (pobjIDataReader["Dt_Alteracao"] != System.DBNull.Value)
                this.DataAlteracao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Alteracao"));

            if (pobjIDataReader["Id_UsuarioAlteracao"] != System.DBNull.Value)
                this.Id_UsuarioAlteracao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioAlteracao"));

            if (pobjIDataReader["Des_Filial"] != System.DBNull.Value)
                this.Des_Filial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Filial"));

            if (pobjIDataReader["Des_Regional"] != System.DBNull.Value)
                this.Des_Regional = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Regional"));

            if (pobjIDataReader["Nom_Superior"] != System.DBNull.Value)
                this.Nom_Superior = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Superior"));

            if (pobjIDataReader["Des_Vigencia"] != System.DBNull.Value)
                this.Des_Vigencia = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Vigencia"));

            if (pobjIDataReader["Des_NivelAprovacao"] != System.DBNull.Value)
                this.Des_NivelAprovacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_NivelAprovacao"));

            if (pobjIDataReader["Des_OrigemChamado"] != System.DBNull.Value)
                this.Des_OrigemSol = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_OrigemChamado"));

            if (pobjIDataReader["Flg_AprovaAreaTI"] != System.DBNull.Value)
                this.Flg_AprovaAreaTI = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AprovaAreaTI"))); 

            if (pobjIDataReader["Flg_AprovaCracha"] != System.DBNull.Value)
                this.Flg_AprovaCracha = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AprovaCracha"))); 

        }
    }

    public class UsuarioAprovadorFilial
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Filial
        /// Class : Model.Filial.UsuarioAprovadorFilial
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model de dados do usuário
        /// </summary> 
        /// <history> 
        ///     [haguiar_5] 18/02/2011 Created 
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intId_Usuario;
        private string strNomUsuario;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Usuario  </summary> 
        public int USU_N_CODIGO
        {
            get { return intId_Usuario; }
            set { intId_Usuario = value; }
        }

        /// <summary> Armazena o conteúdo do campo strNomUsuario </summary> 
        public string USU_C_NOME
        {
            get { return strNomUsuario; }
            set { strNomUsuario = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["USU_N_CODIGO"] != System.DBNull.Value)
                this.USU_N_CODIGO = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("USU_N_CODIGO"));

            if (pobjIDataReader["USU_C_NOME"] != System.DBNull.Value)
                this.USU_C_NOME = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("USU_C_NOME"));

        }
    }
}