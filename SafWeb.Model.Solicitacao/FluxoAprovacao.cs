using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Solicitacao
{
    [Serializable]
    public class FluxoAprovacao
    {

        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Solicitacao
        /// Class : Model.Solicitacao.FluxoAprovacao
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model da tabela de FluxoAprovacao
        /// </summary> 
        /// <history> 
        ///     [no history]
        ///     [haguiar] 25/04/2011 15:00 modify
        ///     incluir propriedade Flg_AprovaAreaTI e acrescentar Flg_AprovaAreaTI no
        ///     datareader
        ///     [haguiar] 24/02/2012 11:03 modify
        ///     incluir propriedade Flg_AprovaCracha e acrescentar Flg_AprovaCracha no
        ///     datareader
        /// </history> 
        /// -----------------------------------------------------------------------------
        /// 
        #region Variáveis

        private int intCodTipoSolicitacao;
	    private string strDesTipoSolicitacao;
        private int intCodOrdemAprovacao;
        private int intCodStatusSolicitacao;
        private string strDesStatusAprovacao;
	    private bool? blnFlgAprovaAreaSeg;
	    private bool? blnFlgAprovaContingencia;
        private int intCodNivelAprovacao;
        private string strDesNivelAprovacao;
        private bool? blnFlgSituacao;
        private bool? blnFlgAprovaAreaTI;
        private bool? blnFlgAprovaCracha;
        private int intCodFluxoAprovacao;
        #endregion

        #region Propriedades

        public int CodTipoSolicitacao
        {
            get { return intCodTipoSolicitacao; }
            set { intCodTipoSolicitacao = value; }
        }

        public string DesTipoSolicitacao
        {
            get { return strDesTipoSolicitacao; }
            set { strDesTipoSolicitacao = value; }
        }

        public int CodOrdemAprovacao
        {
            get { return intCodOrdemAprovacao; }
            set { intCodOrdemAprovacao = value; }
        }

        public int CodStatusSolicitacao
        {
            get { return intCodStatusSolicitacao; }
            set { intCodStatusSolicitacao = value; }
        }

        public string DesStatusAprovacao
        {
            get { return strDesStatusAprovacao; }
            set { strDesStatusAprovacao = value; }
        }

        public bool? FlgAprovaAreaSeg
        {
            get { return blnFlgAprovaAreaSeg; }
            set { blnFlgAprovaAreaSeg = value; }
        }

        public bool? FlgAprovaContingencia
        {
            get { return blnFlgAprovaContingencia; }
            set { blnFlgAprovaContingencia = value; }
        }

        public int CodNivelAprovacao
        {
            get { return intCodNivelAprovacao; }
            set { intCodNivelAprovacao = value; }
        }

        public string DesNivelAprovacao
        {
            get { return strDesNivelAprovacao; }
            set { strDesNivelAprovacao = value; }
        }

        public bool? FlgSituacao
        {
            get { return blnFlgSituacao; }
            set { blnFlgSituacao = value; }
        }

        public bool? FlgAprovaAreaTI
        {
            get { return blnFlgAprovaAreaTI; }
            set { blnFlgAprovaAreaTI = value; }
        }

        public bool? FlgAprovaCracha
        {
            get { return blnFlgAprovaCracha; }
            set { blnFlgAprovaCracha = value; }
        }

        public int CodFluxoAprovacao
        {
            get { return intCodFluxoAprovacao; }
            set { intCodFluxoAprovacao = value; }
        }

        #endregion

        /// <history> 
        ///     [no history]
        ///     [haguiar] 07/06/2011 modify
        ///     converter Flg_AprovaAreaTI BIT para boolean
        /// </history> 
        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_TipoSolicitacao"] != System.DBNull.Value)
                this.CodTipoSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_TipoSolicitacao"));

            if (pobjIDataReader["Des_TipoSolicitacao"] != System.DBNull.Value)
                this.DesTipoSolicitacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_TipoSolicitacao"));

            if (pobjIDataReader["Id_OrdemAprovacao"] != System.DBNull.Value)
                this.CodOrdemAprovacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_OrdemAprovacao"));

            if (pobjIDataReader["Id_StatusSolicitacao"] != System.DBNull.Value)
                this.CodStatusSolicitacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_StatusSolicitacao"));

            if (pobjIDataReader["Des_StatusAprovacao"] != System.DBNull.Value)
                this.DesStatusAprovacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_StatusAprovacao"));

            if (pobjIDataReader["Flg_AprovaAreaSeg"] != System.DBNull.Value)
                this.FlgAprovaAreaSeg = (pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AprovaAreaSeg")).ToString() == "1" ? true : false);

            if (pobjIDataReader["Flg_AprovaContingencia"] != System.DBNull.Value)
                this.FlgAprovaContingencia = (pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AprovaContingencia")).ToString() == "1" ? true : false);

            if (pobjIDataReader["Id_NivelAprovacao"] != System.DBNull.Value)
                this.CodNivelAprovacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_NivelAprovacao"));

            if (pobjIDataReader["Des_NivelAprovacao"] != System.DBNull.Value)
                this.DesNivelAprovacao = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_NivelAprovacao"));

            if (pobjIDataReader["Flg_Situacao"] != System.DBNull.Value)
                this.FlgSituacao = (pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_Situacao")).ToString() == "1" ? true : false);

            if (pobjIDataReader["Id_FluxoAprovacao"] != System.DBNull.Value)
                this.CodFluxoAprovacao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_FluxoAprovacao"));

            if (pobjIDataReader["Flg_AprovaAreaTI"] != System.DBNull.Value)
                this.FlgAprovaAreaTI = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AprovaAreaTI")));

            if (pobjIDataReader["Flg_AprovaCracha"] != System.DBNull.Value)
                this.FlgAprovaCracha = Convert.ToBoolean(pobjIDataReader.GetValue(pobjIDataReader.GetOrdinal("Flg_AprovaCracha")));

        }
    }
}
