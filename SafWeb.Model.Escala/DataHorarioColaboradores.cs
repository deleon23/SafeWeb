using System;
using System.Collections.Generic;
using System.Text;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : DataHorarioColaboradores
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe DataHorarioColaboradores
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 20/1/2010 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    public class DataHorarioColaboradores
    {
        #region Variáveis

        private int intCodigoEscalacao;
        private DateTime datDataColaboradores;
        private string strCodigosColaboradores;
        private string strDesColaboradores;
        private string strCompensado;
        private string strFolga;
        private string strLicenca;
        private string strHorarioColaboradores;

        private string strHorarioFlex;
        private string strHoraExtra;

        private string strDesJornada;

        private int intCodLegado;

        #endregion

        #region Propriedades

        /// <history>
        ///     [haguiar] created 03/01/2012 09:03
        ///</history>
        /// <summary> Armazena o conteúdo do campo horário do colaboradores  </summary> 
        public string HorarioColaborador
        {
            get { return this.strHorarioColaboradores; }
            set { this.strHorarioColaboradores = value; }
        }

        /// <history>
        ///     [haguiar] created 03/01/2012 09:03
        ///</history>
        /// <summary> Armazena o conteúdo do campo Cod_Legado  </summary> 
        public int CodLegado
        {
            get { return this.intCodLegado; }
            set { this.intCodLegado = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Escalacao  </summary> 
        public int IdEscalacao
        {
            get { return this.intCodigoEscalacao; }
            set { this.intCodigoEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo data do colaboradores  </summary> 
        public DateTime DataColaboradores
        {
            get { return this.datDataColaboradores; }
            set { this.datDataColaboradores = value; }
        }

        /// <summary> Armazena o conteúdo dos nomes dos colaboradores  </summary> 
        public string NomesColaboradores
        {
            get { return this.strDesColaboradores; }
            set { this.strDesColaboradores = value; }
        }

        /// <summary> Armazena o conteúdo o Id dos colaboradores  </summary> 
        public string CodigosColaboradores
        {
            get { return this.strCodigosColaboradores; }
            set { this.strCodigosColaboradores = value; }
        }

        /// <summary> Armazena o conteúdo o Compensado  </summary> 
        public string Compensado
        {
            get { return this.strCompensado; }
            set { this.strCompensado = value; }
        }

        /// <summary> Armazena o conteúdo o Folga  </summary> 
        public string Folga
        {
            get { return this.strFolga; }
            set { this.strFolga = value; }
        }

        /// <summary> Armazena o conteúdo Férias  </summary> 
        public string Licenca
        {
            get { return this.strLicenca; }
            set { this.strLicenca = value; }
        }

        /// <summary> Armazena o conteúdo do Horário Flex  </summary> 
        /// <history>
        ///     [haguiar_2] created 04/12/2010
        /// </history>
        public string HorarioFlex
        {
            get { return this.strHorarioFlex; }
            set { this.strHorarioFlex = value; }
        }

        /// <summary> Armazena o conteúdo da Hora Extra </summary> 
        /// <history>
        ///     [haguiar_8829] created 06/07/2011 14:48
        /// </history>
        public string HoraExtra
        {
            get { return this.strHoraExtra; }
            set { this.strHoraExtra = value; }
        }

        /// <summary> Armazena o conteúdo do campo DesJornada  </summary> 
        /// <history>
        ///     [haguiar_9004] created 12/09/2011 11:42
        /// </history>
        public string DesJornada
        {
            get { return this.strDesJornada; }
            set { this.strDesJornada = value; }
        }

        public bool flgIniciaFolgando { get; set; }

        public int IdJornada { get; set; }

        public bool flgSituacao { get; set; }

        #endregion
    }
}
