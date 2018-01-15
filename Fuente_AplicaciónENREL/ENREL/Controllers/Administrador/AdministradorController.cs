using ENREL.Models.Usuarios;
using ENREL.Models.RepresentantesLegales;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ENREL.Models.Empresas;
using System.Net.Mail;
using System.IO;
using ENREL.Models.Home;
using System.Configuration;
using ENREL.Controllers.Auxiliar;
using System.Threading.Tasks;

namespace ENREL.Controllers.Administrador
{
    public class AdministradorController : Controller
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        string FechaDocumento = "01/01/0001";

        #endregion

        #region Páginas:

        //Inicio: 

        public ActionResult Index()
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                return View();
            }
            else { return RedirectToAction("Logout", "Home"); }
        }

        //Validación de representantes legales:

        public ActionResult GestionarSolicitudesRegistro()
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    LogicaRepresentantesLegales LogicaRepresentantesLegales = new LogicaRepresentantesLegales();
                    List<CatRepresentantesLegales> Representantes = LogicaRepresentantesLegales.L_SeleccionarRepresentantesLegalesPorValidar();

                    ViewBag.ListaRepresentantesPorValidar = Representantes;
                    return View();
                }
                catch (Exception ex)
                {
                    TempData["notice"] = "Se produjo un error en la consulta";
                    Session["TipoAlerta"] = "Error";
                    return View("GestionarSolicitudesRegistro");
                }
            }
            else { return RedirectToAction("Logout", "Home"); }

        }

        public ActionResult ValidarSolicitud(int IdRepresentante)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    LogicaUsuarios LogicaUsuarios = new LogicaUsuarios();
                    LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
                    LogicaRepresentantesLegales LogicaRepresentante = new LogicaRepresentantesLegales(); 

                    CatRepresentantesLegales Representante = LogicaRepresentante.L_DetallesRepresentanteLegal(IdRepresentante);
                    CatEmpresas Empresa = LogicaEmpresa.L_DetallesEmpresa(Representante.RL_IdEmpresa);
                    CatUsuarios UsuarioRepresentante = LogicaUsuarios.L_DetallesUsuarioPorRepresentante(IdRepresentante);

                    ViewBag.DatosEmpresa = Empresa;

                    string ruta = @"C:\\inetpub\\RepositorioVER\\Representantes\\" + Representante.RL_IdRepresentanteLegal.ToString();
                    DirectoryInfo di = new DirectoryInfo(ruta);

                    DateTime FechaPoderNotarial = DateTime.Parse("01/01/1900");
                    DateTime FechaCedulaRFC = DateTime.Parse("01/01/1900");
                    DateTime FechaActaConstitutiva = DateTime.Parse("01/01/1900");
                    DateTime FechaIdentificacionOficial = DateTime.Parse("01/01/1900");


                    foreach (var fi in di.GetFiles())
                    {
                        try
                        {
                            string[] Varibales = fi.Name.Split('_');
                            string TipoDocumento = Varibales[0];
                            try
                            {
                                FechaDocumento = Varibales[2].Substring(0, 19).Replace('-', '/').Replace('.', ':');
                            }
                            catch (Exception ex) { }
                            DateTime Fecha = DateTime.Parse(FechaDocumento);

                            switch (TipoDocumento)
                            {
                                case "PoderNotarial":
                                    if (FechaPoderNotarial < Fecha)
                                    {
                                        FechaPoderNotarial = Fecha;
                                    }
                                    break;
                                case "CedulaRFC":
                                    if (FechaCedulaRFC < Fecha)
                                    {
                                        FechaCedulaRFC = Fecha;
                                    }
                                    break;
                                case "ActaConstitutiva":
                                    if (FechaActaConstitutiva < Fecha)
                                    {
                                        FechaActaConstitutiva = Fecha;
                                    }
                                    break;
                                case "NumIdenOficial":
                                    if (FechaIdentificacionOficial < Fecha)
                                    {
                                        FechaIdentificacionOficial = Fecha;
                                    }
                                    break;
                                default:
                                    break;

                            }

                        }
                        catch (Exception exc)
                        {

                        }
                    }    
                    ViewBag.PoderNotarial = ruta + "\\PoderNotarial_PDF_" + FechaPoderNotarial.ToString("yyyy-MM-dd HH.mm.ss") + ".pdf";
                    ViewBag.CedulaRFC = ruta + "\\CedulaRFC_PDF_" + FechaCedulaRFC.ToString("yyyy-MM-dd HH.mm.ss") + ".pdf";
                    ViewBag.ActaConstitutiva = ruta + "\\ActaConstitutiva_PDF_" + FechaActaConstitutiva.ToString("yyyy-MM-dd HH.mm.ss") + ".pdf";
                    ViewBag.IdentificacionOficial = ruta + "\\NumIdenOficial_PDF_" + FechaIdentificacionOficial.ToString("yyyy-MM-dd HH.mm.ss") + ".pdf";
                    ViewBag.DatosRepresentante = Representante;

                    if (Empresa.E_RFC.Length == 12)
                    {
                        ViewBag.MostrarCarta = true;
                    }
                    else
                    {
                        ViewBag.MostrarCarta = false;
                    }

                    return View();
                    
                }
                catch (Exception ex)
                {
                    TempData["notice"] = "Se produjo un error en la consulta";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("GestionarSolicitudesRegistro");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }

        }

        //Gestión de usuarios SENER: 

        public ActionResult GestionUsuariosSENER(FormCollection Formulario)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                int? IdTipoUsuario = null;
                int? IdEstatusUsuario = null;
                try
                {
                    if (Formulario.Count > 0)
                    {
                        try { IdTipoUsuario = Convert.ToInt32(Formulario[0]);  }
                        catch { }
                        try { IdEstatusUsuario = Convert.ToInt32(Formulario[1]); }
                        catch { }
                    }
                    CargarListasDesplegables(IdTipoUsuario, IdEstatusUsuario);
                    LogicaUsuarios LogicaUsuarios = new LogicaUsuarios();
                    List<CatUsuarios> Usuarios = LogicaUsuarios.L_SeleccionarUsuariosSENER(IdTipoUsuario, IdEstatusUsuario);

                    return View(Usuarios);
                }
                catch (Exception ex)
                {
                    List<CatUsuarios> Usuarios = new List<CatUsuarios>();

                    TempData["notice"] = "Se produjo un error en la consulta";
                    Session["TipoAlerta"] = "Error";
                    return View(Usuarios);
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }

        }

        public ActionResult ModificarUsuarioGeneral(int IdUsuarioModificar, string Nombre, string Correo, int TipoUsuario)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    LogicaUsuarios LogicaTiposUsuario = new LogicaUsuarios();
                    List<CatTiposUsuario> TiposUsuario = LogicaTiposUsuario.SeleccionarTiposUsuario();

                    //Se elimina tipo de usuario inversionista
                    TiposUsuario.RemoveAt(1);
                    TiposUsuario.RemoveAt(0);

                    ViewBag.TiposUsuario = new SelectList(TiposUsuario, "IdTipoUsuario", "TipoUsuario");

                    ViewBag.Nombre = Nombre;
                    ViewBag.Correo = Correo;
                    ViewBag.TipoUsuario = TipoUsuario;
                    //LogicaUsuario LogicaUsuario = new LogicaUsuario();
                    CatUsuarios UsuarioAModificar = new CatUsuarios();
                    UsuarioAModificar.U_Nombre = Nombre;
                    UsuarioAModificar.U_CorreoElectronico = Correo;
                    return View();
                }
                catch (Exception ex)
                {

                    TempData["notice"] = "Se produjo un error en la consulta";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("MenuAdministrador");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }

        }

        public ActionResult EliminarUsuario(int? id)
        {

            if (id != null)
            {
                int IdEmpresa = Convert.ToInt32(id);
                ViewBag.IdUsuario = id;
                return View();
            }
            else
            {
                TempData["notice"] = "Se produjo un error";
                Session["TipoAlerta"] = "Error";
                return View("EliminaUsuario");
            }
        }

        //Gestión de usuarios SENER: 

        public ActionResult GestionDiasInhabiles(FormCollection Formulario)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                return RedirectToAction("Index", "DiasInhabiles");
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }

        }

        #endregion

        #region Métodos Auxiliares:

        public ActionResult AceptarResprepresentanteLegal(int IdEmpresa, int IdRepresentante)
        {
            LogicaUsuarios LogicaUsuarios = new LogicaUsuarios();
            LogicaRepresentantesLegales LogicaRepresentantesLegales = new LogicaRepresentantesLegales();
            LogicaHome LogicaHome = new LogicaHome();

            
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 4)
                {
                    TempData["notice"] = "La sesión ha experiado.";
                    return RedirectToAction("Logout", "Home");
                }
                else
                {
                    CatUsuarios UsuarioRepresentante = LogicaUsuarios.L_DetallesUsuarioPorRepresentante(IdRepresentante);
                    UsuarioRepresentante.U_Activo = true;
                    CatRepresentantesLegales Representante = LogicaRepresentantesLegales.L_DetallesRepresentanteLegal(IdRepresentante);

                    string CorreoRepresentante = Representante.RL_CorreoElectronico;
                    Representante.RL_FechaRegistro = DateTime.Now.ToString();
                    Representante.RL_Observaciones = "Validado";
                    Representante.RL_IdEstatusSolicitudRepresentante = 3;
                    Representante.RL_Activo = true;

                    Guid DatoAleatorio = new Guid();
                    DatoAleatorio = Guid.NewGuid();

                    string clave = "C";
                    clave = clave + DatoAleatorio.ToString().Substring(0, 8);
                    clave = clave + DatoAleatorio.ToString().Substring(9, 4);

                    string ContraseniaUsuarioRepresentante = MetodoGeneral.EncriptarPassword(clave);

                    LogicaHome.L_ValidarRegistro(Representante, ContraseniaUsuarioRepresentante, clave);

                    new Task(() =>
                    {
                        try
                        {
                            MailMessage email = new MailMessage("enrel@energia.gob.mx", CorreoRepresentante);
                            email.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoAdministrador"].ToString()));

                            email.Subject = "Solicitud ENREL aprobada";

                            //Obtener la plantilla en HTML:
                            string path = ConfigurationManager.AppSettings["Html_AceptarSolicitud"].ToString();
                            string ContenidoCorreo = System.IO.File.ReadAllText(path);

                            string NombreRepresentanteLegal = Representante.RL_Nombre + " " + Representante.RL_PrimerApellido + " " + Representante.RL_SegundoApellido;
                            ContenidoCorreo = ContenidoCorreo.Replace("#NombreRepresentanteLegal#", NombreRepresentanteLegal);
                            ContenidoCorreo = ContenidoCorreo.Replace("#UsuarioAcreditado#", UsuarioRepresentante.U_Nombre);
                            ContenidoCorreo = ContenidoCorreo.Replace("#Contraseña#", clave);

                            string Enlace = ConfigurationManager.AppSettings["EnlaceENREL"].ToString();
                            ContenidoCorreo = ContenidoCorreo.Replace("#EnlaceENREL#", Enlace);
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
                        }
                        catch(Exception ex)
                        {
                            MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Quinto Paso - Envío de correo");
                        }
                        
                    }).Start();

                    TempData["notice"] = "Se validó el registro";
                    Session["TipoAlerta"] = "Correcto";
                    return RedirectToAction("GestionarSolicitudesRegistro", "Administrador");
                    
                }


            }
            catch (Exception ex)
            {
                
                CatRepresentantesLegales Representante = new CatRepresentantesLegales();
                Representante = LogicaRepresentantesLegales.L_DetallesRepresentanteLegal(IdRepresentante);

                Representante.RL_FechaRegistro = DateTime.Now.ToString();
                Representante.RL_IdEstatusSolicitudRepresentante = 1;
                Representante.RL_Activo = true;
                Representante.RL_Observaciones = "Error, es necesario volver a validar";
                LogicaRepresentantesLegales.L_ActualizarRepresentanteLegal(Representante);
                LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
                CatEmpresas Empresa = LogicaEmpresa.L_DetallesEmpresa(Representante.RL_IdEmpresa);
                ViewBag.DatosEmpresa = Empresa;
                ViewBag.DatosRepresentante = Representante;

                string ruta = @"C:\inetpub\RepositorioVER\" + Representante.RL_IdRepresentanteLegal.ToString() + "-";
                ViewBag.PoderNotarial = ruta + "PoderNotarial.pdf";
                ViewBag.CedulaRFC = ruta + "CedulaRFC.pdf";
                ViewBag.ActaConstitutiva = ruta + "ActaConstitutiva.pdf";
                ViewBag.IdentificacionOficial = ruta + "Identificacion.pdf";
                ViewBag.DatosRepresentante = Representante;
                
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: Insertar");
                return View("ValidarSolicitud", new { IdRepresentante = IdRepresentante });
            }

        }

        public ActionResult RechazarRepresentanteLegal(FormCollection form)
        {
            LogicaUsuarios LogicaUsuarios = new LogicaUsuarios();
            int IdRepresentante = Int32.Parse(form["IdRepresentante"]);
            string Observaciones = form["Observaciones"];
            
            LogicaRepresentantesLegales LogicaRepresentantesLegales = new LogicaRepresentantesLegales();
            CatRepresentantesLegales Representante = new CatRepresentantesLegales();

            Representante = LogicaRepresentantesLegales.L_DetallesRepresentanteLegal(IdRepresentante);
            string CorreoRepresentante = Representante.RL_CorreoElectronico;
            Representante.RL_Observaciones = Observaciones;
            Representante.RL_IdEstatusSolicitudRepresentante = 2;
            Representante.RL_Activo = true;
            LogicaRepresentantesLegales.L_ActualizarRepresentanteLegal(Representante);

            try
            {
                new Task(() =>
                    {
                        try
                        {
                            MailMessage email = new MailMessage("enrel@energia.gob.mx", CorreoRepresentante);
                            email.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoAdministrador"].ToString()));

                            email.Subject = "Solicitud ENREL no aprobada";


                            //Obtener la plantilla en HTML:
                            string path = ConfigurationManager.AppSettings["Html_RechazarSolicitud"].ToString();
                            string ContenidoCorreo = System.IO.File.ReadAllText(path);

                            string NombreRepresentanteLegal = Representante.RL_Nombre + " " + Representante.RL_PrimerApellido + " " + Representante.RL_SegundoApellido;
                            ContenidoCorreo = ContenidoCorreo.Replace("#NombreRL#", NombreRepresentanteLegal);
                            ContenidoCorreo = ContenidoCorreo.Replace("#Observaciones#", Observaciones);

                            string Enlace = ConfigurationManager.AppSettings["EnlaceENREL"].ToString();
                            ContenidoCorreo = ContenidoCorreo.Replace("#EnlaceENREL#", Enlace);

                            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(ContenidoCorreo, null, "text/html");


                            //Obtener imágenes:
                            LinkedResource Logotipo_SENER = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_SENER"].ToString());
                            Logotipo_SENER.ContentId = "Logotipo_SENER";
                            htmlView.LinkedResources.Add(Logotipo_SENER);

                            LinkedResource Logotipo_MEXICO = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_MEXICO"].ToString());
                            Logotipo_MEXICO.ContentId = "Logotipo_MEXICO";
                            htmlView.LinkedResources.Add(Logotipo_MEXICO);



                            email.AlternateViews.Add(htmlView);
                            //email.Body = ContenidoCorreo;
                            email.IsBodyHtml = true;
                            email.Priority = MailPriority.High;

                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "172.16.70.110";
                            smtp.Port = 25;
                            smtp.EnableSsl = false;
                            smtp.UseDefaultCredentials = false;
                            smtp.Send(email);
                            email.Dispose();
                        }
                        catch(Exception ex)
                        {
                            MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Quinto Paso - Envío de correo");
                        }
                        
                    }).Start();
                return RedirectToAction("GestionarSolicitudesRegistro", "Administrador");
            }
            catch (Exception ex)
            {
                Representante.RL_Observaciones = "Ha ocurrido un error en el servicio y no se pudo enviar el correo con estatus rechazado.";
                Representante.RL_IdEstatusSolicitudRepresentante = 1;
                LogicaRepresentantesLegales.L_ActualizarRepresentanteLegal(Representante);
                LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
                CatEmpresas Empresa = LogicaEmpresa.L_DetallesEmpresa(Representante.RL_IdEmpresa);
                ViewBag.DatosEmpresa = Empresa;
                ViewBag.DatosRepresentante = Representante;
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: Insertar");
                return RedirectToAction("GestionarSolicitudesRegistro", "Administrador");
            }
        }

        public FileStreamResult GetPDF(string nombrepdf)
        {
            try
            {
                FileStream fs = new FileStream(nombrepdf, FileMode.Open, FileAccess.Read);
                return File(fs, "application/pdf");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CargarListasDesplegables(int? IdTipoUsuario, int? IdEstatusUsuario)
        {
            LogicaUsuarios LogicaUsuario = new LogicaUsuarios();

            List<CatTiposUsuario> ListaTiposUsuario = LogicaUsuario.SeleccionarTiposUsuario();
            ViewBag.TiposUsuarios = new SelectList(ListaTiposUsuario, "IdTipoUsuario", "TipoUsuario", IdTipoUsuario);

            List<CatEstatusUsuario> ListaEstatusUsuario = LogicaUsuario.SeleccionarTiposEstatusUsuario();
            ViewBag.TiposEstatusUsuarios = new SelectList(ListaEstatusUsuario, "IdEstatusUsuario", "EstatusUsuario", IdEstatusUsuario);

        }


        #endregion

    }
}