using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.CodigosPostales
{
    public class DatosCodigosPostales
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Principales:

        public DataTable D_SeleccionarCodigosPostales(int? IdCodigoPostal)
        {
            DtAuxiliar.Rows.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarCodigosPostales", Conexion);
            if (IdCodigoPostal != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdCodigoPostal", IdCodigoPostal); }

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter SqlDataAdapterAuxiliar = new SqlDataAdapter(SQLComandoAuxiliar);
            SqlDataAdapterAuxiliar.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarUbicacionPorCP(int? IdCodigoPostal)
        {
            DtAuxiliar.Rows.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarUbicacionPorCP", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdCodigoPostal", IdCodigoPostal);

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter SqlDataAdapterAuxiliar = new SqlDataAdapter(SQLComandoAuxiliar);
            SqlDataAdapterAuxiliar.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        #endregion

        #region Métodos Auxiliares:



        #endregion
    }
}