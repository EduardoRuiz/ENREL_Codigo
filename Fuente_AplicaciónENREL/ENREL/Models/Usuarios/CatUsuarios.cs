using System;
using System.Collections.Generic;
using System.Web;

namespace ENREL.Models.Usuarios
{
    public class CatUsuarios
    {
        public int U_IdUsuario { set; get; }
        public int U_IdTipoUsuario { set; get; }
        public string U_Nombre { set; get; }
        public string U_AntiguoNombre { set; get; }
        public string U_Password { set; get; }
        public string U_NuevoPassword { set; get; }
        public string U_ConfirmarPassword { set; get; }
        public string U_PasswordActualParaCambiarNombre { set; get; }
        public string U_PasswordActualParaCambiarCorreo { set; get; }
        public string U_PasswordActualParaCambiarPassword { set; get; }
        public string U_CorreoElectronico { set; get; }
        public int U_IdEmpresa { set; get; }
        public DateTime U_FechaRegistro { set; get; }
        public DateTime? U_FechaModificacion { set; get; }
        public string U_NombreEmpresa { get; set; }
        public string U_TipoUsuario { get; set; }
        public string U_ClavePrivada { get; set; }
        public int U_IdRepresentanteAsociado { get; set; }
        public string U_RFCRepresentanteAsociado { get; set; }
        public bool U_Activo { get; set; }
        public bool U_Ingreso { get; set; }
        public string U_OpenId { get; set; }
    }

    public class CatTiposUsuario
    {
        public int idTipoUsuario { get; set; }
        public string TipoUsuario { get; set; }
    }

    public class CatEstatusUsuario
    {
        public int IdEstatusUsuario { get; set; }
        public string EstatusUsuario { get; set; }
    }
}