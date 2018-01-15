using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Security;


namespace VER.Crypto
{
    public class XMLSign
    {
        public static string DecryptEncryptedData(string PathToPrivateKeyFile)
        {
            X509Certificate2 myCertificate;
            try
            {
                myCertificate = new X509Certificate2(PathToPrivateKeyFile);
            }
            catch (CryptographicException ex)
            {
                throw new CryptographicException("Imposible abrir archivo de llave pública." + ex.Message);
            } RSACryptoServiceProvider rsaObj;
            if (myCertificate.HasPrivateKey)
            {
                rsaObj = (RSACryptoServiceProvider)myCertificate.PrivateKey;
            }
               
            else throw new CryptographicException("La llave privada no está contenidad en el certificado.");
            if (rsaObj == null) return String.Empty;
            // byte[] decryptedBytes; 
            try
            {
                //      decryptedBytes = rsaObj.Decrypt(Convert.FromBase64String(Base64EncryptedData), false); 
            }
            catch
            {
                throw new CryptographicException("Imposible desencriptar datos.");
            }
            //    Check to make sure we decrpyted the string 
            // if(decryptedBytes.Length == 0)  
            return String.Empty;
            // else       
            //    return System.Text.Encoding.UTF8.GetString(decryptedBytes); 
        }
        /// <summary>
        /// Verificar la firma del XML Firmado
        /// </summary>
        /// <param name="Doc">Xml con la firma</param>
        /// <returns></returns>
        public static Boolean VerifyXml(XmlDocument Doc)
        {


            if (Doc == null)
                throw new ArgumentException("No se encontró información en el Documento.");
            //, RSA Key
            XmlNodeList CertNode = Doc.GetElementsByTagName("X509Certificate");
            if (CertNode.Count > 0)
            {
                X509Certificate2 myCertificate = new X509Certificate2(System.Text.ASCIIEncoding.ASCII.GetBytes(CertNode[0].InnerText));

                KeyInfo keyInfo = new KeyInfo();
                keyInfo.AddClause(new KeyInfoX509Data(myCertificate));

                CertNode = null;

                SignedXml signedXml = new SignedXml(Doc);

                // Find the "Signature" node and create a new
                // XmlNodeList object.
                XmlNodeList nodeList = Doc.GetElementsByTagName("Signature");

                // Throw an exception if no signature was found.
                if (nodeList.Count <= 0)
                {
                    throw new CryptographicException("Falló la Verificación: No se encontró la firma en el documento.");
                }

                // This example only supports one signature for
                // the entire XML document.  Throw an exception 
                // if more than one signature was found.
                if (nodeList.Count >= 2)
                {
                    throw new CryptographicException("Falló la Verificación: Se encontró más de una firma en el documento.");
                }

                // Load the first <signature> node.  
                signedXml.LoadXml((XmlElement)nodeList[0]);
                signedXml.KeyInfo = keyInfo;
                // Check the signature and return the result.
                return signedXml.CheckSignature();
            }
            else
            {
                throw new CryptographicException("Falló la Verificación: No se encontró la información del Certificado.");

            }
        }
        /// <summary>
        /// Firmar documento XML con la llave privada del usuario
        /// </summary>
        /// <param name="xmlDoc">XML que se va a firmar</param>
        /// <param name="U_Password">Contraseña del sello</param>
        /// <param name="RutaKey">ruta de la llave privada</param>
        /// <param name="RutaCer">ruta de la llave publica</param>
        /// <returns></returns>
        public XmlDocument SignXml(XmlDocument xmlDoc, String U_Password, String RutaKey, String RutaCer)
        {
            Byte[] pLlavePrivadaenBytes = System.IO.File.ReadAllBytes(RutaKey);
            SecureString lSecStr = new SecureString();
            lSecStr.Clear();
            foreach (char c in U_Password.ToCharArray())
                lSecStr.AppendChar(c);
            RSACryptoServiceProvider Key = JavaScience.opensslkey.DecodeEncryptedPrivateKeyInfo(pLlavePrivadaenBytes, lSecStr);
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (Key == null)
                throw new ArgumentException("Key");

            SignedXml signedXml = new SignedXml(xmlDoc);

            signedXml.SigningKey = Key;
            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            X509Certificate2 cert = new X509Certificate2();
            cert.Import(RutaCer, "", X509KeyStorageFlags.UserProtected);           
            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data kdata = new KeyInfoX509Data(cert);
            X509IssuerSerial xserial;
            xserial.IssuerName = cert.IssuerName.Name.ToString();
            xserial.SerialNumber = cert.SerialNumber;
            kdata.AddIssuerSerial(xserial.IssuerName, xserial.SerialNumber);

            keyInfo.AddClause(kdata);

            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            return xmlDoc;

        }

