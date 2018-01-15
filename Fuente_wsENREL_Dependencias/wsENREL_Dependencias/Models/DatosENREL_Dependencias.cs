using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wsENREL_Dependencias.Models
{
    public class DatosENREL_Dependencias
    {
        #region Variables:

        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Método Principales:

        #endregion

        #region Métodos principales:

        public DataTable D_DetallesEmpresas(string IdGlobal)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = EstablecerConexionBD();
            SQLComandoAuxiliar = CrearLlamadaStoredProcedure("SpDetallesEmpresaParaWS", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdGlobal", IdGlobal);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public DataTable D_DetallesRepresentanteLegal(string RFC, string IdGlobal)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = EstablecerConexionBD();
            SQLComandoAuxiliar = CrearLlamadaStoredProcedure("SpDetallesRepresentanteLegalParaWS", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RFCRepresentanteLegal", RFC);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdGlobal", IdGlobal);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public DataTable D_DetallesProyecto(string IdGlobal)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = EstablecerConexionBD();
            SQLComandoAuxiliar = CrearLlamadaStoredProcedure("SpDetallesProyectoParaWS", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdGlobal", IdGlobal);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public DataTable D_SeleccionarIPsValidadas(string IP)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = EstablecerConexionBD();
            SQLComandoAuxiliar = CrearLlamadaStoredProcedure("SeleccionarIPsValidadas", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IP", IP);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        #endregion

        #region Métodos auxiliares para BD:

        public SqlConnection EstablecerConexionBD()
        {
            string CadenaConexion = ConfigurationManager.ConnectionStrings["CadenaDeConexion"].ConnectionString;
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
    }
}