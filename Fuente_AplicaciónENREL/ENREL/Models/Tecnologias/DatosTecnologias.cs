using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ENREL.Models.Tecnologias
{
    public class DatosTecnologias
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Generales:

        public DataTable D_SeleccionarTecnologias()
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarTecnologias", Conexion);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public int D_InsertarTecnologia(CatTecnologias NuevaTecnologia)
        {
            int IdTecnologia;
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarTecnologias", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@NombreTecnologia", NuevaTecnologia.Tecnologia);
            SQLComandoAuxiliar.Parameters.Add("@IdTecnologia", SqlDbType.Int).Direction = ParameterDirection.Output;
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            IdTecnologia = (int)SQLComandoAuxiliar.Parameters["@IdTecnologia"].Value;
            SQLComandoAuxiliar.Connection.Dispose();
            return IdTecnologia;
        }

        public void D_InsertarTecnologiaPreguntas(int IdTecnologia, string Pregunta)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarTecnologiaPreguntas", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Pregunta", Pregunta);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
        }

        public void D_InsertarTecnologiaTramites(int IdTecnologia, int IdFase, int IdTramite, int IdPosicion)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarTecnologiaTramite", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdFase", IdFase);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTramite", IdTramite);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdPosicion", IdPosicion);

            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
        }

        public bool D_ActualizarTecnologia(CatTecnologias NuevaTecnologia)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarTecnologias", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@NombreTecnologia", NuevaTecnologia.Tecnologia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", NuevaTecnologia.IdTecnologia);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return true;
        }

        public void D_ActualizarTecnologiaPregunta(int IdTecnologia, string pregunta)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarTecnologiaPreguntas", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Pregunta", pregunta);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
        }

        public void D_ActualizarTecnologiaTramites(int IdTecnologia, int IdFase, int IdTramite, int IdPosicion)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarTecnologiaTramite", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdFase", IdFase);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTramite", IdTramite);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdPosicion", IdPosicion);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
        }

        public void D_EliminarTecnologia(int IdTecnologia)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarTecnologias", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
        }

        public void D_EliminarTecnologiaPreguntas(CatTecnologias Tecnologia)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarTecnologiaPreguntas", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", Tecnologia.IdTecnologia);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
        }

        public void D_EliminarTecnologiaPreguntasPorTecnologia(int IdTecnologia)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarTecnologiaPreguntasPorTecnologia", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
        }

        public void D_EliminarTecnologiaTramitesPorTecnologia(int IdTecnologia)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarTecnologiaTramitesPorTecnologia", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
        }

        public DataTable D_DetallesTecnologia(int IdTecnologia)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpDetallesTecnologia", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }

        public DataTable D_SeleccionarTecnologiaPreguntas(int IdTecnologia)
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

        public DataTable D_SeleccionarTecnologiaTramites(int IdTecnologia)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarTecnologiaTramites", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTecnologia", IdTecnologia);
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