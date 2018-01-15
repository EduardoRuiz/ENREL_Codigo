using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsENREL_Dependencias.Models
{
    public class SolicitudENREL
    {
        //Datos de entrada
        public string IdGlobalMacroTramite { get; set; }
        public string RFC_Solicitante { get; set; }
    }

    public class RespuestaENREL
    {
        public Empresa Empresa { get; set; }
        public RepresentanteLegal RepresentanteLegal { get; set; }
        public Proyecto Proyecto { get; set; }
        public string Dependencia { get; set; }
        public string Mensaje { get; set; }
    }

    public class Empresa
    {
        //Empresa: Datos Identidad
        public string NombreComercial { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }

        //Empresa: Datos geográficos
        public string TipoVialidad { get; set; }
        public string Calle { get; set; }

        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string TipoAsentamiento { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }

        //Empresa: Datos de contacto
        public string Lada { get; set; }
        public string TelefonoFijo { get; set; }
        public string CorreoElectronico { get; set; }

        //Empresa: Datos asociados
        public string Localidad { get; set; }
        public string Municipio { get; set; }
        public string EntidadFederativa { get; set; }
    }

    public class RepresentanteLegal
    {
        //Representante: Datos de Identidad
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }

        //Representante: Datos geográficos
        public string TipoVialidad { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string TipoAsentamiento { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Localidad { get; set; }
        public string Municipio { get; set; }
        public string EntidadFederativa { get; set; }

        //Representante: Datos de contacto
        public string Lada { get; set; }
        public string TelefonoFijo { get; set; }
        public string ExtensionTelefonica { get; set; }
        public string TelefonoCelular { get; set; }
        public string CorreoElectronico { get; set; }

    }

    public class Proyecto
    {
        public string P_IdGlobalMacroTramite { get; set; }
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
        public int P_PorcentajePositivo { get; set; }

        public string P_CodigoPostal { get; set; }
        public string P_EntidadFederativa { get; set; }
        public string P_Municipio { get; set; }
        public string P_Localidad { get; set; }
        public int P_IdEntidadFederativa { get; set; }
        public int P_IdMunicipio { get; set; }
        public int P_IdLocalidad { get; set; }

        public int P_IdTipoAsentamiento { get; set; }
        public int P_TipoAsentamiento { get; set; }
        public string P_Colonia { get; set; }
        public int P_IdTipoVialidad { get; set; }
        public string P_TipoVialidad { get; set; }

        public int P_Avance { get; set; }
        public int P_Fase { get; set; }
    }

}