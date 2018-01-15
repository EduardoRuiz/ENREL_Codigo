using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.Empresas
{
    public class DatosEmpresas
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Principales:

        public DataTable D_SeleccionarEmpresas()
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarEmpresas", Conexion);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_DetallesEmpresas(int IdEmpresa)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpDetallesEmpresa", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public DataTable D_DetallesEmpresasPorRFC(string RFC)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpDetallesEmpresaPorRFC", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RFC", RFC);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public bool D_ActualizarEmpresa(CatEmpresas Empresa)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarEmpresa", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@NombreComercial", Empresa.E_NombreComercial);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RazonSocial", Empresa.E_RazonSocial);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RFC", Empresa.E_RFC);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoCalle", Empresa.E_IdTipoVialidad);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Calle", Empresa.E_Calle);
            SQLComandoAuxiliar.Parameters.AddWithValue("@NumeroExterior", Empresa.E_NumeroExterior);
            SQLComandoAuxiliar.Parameters.AddWithValue("@NumeroInterior", Empresa.E_NumeroInterior);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoColonia", Empresa.E_IdTipoAsentamiento);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Colonia", Empresa.E_Colonia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdLocalidad", Empresa.E_IdLocalidad);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CodigoPostal", Empresa.E_CodigoPostal);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Lada", Empresa.E_Lada);
            SQLComandoAuxiliar.Parameters.AddWithValue("@TelefonoFijo", Empresa.E_TelefonoFijo);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", Empresa.E_CorreoElectronico);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", Empresa.E_IdEmpresa);

            SQLComandoAuxiliar.ExecuteNonQuery();
            SQLComandoAuxiliar.Connection.Dispose();

            return true;
        }

        public bool D_EliminarEmpresa(int IdEmpresa)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarEmpresa", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return true;
        }

        #endregion

        #region Métodos Auxiliares:

        #endregion

        #region Métodos Consultor:

        public DataTable D_ReporteEmpresas(int? IdEntidadFed, int? IdMunicipio, DateTime? FechaInicio, DateTime? FechaFin, int? IdTecnologia)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("spReporteEmpresas", Conexion);
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
        
        public DataTable ListaEmpresas()
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEmpresas", Conexion);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            adp.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }


        #endregion


    }
}