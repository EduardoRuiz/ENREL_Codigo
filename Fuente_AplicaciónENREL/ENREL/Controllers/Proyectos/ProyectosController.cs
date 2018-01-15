using ENREL.Models.Proyectos;
using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ENREL.Controllers.Auxiliar;
using System.Threading;
using System.Configuration;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using ENREL.Models.Tramites;
using ENREL.Models.Empresas;
using ENREL.Models.RepresentantesLegales;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Xml;
using System.Net.Mail;
//using ENREL.Controllers.WSCre;
using ENREL.Models.RespuestasWS;
using ENREL.Controllers.WSCre;

namespace ENREL.Controllers.Proyectos
{
    public class ProyectosController : Controller
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        LogicaProyectos LogicaProyecto = new LogicaProyectos();
        LogicaTramites LogicaTramite = new LogicaTramites();
        LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
        LogicaRepresentantesLegales LogicaRepresentanteLegal = new LogicaRepresentantesLegales();
        string respuestaWSCRE;

        #endregion

        #region Páginas:

        // GET: Proyectos
        public ActionResult Index()
        {
            LogicaProyectos LogicaProyectos = new LogicaProyectos();
            List<CatProyectos> ListaProyectos = new List<CatProyectos>();
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && (Usuario.U_IdUsuario == 1 || Usuario.U_IdTipoUsuario == 2))
            {
                ListaProyectos = LogicaProyectos.L_SeleccionarProyectosPorEmpresa(Usuario.U_IdEmpresa);

                return View(ListaProyectos);
            }
            else { return RedirectToAction("Logout", "Home"); }
        }

        public ActionResult Insertar(CatProyectos Proyecto = null)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            try
            {
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                else
                {
                    List<CatPreguntas> ListaPreguntas = LogicaProyecto.L_SeleccionarPreguntas(0);
                    ViewBag.ListaPreguntas = new SelectList(ListaPreguntas, "IdPregunta", "Pregunta");
                    ViewBag.RFCRepresentante = Usuario.U_RFCRepresentanteAsociado;
                    ViewBag.IdTecnologia = 0;
                    CargarListasDesplegables(null, null, null, null);
                    return View(Proyecto);
                }


            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Insertar");
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Detalles()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }


                CatProyectos Proyecto = new CatProyectos();
                int IdProyecto = Convert.ToInt32(Request.Form["IdProyecto"]);

                Proyecto = LogicaProyecto.L_DetallesProyectos(IdProyecto);
                //if (Proyecto.P_CodigoPostal == 0) { Proyecto.P_CodigoPostal = null; }


                return View(Proyecto);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Detalles");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult INERE()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                int IdProyecto = Convert.ToInt32(Request.Form["IdProyecto"]);
                string tecnologia = Request.Form["tecnologia"].ToString();
                double latitud = Convert.ToDouble(Request.Form["latitud"]);
                double longitud = Convert.ToDouble(Request.Form["longitud"]);

                ViewBag.IdProyecto = IdProyecto;
                ViewBag.Tecnologia = tecnologia;
                ViewBag.Latitud = latitud;
                ViewBag.Longitud = longitud;

                return View();
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: INERE");
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult ProyectoTramites()
        {
            string IdEstatusProyecto = "NO CAPTURADO";
            string resultado = "";
            bool ErrorBPM = false;

            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                int Idproyecto = Convert.ToInt32(Request.Form["IdProyecto"]);
                CatProyectos Proyecto = LogicaProyecto.L_DetallesProyectos(Idproyecto);
                string IdProyectoBPM = Idproyecto.ToString();
                string Tecnologia = Proyecto.P_Tecnologia.ToLower();

                //Crea la instancia de los WS de BPM que vaya a utilizar
                WSBPM_Nivel0.ProcessNivel0PortTypeClient CrearProyectos = new WSBPM_Nivel0.ProcessNivel0PortTypeClient();
                WSBPM_Nivel3.ProcessNivel3PortTypeClient EnviarEstatus = new WSBPM_Nivel3.ProcessNivel3PortTypeClient();

                IdEstatusProyecto = LogicaProyecto.L_SeleccionarProyectoSeguimiento(Idproyecto);

                if (IdEstatusProyecto == "CAPTURADO")
                {
                    //COMUNICACIÓN: Enviar proyecto a BPM
                    CrearProyectos.InicioNivel0(Idproyecto.ToString(), Proyecto.P_Tecnologia.ToLower());
                    LogicaProyecto.L_ActualizarProyectoSeguimiento(Idproyecto, 2);
                    MetodoGeneral.RegistroDeError("Datos {IdProyecto: " + Idproyecto.ToString() + ", Tecnología: " + Proyecto.P_Tecnologia.ToLower() + "}", "Invocación Nivel 0 -");
                    MetodoGeneral.RegistroDeError(CrearProyectos.Endpoint.Address.ToString(), "Invocación Nivel 0 -");
                }

                IdEstatusProyecto = LogicaProyecto.L_SeleccionarProyectoSeguimiento(Idproyecto);

                if (IdEstatusProyecto == "ENVIADOBPM")
                {
                    //COMUNICACIÓN: Enviar proyecto a gobmx
                    resultado = MetodoGeneral.RegistrarReferenciaMacroTramite(Proyecto.P_IdGlobal, Usuario.U_RFCRepresentanteAsociado);
                    LogicaProyecto.L_ActualizarProyectoSeguimiento(Idproyecto, 3);
                    LogicaProyecto.L_ActualizarProyectoSeguimiento(Idproyecto, 4);
                }

                IdEstatusProyecto = LogicaProyecto.L_SeleccionarProyectoSeguimiento(Idproyecto);

                if (IdEstatusProyecto == "INICIADO" || IdEstatusProyecto == "SUSPENDIDO")
                {
                    //Preguntar estatus GOBMX
                    RespuestaConsultaMacroTramite Resultado = MetodoGeneral.ConsultarMacroTramite(Proyecto.P_IdGlobal);

                    //Guardar estatus de GOBMX en BD ENREL
                    if (Resultado.cadenaInteroperabilidad != null && Resultado.cadenaInteroperabilidad.Count > 0)
                    {
                        LogicaTramite.L_ActualizarEstatusTramite(Resultado, Proyecto.P_IdProyecto);
                    }

                    //Obtener estatus de los trámites ya en la base de datos que tienen que ser actualizados en BPM:
                    List<CatTramites> ListaTramitesActualizarBPM = LogicaTramite.L_SeleccionarProyectoTramitesParaEnviarBPM(Idproyecto);

                    //COMUNICACIÓN: Reporta estatus a BPM
                    try
                    {
                        foreach (CatTramites Tramite in ListaTramitesActualizarBPM)
                        {
                            if (Tramite.T_ConfirmacionBPM == false)
                            {
                                EnviarEstatus.receiveTask(Idproyecto.ToString(), Tramite.T_HomoClave, Tramite.T_Estatus.ToLower(), Tramite.T_Prorroga);
                                MetodoGeneral.RegistroDeError("IDPROYECTO = '" + Idproyecto.ToString() + "', HOMOCLAVE ='" + Tramite.T_HomoClave + "', ESTATUS = '" + Tramite.T_Estatus.ToLower() + "'", "Invocación Nivel 3 - ");
                                MetodoGeneral.RegistroDeError(EnviarEstatus.Endpoint.Address.ToString(), "Invocación Nivel 3 - ");
                            }
                            Thread.Sleep(100);
                        }
                    }
                    catch (Exception ex)
                    {
                        Session["TipoAlerta"] = "Error";
                        MetodoGeneral.RegistroDeError(ex.Message, "ProyectoTramites");
                        ErrorBPM = true;
                    }



                }

               
                

                IdEstatusProyecto = LogicaProyecto.L_SeleccionarProyectoSeguimiento(Idproyecto);

                switch (IdEstatusProyecto)
                {
                    case "CAPTURADO":
                        Session["TipoAlerta"] = "Error";
                        TempData["notice"] = "El proyecto se registró en ENREL pero no ha sido sido enviado al gestor de trámites.";
                        return RedirectToAction("Index");
                    case "ENVIADOBPM":
                        Session["TipoAlerta"] = "Error";
                        TempData["notice"] = "El proyecto no ha sido registrado en Gobmx.";
                        return RedirectToAction("Index");
                    case "ENVIADOGOBMX":
                        Session["TipoAlerta"] = "Error";
                        TempData["notice"] = "El proyecto ha sido registrado en Gobmx pero ocurrió un error inesperado";
                        return RedirectToAction("Index");
                    case "CONCLUIDO":
                        bool Resultado = MetodoGeneral.NotificarEstatusMacroTramite(Proyecto.P_IdGlobal, "1", "1");
                        if (Resultado)
                        {
                            LogicaProyecto.L_ActualizarProyectoSeguimiento(Idproyecto, 7);
                        }
                        break;
                    default: break;
                }

                //Obtener estatus de los trámites ya en la base de datos:
                Thread.Sleep(1000);
                List<CatTramites> ListaTramites = LogicaTramite.L_SeleccionarProyectoTramites(Idproyecto, Proyecto.P_IdGlobal, Usuario.U_RFCRepresentanteAsociado, Session["OpenId"].ToString());

                CatEmpresas Empresa = LogicaEmpresa.L_DetallesEmpresa(Proyecto.P_IdEmpresa);
                int IdRepresentante = (Int32)Session["IdRepresentanteActual"];
                CatRepresentantesLegales RepresentanteLegal = LogicaRepresentanteLegal.L_DetallesRepresentanteLegal(IdRepresentante);
                List<CatEstatusTramite> TablaEstatus = LogicaTramite.L_TablaEstatusTramite();
                CatAvanceTramites AvanceTramites = LogicaTramite.L_AvanceTramites(ListaTramites);
                CargarDatosProyecto(AvanceTramites, Proyecto, Empresa, RepresentanteLegal);

                if (ErrorBPM)
                {
                    Session["TipoAlerta"] = "Error";
                    TempData["notice"] = "La información podría no estar actualizada";
                }

                return View(ListaTramites);

            }
            catch (Exception ex)
            {
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "ProyectoTramites");
                switch (IdEstatusProyecto)
                {
                    case "CAPTURADO": TempData["notice"] = "El proyecto se registró en ENREL pero no ha sido sido enviado al gestor de trámites.";
                        return RedirectToAction("Index");
                    case "ENVIADOBPM": TempData["notice"] = "El proyecto no ha sido registrado en Gobmx.";
                        return RedirectToAction("Index");
                    case "ENVIADOGOBMX": TempData["notice"] = "El proyecto ha sido registrado en Gobmx pero ocurrió un error inesperado";
                        return RedirectToAction("Index");
                    default: TempData["notice"] = "Error inesperado, por favor consulte a un administrador de la aplicación.";
                        return RedirectToAction("Index");
                }
            }
        }

        #endregion

        #region Métodos Auxiliares:

        [HttpPost]
        public ActionResult Metodo_InsertarProyecto(CatProyectos Proyecto, string Accion, FormCollection Formulario, IEnumerable<HttpPostedFileBase> files)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
            {
                TempData["notice"] = "La sesión ha expirado.";
                return RedirectToAction("Logout", "Home");
            }
            string Validacion = "";
            string RFCUsuario = Session["RFCAsociado"].ToString();
            int IdProyecto = 0;
            string IdEstatusProyecto = "NO CAPTURADO";
            string resultado;

            try
            {
                Validacion = MetodoGeneral.ValidarFIEL(files, Proyecto.P_ClavePrivada, RFCUsuario);
                if (Validacion == "Validación exitosa")
                {
                    Proyecto.P_IdEmpresa = Usuario.U_IdEmpresa;
                    IdProyecto = 0;
                    IdProyecto = LogicaProyecto.L_InsertarProyecto(Proyecto, Usuario.U_IdUsuario);

                    IdEstatusProyecto = "NO CAPTURADO";

                    if (IdProyecto > 0)
                    {
                        Proyecto = LogicaProyecto.L_DetallesProyectos(IdProyecto);
                        LogicaProyecto.L_ActualizarProyectoSeguimiento(IdProyecto, 1);

                        IdEstatusProyecto = LogicaProyecto.L_SeleccionarProyectoSeguimiento(IdProyecto);

                        LogicaProyecto.L_ResetProyectoPregunta(IdProyecto);
                        foreach (var item in Formulario)
                        {
                            string[] Cadena = (item.ToString()).Split('_');

                            if (Cadena[0] == "Pregunta")
                            {
                                int value = Convert.ToInt32(Formulario[item.ToString()]);
                                bool respuesta = Convert.ToBoolean(value);
                                LogicaProyecto.L_ActualizarProyectoPregunta(IdProyecto, Convert.ToInt32(Cadena[1]), respuesta);
                            }
                        }

                        IdEstatusProyecto = LogicaProyecto.L_SeleccionarProyectoSeguimiento(IdProyecto);

                        if (IdEstatusProyecto == "CAPTURADO")
                        {
                            //@@ No se usa para pruebas en desarrollo @@//

                            //COMUNICACIÓN: Enviar proyecto a BPM
                            WSBPM_Nivel0.ProcessNivel0PortTypeClient CrearProyectos = new WSBPM_Nivel0.ProcessNivel0PortTypeClient();
                            CrearProyectos.InicioNivel0(IdProyecto.ToString(), Proyecto.P_Tecnologia.ToLower());
                            LogicaProyecto.L_ActualizarProyectoSeguimiento(IdProyecto, 2);
                            MetodoGeneral.RegistroDeError("Datos {IdProyecto: " + IdProyecto.ToString() + ", Tecnología: " + Proyecto.P_Tecnologia.ToLower() + "}", "Invocación Nivel 0 -");
                            MetodoGeneral.RegistroDeError(CrearProyectos.Endpoint.Address.ToString(), "Invocación Nivel 0 -");


                        }

                        Thread.Sleep(3000);

                        for (int i = 0; i < 2; i++)
                        {
                            IdEstatusProyecto = LogicaProyecto.L_SeleccionarProyectoSeguimiento(IdProyecto);
                            if (IdEstatusProyecto == "ENVIADOBPM") { break; }
                            Thread.Sleep(1000);
                        }

                        if (IdEstatusProyecto == "ENVIADOBPM")
                        {
                            //COMUNICACIÓN: Enviar proyecto a gobmx
                            MetodoGeneral.RegistroDeError("Datos {IdProyecto: " + Proyecto.P_IdGlobal + ", RFC: " + Usuario.U_RFCRepresentanteAsociado + "}", "Invocacion GOB MX");

                            resultado = MetodoGeneral.RegistrarReferenciaMacroTramite(Proyecto.P_IdGlobal, Usuario.U_RFCRepresentanteAsociado);
                            LogicaProyecto.L_ActualizarProyectoSeguimiento(IdProyecto, 3);
                            LogicaProyecto.L_ActualizarProyectoSeguimiento(IdProyecto, 4);
                        }
                       //COMUNICACIÓN: Enviar proyecto a WS de las Dependencias involucradas.
                       // Hacemos el consumo del WebService CRE
                        NotificacionIOP Servicio = new NotificacionIOP();
                        RespuestaClienteWS Respuesta = new RespuestaClienteWS();
                        //RespuestaClienteWS RespuestaCNC = new RespuestaClienteWS();
                        int IdRepresentante = (Int32)Session["IdRepresentanteActual"];
                        Respuesta = Servicio.CreNotificacion(Proyecto.P_IdGlobal, Proyecto.P_IdEmpresa, Proyecto.P_IdProyecto, IdRepresentante);
                        //RespuestaCNC = Servicio.CNCNotificacion(Proyecto.P_IdGlobal, Proyecto.P_IdEmpresa, Proyecto.P_IdProyecto, IdRepresentante); //En espera
                     
                        

                        IdEstatusProyecto = LogicaProyecto.L_SeleccionarProyectoSeguimiento(IdProyecto);

                        switch (IdEstatusProyecto)
                        {
                            case "NO CAPTURADO":
                                string Mensaje1 = "El proyecto no ha sido registrado en ENREL.";
                                MetodoGeneral.RegistroDeError(Mensaje1, "Proyectos: Insertar Proyecto");
                                TempData["notice"] = "Registro del proyecto pendiente, espere unos minutos y vuelva a consultar la aplicación";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Index");
                            case "CAPTURADO":
                                string Mensaje2 = "Proyecto registrado en la BD de ENREL, pero no se envió al BPM. IdProyecto = '" + IdProyecto + "'";
                                MetodoGeneral.RegistroDeError("Proyecto registrado en la BD de ENREL, pero no se envió al BPM.", "Proyectos: Insertar Proyecto");
                                TempData["notice"] = "Registro del proyecto pendiente, espere unos minutos y vuelva a consultar la aplicación";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Index");
                            case "ENVIADOBPM":
                                string Mensaje3 = "Proyecto registrado en la BD de ENREL, pero no se registró en GOBMX. IdProyecto = '" + IdProyecto + "'";
                                MetodoGeneral.RegistroDeError(Mensaje3, "Proyectos: Insertar Proyecto");
                                TempData["notice"] = "Registro del proyecto pendiente, espere unos minutos y vuelva a consultar la aplicación";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Index");
                            case "ENVIADOGOBMX":
                                string Mensaje4 = "Proyecto registrado en la BD de ENREL, pero no se ha registrado el estatus INICIADO. IdProyecto = '" + IdProyecto + "'";
                                MetodoGeneral.RegistroDeError(Mensaje4, "Proyectos: Insertar Proyecto");
                                TempData["notice"] = "Registro del proyecto pendiente, espere unos minutos y vuelva a consultar la aplicación";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Index");
                            case "INICIADO":
                                string Mensaje5 = "Proyecto registrado exitosamente. IdProyecto = '" + IdProyecto + "'";
                                MetodoGeneral.RegistroDeError(Mensaje5, "Proyectos: Insertar Proyecto");
                                TempData["notice"] = "El proyecto se ha registrado exitosamente.";
                                Session["TipoAlerta"] = "Correcto";
                                return RedirectToAction("Index");
                            default:
                                string Mensaje6 = "Error inesperado. IdProyecto = '" + IdProyecto + "'";
                                MetodoGeneral.RegistroDeError(Mensaje6, "Proyectos: Insertar Proyecto");
                                TempData["notice"] = "Error inesperado, por favor consulte a un administrador de la aplicación.";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        Session["TipoAlerta"] = "Error";
                        List<CatPreguntas> ListaPreguntas = LogicaProyecto.L_SeleccionarPreguntas(Proyecto.P_IdTecnologia);
                        ViewBag.ListaPreguntas = new SelectList(ListaPreguntas, "IdPregunta", "Pregunta");
                        CargarListasDesplegables(Proyecto.P_CodigoPostal, Proyecto.P_IdEntidadFederativa, Proyecto.P_IdMunicipio, Proyecto.P_IdLocalidad);
                        Proyecto.P_ClavePrivada = null;
                        ViewBag.IdTecnologia = Proyecto.P_IdTecnologia;
                        ViewBag.RFCRepresentante = Usuario.U_RFCRepresentanteAsociado;
                        return View("Insertar", Proyecto);
                    }
                }
                else
                {
                    TempData["notice"] = Validacion;
                    Session["TipoAlerta"] = "Error";
                    CargarListasDesplegables(Proyecto.P_CodigoPostal, Proyecto.P_IdEntidadFederativa, Proyecto.P_IdMunicipio, Proyecto.P_IdLocalidad);
                    Proyecto.P_ClavePrivada = "";
                    List<CatPreguntas> ListaPreguntas = LogicaProyecto.L_SeleccionarPreguntas(Proyecto.P_IdTecnologia);
                    ViewBag.ListaPreguntas = new SelectList(ListaPreguntas, "IdPregunta", "Pregunta");
                    ViewBag.IdTecnologia = Proyecto.P_IdTecnologia;
                    ViewBag.RFCRepresentante = Usuario.U_RFCRepresentanteAsociado;
                    CargarListasDesplegables(Proyecto.P_CodigoPostal, Proyecto.P_IdEntidadFederativa, Proyecto.P_IdMunicipio, Proyecto.P_IdLocalidad);
                    Proyecto.P_ClavePrivada = null;
                    return View("Insertar", Proyecto);
                }
            }
            catch (Exception ex)
            {
                Proyecto.P_ClavePrivada = null;
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Insertar Proyecto");

                switch (IdEstatusProyecto)
                {
                    case "NO CAPTURADO":
                        TempData["notice"] = "El proyecto no ha sido registrado en ENREL.";
                        MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Insertar Proyecto: Proyecto registrado en la BD de ENREL, pero no se envió al BPM.");
                        List<CatPreguntas> ListaPreguntas = LogicaProyecto.L_SeleccionarPreguntas(Proyecto.P_IdTecnologia);
                        ViewBag.ListaPreguntas = new SelectList(ListaPreguntas, "IdPregunta", "Pregunta");
                        CargarListasDesplegables(Proyecto.P_CodigoPostal, Proyecto.P_IdEntidadFederativa, Proyecto.P_IdMunicipio, Proyecto.P_IdLocalidad);
                        Proyecto.P_ClavePrivada = "";
                        ViewBag.RFCRepresentante = Usuario.U_RFCRepresentanteAsociado;
                        ViewBag.IdTecnologia = Proyecto.P_IdTecnologia;
                        return View("Insertar", Proyecto);

                    case "CAPTURADO": TempData["notice"] = "Registro del proyecto pendiente, espere unos minutos y vuelva a consultar la aplicación.";
                        MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Insertar Proyecto: Proyecto registrado en la BD de ENREL, pero no se envió al BPM");
                        return RedirectToAction("Index");
                    case "ENVIADOBPM": TempData["notice"] = "Registro del proyecto pendiente, espere unos minutos y vuelva a consultar la aplicación.";
                        MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Insertar Proyecto: Proyecto registrado en la BD de ENREL, pero no se registró en GOBMX..");
                        return RedirectToAction("Index");
                    case "ENVIADOGOBMX": TempData["notice"] = "Registro del proyecto pendiente, espere unos minutos y vuelva a consultar la aplicación.";
                        MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Insertar Proyecto: Proyecto registrado en la BD de ENREL, pero no se ha registrado el estatus INICIADO.");
                        return RedirectToAction("Index");
                    default: TempData["notice"] = "Registro del proyecto pendiente, espere unos minutos y vuelva a consultar la aplicación.";
                        MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Insertar Proyecto: Error general.");
                        return RedirectToAction("Index");
                }
            }
        }

        private void CargarDatosProyecto(CatAvanceTramites AvanceTramites, CatProyectos Proyecto, CatEmpresas Empresa, CatRepresentantesLegales Representante)
        {
            int PorcentajePositivo = 0;
            int PorcentajeNegativo = 100;

            ViewBag.DatosProyectos = JsonConvert.SerializeObject(Proyecto);
            ViewBag.DatosEmpresa = JsonConvert.SerializeObject(Empresa);
            ViewBag.DatosRepresentante = JsonConvert.SerializeObject(Representante);
            ViewBag.RFCRepresentante = Representante.RL_RFC;

            ViewBag.NombreProyecto = Proyecto.P_NombreProyecto;
            ViewBag.FechaInicio = Proyecto.P_FechaRegistro;
            ViewBag.DiasAgregados = Proyecto.P_DiasAgregados;

            ViewBag.ImagenMapaTecnologia = ConfigurationManager.AppSettings["Img_MapaTecnologia"].ToString() + Proyecto.P_Tecnologia + ".jpg";
            ViewBag.NombreTecnologia = Proyecto.P_Tecnologia;
            ViewBag.IdProyecto = Proyecto.P_IdProyecto.ToString();
            ViewBag.IdGlobal = Proyecto.P_IdGlobal;

            ViewBag.TramitesFinalizados = AvanceTramites.T_TramitesFinalizados;
            ViewBag.TramitesTotales = AvanceTramites.T_TramitesTotales;
            try
            {
                PorcentajePositivo = (AvanceTramites.T_TramitesFinalizados * 100) / AvanceTramites.T_TramitesTotales;
                PorcentajeNegativo = 100 - PorcentajePositivo;
            }
            catch (Exception ex)
            {
            }


            ViewBag.PorcentajePositivo = PorcentajePositivo;
            ViewBag.PorcentajeNegativo = PorcentajeNegativo;

            LogicaProyecto.L_ActualizarAvanceProyecto(Proyecto.P_IdProyecto, PorcentajePositivo);
            double Finalizados = ((Convert.ToDouble(ViewBag.TramitesFinalizados)));
            double totales = ((Convert.ToDouble(ViewBag.TramitesTotales)));
            double Porcentaje = (Finalizados / totales * 100);
            string Val = string.Format("{0:0}", Porcentaje);
            ViewBag.Porcentaje = Val;
        }

        [HttpGet]
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

                //Enviar correo a dependencia y RL:
                int IdProyectoRecibido = Convert.ToInt32(IdProyecto);
                NotificacionInicioTramite DatosNotificacion = new NotificacionInicioTramite();
                DatosNotificacion = LogicaProyecto.L_SeleccionarDatosNotificacionInicioTramite(IdProyecto, HomoclaveEnviada, RFCRL);

                //Primer Correo (Dependencia): 
                MailMessage email = new MailMessage("enrel@energia.gob.mx", DatosNotificacion.CorreoResponsable);
                email.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoAdministrador"].ToString()));
                email.Subject = "Notificación de inicio de trámite desde ENREL";

                //Primer Correo - Obtener la plantilla en HTML:
                string ContenidoCorreo = "";
                string path = ConfigurationManager.AppSettings["Html_NotificacionInicioTramiteDependencia"].ToString();
                ContenidoCorreo = System.IO.File.ReadAllText(path);

                //Primer Correo - Datos del correo
                ContenidoCorreo = ContenidoCorreo.Replace("#IdGlobalMacro#", DatosNotificacion.IdGlobal);
                ContenidoCorreo = ContenidoCorreo.Replace("#FechaHora#", FechaHora);
                ContenidoCorreo = ContenidoCorreo.Replace("#Tecnologia#", DatosNotificacion.Tecnologia);
                ContenidoCorreo = ContenidoCorreo.Replace("#Homoclave#", DatosNotificacion.HomoclaveGeneral);
                ContenidoCorreo = ContenidoCorreo.Replace("#Dependencia#", DatosNotificacion.Dependencia);
                ContenidoCorreo = ContenidoCorreo.Replace("#RFCRL#", RFCRL);

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(ContenidoCorreo, null, "text/html");

                //Primer Correo - Obtener imágenes:

                LinkedResource Logotipo_SENER = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_SENER"].ToString());
                Logotipo_SENER.ContentId = "Logotipo_SENER";
                htmlView.LinkedResources.Add(Logotipo_SENER);

                LinkedResource Logotipo_MEXICO = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_MEXICO"].ToString());
                Logotipo_MEXICO.ContentId = "Logotipo_MEXICO";
                htmlView.LinkedResources.Add(Logotipo_MEXICO);

                email.AlternateViews.Add(htmlView);
                email.IsBodyHtml = true;
                email.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "172.16.70.110";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Send(email);
                email.Dispose();

                //Segundo Correo (RL): 
                email = new MailMessage("enrel@energia.gob.mx", DatosNotificacion.CorreoRL);
                email.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoAdministrador"].ToString()));
                email.Subject = "Notificación de inicio de trámite desde ENREL";

                //Segundo Correo - Obtener la plantilla en HTML:
                ContenidoCorreo = "";
                path = ConfigurationManager.AppSettings["Html_NotificacionInicioTramiteRL"].ToString();
                ContenidoCorreo = System.IO.File.ReadAllText(path);

                //Segundo Correo - Datos del correo
                ContenidoCorreo = ContenidoCorreo.Replace("#IdGlobalMacro#", DatosNotificacion.IdGlobal);
                ContenidoCorreo = ContenidoCorreo.Replace("#FechaHora#", FechaHora);
                ContenidoCorreo = ContenidoCorreo.Replace("#Tecnologia#", DatosNotificacion.Tecnologia);
                ContenidoCorreo = ContenidoCorreo.Replace("#Homoclave#", DatosNotificacion.HomoclaveGeneral);
                ContenidoCorreo = ContenidoCorreo.Replace("#Dependencia#", DatosNotificacion.Dependencia);
                ContenidoCorreo = ContenidoCorreo.Replace("#NombreRL#", DatosNotificacion.NombreRL);

                htmlView = AlternateView.CreateAlternateViewFromString(ContenidoCorreo, null, "text/html");

                //Segundo Correo - Obtener imágenes:

                Logotipo_SENER = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_SENER"].ToString());
                Logotipo_SENER.ContentId = "Logotipo_SENER";
                htmlView.LinkedResources.Add(Logotipo_SENER);

                Logotipo_MEXICO = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_MEXICO"].ToString());
                Logotipo_MEXICO.ContentId = "Logotipo_MEXICO";
                htmlView.LinkedResources.Add(Logotipo_MEXICO);

                email.AlternateViews.Add(htmlView);
                email.IsBodyHtml = true;
                email.Priority = MailPriority.High;

                smtp = new SmtpClient();
                smtp.Host = "172.16.70.110";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Send(email);
                email.Dispose();

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

        private void CargarListasDesplegables(string CodigoPostal, int? IdEntidadFederativa, int? IdMunicipio, int? IdLocalidad)
        {
            ViewBag.EntidadesFederativas = MetodoGeneral.CargarEntidadesFederativas(CodigoPostal);
            ViewBag.Municipios = MetodoGeneral.CargarMunicipios(CodigoPostal, IdEntidadFederativa, IdMunicipio, IdLocalidad);
            ViewBag.Localidades = MetodoGeneral.CargarLocalidades(CodigoPostal, IdEntidadFederativa, IdMunicipio, IdLocalidad);
            ViewBag.TiposAsentamiento = MetodoGeneral.CargarTiposAsentamiento();
            ViewBag.TiposVialidad = MetodoGeneral.CargarTiposVialidad();
            ViewBag.Tecnologias = MetodoGeneral.CargarTecnologias();
        }

        [HttpPost]
        public ActionResult ActualizarPreguntas(FormCollection Formulario)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
            {
                TempData["notice"] = "La sesión ha expirado.";
                return RedirectToAction("Logout", "Home");
            }


            try
            {

                int IdProyecto = Convert.ToInt32(Formulario.GetValue("IdProyecto"));
                if (IdProyecto > 0)
                {
                    foreach (var item in Formulario)
                    {
                        string[] Cadena = (item.ToString()).Split('_');

                        if (Cadena[0] == "Pregunta")
                        {
                            int value = Convert.ToInt32(Formulario[item.ToString()]);
                            bool respuesta = Convert.ToBoolean(value);
                            LogicaProyecto.L_ActualizarProyectoPregunta(IdProyecto, Convert.ToInt32(Cadena[1]), respuesta);
                        }
                    }
                    Session["TipoAlerta"] = "Correcto";
                    TempData["notice"] = "Las respuestas han sido actualizadas.";
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["TipoAlerta"] = "Error";
                    TempData["notice"] = "No se ha podido identificar el proyecto en el que se cambiarán las respuestas.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Proyectos: Actualizar Proyecto Preguntas");
                TempData["notice"] = "No se ha podido comprobar que las respuestas se hayan actualizado con éxito.";
                return RedirectToAction("Index");
            }
        }


        #endregion

    }
}