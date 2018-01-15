using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ENREL.Models.RespuestasWS
{
    public class LogicaENRELRespuestasWS
    {
        Datos_ENREL_Dep_WS DatosAuxiliar = new Datos_ENREL_Dep_WS();
        DataTable DtAuxiliar = new DataTable();
        public int L_InsertarNotificacionIOP(string fpet, string fresp, int codigo, string descripcion, string idglobalM, string XML)
        {
            int IdRespuesta = 0;
            IdRespuesta = DatosAuxiliar.D_InsertarNotificacionIOP(fpet, fresp, codigo, descripcion, idglobalM, XML);

            return IdRespuesta;
        }
    }
}