using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.Usuarios
{
    public class DatosUsuarios
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Principales:

            public DataTable D_SeleccionarUsuariosSENER(int? IdTipoUsuario, int? IdEstatus)
            {
                DtAuxiliar.Rows.Clear();
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarUsuariosSENER", Conexion);

                if (IdTipoUsuario != null)
                { SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoUsuario", IdTipoUsuario); }
                if (IdEstatus != null)
                { SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", IdEstatus); }

                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
                adp.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();
                return DtAuxiliar;
            }

            public DataTable D_SeleccionarUsuarioPorNombre(string NombreUsuario, string U_Password)
            {
                string passwordEncriptado = MetodoGeneral.EncriptarPassword(U_Password);
                DtAuxiliar.Rows.Clear();
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarUsuarioPorNombre", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Password", passwordEncriptado);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter SqlDataAdapterAuxiliar = new SqlDataAdapter(SQLComandoAuxiliar);
                SqlDataAdapterAuxiliar.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();
                return DtAuxiliar;
            }

            public DataTable D_SeleccionarUsuarioPorOpenId(string OpenId)
            {
                DtAuxiliar.Rows.Clear();
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarUsuarioPorOpenId", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@OpenId", OpenId);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter SqlDataAdapterAuxiliar = new SqlDataAdapter(SQLComandoAuxiliar);
                SqlDataAdapterAuxiliar.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();
                return DtAuxiliar;
            }

        public string D_ActualizarOpenId(int IdUsuario)
            {
                string OpenIdSinEncriptar = IdUsuario.ToString() + DateTime.Now.ToString();
                string passwordOpenIdEncriptado = MetodoGeneral.EncriptarPassword(OpenIdSinEncriptar);
                DtAuxiliar.Rows.Clear();
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarOpenId", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                SQLComandoAuxiliar.Parameters.AddWithValue("@OpenId", passwordOpenIdEncriptado);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SQLComandoAuxiliar.Connection.Dispose();
                return passwordOpenIdEncriptado;
            }

            public DataTable D_SeleccionarUsuariosDelRegistro(int IdEmpresa, int IdTipoUsuario)
            {
                DtAuxiliar.Rows.Clear();
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarUsuariosDelRegistro", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoUsuario", IdTipoUsuario);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter SqlDataAdapterAuxiliar = new SqlDataAdapter(SQLComandoAuxiliar);
                SqlDataAdapterAuxiliar.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();
                return DtAuxiliar;
            }

            public DataTable D_SeleccionarUsuariosPorEmpresa(int IdEmpresa, bool Activo)
            {
                DtAuxiliar.Rows.Clear();
                DtAuxiliar.Columns.Clear();

                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarUsuariosPorEmpresa", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", Activo);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
                dr.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();

                return DtAuxiliar;
            }

            public DataTable D_SeleccionarUsuariosPorNombreUnicamente(string Nombre)
            {
                DtAuxiliar.Rows.Clear();
                DtAuxiliar.Columns.Clear();

                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarUsuariosPorNombreUnicamente", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", Nombre);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
                dr.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();

                return DtAuxiliar;
            }

            public DataTable D_SeleccionarUsuariosPorClaveReset(string ClaveReset)
            {
                DtAuxiliar.Rows.Clear();
                DtAuxiliar.Columns.Clear();

                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarUsuariosPorClaveReset", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@ClaveReset", ClaveReset);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
                dr.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();

                return DtAuxiliar;
            }

            public DataTable SeleccionarTiposUsuarios()
            {
                DtAuxiliar.Rows.Clear();
                DtAuxiliar.Columns.Clear();

                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarTiposUsuarioSENER", Conexion);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(SQLComandoAuxiliar);
                adp.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();
                return DtAuxiliar;
            }

            public DataTable D_DetallesUsuario(int IdUsuario)
            {
                DtAuxiliar.Rows.Clear();
                DtAuxiliar.Columns.Clear();

                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpDetallesUsuario", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
                dr.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();

                return DtAuxiliar;
            }

            public bool D_InsertarUsuario(CatUsuarios UsuarioOperativo, int IdEmpresa, int IdRepresentanteLegal)
            {
                bool Respuesta = false;
                string PasswordEncryptado = "";
                PasswordEncryptado = MetodoGeneral.EncriptarPassword(UsuarioOperativo.U_Password);

                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarUsuario", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoUsuario", UsuarioOperativo.U_IdTipoUsuario);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", UsuarioOperativo.U_Nombre);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Password", PasswordEncryptado);
                SQLComandoAuxiliar.Parameters.AddWithValue("@PasswordReal", "");
                SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", UsuarioOperativo.U_CorreoElectronico);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdRepresentanteAsociado", IdRepresentanteLegal);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", UsuarioOperativo.U_Activo);
                SQLComandoAuxiliar.Parameters.AddWithValue("@CreadoDuranteRegistro", false);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Ingreso", false);
                SQLComandoAuxiliar.ExecuteNonQuery();

                SQLComandoAuxiliar.Connection.Dispose();
                Respuesta = true;
                return Respuesta;
            }

            public bool D_InsertarUsuarioSENER(CatUsuarios UsuarioSENER, int IdEmpresa)
            {
                bool Respuesta = false;
                string PasswordEncryptado = "";
                PasswordEncryptado = MetodoGeneral.EncriptarPassword(UsuarioSENER.U_Password);

                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarUsuarioSENER", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoUsuario", UsuarioSENER.U_IdTipoUsuario);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", UsuarioSENER.U_Nombre);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Password", PasswordEncryptado); //Modificado 13/03/17
                SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", UsuarioSENER.U_CorreoElectronico);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", 1);
                SQLComandoAuxiliar.ExecuteNonQuery();

                SQLComandoAuxiliar.Connection.Dispose();
                Respuesta = true;
                return Respuesta;
            }

            public bool D_ActualizarUsuario(CatUsuarios Usuario, string ContraseniaReal)
            {
                bool Resultado = false;
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarUsuario", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", Usuario.U_Nombre);
                SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", Usuario.U_CorreoElectronico);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", Usuario.U_Activo);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Password", Usuario.U_Password);
                SQLComandoAuxiliar.Parameters.AddWithValue("@PasswordReal", ContraseniaReal);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdUsuario", Usuario.U_IdUsuario);

                SQLComandoAuxiliar.ExecuteNonQuery();
                SQLComandoAuxiliar.Connection.Dispose();
                Resultado = true;
                return Resultado;
            }

            public bool D_ActualizarNombreUsuarioSP(string NombreAnterior, string NuevoNombre)
            {
                bool Resultado = false;
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBDSharePoint();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarNombreUsuario", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@NombreAnterior", NombreAnterior);
                SQLComandoAuxiliar.Parameters.AddWithValue("@NuevoNombre", NuevoNombre);

                SQLComandoAuxiliar.ExecuteNonQuery();
                SQLComandoAuxiliar.Connection.Dispose();
                Resultado = true;
                return Resultado;
            }
            public bool D_ActivarUsuariosPorRepresentante(int IdRepresentante, string Contrasenia, string ContraseniaReal)
            {
                bool Resultado = false;
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActivarUsuariosPorRepresentante", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdRepresentante", IdRepresentante);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Contrasenia", Contrasenia);
                SQLComandoAuxiliar.Parameters.AddWithValue("@ContraseniaReal", ContraseniaReal);

                SQLComandoAuxiliar.ExecuteNonQuery();
                SQLComandoAuxiliar.Connection.Dispose();
                Resultado = true;
                return Resultado;
            }

            public bool D_EliminarUsuario(int Idusuario)
            {
                bool Resultado = false;
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarUsuario", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdUsuario", Idusuario);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();
                Resultado = true;
                return Resultado;
            }

            public bool D_ActualizarEstatusUsuarioSENER(int Idusuario, bool IdEstatus)
            {
                bool Resultado = false;
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarEstatusUsuarioSENER", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdUsuario", Idusuario);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", IdEstatus);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();
                Resultado = true;
                return Resultado;
            }

            public void D_ActualizarUsuarioEntrada(int Idusuario, bool Ingreso)
            {
                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpActualizarUsuarioEntrada", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdUsuario", Idusuario);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Ingreso", Ingreso);

                SQLComandoAuxiliar.ExecuteNonQuery();
                SQLComandoAuxiliar.Connection.Dispose();
            }

        #endregion

        #region Métodos Auxiliares:

            public DataTable D_DetallesUsuarioPorRepresentante(int IdRepresentanteLegal)
            {
                DtAuxiliar.Rows.Clear();
                DtAuxiliar.Columns.Clear();

                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpDetallesUsuarioPorRepresentante", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdRepresentanteLegal", IdRepresentanteLegal);
                SQLComandoAuxiliar.ExecuteNonQuery();
                SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
                dr.Fill(DtAuxiliar);
                SQLComandoAuxiliar.Connection.Dispose();

                return DtAuxiliar;
            }

            public string D_SolicitarCambioContraseña(string NombreUsuario, string ClaveReset)
            {
                string CorreoElectronico;

                SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
                SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpAuxiliarSolicitarCambioContraseña", Conexion);
                SQLComandoAuxiliar.Parameters.AddWithValue("@NombreUsuario", NombreUsuario);
                SQLComandoAuxiliar.Parameters.AddWithValue("@ClaveReset", ClaveReset);
                SQLComandoAuxiliar.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                SQLComandoAuxiliar.ExecuteNonQuery();

                CorreoElectronico = (SQLComandoAuxiliar.Parameters["@CorreoElectronico"].Value).ToString();
                SQLComandoAuxiliar.Connection.Dispose();
                return CorreoElectronico;
            }

        #endregion
    }
}