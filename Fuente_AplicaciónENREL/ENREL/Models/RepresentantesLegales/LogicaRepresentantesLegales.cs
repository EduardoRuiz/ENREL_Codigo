using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ENREL.Models.RepresentantesLegales
{
    public class LogicaRepresentantesLegales
    {
        #region Variables:

        List<CatRepresentantesLegales> ListaTecnologias = new List<CatRepresentantesLegales>();
        DatosRepresentantesLegales DatosAuxiliar = new DatosRepresentantesLegales();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos Principales:

        public List<CatRepresentantesLegales> L_SeleccionarRepresentantesLegalesPorValidar()
        {
            List<CatRepresentantesLegales> ListaRepresentantesLegales = new List<CatRepresentantesLegales>();

            DtAuxiliar = DatosAuxiliar.D_SeleccionarRepresentantesLegalesPorValidar();

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatRepresentantesLegales Representante = new CatRepresentantesLegales();
                    Representante.RL_IdRepresentanteLegal = (Int32)dr["IdRepresentanteLegal"];
                    Representante.RL_Nombre = dr["NombreRepresentante"].ToString();
                    Representante.E_NombreComercial = dr["NombreComercial"].ToString();
                    Representante.RL_FechaSolicitud = dr["FechaSolicitud"].ToString();
                    Representante.RL_Observaciones = dr["Observaciones"].ToString();

                    ListaRepresentantesLegales.Add(Representante);
                }
            }

            return ListaRepresentantesLegales;
        }

        public List<CatRepresentantesLegales> L_SeleccionarRepresentantesLegalesPorEmpresa(int IdEmpresa)
        {
            List<CatRepresentantesLegales> ListaRepresentantesLegales = new List<CatRepresentantesLegales>();

            DtAuxiliar = DatosAuxiliar.D_SeleccionarRepresentantesLegalesPorEmpresa(IdEmpresa);

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatRepresentantesLegales Representante = new CatRepresentantesLegales();
                    Representante.RL_IdRepresentanteLegal = (Int32)dr["IdRepresentanteLegal"];
                    Representante.RL_Nombre = dr["NombreRepresentante"].ToString();
                    Representante.RL_CURP = dr["CURP"].ToString();
                    Representante.RL_RFC = dr["RFC"].ToString();
                    Representante.RL_NumeroExterior = dr["NumeroExterior"].ToString();
                    Representante.RL_Calle = dr["Calle"].ToString();
                    Representante.RL_IdTipoVialidad = (Int32)dr["IdTipoCalle"];
                    try
                    { Representante.RL_NumeroInterior = dr["NumeroInterior"].ToString(); }
                    catch (Exception ex) { };
                    Representante.RL_IdTipoAsentamiento = (Int32)dr["IdTipoColonia"];
                    Representante.RL_Colonia = dr["Colonia"].ToString();
                    //Cambio de código postal
                    //try
                    //{ Representante.RL_CodigoPostal = (Int32)dr["CodigoPostal"]; }
                    //catch (Exception ex) { };
                    Representante.RL_CodigoPostal = dr["CodigoPostal"].ToString();
                    Representante.RL_Localidad = dr["Localidad"].ToString();
                    Representante.RL_Municipio = dr["Municipio"].ToString();
                    Representante.RL_EntidadFederativa = dr["EntidadFederativa"].ToString();

                    Representante.RL_Lada = dr["Lada"].ToString();
                    Representante.RL_TelefonoFijo = dr["TelefonoFijo"].ToString();
                    try
                    { Representante.RL_ExtensionTelefonica = dr["ExtensionTelefonica"].ToString(); }
                    catch (Exception ex) { };
                    try
                    { Representante.RL_TelefonoCelular = dr["TelefonoMovil"].ToString(); }
                    catch (Exception ex) { };
                    Representante.RL_CorreoElectronico = dr["CorreoElectronico"].ToString();

                    Representante.RL_FechaSolicitud = dr["FechaSolicitud"].ToString();
                    //Representante.RL_IdentificacionOficial = dr["IdentificacionOficial"].ToString();
                    Representante.RL_IdEmpresa = (Int32)dr["IdEmpresa"];
                    //Representante.RL_EstatusSolicitudRepresentante = dr["EstatusSolicitudRepresentante"].ToString();
                    Representante.RL_IdEstatusSolicitudRepresentante = (Int32)dr["IdEstatusSolicitudRepresentante"];
                    Representante.RL_Observaciones = dr["Observaciones"].ToString();

                    ListaRepresentantesLegales.Add(Representante);
                }
            }

            return ListaRepresentantesLegales;
        }

        public CatRepresentantesLegales L_DetallesRepresentanteLegal(int IdRepresentanteLegal)
        {
            DtAuxiliar = DatosAuxiliar.D_DetallesRepresentanteLegal(IdRepresentanteLegal);
            CatRepresentantesLegales Representante = new CatRepresentantesLegales();
            if (DtAuxiliar.Rows.Count > 0)
            {
                Representante.RL_IdRepresentanteLegal = (Int32)DtAuxiliar.Rows[0]["IdRepresentanteLegal"];
                Representante.RL_Nombre = DtAuxiliar.Rows[0]["Nombre"].ToString();
                Representante.RL_PrimerApellido = DtAuxiliar.Rows[0]["PrimerApellido"].ToString();
                Representante.RL_SegundoApellido = DtAuxiliar.Rows[0]["SegundoApellido"].ToString();
                Representante.RL_CURP = DtAuxiliar.Rows[0]["CURP"].ToString();
                Representante.RL_RFC = DtAuxiliar.Rows[0]["RFC"].ToString();
                Representante.RL_IdTipoVialidad = (Int32)DtAuxiliar.Rows[0]["IdTipoCalle"];
                Representante.RL_Calle = DtAuxiliar.Rows[0]["Calle"].ToString();
                Representante.RL_NumeroExterior = DtAuxiliar.Rows[0]["NumeroExterior"].ToString();
                try
                { Representante.RL_NumeroInterior = DtAuxiliar.Rows[0]["NumeroInterior"].ToString(); }
                catch (Exception ex) { };
                Representante.RL_IdTipoAsentamiento = (Int32)DtAuxiliar.Rows[0]["IdTipoColonia"];
                Representante.RL_Colonia = DtAuxiliar.Rows[0]["Colonia"].ToString();
                //Cambio de código postal
                //try
                //{ Representante.RL_CodigoPostal = (Int32)DtAuxiliar.Rows[0]["CodigoPostal"]; }
                //catch (Exception ex) { };
                Representante.RL_CodigoPostal = DtAuxiliar.Rows[0]["CodigoPostal"].ToString();
                Representante.RL_IdLocalidad = (Int32)DtAuxiliar.Rows[0]["IdLocalidad"];
                Representante.RL_Localidad = DtAuxiliar.Rows[0]["Localidad"].ToString();
                Representante.RL_IdMunicipio = (Int32)DtAuxiliar.Rows[0]["IdMunicipio"];
                Representante.RL_Municipio = DtAuxiliar.Rows[0]["Municipio"].ToString();
                Representante.RL_IdEntidadFederativa = (Int32)DtAuxiliar.Rows[0]["IdEntidadFederativa"];
                Representante.RL_EntidadFederativa = DtAuxiliar.Rows[0]["EntidadFederativa"].ToString();

                Representante.RL_Lada = DtAuxiliar.Rows[0]["Lada"].ToString();
                Representante.RL_TelefonoFijo = DtAuxiliar.Rows[0]["TelefonoFijo"].ToString();
                try
                { Representante.RL_ExtensionTelefonica = DtAuxiliar.Rows[0]["ExtensionTelefonica"].ToString(); }
                catch (Exception ex) { };
                try
                { Representante.RL_TelefonoCelular = DtAuxiliar.Rows[0]["TelefonoMovil"].ToString(); }
                catch (Exception ex) { };
                Representante.RL_CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();

                Representante.RL_FechaSolicitud = DtAuxiliar.Rows[0]["FechaSolicitud"].ToString();
                //Representante.RL_IdentificacionOficial = DtAuxiliar.Rows[0]["IdentificacionOficial"].ToString();
                Representante.RL_IdEmpresa = (Int32)DtAuxiliar.Rows[0]["IdEmpresa"];
                Representante.RL_IdEstatusSolicitudRepresentante = (Int32)DtAuxiliar.Rows[0]["IdEstatusSolicitudRepresentante"];
            }

            return Representante;
        }

        public int L_InsertarRepresentanteLegal(CatRepresentantesLegales Representante, int IdEmpresa)
        {
            int IdRepresentanteLegal = 0;
            IdRepresentanteLegal = DatosAuxiliar.D_InsertarRepresentanteLegal(Representante, IdEmpresa);
            
            return IdRepresentanteLegal;
        }

        public bool L_ActualizarRepresentanteLegal(CatRepresentantesLegales Representante)
        {
            bool Resultado = false;
            CatUsuarios UsuarioDelRepresentante = new CatUsuarios();
            LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
            UsuarioDelRepresentante = LogicaUsuario.L_DetallesUsuarioPorRepresentante(Representante.RL_IdRepresentanteLegal);
            LogicaUsuario.L_EliminarUsuario(UsuarioDelRepresentante.U_IdUsuario);
            Resultado = DatosAuxiliar.D_ActualizarRepresentanteLegal(Representante);
            return Resultado;
        }

        public bool L_EliminarRepresentanteLegal(int IdRepresentante)
        {
            bool Resultado = false;
            CatUsuarios UsuarioDelRepresentante = new CatUsuarios();
            LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
            UsuarioDelRepresentante = LogicaUsuario.L_DetallesUsuarioPorRepresentante(IdRepresentante);
            LogicaUsuario.L_EliminarUsuario(UsuarioDelRepresentante.U_IdUsuario);
            Resultado = DatosAuxiliar.D_EliminarRepresentanteLegal(IdRepresentante);
            return Resultado;
        }

        #endregion

        #region Métodos Auxiliares:

        public int L_DetallesRepresentanteLegalPorRFC(string RFCRepresentante)
        {
            int Respuesta = 0;
            DtAuxiliar = DatosAuxiliar.D_DetallesRepresentanteLegalPorRFC(RFCRepresentante);
            CatRepresentantesLegales Representante = new CatRepresentantesLegales();
            if (DtAuxiliar.Rows.Count > 0)
            {
                Respuesta = (Int32)DtAuxiliar.Rows[0]["IdRepresentanteLegal"];
            }
            return Respuesta;
        }

        #endregion
    }
}