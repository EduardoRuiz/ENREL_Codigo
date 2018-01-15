using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENREL.Models.DiasInhabiles
{
    public class CatDiasInhabiles
    {
        public int IdDiaInhabil { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }
        public string FechaCompleta { get; set; }
        public bool Activo { get; set; }
    }
}