using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ENREL.Models.DiasInhabiles
{
    public class DatosDiasInhabiles
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Principales:

        public DataTable D_SeleccionarDiasInhabiles(int Mes, int Año)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarDiasInhabiles", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Mes", Mes);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Año", Año);

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public bool D_InsertarDiaInhabil(CatDiasInhabiles DiaInhabil)
        {
            bool Respuesta = false;
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarDiaInhabil", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Dia", DiaInhabil.Dia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Mes", DiaInhabil.Mes);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Año", DiaInhabil.Año);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", 1);
            SQLComandoAuxiliar.ExecuteNonQuery();

            SQLComandoAuxiliar.Connection.Dispose();
            Respuesta = true;
            return Respuesta;
        }

        public bool D_ActualizarEstatusDiaInhabil(int IdDiaInhabil, bool IdEstatus)
        {
            bool Resultado = false;
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarEstatusDiaInhabil", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdDiaInhabil", IdDiaInhabil);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", IdEstatus);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            Resultado = true;
            return Resultado;
        }

        #endregion
    }
}