using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wsENREL_Dependencias.Models;
using System.Web.Script.Serialization;
using System.Web;
using System.IO;
using System.Configuration;

namespace wsENREL_Dependencias.Controllers
{
    public class ENRELController : ApiController
    {
        // POST api/ENREL
        public RespuestaENREL Post(SolicitudENREL DatosDeReferencia)
        {
            DateTime FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            string registro = FechaActual.ToString() + " - WS_HabilitarTramite - ";
            RespuestaENREL Respuesta = new RespuestaENREL();
            LogicaENRELDependencias Logica = new LogicaENRELDependencias();

            try 
            {
                //Obtener la IP de origen de la llamada y consultar la Dependencia a la que pertenece
                string IP = HttpContext.Current.Request.UserHostAddress;
                string Dependencia = ValidarIP(IP);

                if (Dependencia != "")
                {
                    Respuesta = Logica.ConsultarDatos(DatosDeReferencia.IdGlobalMacroTramite, DatosDeReferencia.RFC_Solicitante);
                    Respuesta.Dependencia = Dependencia;
                    var jsonRespuesta = new JavaScriptSerializer().Serialize(Respuesta);
                }
                else
                {
                    Respuesta.Mensaje = "La IP: " + IP + " no es válida";
                }

                return Respuesta;
            }
            catch(Exception ex)
            {
                Respuesta.Mensaje = "Error inesperado, favor de contactar a un administrador de la aplicación.";

                //Registrar error
                registro = registro + ex.Message.ToString() + "Datos: IdGlobalMacroTramite - " + DatosDeReferencia.IdGlobalMacroTramite + ", RFC_Solicitante - " + DatosDeReferencia.RFC_Solicitante;

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registro);
                }

                return Respuesta;
            }
            
        }

        public string ValidarIP(string IP)
        {
            string Dependencia = "";
            LogicaENRELDependencias Logica = new LogicaENRELDependencias();
            Dependencia = Logica.L_ValidarIP(IP);
            return Dependencia;
        }
    }
}
