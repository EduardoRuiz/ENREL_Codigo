using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ENREL.Models.Usuarios 
{
    public class LogicaUsuarios : System.Web.UI.Page
    {
        #region Variables:

        DatosUsuarios DatosAuxiliar = new DatosUsuarios();
        DataTable DtAuxiliar = new DataTable();
        List<CatTiposUsuario> ListaTiposUsuario = new List<CatTiposUsuario>();

        #endregion

        #region Métodos Principales:

        public List<CatUsuarios> L_SeleccionarUsuariosPorEmpresa(int IdEmpresa, bool Activo)
        {
            List<CatUsuarios> ListaUsuarios = new List<CatUsuarios>();
            DtAuxiliar = DatosAuxiliar.D_SeleccionarUsuariosPorEmpresa(IdEmpresa, Activo);


            foreach (DataRow dr in DtAuxiliar.Rows)
            {
                CatUsuarios UsuarioEntrante = new CatUsuarios();
                UsuarioEntrante.U_Nombre = dr["Nombre"].ToString();
                UsuarioEntrante.U_CorreoElectronico = dr["CorreoElectronico"].ToString();
                UsuarioEntrante.U_IdTipoUsuario = (Int32)dr["IdTipoUsuario"];
                UsuarioEntrante.U_IdUsuario = (Int32)dr["IdUsuario"];
                UsuarioEntrante.U_Activo = (bool)dr["Activo"];


                ListaUsuarios.Add(UsuarioEntrante);
            }


            return ListaUsuarios;

        }

        public List<CatUsuarios> L_SeleccionarUsuariosSENER(int? IdTipoUsuario, int? IdEstatus)
        {
            DataTable dtUsuarios = new DataTable();
            List<CatUsuarios> ListaUsuarios = new List<CatUsuarios>();

            dtUsuarios = DatosAuxiliar.D_SeleccionarUsuariosSENER(IdTipoUsuario, IdEstatus);

            if (dtUsuarios.Rows.Count > 0)
            {

                foreach (DataRow row in dtUsuarios.Rows)
                {
                    CatUsuarios Usuario = new CatUsuarios();
                    Usuario.U_IdUsuario = (Int32)row["IdUsuario"];
                    Usuario.U_IdTipoUsuario = (Int32)row["IdTipoUsuario"];
                    Usuario.U_Nombre = row["Nombre"].ToString();
                    Usuario.U_CorreoElectronico = row["CorreoElectronico"].ToString();
                    Usuario.U_IdEmpresa = (Int32)row["IdEmpresa"];
                    Usuario.U_NombreEmpresa = row["NombreEmpresa"].ToString();
                    Usuario.U_FechaRegistro = Convert.ToDateTime(row["FechaRegistro"]);
                    Usuario.U_TipoUsuario = row["TipoUsuario"].ToString();
                    Usuario.U_Activo = (bool)row["Activo"];


                    ListaUsuarios.Add(Usuario);
                }
            }
            return ListaUsuarios;
        }

        public List<CatTiposUsuario> SeleccionarTiposUsuario()
        {
            DtAuxiliar = DatosAuxiliar.SeleccionarTiposUsuarios();
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatTiposUsuario TipoUsuario = new CatTiposUsuario();
                    TipoUsuario.idTipoUsuario = (Int32)dr["IdTipoUsuario"];
                    TipoUsuario.TipoUsuario = dr["TipoUsuario"].ToString();
                    ListaTiposUsuario.Add(TipoUsuario);
                }
            }
            return ListaTiposUsuario;
        }

        public List<CatEstatusUsuario> SeleccionarTiposEstatusUsuario()
        {
            List<CatEstatusUsuario> ListaTiposEstatusUsuario = new List<CatEstatusUsuario>();

            CatEstatusUsuario TipoEstatusUsuario1 = new CatEstatusUsuario();
            TipoEstatusUsuario1.IdEstatusUsuario = 0;
            TipoEstatusUsuario1.EstatusUsuario = "INACTIVO";

            CatEstatusUsuario TipoEstatusUsuario2 = new CatEstatusUsuario();
            TipoEstatusUsuario2.IdEstatusUsuario = 1;
            TipoEstatusUsuario2.EstatusUsuario = "ACTIVO";

            ListaTiposEstatusUsuario.Add(TipoEstatusUsuario1);
            ListaTiposEstatusUsuario.Add(TipoEstatusUsuario2);
            return ListaTiposEstatusUsuario;
        }

        public CatUsuarios L_DetallesUsuario(int IdUsuario)
        {
            CatUsuarios UsuarioPorEditar = new CatUsuarios();
            DtAuxiliar = DatosAuxiliar.D_DetallesUsuario(IdUsuario);

            if (DtAuxiliar.Rows.Count > 0)
            {
                UsuarioPorEditar.U_Nombre = DtAuxiliar.Rows[0]["Nombre"].ToString();
                UsuarioPorEditar.U_CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();
                UsuarioPorEditar.U_IdTipoUsuario = (Int32)DtAuxiliar.Rows[0]["IdTipoUsuario"];
                UsuarioPorEditar.U_IdUsuario = (Int32)DtAuxiliar.Rows[0]["IdUsuario"];
                UsuarioPorEditar.U_FechaRegistro = (DateTime)DtAuxiliar.Rows[0]["FechaRegistro"];
                try { UsuarioPorEditar.U_FechaModificacion = (DateTime)DtAuxiliar.Rows[0]["FechaModificacion"]; }
                catch { }
            }

            return UsuarioPorEditar;

        }

        public bool L_InsertarUsuario(CatUsuarios Usuario, int IdEmpresa, int IdRepresentanteLegal)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_InsertarUsuario(Usuario, IdEmpresa, IdRepresentanteLegal);
            return Resultado;
        }

        public bool L_InsertarUsuarioSENER(CatUsuarios Usuario, int IdEmpresa)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_InsertarUsuarioSENER(Usuario, IdEmpresa);
            return Resultado;
        }

        public bool L_ActualizarUsuario(CatUsuarios Usuario, CatUsuarios UsuarioPorEditar, string Caracteristica)
        {
            bool Resultado = false;
            if (Caracteristica == "Nombre")
            {
                Usuario.U_Nombre = UsuarioPorEditar.U_Nombre;
                MetodosGenerales MetodoGeneral = new MetodosGenerales();
                string PasswordEncryptado = MetodoGeneral.EncriptarPassword(UsuarioPorEditar.U_Password);
                Usuario.U_Password = PasswordEncryptado;
                DatosAuxiliar.D_ActualizarUsuario(Usuario, "");
            }
            if (Caracteristica == "Correo")
            {
                Usuario.U_CorreoElectronico = UsuarioPorEditar.U_CorreoElectronico;
                MetodosGenerales MetodoGeneral = new MetodosGenerales();
                string PasswordEncryptado = MetodoGeneral.EncriptarPassword(UsuarioPorEditar.U_Password);
                Usuario.U_Password = PasswordEncryptado;
                DatosAuxiliar.D_ActualizarUsuario(Usuario, "");
            }
            if (Caracteristica == "Contraseña")
            {
                MetodosGenerales MetodoGeneral = new MetodosGenerales();
                string PasswordEncryptado = "";
                PasswordEncryptado = MetodoGeneral.EncriptarPassword(UsuarioPorEditar.U_NuevoPassword);

                Usuario.U_Password = PasswordEncryptado;
                DatosAuxiliar.D_ActualizarUsuario(Usuario, UsuarioPorEditar.U_NuevoPassword);
            }

            Resultado = DatosAuxiliar.D_ActualizarUsuario(Usuario, Usuario.U_Password);
            return Resultado;
        }

        public void L_ActualizarNombreUsuarioSP(string NombreAnterior, string NuevoNombre)
        {
            DatosAuxiliar.D_ActualizarNombreUsuarioSP(NombreAnterior, NuevoNombre);
        }

        public bool L_EliminarUsuario(int U_IdUsuario)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_EliminarUsuario(U_IdUsuario);
            return Resultado;
        }

        public bool L_ActualizarEstatusUsuarioSENER(int U_IdUsuario, bool IdEstatus)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_ActualizarEstatusUsuarioSENER(U_IdUsuario, IdEstatus);
            return Resultado;
        }

        public void L_ActualizarUsuarioEntrada(int U_IdUsuario, bool Ingreso)
        {
            DatosAuxiliar.D_ActualizarUsuarioEntrada(U_IdUsuario, Ingreso);
        }

        #endregion

        #region Métodos Auxiliares:

        public CatUsuarios L_DetallesUsuarioPorRepresentante(int IdRepresentanteLegal)
        {
            CatUsuarios UsuarioPorEditar = new CatUsuarios();
            DtAuxiliar = DatosAuxiliar.D_DetallesUsuarioPorRepresentante(IdRepresentanteLegal);

            if (DtAuxiliar.Rows.Count > 0)
            {
                UsuarioPorEditar.U_Nombre = DtAuxiliar.Rows[0]["Nombre"].ToString();
                UsuarioPorEditar.U_CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();
                UsuarioPorEditar.U_IdTipoUsuario = (Int32)DtAuxiliar.Rows[0]["IdTipoUsuario"];
                UsuarioPorEditar.U_IdUsuario = (Int32)DtAuxiliar.Rows[0]["IdUsuario"];
                UsuarioPorEditar.U_FechaRegistro = (DateTime)DtAuxiliar.Rows[0]["FechaRegistro"];
                try { UsuarioPorEditar.U_FechaModificacion = (DateTime)DtAuxiliar.Rows[0]["FechaModificacion"]; }
                catch { }
            }

            return UsuarioPorEditar;

        }

        public CatUsuarios L_DetallesUsuarioPorNombreUnicamente(string Nombre)
        {
            CatUsuarios UsuarioPorEditar = new CatUsuarios();
            DtAuxiliar = DatosAuxiliar.D_SeleccionarUsuariosPorNombreUnicamente(Nombre);

            if (DtAuxiliar.Rows.Count > 0)
            {
                UsuarioPorEditar.U_Nombre = DtAuxiliar.Rows[0]["Nombre"].ToString();
                UsuarioPorEditar.U_CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();
                UsuarioPorEditar.U_IdTipoUsuario = (Int32)DtAuxiliar.Rows[0]["IdTipoUsuario"];
                UsuarioPorEditar.U_IdUsuario = (Int32)DtAuxiliar.Rows[0]["IdUsuario"];
                UsuarioPorEditar.U_FechaRegistro = (DateTime)DtAuxiliar.Rows[0]["FechaRegistro"];
                try
                {
                    UsuarioPorEditar.U_FechaModificacion = (DateTime)DtAuxiliar.Rows[0]["FechaModificacion"];
                }
                catch { }

                UsuarioPorEditar.U_Password = DtAuxiliar.Rows[0]["Password"].ToString();
                UsuarioPorEditar.U_Activo = (bool)DtAuxiliar.Rows[0]["Activo"];
            }


            return UsuarioPorEditar;

        }

        public CatUsuarios L_DetallesUsuarioPorClaveReset(string ClaveReset)
        {
            CatUsuarios UsuarioPorEditar = new CatUsuarios();
            DtAuxiliar = DatosAuxiliar.D_SeleccionarUsuariosPorClaveReset(ClaveReset);

            if (DtAuxiliar.Rows.Count > 0)
            {
                UsuarioPorEditar.U_Nombre = DtAuxiliar.Rows[0]["Nombre"].ToString();
                UsuarioPorEditar.U_IdUsuario = (Int32)DtAuxiliar.Rows[0]["IdUsuario"];
                UsuarioPorEditar.U_CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();
                UsuarioPorEditar.U_CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();
                UsuarioPorEditar.U_IdTipoUsuario = (Int32)DtAuxiliar.Rows[0]["IdTipoUsuario"];
                UsuarioPorEditar.U_IdUsuario = (Int32)DtAuxiliar.Rows[0]["IdUsuario"];
                UsuarioPorEditar.U_FechaRegistro = (DateTime)DtAuxiliar.Rows[0]["FechaRegistro"];
                try { UsuarioPorEditar.U_FechaModificacion = (DateTime)DtAuxiliar.Rows[0]["FechaModificacion"]; }
                catch { }
                UsuarioPorEditar.U_Password = DtAuxiliar.Rows[0]["Password"].ToString();
                UsuarioPorEditar.U_Activo = (bool)DtAuxiliar.Rows[0]["Activo"];
            }
            return UsuarioPorEditar;
        }

        public bool L_ValidarUsuario(string NombreUsuario, string U_Password)
        {
            bool resultado = false;
            DtAuxiliar = DatosAuxiliar.D_SeleccionarUsuarioPorNombre(NombreUsuario, U_Password);
            CatUsuarios UsuarioEntrante = new CatUsuarios();

            if (DtAuxiliar != null && DtAuxiliar.Rows.Count > 0)
            {
                UsuarioEntrante.U_IdUsuario = (Int32)DtAuxiliar.Rows[0]["IdUsuario"];
                UsuarioEntrante.U_IdTipoUsuario = (Int32)DtAuxiliar.Rows[0]["IdTipoUsuario"];
                UsuarioEntrante.U_Nombre = DtAuxiliar.Rows[0]["Nombre"].ToString();
                UsuarioEntrante.U_CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();
                UsuarioEntrante.U_IdEmpresa = (Int32)DtAuxiliar.Rows[0]["IdEmpresa"];
                try { UsuarioEntrante.U_IdRepresentanteAsociado = (Int32)DtAuxiliar.Rows[0]["IdRepresentanteAsociado"]; }
                catch { }              
                UsuarioEntrante.U_FechaRegistro = (DateTime)DtAuxiliar.Rows[0]["FechaRegistro"];
                try { UsuarioEntrante.U_FechaModificacion = (DateTime)DtAuxiliar.Rows[0]["FechaModificacion"]; }
                catch { }
                UsuarioEntrante.U_Password = DtAuxiliar.Rows[0]["Password"].ToString();
                UsuarioEntrante.U_Activo = (bool)DtAuxiliar.Rows[0]["Activo"];
                UsuarioEntrante.U_Ingreso = (bool)DtAuxiliar.Rows[0]["Ingreso"];
                UsuarioEntrante.U_RFCRepresentanteAsociado = DtAuxiliar.Rows[0]["RFC"].ToString();
                Session.Add("Usuario", UsuarioEntrante);

                string OpenId = DatosAuxiliar.D_ActualizarOpenId(UsuarioEntrante.U_IdUsuario);
                Session.Add("OpenId", OpenId);

                resultado = true;
            }
            return resultado;
        }

        public bool L_ValidarUsuarioPorOpenId(string OpenId)
        {
            bool resultado = false;
            DtAuxiliar = DatosAuxiliar.D_SeleccionarUsuarioPorOpenId(OpenId);
            CatUsuarios UsuarioEntrante = new CatUsuarios();

            if (DtAuxiliar != null && DtAuxiliar.Rows.Count > 0)
            {
                UsuarioEntrante.U_IdUsuario = (Int32)DtAuxiliar.Rows[0]["IdUsuario"];
                UsuarioEntrante.U_IdTipoUsuario = (Int32)DtAuxiliar.Rows[0]["IdTipoUsuario"];
                UsuarioEntrante.U_Nombre = DtAuxiliar.Rows[0]["Nombre"].ToString();
                UsuarioEntrante.U_CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();
                UsuarioEntrante.U_IdEmpresa = (Int32)DtAuxiliar.Rows[0]["IdEmpresa"];
                try { UsuarioEntrante.U_IdRepresentanteAsociado = (Int32)DtAuxiliar.Rows[0]["IdRepresentanteAsociado"]; }
                catch { }
                UsuarioEntrante.U_FechaRegistro = (DateTime)DtAuxiliar.Rows[0]["FechaRegistro"];
                try { UsuarioEntrante.U_FechaModificacion = (DateTime)DtAuxiliar.Rows[0]["FechaModificacion"]; }
                catch { }
                UsuarioEntrante.U_Password = DtAuxiliar.Rows[0]["Password"].ToString();
                UsuarioEntrante.U_Activo = (bool)DtAuxiliar.Rows[0]["Activo"];
                UsuarioEntrante.U_Ingreso = (bool)DtAuxiliar.Rows[0]["Ingreso"];
                UsuarioEntrante.U_RFCRepresentanteAsociado = DtAuxiliar.Rows[0]["RFC"].ToString();
                Session.Add("Usuario", UsuarioEntrante);

                resultado = true;
            }

            
            return resultado;
        }

        public string L_SolicitarCambioContraseña(string NombreUsuario, string ClaveReset)
        {
            string CorreoElectronico = "";
            CorreoElectronico = DatosAuxiliar.D_SolicitarCambioContraseña(NombreUsuario, ClaveReset);
            return CorreoElectronico;        
        }

        #endregion
    }
}