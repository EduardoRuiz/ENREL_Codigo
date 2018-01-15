using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.RepresentantesLegales
{
    public class DatosRepresentantesLegales
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Principales:

        public DataTable D_SeleccionarRepresentantesLegalesPorValidar()
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarRepresentantesLegalesPorValidar", Conexion);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public DataTable D_SeleccionarRepresentantesLegalesPorEmpresa(int IdEmpresa)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarRepresentantesLegalesPorEmpresa", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public int D_InsertarRepresentanteLegal(CatRepresentantesLegales RepresentanteLegal, int IdEmpresa)
        {
            int IdRepresentanteLegal = 0;
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarRepresentanteLegal", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", RepresentanteLegal.RL_Nombre);
            SQLComandoAuxiliar.Parameters.AddWithValue("@PrimerApellido", RepresentanteLegal.RL_PrimerApellido);
            SQLComandoAuxiliar.Parameters.AddWithValue("@SegundoApellido", RepresentanteLegal.RL_SegundoApellido);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CURP", RepresentanteLegal.RL_CURP);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RFC", RepresentanteLegal.RL_RFC);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoCalle", RepresentanteLegal.RL_IdTipoVialidad);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Calle", RepresentanteLegal.RL_Calle);
            SQLComandoAuxiliar.Parameters.AddWithValue("@NumeroExterior", RepresentanteLegal.RL_NumeroExterior);
            if (RepresentanteLegal.RL_NumeroInterior != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@NumeroInterior", RepresentanteLegal.RL_NumeroInterior); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoColonia", RepresentanteLegal.RL_IdTipoAsentamiento);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Colonia", RepresentanteLegal.RL_Colonia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdLocalidad", RepresentanteLegal.RL_IdLocalidad);
            //Cambio Código Postal:
            //if (RepresentanteLegal.RL_CodigoPostal != null && RepresentanteLegal.RL_CodigoPostal > 0) { SQLComandoAuxiliar.Parameters.AddWithValue("@CodigoPostal", RepresentanteLegal.RL_CodigoPostal); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@CodigoPostal", RepresentanteLegal.RL_CodigoPostal);
            if (RepresentanteLegal.RL_Lada != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@Lada", RepresentanteLegal.RL_Lada); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@TelefonoFijo", RepresentanteLegal.RL_TelefonoFijo);
            if (RepresentanteLegal.RL_ExtensionTelefonica != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@ExtensionTelefonica", RepresentanteLegal.RL_ExtensionTelefonica); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@TelefonoMovil", RepresentanteLegal.RL_TelefonoCelular);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", RepresentanteLegal.RL_CorreoElectronico);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", true);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEstatusSolicitudRepresentante", 1);
            SQLComandoAuxiliar.Parameters.Add("@IdRepresentanteLegal", SqlDbType.Int).Direction = ParameterDirection.Output;
            SQLComandoAuxiliar.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;
            //SQLComandoAuxiliar.Transaction = transaction;
            SQLComandoAuxiliar.ExecuteNonQuery();

            IdRepresentanteLegal = (int)SQLComandoAuxiliar.Parameters["@IdRepresentanteLegal"].Value;

            SQLComandoAuxiliar.Connection.Dispose();

            if (IdRepresentanteLegal > 1)
            {
                Guid DatoAleatorio = new Guid();
                DatoAleatorio = Guid.NewGuid();
                string NombreUsuario = DatoAleatorio.ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));
                string PasswordEncryptado = MetodoGeneral.EncriptarPassword("Temporal0");

                Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarUsuario", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoUsuario", 2);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", NombreUsuario);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Password", PasswordEncryptado);
                SQLComandoAuxiliar.Parameters.AddWithValue("@PasswordReal", "");
                SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", RepresentanteLegal.RL_CorreoElectronico);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdRepresentanteAsociado", IdRepresentanteLegal);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", false);
                SQLComandoAuxiliar.Parameters.AddWithValue("@CreadoDuranteRegistro", true);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Ingreso", false);
                SQLComandoAuxiliar.ExecuteNonQuery();
            }

            return IdRepresentanteLegal;
        }

        public DataTable D_DetallesRepresentanteLegal(int IdRepresentanteLegal)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpDetallesRepresentanteLegal", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdRepresentanteLegal", IdRepresentanteLegal);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        public bool D_ActualizarRepresentanteLegal(CatRepresentantesLegales RepresentanteLegal)
        {
            bool Resultado = false;
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarRepresentanteLegal", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", RepresentanteLegal.RL_Nombre);
            SQLComandoAuxiliar.Parameters.AddWithValue("@PrimerApellido", RepresentanteLegal.RL_PrimerApellido);
            SQLComandoAuxiliar.Parameters.AddWithValue("@SegundoApellido", RepresentanteLegal.RL_SegundoApellido);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CURP", RepresentanteLegal.RL_CURP);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RFC", RepresentanteLegal.RL_RFC);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoCalle", RepresentanteLegal.RL_IdTipoVialidad);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Calle", RepresentanteLegal.RL_Calle);
            SQLComandoAuxiliar.Parameters.AddWithValue("@NumeroExterior", RepresentanteLegal.RL_NumeroExterior);
            if (RepresentanteLegal.RL_NumeroInterior != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@NumeroInterior", RepresentanteLegal.RL_NumeroInterior); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoColonia", RepresentanteLegal.RL_IdTipoAsentamiento);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Colonia", RepresentanteLegal.RL_Colonia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdLocalidad", RepresentanteLegal.RL_IdLocalidad);
            //Cambio código postal
            //if (RepresentanteLegal.RL_CodigoPostal != null && RepresentanteLegal.RL_CodigoPostal > 0) { SQLComandoAuxiliar.Parameters.AddWithValue("@CodigoPostal", RepresentanteLegal.RL_CodigoPostal); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@CodigoPostal", RepresentanteLegal.RL_CodigoPostal);
            if (RepresentanteLegal.RL_Lada != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@Lada", RepresentanteLegal.RL_Lada); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@TelefonoFijo", RepresentanteLegal.RL_TelefonoFijo);
            if (RepresentanteLegal.RL_ExtensionTelefonica != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@ExtensionTelefonica", RepresentanteLegal.RL_ExtensionTelefonica); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@TelefonoMovil", RepresentanteLegal.RL_TelefonoCelular);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", RepresentanteLegal.RL_CorreoElectronico);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", RepresentanteLegal.RL_Activo);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEstatusSolicitudRepresentante", RepresentanteLegal.RL_IdEstatusSolicitudRepresentante);
            if (RepresentanteLegal.RL_Observaciones != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@Observaciones", RepresentanteLegal.RL_Observaciones); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdRepresentanteLegal", RepresentanteLegal.RL_IdRepresentanteLegal);


            SQLComandoAuxiliar.ExecuteNonQuery();

            SQLComandoAuxiliar.Connection.Dispose();
            Resultado = true;
            return Resultado;
        }

        public bool D_EliminarRepresentanteLegal(int IdRepresentante)
        {
            bool Resultado = false;
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarRepresentanteLegal", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdRepresentante", IdRepresentante);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            Resultado = true;
            return Resultado;
        }

        #endregion

        #region Métodos Auxiliares:

        public DataTable D_DetallesRepresentanteLegalPorRFC(string RFCRepresentante)
        {
            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();

            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpDetallesRepresentantePorRFC", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RFCRepresentanteLegal", RFCRepresentante);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();

            return DtAuxiliar;
        }

        #endregion 
    }
}