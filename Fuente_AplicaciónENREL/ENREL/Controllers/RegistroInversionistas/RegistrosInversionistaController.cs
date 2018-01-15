using ENREL.Models.Asentamientos;
using ENREL.Models.CodigosPostales;
using ENREL.Models.EntidadesFederativas;
using ENREL.Models.Localidades;
using ENREL.Models.Municipios;
using ENREL.Models.Tecnologias;
using ENREL.Models.Vialidades;
using ENREL.Models.Home;
using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ENREL.Models.Empresas;
using ENREL.Models.RepresentantesLegales;
using ENREL.Models.Usuarios;
using System.IO;
using VER.Crypto;
using System.Configuration;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Office.Interop.Word;

namespace ENREL.Controllers.Registros
{
    public class RegistrosInversionistaController : Controller
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        CatEmpresas Empresa = new CatEmpresas();
        CatRepresentantesLegales Representante = new CatRepresentantesLegales();
        CatUsuarios Usuario = new CatUsuarios();

        #endregion

        #region Páginas:

        public ActionResult Index()
        {
            try
            {
                Session.Clear();
                Session["Registrando"] = 1;
                CargarListasDesplegables(null, null, null, null);
                Session["MensajePrivacidad"] = ConfigurationManager.AppSettings["MensjaePrivacidad"].ToString();
                return View();
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Primer Paso");
                return View("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult DatosRepresentante(CatEmpresas Empresa)
        {
            try
            {
                if (Session["Registrando"] == null)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                Representante = (CatRepresentantesLegales)Session["NuevoAcceso_Representante"];
                Session.Add("NuevoAcceso_Empresa", Empresa);

                LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
                CatEmpresas EmpresaExistente = LogicaEmpresa.L_DetallesEmpresaPorRFC(Empresa.E_RFC);

                if (EmpresaExistente.E_RFC == null)
                {
                    if (Representante == null)
                    {
                        CargarListasDesplegables(null, null, null, null);
                        return View();
                    }
                    else
                    {
                        //Cambio de Código Póstal
                        //if (Empresa.E_CodigoPostal == 0 || Empresa.E_CodigoPostal == null) { Empresa.E_CodigoPostal = null; }
                        CargarListasDesplegables(Representante.RL_CodigoPostal, Representante.RL_IdEntidadFederativa, Representante.RL_IdMunicipio, Representante.RL_IdLocalidad);
                        return View("DatosRepresentante", Representante);
                    }
                }
                else
                {
                    TempData["notice"] = "Ya existe una empresa con el mismo RFC en la base de datos.";
                    Session["TipoAlerta"] = "Error";
                    CargarListasDesplegables(Empresa.E_CodigoPostal, Empresa.E_IdEntidadFederativa, Empresa.E_IdMunicipio, Empresa.E_IdLocalidad);
                    return View("Index", Empresa);
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Segundo Paso");
                return View("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult DatosUsuario(CatRepresentantesLegales Representante)
        {
            try
            {
                if (Session["Registrando"] == null)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                Usuario = (CatUsuarios)Session["NuevoAcceso_Usuario"];
                Session.Add("NuevoAcceso_Representante", Representante);
                if (Usuario == null)
                {
                    return View();
                }
                else
                {
                    return View("DatosUsuario", Usuario);
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Tercer Paso");
                return View("Index", "Home");
            }


        }

        //COMUNICACIÓN: Pasando a Datos de acreditación después de haber cargado datos de un usuario operativo
        [HttpPost]
        public ActionResult DatosAcreditacion(CatUsuarios UsuarioOperativo)
        {
            try
            {
                if (Session["Registrando"] == null)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                if (UsuarioOperativo.U_Password == UsuarioOperativo.U_ConfirmarPassword)
                {
                    LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
                    CatUsuarios UsuarioExistente = LogicaUsuario.L_DetallesUsuarioPorNombreUnicamente(UsuarioOperativo.U_Nombre);

                    if (UsuarioExistente.U_IdUsuario > 0)
                    {
                        TempData["notice"] = "Ya existe un usuario con el mismo nombre, por favor elige otro.";
                        Session["TipoAlerta"] = "Error";
                        return View("DatosUsuario", UsuarioOperativo);
                    }
                    else
                    {

                        Representante = (CatRepresentantesLegales)Session["NuevoAcceso_Representante"];
                        Session.Add("NuevoAcceso_Usuario", UsuarioOperativo);
                        ViewBag.RFCRepresentante = Representante.RL_RFC;
                        Empresa = (CatEmpresas)Session["NuevoAcceso_Empresa"];
                        if (Empresa.E_RFC.Length == 12)
                        {
                            ViewBag.MostrarDocumento = true;
                        }
                        else
                        {
                            ViewBag.MostrarDocumento = false;
                        }
                        return View();
                    }

                }
                else
                {
                    TempData["notice"] = "Las contraseñas no coinciden.";
                    Session["TipoAlerta"] = "Error";
                    return View("DatosUsuario", UsuarioOperativo);
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Cuarto Paso A");
                return View("Index", "Home");
            }

        }

        //COMUNICACIÓN: Pasando a Datos de acreditación sin haber cargado datos de un usuario operativo
        public ActionResult DatosAcreditacionB()
        {
            try
            {
                if (Session["Registrando"] == null)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                Session["NuevoAcceso_Usuario"] = null;
                Session["NuevoAcceso_Usuario"] = null;
                Representante = (CatRepresentantesLegales)Session["NuevoAcceso_Representante"];
                ViewBag.RFCRepresentante = Representante.RL_RFC;
                Empresa = (CatEmpresas)Session["NuevoAcceso_Empresa"];
                if (Empresa.E_RFC.Length == 12)
                {
                    ViewBag.MostrarDocumento = true;
                }
                else
                {
                    ViewBag.MostrarDocumento = false;
                }
                return View("DatosAcreditacion");
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Cuarto Paso B");
                return View("Index", "Home");
            }

        }

        #endregion

        #region Métodos Auxiliares:

        [HttpPost]
        public ActionResult InsertarPrimerusuario(IEnumerable<HttpPostedFileBase> files, FormCollection form)
        {
            LogicaUsuarios Logicausuarios = new LogicaUsuarios();
            LogicaEmpresas LogicaEmpresas = new LogicaEmpresas();
            LogicaRepresentantesLegales LogicaRepresentantesLegales = new LogicaRepresentantesLegales();
            LogicaHome LogicaHome = new LogicaHome();
            string FechaActual = DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss");

            try
            {
                if (Session["Registrando"] == null)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                Empresa = (CatEmpresas)Session["NuevoAcceso_Empresa"];

                Representante = (CatRepresentantesLegales)Session["NuevoAcceso_Representante"];

                string ClavePrivada = form["RL_ClavePrivada"];
                string Validacion = MetodoGeneral.ValidarFIEL(files, ClavePrivada, Representante.RL_RFC);

                if (Validacion == "Validación exitosa")
                {

                    CatEmpresas EmpresaSession = (CatEmpresas)Session["NuevoAcceso_Empresa"];

                    EmpresaSession.E_CompartirDatos = false;
                    if (form["CompartirDatos"].Equals("2")) { EmpresaSession.E_CompartirDatos = true; }

                    CatUsuarios UsuarioOperativoSession = (CatUsuarios)Session["NuevoAcceso_Usuario"];

                    //LogicaHome.L_InsertarRegistroSolicitud(EmpresaSession, Representante, UsuarioOperativoSession);
                    string GUID = LogicaHome.L_InsertarRegistroSolicitud(Empresa, Representante, UsuarioOperativoSession);
                    string ext = "";

                    X509Certificate2 CertificadoParaFirma = new X509Certificate2();
                    CatRepresentantesLegales NuevoRepresentante = (CatRepresentantesLegales)Session["NuevoAcceso_Representante"];

                    if (!Directory.Exists(@"C:\inetpub\RepositorioVER\Representantes\" + NuevoRepresentante.RL_IdRepresentanteLegal.ToString()))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(@"C:\inetpub\RepositorioVER\Representantes\" + NuevoRepresentante.RL_IdRepresentanteLegal.ToString());
                    }

                    int num_archivo = 0;
                    foreach (string inputTagName in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[num_archivo];
                        num_archivo = num_archivo + 1;
                        ext = Path.GetExtension(file.FileName);

                        if (ext != ".cer" && ext != ".key")
                        {
                            bool PdfValido = MetodoGeneral.ReadByteArrayFromFile(file);

                            if ((file.ContentType.ToString().ToLower() == "application/pdf" && PdfValido) || file.ContentType.ToString().ToLower() == "application/x-zip-compressed")
                            {
                                string path = @"C:\inetpub\RepositorioVER\Representantes\" + NuevoRepresentante.RL_IdRepresentanteLegal.ToString() + "\\" + inputTagName + "_PDF_" + FechaActual + ext;
                                using (Stream fs = file.InputStream)
                                {
                                    fs.Flush();
                                    fs.Position = 0;

                                    CertificadoParaFirma = MetodoGeneral.ObtenerCertificadoParaFirmar(files, ClavePrivada);
                                    MetodoGeneral.SignWithThisCert(CertificadoParaFirma, fs, path);
                                }
                            }
                        }
                    }
                    TempData["notice"] = "Los datos han sido registrados exitosamente.";

                    Session.Clear();
                    Session["TipoAlerta"] = "Correcto";

                    string target = @"C:\inetpub\RepositorioVER\Representantes\" + NuevoRepresentante.RL_IdRepresentanteLegal.ToString() + "\\" + "AcuseRegistroInversionista_PDF_" + FechaActual;
                    CatRepresentantesLegales DatosRepresentante = LogicaRepresentantesLegales.L_DetallesRepresentanteLegal(NuevoRepresentante.RL_IdRepresentanteLegal);
                    CatEmpresas DatosEmpresa = LogicaEmpresas.L_DetallesEmpresa(DatosRepresentante.RL_IdEmpresa);
                    CrearAcuseRegistroInversionista(GUID, target, DatosEmpresa, DatosRepresentante);


                    //Enviar acuse:
                    new System.Threading.Tasks.Task(() =>
                    {
                        try
                        {
                            string CorreoRepresentante = Representante.RL_CorreoElectronico;
                            Representante.RL_FechaRegistro = DateTime.Now.ToString();
                            Representante.RL_Observaciones = "Solicitud de registro enviada";

                            //CatRepresentantesLegales DatosRepresentante = LogicaRepresentantesLegales.L_DetallesRepresentanteLegal(NuevoRepresentante.RL_IdRepresentanteLegal);
                            //CatEmpresas DatosEmpresa = LogicaEmpresas.L_DetallesEmpresa(DatosRepresentante.RL_IdEmpresa);

                            System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage("enrel@energia.gob.mx", CorreoRepresentante);
                            email.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoAdministrador"].ToString()));
                            email.Subject = "Acuse de registro";
                            email.Attachments.Add(new Attachment(target + ".pdf"));

                            //Obtener la plantilla en HTML:
                            string pathCorreo = ConfigurationManager.AppSettings["Html_AcuseRegistroENREL"].ToString();
                            string ContenidoCorreo = System.IO.File.ReadAllText(pathCorreo);

                            //Reemplazar información

                            string ContactoEmpresa = "Teléfono (" + DatosEmpresa.E_Lada + ") " + DatosEmpresa.E_TelefonoFijo.ToString();
                            string NombreRepresentante = DatosRepresentante.RL_Nombre + " " + DatosRepresentante.RL_PrimerApellido + " " + DatosRepresentante.RL_SegundoApellido;
                            string ContactoRepresentanteLegal = "Teléfono (" + DatosRepresentante.RL_Lada + ") " + DatosRepresentante.RL_TelefonoFijo.ToString() + " Ext. " + DatosRepresentante.RL_ExtensionTelefonica + ", Teléfono móvil: " + DatosRepresentante.RL_TelefonoCelular;
                            string EmpresaNumeroInterior = "";
                            try { EmpresaNumeroInterior = DatosEmpresa.E_NumeroInterior.ToString(); }
                            catch { }
                            string EmpresaCodigoPostal = "";
                            try { EmpresaCodigoPostal = DatosEmpresa.E_CodigoPostal.ToString(); }
                            catch { }

                            string RepresentanteNumeroInterior = "";
                            try { RepresentanteNumeroInterior = DatosRepresentante.RL_NumeroInterior.ToString(); }
                            catch { }
                            string RepresentanteCodigoPostal = "";
                            try { RepresentanteCodigoPostal = DatosRepresentante.RL_CodigoPostal.ToString(); }
                            catch { }

                            ContenidoCorreo = ContenidoCorreo.Replace("#NombreEmpresa#", DatosEmpresa.E_NombreComercial);
                            ContenidoCorreo = ContenidoCorreo.Replace("#RFCEmpresa#", DatosEmpresa.E_RFC);
                            ContenidoCorreo = ContenidoCorreo.Replace("#ContactoEmpresa#", ContactoEmpresa);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioEmpresaCalle#", DatosEmpresa.E_Calle);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioEmpresaNExterior#", DatosEmpresa.E_NumeroExterior.ToString());
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioEmpresaNInterior#", EmpresaNumeroInterior);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioEmpresaColonia#", DatosEmpresa.E_Colonia);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioEmpresaCP#", EmpresaCodigoPostal);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioEmpresaMunicipio#", DatosEmpresa.E_Municipio);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioEmpresaEstado#", DatosEmpresa.E_EntidadFederativa);
                            ContenidoCorreo = ContenidoCorreo.Replace("#NombreRepresentanteLegal#", NombreRepresentante);
                            ContenidoCorreo = ContenidoCorreo.Replace("#RFCRepresentanteLegal#", DatosRepresentante.RL_RFC);
                            ContenidoCorreo = ContenidoCorreo.Replace("#ContactoRepresentanteLegal#", ContactoRepresentanteLegal);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioRepresentanteCalle#", DatosRepresentante.RL_Calle);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioRepresentanteNExterior#", DatosRepresentante.RL_NumeroExterior.ToString());
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioRepresentanteNInterior#", RepresentanteNumeroInterior);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioRepresentanteColonia#", DatosRepresentante.RL_Colonia);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioRepresentanteCP#", RepresentanteCodigoPostal);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioRepresentanteMunicipio#", DatosRepresentante.RL_Municipio);
                            ContenidoCorreo = ContenidoCorreo.Replace("#DomicilioRepresentanteEstado#", DatosRepresentante.RL_EntidadFederativa);
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
                        catch (Exception ex)
                        {
                            MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Quinto Paso - Envío de correo");
                        }

                    }).Start();
                    return View();

                }
                else
                {
                    TempData["notice"] = Validacion;
                    Session["TipoAlerta"] = "Error";
                    MetodoGeneral.RegistroDeError(Validacion, "Registro de inversionista: Quinto Paso");
                    Representante = (CatRepresentantesLegales)Session["NuevoAcceso_Representante"];
                    ViewBag.RFCRepresentante = Representante.RL_RFC;
                    if (Empresa.E_RFC.Length == 12)
                    {
                        ViewBag.MostrarDocumento = true;
                    }
                    else
                    {
                        ViewBag.MostrarDocumento = false;
                    }
                    return View("DatosAcreditacion");
                }

            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.InnerException.ToString(), "Registro de inversionista: Quinto Paso");
                if (Empresa.E_IdEmpresa > 0)
                {
                    LogicaHome.L_EliminarIntentoRegistro(Empresa.E_IdEmpresa);
                }
                ViewBag.RFCRepresentante = Representante.RL_RFC;

                if (Empresa.E_RFC.Length == 12)
                {
                    ViewBag.MostrarDocumento = true;
                }
                else
                {
                    ViewBag.MostrarDocumento = false;
                }
                return View("DatosAcreditacion");
            }

        }

        private void CrearAcuseRegistroInversionista(string GUID, string target, CatEmpresas Empresa, CatRepresentantesLegales Representante)
        {
            string Fecha = DateTime.Now.ToString();
            #region CrearCódigo QR

            string EmpresaNumeroInterior = "SN";
            string RepresentanteNumeroInterior = "SN";
            string RepresentanteExtensionTelefonica = "SN";

            if (Empresa.E_NumeroInterior != null) { EmpresaNumeroInterior = Empresa.E_NumeroInterior; }
            if (Representante.RL_ExtensionTelefonica != null) { RepresentanteExtensionTelefonica = Representante.RL_ExtensionTelefonica.ToString(); }
            if (Representante.RL_NumeroInterior != null) { RepresentanteNumeroInterior = Representante.RL_NumeroInterior; }

            string CadenaDatos =
                "Nombre Comercial: " + Empresa.E_NombreComercial + "//" +
                "RFC Empresa: " + Empresa.E_RFC + "//" +
                "Contacto: " + "(" + Empresa.E_Lada.ToString() + ") " + Empresa.E_TelefonoFijo.ToString() + "//" +
                "Calle: " + Empresa.E_Calle + "//" +
                "Número Exterior: " + Empresa.E_NumeroExterior + "//" +
                "Número Interior: " + EmpresaNumeroInterior + "//" +
                "Colonia: " + Empresa.E_Colonia + "//" +
                "C.P.: " + Empresa.E_CodigoPostal + "//" +
                "Municipio: " + Empresa.E_Municipio + "//" +
                "Entidad Federativa: " + Empresa.E_EntidadFederativa + "//" +
                "Nombre: " + Representante.RL_Nombre + " " + Representante.RL_PrimerApellido + " " + Representante.RL_SegundoApellido + "//" +
                "RFC Representante: " + Representante.RL_RFC + "//" +
                "Contacto: " + "(" + Representante.RL_Lada.ToString() + ") " + Representante.RL_TelefonoFijo.ToString() + " Ext. " + RepresentanteExtensionTelefonica + "//" +
                "Teléfono Móvil: " + Representante.RL_TelefonoCelular + "//" +
                "Calle: " + Representante.RL_Calle + "//" +
                "Número Exterior: " + Representante.RL_NumeroExterior + "//" +
                "Número Interior: " + Representante.RL_NumeroInterior + "//" +
                "Colonia: " + Representante.RL_Colonia + "//" +
                "C.P.: " + Representante.RL_CodigoPostal + "//" +
                "Municipio: " + Representante.RL_Municipio + "//" +
                "Entidad Federativa: " + Representante.RL_EntidadFederativa + "//" +
                "Fecha: " + Fecha;
            string CadenaDatosEncriptada = MetodoGeneral.EncriptarPassword(CadenaDatos + GUID + "ClaveSENER" + Fecha);
            string UrlImagen = @"C:\inetpub\RepositorioVER\Representantes\" + Representante.RL_IdRepresentanteLegal + "\\" + "qrcodeAcuseRegistroRepresentante_PNG_A.png";

            var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            var qrCode = qrEncoder.Encode(CadenaDatos + "//" + CadenaDatosEncriptada);

            var renderer = new GraphicsRenderer(new FixedModuleSize(3, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (var stream = new FileStream(UrlImagen, FileMode.Create))
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);

            #endregion

            #region Documento

            string sourcePath = ConfigurationManager.AppSettings["Oficio_RecepcionSolicitudRegistro"].ToString() + ".docx";
            string targetPathDocx = target + ".docx";
            string targetPathPdf = target + ".pdf";

            System.IO.File.Copy(sourcePath, targetPathDocx, true);

            Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
            appWord.Visible = false;
            Microsoft.Office.Interop.Word.Document wordDocument = appWord.Documents.Open(targetPathDocx, ReadOnly: false, Visible: false);
            wordDocument.Activate();

            string ContactoEmpresa = "Teléfono (" + Empresa.E_Lada.ToString() + ") " + Empresa.E_TelefonoFijo.ToString();
            string NombreRepresentante = Representante.RL_Nombre + " " + Representante.RL_PrimerApellido + " " + Representante.RL_SegundoApellido;
            string ContactoRepresentanteLegal = "Teléfono (" + Representante.RL_Lada.ToString() + ") " + Representante.RL_TelefonoFijo.ToString() + " Ext. " + RepresentanteExtensionTelefonica + ", Teléfono móvil: " + Representante.RL_TelefonoCelular.ToString();
            try { EmpresaNumeroInterior = Empresa.E_NumeroInterior.ToString(); }
            catch { }
            string EmpresaCodigoPostal = "";
            try { EmpresaCodigoPostal = Empresa.E_CodigoPostal.ToString(); }
            catch { }
            try { RepresentanteNumeroInterior = Representante.RL_NumeroInterior.ToString(); }
            catch { }
            string RepresentanteCodigoPostal = "";
            try { RepresentanteCodigoPostal = Representante.RL_CodigoPostal.ToString(); }
            catch { }

            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#NombreEmpresa#", Empresa.E_NombreComercial);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#RFCEmpresa#", Empresa.E_RFC);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#ContactoEmpresa#", ContactoEmpresa);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioEmpresaCalle#", Empresa.E_Calle);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioEmpresaNExterior#", Empresa.E_NumeroExterior.ToString());
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioEmpresaNInterior#", EmpresaNumeroInterior);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioEmpresaColonia#", Empresa.E_Colonia);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioEmpresaCP#", EmpresaCodigoPostal);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioEmpresaMunicipio#", Empresa.E_Municipio);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioEmpresaEstado#", Empresa.E_EntidadFederativa);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#NombreRepresentanteLegal#", NombreRepresentante);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#RFCRepresentanteLegal#", Representante.RL_RFC);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#ContactoRepresentanteLegal#", ContactoRepresentanteLegal);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioRepresentanteCalle#", Representante.RL_Calle);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioRepresentanteNExterior#", Representante.RL_NumeroExterior.ToString());
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioRepresentanteNInterior#", RepresentanteNumeroInterior);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioRepresentanteColonia#", Representante.RL_Colonia);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioRepresentanteCP#", RepresentanteCodigoPostal);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioRepresentanteMunicipio#", Representante.RL_Municipio);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#DomicilioRepresentanteEstado#", Representante.RL_EntidadFederativa);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#Fecha#", Fecha);
            MetodoGeneral.ReemplazarValoresEnTexto(appWord, "#Clave#", CadenaDatosEncriptada);
            var shape = wordDocument.Bookmarks["QRCode"].Range.InlineShapes.AddPicture(UrlImagen, false, true);
            shape.Width = 100;
            shape.Height = 100;
            //app.Visible = true;

            //MetodoGeneral.ReemplazarQRCodeEnTexto(appWord, "#QRCode#", "Jaja", UrlImagen );

            wordDocument.Save();
            //wordDocument.SaveAs2(targetPathPdf, WdSaveFormat.wdFormatPDF);
            wordDocument.ExportAsFixedFormat(targetPathPdf, WdExportFormat.wdExportFormatPDF);
            ((Microsoft.Office.Interop.Word._Document)wordDocument).Close();
            ((Microsoft.Office.Interop.Word._Application)appWord).Quit();

            #endregion
        }

        [HttpPost]
        public ActionResult RegresarDatosEmpresa(CatRepresentantesLegales Representante)
        {
            try
            {
                if (Session["Registrando"] == null)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                Session["NuevoAcceso_Representante"] = Representante;
                Empresa = (CatEmpresas)Session["NuevoAcceso_Empresa"];
                CargarListasDesplegables(Empresa.E_CodigoPostal, Empresa.E_IdEntidadFederativa, Empresa.E_IdMunicipio, Empresa.E_IdLocalidad);
                return View("Index", Empresa);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Regreso a empresa");
                return View("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult RegresarDatosRepresentante(CatUsuarios Usuario)
        {
            try
            {
                if (Session["Registrando"] == null)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                Session["NuevoAcceso_Usuario"] = Usuario;
                Representante = (CatRepresentantesLegales)Session["NuevoAcceso_Representante"];
                CargarListasDesplegables(Representante.RL_CodigoPostal, Representante.RL_IdEntidadFederativa, Representante.RL_IdMunicipio, Representante.RL_IdLocalidad);
                return View("DatosRepresentante", Representante);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Regreso a Representante");
                return View("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult RegresarDatosUsuario()
        {
            try
            {
                if (Session["Registrando"] == null)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                CatUsuarios Usuario = (CatUsuarios)Session["NuevoAcceso_Usuario"];
                return View("DatosUsuario", Usuario);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Registro de inversionista: Regreso a usuario");
                return View("Index", "Home");
            }

        }

        //Actualiza las listas deplegables
        private void CargarListasDesplegables(string CodigoPostal, int? IdEntidadFederativa, int? IdMunicipio, int? IdLocalidad)
        {
            ViewBag.EntidadesFederativas = MetodoGeneral.CargarEntidadesFederativas(CodigoPostal);
            ViewBag.Municipios = MetodoGeneral.CargarMunicipios(CodigoPostal, IdEntidadFederativa, IdMunicipio, IdLocalidad);
            ViewBag.Localidades = MetodoGeneral.CargarLocalidades(CodigoPostal, IdEntidadFederativa, IdMunicipio, IdLocalidad);
            ViewBag.TiposAsentamiento = MetodoGeneral.CargarTiposAsentamiento();
            ViewBag.TiposVialidad = MetodoGeneral.CargarTiposVialidad();
        }

        #endregion

    }
}