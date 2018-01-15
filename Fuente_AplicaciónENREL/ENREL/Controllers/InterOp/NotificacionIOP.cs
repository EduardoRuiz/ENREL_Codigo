using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ENREL.Models.RespuestasWS;
using ENREL.Models.Empresas;
using ENREL.Models.Proyectos;
using ENREL.Models.RepresentantesLegales;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using ENREL.Controllers.Auxiliar;
using ENREL.Models.Tramites;

namespace ENREL.Controllers.WSCre
{
    public class NotificacionIOP : Controller
    {
        //
        RespuestaClienteWS Respuesta = new RespuestaClienteWS();
        LogicaProyectos logicaproyectos = new LogicaProyectos();
        LogicaEmpresas logicaempresas = new LogicaEmpresas();
        LogicaRepresentantesLegales logicaRL = new LogicaRepresentantesLegales();
        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        LogicaTramites LogicaTramite = new LogicaTramites();


        // GET: /WSCliente/
        String FolioRespuesta;
        String FolioPeticion;
        String Codigo;
        String Descripcion;

  #region CRE

         public RespuestaClienteWS CreNotificacion(string IdGlobalMacroTramite, int idEmpresa, int idProyecto, int idRL)
        {
           

            var empresa = logicaempresas.L_DetallesEmpresa(idEmpresa);
            var RL = logicaRL.L_DetallesRepresentanteLegal(idRL);
            var proyecto = logicaproyectos.L_DetallesProyectos(idProyecto);

            var proxy = new NotificacionesIOP.NotificacionDeInteroperabilidadClient();
            var request = new NotificacionesIOP.NotificacionDeInteroperabilidadRequest()
            {

                HeaderInteroperabilidad = ObtenerHeaderInteroperabilidad(proyecto, RL),
                InformacionDeNegocio = ObtenerInformacionDeNegocio(proyecto, empresa, RL),
                DocumentosAdjuntos = new NotificacionesIOP.DocumentosAdjuntos()

            };

            
            var response = proxy.Notificacion(request);
           
            
          
            //Serializamos el payload o request para poder almacenarlo en la B.D.
            var payload = Serializar.SerializarToXml(request);
        
      
            Respuesta.FolioPeticion = response.FolioPeticion;
            Respuesta.FolioRespuesta = response.FolioRespuesta;
            Respuesta.Codigo = response.EstatusEntregaMensaje.Codigo;
            Respuesta.Descripcion = response.EstatusEntregaMensaje.Descripcion;
            Respuesta.XML = payload;

            LogicaENRELRespuestasWS logicaWS = new LogicaENRELRespuestasWS();

            if (Respuesta.Codigo == 1)
            {
                int codResp = Respuesta.Codigo;
                var fpeticion = Respuesta.FolioPeticion;
                var fResp = Respuesta.FolioRespuesta;
                var fmensaje = Respuesta.Descripcion;
                var xml = Respuesta.XML;
                MetodoGeneral.RegistroDeError("Respuesta Interop Sincrona- CodigoRespuesta: " + codResp + ", foliopeticion: " + fpeticion + ", foliorespuesta: " + fResp + ", Descripcion: " + fmensaje + ", XML: " + xml + "", "-Invocacion WSCRE");
                var insertarRespuesta = logicaWS.L_InsertarNotificacionIOP(fpeticion, fResp, codResp, fmensaje,IdGlobalMacroTramite , xml);
                
            }
            else
            {
                int codResp = Respuesta.Codigo;
                var fpeticion = Respuesta.FolioPeticion;
                var fResp = Respuesta.FolioRespuesta;
                var fmensaje = Respuesta.Descripcion;
                var xml = Respuesta.XML;
                MetodoGeneral.RegistroDeError("Respuesta Interop Sincrona- CodigoRespuesta: " + codResp + ", foliopeticion: " + fpeticion + ", foliorespuesta: " + fResp + ", Descripcion: " + fmensaje + ", XML: " + xml + "", "-Invocacion WSCRE");
                var insertarRespuesta = logicaWS.L_InsertarNotificacionIOP(fpeticion, fResp, codResp, fmensaje,IdGlobalMacroTramite, xml);
                var notificar = NotificarEnvioTramite("CRE-15-022", idProyecto.ToString(), RL.RL_RFC);
            }



            return Respuesta;

            }



