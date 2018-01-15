using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.Tramites
{
    public class DatosTramites
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();


        #endregion

        #region Métodos Principales:

        public DataTable D_SeleccionarProyectoTramites(int idProyecto)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarProyectoTramites", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", idProyecto);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarProyectoTramitesParaEnviar(int idProyecto)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarProyectoTramitesParaEnviar", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", idProyecto);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public bool D_SeleccionarEstatusRequerido(int IdProyecto, string Homoclave)
        {
            bool Estatus = false;
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarEstatusRequerido", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Homoclave", Homoclave);
            SQLComandoAuxiliar.Parameters.Add("@Estatus", SqlDbType.Bit).Direction = ParameterDirection.Output;
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            Estatus = (bool)SQLComandoAuxiliar.Parameters["@Estatus"].Value;
            SQLComandoAuxiliar.Connection.Dispose();
            return Estatus;
        }

        public DataTable D_SeleccionarTramites()
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarTramites", Conexion);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public void D_EliminarTramite(int IdTramite)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarTramite", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTramite", IdTramite);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
        }

        public int D_InsertarTramite(CatTramites NuevoTramite)
        {
            int IdTecnologia;
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarTramite", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", NuevoTramite.T_NombreTramite);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Homoclave", NuevoTramite.T_HomoClave);
            SQLComandoAuxiliar.Parameters.AddWithValue("@TiempoRecepcionDocumentos", NuevoTramite.T_TiempoRecepcionDocumentos);
            SQLComandoAuxiliar.Parameters.AddWithValue("@TiempoResolucion", NuevoTramite.T_TiempoResolucion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Descripcion", NuevoTramite.T_Descripcion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RegistroCOFEMER", NuevoTramite.T_RegistroCOFEMER);
            SQLComandoAuxiliar.Parameters.AddWithValue("@URLTramites", NuevoTramite.T_URLDependencia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@URLRequisitos", NuevoTramite.T_URLRequisitos);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Dependencia", NuevoTramite.T_Dependencia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoResponsable", NuevoTramite.T_CorreoResponsable);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoSuperior", NuevoTramite.T_CorreoSuperior);
            SQLComandoAuxiliar.Parameters.AddWithValue("@PorcentajeAlertaA", NuevoTramite.T_PorcentajeAlertaA);
            SQLComandoAuxiliar.Parameters.AddWithValue("@PorcentajeAlertaB", NuevoTramite.T_PorcentajeAlertaB);
            SQLComandoAuxiliar.Parameters.AddWithValue("@PorcentajeAlertaC", NuevoTramite.T_PorcentajeAlertaC);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoDias", NuevoTramite.T_IdTipoDia);
            SQLComandoAuxiliar.Parameters.Add("@IdTramite", SqlDbType.Int).Direction = ParameterDirection.Output;
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            IdTecnologia = (int)SQLComandoAuxiliar.Parameters["@IdTramite"].Value;
            SQLComandoAuxiliar.Connection.Dispose();
            return IdTecnologia;
        }

        public bool D_ActualizarTramite(CatTramites NuevoTramite)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarTramite", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", NuevoTramite.T_NombreTramite);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Homoclave", NuevoTramite.T_HomoClave);
            SQLComandoAuxiliar.Parameters.AddWithValue("@TiempoRecepcionDocumentos", NuevoTramite.T_TiempoRecepcionDocumentos);
            SQLComandoAuxiliar.Parameters.AddWithValue("@TiempoResolucion", NuevoTramite.T_TiempoResolucion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Descripcion", NuevoTramite.T_Descripcion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RegistroCOFEMER", NuevoTramite.T_RegistroCOFEMER);
            SQLComandoAuxiliar.Parameters.AddWithValue("@URLTramites", NuevoTramite.T_URLDependencia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@URLRequisitos", NuevoTramite.T_URLRequisitos);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Dependencia", NuevoTramite.T_Dependencia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoResponsable", NuevoTramite.T_CorreoResponsable);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoSuperior", NuevoTramite.T_CorreoSuperior);
            SQLComandoAuxiliar.Parameters.AddWithValue("@PorcentajeAlertaA", NuevoTramite.T_PorcentajeAlertaA);
            SQLComandoAuxiliar.Parameters.AddWithValue("@PorcentajeAlertaB", NuevoTramite.T_PorcentajeAlertaB);
            SQLComandoAuxiliar.Parameters.AddWithValue("@PorcentajeAlertaC", NuevoTramite.T_PorcentajeAlertaC);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoDias", NuevoTramite.T_IdTipoDia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTramite", NuevoTramite.T_IdTramites);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return true;
        }

        #endregion

        #region Métodos Auxiliares:

        #endregion

        public DataTable D_AvanceTramites(int idProyecto)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarAvanceTramites", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", idProyecto);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_DetallesTramite(int? IdTramite)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpDetallesTramites", Conexion);
            if (IdTramite != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdTramite", IdTramite); }
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_TablaEstatus()
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarEstatusTramiteCompletos", Conexion);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public string D_SeleccionarColorEstatusTramite(int IdEstatusTramite)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarColorEstatus", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEstatusTramite", IdEstatusTramite);
            SQLComandoAuxiliar.Parameters.Add("@Color", SqlDbType.VarChar,8).Direction = ParameterDirection.Output;
            SQLComandoAuxiliar.ExecuteNonQuery();
            string Color  = SQLComandoAuxiliar.Parameters["@Color"].Value.ToString();            
            SQLComandoAuxiliar.Connection.Dispose();
            return Color;
        }

        public int D_ActualizarEstatusTramite(EstatusTramite EstatusTramite, int IdProyecto, string HomoclaveAsignada, string idGlobalTramite)
        {
            int IdTecnologia;
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            int IdEstatusTramiteENREL;
            string Observacion;
            int Prorroga;

            switch (EstatusTramite.estatus)
            {
                case "ENVIADO":
                    IdEstatusTramiteENREL = 3;
                    if (EstatusTramite.nota != null) { Observacion = EstatusTramite.nota; } else { Observacion = "Sin información"; };
                    Prorroga = 0;
                    break;
                case "RECIBIDO":
                    IdEstatusTramiteENREL = 4;
                    if (EstatusTramite.nota != null) { Observacion = EstatusTramite.nota; } else { Observacion = "Sin información"; };
                    Prorroga = 0;
                    break;
                case "INICIADO":
                    IdEstatusTramiteENREL = 5;
                    if (EstatusTramite.nota != null) { Observacion = EstatusTramite.nota; } else { Observacion = "Sin información"; };
                    Prorroga = 0;
                    break;
                case "RECHAZADO":
                    IdEstatusTramiteENREL = 11;
                    if (EstatusTramite.nota != null) { Observacion = EstatusTramite.nota; } else { Observacion = "Sin información"; };
                    Prorroga = 0;
                    break;
                case "EN PROCESO":
                    IdEstatusTramiteENREL = 6;
                    if (EstatusTramite.nota != null) { Observacion = EstatusTramite.nota; } else { Observacion = "Sin información"; };
                    Prorroga = 0;
                    break;
                case "DETENIDO":
                    IdEstatusTramiteENREL = 7;
                    if (EstatusTramite.nota != null) { Observacion = EstatusTramite.nota; } else { Observacion = "Sin información"; };
                    Prorroga = 0;
                    break;
                case "CANCELADO":
                    IdEstatusTramiteENREL = 12;
                    if (EstatusTramite.nota != null) { Observacion = EstatusTramite.nota; } else { Observacion = "Sin información"; };
                    Prorroga = 0;
                    break;
                case "TERMINADO":
                    string opcion = EstatusTramite.resolucion.ToUpper();
                    switch (opcion)
                    {
                        case "ACEPTADO": IdEstatusTramiteENREL = 9; break;
                        case "RECHAZADO": IdEstatusTramiteENREL = 16; break;
                        default: IdEstatusTramiteENREL = 14; break;
                    }
                    Observacion = EstatusTramite.nota;
                    Prorroga = 0;
                    break;
                case "PREVENCION":
                    IdEstatusTramiteENREL = 8;
                    Observacion = "Se agregaron " + EstatusTramite.nota + " días de prórroga";
                    Prorroga = 0;
                    //Prorroga = Convert.ToInt16(EstatusTramite.nota);
                    break;
                case "PRORROGA":
                    IdEstatusTramiteENREL = 13;
                    Observacion = "Se agregaron " + EstatusTramite.nota + " días de prórroga";
                    Prorroga = 0;
                    try { Prorroga = Convert.ToInt32(EstatusTramite.nota); }
                    catch { }
                    break;
                default:
                    IdEstatusTramiteENREL = 14;
                    if (EstatusTramite.nota != null) { Observacion = EstatusTramite.nota; } else { Observacion = "Sin información"; };
                    Prorroga = 0;
                    break;
            }

            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarEstatusTramite", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@HomoclaveAsignada", HomoclaveAsignada);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEstatus", IdEstatusTramiteENREL);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Resolutivo", EstatusTramite.resolucion);
            string Fecha = (Convert.ToDateTime(EstatusTramite.fechaRegistro)).ToString("dd-MM-yyyy HH:mm:ss");
            SQLComandoAuxiliar.Parameters.AddWithValue("@FechaRealDeActividad", Fecha);
            //SQLComandoAuxiliar.Parameters.AddWithValue("@FechaRealDeActividad", DateTime.ParseExact(EstatusTramite.fechaRegistro,"dd-MM-yyyy HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture));            
            SQLComandoAuxiliar.Parameters.AddWithValue("@Observacion", Observacion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@DiasAgregados", Prorroga);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdGlobalTramite", idGlobalTramite);

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return 1;
        }

        public int D_ActualizarEstatusTramiteEnviado(int IdProyecto, string Homoclave)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();


            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarEstatusTramiteEnviado", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Homoclave", Homoclave);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEstatus", 3);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Resolutivo", "PENDIENTE");
            string Fecha = (DateTime.Now).ToString("dd-MM-yyyy HH:mm:ss");
            SQLComandoAuxiliar.Parameters.AddWithValue("@FechaRealDeActividad", Fecha);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Observacion", "Enviado desde ENREL");
            SQLComandoAuxiliar.Parameters.AddWithValue("@DiasAgregados", 0);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdGlobalTramite", "Pendiente");

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return 1;
        }

        #region Reportes

        public DataTable D_ReporteTramites(int idProyecto)
        {
            DataTable dtTramites = new DataTable();
            dtTramites.Rows.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpReporteTramites", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", idProyecto);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            adp.Fill(dtTramites);
            SQLComandoAuxiliar.Connection.Dispose();

            return dtTramites;
        }

        #endregion
    }
}