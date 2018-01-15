using ENREL.Models.Empresas;
using ENREL.Models.RepresentantesLegales;
using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Web;
using ENREL.Controllers.Auxiliar;

namespace ENREL.Models.Home
{
    public class LogicaHome
    {
        #region Variables:

        DatosHome DatosAuxiliar = new DatosHome();

        #endregion

        #region Métodos Principales:

        public string L_InsertarRegistroSolicitud(CatEmpresas Empresa, CatRepresentantesLegales Representante, CatUsuarios UsuarioOperativo)
        {
            return DatosAuxiliar.D_InsertarRegistroSolicitud(Empresa, Representante, UsuarioOperativo);
        }

        public void L_EliminarIntentoRegistro(int IdEmpresa)
        {
            DatosAuxiliar.D_EliminarIntentoRegistro(IdEmpresa);
        }

        public void L_ValidarRegistro(CatRepresentantesLegales Representante, string Contrasenia, string ContraseniaReal)
        {
            DatosRepresentantesLegales DatosAuxiliar = new DatosRepresentantesLegales();
            Representante.RL_IdEstatusSolicitudRepresentante = 3;
            Representante.RL_Activo = true;
            DatosAuxiliar.D_ActualizarRepresentanteLegal(Representante);

            DatosUsuarios DatosAuxiliar2 = new DatosUsuarios();
            DatosAuxiliar2.D_ActivarUsuariosPorRepresentante(Representante.RL_IdRepresentanteLegal, Contrasenia, ContraseniaReal);

            MetodosGenerales MetodoGeneral = new MetodosGenerales();
            LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
            //CatUsuarios UsuarioDigitalizacion = LogicaUsuario.L_DetallesUsuarioPorRepresentante(Representante.RL_IdRepresentanteLegal);
            //MetodoGeneral.RegistrarUsuario(UsuarioDigitalizacion.U_Nombre, ContraseniaReal, UsuarioDigitalizacion.U_CorreoElectronico);
        }

        #endregion
    }
}