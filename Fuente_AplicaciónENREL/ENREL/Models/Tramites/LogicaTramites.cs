using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ENREL.Models.Tramites
{
    public class LogicaTramites
    {
        #region Variables:

        DatosTramites DatosAuxiliar = new DatosTramites();
        DataTable DtAuxiliar = new DataTable();
        List<CatTramites> ListaTramites = new List<CatTramites>();

        #endregion

        #region Métodos Principales:

        public List<CatTramites> L_SeleccionarProyectoTramites(int idProyecto, string IdMacroTramite, string RFCRepresentante, string OpenId)
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarProyectoTramites(idProyecto);
            List<CatTramites> ListaTramites = new List<CatTramites>();

            int DiferenciaFechaFinalFechaInicial = 0;
            int DiferenciaFechaActualFechaInicio = 0;

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow row in DtAuxiliar.Rows)
                {
                    CatTramites Tramite = new CatTramites();
                    Tramite.T_IdFase = (Int32)row["IdFase"];
                    Tramite.T_Dependencia = row["Dependencia"].ToString();
                    Tramite.T_HomoClave = row["Homoclave"].ToString();
                    Tramite.T_NombreTramite = row["NombreTramite"].ToString();
                    Tramite.T_Estatus = L_TraduceEstatus((Int32)row["IdEstatusTramite"]);
                    Tramite.T_FechaInicio = row["FechaInicio"].ToString();
                    if (row["FechaFinReal"] == null) { Tramite.T_FechaFin = ""; }
                    else { Tramite.T_FechaFin = row["FechaFinReal"].ToString(); }
                    Tramite.T_URLDependencia = row["URLTramites"].ToString();
                    Tramite.T_URLRequisitos = row["URLRequisitos"].ToString();
                    Tramite.T_InterOperabilidad = (bool)row["Interoperabilidad"];

                    if(Tramite.T_InterOperabilidad == true)
                    {
                        try
                        {
                            string IdMacroTramiteEncriptado = EncriptarString(IdMacroTramite);
                            string RFCEncriptado = EncriptarString(RFCRepresentante);
                            Tramite.T_URLDependencia = Tramite.T_URLDependencia + "?IdGlobalMacroTramite=" + IdMacroTramiteEncriptado + "&RFCRepresentante=" + RFCEncriptado + "&OpenId=" + OpenId;
                        }
                        catch(Exception ex)
                        { }

                    }
                    
                    
                    Tramite.T_PosicionX = (Int32)row["PosicionX"];
                    Tramite.T_PosicionY = (Int32)row["PosicionY"];
                    try { Tramite.T_Prorroga = (Int32)row["DiasAgregados"]; }
                    catch { }

                    if (Tramite.T_FechaFin != "" && Tramite.T_FechaFin != null)
                    {
                        //Calcular porcentaje:
                        TimeSpan ts = Convert.ToDateTime(Tramite.T_FechaFin) - Convert.ToDateTime(Tramite.T_FechaInicio);
                        DiferenciaFechaFinalFechaInicial = ts.Days;

                        ts = DateTime.Now - Convert.ToDateTime(Tramite.T_FechaInicio);
                        DiferenciaFechaActualFechaInicio = ts.Days;
                        Tramite.T_DiasTranscurridos = ts.Days;

                        try
                        {
                            Tramite.T_PorcentajeAvancePositivo = (DiferenciaFechaActualFechaInicio * 100) / DiferenciaFechaFinalFechaInicial;
                            Tramite.T_PorcentajeAvanceNegativo = 100 - Tramite.T_PorcentajeAvancePositivo;
                        }
                        catch
                        {
                            Tramite.T_PorcentajeAvancePositivo = 0;
                            Tramite.T_PorcentajeAvanceNegativo = 100;
                        }


                    }
                    else
                    {
                        Tramite.T_PorcentajeAvancePositivo = 0;
                        Tramite.T_PorcentajeAvanceNegativo = 100;

                    }
                    DatosTramites DatosTramite = new DatosTramites();
                    
                    Tramite.T_Color = DatosTramite.D_SeleccionarColorEstatusTramite((Int32)row["IdEstatusTramite"]);
                    Tramite.T_Requerido = DatosTramite.D_SeleccionarEstatusRequerido(idProyecto, Tramite.T_HomoClave);
                    ListaTramites.Add(Tramite);
                }
            }
            return ListaTramites;
        }

        public List<CatTramites> L_SeleccionarProyectoTramitesParaEnviarBPM(int idProyecto)
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarProyectoTramitesParaEnviar(idProyecto);
            List<CatTramites> ListaTramites = new List<CatTramites>();

            int DiferenciaFechaFinalFechaInicial = 0;
            int DiferenciaFechaActualFechaInicio = 0;

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow row in DtAuxiliar.Rows)
                {
                    CatTramites Tramite = new CatTramites();
                    Tramite.T_IdFase = (Int32)row["IdFase"];
                    Tramite.T_Dependencia = row["Dependencia"].ToString();
                    Tramite.T_HomoClave = row["Homoclave"].ToString();
                    Tramite.T_NombreTramite = row["NombreTramite"].ToString();
                    Tramite.T_Estatus = L_TraduceEstatus((Int32)row["IdEstatusTramite"]);
                    Tramite.T_FechaInicio = row["FechaInicio"].ToString();
                    if (row["FechaFinReal"] == null) { Tramite.T_FechaFin = ""; }
                    else { Tramite.T_FechaFin = row["FechaFinReal"].ToString(); }
                    Tramite.T_URLDependencia = row["URLTramites"].ToString();
                    Tramite.T_URLRequisitos = row["URLRequisitos"].ToString();
                    //Tramite.T_Color = row["Color"].ToString();

                    Tramite.T_PosicionX = (Int32)row["PosicionX"];
                    Tramite.T_PosicionY = (Int32)row["PosicionY"];
                    Tramite.T_ConfirmacionBPM = (bool)row["Enviado"];
                    try { Tramite.T_Prorroga = (Int32)row["DiasAgregados"]; }
                    catch { }

                    if (Tramite.T_FechaFin != "" && Tramite.T_FechaFin != null)
                    {
                        //Calcular porcentaje:
                        TimeSpan ts = Convert.ToDateTime(Tramite.T_FechaFin) - Convert.ToDateTime(Tramite.T_FechaInicio);
                        DiferenciaFechaFinalFechaInicial = ts.Days;

                        ts = DateTime.Now - Convert.ToDateTime(Tramite.T_FechaInicio);
                        DiferenciaFechaActualFechaInicio = ts.Days;
                        Tramite.T_DiasTranscurridos = ts.Days;

                        try
                        {
                            Tramite.T_PorcentajeAvancePositivo = (DiferenciaFechaActualFechaInicio * 100) / DiferenciaFechaFinalFechaInicial;
                            Tramite.T_PorcentajeAvanceNegativo = 100 - Tramite.T_PorcentajeAvancePositivo;
                        }
                        catch
                        {
                            Tramite.T_PorcentajeAvancePositivo = 0;
                            Tramite.T_PorcentajeAvanceNegativo = 100;
                        }


                    }
                    else
                    {
                        Tramite.T_PorcentajeAvancePositivo = 0;
                        Tramite.T_PorcentajeAvanceNegativo = 100;

                    }
                    DatosTramites DatosTramite = new DatosTramites();
                    Tramite.T_Color = DatosTramite.D_SeleccionarColorEstatusTramite((Int32)row["IdEstatusTramite"]);
                    ListaTramites.Add(Tramite);
                }
            }
            return ListaTramites;
        }

        public bool L_SeleccionarEstatusRequerido(int IdProyecto, string Homoclave)
        {
            bool Estatus = false;
            DatosTramites DatosTramites = new DatosTramites();
            Estatus = DatosTramites.D_SeleccionarEstatusRequerido(IdProyecto, Homoclave);
            return Estatus;
        }

        public CatTramites L_DetallesTramites(int? IdTramite)
        {
            DtAuxiliar = DatosAuxiliar.D_DetallesTramite(IdTramite);

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow row in DtAuxiliar.Rows)
                {
                    CatTramites Tramite = new CatTramites();
                    Tramite.T_IdTramites = (Int32)row["IdTramite"];
                    Tramite.T_NombreTramite = row["Nombre"].ToString();
                    Tramite.T_HomoClave = row["Homoclave"].ToString();
                    Tramite.T_TiempoRecepcionDocumentos = (Int32)row["TiempoRecepcionDocumentos"];
                    Tramite.T_TiempoResolucion = (Int32)row["TiempoResolucion"];
                    Tramite.T_URLDependencia = row["URLTramites"].ToString();
                    Tramite.T_URLRequisitos = row["URLRequisitos"].ToString();
                    Tramite.T_Dependencia = row["Dependencia"].ToString();
                    Tramite.T_CorreoResponsable = row["CorreoResponsable"].ToString();
                    Tramite.T_CorreoSuperior = row["CorreoSuperior"].ToString();
                    Tramite.T_PorcentajeAlertaA = (Int32)row["PorcentajeAlertaA"];
                    Tramite.T_PorcentajeAlertaB = (Int32)row["PorcentajeAlertaB"];
                    Tramite.T_PorcentajeAlertaC = (Int32)row["PorcentajeAlertaC"];
                    Tramite.T_Descripcion = row["Descripcion"].ToString();
                    Tramite.T_RegistroCOFEMER = row["RegistroCOFEMER"].ToString();
                    Tramite.T_Activo = Convert.ToBoolean(row["Activo"]);

                    ListaTramites.Add(Tramite);
                }
            }
            return ListaTramites[0];
        }

        public CatAvanceTramites L_AvanceTramites(List<CatTramites> ListaTramites)
        {
            CatAvanceTramites AvanceTramites = new CatAvanceTramites();
            AvanceTramites.T_TramitesFinalizados = 0;
            AvanceTramites.T_TramitesTotales = 0;

            foreach(CatTramites Tramite in ListaTramites)
            {
                if (Tramite.T_Requerido == true)
                {
                    if (Tramite.T_Estatus == "AUTORIZADO")
                    {
                        AvanceTramites.T_TramitesFinalizados++;
                        AvanceTramites.T_TramitesTotales++;
                    }
                    else
                    {
                        AvanceTramites.T_TramitesTotales++;
                    }
                }
            }
            
            return AvanceTramites;
        }

        public List<CatTramites> L_SeleccionarTramites()
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarTramites();
            List<CatTramites> ListaTramites = new List<CatTramites>();

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow row in DtAuxiliar.Rows)
                {
                    CatTramites Tramite = new CatTramites();
                    Tramite.T_IdTramites = (Int32)row["IdTramite"];
                    Tramite.T_Dependencia = row["Dependencia"].ToString();
                    Tramite.T_HomoClave = row["Homoclave"].ToString();
                    Tramite.T_NombreTramite = row["NombreTramite"].ToString();                  
                    Tramite.T_URLDependencia = row["URLTramites"].ToString();
                    Tramite.T_URLRequisitos = row["URLRequisitos"].ToString();
                    Tramite.T_CorreoResponsable = row["CorreoResponsable"].ToString();
                    Tramite.T_CorreoResponsable = row["CorreoSuperior"].ToString();
                    Tramite.T_Activo = Convert.ToBoolean(row["Activo"]);

                    ListaTramites.Add(Tramite);
                }
            }
            return ListaTramites;
        }

        public int L_InsertarTramite(CatTramites NuevoTramite)
        {
            int IdTramite;
            DatosTramites DatosTramites = new DatosTramites();
            IdTramite = DatosTramites.D_InsertarTramite(NuevoTramite);
            return IdTramite;
        }

        public bool L_ActualizarTramite(CatTramites NuevoTramite)
        {
            bool respuesta = false;
            DatosTramites DatosTramites = new DatosTramites();
            respuesta = DatosTramites.D_ActualizarTramite(NuevoTramite);
            return respuesta;
        }

        public bool L_EliminarTramite(int IdTramite)
        {
            DatosTramites DatosTecnologias = new DatosTramites();
            DatosTecnologias.D_EliminarTramite(IdTramite);
            return true;
        }

        #endregion

        #region Métodos Auxiliares:

        private string L_TraduceEstatus(int IdTramite)
        {
            switch (IdTramite)
            {
                case 1: return "INHABILITADO";
                case 2: return "HABILITADO";
                case 3: return "ENVIADO";
                case 4: return "RECIBIDO";
                case 5: return "INICIADO";
                case 6: return "EN PROCESO";
                case 7: return "DETENIDO";
                case 8: return "PREVENCION";
                case 9: return "AUTORIZADO";
                case 10: return "DENEGADO";
                case 11: return "RECHAZADO";
                case 12: return "CANCELADO";
                case 13: return "PRORROGA";
                case 15: return "AUTORIZADO";
                case 16: return "DENEGADO";
                default: return "DESCONOCIDO";
            }
        }

        private string L_SeleccionarColorEstatus(int IdEstatusTramite)
        {
            return DatosAuxiliar.D_SeleccionarColorEstatusTramite(IdEstatusTramite);
        }

        public string L_ActualizarEstatusTramite(RespuestaConsultaMacroTramite RespuestaConsultaMacroTramite, int IdProyecto)
        {
            foreach (CadenaInteroperabilidad CadenaInteroperabilidad in RespuestaConsultaMacroTramite.cadenaInteroperabilidad)
            {
                foreach (EstatusTramite EstatusTramite in CadenaInteroperabilidad.listaEstatus)
                {
                    int TamañoHomoclave = CadenaInteroperabilidad.idGlobalTramite.Length - 15 + 1;
                    string HomoclaveAsignada = CadenaInteroperabilidad.idGlobalTramite.ToString().Substring(0, TamañoHomoclave);
                    int respuesta = DatosAuxiliar.D_ActualizarEstatusTramite(EstatusTramite, IdProyecto, HomoclaveAsignada, CadenaInteroperabilidad.idGlobalTramite.ToString());
                }
            }

            return "ok";
        }

        public string L_ActualizarEstatusTramiteDesdeENREL(int IdProyecto, string Homoclave)
        {
            DatosAuxiliar.D_ActualizarEstatusTramiteEnviado(IdProyecto, Homoclave);

            return "ok";
        }

        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

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

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private string EncriptarString(string input)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes("L1SP1984");

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);


            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }



        #endregion

        #region Reportes

        public List<CatTramites> L_ReporteTramites(int idProyecto)
        {
            DatosTramites DatosTramite = new DatosTramites();
            DataTable dtTramites = DatosTramite.D_ReporteTramites(idProyecto);
            List<CatTramites> ListaTramites = new List<CatTramites>();

            if (dtTramites.Rows.Count > 0)
            {
                foreach (DataRow row in dtTramites.Rows)
                {
                    CatTramites Tramite = new CatTramites();
                    Tramite.T_HomoClave = row["Homoclave"].ToString();
                    Tramite.T_NombreTramite = row["NombreTramite"].ToString();
                    Tramite.T_Estatus = row["Estatus"].ToString();
                    Tramite.T_FechaInicio = row["FechaInicio"].ToString();
                    Tramite.T_NombreProyecto = row["nombreProyecto"].ToString();

                    ListaTramites.Add(Tramite);
                }
            }
            return ListaTramites;
        }

        public List<CatEstatusTramite> L_TablaEstatusTramite()
        {
            DatosTramites DatosTramite = new DatosTramites();
            DataTable dtTramites = DatosTramite.D_TablaEstatus();
            List<CatEstatusTramite> ListaEstatusTramite = new List<CatEstatusTramite>();

            if (dtTramites.Rows.Count > 0)
            {
                foreach (DataRow row in dtTramites.Rows)
                {
                    CatEstatusTramite EstatusTramite = new CatEstatusTramite();
                    EstatusTramite.T_NombreEstatus = row["Nombre"].ToString();
                    EstatusTramite.T_ColorEstatus = row["Color"].ToString();

                    ListaEstatusTramite.Add(EstatusTramite);
                }
            }
            return ListaEstatusTramite;
        }

        #endregion
    }
}