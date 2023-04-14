using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace SafWeb.Model.Escala
{
    /// ----------------------------------------------------------------------------- 
    /// Project : 
    /// Class : JornadaColaboradores
    /// ----------------------------------------------------------------------------- 
    /// <summary>  
    /// Implementação da classe JornadaColaboradores
    /// </summary> 
    /// <history> 
    /// [cmarchi] created 13/1/2009 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    [Serializable]
    public class JornadaColaboradores
    {
        #region Variáveis

        private int intCodigoEscalacao;
        private int intCodigoJornada;
        private string strDesJornada;
        private string strCodigosColaboradors;
        private string strDesColaboradores;

        #endregion

        #region Propriedades

        /// <summary> Armazena o conteúdo do campo Id_Escalacao  </summary> 
        public int IdEscalacao
        {
            get { return this.intCodigoEscalacao; }
            set { this.intCodigoEscalacao = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Jornada  </summary> 
        public int IdJornada
        {
            get { return this.intCodigoJornada; }
            set { this.intCodigoJornada = value; }
        }

        /// <summary> Armazena o conteúdo do campo Des_Jornada  </summary> 
        public string DescricaoJornada
        {
            get { return this.strDesJornada; }
            set { this.strDesJornada = value; }
        }

        /// <summary> Armazena o conteúdo do campo Id_Colaborador  </summary> 
        public string CodigosColaboradores
        {
            get { return this.strCodigosColaboradors; }
            set { this.strCodigosColaboradors = value; }
        }

        /// <summary> Armazena o conteúdo do campo Nom_Colaborador  </summary> 
        public string NomesColaboradores
        {
            get { return this.strDesColaboradores; }
            set { this.strDesColaboradores = value; }
        }

        #endregion
    }
}
