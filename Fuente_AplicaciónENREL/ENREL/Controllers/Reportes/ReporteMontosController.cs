
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using ENREL.Models.Empresas;
using ENREL.Models.EntidadesFederativas;
using ENREL.Models.Municipios;
using ENREL.Models.Tecnologias;
using ENREL.Models.Usuarios;
using ENREL.Models.Graficas;
using Newtonsoft.Json;

namespace ENREL.Controllers.Reportes
{
    public class ReporteMontosController : Controller
    {
        #region Variables:

        LogicaTecnologias logTecnologias = new LogicaTecnologias();
        LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
        List<CatTecnologias> listaTecnologias = new List<CatTecnologias>();
        List<CatEmpresas> ListaEmp = new List<CatEmpresas>();
        List<CatEmpresas> ListaEmpresasMontos = new List<CatEmpresas>();
        List<CatEntidadesFederativas> ListaEntFed = new List<CatEntidadesFederativas>();
        List<CatMunicipios> ListaMunicipios = new List<CatMunicipios>();
        LogicaEntidadesFederativas LogEntFederativa = new LogicaEntidadesFederativas();
        LogicaMunicipios logMun = new LogicaMunicipios();

        #endregion

        #region Páginas:

        public ActionResult Index(FormCollection Formulario)
        {
            DateTime? FechaInicio = null;
            DateTime? FechaFin = null;

            if (Formulario.Count > 0)
            {
                try { FechaInicio = Convert.ToDateTime(Formulario[0]); TempData["FechaInicio"] = String.Format("{0:dd/MMMM/yyyy}", FechaInicio); }
                catch { }
                try { FechaFin = Convert.ToDateTime(Formulario[1]); TempData["FechaFin"] = String.Format("{0:dd/MMMM/yyyy}", FechaFin); }
                catch { }
            }
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

                if (Usuario != null && Usuario.U_IdUsuario != 0)
                {
                    CargarGrafica_MontosInversionTecnologia(FechaInicio, FechaFin);
                    CargarGrafica_MontosInversionEntidadFederativa(FechaInicio, FechaFin);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Consultor");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Consultor");
            }
        }

        #endregion

        #region Métodos Auxiliares:

        private void CargarGrafica_MontosInversionTecnologia(DateTime? FechaInicio, DateTime? FechaFin)
        {
            //Grafica 1
            LogicaGraficas LogGraficas = new LogicaGraficas();
            List<CatGraficas> ListaGraficas = new List<CatGraficas>();
            List<CatGraficas> ListaMontoInversion = new List<CatGraficas>();

            ListaGraficas = LogGraficas.L_GraficaInversionPorTecnologia(FechaInicio,FechaFin);

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