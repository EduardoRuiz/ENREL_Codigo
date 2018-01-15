using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace wsENREL_Dependencias.Models
{
    public class LogicaENRELDependencias
    {
        #region Variables:

        DatosENREL_Dependencias DatosAuxiliar = new DatosENREL_Dependencias();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos principales:

        public RespuestaENREL ConsultarDatos(string IdGlobal, string RFC_Representate)
        {
            RespuestaENREL Respuesta = new RespuestaENREL();
            Respuesta.Mensaje = "Error";
            try
            {
                string IdGloblDesencriptado = DesencriptarString(IdGlobal);
                string RFCRepresentanteDesencriptado = DesencriptarString(RFC_Representate);
                Respuesta.Empresa = L_DetallesEmpresa(IdGloblDesencriptado);
                Respuesta.RepresentanteLegal = L_DetallesRepresentanteLegal(RFCRepresentanteDesencriptado, IdGloblDesencriptado);
                Respuesta.Proyecto = L_DetallesProyecto(IdGloblDesencriptado);

                if(Respuesta.Empresa.RFC.Length == null  || Respuesta.RepresentanteLegal.CURP.Length == null)
                {
                    Respuesta.Empresa = null;
                    Respuesta.RepresentanteLegal = null;
                    Respuesta.Proyecto = null;
                    Respuesta.Mensaje = "No se encontraron coincidencias para los datos proporcionados.";
                }
                else
                {
                    Respuesta.Mensaje = "Ok";
                }
                
                return Respuesta;
            }
            catch(Exception ex)
            {
                Respuesta.Empresa = null;
                Respuesta.RepresentanteLegal = null;
                Respuesta.Proyecto = null;
                return Respuesta;
            }
        }

        public Empresa L_DetallesEmpresa(string IdGlobal)
        {

            Empresa Empresa = new Empresa();

            DtAuxiliar = DatosAuxiliar.D_DetallesEmpresas(IdGlobal);
            if (DtAuxiliar.Rows.Count > 0)
            {
                Empresa.NombreComercial = DtAuxiliar.Rows[0]["NombreComercial"].ToString();
                Empresa.RazonSocial = DtAuxiliar.Rows[0]["RazonSocial"].ToString();
                Empresa.RFC = DtAuxiliar.Rows[0]["RFC"].ToString();

                Empresa.Lada = DtAuxiliar.Rows[0]["Lada"].ToString();
                Empresa.TelefonoFijo = DtAuxiliar.Rows[0]["TelefonoFijo"].ToString();
                Empresa.CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();

                try
                { Empresa.CodigoPostal = DtAuxiliar.Rows[0]["CodigoPostal"].ToString(); }
                catch (Exception ex) { };
                Empresa.Localidad = DtAuxiliar.Rows[0]["Localidad"].ToString();
                Empresa.Municipio = DtAuxiliar.Rows[0]["Municipio"].ToString();
                Empresa.EntidadFederativa = DtAuxiliar.Rows[0]["EntidadFederativa"].ToString();

                Empresa.TipoAsentamiento = DtAuxiliar.Rows[0]["TipoAsentamiento"].ToString();
                Empresa.Colonia = DtAuxiliar.Rows[0]["Colonia"].ToString();
                Empresa.TipoVialidad = DtAuxiliar.Rows[0]["TipoVialidad"].ToString(); ;
                Empresa.Calle = DtAuxiliar.Rows[0]["Calle"].ToString();
                Empresa.NumeroExterior = DtAuxiliar.Rows[0]["NumeroExterior"].ToString();
                try
                { Empresa.NumeroInterior = DtAuxiliar.Rows[0]["NumeroInterior"].ToString(); }
                catch (Exception ex) { };
            }

            return Empresa;
        }

        public Proyecto L_DetallesProyecto(string IdGlobal)
        {

            Proyecto Proyecto = new Proyecto();

            DtAuxiliar = DatosAuxiliar.D_DetallesProyecto(IdGlobal);
            if (DtAuxiliar.Rows.Count > 0)
            {
                Proyecto.P_IdGlobalMacroTramite = DtAuxiliar.Rows[0]["IdGlobalMacroTramite"].ToString();
                try { Proyecto.P_Tecnologia = DtAuxiliar.Rows[0]["Tecnologia"].ToString(); }
                catch { }
               
                Proyecto.P_CodigoPostal = DtAuxiliar.Rows[0]["CodigoPostal"].ToString();
                Proyecto.P_EntidadFederativa = DtAuxiliar.Rows[0]["EntidadFederativa"].ToString();
                Proyecto.P_Municipio = DtAuxiliar.Rows[0]["Municipio"].ToString();
                Proyecto.P_Localidad = DtAuxiliar.Rows[0]["Localidad"].ToString();
                Proyecto.P_IdEntidadFederativa = (Int32)DtAuxiliar.Rows[0]["IdEntidadFederativa"];
                Proyecto.P_IdMunicipio = (Int32)DtAuxiliar.Rows[0]["IdMunicipio"];
                Proyecto.P_IdLocalidad = (Int32)DtAuxiliar.Rows[0]["IdLocalidad"];
                try { Proyecto.P_IdTipoAsentamiento = (Int32)DtAuxiliar.Rows[0]["IdTipoColonia"]; }
                catch { }
                Proyecto.P_Colonia = DtAuxiliar.Rows[0]["Colonia"].ToString();
                Proyecto.P_EstatusProyecto = DtAuxiliar.Rows[0]["EstatusProyecto"].ToString();
                Proyecto.P_NombreProyecto = DtAuxiliar.Rows[0]["NombreProyecto"].ToString();
                Proyecto.P_Latitud = (double)DtAuxiliar.Rows[0]["Latitud"];
                Proyecto.P_Longitud = (double)DtAuxiliar.Rows[0]["Longitud"];
                Proyecto.P_CapacidadInstalada = (double)DtAuxiliar.Rows[0]["CapacidadInstalada"];
                Proyecto.P_GeneracionAnual = (double)DtAuxiliar.Rows[0]["GeneracionAnual"];
                Proyecto.P_FactorPlanta = (double)DtAuxiliar.Rows[0]["FactorPlanta"];
                Proyecto.P_MontoInversion = (double)DtAuxiliar.Rows[0]["MontoInversion"];
                Proyecto.P_FechaRegistro = (DateTime)DtAuxiliar.Rows[0]["FechaRegistro"];
            }

            return Proyecto;
        }

        public RepresentanteLegal L_DetallesRepresentanteLegal(string RFC, string IdGlobal)
        {
            DtAuxiliar = DatosAuxiliar.D_DetallesRepresentanteLegal(RFC, IdGlobal);
            RepresentanteLegal Representante = new RepresentanteLegal();
            if (DtAuxiliar.Rows.Count > 0)
            {
                Representante.Nombre = DtAuxiliar.Rows[0]["Nombre"].ToString();
                Representante.PrimerApellido = DtAuxiliar.Rows[0]["PrimerApellido"].ToString();
                Representante.SegundoApellido = DtAuxiliar.Rows[0]["SegundoApellido"].ToString();
                Representante.CURP = DtAuxiliar.Rows[0]["CURP"].ToString();
                Representante.RFC = DtAuxiliar.Rows[0]["RFC"].ToString();
                Representante.TipoVialidad = DtAuxiliar.Rows[0]["TipoVialidad"].ToString();
                Representante.Calle = DtAuxiliar.Rows[0]["Calle"].ToString();
                Representante.NumeroExterior = DtAuxiliar.Rows[0]["NumeroExterior"].ToString();
                try
                { Representante.NumeroInterior = DtAuxiliar.Rows[0]["NumeroInterior"].ToString(); }
                catch (Exception ex) { };
                Representante.TipoAsentamiento = DtAuxiliar.Rows[0]["TipoAsentamiento"].ToString();
                Representante.Colonia = DtAuxiliar.Rows[0]["Colonia"].ToString();
                try
                { Representante.CodigoPostal = DtAuxiliar.Rows[0]["CodigoPostal"].ToString(); }
                catch (Exception ex) { };
                Representante.Localidad = DtAuxiliar.Rows[0]["Localidad"].ToString();
                Representante.Municipio = DtAuxiliar.Rows[0]["Municipio"].ToString();
                Representante.EntidadFederativa = DtAuxiliar.Rows[0]["EntidadFederativa"].ToString();

                Representante.Lada = DtAuxiliar.Rows[0]["Lada"].ToString();
                Representante.TelefonoFijo = DtAuxiliar.Rows[0]["TelefonoFijo"].ToString();
                try
                { Representante.ExtensionTelefonica = DtAuxiliar.Rows[0]["ExtensionTelefonica"].ToString(); }
                catch (Exception ex) { };
                try
                { Representante.TelefonoCelular = DtAuxiliar.Rows[0]["TelefonoMovil"].ToString(); }
                catch (Exception ex) { };
                Representante.CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();
            }

            return Representante;
        }

        public string L_ValidarIP(string IP)
        {
            string respuesta = "";

            DtAuxiliar = DatosAuxiliar.D_SeleccionarIPsValidadas(IP);

            if(DtAuxiliar.Rows.Count > 0)
            {
                respuesta = DtAuxiliar.Rows[0]["Dependencia"].ToString();
            }

            return respuesta;
        }

        #endregion

        #region Métodos Auxiliares

        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public string DesencriptarString(string input)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes("L1SP1984");
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        #endregion

    }
}