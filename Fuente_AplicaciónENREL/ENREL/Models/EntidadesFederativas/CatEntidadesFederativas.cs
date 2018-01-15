using System;
using System.Collections.Generic;
using System.Web;

namespace ENREL.Models.EntidadesFederativas
{
    public class CatEntidadesFederativas
    {
        public int IdEntidadFederativa { get; set; }
        public string EntidadFederativa { get; set; }
        public int IdMunicipioAsociado { get; set; }
        public string MunicipioAsociado { get; set; }
        public int IdLocalidadAsociada { get; set; }
        public string LocalidadAsociada { get; set; }
        public string TipoUbicacion { get; set; }
    }
}