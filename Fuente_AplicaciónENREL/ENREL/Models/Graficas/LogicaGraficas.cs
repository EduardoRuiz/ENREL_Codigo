using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ENREL.Models.Graficas
{
    public class LogicaGraficas
    {
        #region Variables:

        DatosGraficas DtosGraficas = new DatosGraficas();
        List<CatGraficas> ListaGraficas = new List<CatGraficas>();
        List<CatGraficas> ListaMontoInversion = new List<CatGraficas>();

        #endregion

        #region Métodos Auxiliares:

        public List<CatGraficas> L_GraficaProyectoPorTecnologia(int? IdEntidadFed, int? IdEmpresa, int? idMuncipio, DateTime? FechaInicio, DateTime? FechaFin, int? idEstatusProyecto)
        {

            DataTable dtEmpresas = DtosGraficas.D_GraficoProyectosPorTecnologia( IdEntidadFed, IdEmpresa, idMuncipio, FechaInicio, FechaFin, idEstatusProyecto);

            if (dtEmpresas.Rows.Count > 0)
            {

                foreach (DataRow row in dtEmpresas.Rows)
                {
                    CatGraficas RepoGra = new CatGraficas();
                    RepoGra.TotalDatos = (Int32)row["CantidadProyectos"];
                    RepoGra.Categoria = row["Tecnologia"].ToString();
                    ListaGraficas.Add(RepoGra);
                }
            }
            return ListaGraficas;
        }

        public List<CatGraficas> L_GraficaProyectoPorEntidadFederativa(int? IdEntidadFed, int? IdEmpresa, int? idMuncipio, DateTime? FechaInicio, DateTime? FechaFin, int? idEstatusProyecto)
        {

            DataTable dtEmpresas = DtosGraficas.D_GraficoProyectosPorEntidadFederativa(IdEntidadFed, IdEmpresa, idMuncipio, FechaInicio, FechaFin, idEstatusProyecto);

            if (dtEmpresas.Rows.Count > 0)
            {

                foreach (DataRow row in dtEmpresas.Rows)
                {
                    CatGraficas RepoGra = new CatGraficas();
                    RepoGra.TotalDatos = (Int32)row["CantidadProyectos"];
                    RepoGra.Categoria = row["EntidadFederativa"].ToString();
                    ListaGraficas.Add(RepoGra);
                }
            }
            return ListaGraficas;
        }

        public List<CatGraficas> L_GraficaInversionPorTecnologia(DateTime? FechaInicio, DateTime? FechaFin)
        {
            DataTable dtMontoInversion = new DataTable();
            dtMontoInversion.Rows.Clear();
            dtMontoInversion.Columns.Clear();

            dtMontoInversion = DtosGraficas.D_GraficaInversionPorTecnologia(FechaInicio,FechaFin);

            if (dtMontoInversion.Rows.Count > 0)
            {

                foreach (DataRow row in dtMontoInversion.Rows)
                {
                    CatGraficas MontoInversion = new CatGraficas();
                    MontoInversion.MontoInversion = (double)row["MontoInversion"];
                    MontoInversion.Categoria = row["Tecnologia"].ToString();
                    MontoInversion.TotalDatos = (Int32)row["TotalDatos"];
                    ListaMontoInversion.Add(MontoInversion);
                }
            }
            return ListaMontoInversion;
        }

        public List<CatGraficas> L_GraficaInversionPorEntidadFederativa(DateTime? FechaInicio, DateTime? FechaFin)
        {
            DataTable dtMontoInversion = new DataTable();
            dtMontoInversion.Rows.Clear();
            dtMontoInversion.Columns.Clear();

            dtMontoInversion = DtosGraficas.D_GraficaInversionPorEntidadFederativa(FechaInicio, FechaFin);

            if (dtMontoInversion.Rows.Count > 0)
            {

                foreach (DataRow row in dtMontoInversion.Rows)
                {
                    CatGraficas MontoInversion = new CatGraficas();
                    MontoInversion.MontoInversion = (double)row["MontoInversion"];
                    MontoInversion.Categoria = row["EntidadFederativa"].ToString();
                    MontoInversion.TotalDatos = (Int32)row["TotalDatos"];
                    ListaMontoInversion.Add(MontoInversion);
                }
            }
            return ListaMontoInversion;
        }

        public List<CatGraficas> L_GraficaEmpresasPorTecnologia(int? IdEntidadFed, int? IdMunicipio, DateTime? FechaInicio, DateTime? FechaFin, int? IdTecnologia)
        {

            DataTable dtEmpresas = DtosGraficas.D_GraficoEmpresasPorTecnologia(IdEntidadFed, IdMunicipio, FechaInicio, FechaFin, IdTecnologia);

            if (dtEmpresas.Rows.Count > 0)
            {

                foreach (DataRow row in dtEmpresas.Rows)
                {
                    CatGraficas RepoGra = new CatGraficas();
                    RepoGra.TotalDatos = (Int32)row["CantidadEmpresas"];
                    RepoGra.Categoria = row["Tecnologia"].ToString();
                    ListaGraficas.Add(RepoGra);
                }
            }
            return ListaGraficas;
        }

        public List<CatGraficas> L_GraficaEmpresasPorEntidadFederativa(int? IdEntidadFed, int? IdMunicipio, DateTime? FechaInicio, DateTime? FechaFin, int? IdTecnologia)
        {

            DataTable dtEmpresas = DtosGraficas.D_GraficoEmpresasPorEntidadFederativa( IdEntidadFed, IdMunicipio, FechaInicio, FechaFin, IdTecnologia);

            if (dtEmpresas.Rows.Count > 0)
            {

                foreach (DataRow row in dtEmpresas.Rows)
                {
                    CatGraficas RepoGra = new CatGraficas();
                    RepoGra.TotalDatos = (Int32)row["CantidadEmpresas"];
                    RepoGra.Categoria = row["EntidadFederativa"].ToString();
                    ListaGraficas.Add(RepoGra);
                }
            }
            return ListaGraficas;
        }

        #endregion
               
    }
}