using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENREL.Models.Tecnologias
{
    public class CatTecnologias
    {
        public int IdTecnologia { get; set; }
        public string Tecnologia { get; set; }
        public string UltimaReglaNegocio { get; set; }
        public int UltimoTramite { get; set; }
        public int PosicionUltimoTramite { get; set; }
        public List<string> ReglasDeNegocio { get; set; }
        public List<int> Tramites { get; set; }
    }

    public class CatTecnologiaPreguntas
    {
        public int IdTecnologiaPregunta { get; set; }
        public int IdTecnologia { get; set; }
        public string Pregunta { get; set; }
        public bool Activo { get; set; }
    }

    public class CatTecnologiaTramites
    {
        public int IdTecnologiaTramite { get; set; }
        public int IdTecnologia { get; set; }
        public string Homoclave { get; set; }
        public int IdTramite { get; set; }
        public int IdFase { get; set; }
        public int? IdPosicion { get; set; }
        public bool Activo { get; set; }
    }
}