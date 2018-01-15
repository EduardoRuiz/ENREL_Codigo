using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ENREL.Models.Empresas
{
    public class CatEmpresas
    {
        //Identidad
        public int E_IdEmpresa { get; set; }
        public string E_NombreComercial { get; set; }
        public string E_RazonSocial { get; set; }
        public string E_RFC { get; set; }
        
        //Datos geográficos
        public int E_IdTipoVialidad { get; set; }
        public string E_Calle { get; set; }

        //public int E_NumeroExterior { get; set; }  7/03/2017
        public string E_NumeroExterior { get; set; }
        public string E_NumeroInterior { get; set; }
        public int E_IdTipoAsentamiento { get; set; }
        public string E_Colonia { get; set; }
        public int E_IdLocalidad { get; set; }
        public int E_IdMunicipio { get; set; }
        public int E_IdEntidadFederativa { get; set; }
        public string E_CodigoPostal { get; set; }

        //Datos de contacto
        public string E_Lada { get; set; }
        public string E_TelefonoFijo { get; set; }
        public string E_CorreoElectronico { get; set; }
        public bool E_Activo { get; set; }

        //Datos asociados
        public string E_Localidad { get; set; }
        public string E_Municipio { get; set; }
        public string E_EntidadFederativa { get; set; }

        //Datos de acreditación
        public string E_ClavePrivada { get; set; }
        public bool E_CompartirDatos { get; set; }

        //Datos de aplicación *RP
        //public DateTime FechaRegistro { get; set; }
        //public DateTime FechaSolicitud { get; set; }
        //public DateTime FechaModificacion { get; set; }
        //public bool Activo { get; set; }

        #region Reportes

        public DateTime E_FechaInicio { get; set; }
        public DateTime E_FechaFin { get; set; }
        public string E_Tecnologia { get; set; }
        public double E_MontoInversion { get; set; }
        #endregion

    }
}