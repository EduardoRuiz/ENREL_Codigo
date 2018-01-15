#region "using"

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;
using VER.Crypto;
using VER.Crypto.JavaScience;
//using VER.SIGAS.PWD;
using Org.BouncyCastle.X509;
using X509Certificate = System.Security.Cryptography.X509Certificates.X509Certificate;
using System.Threading;

#endregion

namespace VER.Crypto
{
    public class ValidaCertificado
    {
        #region class Members

        private Hashtable _h;
        private Byte[] _byteKey;
        private Byte[] _byteCer;
        private string _sPwd;
        private string _sRfc;
        private string _sSubject;
        private string _sNuevoPwd;

        #endregion

        #region "properties"

        public string guidDocumento { get; private set; }

        public string guidProceso { get; private set; }

        public string msgProcFirmado { get; private set; }

        #endregion

        #region "Validación Certificado SENER"

        public string validacionSenerCert(string certRfc, Byte[] byteCer, Byte[] byteKey, string password)
        {
            X509Certificate2 cert = new X509Certificate2();
            cert.Import(byteCer, "", X509KeyStorageFlags.UserProtected);

            DateTime fechaActual = DateTime.Now;
            DateTime validoDesde = cert.NotBefore;
            DateTime validoHasta = cert.NotAfter;

            if (validoDesde > fechaActual)
            {
                msgProcFirmado = "Fecha " + validoDesde.ToString("dd/MM/yyyy") + " de Inicio de Certificado No es Válida";
                return msgProcFirmado;
            }
            if (validoHasta < fechaActual)
            {
                msgProcFirmado = "Certificado expirado.";
                return msgProcFirmado;
            }

            SecureString lSecStr = new SecureString();
            lSecStr.Clear();
            foreach (char c in password.ToCharArray())
                lSecStr.AppendChar(c);
            RSACryptoServiceProvider Key = opensslkey.DecodeEncryptedPrivateKeyInfo(byteKey, lSecStr);
			
			msgProcFirmado = "TODOS";
            if (Key == null)
            {
                msgProcFirmado = "La contraseña de la clave privada no es válida.";
                return msgProcFirmado;
            }
            else
            {
                _sSubject = cert.SubjectName.Name;

                if (_sSubject != null)
                {
                    //Encontrar la cadena correspondiente al RFC dentro del subject y determinar si se trata de una FIEL de persona moral o física
                    String[] arrSubject = _sSubject.Split(new[] { ',' });
                    string cadenaRfc = arrSubject[1];
                    String[] arrRFC = cadenaRfc.Split(new[] { '=' });
                    int indiceRfc = cadenaRfc.IndexOf("=");
                    string certificadoRfc = "";
                    

                    if(arrRFC[1].Length > 20)
                    {
                        //RFC compuesto (persona moral)
                        certificadoRfc = cadenaRfc.Substring(indiceRfc + 16, 13);
                    }
                    else
                    {
                        //RFC de persona física
                        certificadoRfc = cadenaRfc.Substring(indiceRfc + 1, 13);
                    }
                    


                    if (certificadoRfc.ToUpper() != certRfc.ToUpper())
                    {
                        msgProcFirmado = "El RFC " + certRfc.ToUpper() + " no corresponde con el del certificado.";
                        return msgProcFirmado;
                    }
                    else
                    {
                        return "ok";    
                    }
                }
                else
                {
                    msgProcFirmado = "Error al leer datos del certificado.";
                    return msgProcFirmado;
                }
            }
        }

        #endregion

        #region "Validación Certificado SAT"

        public bool validacionSatCert(Byte[] byteCer)
        {

            bool respuesta = false;
            //DAL dal = new DAL();
            X509CertificateParser clientCertParser = new X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate clientCert = clientCertParser.ReadCertificate(byteCer);

            OCSP validaOcsp = new OCSP();
            string respuestaOcsp;
            string pathIssuerCert;
            X509CertificateParser issuerCertParser = new X509CertificateParser();
            FileStream fs;
            
            //DirectoryInfo di = new DirectoryInfo(@"C:\Users\jesusalejandro.ramos\Documents\19735\Firma\Certificados");
            //System.IO.DriveInfo di = new System.IO.DriveInfo(@"C:\Users\jesusalejandro.ramos\Documents\19735\Firma\Certificados");
            DirectoryInfo di = new DirectoryInfo(@"C:\inetpub\DocumentosAplicacionENREL\Certificados");
            Console.WriteLine("No search pattern returns:");
            foreach (var fi in di.GetFiles())
            {
                pathIssuerCert = fi.FullName;
                //Console.WriteLine("{0}: {1}: {2}", fi.Name, fi.LastAccessTime, fi.Length);

                //fs = new FileStream("C:\\Users\\jesusalejandro.ramos\\Documents\\19735\\Firma\\Certificados\\pruebaSAT\\AC3_SAT.cer", FileMode.Open);
                fs = new FileStream(pathIssuerCert, FileMode.Open);
                Org.BouncyCastle.X509.X509Certificate issuerCert = issuerCertParser.ReadCertificate(fs);
                fs.Close();

                validaOcsp.validateOcsp(clientCert, issuerCert, out respuestaOcsp);
                msgProcFirmado = respuestaOcsp;

                if (msgProcFirmado.Contains("Válido"))
                {
                    respuesta =  true;
                    break;
                }
                else
                {
                    Thread.Sleep(500);
                }

                msgProcFirmado = "No se pudo realizar la validación OCSP";

            }

            return respuesta;
        }

        #endregion

        
    }
}

