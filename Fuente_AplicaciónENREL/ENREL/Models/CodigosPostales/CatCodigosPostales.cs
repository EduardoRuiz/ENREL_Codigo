using System;
using System.Collections.Generic;
using System.Web;

namespace ENREL.Models.CodigosPostales
{
    public class CatCodigosPostales
    {
        public int IdCodigoPostal { get; set; }
        public string CodigoPostal { get; set; }
        public int IdEntidadFederativa { get; set; }
        public int IdMunicipioINEGI { get; set; }
        public int IdLocalidadINEGI { get; set; }
    }

    public class UbicacionPorCP
    {
        public string TipoUbicacion { get; set; }
        public int IdUbicacion { get; set; }
    }
}