        public NotificacionesIOP.HeaderInteroperabilidad ObtenerHeaderInteroperabilidad(CatProyectos proyectos, CatRepresentantesLegales ReLegal)
        {

            var headerInteroperabilidad = new NotificacionesIOP.HeaderInteroperabilidad()
            {

                DestinoNegocio = "T",
                FolioPeticion = Guid.NewGuid().ToString(),
                HomoclaveDestino = "CRE-15-022",
                IdConsumidorIOP = "SENER",
                IdGlobal = proyectos.P_IdGlobal,
                IdProductorIOP = "CRE",
                Identidad = ReLegal.RL_CedulaRFC,
                OrigenNegocio = "M"
            };

            return headerInteroperabilidad;
        }

        public NotificacionesIOP.InformacionDeNegocio ObtenerInformacionDeNegocio(CatProyectos proyectos, CatEmpresas empresas, CatRepresentantesLegales REl)
        {

            var informacionDeNegocio = new NotificacionesIOP.InformacionDeNegocio()
            {

                CamposPersonalizados = ObtenerCamposPersonalizados(proyectos, empresas, REl),
                Dependencia = "CRE",
                HomoclaveOrigen = proyectos.P_Tecnologia,
                Mensaje = "Envío de Información"
            };
            return informacionDeNegocio;
        }

        public NotificacionesIOP.CamposPersonalizados ObtenerCamposPersonalizados(CatProyectos proyectos, CatEmpresas empresas, CatRepresentantesLegales ReLegal)
        {
            var empresa = new NotificacionesIOP.Empresa()
            {
                Calle = empresas.E_Calle,
                CodigoPostal = empresas.E_CodigoPostal,
                Colonia = empresas.E_Colonia,
                CorreoElectronico = empresas.E_CorreoElectronico,
                EntidadFederativa = empresas.E_EntidadFederativa,
                Lada = empresas.E_Lada,
                Localidad = empresas.E_Localidad,
                Municipio = empresas.E_Municipio,
                NombreComercial = empresas.E_NombreComercial,
                NumeroExterior = empresas.E_NumeroExterior,
                NumeroInterior = empresas.E_NumeroInterior,
                RFC = empresas.E_RFC,
                RazonSocial = empresas.E_RazonSocial,
                TelefonoFijo = empresas.E_TelefonoFijo,
                TipoAsentamiento = empresas.E_IdTipoAsentamiento.ToString(),
                TipoVialidad = empresas.E_IdTipoVialidad.ToString()
            };

            var proyecto = new NotificacionesIOP.Proyecto()
            {
                P_Avance = "0",
                P_CapacidadInstalada = proyectos.P_CapacidadInstalada.ToString(),
                P_CodigoPostal = proyectos.P_CodigoPostal,
                P_Colonia = proyectos.P_Colonia,
                P_EntidadFederativa = proyectos.P_EntidadFederativa,
                P_EstatusProyecto = proyectos.P_EstatusProyecto,
                P_FactorPlanta = proyectos.P_FactorPlanta.ToString(),
                P_Fase = "1",
                P_FechaRegistro = proyectos.P_FechaRegistro.ToString(),
                P_GeneracionAnual = proyectos.P_GeneracionAnual.ToString(),
                P_IdGlobalMacroTramite = proyectos.P_IdGlobal,
                P_IdTipoAsentamiento = proyectos.P_IdTipoAsentamiento.ToString(),
                P_IdTipoVialidad = proyectos.P_IdTipoVialidad.ToString(),
                P_Latitud = proyectos.P_Latitud.ToString(),
                P_Localidad = proyectos.P_Localidad,
                P_Longitud = proyectos.P_Longitud.ToString(),
                P_MontoInversion = proyectos.P_MontoInversion.ToString(),
                P_Municipio = proyectos.P_Municipio,
                P_NombreProyecto = proyectos.P_NombreProyecto,
                P_PorcentajePositivo = proyectos.P_PorcentajePositivo.ToString(),
                P_Tecnologia = proyectos.P_Tecnologia,
                P_TipoAsentamiento = proyectos.P_IdTipoAsentamiento.ToString(),
                P_TipoVialidad = proyectos.P_TipoVialidad,
                P_Unidades = proyectos.P_Unidades.ToString()
            };

            var representanteLegal = new NotificacionesIOP.RepresentanteLegal()
            {
                Apellido = ReLegal.RL_PrimerApellido,
                CURP = ReLegal.RL_CURP,
                Calle = ReLegal.RL_Calle,
                CodigoPostal = ReLegal.RL_CodigoPostal,
                Colonia = ReLegal.RL_Colonia,
                CorreoElectronico = ReLegal.RL_CorreoElectronico,
                EntidadFederativa = ReLegal.RL_EntidadFederativa,
                ExtensionTelefonica = ReLegal.RL_ExtensionTelefonica,
                Lada = ReLegal.RL_Lada,
                Localidad = ReLegal.RL_Localidad,
                Municipio = ReLegal.RL_IdMunicipio.ToString(),
                Nombre = ReLegal.RL_Nombre,
                NumeroExterior = ReLegal.RL_NumeroExterior,
                NumeroInterior = ReLegal.RL_NumeroInterior,
                RFC = ReLegal.RL_RFC,
                SegundoApellido = ReLegal.RL_SegundoApellido,
                TelefonoCelular = ReLegal.RL_TelefonoCelular,
                TelefonoFijo = ReLegal.RL_TelefonoFijo,
                TipoAsentamiento = ReLegal.RL_IdTipoAsentamiento.ToString(),
                TipoVialidad = ReLegal.RL_TipoCalle
            };

            var camposPersonalizados = new NotificacionesIOP.CamposPersonalizados()
            {
                Empresa = empresa,
                Proyecto = proyecto,
                RepresentanteLegal = representanteLegal
            };

            return camposPersonalizados;
        }

