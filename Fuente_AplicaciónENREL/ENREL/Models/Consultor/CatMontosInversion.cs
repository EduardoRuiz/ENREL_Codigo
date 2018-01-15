using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENREL.Models.Consultar
{
    public class CatMontosInversion
    {
        public int E_IdEmpresa { get; set; }
        public int M_IdEntidadFederativa { get; set; }
        public int M_IdMunicipio { get; set; }
        public string E_EntidadFederativa { get; set; }
        public string E_Municipio { get; set; }
        public string E_Nombre { get; set; }
        public DateTime E_FechaInicio { get; set; }
        public string E_Tecnologia { get; set; }
        public decimal E_MontoInversion { get; set; }

    }
}
