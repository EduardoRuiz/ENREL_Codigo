using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ENREL.Models.Graficas
{
    public class DatosGraficas
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Principales:

        #endregion

        #region Métodos Auxiliares:

        #endregion

        public DataTable D_GraficoProyectosPorTecnologia(int? IdEntidadFed, int? IdEmpresa, int? idMuncipio, DateTime? FechaInicio, DateTime? FechaFin, int? idEstatusProyecto)
        {
            DtAuxiliar.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpGraficoProyectosPorTecnologia", Conexion);
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

        public DataTable D_GraficoProyectosPorEntidadFederativa(int? IdEntidadFed, int? IdEmpresa, int? idMuncipio, DateTime? FechaInicio, DateTime? FechaFin, int? idEstatusProyecto)
        {
            DtAuxiliar.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpGraficoProyectosPorEntidadFederativa", Conexion);
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

        public DataTable D_GraficaInversionPorTecnologia(DateTime? FechaInicio, DateTime? FechaFin)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpGraficoInversionPorTecnologia", Conexion);
            if (FechaInicio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechainicio", FechaInicio); }
            if (FechaFin != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechafin", FechaFin); }
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            adp.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public DataTable D_GraficaInversionPorEntidadFederativa(DateTime? FechaInicio, DateTime? FechaFin)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpGraficoInversionPorEntidadFederativa", Conexion);
            if (FechaInicio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechainicio", FechaInicio); }
            if (FechaFin != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechafin", FechaFin); }
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            adp.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public DataTable D_GraficoEmpresasPorTecnologia(int? IdEntidadFed, int? IdMunicipio, DateTime? FechaInicio, DateTime? FechaFin, int? IdTecnologia)
        {
            DtAuxiliar.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpGraficoEmpresasPorTecnologia", Conexion);
            if (IdEntidadFed != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", IdEntidadFed); }
            if (IdTecnologia != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia); }
            if (IdMunicipio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", IdMunicipio); }
            if (FechaInicio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechainicio", FechaInicio); }
            if (FechaFin != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechafin", FechaFin); }
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            adp.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public DataTable D_GraficoEmpresasPorEntidadFederativa(int? IdEntidadFed, int? IdMunicipio, DateTime? FechaInicio, DateTime? FechaFin, int? IdTecnologia)
        {
            DtAuxiliar.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpGraficoEmpresasPorEntidadFederativa", Conexion);
            if (IdEntidadFed != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", IdEntidadFed); }
            if (IdTecnologia != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia); }
            if (IdMunicipio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", IdMunicipio); }
            if (FechaInicio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechainicio", FechaInicio); }
            if (FechaFin != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@fechafin", FechaFin); }
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            adp.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

    }
}