  #endregion

  //#region CENACE

  //      public RespuestaClienteWS CNCNotificacion(string IdGlobalMacroTramite, int idEmpresa, int idProyecto, int idRL)
  //      {
  //          var empresa = logicaempresas.L_DetallesEmpresa(idEmpresa);
  //          var RL = logicaRL.L_DetallesRepresentanteLegal(idRL);
  //          var proyecto = logicaproyectos.L_DetallesProyectos(idProyecto);
  //          var proxy = new NotIOPCENACE.VunSvcClient();
  //          var request = new NotIOPCENACE.NotificacionDeInteroperabilidadRequest()
  //          {
  //              HeaderInteroperabilidad = ObtenerHeaderInteroperabilidadCNC(proyecto, RL),
  //              InformacionDeNegocio = ObtenerInformacionDeNegocioCNC(proyecto, empresa, RL),
  //              DocumentosAdjuntos= new NotIOPCENACE.DocumentosAdjuntos()

  //          };
  //          var response = proxy.NotificacionDeInteroperabilidadRequest(request);

  //          //Serializamos el payload o request para poder almacenarlo en la B.D.
  //          var payload = Serializar.SerializarToXml(request);


  //          Respuesta.FolioPeticion = response.FolioPeticion;
  //          Respuesta.FolioRespuesta = response.FolioRespuesta;
  //          Respuesta.Codigo = response.EstatusEntregaMensaje.Codigo;
  //          Respuesta.Descripcion = response.EstatusEntregaMensaje.Descripcion;
  //          Respuesta.XML = payload;

  //          return Respuesta;
  //      }

  //      public NotIOPCENACE.HeaderInteroperabilidad ObtenerHeaderInteroperabilidadCNC(CatProyectos proyectos, CatRepresentantesLegales ReLegal)
  //      {
  //          var headerInteroperabilidad = new NotIOPCENACE.HeaderInteroperabilidad()
  //          {
  //              DestinoNegocio = "T",
  //              FolioPeticion = Guid.NewGuid().ToString(),
  //              HomoclaveDestino ="CENACE-01-001-A",
  //              IdConsumidorIOP = "SENER",
  //              IdGlobal = proyectos.P_IdGlobal,
  //              IdProductorIOP = "CENACE",
  //              Identidad = ReLegal.RL_CedulaRFC,
  //              OrigenNegocio = "M"
  //          };
  //          return headerInteroperabilidad;

