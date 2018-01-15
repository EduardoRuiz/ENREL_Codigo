using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.Localidades
{
    public class DatosLocalidades
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Generales:

        public DataTable D_SeleccionarLocalidades(int? IdEntidadFederativa, int? IdMunicipio, int? IdLocalidad)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarLocalidades", Conexion);

            if (IdEntidadFederativa != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", IdEntidadFederativa); }
            else { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", DBNull.Value); }
            if (IdMunicipio != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", IdMunicipio); }
            else { SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipio", DBNull.Value); }
            if (IdLocalidad != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdLocalidad", IdLocalidad); }
            else { SQLComandoAuxiliar.Parameters.AddWithValue("@IdLocalidad", DBNull.Value); }

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarLocalidadesPorINEGI(int? IdEntidadFederativa, int? IdMunicipioINEGI, int? IdLocalidadINEGI)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarLocalidadesPorCP", Conexion);

            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", IdEntidadFederativa);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdMunicipioINEGI", IdMunicipioINEGI);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdLocalidadINEGI", IdLocalidadINEGI);


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