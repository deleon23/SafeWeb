using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Solicitacao
{
    [Serializable]
    public class PermissaoCrachaTitular
    {
        #region Variáveis

		private int intId_SolicitacaoCrachaTitular;
		private int intId_UsuarioSolicitante;
		private string strNom_UsuarioSolicitante;
		private int intId_Colaborador;
		private string strNom_Colaborador;
		private DateTime Dt_Solicitacao;
		private int intId_Regional;
		private int intId_Filial;
		private string strAlias_Filial;
		private int intId_TipoSolicitacao;
		private string strDes_TipoSolicitacao;
		private string  strId_Area;
        private string strDes_Area;
		private int intId_StatusSolicitacao;
		private string strDes_StatusAprovacao;
		private string strDes_MotivoSolicitacao;
		private string strDes_MotivoReprovacao;
        private bool blnFlg_Situacao;

        private string strId_Colaboradores;
        private string strNom_Colaboradores;

        private string strNom_PendenteCom;

        private string strCorFonte;

        #endregion

        #region Propriedades

        public string Nom_PendenteCom
        {
            get { return strNom_PendenteCom; }
            set { strNom_PendenteCom = value; }
        }

        public int Id_UsuarioSolicitante
        {
            get { return intId_UsuarioSolicitante; }
            set { intId_UsuarioSolicitante = value; }
        }


        public string Nom_UsuarioSolicitante
        {
            get { return strNom_UsuarioSolicitante; }
            set { strNom_UsuarioSolicitante = value; }
        }

        public string Id_Colaboradores
        {
            get { return strId_Colaboradores; }
            set { strId_Colaboradores = value; }
        }

        public string Nom_Colaboradores
        {
            get { return strNom_Colaboradores; }
            set { strNom_Colaboradores = value; }
        }

        public int Id_Colaborador
        {
            get { return intId_Colaborador; }
            set { intId_Colaborador = value; }
        }

        public string Nom_Colaborador
        {
            get { return strNom_Colaborador; }
            set { strNom_Colaborador = value; }
        }

        public DateTime Data_Solicitacao
        {
            get { return Dt_Solicitacao; }
            set { Dt_Solicitacao = value; }
        }

        public int Id_Regional
        {
            get { return intId_Regional; }
            set { intId_Regional = value; }
        }

        public int Id_Filial
        {
            get { return intId_Filial; }
            set { intId_Filial = value; }
        }

        public string Alias_Filial
        {
            get { return strAlias_Filial; }
            set { strAlias_Filial = value; }
        }

        public  int Id_TipoSolicitacao
        {
            get { return intId_TipoSolicitacao; }
            set { intId_TipoSolicitacao = value; }
        }

        public string Des_TipoSolicitacao
        {
            get { return strDes_TipoSolicitacao; }
            set { strDes_TipoSolicitacao = value; }
        }

        public string Id_Area
        {
            get { return strId_Area; }
            set { strId_Area = value; }
        }

        public string Des_Area
        {
            get { return strDes_Area; }
            set { strDes_Area = value; }
        }

        public int Id_StatusSolicitacao
        {
            get { return intId_StatusSolicitacao; }
            set { intId_StatusSolicitacao = value; }
        }

        public string Des_StatusAprovacao
        {
            get { return strDes_StatusAprovacao; }
            set { strDes_StatusAprovacao = value; }
        }


        public string Des_MotivoSolicitacao
        {
            get { return strDes_MotivoSolicitacao; }
            set { strDes_MotivoSolicitacao = value; }
        }

        public string Des_MotivoReprovacao
        {
            get { return strDes_MotivoReprovacao; }
            set { strDes_MotivoReprovacao = value; }
        }

        public bool Flg_Situacao
        {
            get { return blnFlg_Situacao; }
            set { blnFlg_Situacao = value; }
        }

        public int Id_SolicitacaoCrachaTitular
        {
            get { return intId_SolicitacaoCrachaTitular; }
            set { intId_SolicitacaoCrachaTitular = value; }
        }

        



        public string CorFonte
        {
            get { return strCorFonte; }
            set { strCorFonte = value; }
        }

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_SolicitacaoCrachaTitular"] != System.DBNull.Value)
                this.Id_SolicitacaoCrachaTitular = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_SolicitacaoCrachaTitular"));

            if (pobjIDataReader["Id_UsuarioSolicitante"] != System.DBNull.Value)
                this.Id_UsuarioSolicitante = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_UsuarioSolicitante"));

            if (pobjIDataReader["Nom_UsuarioSolicitante"] != System.DBNull.Value)
                this.Nom_UsuarioSolicitante = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_UsuarioSolicitante"));

            if (pobjIDataReader["Id_Colaborador"] != System.DBNull.Value)
                this.Id_Colaborador = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Colaborador"));

            if (pobjIDataReader["Nom_Colaborador"] != System.DBNull.Value)
                this.Nom_Colaborador = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Nom_Colaborador"));

            if (pobjIDataReader["Dt_Solicitacao"] != System.DBNull.Value)
                this.Dt_Solicitacao = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_Solicitacao"));
            
            if (pobjIDataReader["Id_Regional"] != System.DBNull.Value)
                this.Id_Regional = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Regional"));

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.Id_Filial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Alias_Filial"] != System.DBNull.Value)
                this.Alias_Filial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Alias_Filial"));


            if (pobjIDataReader["Id_TipoSolicitacao"] != System.DBNull.Value)
                this.Id_TipoSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoSolicitacao"));

            if (pobjIDataReader["Des_TipoSolicitacao"] != System.DBNull.Value)
                this.Des_TipoSolicitacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_TipoSolicitacao"));

            if (pobjIDataReader["Id_Area"] != System.DBNull.Value)
                this.Id_Area = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Id_Area"));

            if (pobjIDataReader["Des_Area"] != System.DBNull.Value)
                this.Des_Area = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Area"));
            
          
            if (pobjIDataReader["Id_StatusSolicitacao"] != System.DBNull.Value)
                this.Id_StatusSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_StatusSolicitacao"));

            if (pobjIDataReader["Des_StatusAprovacao"] != System.DBNull.Value)
                this.Des_StatusAprovacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_StatusAprovacao"));

            if (pobjIDataReader["Des_MotivoSolicitacao"] != System.DBNull.Value)
                this.Des_MotivoSolicitacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_MotivoSolicitacao"));

            if (pobjIDataReader["Des_MotivoReprovacao"] != System.DBNull.Value)
                this.Des_MotivoReprovacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_MotivoReprovacao"));


            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.Flg_Situacao = (Convert.ToInt16(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_Situacao"))) == 1 ? true : false);


            //if (pobjIDataReader["Html_CorFonte"] != System.DBNull.Value)
            //    this.CorFonte = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Html_CorFonte"));
        }
    }
}