  //      }

  //      public NotIOPCENACE.InformacionDeNegocio ObtenerInformacionDeNegocioCNC(CatProyectos proyectos, CatEmpresas empresas, CatRepresentantesLegales REl)
  //      {
  //          var informacionDeNegocio = new NotIOPCENACE.InformacionDeNegocio()
  //          {

  //              CamposPersonalizados = ObtenerCamposPersonalizadosCNC(proyectos, empresas, REl),
  //              Dependencia = "CENACE",
  //              HomoclaveOrigen = proyectos.P_Tecnologia,
  //              Mensaje = "Envío de Información"
  //          };
  //          return informacionDeNegocio;

  //      }

  //      public NotIOPCENACE.CamposPersonalizados ObtenerCamposPersonalizadosCNC(CatProyectos proyectos, CatEmpresas empresas, CatRepresentantesLegales ReLegal)
  //      {
  //          var empresaCNC = new NotIOPCENACE.Empresa()
  //          {
  //              Calle = empresas.E_Calle,
  //              CodigoPostal = empresas.E_CodigoPostal,
  //              Colonia = empresas.E_Colonia,
  //              CorreoElectronico = empresas.E_CorreoElectronico,
  //              EntidadFederativa = empresas.E_EntidadFederativa,
  //              Lada = empresas.E_Lada,
  //              Localidad = empresas.E_Localidad,
  //              Municipio = empresas.E_Municipio,
  //              NombreComercial = empresas.E_NombreComercial,
  //              NumeroExterior = empresas.E_NumeroExterior,
  //              NumeroInterior = empresas.E_NumeroInterior,
  //              RFC = empresas.E_RFC,
  //              RazonSocial = empresas.E_RazonSocial,
  //              TelefonoFijo = empresas.E_TelefonoFijo,
  //              TipoAsentamiento = empresas.E_IdTipoAsentamiento.ToString(),
  //              TipoVialidad = empresas.E_IdTipoVialidad.ToString()
  //          };

  //          var proyectoCNC = new NotIOPCENACE.Proyecto()
  //          {
  //              P_Avance = "0",
  //              P_CapacidadInstalada = proyectos.P_CapacidadInstalada.ToString(),
  //              P_CodigoPostal = proyectos.P_CodigoPostal,
  //              P_Colonia = proyectos.P_Colonia,
  //              P_EntidadFederativa = proyectos.P_EntidadFederativa,
  //              P_EstatusProyecto = proyectos.P_EstatusProyecto,
  //              P_FactorPlanta = proyectos.P_FactorPlanta.ToString(),
  //              P_Fase = "1",
  //              P_FechaRegistro = proyectos.P_FechaRegistro.ToString(),
  //              P_GeneracionAnual = proyectos.P_GeneracionAnual.ToString(),
  //              P_IdGlobalMacroTramite = proyectos.P_IdGlobal,
  //              P_IdTipoAsentamiento = proyectos.P_IdTipoAsentamiento.ToString(),
  //              P_IdTipoVialidad = proyectos.P_IdTipoVialidad.ToString(),
  //              P_Latitud = proyectos.P_Latitud.ToString(),
  //              P_Localidad = proyectos.P_Localidad,
  //              P_Longitud = proyectos.P_Longitud.ToString(),
  //              P_MontoInversion = proyectos.P_MontoInversion.ToString(),
  //              P_Municipio = proyectos.P_Municipio,
  //              P_NombreProyecto = proyectos.P_NombreProyecto,
  //              P_PorcentajePositivo = proyectos.P_PorcentajePositivo.ToString(),
  //              P_Tecnologia = proyectos.P_Tecnologia,
  //              P_TipoAsentamiento = proyectos.P_IdTipoAsentamiento.ToString(),
  //              P_TipoVialidad = proyectos.P_TipoVialidad,
  //              P_Unidades = proyectos.P_Unidades.ToString()
  //          };

