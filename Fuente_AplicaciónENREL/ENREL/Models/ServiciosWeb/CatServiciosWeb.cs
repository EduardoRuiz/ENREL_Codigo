using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENREL.Models.ServiciosWeb
{
    public class CatServiciosWeb
    {

    }

    public class BusquedaMacroTramite
    {
        public string idGlobalMacroTramite { get; set; }
        public string todos { get; set; }
        public string idOperador { get; set; }
        public string ip { get; set; }
    }

    public class NotificarEstatusMacroTramite
    {
        public string metodo { get; set; }
        public string idGlobalMacroTramite { get; set; }
        public string estatus { get; set; }
        public string resolucion { get; set; }
        public string idOperador { get; set; }
        public string ip { get; set; }
        public string fechaHora { get; set; }
        public string nota { get; set; }
    }

}