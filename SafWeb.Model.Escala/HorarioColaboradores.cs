using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : HorarioColaborador
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementa��o da classe HorarioColaborador
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 18/1/2010 
    /// [haguiar] modify 02/01/2012 14:52
    /// incluir campo cod_legado
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    [Serializable]
    public class HorarioColaboradores
    {
        #region Vari�veis

        private int intCodigoEscalacao;
        private string strHorarioColaboradores;
        private Collection<DateTime> colDataColaboradores;
        private string strCodigosColaboradores;
        private string strDesColaboradores;

        private int intCodLegado;

        private bool blnBloqueado;
        private bool blnCompensado;
        private bool blnFolga;
        private bool blnLicenca;

        private bool blnExcluirTrocaHorario;
        private bool blnHorarioFlex;
        private bool blnHoraExtra;
        #endregion

        #region Propriedades

        /// <history>
        ///     [haguiar] created 03/01/2012 09:03
        ///</history>
        /// <summary> Armazena o conte�do do campo Cod_Legado  </summary> 
        public int CodLegado
        {
            get { return this.intCodLegado; }
            set { this.intCodLegado = value; }
        }

        /// <summary> Armazena o conte�do do campo Id_Escalacao  </summary> 
        public int IdEscalacao
        {
            get { return this.intCodigoEscalacao; }
            set { this.intCodigoEscalacao = value; }
        }

        /// <summary> Armazena o conte�do do campo hor�rio do colaboradores  </summary> 
        public string HorarioColaborador
        {
            get { return this.strHorarioColaboradores; }
            set { this.strHorarioColaboradores = value; }
        }

        /// <summary> Armazena o conte�do do campo datas do colaboradores  </summary> 
        public Collection<DateTime> DatasColaboradores
        {
            get { return this.colDataColaboradores; }
            set { this.colDataColaboradores = value; }
        }

        /// <summary> Armazena o conte�do dos nomes dos colaboradores  </summary> 
        public string NomesColaboradores
        {
            get { return this.strDesColaboradores; }
            set { this.strDesColaboradores = value; }
        }

        /// <summary> Armazena o conte�do o Id dos colaboradores  </summary> 
        public string CodigosColaboradores
        {
            get { return this.strCodigosColaboradores; }
            set { this.strCodigosColaboradores = value; }
        }

        
        /// <summary> Excluir a troca de hor�rio </summary> 
        /// <history>
        ///     [haguiar] created 02/12/2010
        /// </history>
        public bool ExcluirTrocaHorario
        {
            get { return this.blnExcluirTrocaHorario; }
            set { this.blnExcluirTrocaHorario = value; }
        }

        /// <summary> Hora Extra </summary> 
        /// <history>
        ///     [haguiar_8829] created 06/07/2011 14:51
        /// </history>
        public bool HoraExtra
        {
            get { return this.blnHoraExtra; }
            set { this.blnHoraExtra = value; }
        }


        /// <summary> Hor�rio 08 �s 09 Flex </summary> 
        /// <history>
        ///     [haguiar] created 04/12/2010
        /// </history>
        public bool HorarioFlex
        {
            get { return this.blnHorarioFlex; }
            set { this.blnHorarioFlex = value; }
        }

        /// <summary> O item est� ou n�o bloqueado </summary> 
        public bool Bloqueado
        {
            get { return this.blnBloqueado; }
            set { this.blnBloqueado = value; }
        }

        /// <summary> Armazena o conte�do do campo Flg_Compensado  </summary> 
        public bool Compensado
        {
            get { return this.blnCompensado; }
            set { this.blnCompensado = value; }
        }

        /// <summary> Armazena o conte�do do campo Flg_Folga  </summary> 
        public bool Folga
        {
            get { return this.blnFolga; }
            set { this.blnFolga = value; }
        }

        /// <summary> Armazena o conte�do do campo Flg_Licenca  </summary> 
        public bool Licenca
        {
            get { return this.blnLicenca; }
            set { this.blnLicenca = value; }
        }

        public bool FlgIniciaFolgando { get; set; }
        #endregion
    }
}