  //          var representanteLegalCNC = new NotIOPCENACE.RepresentanteLegal()
  //          {
  //              Apellido = ReLegal.RL_PrimerApellido,
  //              CURP = ReLegal.RL_CURP,
  //              Calle = ReLegal.RL_Calle,
  //              CodigoPostal = ReLegal.RL_CodigoPostal,
  //              Colonia = ReLegal.RL_Colonia,
  //              CorreoElectronico = ReLegal.RL_CorreoElectronico,
  //              EntidadFederativa = ReLegal.RL_EntidadFederativa,
  //              ExtensionTelefonica = ReLegal.RL_ExtensionTelefonica,
  //              Lada = ReLegal.RL_Lada,
  //              Localidad = ReLegal.RL_Localidad,
  //              Municipio = ReLegal.RL_IdMunicipio.ToString(),
  //              Nombre = ReLegal.RL_Nombre,
  //              NumeroExterior = ReLegal.RL_NumeroExterior,
  //              NumeroInterior = ReLegal.RL_NumeroInterior,
  //              RFC = ReLegal.RL_RFC,
  //              SegundoApellido = ReLegal.RL_SegundoApellido,
  //              TelefonoCelular = ReLegal.RL_TelefonoCelular,
  //              TelefonoFijo = ReLegal.RL_TelefonoFijo,
  //              TipoAsentamiento = ReLegal.RL_IdTipoAsentamiento.ToString(),
  //              TipoVialidad = ReLegal.RL_TipoCalle
  //          };

  //          var camposPersonalizados = new NotIOPCENACE.CamposPersonalizados()
  //          {
  //              Empresa = empresaCNC,
  //              Proyecto = proyectoCNC,
  //              RepresentanteLegal = representanteLegalCNC
  //          };

  //          return camposPersonalizados;
  //      }

        //#endregion
      
        
  #region Métodos Auxiliares:

