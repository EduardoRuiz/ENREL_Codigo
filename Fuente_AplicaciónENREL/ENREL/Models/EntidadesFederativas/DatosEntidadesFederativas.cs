using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.EntidadesFederativas
{
    public class DatosEntidadesFederativas
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Generales:

        public DataTable D_SeleccionarEntidadesFederativas(int? IdEntidadFederativa)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarEntidadesFederativas", Conexion);

            if (IdEntidadFederativa != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", IdEntidadFederativa); }


            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarEntidadesFederativasPorCP(int? IdCodigoPostal)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarEntidadesFederativasPorCP", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdCodigoPostal", IdCodigoPostal);

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarEntidadesFederativasPorCPLimitado(int? IdCodigoPostal)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarEntidadesFederativasPorCPLimitado", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdCodigoPostal", IdCodigoPostal);

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        #endregion

        #region Métodos Auxiliares:

        #endregion
    }
}