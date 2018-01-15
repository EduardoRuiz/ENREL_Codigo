using ENREL.Controllers.Auxiliar;
using ENREL.Models.Empresas;
using ENREL.Models.RepresentantesLegales;
using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.Home
{
    public class DatosHome : System.Web.UI.Page
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        #endregion

        #region Métodos Principales:

        public string D_InsertarRegistroSolicitud(CatEmpresas Empresa, CatRepresentantesLegales RepresentanteLegal, CatUsuarios UsuarioOperativo)
        {
            bool resultado = false;
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            string PasswordEncryptado = "";
            string GUID = "Error";

            //Insertar Empresa
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarEmpresa", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@NombreComercial", Empresa.E_NombreComercial);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RazonSocial", Empresa.E_RazonSocial);
            SQLComandoAuxiliar.Parameters.AddWithValue("@RFC", Empresa.E_RFC);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoCalle", Empresa.E_IdTipoVialidad);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Calle", Empresa.E_Calle);
            SQLComandoAuxiliar.Parameters.AddWithValue("@NumeroExterior", Empresa.E_NumeroExterior);
            if (Empresa.E_NumeroInterior != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@NumeroInterior", Empresa.E_NumeroInterior); }
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoColonia", Empresa.E_IdTipoAsentamiento);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Colonia", Empresa.E_Colonia);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdLocalidad", Empresa.E_IdLocalidad);
            //Cambio de código postal
            //if (Empresa.E_CodigoPostal != null && Empresa.E_CodigoPostal > 0) { }
            SQLComandoAuxiliar.Parameters.AddWithValue("@CodigoPostal", Empresa.E_CodigoPostal);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Lada", Empresa.E_Lada);
            SQLComandoAuxiliar.Parameters.AddWithValue("@TelefonoFijo", Empresa.E_TelefonoFijo);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", Empresa.E_CorreoElectronico);
            SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", true);
            SQLComandoAuxiliar.Parameters.AddWithValue("@CompartirDatos", Empresa.E_CompartirDatos);
            SQLComandoAuxiliar.Parameters.Add("@idEmpresa", SqlDbType.Int).Direction = ParameterDirection.Output;

            SQLComandoAuxiliar.ExecuteNonQuery();

            int IdEmpresa = (int)SQLComandoAuxiliar.Parameters["@idEmpresa"].Value;
            Empresa.E_IdEmpresa = IdEmpresa;

            Session["NuevoAcceso_Empresa"] = Empresa;

            if (IdEmpresa > 1)
            {
                //Insertar Representante Legal
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
                //Cambio de código postal
                //if (RepresentanteLegal.RL_CodigoPostal != null && RepresentanteLegal.RL_CodigoPostal > 0) { SQLComandoAuxiliar.Parameters.AddWithValue("@CodigoPostal", RepresentanteLegal.RL_CodigoPostal); }
                SQLComandoAuxiliar.Parameters.AddWithValue("@CodigoPostal", RepresentanteLegal.RL_CodigoPostal);
                if (RepresentanteLegal.RL_Lada != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@Lada", RepresentanteLegal.RL_Lada); }
                //COMUNICACIÓN: Eliminado temporalmente
                SQLComandoAuxiliar.Parameters.AddWithValue("@TelefonoFijo", RepresentanteLegal.RL_TelefonoFijo);
                if (RepresentanteLegal.RL_ExtensionTelefonica != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@ExtensionTelefonica", RepresentanteLegal.RL_ExtensionTelefonica); }
                SQLComandoAuxiliar.Parameters.AddWithValue("@TelefonoMovil", RepresentanteLegal.RL_TelefonoCelular);
                SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", RepresentanteLegal.RL_CorreoElectronico);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", true);
                SQLComandoAuxiliar.Parameters.AddWithValue("@IdEstatusSolicitudRepresentante", 1);
                SQLComandoAuxiliar.Parameters.Add("@IdRepresentanteLegal", SqlDbType.Int).Direction = ParameterDirection.Output;
                SQLComandoAuxiliar.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;
                SQLComandoAuxiliar.ExecuteNonQuery();

                int IdRepresentanteLegal = (int)SQLComandoAuxiliar.Parameters["@IdRepresentanteLegal"].Value;
                GUID = SQLComandoAuxiliar.Parameters["@GUID"].Value.ToString();
                RepresentanteLegal.RL_IdRepresentanteLegal = IdRepresentanteLegal;

                Session["NuevoAcceso_Representante"] = RepresentanteLegal;

                if (IdRepresentanteLegal > 1)
                {
                    //Insertar Usuario Operativo
                    if (UsuarioOperativo != null)
                    {
                        PasswordEncryptado = MetodoGeneral.EncriptarPassword(UsuarioOperativo.U_Password);
                        SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpInsertarUsuario", Conexion);
                        SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
                        SQLComandoAuxiliar.Parameters.AddWithValue("@IdTipoUsuario", 1);
                        SQLComandoAuxiliar.Parameters.AddWithValue("@Nombre", UsuarioOperativo.U_Nombre);
                        SQLComandoAuxiliar.Parameters.AddWithValue("@Password", PasswordEncryptado);
                        SQLComandoAuxiliar.Parameters.AddWithValue("@PasswordReal", "");
                        SQLComandoAuxiliar.Parameters.AddWithValue("@CorreoElectronico", UsuarioOperativo.U_CorreoElectronico);
                        SQLComandoAuxiliar.Parameters.AddWithValue("@IdRepresentanteAsociado", IdRepresentanteLegal);
                        SQLComandoAuxiliar.Parameters.AddWithValue("@Activo", false);
                        SQLComandoAuxiliar.Parameters.AddWithValue("@CreadoDuranteRegistro", true);
                        SQLComandoAuxiliar.Parameters.AddWithValue("@Ingreso", false);
                        SQLComandoAuxiliar.ExecuteNonQuery();
                    }

                    Guid DatoAleatorio = new Guid();
                    DatoAleatorio = Guid.NewGuid();
                    string NombreUsuario = DatoAleatorio.ToString().Substring(0, Guid.NewGuid().ToString().IndexOf("-"));

                    PasswordEncryptado = MetodoGeneral.EncriptarPassword("Temporal0");
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
            }



            SQLComandoAuxiliar.Connection.Close();

            return GUID;

        }


        public void D_EliminarIntentoRegistro(int IdEmpresa)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar.Connection.Dispose();

            //Insertar Empresa
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpEliminarIntentoRegistro", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SQLComandoAuxiliar.Connection.Dispose();

        }

        public void D_ValidarRegistro(int IdEmpresa, int IdRepresentante)
        {
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpValidarRegistro", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdEmpresa", IdEmpresa);
            SQLComandoAuxiliar.Parameters.AddWithValue("@IdRepresentanteLegal", IdRepresentante);
            SQLComandoAuxiliar.ExecuteNonQuery();
            SQLComandoAuxiliar.Connection.Dispose();
        }

        #endregion

        #region Métodos Auxiliares:

        #endregion
    }
}