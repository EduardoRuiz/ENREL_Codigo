using ENREL.Models.Asentamientos;
using ENREL.Models.CodigosPostales;
using ENREL.Models.Consultar;
using ENREL.Models.EntidadesFederativas;
using ENREL.Models.Localidades;
using ENREL.Models.Municipios;
using ENREL.Models.Tecnologias;
using ENREL.Models.Vialidades;
using ENREL.Models.Empresas;
using ENREL.Models.Proyectos;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.X509;
using SysX509 = System.Security.Cryptography.X509Certificates;

using System.Text;
using System.Web;
using System.Web.Mvc;
using VER.Crypto;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ENREL.Models.EstatusProyecto;
using ENREL.Models.Usuarios;
using System.Web.Security;
using System.Net;
using System.Web.Script.Serialization;
using ENREL.Models.ServiciosWeb;
using ENREL.Models.RepresentantesLegales;
using System.Xml.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using VER.Crypto.JavaScience;
using iTextSharp.text.pdf.security;

namespace ENREL.Controllers.Auxiliar
{
    public class MetodosGenerales : Controller
    {
        
        #region Cargar Listas Desplegables:

        public SelectList CargarEntidadesFederativas(string IdCodigoPostal)
        {
            List<CatCodigosPostales> ListaCodigosPostales = new List<CatCodigosPostales>();
            List<UbicacionPorCP> ListaUbicacionesPorCP = new List<UbicacionPorCP>();
            List<CatEntidadesFederativas> ListaEntidadesFederativas = new List<CatEntidadesFederativas>();

            LogicaEntidadesFederativas LogicaEntidadesFederativas = new LogicaEntidadesFederativas();

            ListaEntidadesFederativas = LogicaEntidadesFederativas.L_SeleccionarEntidadesFederativas(null, null);
            return new SelectList(ListaEntidadesFederativas, "IdEntidadFederativa", "EntidadFederativa");
        }

        public SelectList CargarMunicipios(string IdCodigoPostal, int? IdEntidadFederativa, int? IdMunicipio, int? IdLocalidad)
        {
            List<CatCodigosPostales> ListaCodigosPostales = new List<CatCodigosPostales>();
            List<UbicacionPorCP> ListaUbicacionesPorCP = new List<UbicacionPorCP>();
            List<CatMunicipios> ListaMunicipios = new List<CatMunicipios>();

            LogicaMunicipios LogicaMunicipios = new LogicaMunicipios();

            if (IdEntidadFederativa != null && IdEntidadFederativa > 0)
            {
                ListaMunicipios = LogicaMunicipios.L_SeleccionarMunicipios(null, IdEntidadFederativa, null);
            }
            return new SelectList(ListaMunicipios, "IdMunicipio", "Municipio");
        }

        public SelectList CargarLocalidades(string IdCodigoPostal, int? IdEntidadFederativa, int? IdMunicipio, int? IdLocalidad)
        {
            List<CatCodigosPostales> ListaCodigosPostales = new List<CatCodigosPostales>();
            List<UbicacionPorCP> ListaUbicacionesPorCP = new List<UbicacionPorCP>();
            List<CatLocalidades> ListaLocalidades = new List<CatLocalidades>();

            LogicaLocalidades LogicaLocalidades = new LogicaLocalidades();

            if (IdEntidadFederativa > 0 && IdMunicipio > 0)
            {
                ListaLocalidades = LogicaLocalidades.L_SeleccionarLocalidades(null, IdEntidadFederativa, IdMunicipio, null);
            }

            return new SelectList(ListaLocalidades, "IdLocalidad", "Localidad");
        }

        public SelectList CargarTiposAsentamiento()
        {
            List<CatTiposAsentamiento> ListaTiposAsentamiento = new List<CatTiposAsentamiento>();
            LogicaTiposAsentamiento LogicaTiposAsentamiento = new LogicaTiposAsentamiento();

            ListaTiposAsentamiento = LogicaTiposAsentamiento.L_SeleccionarTiposAsentamiento();
            return new SelectList(ListaTiposAsentamiento, "IdTipoAsentamiento", "TipoAsentamiento");
        }

