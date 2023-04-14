using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Filial
{
    public class FusoHorario
    {
        /// ----------------------------------------------------------------------------- 
        /// Project : SafWeb.Model.Filial
        /// Class : Model.Filial.FusoHorario
        /// ----------------------------------------------------------------------------- 
        /// <summary>  
        ///     Model de Fuso Horario
        /// </summary> 
        /// <history> 
        ///     [haguiar_4] 13/01/2011  Created
        /// </history> 
        /// -----------------------------------------------------------------------------
        #region Variáveis

        private int intIdFusoHorario;
        private double intFusoHorario;
        private string srtDescricaoFusoHorario;

        #endregion

        #region Propriedades
        /// <summary> Armazena o conteúdo de IdFusoHorario  </summary> 
        public int  IdFusoHorario
        {
            get { return intIdFusoHorario; }
            set { intIdFusoHorario = value; }
        }

        /// <summary> Armazena o conteúdo de dtFusoHorario  </summary> 
        public double ValorFusoHorario
        {
            get { return intFusoHorario; }
            set { intFusoHorario = value; }
        }

        /// <summary> Armazena o conteúdo da descrição do Fuso Horário  </summary> 
        public string DescricaoFusoHorario
        {
            get { return srtDescricaoFusoHorario; }
            set { srtDescricaoFusoHorario = value; }
        }
        
       #endregion

        public void FromIDataReader(System.Data.IDataReader pobjIDataReader)
        {
            if (pobjIDataReader == null) return;

            if (pobjIDataReader["Id_FusoHorario"] != System.DBNull.Value)
                this.IdFusoHorario = pobjIDataReader.GetInt32(pobjIDataReader.GetOrdinal("Id_FusoHorario"));

            if (pobjIDataReader["Des_FusoHorario"] != System.DBNull.Value)
                this.DescricaoFusoHorario = pobjIDataReader.GetString(pobjIDataReader.GetOrdinal("Des_FusoHorario"));

            if (pobjIDataReader["Vlr_FusoHorario"] != System.DBNull.Value)
                this.ValorFusoHorario = pobjIDataReader.GetDouble(pobjIDataReader.GetOrdinal("Vlr_FusoHorario"));
        }
   }
}