using ENREL.Controllers.Auxiliar;
using ENREL.Models.Empresas;
using ENREL.Models.RepresentantesLegales;
using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Firma:
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.X509;
using SysX509 = System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
using System.Text;

namespace ENREL.Controllers.RepresentantesLegales
{
    public class RepresentantesLegalesController : Controller
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        LogicaRepresentantesLegales LogicaRepresentanteLegal = new LogicaRepresentantesLegales();

        #endregion

        #region Páginas

        public ActionResult Index()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                List<CatRepresentantesLegales> ListaRepresentantesLegales = new List<CatRepresentantesLegales>();

                ListaRepresentantesLegales = LogicaRepresentanteLegal.L_SeleccionarRepresentantesLegalesPorEmpresa(Usuario.U_IdEmpresa);
                ViewBag.CantidadRL = ListaRepresentantesLegales.Count();
                return View(ListaRepresentantesLegales);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: Index");
                return View("Index", "Home");
            }

        }

        public ActionResult Insertar()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                CargarListasDesplegables(null, null, null, null);
                ViewBag.RFCRepresentante = Usuario.U_RFCRepresentanteAsociado;
                return View();
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: Insertar");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Actualizar(int IdRepresentanteLegal)
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null && Usuario.U_IdTipoUsuario == 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                ViewBag.ActaConstitutiva = @"C:\inetpub\RepositorioVER\" + IdRepresentanteLegal.ToString() + "-ActaConstitutiva.pdf";
                ViewBag.CedulaRFC = @"C:\inetpub\RepositorioVER\" + IdRepresentanteLegal.ToString() + "-CedulaRFC.pdf";
                ViewBag.Identificacion = @"C:\inetpub\RepositorioVER\" + IdRepresentanteLegal.ToString() + "-Identificacion.pdf";
                ViewBag.PoderNotarial = @"C:\inetpub\RepositorioVER\" + IdRepresentanteLegal.ToString() + "-PoderNotarial.pdf";
                CatRepresentantesLegales RepresentanteLegal = new CatRepresentantesLegales();
                RepresentanteLegal = LogicaRepresentanteLegal.L_DetallesRepresentanteLegal(IdRepresentanteLegal);
                //Cambio de código postal
                if (RepresentanteLegal.RL_CodigoPostal == "00000") { RepresentanteLegal.RL_CodigoPostal = ""; }
                CargarListasDesplegables(RepresentanteLegal.RL_CodigoPostal, RepresentanteLegal.RL_IdEntidadFederativa, RepresentanteLegal.RL_IdMunicipio, RepresentanteLegal.RL_IdLocalidad);
                ViewBag.RFCRepresentante = Usuario.U_RFCRepresentanteAsociado;
                return View(RepresentanteLegal);

            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: Actualizar");
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Detalles(int IdRepresentanteLegal)
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                CatRepresentantesLegales RepresentanteLegal = new CatRepresentantesLegales();

                RepresentanteLegal = LogicaRepresentanteLegal.L_DetallesRepresentanteLegal(IdRepresentanteLegal);
                //RepresentanteLegal.RL_CodigoPostal = null;
                //if (RepresentanteLegal.RL_CodigoPostal == "00000") { RepresentanteLegal.RL_CodigoPostal = ""; }
                return View(RepresentanteLegal);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: Detalles");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int IdRepresentanteLegal)
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                CatRepresentantesLegales Representante = new CatRepresentantesLegales();

                Representante = LogicaRepresentanteLegal.L_DetallesRepresentanteLegal(IdRepresentanteLegal);

                if (Representante.RL_CodigoPostal == "00000") { Representante.RL_CodigoPostal = ""; }
                ViewBag.RFCRepresentante = Usuario.U_RFCRepresentanteAsociado;
                ViewBag.NombreRepresentanteEliminar = Representante.RL_Nombre + " " + Representante.RL_PrimerApellido + " " + Representante.RL_SegundoApellido;
                return View(Representante);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: Eliminar");
                return RedirectToAction("Index");
            }

        }


        #endregion

        #region Métodos Auxiliares:

        //[HttpPost]
        //public ActionResult EjecutarAccionRepresentante(CatRepresentantesLegales Representante, string Accion, IEnumerable<HttpPostedFileBase> files)
        //{
        //    CatEmpresas Empresa = new CatEmpresas();
        //    MetodosGenerales MetodoGeneral = new MetodosGenerales();

        //    CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
        //    if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
        //    {
        //        TempData["notice"] = "La sesión ha expirado.";
        //        RedirectToAction("Logout", "Home");
        //    }

        //    try
        //    {
        //        LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
        //        Empresa = LogicaEmpresa.L_DetallesEmpresa(Usuario.U_IdEmpresa);
        //        string Validacion = "";
        //        string RFCAsociado = "";
        //        RFCAsociado = Session["RFCAsociado"].ToString();

        //        switch (Accion)
        //        {
        //            case "Insertar":
        //                Validacion = MetodoGeneral.ValidarFIEL(files, Representante.RL_ClavePrivada, RFCAsociado);
        //                if (Validacion == "Validación exitosa")
        //                {
        //                    int IdRepresentanteLegal = LogicaRepresentanteLegal.L_InsertarRepresentanteLegal(Representante, Usuario.U_IdEmpresa);
        //                    if (IdRepresentanteLegal > 0)
        //                    {
        //                        string ext = "";

        //                        int num_archivo = 0;
        //                        foreach (string inputTagName in Request.Files)
        //                        {
        //                            HttpPostedFileBase file = Request.Files[num_archivo];
        //                            num_archivo = num_archivo + 1;
        //                            ext = Path.GetExtension(file.FileName);

        //                            if (ext != ".cer" && ext != ".key")
        //                            {
        //                                var fileName = Path.GetFileName(file.FileName);
        //                                bool PdfValido = MetodoGeneral.ReadByteArrayFromFile(file);


        //                                //if (file.ContentType.ToString().ToLower() == "application/pdf" && fileSubHeader == "25504446")
        //                                if (file.ContentType.ToString().ToLower() == "application/pdf" && PdfValido)
        //                                {
        //                                    //string path = @"C:\Users\jesusalejandro.ramos\Desktop\repositorio\" + Representante.RL_IdRepresentanteL.ToString() + "-" + inputTagName + ext;
        //                                    string path = @"C:\inetpub\RepositorioVER\" + IdRepresentanteLegal.ToString() + "-" + inputTagName + ext;
        //                                    file.SaveAs(path);
        //                                }

        //                            }
        //                        }
        //                        TempData["notice"] = "Se ha solicitado la autorización de un nuevo representante al cual, se le hará llegar un correo con la resolución del administrador. ";
        //                        Session["TipoAlerta"] = "Correcto";
        //                        //RedirigirConParametros("http://" + HttpContext.Request.Url.Authority + "/RepresentantesLegales/Index", "a", "0", "");
        //                        return View(Representante);
        //                    }
        //                    else
        //                    {
        //                        TempData["notice"] = "No se pudo registrar el representante legal, por favor revise la información.";
        //                        Session["TipoAlerta"] = "Error";
        //                        CargarListasDesplegables(Representante.RL_CodigoPostal, Representante.RL_IdEntidadFederativa, Representante.RL_IdMunicipio, Representante.RL_IdLocalidad);
        //                        //RedirectToAction("Insertar", "RepresentantesLegales", new { IdRepresentanteLegal = Representante.RL_IdRepresentanteLegal });
        //                        //RedirigirConParametros("http://" + HttpContext.Request.Url.Authority + "/RepresentantesLegales/Insertar", "IdRepresentanteLegal", Representante.RL_IdRepresentanteLegal.ToString(), "method='post'");
        //                        return View(Representante);
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["notice"] = Validacion;
        //                    Session["TipoAlerta"] = "Error";
        //                    CargarListasDesplegables(Representante.RL_CodigoPostal, Representante.RL_IdEntidadFederativa, Representante.RL_IdMunicipio, Representante.RL_IdLocalidad);
        //                    //RedirectToAction("Insertar", "RepresentantesLegales", new { IdRepresentanteLegal = Representante.RL_IdRepresentanteLegal });
        //                    //RedirigirConParametros("http://" + HttpContext.Request.Url.Authority + "/RepresentantesLegales/Insertar", "IdRepresentanteLegal", Representante.RL_IdRepresentanteLegal.ToString(), "method='post'");
        //                    return View(Representante);
        //                }
        //            case "Actualizar":
        //                Validacion = MetodoGeneral.ValidarFIEL(files, Representante.RL_ClavePrivada, RFCAsociado);
        //                if (Validacion == "Validación exitosa")
        //                {
        //                    Representante.RL_IdEstatusSolicitudRepresentante = 1;
        //                    Representante.RL_Activo = true;
        //                    if (LogicaRepresentanteLegal.L_ActualizarRepresentanteLegal(Representante))
        //                    {
        //                        try
        //                        {
        //                            string ext = "";

        //                            int num_archivo = 0;
        //                            foreach (string inputTagName in Request.Files)
        //                            {
        //                                HttpPostedFileBase file = Request.Files[num_archivo];

        //                                num_archivo = num_archivo + 1;
        //                                ext = Path.GetExtension(file.FileName);

        //                                if (ext != ".cer" && ext != ".key")
        //                                {
        //                                    if (file.ContentType.ToString().ToLower() == "application/pdf")
        //                                    {
        //                                        var fileName = Path.GetFileName(file.FileName);
        //                                        //string path = @"C:\Users\jesusalejandro.ramos\Desktop\repositorio\" + Representante.RL_IdRepresentanteL.ToString() + "-" + inputTagName + ext;
        //                                        string path = @"C:\inetpub\RepositorioVER\" + Representante.RL_IdRepresentanteLegal.ToString() + "-" + inputTagName + ext;
        //                                        file.SaveAs(path);
        //                                    }

        //                                }
        //                            }
        //                            TempData["notice"] = "El representante ha sido actualizado, deberá esperar un correo con el resultado de la nueva validación.";
        //                            Session["TipoAlerta"] = "Correcto";
        //                            //RedirigirConParametros(HttpContext.Request.Url.Authority+ "/RepresentantesLegales/Index", "a", "0", "");
        //                            return View(Representante);
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            TempData["notice"] = "La informació se ha actualizado pero uno o varios archivos no han sido enviados, comunícate con algun administrador de SENER.";
        //                            Session["TipoAlerta"] = "Error";
        //                            //RedirectToAction("Actualizar", "RepresentantesLegales", new { IdRepresentanteLegal = Representante.RL_IdRepresentanteLegal });
        //                            //RedirigirConParametros("http://"+ HttpContext.Request.Url.Authority + "/RepresentantesLegales/Actualizar", "IdRepresentanteLegal", Representante.RL_IdRepresentanteLegal.ToString(), "method='post'");
        //                            return View(Representante);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        TempData["notice"] = "El representante no ha sido actualizado, por favor revise la información.";
        //                        Session["TipoAlerta"] = "Error";
        //                        CargarListasDesplegables(Representante.RL_CodigoPostal, Representante.RL_IdEntidadFederativa, Representante.RL_IdMunicipio, Representante.RL_IdLocalidad);
        //                        //RedirectToAction("Actualizar", "RepresentantesLegales", new { IdRepresentanteLegal = Representante.RL_IdRepresentanteLegal });
        //                        //RedirigirConParametros("http://" + HttpContext.Request.Url.Authority + "/RepresentantesLegales/Actualizar", "IdRepresentanteLegal", Representante.RL_IdRepresentanteLegal.ToString(), "method='post'");
        //                        return View(Representante);
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["notice"] = Validacion;
        //                    Session["TipoAlerta"] = "Error";
        //                    CargarListasDesplegables(Representante.RL_CodigoPostal, Representante.RL_IdEntidadFederativa, Representante.RL_IdMunicipio, Representante.RL_IdLocalidad);
        //                    //RedirectToAction("Actualizar", "RepresentantesLegales", new { IdRepresentanteLegal = Representante.RL_IdRepresentanteLegal });
        //                    //RedirigirConParametros("http://"+ HttpContext.Request.Url.Authority + "/RepresentantesLegales/Index", "IdRepresentanteLegal", Representante.RL_IdRepresentanteLegal.ToString(), "method='post'");
        //                    return View(Representante);
        //                }
        //            case "Eliminar":
        //                Validacion = MetodoGeneral.ValidarFIEL(files, Representante.RL_ClavePrivada, RFCAsociado);
        //                if (Validacion == "Validación exitosa")
        //                {
        //                    if (LogicaRepresentanteLegal.L_EliminarRepresentanteLegal(Representante.RL_IdRepresentanteLegal))
        //                    {
        //                        CatUsuarios UsuarioDelRepresentante = new CatUsuarios();
        //                        LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
        //                        UsuarioDelRepresentante = LogicaUsuario.L_DetallesUsuarioPorRepresentante(Representante.RL_IdRepresentanteLegal);

        //                        if (Usuario.U_IdUsuario == UsuarioDelRepresentante.U_IdUsuario)
        //                        {

        //                            //RedirectToAction("Index", "Home");
        //                            //RedirigirConParametros("http://" + HttpContext.Request.Url.Authority + "/RepresentantesLegales/Index", "a", "0", "");
        //                            return View(Representante);
        //                        }
        //                        else
        //                        {
        //                            TempData["notice"] = "Se ha deshabilitado un representante legal para esta empresa.";
        //                            Session["TipoAlerta"] = "Error";
        //                            RedirigirConParametros("http://"+ HttpContext.Request.Url.Authority + "/RepresentantesLegales/Index", "a", "0", "");
                                    
        //                            //RedirigirConParametros("http://localhost:58021/RepresentantesLegales/Eliminar", "IdRepresentanteLegal", Representante.RL_IdRepresentanteLegal.ToString(), "method='post'");
        //                            //RedirectToAction("Index", "RepresentantesLegales");
        //                            return View(Representante);
        //                        }

        //                    }
        //                    else
        //                    {
        //                        TempData["notice"] = "Operación no concluida, favor de comunicarse con un administrador de SENER.";
        //                        Session["TipoAlerta"] = "Error";
        //                        //RedirigirConParametros("http://localhost:58021/RepresentantesLegales/Eliminar", "IdRepresentanteLegal", Representante.RL_IdRepresentanteLegal.ToString(), "method='post'");
        //                        //RedirigirConParametros("http://" + HttpContext.Request.Url.Authority + "/RepresentantesLegales/Index", "a", "0", "");
        //                        return View(Representante);
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["notice"] = Validacion;
        //                    Session["TipoAlerta"] = "Error";
        //                    RedirigirConParametros("http://" + HttpContext.Request.Url.Authority + "/RepresentantesLegales/Index", "a", "0", "");
        //                    //RedirigirConParametros("http://localhost:58021/RepresentantesLegales/Eliminar", "IdRepresentanteLegal", Representante.RL_IdRepresentanteLegal.ToString(), "method='post'");
        //                    return View(Representante);
        //                }
        //            default:
        //                //RedirigirConParametros("http://" + HttpContext.Request.Url.Authority + "/RepresentantesLegales/Index", "a", "0", "");
        //                RedirectToAction("Index", "RepresentantesLegales");
        //                break;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
        //        Session["TipoAlerta"] = "Error";
        //        MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: EjecutarAccionesRepresentante(" + Accion + ")");
        //        //return View("Index");
        //        //RedirigirConParametros("http://" + HttpContext.Request.Url.Authority + "/RepresentantesLegales/Index", "a", "0", "");
        //        return RedirectToAction("Index", "RepresentantesLegales");
        //    }

        //}
        
        [HttpPost]
        public ActionResult InsertarRepresentanteLegal(CatRepresentantesLegales Representante, string Accion, IEnumerable<HttpPostedFileBase> files)
        {
            CatEmpresas Empresa = new CatEmpresas();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();
            string FechaActual = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
            {
                TempData["notice"] = "La sesión ha expirado.";
                RedirectToAction("Logout", "Home");
            }

            try
            {
                LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
                Empresa = LogicaEmpresa.L_DetallesEmpresa(Usuario.U_IdEmpresa);
                string Validacion = "";
                string RFCAsociado = "";
                RFCAsociado = Session["RFCAsociado"].ToString();

                Validacion = MetodoGeneral.ValidarFIEL(files, Representante.RL_ClavePrivada, RFCAsociado);
                if (Validacion == "Validación exitosa")
                {
                    int IdRepresentanteLegal = LogicaRepresentanteLegal.L_InsertarRepresentanteLegal(Representante, Usuario.U_IdEmpresa);
                    if (IdRepresentanteLegal > 0)
                    {
                        string ext = "";
                        if (!Directory.Exists(@"C:\inetpub\RepositorioVER\Representantes\" + IdRepresentanteLegal.ToString()))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(@"C:\inetpub\RepositorioVER\Representantes\" + IdRepresentanteLegal.ToString());
                        }

                        int num_archivo = 0;
                        foreach (string inputTagName in Request.Files)
                        {
                            HttpPostedFileBase file = Request.Files[num_archivo];
                            num_archivo = num_archivo + 1;
                            ext = Path.GetExtension(file.FileName);

                            if (ext != ".cer" && ext != ".key")
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                bool PdfValido = MetodoGeneral.ReadByteArrayFromFile(file);
                                if (file.ContentType.ToString().ToLower() == "application/pdf" && PdfValido)
                                {
                                    string path = @"C:\inetpub\RepositorioVER\Representantes\" + IdRepresentanteLegal.ToString() + "\\" + inputTagName + "_PDF_" + FechaActual + ext;
                                    //string path = @"C:\inetpub\RepositorioVER\" + IdRepresentanteLegal.ToString() + "-" + inputTagName + ext;
                                    file.SaveAs(path);
                                }
                            }
                        }
                        TempData["notice"] = "Se ha solicitado la autorización de un nuevo representante al cual, se le hará llegar un correo con la resolución del administrador. ";
                        Session["TipoAlerta"] = "Correcto";
                    }
                    else
                    {
                        TempData["notice"] = "No se pudo registrar el representante legal, por favor revise la información.";
                        Session["TipoAlerta"] = "Error";                        
                    }
                }
                else
                {
                    TempData["notice"] = Validacion;
                    Session["TipoAlerta"] = "Error";                                      
                }

                //Despliegue de vista
                if (Session["TipoAlerta"] == "Error")
                {
                    CargarListasDesplegables(Representante.RL_CodigoPostal, Representante.RL_IdEntidadFederativa, Representante.RL_IdMunicipio, Representante.RL_IdLocalidad);
                    return View("Insertar",Representante);
                }
                else
                {
                    Usuario = (CatUsuarios)Session["Usuario"];
                    if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                    {
                        TempData["notice"] = "La sesión ha expirado.";
                        return RedirectToAction("Logout", "Home");
                    }
                    List<CatRepresentantesLegales> ListaRepresentantesLegales = new List<CatRepresentantesLegales>();
                    ListaRepresentantesLegales = LogicaRepresentanteLegal.L_SeleccionarRepresentantesLegalesPorEmpresa(Usuario.U_IdEmpresa);
                    ViewBag.CantidadRL = ListaRepresentantesLegales.Count();
                    return View("Index", ListaRepresentantesLegales);
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: InsertarRepresentanteLegal");
                return View("Insertar", Representante);
            }

        }

        [HttpPost]
        public ActionResult ActualizarRepresentanteLegal(CatRepresentantesLegales Representante, string Accion, IEnumerable<HttpPostedFileBase> files)
        {
            CatEmpresas Empresa = new CatEmpresas();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();

            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
            {
                TempData["notice"] = "La sesión ha expirado.";
                RedirectToAction("Logout", "Home");
            }

            try
            {
                LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
                Empresa = LogicaEmpresa.L_DetallesEmpresa(Usuario.U_IdEmpresa);
                string Validacion = "";
                string RFCAsociado = "";
                RFCAsociado = Session["RFCAsociado"].ToString();

                Validacion = MetodoGeneral.ValidarFIEL(files, Representante.RL_ClavePrivada, RFCAsociado);
                if (Validacion == "Validación exitosa")
                {
                    Representante.RL_IdEstatusSolicitudRepresentante = 1;
                    Representante.RL_Activo = true;
                    if (LogicaRepresentanteLegal.L_ActualizarRepresentanteLegal(Representante))
                    {
                        try
                        {
                            string ext = "";

                            int num_archivo = 0;
                            foreach (string inputTagName in Request.Files)
                            {
                                HttpPostedFileBase file = Request.Files[num_archivo];

                                num_archivo = num_archivo + 1;
                                ext = Path.GetExtension(file.FileName);

                                if (ext != ".cer" && ext != ".key")
                                {
                                    if (file.ContentType.ToString().ToLower() == "application/pdf")
                                    {
                                        var fileName = Path.GetFileName(file.FileName);
                                        //string path = @"C:\Users\jesusalejandro.ramos\Desktop\repositorio\" + Representante.RL_IdRepresentanteL.ToString() + "-" + inputTagName + ext;
                                        string path = @"C:\inetpub\RepositorioVER\" + Representante.RL_IdRepresentanteLegal.ToString() + "-" + inputTagName + ext;
                                        file.SaveAs(path);
                                    }

                                }
                            }
                            TempData["notice"] = "El representante ha sido actualizado, deberá esperar un correo con el resultado de la nueva validación.";
                            Session["TipoAlerta"] = "Correcto";
                        }
                        catch (Exception ex)
                        {
                            TempData["notice"] = "La información se ha actualizado pero uno o varios archivos no han sido enviados, comunícate con algun administrador de SENER.";
                            Session["TipoAlerta"] = "Error";
                        }
                    }
                    else
                    {
                        TempData["notice"] = "El representante no ha sido actualizado, por favor revise la información.";
                        Session["TipoAlerta"] = "Error";
                    }
                }
                else
                {
                    TempData["notice"] = Validacion;
                    Session["TipoAlerta"] = "Error";
                }

                //Despliegue de vista
                if (Session["TipoAlerta"] == "Error")
                {
                    CargarListasDesplegables(Representante.RL_CodigoPostal, Representante.RL_IdEntidadFederativa, Representante.RL_IdMunicipio, Representante.RL_IdLocalidad);
                    return View("Actualizar",Representante);
                }
                else
                {      
                        return RedirectToAction("Logout", "Home"); 
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: ActualizarRepresentanteLegal");
                return View("Actualizar",Representante);
            }

        }

        [HttpPost]
        public ActionResult EliminarRepresentanteLegal(CatRepresentantesLegales Representante, string Accion, IEnumerable<HttpPostedFileBase> files)
        {
            CatEmpresas Empresa = new CatEmpresas();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();

            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
            {
                TempData["notice"] = "La sesión ha expirado.";
                return RedirectToAction("Logout", "Home");
            }

            try
            {
                LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
                Empresa = LogicaEmpresa.L_DetallesEmpresa(Usuario.U_IdEmpresa);
                string Validacion = "";
                string RFCAsociado = "";
                RFCAsociado = Session["RFCAsociado"].ToString();

                Validacion = MetodoGeneral.ValidarFIEL(files, Representante.RL_ClavePrivada, RFCAsociado);
                if (Validacion == "Validación exitosa")
                {

                    if (LogicaRepresentanteLegal.L_EliminarRepresentanteLegal(Representante.RL_IdRepresentanteLegal))
                    {
                        CatUsuarios UsuarioDelRepresentante = new CatUsuarios();
                        LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
                        UsuarioDelRepresentante = LogicaUsuario.L_DetallesUsuarioPorRepresentante(Representante.RL_IdRepresentanteLegal);

                        if (Usuario.U_IdUsuario == UsuarioDelRepresentante.U_IdUsuario)
                        {
                            TempData["notice"] = "El representante ha sido eliminado de manera existosa.";
                            Session["TipoAlerta"] = "Correcto";
                            return RedirectToAction("Logout", "Home");
                        }
                        else
                        {
                            TempData["notice"] = "Se ha deshabilitado un representante legal para esta empresa.";
                            Session["TipoAlerta"] = "Correcto";
                            Usuario = (CatUsuarios)Session["Usuario"];
                            if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                            {
                                TempData["notice"] = "La sesión ha expirado.";
                                return RedirectToAction("Logout", "Home");
                            }
                            else
                            {

                                List<CatRepresentantesLegales> ListaRepresentantesLegales = new List<CatRepresentantesLegales>();
                                ListaRepresentantesLegales = LogicaRepresentanteLegal.L_SeleccionarRepresentantesLegalesPorEmpresa(Usuario.U_IdEmpresa);
                                ViewBag.CantidadRL = ListaRepresentantesLegales.Count();
                                return View("Index", ListaRepresentantesLegales);
                            }
                        }

                    }
                    else
                    {
                        TempData["notice"] = "Operación no concluida, favor de comunicarse con un administrador de SENER.";
                        Session["TipoAlerta"] = "Error";
                        return View("Eliminar", Representante);
                    }
                }
                else
                {
                    TempData["notice"] = Validacion;
                    Session["TipoAlerta"] = "Error";
                    return View("Eliminar", Representante);
                }

            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: EliminarRepresentanteLegal");
                return View("Eliminar",Representante);
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

        private void RedirigirConDatos(string IdRepresentanteLegal, string url)
        {
            Response.Clear();
            var sb = new System.Text.StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat("<body onload='document.forms[0].submit()'>");
            sb.AppendFormat("<form action='{0}' method='post'>", url);
            sb.AppendFormat("<input type='hidden' name='now' value='{0}'>", IdRepresentanteLegal);
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");
            Response.Write(sb.ToString());
            Response.End();
        }

        private void RedirigirConParametros(string Url, string Parametro, string Valor, string Metodo)
        {

            //method=
            try
            {
                Response.Clear();
                var sb = new System.Text.StringBuilder();
                sb.Append("<html>");
                sb.AppendFormat("<body onload='document.forms[0].submit()'>");
                sb.AppendFormat("<form action='{0}' '{1}' class='ns_'>", Url, Metodo);
                sb.AppendFormat("<input type='hidden' name='{0}' id='{1}' value='{2}'>", Parametro, Parametro, Valor);
                sb.Append("</form>");
                sb.Append("</body>");
                sb.Append("</html>");
                Response.Write(sb.ToString());
                Response.End();
            }
            catch (Exception ex) { }

        }

        #endregion

    }
}