using ENREL.Models.EstatusProyecto;
using ENREL.Models.Tramites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ENREL.Models.Proyectos
{
    public class LogicaProyectos
    {
        #region Variables:

        DatosProyectos DatosAuxiliar = new DatosProyectos();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos Principales:

        public List<CatProyectos> L_SeleccionarProyectosPorEmpresa(int IdEmpresa)
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarProyectos(IdEmpresa);
            List<CatProyectos> ListaProyectos = new List<CatProyectos>();
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatProyectos Proyecto = new CatProyectos();
                    Proyecto.P_IdEntidadFederativa = (Int32)dr["IdEntidadFederativa"];
                    Proyecto.P_EntidadFederativa = dr["EntidadFederativa"].ToString();
                    Proyecto.P_IdEmpresa = (Int32)dr["IdEmpresa"];
                    Proyecto.P_Empresa = dr["NombreEmpresa"].ToString();
                    Proyecto.P_IdTecnologia = (Int32)dr["IdTecnologia"];
                    Proyecto.P_Tecnologia = dr["Tecnologia"].ToString();
                    Proyecto.P_IdMunicipio = (Int32)dr["IdMunicipio"];
                    Proyecto.P_Municipio = dr["Municipio"].ToString();
                    Proyecto.P_EstatusProyecto = dr["EstatusProyecto"].ToString();
                    Proyecto.P_NombreProyecto = dr["NombreProyecto"].ToString();
                    Proyecto.P_Latitud = (double)dr["Latitud"];
                    Proyecto.P_Longitud = (double)dr["Longitud"];
                    Proyecto.P_CapacidadInstalada = (double)dr["CapacidadInstalada"];
                    Proyecto.P_GeneracionAnual = (double)dr["GeneracionAnual"];
                    Proyecto.P_FactorPlanta = (double)dr["FactorPlanta"];
                    Proyecto.P_MontoInversion = (double)dr["MontoInversion"];
                    Proyecto.P_FechaRegistro = (DateTime)dr["FechaRegistro"];
                    Proyecto.P_IdGlobal = dr["FechaRegistro"].ToString();
                    Proyecto.P_IdProyecto = (Int32)dr["IdProyecto"];

                    Proyecto.P_PorcentajePositivo = 0;
                    Proyecto.P_PorcentajeNegativo = 100;

                    try
                    {
                        Proyecto.P_Avance = (Int32)dr["Avance"];
                        Proyecto.P_Fase = (Int32)dr["Fase"];
                        Proyecto.P_PorcentajePositivo = Proyecto.P_Avance;
                        Proyecto.P_PorcentajeNegativo = 100 - Proyecto.P_Avance;
                    }
                    catch { }
                    //DatosTramites DatosAuxiliar2 = new DatosTramites();
                    //DataTable dtAvanceTramites = DatosAuxiliar2.D_AvanceTramites(Proyecto.P_IdProyecto);
                    //int tramitesfinalizados = 0;
                    //int tramitestotales = 1;
                    //if (dtAvanceTramites.Rows.Count == 2)
                    //{
                    //    int contador = 0;
                    //    foreach (DataRow row_j in dtAvanceTramites.Rows)
                    //    {
                    //        if (contador == 0)
                    //        {
                    //            tramitesfinalizados = (Int32)row_j[0];
                    //            contador = contador + 1;
                    //        }
                    //        else
                    //        {
                    //            tramitestotales = (Int32)row_j[0];
                    //        }
                    //    }
                    //    Proyecto.P_PorcentajePositivo = (tramitesfinalizados * 100) / tramitestotales;
                    //    Proyecto.P_PorcentajeNegativo = 100 - Proyecto.P_PorcentajePositivo;
                    //}
                    //else if (dtAvanceTramites.Rows.Count == 1 && (Int32)dtAvanceTramites.Rows[0][0] > 0)
                    //{
                    //    Proyecto.P_PorcentajePositivo = 100;
                    //    Proyecto.P_PorcentajeNegativo = 0;
                    //}

                    ListaProyectos.Add(Proyecto);
                }
            }
            else
            {
                ListaProyectos = new List<CatProyectos>();

            }
            return ListaProyectos;

        }

        public CatProyectos L_DetallesProyectos(int IdProyecto)
        {
            List<CatProyectos> ListaProyectos = new List<CatProyectos>();
            DtAuxiliar = DatosAuxiliar.D_DetallesProyectos(IdProyecto);
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatProyectos Proyecto = new CatProyectos();
                    Proyecto.P_IdEntidadFederativa = (Int32)dr["IdEntidadFederativa"];
                    Proyecto.P_EntidadFederativa = dr["EntidadFederativa"].ToString();
                    Proyecto.P_IdEmpresa = (Int32)dr["IdEmpresa"];
                    Proyecto.P_Empresa = dr["NombreEmpresa"].ToString();
                    Proyecto.P_IdTecnologia = (Int32)dr["IdTecnologia"];
                    try { Proyecto.P_Tecnologia = dr["Tecnologia"].ToString(); }
                    catch { }
                    try { Proyecto.P_IdTipoAsentamiento = (Int32)dr["IdTipoColonia"]; }
                    catch { }
                    Proyecto.P_Colonia = dr["Colonia"].ToString();
                    Proyecto.P_IdLocalidad = (Int32)dr["IdLocalidad"];
                    Proyecto.P_Localidad = dr["Localidad"].ToString();
                    //try { Proyecto.P_CodigoPostal = dr["CodigoPostal"].ToString(); }
                    //catch { }
                    Proyecto.P_CodigoPostal = dr["CodigoPostal"].ToString();
                    Proyecto.P_IdMunicipio = (Int32)dr["IdMunicipio"];
                    Proyecto.P_Municipio = dr["Municipio"].ToString();
                    Proyecto.P_EstatusProyecto = dr["EstatusProyecto"].ToString();
                    Proyecto.P_NombreProyecto = dr["NombreProyecto"].ToString();
                    Proyecto.P_Latitud = (double)dr["Latitud"];
                    Proyecto.P_Longitud = (double)dr["Longitud"];
                    Proyecto.P_CapacidadInstalada = (double)dr["CapacidadInstalada"];
                    Proyecto.P_GeneracionAnual = (double)dr["GeneracionAnual"];
                    Proyecto.P_FactorPlanta = (double)dr["FactorPlanta"];
                    Proyecto.P_MontoInversion = (double)dr["MontoInversion"];
                    Proyecto.P_FechaRegistro = (DateTime)dr["FechaRegistro"];
                    Proyecto.P_DiasAgregados = 0;
                    try { Proyecto.P_DiasAgregados = (Int32)dr["DiasAgregados"]; }
                    catch { }
                    Proyecto.P_IdGlobal = dr["IdGlobal"].ToString();
                    Proyecto.P_IdProyecto = (Int32)dr["IdProyecto"];

                    ListaProyectos.Add(Proyecto);
                }

                return ListaProyectos[0];
            }
            else
            {
                CatProyectos Proyecto = new CatProyectos();
                return Proyecto;
            }

        }

        public int L_InsertarProyecto(CatProyectos Proyecto, int IdUsuario)
        {
            return DatosAuxiliar.D_InsertarProyectos(Proyecto, IdUsuario);
        }

        public bool L_ActualizarProyecto(CatProyectos Proyecto)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_ActualizarProyectos(Proyecto);
            return Resultado;
        }

        public bool L_EliminarProyecto(int IdProyecto)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_EliminarProyectos(IdProyecto);
            return Resultado;
        }

        #endregion

        #region Métodos Auxiliares:


        public List<CatPreguntas> L_SeleccionarPreguntas(int IdTecnologia)
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarPreguntas(IdTecnologia);
            List<CatPreguntas> ListaPreguntas = new List<CatPreguntas>();
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatPreguntas Pregunta = new CatPreguntas();
                    Pregunta.IdPregunta = (Int32)dr["IdPregunta"];
                    Pregunta.Pregunta = dr["Pregunta"].ToString();

                    ListaPreguntas.Add(Pregunta);
                }
            }
            else
            {
                ListaPreguntas = new List<CatPreguntas>();

            }
            return ListaPreguntas;

        }

        public List<ProyectoPreguntas> L_SeleccionarProyectoPreguntas(int IdProyecto)
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarProyectoPreguntas(IdProyecto);
            List<ProyectoPreguntas> ListaPreguntas = new List<ProyectoPreguntas>();
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    if (!dr["Pregunta"].ToString().Contains("MW"))
                    {
                        ProyectoPreguntas ProyectoPregunta = new ProyectoPreguntas();
                        ProyectoPregunta.IdPregunta = (Int32)dr["IdPregunta"];
                        ProyectoPregunta.Pregunta = dr["Pregunta"].ToString();
                        ProyectoPregunta.Respuesta = Convert.ToBoolean(dr["Respuesta"]);
                        ProyectoPregunta.Modificar = Convert.ToBoolean(dr["Modificar"]);

                        ListaPreguntas.Add(ProyectoPregunta);
                    }

                }
            }
            else
            {
                ListaPreguntas = new List<ProyectoPreguntas>();

            }
            return ListaPreguntas;

        }

        public List<CatEstatusProyecto> L_SeleccionarEstatusProyecto()
        {
            List<CatEstatusProyecto> ListaEstatusProyecto = new List<CatEstatusProyecto>();
            DtAuxiliar = DatosAuxiliar.D_SeleccionarEstatusProyectos();
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatEstatusProyecto EstatusProyecto = new CatEstatusProyecto();
                    EstatusProyecto.IdEstatusProyecto = (Int32)dr["IdEstatusProyecto"];
                    EstatusProyecto.EstadoProyecto = dr["EstatusProyecto"].ToString();
                    ListaEstatusProyecto.Add(EstatusProyecto);
                }

                return ListaEstatusProyecto;
            }
            else
            {
                ListaEstatusProyecto = new List<CatEstatusProyecto>();
                return ListaEstatusProyecto;
            }
        }

        public bool L_ActualizarProyectoSeguimiento(int IdProyecto, int IdEstatusProyecto)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_ActualizarProyectoSeguimiento(IdProyecto, IdEstatusProyecto);
            return Resultado;
        }

        public string L_SeleccionarProyectoSeguimiento(int IdProyecto)
        {
            string IdEstatus = "NO CAPTURADO";
            IdEstatus = DatosAuxiliar.D_SeleccionarProyectoSeguimiento(IdProyecto);
            return IdEstatus;
        }

        public bool L_ActualizarProyectoPregunta(int IdProyecto, int IdPregunta, bool Respuesta)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_ActualizarProyectoPreguntas(IdProyecto, IdPregunta, Respuesta);
            return Resultado;
        }

        public bool L_ResetProyectoPregunta(int IdProyecto)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_ResetProyectoPreguntas(IdProyecto);
            return Resultado;
        }

        public bool L_ActualizarAvanceProyecto(int IdProyecto, float Avance)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_ActualizarAvanceProyecto(IdProyecto, Avance);
            return Resultado;
        }

        public NotificacionInicioTramite L_SeleccionarDatosNotificacionInicioTramite(int IdProyecto, string Homoclave, string RFCRL)
        {
            NotificacionInicioTramite Notificacion = new NotificacionInicioTramite();
            DtAuxiliar = DatosAuxiliar.D_SeleccionarDatosNotificacionInicioTramite(IdProyecto, Homoclave, RFCRL);
            try
            {
                if (DtAuxiliar.Rows.Count > 0)
                {
                    Notificacion.IdEstatusTramite = (Int32)DtAuxiliar.Rows[0][0];
                    Notificacion.IdGlobal = DtAuxiliar.Rows[0][1].ToString();
                    Notificacion.HomoclaveGeneral = DtAuxiliar.Rows[0][2].ToString();
                    Notificacion.Dependencia = DtAuxiliar.Rows[0][3].ToString();
                    Notificacion.CorreoResponsable = DtAuxiliar.Rows[0][4].ToString();
                    Notificacion.CorreoRL = DtAuxiliar.Rows[0][5].ToString();
                    Notificacion.NombreRL = DtAuxiliar.Rows[0][6].ToString();
                    Notificacion.Tecnologia = DtAuxiliar.Rows[0][7].ToString();
                }
                return Notificacion;
            }
            catch (Exception ex)
            {
                return Notificacion;
            }
        }

        #endregion

        #region Reportes

        public List<CatProyectos> L_ReporteProyectos(int? IdEntidadFed, int? IdEmpresa, int? idMuncipio, DateTime? FechaInicio, DateTime? FechaFin, int? idEstatusProyecto)
        {
            List<CatProyectos> ListaProyectos = new List<CatProyectos>();
            DataTable dtProyectos = DatosAuxiliar.D_ReporteProyectos(IdEntidadFed, IdEmpresa, idMuncipio, FechaInicio, FechaFin, idEstatusProyecto);

            if (dtProyectos.Rows.Count > 0)
            {

                foreach (DataRow row in dtProyectos.Rows)
                {
                    CatProyectos pro = new CatProyectos();
                    pro.P_IdEntidadFederativa = (Int32)row["IdEntidadFederativa"];
                    pro.P_EntidadFederativa = row["NombreEntidadF"].ToString();
                    pro.P_IdEmpresa = (Int32)row["IdEmpresa"];
                    pro.P_Empresa = row["NombreEmpresa"].ToString();
                    pro.P_IdTecnologia = (Int32)row["IdTecnologia"];
                    pro.P_Tecnologia = row["Tecnologia"].ToString();
                    pro.P_IdMunicipio = (Int32)row["IdMunicipio"];
                    pro.P_Municipio = row["Municipio"].ToString();
                    pro.P_EstatusProyecto = row["EstadoProyecto"].ToString();
                    pro.P_NombreProyecto = row["NombreProyecto"].ToString();
                    pro.P_Latitud = (double)row["Latitud"];
                    pro.P_Longitud = (double)row["Longitud"];
                    pro.P_CapacidadInstalada = (double)row["CapacidadInstalada"];
                    pro.P_GeneracionAnual = (double)row["GeneracionAnual"];
                    pro.P_FactorPlanta = (double)row["FactorPlanta"];
                    pro.P_MontoInversion = (double)row["MontoInversion"];
                    pro.P_FechaRegistro = (DateTime)row["FechaRegistro"];
                    pro.P_IdProyecto = (Int32)row["IdProyecto"];
                    ListaProyectos.Add(pro);
                }
            }
            return ListaProyectos;
        }

        #endregion

    }
}