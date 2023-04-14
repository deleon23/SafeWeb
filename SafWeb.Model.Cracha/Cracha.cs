using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Cracha
{
    public class Cracha
    {
        #region Variáveis

        private int intCodCracha;
        private string strNumCracha;
		private int intCodCrachaTipo;
		private string strDesCrachaTipo;
		private int intCodFilial;
		private string strAliasFilial;
		private int intCodRegional;
		private string strDesRegional;
		private bool? blnFlgSituacao;
		private string strDesObservacao;

        #endregion

        #region Propriedades

        public int CodCracha
        {
            get{return intCodCracha;}
            set{intCodCracha=value;}
        }
        
		public string NumCracha
        {
            get{return strNumCracha;}
            set { strNumCracha = value; }
        }
        
		public int CodCrachaTipo
        {
            get{return intCodCrachaTipo;}
            set{intCodCrachaTipo=value;}
        }
        
		public string DesCrachaTipo
        {
            get{return strDesCrachaTipo;}
            set{strDesCrachaTipo=value;}
        }
        
		public int CodFilial
        {
            get{return intCodFilial;}
            set{intCodFilial=value;}
        }
        
		public string AliasFilial
        {
            get{return strAliasFilial;}
            set{strAliasFilial=value;}
        }
        
		public int CodRegional
        {
            get{return intCodRegional;}
            set{intCodRegional=value;}
        }
        
		public string DesRegional
        {
            get{return strDesRegional;}
            set{strDesRegional=value;}
        }
        
		public bool? FlgSituacao
        {
            get{return blnFlgSituacao;}
            set{blnFlgSituacao=value;}
        }
        
		public string DesObservacao
        {
            get{return strDesObservacao;}
            set{strDesObservacao=value;}
        }
        

        #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_Cracha"] != System.DBNull.Value)
                this.CodCracha = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Cracha"));

            if (pobjIDataReader["Num_Cracha"] != System.DBNull.Value)
                this.NumCracha = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Num_Cracha"));

            if (pobjIDataReader["Id_CrachaTipo"] != System.DBNull.Value)
                this.CodCrachaTipo = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_CrachaTipo"));

            if (pobjIDataReader["Des_CrachaTipo"] != System.DBNull.Value)
                this.DesCrachaTipo = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_CrachaTipo"));

            if (pobjIDataReader["Id_Filial"] != System.DBNull.Value)
                this.CodFilial = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Filial"));

            if (pobjIDataReader["Alias_Filial"] != System.DBNull.Value)
                this.AliasFilial = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Alias_Filial"));

            if (pobjIDataReader["Id_Regional"] != System.DBNull.Value)
                this.CodRegional = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_Regional"));

            if (pobjIDataReader["Des_Regional"] != System.DBNull.Value)
                this.DesRegional = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Regional"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.FlgSituacao = (pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_Situacao")).ToString() == "1");

             if (pobjIDataReader["Des_Observacao"] != System.DBNull.Value)
                this.DesObservacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_Observacao"));
        }
    }
}
