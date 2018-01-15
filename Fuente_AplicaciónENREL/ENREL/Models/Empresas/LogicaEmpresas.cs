using ENREL.Models.Consultar;
using ENREL.Models.Proyectos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ENREL.Models.Empresas
{
    public class LogicaEmpresas
    {
        #region Variables:

        DatosEmpresas DatosAuxiliar = new DatosEmpresas();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos Princiaples:

        public List<CatEmpresas> L_SeleccionarEmpresas()
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarEmpresas();
            List<CatEmpresas> ListaEmpresas = new List<CatEmpresas>();
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatEmpresas Empresa = new CatEmpresas();
                    Empresa.E_IdEmpresa = (Int32)dr["IdEmpresa"];
                    Empresa.E_NombreComercial = dr["NombreComercial"].ToString();

                    ListaEmpresas.Add(Empresa);
                }
            }
            return ListaEmpresas;

        }

        public CatEmpresas L_DetallesEmpresa(int IdEmpresa)
        {
            CatEmpresas ListaEmpresasLegales = new CatEmpresas();

            DtAuxiliar = DatosAuxiliar.D_DetallesEmpresas(IdEmpresa);
            CatEmpresas Empresa = new CatEmpresas();
            if (DtAuxiliar.Rows.Count > 0)
            {
                Empresa.E_IdEmpresa = (Int32)DtAuxiliar.Rows[0]["IdEmpresa"];
                Empresa.E_NombreComercial = DtAuxiliar.Rows[0]["NombreComercial"].ToString();
                Empresa.E_RazonSocial = DtAuxiliar.Rows[0]["RazonSocial"].ToString();

                Empresa.E_Lada = DtAuxiliar.Rows[0]["Lada"].ToString();
                Empresa.E_TelefonoFijo = DtAuxiliar.Rows[0]["TelefonoFijo"].ToString();
                Empresa.E_CorreoElectronico = DtAuxiliar.Rows[0]["CorreoElectronico"].ToString();

                Empresa.E_RFC = DtAuxiliar.Rows[0]["RFC"].ToString();
                Empresa.E_IdTipoVialidad = (Int32)DtAuxiliar.Rows[0]["IdTipoCalle"];
                Empresa.E_Calle = DtAuxiliar.Rows[0]["Calle"].ToString();
                //Empresa.E_NumeroExterior = (Int32)DtAuxiliar.Rows[0]["NumeroExterior"]; //07/03/2017
                Empresa.E_NumeroExterior = DtAuxiliar.Rows[0]["NumeroExterior"].ToString();
                try
                { Empresa.E_NumeroInterior = DtAuxiliar.Rows[0]["NumeroInterior"].ToString(); }
                catch (Exception ex) { };
                Empresa.E_IdTipoAsentamiento = (Int32)DtAuxiliar.Rows[0]["IdTipoColonia"];
                Empresa.E_Colonia = DtAuxiliar.Rows[0]["Colonia"].ToString();
                //Cambio de código postal
                //try
                //{ Empresa.E_CodigoPostal = DtAuxiliar.Rows[0]["CodigoPostal"].ToString(); }
                //catch (Exception ex) { };
                Empresa.E_CodigoPostal = DtAuxiliar.Rows[0]["CodigoPostal"].ToString();
                Empresa.E_IdLocalidad = (Int32)DtAuxiliar.Rows[0]["IdLocalidad"];
                Empresa.E_Localidad = DtAuxiliar.Rows[0]["Localidad"].ToString();
                Empresa.E_IdMunicipio = (Int32)DtAuxiliar.Rows[0]["IdMunicipio"];
                Empresa.E_Municipio = DtAuxiliar.Rows[0]["Municipio"].ToString();
                Empresa.E_IdEntidadFederativa = (Int32)DtAuxiliar.Rows[0]["IdEntidadFederativa"];
                Empresa.E_EntidadFederativa = DtAuxiliar.Rows[0]["EntidadFederativa"].ToString();

            }

            return Empresa;
        }

        public CatEmpresas L_DetallesEmpresaPorRFC(string RFC)
        {
            CatEmpresas ListaEmpresasLegales = new CatEmpresas();

            DtAuxiliar = DatosAuxiliar.D_DetallesEmpresasPorRFC(RFC);
            CatEmpresas Empresa = new CatEmpresas();
            if (DtAuxiliar.Rows.Count > 0)
            {
                Empresa.E_IdEmpresa = (Int32)DtAuxiliar.Rows[0]["IdEmpresa"];
                Empresa.E_NombreComercial = DtAuxiliar.Rows[0]["NombreComercial"].ToString();
                Empresa.E_RazonSocial = DtAuxiliar.Rows[0]["RazonSocial"].ToString();
                Empresa.E_RFC = DtAuxiliar.Rows[0]["RFC"].ToString();
            }

            return Empresa;
        }

        public bool L_ActualizarEmpresa(CatEmpresas Empresa)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_ActualizarEmpresa(Empresa);
            return Resultado;
        }

        public bool L_EliminarEmpresa(int IdEmpresa)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_EliminarEmpresa(IdEmpresa);
            return Resultado;
        }

        #endregion

        #region Métodos Auxiliares:

        public int ContarProyectos(int IdEmpresa)
        {
            int Resultado = 0;
            DatosProyectos DatosProyecto = new DatosProyectos();
            DtAuxiliar = DatosProyecto.D_SeleccionarProyectos(IdEmpresa);

            Resultado = DtAuxiliar.Rows.Count;

            return Resultado;
        }

        #endregion

        #region Métodos Consultor:

        public List<CatEmpresas> L_ReporteEmpresas(int? IdEntidadFed, int? IdMunicipio, DateTime? FechaInicio, DateTime? FechaFin, int? IdTecnologia)
        {

            DataTable dtEmpresas = DatosAuxiliar.D_ReporteEmpresas(IdEntidadFed, IdMunicipio, FechaInicio, FechaFin, IdTecnologia);
            List<CatEmpresas> ListaEmpresas = new List<CatEmpresas>();
            if (dtEmpresas.Rows.Count > 0)
            {

                foreach (DataRow row in dtEmpresas.Rows)
                {
                    CatEmpresas Empresa = new CatEmpresas();
                    Empresa.E_Municipio = row["NombreMunicipio"].ToString();
                    Empresa.E_RazonSocial = row["RazonSocial"].ToString();
                    Empresa.E_FechaInicio = (DateTime)row["fecharegistro"];
                    Empresa.E_RFC = row["RFC"].ToString();
                    Empresa.E_CorreoElectronico = row["CorreoElectronico"].ToString();
                    Empresa.E_EntidadFederativa = row["NombreEntidad"].ToString();
                    Empresa.E_MontoInversion = (double)row["MontoInversion"];
                    //Empresa.E_Tecnologia = row["Tecnologia"].ToString();
                    ListaEmpresas.Add(Empresa);
                }
            }
            return ListaEmpresas;
        }

        public List<CatEmpresas> ListadoEmpresas()
        {
            List<CatEmpresas> ListaEmpresas = new List<CatEmpresas>();
            DataTable dtEmpresas = DatosAuxiliar.ListaEmpresas();

            if (dtEmpresas.Rows.Count > 0)
            {

                foreach (DataRow row in dtEmpresas.Rows)
                {
                    CatEmpresas Empresa = new CatEmpresas();
                    Empresa.E_IdEmpresa = (Int32)row["IdEmpresa"];
                    Empresa.E_NombreComercial = row["Nombre"].ToString();

                    ListaEmpresas.Add(Empresa);
                }
            }
            return ListaEmpresas;
        }

        
        #endregion
    }
}