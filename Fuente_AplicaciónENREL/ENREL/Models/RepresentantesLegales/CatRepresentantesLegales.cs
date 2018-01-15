using System;
using System.Collections.Generic;
using System.Web;

namespace ENREL.Models.RepresentantesLegales
{
    public class CatRepresentantesLegales
    {
        //Datos de Identidad

        public int RL_IdRepresentanteLegal { get; set; }
        public string RL_Nombre { get; set; }
        public string RL_PrimerApellido { get; set; }
        public string RL_SegundoApellido { get; set; }
        public string RL_CURP { get; set; }
        public string RL_RFC { get; set; }

        //Datos geográficos

        public int RL_IdTipoVialidad { get; set; }
        public string RL_Calle { get; set; }
        public string RL_NumeroExterior { get; set; }
        public string RL_NumeroInterior { get; set; }
        public int RL_IdTipoAsentamiento { get; set; }
        public string RL_Colonia { get; set; }
        public int RL_IdLocalidad { get; set; }
        public int RL_IdMunicipio { get; set; }
        public int RL_IdEntidadFederativa { get; set; }
        public string RL_CodigoPostal { get; set; }
        public int RL_IdTipoColonia { get; set; }
        public string RL_TipoCalle { get; set; }
        public string RL_Localidad { get; set; }
        public string RL_Municipio { get; set; }
        public string RL_EntidadFederativa { get; set; }

        //Datos de contacto

        public string RL_Lada { get; set; }       
        public string RL_TelefonoFijo { get; set; }
        public string RL_ExtensionTelefonica { get; set; }
        public string RL_TelefonoCelular { get; set; }
        public string RL_CorreoElectronico { get; set; }

        //Datos de acreditación

        public int RL_IdEmpresa { get; set; }
        public DateTime RL_Vigencia { get; set; }
        public string RL_PoderNotarial { get; set; }
        public string RL_ActaConstitutiva { get; set; }
        public string RL_IdentificacionOficial { get; set; }
        public string RL_CedulaRFC { get; set; }
        public string RL_FechaSolicitud { get; set; }
        public string RL_FechaRegistro { get; set; }
        public string RL_FechaModificacion { get; set; }
        public string RL_Observaciones { get; set; }
        public bool RL_Activo { get; set; }
        public int RL_IdEstatusSolicitudRepresentante { get; set; }
        public string RL_EstatusSolicitudRepresentante { get; set; }
        public string RL_ClavePrivada { get; set; }

        //Relaciones

        public string E_NombreComercial { get; set; }
        public string E_RFC { get; set; }
    }
}