using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.Proyectos
{
    public class DatosProyectos
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Generales:

        public DataTable D_SeleccionarProyectos(int IdEmpresa)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarProyectos", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarEstatusProyectos()
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarEstatusProyecto", Conexion);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_DetallesProyectos(int IdProyecto)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpDetallesProyecto", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public int D_InsertarProyectos(CatProyectos Proyecto, int IdUsuario)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarProyectos", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", Proyecto.P_IdEmpresa);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", Proyecto.P_IdTecnologia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", Proyecto.P_IdMunicipio);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", Proyecto.P_NombreProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Latitud", Proyecto.P_Latitud);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Longitud", Proyecto.P_Longitud);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CapacidadInstalada", Proyecto.P_CapacidadInstalada);
            SQLComandoAuxiliar.Parameters.AddWithValue("@GeneracionAnual", Proyecto.P_GeneracionAnual);
            SQLComandoAuxiliar.Parameters.AddWithValue("@FactorPlanta", Proyecto.P_FactorPlanta);
            SQLComandoAuxiliar.Parameters.AddWithValue("@MontoInversion", Proyecto.P_MontoInversion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdLocalidad", Proyecto.P_IdLocalidad);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CodigoPostal", Proyecto.P_CodigoPostal);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Unidades", Proyecto.P_Unidades);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdUsuario", IdUsuario);
            if (Proyecto.P_IdTipoAsentamiento != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@idTipoColonia", Proyecto.P_IdTipoAsentamiento); }
            if (Proyecto.P_Colonia != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@Colonia", Proyecto.P_Colonia); }
            SQLComandoAuxiliar.Parameters.Add("@IdProyectoAsignado", SqlDbType.Int).Direction = ParameterDirection.Output;

            SQLComandoAuxiliar.ExecuteNonQuery();

            int IdProyecto = (int)SQLComandoAuxiliar.Parameters["@IdProyectoAsignado"].Value;
            SQLComandoAuxiliar.Connection.Dispose();
            return IdProyecto;
        }

        public bool D_ActualizarProyectos(CatProyectos Proyecto)
        {
            bool Resultado = false;
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarProyecto", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", Proyecto.P_IdProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", Proyecto.P_IdTecnologia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", Proyecto.P_IdMunicipio);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", Proyecto.P_NombreProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Latitud", Proyecto.P_Latitud);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Longitud", Proyecto.P_Longitud);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CapacidadInstalada", Proyecto.P_CapacidadInstalada);
            SQLComandoAuxiliar.Parameters.AddWithValue("@GeneracionAnual", Proyecto.P_GeneracionAnual);
            SQLComandoAuxiliar.Parameters.AddWithValue("@FactorPlanta", Proyecto.P_FactorPlanta);
            SQLComandoAuxiliar.Parameters.AddWithValue("@MontoInversion", Proyecto.P_MontoInversion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdLocalidad", Proyecto.P_IdLocalidad);
            if (Proyecto.P_IdTipoAsentamiento != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@idTipoColonia", Proyecto.P_IdTipoAsentamiento); }
            if (Proyecto.P_Colonia != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@Colonia", Proyecto.P_Colonia); }

            SQLComandoAuxiliar.ExecuteNonQuery();

            SQLComandoAuxiliar.Connection.Dispose();
            Resultado = true;
            return Resultado;
        }

        public bool D_EliminarProyectos(int IdProyecto)
        {
            bool Resultado = false;
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarProyectos", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            Resultado = true;
            return Resultado;
        }

        #endregion

        #region Métodos Auxiliares:

        public DataTable D_SeleccionarPreguntas(int IdTecnologia)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarTecnologiaPreguntas", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarProyectoPreguntas(int IdProyecto)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarProyectoPreguntas", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarDatosProyectoTramite(int IdProyecto, string Homoclave)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarDatosProyectoTramite", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Homoclave", Homoclave);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public bool D_ActualizarProyectoPreguntas(int IdProyecto, int IdPregunta, bool Respuesta)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarProyectoPreguntas", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdPregunta", IdPregunta);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Respuesta", Respuesta);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SQLComandoAuxiliar.Connection.Dispose();
            return true;
        }

        public bool D_ResetProyectoPreguntas(int IdProyecto)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpResetProyectoPreguntas", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SQLComandoAuxiliar.Connection.Dispose();
            return true;
        }

        public bool D_ActualizarProyectoSeguimiento(int IdProyecto, int IdProyectoEstatus)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarProyectoSeguimiento", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEstatusProyecto", IdProyectoEstatus);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SQLComandoAuxiliar.Connection.Dispose();
            return true;
        }

        public string D_SeleccionarProyectoSeguimiento(int IdProyecto)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarProyectoSeguimiento", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.Parameters.Add("@IdSeguimiento", SqlDbType.VarChar,20).Direction = ParameterDirection.Output;

            SQLComandoAuxiliar.ExecuteNonQuery();

            string IdSeguimiento = SQLComandoAuxiliar.Parameters["@IdSeguimiento"].Value.ToString();
            SQLComandoAuxiliar.Connection.Dispose();
            return IdSeguimiento;
        }

        public bool D_ActualizarAvanceProyecto(int IdProyecto, float Avance)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarAvanceProyecto", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Avance", Avance);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SQLComandoAuxiliar.Connection.Dispose();
            return true;
        }

        public DataTable D_SeleccionarDatosNotificacionInicioTramite(int IdProyecto, string Homoclave, string RFCRL)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarDatosAlertaInicioTramite", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdProyecto", IdProyecto);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Homoclave", Homoclave);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RFCRL", RFCRL);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        #endregion

        #region Reportes

        public DataTable D_ReporteProyectos(int? IdEntidadFed, int? IdEmpresa, int? idMuncipio, DateTime? FechaInicio, DateTime? FechaFin, int? idEstatusProyecto)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpReporteProyecto", Conexion);
            if (IdEmpresa != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa); }
            if (IdEntidadFed != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", IdEntidadFed); }
            if (idMuncipio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", idMuncipio); }
            if (FechaInicio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechainicio", FechaInicio); }
            if (FechaFin != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechafin", FechaFin); }
            if (idEstatusProyecto != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEstatusProyecto", idEstatusProyecto); }
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            adp.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        #endregion
    }
}