        public SelectList CargarTiposVialidad()
        {
            List<CatTiposVialidad> ListaTiposVialidad = new List<CatTiposVialidad>();          
            LogicaTiposVialidad LogicaTiposVialidad = new LogicaTiposVialidad();

            ListaTiposVialidad = LogicaTiposVialidad.L_SeleccionarTiposVialidad();
            return new SelectList(ListaTiposVialidad, "IdTipoVialidad", "TipoVialidad");
        }

        public SelectList CargarTecnologias()
        {
            List<CatTecnologias> ListaTecnologias = new List<CatTecnologias>();
            LogicaTecnologias LogicaTecnologias = new LogicaTecnologias();

            ListaTecnologias = LogicaTecnologias.L_SeleccionarTecnologias();
            return new SelectList(ListaTecnologias, "IdTecnologia", "Tecnologia");
        }

        public SelectList CargarEmpresas()
        {
            List<CatEmpresas> ListaEmpresas = new List<CatEmpresas>();
            LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();

            ListaEmpresas = LogicaEmpresa.L_SeleccionarEmpresas();
            return new SelectList(ListaEmpresas, "E_IdEmpresa", "E_NombreComercial");
        }

        public SelectList CargarTiposUsuario()
        {
            LogicaUsuarios LogicaTiposUsuario = new LogicaUsuarios();

            List<CatTiposUsuario> ListaTiposUsuario = LogicaTiposUsuario.SeleccionarTiposUsuario();
            //Se dejan todos los usuarios
            //ListaTiposUsuario.RemoveAt(1);
            //ListaTiposUsuario.RemoveAt(0);
            return new SelectList(ListaTiposUsuario, "IdTipoUsuario", "TipoUsuario");
        }

        public SelectList CargarEstatusuUsuario()
        {
            LogicaUsuarios LogicaUsuario = new LogicaUsuarios();

            List<CatEstatusUsuario> ListaTiposUsuario = LogicaUsuario.SeleccionarTiposEstatusUsuario();
            return new SelectList(ListaTiposUsuario, "IdEstatusUsuario", "EstatusUsuario");
        }

        public SelectList CargarEstatusActivo()
        {
            List<CatTiposVialidad> ListaTiposVialidad = new List<CatTiposVialidad>();
            LogicaTiposVialidad LogicaTiposVialidad = new LogicaTiposVialidad();

            ListaTiposVialidad = LogicaTiposVialidad.L_SeleccionarTiposVialidad();
            return new SelectList(ListaTiposVialidad, "IdTipoVialidad", "TipoVialidad");
        }

        

        #endregion

        #region Métodos para BD:

        public string EncriptarPassword(string U_Password)
        {
            SHA512Managed crypt = new SHA512Managed();
            StringBuilder hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(U_Password), 0, Encoding.UTF8.GetByteCount(U_Password));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            string claveEncriptada = hash.ToString();
            return claveEncriptada;
        }

