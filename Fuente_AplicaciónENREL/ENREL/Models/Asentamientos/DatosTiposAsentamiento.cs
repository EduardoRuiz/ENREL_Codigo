using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.Asentamientos
{
    public class DatosTiposAsentamiento
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Generales:

        public DataTable D_SeleccionarTiposAsentamiento()
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarTiposAsentamiento", Conexion);
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