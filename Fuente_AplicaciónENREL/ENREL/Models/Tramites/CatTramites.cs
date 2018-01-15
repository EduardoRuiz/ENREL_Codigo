using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENREL.Models.Tramites
{
    public class CatTramites
    {
        public string T_HomoClave { get; set; }
        public string T_NombreTramite { get; set; }
        public string T_Estatus { get; set; }
        public string T_Descripcion { get; set; }
        public string T_RegistroCOFEMER { get; set; }
        public int T_PorcentajeAlertaA { get; set; }
        public int T_PorcentajeAlertaB { get; set; }
        public int T_PorcentajeAlertaC { get; set; }
        public int T_Prorroga { get; set; }
        public int T_IdTipoDia { get; set; }

        public string T_FechaInicio { get; set; }
        public string T_FechaFin { get; set; }
        public int T_IdTramites { get; set; }
        public string T_URLDependencia { get; set; }
        public bool T_InterOperabilidad { get; set; }
        public string T_URLRequisitos { get; set; }
        public string T_Color { get; set; }
        public int T_PorcentajeAvancePositivo { get; set; }
        public int T_PorcentajeAvanceNegativo { get; set; }
        public int T_IdFase { get; set; }
        public string T_Dependencia { get; set; }
        public int T_DiasTranscurridos { get; set; }
        public int T_TiempoRecepcionDocumentos { get; set; }
        public int T_TiempoResolucion { get; set; }
        public bool T_Requerido { get; set; }

        public int T_PosicionX { get; set; }
        public int T_PosicionY { get; set; }

        public string T_CorreoResponsable { get; set; }
        public string T_CorreoSuperior { get; set; }

        public bool T_Activo { get; set; }
        public bool T_ConfirmacionBPM { get; set; }

        #region Reportes

        public string T_NombreProyecto { get; set; }

        #endregion
    }

    public class CatAvanceTramites
    {
        public int T_TramitesFinalizados { get; set; }
        public int T_TramitesTotales { get; set; }
    }

    public class CatEstatusTramite
    {
        public string T_NombreEstatus { get; set; }
        public string T_ColorEstatus { get; set; }
    }
}