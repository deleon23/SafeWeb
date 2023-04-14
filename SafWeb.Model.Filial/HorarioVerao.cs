using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Filial
{
    public class HorarioVerao
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Filial
        /// Class : Model.Filial.HorarioVerao
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model de HorarioVerao
        /// </summary> 
        /// <history> 
        ///     [haguiar_4] 14/01/2011  Created
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intIdHorarioVerao;
        private DateTime dtInicioPeriodo;
        private DateTime dtFinalPeriodo;

        private string strIdFiliais;
        private bool blnFlgSituacao;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Flg_Situacao  </summary> 
        public bool Situacao
        {
            get { return blnFlgSituacao; }
            set { blnFlgSituacao = value; }
        }

        /// <summary> Armazena o conteúdo de IdHorarioVerao  </summary> 
        public int  IdHorarioVerao
        {
            get { return intIdHorarioVerao; }
            set { intIdHorarioVerao = value; }
        }

        /// <summary> Armazena o conteúdo de dtInicioPeriodo  </summary> 
        public DateTime InicioPeriodo
        {
            get { return dtInicioPeriodo; }
            set { dtInicioPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo de dtFinalPeriodo  </summary> 
        public DateTime FinalPeriodo
        {
            get { return dtFinalPeriodo; }
            set { dtFinalPeriodo = value; }
        }

        /// <summary> Armazena o conteúdo de strCodigoFiliais  </summary> 
        public string IdFiliais
        {
            get { return strIdFiliais; }
            set { strIdFiliais  = value; }
        }
       #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_HorarioVerao"] != System.DBNull.Value)
                this.IdHorarioVerao = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_HorarioVerao"));

            if (pobjIDataReader["Dt_InicioPeriodo"] != System.DBNull.Value)
                this.InicioPeriodo = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_InicioPeriodo"));

            if (pobjIDataReader["Dt_FinalPeriodo"] != System.DBNull.Value)
                this.InicioPeriodo = pobjIDataReader.GetDateTime(pobjIDataReader.GetOrdinal("Dt_FinalPeriodo"));

            if (pobjIDataReader["Id_Filiais"] != System.DBNull.Value)
                this.IdFiliais = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Id_Filiais"));
            
            if (pobjIDataReader["Flg_HorarioVerao"] != System.DBNull.Value)
                this.blnFlgSituacao = pobjIDataReader.GetBoolean(pobjIDataReader.GetOrdinal("Flg_HorarioVerao"));

        }
   }
}