using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;

namespace VER.Crypto
{
    public class OCSP
    {
        public enum CertificateStatus { good, revoked, unknown };

        private readonly double MaxClockSkew = 360000000000;

        public CertificateStatus validateOcsp(X509Certificate clientCert, X509Certificate issuerCert, out string respMsg)
        {
            string url = "http://www.sat.gob.mx/OCSP";
            OcspReq req = generateOcspRequest(issuerCert, clientCert.SerialNumber);
            byte[] binaryResp = IoUtils.PostData(url, req.GetEncoded(), "application/ocsp-request", "application/ocsp-response");
            
            return processOcspResponse(clientCert, issuerCert, binaryResp, out respMsg);
        }

        private CertificateStatus processOcspResponse(X509Certificate clientCert, X509Certificate issuerCert, byte[] binaryResp, out string respMsg)
        {
            OcspResp r = new OcspResp(binaryResp);
            CertificateStatus cStatus = CertificateStatus.unknown;

            switch (r.Status)
            {
                case OcspRespStatus.Successful:
                    
                    BasicOcspResp or = (BasicOcspResp)r.GetResponseObject();
                    validateResponse(or, issuerCert);
                    respMsg = string.Empty;

                    if (or.Responses.Length == 1)
                    {
                        SingleResp resp = or.Responses[0];

                        validateCertificateId(issuerCert, clientCert, resp.GetCertID());
                        validateThisUpdate(resp);
                        validateNextUpdate(resp);

                        Object certificateStatus = resp.GetCertStatus();

                        if (certificateStatus == Org.BouncyCastle.Ocsp.CertificateStatus.Good)
                        {
                            cStatus = CertificateStatus.good;
                            respMsg = "Certificado Válido";
                        }
                        else if (certificateStatus is RevokedStatus)
                        {
                            cStatus = CertificateStatus.revoked;
                            respMsg = "Certificado Revocado";
                        }
                        else if (certificateStatus is UnknownStatus)
                        {
                            cStatus = CertificateStatus.unknown;
                            respMsg = "Certificado Desconocido";
                        }
                    }
                    break;

                default:
                    respMsg = "No se ha podido procesar la respuesta OCSP";
                    //throw new Exception("Status Desconocido '" + r.Status + "'.");
                    return cStatus;
            }
            
            return cStatus;
        }

        private void validateResponse(BasicOcspResp or, X509Certificate issuerCert)
        {
            validateResponseSignature(or, issuerCert.GetPublicKey());
            validateSignerAuthorization(issuerCert, or.GetCerts()[0]);
        }

        //3. The identity of the signer matches the intended recipient of the
        //request.
        //4. The signer is currently authorized to sign the response.
        private void validateSignerAuthorization(X509Certificate issuerCert, X509Certificate signerCert)
        {
            // This code just check if the signer certificate is the same that issued the ee certificate
            // See RFC 2560 for more information
            if (!(issuerCert.IssuerDN.Equivalent(signerCert.IssuerDN) && issuerCert.SerialNumber.Equals(signerCert.SerialNumber)))
            {
                //throw new Exception("Invalid OCSP signer");
            }
        }

        //2. The signature on the response is valid;
        private void validateResponseSignature(BasicOcspResp or, Org.BouncyCastle.Crypto.AsymmetricKeyParameter asymmetricKeyParameter)
        {
            if (!or.Verify(asymmetricKeyParameter))
            {
               // throw new Exception("Invalid OCSP signature");
            }
        }

        //6. When available, the time at or before which newer information will
        //be available about the status of the certificate (nextUpdate) is
        //greater than the current time.
        private void validateNextUpdate(SingleResp resp)
        {
            if (resp.NextUpdate != null && resp.NextUpdate.Value != null && resp.NextUpdate.Value.Ticks <= DateTime.Now.Ticks)
            {
                throw new Exception("Invalid next update.");
            }
        }

        //5. The time at which the status being indicated is known to be
        //correct (thisUpdate) is sufficiently recent.
        private void validateThisUpdate(SingleResp resp)
        {
            if (Math.Abs(resp.ThisUpdate.Ticks - DateTime.Now.Ticks) > MaxClockSkew)
            {
                throw new Exception("Max clock skew reached.");
            }
        }

        //1. The certificate identified in a received response corresponds to
        //that which was identified in the corresponding request;
        private void validateCertificateId(X509Certificate issuerCert, X509Certificate eeCert, CertificateID certificateId)
        {
            CertificateID expectedId = new CertificateID(CertificateID.HashSha1, issuerCert, eeCert.SerialNumber);

            if (!expectedId.SerialNumber.Equals(certificateId.SerialNumber))
            {
                throw new Exception("Invalid certificate ID in response");
            }

            if (!Org.BouncyCastle.Utilities.Arrays.AreEqual(expectedId.GetIssuerNameHash(), certificateId.GetIssuerNameHash()))
            {
                throw new Exception("Invalid certificate Issuer in response");
            }

        }

        private OcspReq generateOcspRequest(X509Certificate issuerCert, BigInteger serialNumber)
        {
            CertificateID id = new CertificateID(CertificateID.HashSha1, issuerCert, serialNumber);
            return generateOcspRequest(id);
        }

        private OcspReq generateOcspRequest(CertificateID id)
        {
            OcspReqGenerator ocspRequestGenerator = new OcspReqGenerator();

            ocspRequestGenerator.AddRequest(id);

            BigInteger nonce = BigInteger.ValueOf(new DateTime().Ticks);

            ArrayList oids = new ArrayList();
            Hashtable values = new Hashtable();

            oids.Add(OcspObjectIdentifiers.PkixOcsp);

            Asn1OctetString asn1 = new DerOctetString(new DerOctetString(new byte[] { 1, 3, 6, 1, 5, 5, 7, 48, 1, 1 }));

            values.Add(OcspObjectIdentifiers.PkixOcsp, new X509Extension(false, asn1));
            ocspRequestGenerator.SetRequestExtensions(new X509Extensions(oids, values));

            return ocspRequestGenerator.Generate();
        }
    }
}
