using ENREL.Controllers.Auxiliar;
using ENREL.Models.Consultar;
using ENREL.Models.Empresas;
using ENREL.Models.Graficas;
using ENREL.Models.Municipios;
using ENREL.Models.Proyectos;
using ENREL.Models.Usuarios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ENREL.Controllers.Consultor
{
    public class ConsultorController : Controller
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        List<CatMontosInversion> ListaMontosInversion = new List<CatMontosInversion>();
        LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
        #endregion

        #region Paginas:
        // GET: Consultor
        public ActionResult Index()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario < 3 || Usuario.U_IdTipoUsuario > 4)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                CargarGrafica_MontosInversionEntidadFederativa(null, null);
                CargarGrafica_MontosInversionTecnologia(null, null);

                CargarGrafica_ProyectosPorTecnologia(null, null, null, null, null, null);
                CargarGrafica_ProyectosEntidadFederativa(null, null, null, null, null, null);

                CargarGrafica_EmpresasTecnologia(null, null, null, null, null);
                CargarGrafica_EmpresasEntidadFederativa(null, null, null, null, null);

                return View();

            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: Insertar");
                return RedirectToAction("Index");
            }

        }

        public ActionResult Error()
        {
            return PartialView();
        }

        #endregion

        #region Metodos Auxiliares:

        private void CargarGrafica_ProyectosPorTecnologia(int? IdEntidadFed, int? IdEmpresa, int? idMuncipio, DateTime? FechaInicio, DateTime? FechaFin, int? idEstatusProyecto)
        {
            LogicaGraficas LogGraficas = new LogicaGraficas();
            List<CatGraficas> ListaGraficas = new List<CatGraficas>();

            ListaGraficas = LogGraficas.L_GraficaProyectoPorTecnologia(IdEntidadFed, IdEmpresa, idMuncipio, FechaInicio, FechaFin, idEstatusProyecto);

            var charData = new object[ListaGraficas.Count + 1];
            charData[0] = new object[]
            {
                "Tencologia",
                "Proyectos Activos",
            };
            int j = 0;
            foreach (var i in ListaGraficas)
            {
                j++;
                charData[j] = new object[] { i.Categoria, i.TotalDatos };
            }
            string dts = JsonConvert.SerializeObject(charData, Formatting.None);
            ViewBag.ProyectosPorTecnologia = new HtmlString(dts);
        }

        private void CargarGrafica_ProyectosEntidadFederativa(int? IdEntidadFed, int? IdEmpresa, int? idMuncipio, DateTime? FechaInicio, DateTime? FechaFin, int? idEstatusProyecto)
        {
            LogicaGraficas LogGraficas = new LogicaGraficas();
            List<CatGraficas> ListaGraficas = new List<CatGraficas>();

            ListaGraficas = LogGraficas.L_GraficaProyectoPorEntidadFederativa(IdEntidadFed, IdEmpresa, idMuncipio, FechaInicio, FechaFin, idEstatusProyecto);

            var charData = new object[ListaGraficas.Count + 1];
            charData[0] = new object[]
            {
                "Estado",
                "Proyectos Activos",
            };
            int j = 0;
            foreach (var i in ListaGraficas)
            {
                j++;
                charData[j] = new object[] { i.Categoria, i.TotalDatos };
            }
            string dts = JsonConvert.SerializeObject(charData, Formatting.None);
            ViewBag.ProyectosPorEntidadFederativa = new HtmlString(dts);
        }

        private void CargarGrafica_EmpresasTecnologia(int? IdEntidadFed, int? IdMunicipio, DateTime? FechaInicio, DateTime? FechaFin, int? IdTecnologia)
        {
            LogicaGraficas LogGraficas = new LogicaGraficas();
            List<CatGraficas> ListaGraficas = new List<CatGraficas>();

            ListaGraficas = LogGraficas.L_GraficaEmpresasPorTecnologia(IdEntidadFed, IdMunicipio, FechaInicio, FechaFin, IdTecnologia);

            var charData = new object[ListaGraficas.Count + 1];
            charData[0] = new object[]
            {
                "Tencologia",
                "Empresas",
            };
            int j = 0;
            foreach (var i in ListaGraficas)
            {
                j++;
                charData[j] = new object[] { i.Categoria, i.TotalDatos };
            }
            string dts = JsonConvert.SerializeObject(charData, Formatting.None);
            ViewBag.EmpresasPorTecnologia = new HtmlString(dts);
        }

        private void CargarGrafica_EmpresasEntidadFederativa(int? IdEntidadFed, int? IdMunicipio, DateTime? FechaInicio, DateTime? FechaFin, int? IdTecnologia)
        {
            LogicaGraficas LogGraficas = new LogicaGraficas();
            List<CatGraficas> ListaGraficas = new List<CatGraficas>();

            ListaGraficas = LogGraficas.L_GraficaEmpresasPorEntidadFederativa(IdEntidadFed, IdMunicipio, FechaInicio, FechaFin, IdTecnologia);

            var charData = new object[ListaGraficas.Count + 1];
            charData[0] = new object[]
            {
                "Estado",
                "Empresas",
            };
            int j = 0;
            foreach (var i in ListaGraficas)
            {
                j++;
                charData[j] = new object[] { i.Categoria, i.TotalDatos };
            }
            string dts = JsonConvert.SerializeObject(charData, Formatting.None);
            ViewBag.EmpresasPorEntidadFederativa = new HtmlString(dts);
        }

        private void CargarGrafica_MontosInversionTecnologia(DateTime? FechaInicio, DateTime? FechaFin)
        {
            //Grafica 1
            LogicaGraficas LogGraficas = new LogicaGraficas();
            List<CatGraficas> ListaGraficas = new List<CatGraficas>();
            List<CatGraficas> ListaMontoInversion = new List<CatGraficas>();

            ListaGraficas = LogGraficas.L_GraficaInversionPorTecnologia(FechaInicio, FechaFin);

            var charData = new object[ListaGraficas.Count + 1];
            charData[0] = new object[]
            {
                "Estado",
                "% Inversión",
            };
            int j = 0;
            foreach (var i in ListaGraficas)
            {
                j++;
                charData[j] = new object[] { i.Categoria, i.MontoInversion };
            }
            string dts = JsonConvert.SerializeObject(charData, Formatting.None);
            ViewBag.InversionPorTectnologia = new HtmlString(dts);
        }

        private void CargarGrafica_MontosInversionEntidadFederativa(DateTime? FechaInicio, DateTime? FechaFin)
        {
            //Grafica 1
            LogicaGraficas LogGraficas = new LogicaGraficas();
            List<CatGraficas> ListaGraficas = new List<CatGraficas>();
            List<CatGraficas> ListaMontoInversion = new List<CatGraficas>();

            ListaGraficas = LogGraficas.L_GraficaInversionPorEntidadFederativa(FechaInicio, FechaFin);

            var charData = new object[ListaGraficas.Count + 1];
            charData[0] = new object[]
            {
                "Estado",
                "Inversión(USD)",
            };
            int j = 0;
            foreach (var i in ListaGraficas)
            {
                j++;
                charData[j] = new object[] { i.Categoria, i.MontoInversion };
            }
            string dts = JsonConvert.SerializeObject(charData, Formatting.None);
            ViewBag.InversionPorEntidadFederativa = new HtmlString(dts);
        }

        #endregion

    }
}