using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.Municipios
{
    public class DatosMunicipios
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Generales:

        public DataTable D_SeleccionarMunicipios( int? IdEntidadFederativa, int? IdMunicipio)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarMunicipios", Conexion);

            if (IdEntidadFederativa != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", IdEntidadFederativa); }
            else { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", DBNull.Value); }
            if (IdMunicipio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", IdMunicipio); }
            else { SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", DBNull.Value); }

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarMunicipiosPorCP( int? IdEntidadFederativa, int? IdMunicipio)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarMunicipiosPorCP", Conexion);

            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", IdEntidadFederativa);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", IdMunicipio);

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