        public SqlConnection EstablecerConexionBD()
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["CadenaDeConexion"].ConnectionString;
            SqlConnection Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaConexion;
            Conexion.Open();
            return Conexion;
        }

        public SqlConnection EstablecerConexionBDSharePoint()
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["FBADB"].ConnectionString;
            SqlConnection Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaConexion;
            Conexion.Open();
            return Conexion;
        }

        public SqlCommand CrearLlamadaStoredProcedure(string Procedimiento, SqlConnection Conexion)
        {
            SqlCommand comando = new SqlCommand(Procedimiento, Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            return comando;
        }

        public void CerrarConexionBD(SqlConnection Conexion)
        {
            Conexion.Close();
            Conexion.Dispose();
        }

        #endregion

        #region FIEL

        public string ValidarFIEL(IEnumerable<HttpPostedFileBase> files, string Clave, string RFC_Empresa)
        {
            string resultado;
            string ext;
            byte[] byteCer = new byte[0];
            byte[] byteKey = new byte[0];
            bool cerValido = false;
            bool keyValido = false;

            string cerURL = "";
            string keyURL = "";

            string ClavePrivada = Clave;
            int num_archivo = 0;
            foreach (HttpPostedFileBase file in files)
            {
                ext = Path.GetExtension(file.FileName);

                if (ext == ".cer")
                {
                    HttpPostedFileBase cerFile = file;
                    StreamReader streamReaderCer = new StreamReader(cerFile.InputStream);
                    Stream streamCer = streamReaderCer.BaseStream;
                    long fileLengthCer = streamCer.Length;
                    byteCer = new byte[fileLengthCer];
                    BinaryReader binaryFileCer = new BinaryReader(streamCer);

                    for (int i = 0; i < fileLengthCer; i++)
                    {
                        byteCer[i] = binaryFileCer.ReadByte();
                    }

                    cerValido = true;
                }
                else if (ext == ".key")
                {
                    HttpPostedFileBase keyFile = file;
                    StreamReader streamReaderKey = new StreamReader(keyFile.InputStream);
                    Stream streamKey = streamReaderKey.BaseStream;
                    long fileLengthKey = streamKey.Length;
                    byteKey = new byte[fileLengthKey];
                    BinaryReader binaryFileKey = new BinaryReader(streamKey);

                    for (int i = 0; i < fileLengthKey; i++)
                    {
                        byteKey[i] = binaryFileKey.ReadByte();
                    }

                    keyValido = true;
                }
            }

            if (cerValido && keyValido)
            {
                ValidaCertificado vc = new ValidaCertificado();
                string sener = vc.validacionSenerCert(RFC_Empresa, byteCer, byteKey, ClavePrivada);
                if(sener == "ok")
                {
                    bool sat = vc.validacionSatCert(byteCer);
                    //bool sat = true;
                    if (sat)
                    {
                        resultado = "Validación exitosa";
                        return resultado;

                    }
                    else
                    {
                        resultado = "Error de comunicación con entidad certificadora.";
                        return resultado;
                    }
                }
                else
                {
                    resultado = sener;
                    return resultado;
                }
            }
            else
            {
                resultado = "El certificado o la clave no fueron aprobados.";
                return resultado;
            }
        }

        #endregion

        #region Manejo de eventos:

        //Registra error en el histórico
        public void RegistroDeError(string ex, string UbicacionError)
        {
            DateTime FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            string registro = FechaActual.ToString() + " - " + UbicacionError + " - " + ex;

            using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString() + DateTime.Now.ToString("_yyyyMMdd") + ".txt").AppendText())
            {
                RegistroDeErrores.WriteLine(registro);
            }
        }

        //Salir de la aplicación
        public ActionResult Salir()
        {
            TempData["notice"] = "La sesión ha expirado.";
            return RedirectToAction("Logout", "Home");
        }

        #endregion

        #region Método para traspaso de usuarios y contraseñas:

        public void RegistrarUsuario(string NombreUsuario, string Password, string CorreoElectronico )
        {
            MembershipCreateStatus result;
            string Excepcion;

            try
            {
                // Create new user.
                MembershipUserCollection Usuarios = Membership.FindUsersByEmail(CorreoElectronico);
                if(Usuarios.Count <= 0)
                {
                    if (Membership.RequiresQuestionAndAnswer)
                    {
                        MembershipUser newUser = Membership.CreateUser(
                          NombreUsuario, Password, CorreoElectronico, "Pregunta", "Respuesta", false, out result);
                    }
                    else
                    {
                        MembershipUser newUser = Membership.CreateUser(NombreUsuario, Password, CorreoElectronico);
                    }
                }


                //Usada en código de Digitalización.
                //Response.Redirect("login.aspx");
            }
            catch (MembershipCreateUserException e)
            {
                Excepcion = GetErrorMessage(e.StatusCode);
            }
            catch (HttpException e)
            {
                Excepcion = e.Message;
            }
        }

        public string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }


        #endregion

        #region Métodos para WS Gobmx:

        public string RegistrarReferenciaMacroTramite(string IdGlobalMacroTramite, string RFC)
        {
            try
            {
                var webAddr = ConfigurationManager.AppSettings["wsGobmx_registrarRefMacroTramite"].ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.PreAuthenticate = true;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var payload = new WS_Gobmx();
                    payload.metodo = Convert.ToInt32(ConfigurationManager.AppSettings["wsGobmx_metodo"]);
                    payload.idGlobalMacroTramite = IdGlobalMacroTramite;
                    payload.idOperador = ConfigurationManager.AppSettings["wsGobmx_idOperador"].ToString();
                    payload.fechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    payload.identidad = RFC;
                    payload.ip = ConfigurationManager.AppSettings["wsGobmx_ip"].ToString();
                    payload.tipoIdentidad = Convert.ToInt32(ConfigurationManager.AppSettings["wsGobmx_tipoIdentidad"]);
                    payload.email = ConfigurationManager.AppSettings["wsGobmx_email"].ToString();

                    var JsonPayload = new JavaScriptSerializer().Serialize(payload);
                    RegistroDeError("Prueba","Payload: " + JsonPayload.ToString());
                    RegistroDeError("Prueba","URL:" + webAddr);
                    streamWriter.Write(JsonPayload);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpResponse.StatusDescription == "OK")
                {
                    var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    return "ok";
                }
                else
                {

                    RegistroDeError(httpResponse.StatusDescription, "Proyectos: Registro MacroTrámiteGobmx. Parámetros: " + httpResponse.ToString());
                    return "error";
                }
                

                

            }
            catch (WebException ex)
            {
                return "Error";
            }

        }

        public RespuestaConsultaMacroTramite ConsultarMacroTramite(string IdGlobalMacroTramite)
        {
            RespuestaConsultaMacroTramite Resultado = new RespuestaConsultaMacroTramite();
            try
            {
                var webAddr = ConfigurationManager.AppSettings["wsGobmx_consultarEstatusMacroTramite"].ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                var result = "";
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.PreAuthenticate = true;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    CatServiciosWeb Servicio = new CatServiciosWeb();
                    BusquedaMacroTramite Busqueda = new BusquedaMacroTramite();

                    Busqueda.idGlobalMacroTramite = IdGlobalMacroTramite;
                    Busqueda.todos = "1";
                    Busqueda.idOperador = ConfigurationManager.AppSettings["wsGobmx_idOperador"].ToString();
                    Busqueda.ip = ConfigurationManager.AppSettings["wsGobmx_ip"].ToString();

                    string json = new JavaScriptSerializer().Serialize(Busqueda);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                Resultado = new JavaScriptSerializer().Deserialize<RespuestaConsultaMacroTramite>(result);

                return Resultado;
            }
            catch(Exception ex)
            {
                RegistroDeError(ex.Message, "Proyecto-Tramites: Consulta de estatus de trámites de un proyecto en GobMx");
                return Resultado;
            }

        }

        public bool NotificarEstatusMacroTramite(string IdGlobalMacroTramite, string estatus, string resolucion)
        {
            bool Resultado = false;
            try
            {
                var webAddr = ConfigurationManager.AppSettings["wsGobmx_consultarEstatusMacroTramite"].ToString();
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                var result = "";
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "POST";
                httpWebRequest.PreAuthenticate = true;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    CatServiciosWeb Servicio = new CatServiciosWeb();
                    NotificarEstatusMacroTramite Notificacion = new NotificarEstatusMacroTramite();

                    Notificacion.metodo = "1";
                    Notificacion.idGlobalMacroTramite = IdGlobalMacroTramite;
                    Notificacion.estatus = estatus;
                    Notificacion.resolucion = resolucion;
                    Notificacion.idOperador = ConfigurationManager.AppSettings["wsGobmx_idOperador"].ToString();
                    Notificacion.ip = ConfigurationManager.AppSettings["wsGobmx_ip"].ToString();
                    Notificacion.fechaHora = DateTime.Now.ToString();
                    Notificacion.nota = "Enviado desde ENREL";

                    string json = new JavaScriptSerializer().Serialize(Notificacion);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                Resultado = result.Contains("Exito");
                return Resultado;
            }
            catch(Exception ex)
            {
                RegistroDeError(ex.Message, "Proyecto-Tramites: Actualización de estatus de proyecto en GobMx");
                return Resultado;
            }

        }

        #endregion

        #region Firma:

        public X509Certificate2 ObtenerCertificadoParaFirmar(IEnumerable<HttpPostedFileBase> Files, string contrasenia)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2();
                byte[] byteCer = new byte[0];
                byte[] byteKey = new byte[0];
                int num_archivo = 0;
                string ext = "";

                foreach (HttpPostedFileBase file in Files)
                {
                    ext = Path.GetExtension(file.FileName);

                    if (ext == ".cer")
                    {
                        cert = ObtenerCertificado(file);
                    }
                    else if (ext == ".key")
                    {
                        byteKey = ObtenerClavePrivada(file);
                    }
                    else { }
                }
                SecureString lSecStr = new SecureString();
                lSecStr.Clear();
                foreach (char c in contrasenia.ToCharArray())
                    lSecStr.AppendChar(c);
                RSACryptoServiceProvider Key = opensslkey.DecodeEncryptedPrivateKeyInfo(byteKey, lSecStr);
                cert.PrivateKey = Key;

                return cert;
            }
            catch (Exception ex)
            {
                return new X509Certificate2();
            }

        }

        public void SignWithThisCert(X509Certificate2 cert, Stream Archivo, string URLDocumento)
        {
            string DestPdfFileName = URLDocumento;


            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };
            IExternalSignature externalSignature = new X509Certificate2Signature(cert, "SHA-1");


            Archivo.Flush(); // Don't know if this is necessary
            Archivo.Position = 0;
            PdfReader pdfReader = new PdfReader(Archivo);
            FileStream signedPdf = new FileStream(DestPdfFileName, FileMode.Create);  //the output pdf file
            PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '\0');
            PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
            //here set signatureAppearance at your will
            signatureAppearance.Reason = ConfigurationManager.AppSettings["firma_RazonInversionista"].ToString();
            signatureAppearance.Location = ConfigurationManager.AppSettings["firma_Ubicacion"].ToString();
            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.DESCRIPTION;
            MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);
            //MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CADES);
            signedPdf.Close();

            //signedPdf = new FileStream(DestPdfFileName, FileMode.Open);
            //return signedPdf;
        }
        #endregion

        #region Comprobación de documentos

        public bool ReadByteArrayFromFile(HttpPostedFileBase file)
        {
            byte[] buffer = null;
            buffer = new byte[file.ContentLength];
            file.InputStream.Read(buffer, 0, file.ContentLength);

            try
            {
                new iTextSharp.text.pdf.PdfReader(buffer);
                return true;
            }
            catch (iTextSharp.text.exceptions.InvalidPdfException)
            {
                return false;
            }
        }

        public X509Certificate2 ObtenerCertificado(HttpPostedFileBase file)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2();
                HttpPostedFileBase cerFile = file;
                StreamReader streamReaderCer = new StreamReader(cerFile.InputStream);
                Stream streamCer = streamReaderCer.BaseStream;
                long fileLengthCer = streamCer.Length;
                byte[] byteCer = new byte[fileLengthCer];
                BinaryReader binaryFileCer = new BinaryReader(streamCer);
                binaryFileCer.BaseStream.Position = 0;

                for (int i = 0; i < fileLengthCer; i++)
                {
                    byteCer[i] = binaryFileCer.ReadByte();
                }
                cert.Import(byteCer, "", X509KeyStorageFlags.UserProtected);
                return cert;
            }
            catch (Exception ex)
            {
                X509Certificate2 cert = new X509Certificate2();
                return cert;
            }

        }

        public byte[] ObtenerClavePrivada(HttpPostedFileBase file)
        {
            try
            {
                HttpPostedFileBase keyFile = file;
                StreamReader streamReaderKey = new StreamReader(keyFile.InputStream);
                Stream streamKey = streamReaderKey.BaseStream;
                long fileLengthKey = streamKey.Length;
                byte[] byteKey = new byte[fileLengthKey];
                BinaryReader binaryFileKey = new BinaryReader(streamKey);
                binaryFileKey.BaseStream.Position = 0;

                for (int i = 0; i < fileLengthKey; i++)
                {
                    byteKey[i] = binaryFileKey.ReadByte();
                }
                return byteKey;
            }
            catch (Exception ex)
            {
                byte[] byteKey = new byte[0];
                return byteKey;
            }

        }

        public void ReemplazarValoresEnTexto(Microsoft.Office.Interop.Word.Application appWord, string AntiguoTexto, string NuevoTexto)
        {
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            appWord.Selection.Find.Execute(AntiguoTexto, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, NuevoTexto, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }

        #endregion

    }
}