        public JsonResult NotificarEnvioTramite(string HomoclaveEnviada, string ProyectoEnviado, string RFCRL)
        {
            try
            {

                EstatusTramite EstatusTramiteEnviado = new EstatusTramite();
                EstatusTramiteEnviado.estatus = "ENVIADO";
                EstatusTramiteEnviado.fechaRegistro = DateTime.Now.ToString();
                EstatusTramiteEnviado.nota = "Enviado a dependencia desde ENREL";
                EstatusTramiteEnviado.resolucion = "PENDIENTE";

                int IdProyecto = Convert.ToInt32(ProyectoEnviado);

                LogicaTramite.L_ActualizarEstatusTramiteDesdeENREL(IdProyecto, HomoclaveEnviada);
                WSBPM_Nivel3.ProcessNivel3PortTypeClient EnviarEstatus = new WSBPM_Nivel3.ProcessNivel3PortTypeClient();
                EnviarEstatus.receiveTask(ProyectoEnviado, HomoclaveEnviada, "enviado", 0);
                MetodoGeneral.RegistroDeError("IDPROYECTO = '" + IdProyecto.ToString() + "', HOMOCLAVE ='" + HomoclaveEnviada + "', ESTATUS = 'enviado'", "Invocación Nivel 3 - ");
                MetodoGeneral.RegistroDeError(EnviarEstatus.Endpoint.Address.ToString(), "Invocación Nivel 3 - ");
                string FechaHora = DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss");

                ////Enviar correo a dependencia y RL:
                //int IdProyectoRecibido = Convert.ToInt32(IdProyecto);
                //NotificacionInicioTramite DatosNotificacion = new NotificacionInicioTramite();
                //DatosNotificacion = LogicaProyecto.L_SeleccionarDatosNotificacionInicioTramite(IdProyecto, HomoclaveEnviada, RFCRL);

                ////Primer Correo (Dependencia): 
                //MailMessage email = new MailMessage("enrel@energia.gob.mx", DatosNotificacion.CorreoResponsable);
                //email.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoAdministrador"].ToString()));
                //email.Subject = "Notificación de inicio de trámite desde ENREL";

                ////Primer Correo - Obtener la plantilla en HTML:
                //string ContenidoCorreo = "";
                //string path = ConfigurationManager.AppSettings["Html_NotificacionInicioTramiteDependencia"].ToString();
                //ContenidoCorreo = System.IO.File.ReadAllText(path);

                ////Primer Correo - Datos del correo
                //ContenidoCorreo = ContenidoCorreo.Replace("#IdGlobalMacro#", DatosNotificacion.IdGlobal);
                //ContenidoCorreo = ContenidoCorreo.Replace("#FechaHora#", FechaHora);
                //ContenidoCorreo = ContenidoCorreo.Replace("#Tecnologia#", DatosNotificacion.Tecnologia);
                //ContenidoCorreo = ContenidoCorreo.Replace("#Homoclave#", DatosNotificacion.HomoclaveGeneral);
                //ContenidoCorreo = ContenidoCorreo.Replace("#Dependencia#", DatosNotificacion.Dependencia);
                //ContenidoCorreo = ContenidoCorreo.Replace("#RFCRL#", RFCRL);

                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(ContenidoCorreo, null, "text/html");

                ////Primer Correo - Obtener imágenes:

                //LinkedResource Logotipo_SENER = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_SENER"].ToString());
                //Logotipo_SENER.ContentId = "Logotipo_SENER";
                //htmlView.LinkedResources.Add(Logotipo_SENER);

                //LinkedResource Logotipo_MEXICO = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_MEXICO"].ToString());
                //Logotipo_MEXICO.ContentId = "Logotipo_MEXICO";
                //htmlView.LinkedResources.Add(Logotipo_MEXICO);

                //email.AlternateViews.Add(htmlView);
                //email.IsBodyHtml = true;
                //email.Priority = MailPriority.High;

                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "172.16.70.110";
                //smtp.Port = 25;
                //smtp.EnableSsl = false;
                //smtp.UseDefaultCredentials = false;
                //smtp.Send(email);
                //email.Dispose();

                ////Segundo Correo (RL): 
                //email = new MailMessage("enrel@energia.gob.mx", DatosNotificacion.CorreoRL);
                //email.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoAdministrador"].ToString()));
                //email.Subject = "Notificación de inicio de trámite desde ENREL";

                ////Segundo Correo - Obtener la plantilla en HTML:
                //ContenidoCorreo = "";
                //path = ConfigurationManager.AppSettings["Html_NotificacionInicioTramiteRL"].ToString();
                //ContenidoCorreo = System.IO.File.ReadAllText(path);

                ////Segundo Correo - Datos del correo
                //ContenidoCorreo = ContenidoCorreo.Replace("#IdGlobalMacro#", DatosNotificacion.IdGlobal);
                //ContenidoCorreo = ContenidoCorreo.Replace("#FechaHora#", FechaHora);
                //ContenidoCorreo = ContenidoCorreo.Replace("#Tecnologia#", DatosNotificacion.Tecnologia);
                //ContenidoCorreo = ContenidoCorreo.Replace("#Homoclave#", DatosNotificacion.HomoclaveGeneral);
                //ContenidoCorreo = ContenidoCorreo.Replace("#Dependencia#", DatosNotificacion.Dependencia);
                //ContenidoCorreo = ContenidoCorreo.Replace("#NombreRL#", DatosNotificacion.NombreRL);

                //htmlView = AlternateView.CreateAlternateViewFromString(ContenidoCorreo, null, "text/html");

                ////Segundo Correo - Obtener imágenes:

                //Logotipo_SENER = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_SENER"].ToString());
                //Logotipo_SENER.ContentId = "Logotipo_SENER";
                //htmlView.LinkedResources.Add(Logotipo_SENER);

                //Logotipo_MEXICO = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_MEXICO"].ToString());
                //Logotipo_MEXICO.ContentId = "Logotipo_MEXICO";
                //htmlView.LinkedResources.Add(Logotipo_MEXICO);

                //email.AlternateViews.Add(htmlView);
                //email.IsBodyHtml = true;
                //email.Priority = MailPriority.High;

                //smtp = new SmtpClient();
                //smtp.Host = "172.16.70.110";
                //smtp.Port = 25;
                //smtp.EnableSsl = false;
                //smtp.UseDefaultCredentials = false;
                //smtp.Send(email);
                //email.Dispose();

                return new JsonResult
                {
                    Data = "ok"
                };
            }
            catch (Exception ex)
            {
                MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Enviar Trámite a BPM");
                return new JsonResult
                {
                    Data = "error"
                };
            }
        }

# endregion



    }
}