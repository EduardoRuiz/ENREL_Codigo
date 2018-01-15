using System;
using System.Collections.Generic;
using System.Web;

namespace ENREL.Models.Proyectos
{
    public class CatProyectos
    {
        public int P_IdProyecto { get; set; }
        public int P_IdEmpresa { get; set; }
        public string P_Empresa { get; set; }
        public int P_IdTecnologia { get; set; }
        public string P_Tecnologia { get; set; }
        public string P_EstatusProyecto { get; set; }
        public string P_NombreProyecto { get; set; }
        public double P_Latitud { get; set; }
        public double P_Longitud { get; set; }
        public double P_CapacidadInstalada { get; set; }
        public int P_Unidades { get; set; }
        public double P_GeneracionAnual { get; set; }
        public double P_FactorPlanta { get; set; }
        public double P_MontoInversion { get; set; }
        public DateTime P_FechaRegistro { get; set; }
        public DateTime P_FechaFin { get; set; }
        public int P_DiasAgregados { get; set; }
        public int P_PorcentajePositivo { get; set; }
        public int P_PorcentajeNegativo { get; set; }
        public string P_IdGlobal { get; set; }
        public bool P_Activo { get; set; }

        public string P_CodigoPostal { get; set; }
        public int P_IdEntidadFederativa { get; set; }
        public string P_EntidadFederativa { get; set; }
        public int P_IdMunicipio { get; set; }
        public string P_Municipio { get; set; }
        public int P_IdLocalidad { get; set; }
        public string P_Localidad { get; set; }
        
        public int P_IdTipoAsentamiento { get; set; }
        public int P_TipoAsentamiento { get; set; }
        public string P_Colonia { get; set; }
        public int P_IdTipoVialidad { get; set; }
        public string P_TipoVialidad { get; set; }

        public string P_ClavePrivada { get; set; }

        public int P_Avance { get; set; }
        public int P_Fase { get; set; }
    }

    public class CatPreguntas
    {
        public int IdPregunta { get; set; }
        public string Pregunta { get; set; }
    }

    public class ProyectoPreguntas
    {
        public int IdPregunta { get; set; }
        public string Pregunta { get; set; }
        public bool Respuesta { get; set; }
        public bool Modificar { get; set; }
    }

    public class NotificacionInicioTramite
    {
        public int IdEstatusTramite { get; set; }
        public string IdGlobal { get; set; }
        public string HomoclaveGeneral { get; set; }
        public string Dependencia { get; set; }
        public string CorreoResponsable { get; set; }
        public string CorreoRL { get; set; }
        public string NombreRL { get; set; }
        public string Tecnologia { get; set; }
    }

}