        /// <summary>
        /// Firmar documento XML con la llave privada del usuario
        /// </summary>
        /// <param name="xmlDoc">XML que se va a firmar</param>
        /// <param name="byteCer">ByteArray de la llave publica</param>
        /// <param name="byteKey">ByteArray de la llave privada</param>
        /// <param name="U_Password">Contraseña del sello</param>
        /// <returns></returns>
        public static XmlDocument SignXml(XmlDocument xmlDoc, Byte[] byteCer, Byte[] byteKey, String U_Password)
        {
            SecureString lSecStr = new SecureString();
            lSecStr.Clear();
            foreach (char c in U_Password.ToCharArray())
                lSecStr.AppendChar(c);
            RSACryptoServiceProvider Key = JavaScience.opensslkey.DecodeEncryptedPrivateKeyInfo(byteKey,lSecStr);
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (Key == null)
                throw new ArgumentException("Key");

            SignedXml signedXml = new SignedXml(xmlDoc);

            signedXml.SigningKey = Key;
            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            X509Certificate2 cert = new X509Certificate2();
            cert.Import(byteCer, "", X509KeyStorageFlags.UserProtected);
            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data kdata = new KeyInfoX509Data(cert);
            X509IssuerSerial xserial;
            xserial.IssuerName = cert.IssuerName.Name.ToString();
            xserial.SerialNumber = cert.SerialNumber;
            kdata.AddIssuerSerial(xserial.IssuerName, xserial.SerialNumber);

            keyInfo.AddClause(kdata);

            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            return xmlDoc;
        }

        /// <summary>
        /// Firmar Datos con la llave privada del usuario
        /// </summary>
        /// <param name="dataToBeSigned">Datos a Firmar</param>
        /// <param name="byteKey">ByteArray de la llave privada</param>
        /// <param name="U_Password">Contraseña de la llave privada</param>
        /// <returns></returns>
        public static byte[] SignData(byte[] dataToBeSigned, byte[] byteKey, string U_Password)
        {
            try
            {
                SecureString lSecStr = new SecureString();
                lSecStr.Clear();
                foreach (char c in U_Password.ToCharArray())
                    lSecStr.AppendChar(c);

                //Create a RSA Provider, using the private key
                RSACryptoServiceProvider rsaCryptoServiceProvider = JavaScience.opensslkey.DecodeEncryptedPrivateKeyInfo(byteKey, lSecStr);

                //Sign the data using a desired hashing algorithm
                return rsaCryptoServiceProvider.SignData(dataToBeSigned, new SHA1CryptoServiceProvider());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<byte[]> Sign(string text, String U_Password, String RutaKey, String RutaCer)
        {
            // Access Personal (MY) certificate store of current user
            Byte[] pLlavePrivadaenBytes = System.IO.File.ReadAllBytes(RutaKey);
            SecureString lSecStr = new SecureString();
            lSecStr.Clear();
            foreach (char c in U_Password.ToCharArray())
                lSecStr.AppendChar(c);
            RSACryptoServiceProvider csp = JavaScience.opensslkey.DecodeEncryptedPrivateKeyInfo(pLlavePrivadaenBytes, lSecStr);
            if (csp == null)
            {
                throw new Exception("No se encontró un certificado válido.");
            }

            // Hash the data
            SHA1Managed sha1 = new SHA1Managed();
            UnicodeEncoding encoding = new UnicodeEncoding();

            X509Certificate2 cert = new X509Certificate2();
            cert.Import(RutaCer, "", X509KeyStorageFlags.UserProtected);


            byte[] data = encoding.GetBytes(text);
            byte[] hash = sha1.ComputeHash(data);
            byte[] dataCript = csp.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
            /*FirmaElectronica F = new FirmaElectronica()
            {
                Hash = hash,
                Firma = dataCript
              ,
                SelloDigital = cert.GetPublicKey()
              ,
                AlgoritmoHash = AlgoritmosHash.SHA1
            };
            Anexo Anx = new Anexo() { AlgoritmoHash = AlgoritmosHash.SHA1, HashArchivo = hash };
            Anx.FirmaElectronicaArchivo.Add(F);

            return Anx;
             */

            List<byte[]> returnList = new List<byte[]>();
            returnList.Add(hash);
            returnList.Add(dataCript);
            returnList.Add(cert.GetPublicKey());
            return returnList;
            // Sign the hash
        }

        public static bool Verify(string text, byte[] signature, string certPath)
        {
            // Load the certificate we'll use to verify the signature from a file 
            X509Certificate2 cert = new X509Certificate2(certPath);
            // Note: 
            // If we want to use the client cert in an ASP.NET app, we may use something like this instead:
            // X509Certificate2 cert = new X509Certificate2(Request.ClientCertificate.Certificate);

            // Get its associated CSP and public key
            RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PublicKey.Key;

            // Hash the data
            SHA1Managed sha1 = new SHA1Managed();
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] data = encoding.GetBytes(text);
            byte[] hash = sha1.ComputeHash(data);

            // Verify the signature with the hash
            return csp.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signature);
        }
    }
}