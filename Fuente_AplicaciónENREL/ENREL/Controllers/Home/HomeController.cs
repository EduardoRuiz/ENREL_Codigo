using ENREL.Controllers.Auxiliar;
using ENREL.Models.Proyectos;
using ENREL.Models.Usuarios;
using ENREL.Models.RepresentantesLegales;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ENREL.Controllers.Home
{
    public class HomeController : Controller
    {
        #region Variables:

        LogicaUsuarios LogicaUsuarios = new LogicaUsuarios();
        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        LogicaRepresentantesLegales LogicaRepresentanteLegal = new LogicaRepresentantesLegales();

        #endregion

        #region Páginas:

        // Página de inicio de la aplicación
        public ActionResult Index()
        {
            try
            {

                if (Session.Keys.Count > 1 && Session["TipoAlerta"] != null)
                {
                    CatUsuarios UsuarioEntrante = (CatUsuarios)Session["Usuario"];
                    if (UsuarioEntrante != null)
                    {
                        switch (UsuarioEntrante.U_IdTipoUsuario)
                        {
                            case 1:
                                Session["TipoUsuario"] = "UsuarioOperativo";
                                Session["IdEmpresa"] = UsuarioEntrante.U_IdEmpresa;
                                Session["NombreUsuario"] = UsuarioEntrante.U_Nombre;
                                Session["IdUsuario"] = UsuarioEntrante.U_IdUsuario;
                                LogicaUsuarios.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, true);
                                return RedirectToAction("InicioInversionista", "Home");
                            case 2:
                                CatRepresentantesLegales RepresentanteAsociado = new CatRepresentantesLegales();
                                RepresentanteAsociado = LogicaRepresentanteLegal.L_DetallesRepresentanteLegal(UsuarioEntrante.U_IdRepresentanteAsociado);
                                Session["RFCAsociado"] = RepresentanteAsociado.RL_RFC;
                                Session["TipoUsuario"] = "Inversionista";
                                Session["IdEmpresa"] = UsuarioEntrante.U_IdEmpresa;
                                Session["NombreUsuario"] = UsuarioEntrante.U_Nombre;
                                Session["IdUsuario"] = UsuarioEntrante.U_IdUsuario;
                                Session["IdRepresentanteActual"] = UsuarioEntrante.U_IdRepresentanteAsociado;
                                LogicaUsuarios.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, true);
                                return RedirectToAction("InicioInversionista", "Home");
                            case 3:
                                Session["TipoUsuario"] = "Consultor";
                                Session["IdUsuario"] = UsuarioEntrante.U_IdUsuario;
                                Session["NombreUsuario"] = UsuarioEntrante.U_Nombre;
                                LogicaUsuarios.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, true);
                                return RedirectToAction("Index", "Consultor");
                            case 4:
                                Session["TipoUsuario"] = "Administrador";
                                Session["IdUsuario"] = UsuarioEntrante.U_IdUsuario;
                                Session["NombreUsuario"] = UsuarioEntrante.U_Nombre;
                                LogicaUsuarios.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, true);
                                return RedirectToAction("InicioAdministrador", "Home");
                            default:
                                //Session["TipoUsuario"] = null;
                                Session["IdUsuario"] = 0;
                                Session["TipoUsuario"] = "No disponible";
                                TempData["notice"] = "Este tipo de usuarios no puede ingresar a esta plataforma.";
                                Session["TipoAlerta"] = "Error";
                                return View("Index", "Home");
                        }
                    }
                }

                Session["TipoAlerta"] = Session["TipoAlerta"];
                TempData["notice"] = TempData["notice"];
                Session["MensajePrivacidad"] = ConfigurationManager.AppSettings["MensjaePrivacidad"].ToString();
                return View();
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                Session["MensajePrivacidad"] = ConfigurationManager.AppSettings["MensjaePrivacidad"].ToString();
                MetodoGeneral.RegistroDeError(ex.Message, "Home: Inicio");
                return View("Index", "Home");
            }

        }

        public ActionResult DescripcionAplicacion()
        {
            return View();
        }

        public ActionResult GuiaDeUso()
        {
            return View();
        }

        public ActionResult SolicitudCambioDeContraseña()
        {
            return View();
        }

        public ActionResult InicioInversionista()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario < 1 || Usuario.U_IdTipoUsuario > 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                Session["MensajePrivacidad"] = ConfigurationManager.AppSettings["MensjaePrivacidad"].ToString();
                LogicaProyectos LogicaProyectos = new LogicaProyectos();
                List<CatProyectos> ListaProyectos = new List<CatProyectos>();

                ListaProyectos = LogicaProyectos.L_SeleccionarProyectosPorEmpresa(Usuario.U_IdEmpresa);
                return View(ListaProyectos);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Home: InicioInversionista");
                return View("Index", "Home");
            }

        }

        public ActionResult InicioAdministrador()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 4)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Home: InicioAdministrador");
                return View("Index", "Home");
            }

        }

        public ActionResult InicioConsultor()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 3)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Home: InicioConsultor");
                return View("Index", "Home");
            }

        }

        #endregion

        #region Métodos Auxiliares:

        //Valida que el usuario exista en la BD y lo redirige a una de las pantallas de inicio
        [HttpPost]
        public ActionResult Ingresar(CatUsuarios Usuario)
        {
            Session["TipoAlerta"] = "";
            try
            {
                if (LogicaUsuarios.L_ValidarUsuario(Usuario.U_Nombre, Usuario.U_Password))
                {
                    CatUsuarios UsuarioEntrante = (CatUsuarios)Session["Usuario"];
                    if (UsuarioEntrante.U_Ingreso == false)
                    {
                        switch (UsuarioEntrante.U_IdTipoUsuario)
                        {
                            case 1:
                                Session["TipoUsuario"] = "UsuarioOperativo";
                                Session["IdEmpresa"] = UsuarioEntrante.U_IdEmpresa;
                                Session["NombreUsuario"] = UsuarioEntrante.U_Nombre;
                                Session["IdUsuario"] = UsuarioEntrante.U_IdUsuario;
                                LogicaUsuarios.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, true);
                                return RedirectToAction("InicioInversionista", "Home");
                            case 2:
                                CatRepresentantesLegales RepresentanteAsociado = new CatRepresentantesLegales();
                                RepresentanteAsociado = LogicaRepresentanteLegal.L_DetallesRepresentanteLegal(UsuarioEntrante.U_IdRepresentanteAsociado);
                                Session["RFCAsociado"] = RepresentanteAsociado.RL_RFC;
                                Session["TipoUsuario"] = "Inversionista";
                                Session["IdEmpresa"] = UsuarioEntrante.U_IdEmpresa;
                                Session["NombreUsuario"] = UsuarioEntrante.U_Nombre;
                                Session["IdUsuario"] = UsuarioEntrante.U_IdUsuario;
                                Session["IdRepresentanteActual"] = UsuarioEntrante.U_IdRepresentanteAsociado;
                                LogicaUsuarios.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, true);
                                return RedirectToAction("InicioInversionista", "Home");
                            case 3:
                                Session["TipoUsuario"] = "Consultor";
                                Session["IdUsuario"] = UsuarioEntrante.U_IdUsuario;
                                Session["NombreUsuario"] = UsuarioEntrante.U_Nombre;
                                LogicaUsuarios.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, true);
                                return RedirectToAction("Index", "Consultor");
                            case 4:
                                Session["TipoUsuario"] = "Administrador";
                                Session["IdUsuario"] = UsuarioEntrante.U_IdUsuario;
                                Session["NombreUsuario"] = UsuarioEntrante.U_Nombre;
                                LogicaUsuarios.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, true);
                                return RedirectToAction("InicioAdministrador", "Home");
                            default:
                                Session.Clear();
                                UsuarioEntrante = null;
                                Session["TipoAlerta"] = "Error";
                                TempData["notice"] = "Tipo de usuario no válido.";
                                return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        Session.Clear();
                        //FinalizarSesion();
                        Session["TipoAlerta"] = "Error";
                        TempData["notice"] = "Ya existe una sesión con este usuario.";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    //FinalizarSesion();
                    Session.Clear();
                    Session["TipoAlerta"] = "Error";
                    TempData["notice"] = "No existe un usuario con este nombre y contraseña.";
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Home: Ingresar");
                return View("Index", "Home");
            }
        }

        private void FinalizarSesion()
        {
            Session.Clear();

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                var c = new HttpCookie("ASP.NET_SessionId");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);

            }
            if (Request.Cookies["__RequestVerificationToken"] != null)
            {
                var d = new HttpCookie("__RequestVerificationToken");
                d.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(d);
            }
        }

        //Borra todas las variables de sesión almacenadas y redirige a la página de inicio de la aplicación
        public ActionResult Logout()
        {
            CatUsuarios UsuarioEntrante = (CatUsuarios)Session["Usuario"];
            if (UsuarioEntrante != null)
            {
                LogicaUsuarios.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, false);
            }

            FinalizarSesion();

            return RedirectToAction("Index", "Home");
        }

        //Envía liga para el cambio de contraseña 
        public ActionResult EnviarCambioDeContraseña(FormCollection Formulario)
        {
            string NombreUsuario;

            try
            {
                if (Formulario.Count > 0)
                {
                    NombreUsuario = Formulario[1].ToString();
                    Guid ClaveReset = new Guid();
                    ClaveReset = Guid.NewGuid();


                    string correoelectronico = LogicaUsuarios.L_SolicitarCambioContraseña(NombreUsuario, ClaveReset.ToString());
                    if (correoelectronico != "")
                    {
                        MailMessage email = new MailMessage("enrel@energia.gob.mx", correoelectronico);
                        email.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoAdministrador"].ToString()));
                        email.Subject = "Asunto ( Solicitud de cambio de contraseña " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";

                        //Obtener la plantilla en HTML:

                        string path = ConfigurationManager.AppSettings["Html_SolicitarCambioContraseña"].ToString();
                        string ContenidoCorreo = System.IO.File.ReadAllText(path);

                        string Enlace = ConfigurationManager.AppSettings["Enlace_SolicitarCambioContraseña"].ToString() + ClaveReset.ToString();
                        ContenidoCorreo = ContenidoCorreo.Replace("#EnlaceReposicionContrasenia#", Enlace);
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(ContenidoCorreo, null, "text/html");


                        //Obtener imágenes:

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
                        TempData["notice"] = "Se ha enviado un correo con el enlace para el cambio de contraseña.";
                        Session["TipoAlerta"] = "Correcto";
                        return RedirectToAction("SolicitudCambioDeContraseña", "Home");
                    }
                    else
                    {
                        TempData["notice"] = "Los datos registrados para este usuario son incorrectos.";
                        Session["TipoAlerta"] = "Error";
                        return RedirectToAction("SolicitudCambioDeContraseña", "Home");
                    }
                }
                else
                {
                    TempData["notice"] = "El usuario no se encuentra en el sistema.";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("SolicitudCambioDeContraseña", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Home: SolicitudDeContraseña");
                return RedirectToAction("SolicitudCambioDeContraseña", "Home");
            }



        }



        #endregion

    }
}