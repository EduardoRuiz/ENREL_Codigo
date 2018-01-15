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
using ENREL.Models.EstatusProyecto;
using ENREL.Models.Municipios;
using ENREL.Models.Proyectos;
using ENREL.Models.Usuarios;
using ENREL.Models.Graficas;
using Newtonsoft.Json;
using System.Configuration;
using ENREL.Controllers.Auxiliar;

namespace ENREL.Controllers.ReportesProyectoController
{
    public class ReportesProyectoController : Controller
    {
        #region Variables:

        LogicaEstatusProyecto LogEstatusProyecto = new LogicaEstatusProyecto();
        LogicaProyectos logicaProyects = new LogicaProyectos();
        List<CatProyectos> ListaProyectos = new List<CatProyectos>();
        List<CatEstatusProyecto> listEstatusP = new List<CatEstatusProyecto>();
        List<CatEmpresas> listadEmpresas = new List<CatEmpresas>();
        LogicaEmpresas LogicaEmpresas = new LogicaEmpresas();
        List<CatEntidadesFederativas> ListaEntFed = new List<CatEntidadesFederativas>();
        List<CatMunicipios> ListaMunicipios = new List<CatMunicipios>();
        LogicaEntidadesFederativas LogEntFederativa = new LogicaEntidadesFederativas();
        LogicaMunicipios logMun = new LogicaMunicipios();
        MetodosGenerales MetodoGeneral = new MetodosGenerales();


        #endregion

        #region Páginas:

        public ActionResult ReporteProyectos(FormCollection Formulario)
        {
            int? IdEmpresa = null;
            int? IdEntidad = null;
            int? IdMunicipio = null;
            DateTime? FechaInicio = null;
            DateTime? FechaFin = null;
            int? idEstatusProyecto = null;

            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario < 3 || Usuario.U_IdTipoUsuario > 4)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                if (Formulario.Count > 0)
                {

                    try { IdEntidad = Convert.ToInt32(Formulario[0]); TempData["IdEntidad"] = IdEntidad; }
                    catch { }
                    if (IdEntidad != null)
                    {
                        try { IdMunicipio = Convert.ToInt32(Formulario[1]); TempData["IdMunicipio"] = IdMunicipio; }
                        catch { }
                    }
                    try { IdEmpresa = Convert.ToInt32(Formulario[2]); TempData["IdEmpresa"] = IdEmpresa; }
                    catch { }
                    try { idEstatusProyecto = Convert.ToInt32(Formulario[3]); TempData["idEstatusProyecto"] = idEstatusProyecto; }
                    catch { }
                    try { FechaInicio = Convert.ToDateTime(Formulario[4]); TempData["FechaInicio"] = String.Format("{0:dd/MMMM/yyyy}", FechaInicio); }
                    catch { }
                    try { FechaFin = Convert.ToDateTime(Formulario[5]); TempData["FechaFin"] = String.Format("{0:dd/MMMM/yyyy}", FechaFin); }
                    catch { }
                }
                CargarListasDesplegables(IdEntidad, IdMunicipio, IdEmpresa, idEstatusProyecto);
                CargarGrafica_ProyectosTecnologia(IdEntidad, IdEmpresa, IdMunicipio, FechaInicio, FechaFin, idEstatusProyecto);
                CargarGrafica_ProyectosEntidadFederativa(IdEntidad, IdEmpresa, IdMunicipio, FechaInicio, FechaFin, idEstatusProyecto);

                ListaProyectos = logicaProyects.L_ReporteProyectos(IdEntidad, IdEmpresa, IdMunicipio, FechaInicio, FechaFin, idEstatusProyecto).ToList();
                ViewBag.ListadoProyectos = ListaProyectos;
                Session["ReporteProyectos"] = ListaProyectos;

                return View(ListaProyectos);

            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "RepresentantesLegales: Insertar");
                return RedirectToAction("Index", "Consultor");
            }
        }


        #endregion

        #region Métodos Auxiliares:

        private void CargarListasDesplegables(int? IdEntidad, int? IdMunicipio, int? IdEmpresa, int? IdEstatusProyecto)
        {
            listadEmpresas = LogicaEmpresas.ListadoEmpresas();
            ViewBag.ListadoEmpresas = new SelectList(listadEmpresas, "E_IdEmpresa", "E_NombreComercial", IdEmpresa);

            listEstatusP = LogEstatusProyecto.ListaProyectos();
            ViewBag.ListaEstatusP = new SelectList(listEstatusP, "idEstatusProyecto", "EstadoProyecto", IdEstatusProyecto);

            ListaEntFed = LogEntFederativa.L_SeleccionarEntidadesFederativas(null, null);
            ViewBag.Estados = new SelectList(ListaEntFed, "IdEntidadFederativa", "EntidadFederativa", IdEntidad);

            if (IdEntidad != null)
            {
                ListaMunicipios = logMun.L_SeleccionarMunicipios(null, IdEntidad, null);
            }
            else
            {
                IdMunicipio = null;
                ListaMunicipios = new List<CatMunicipios>();
            }
            ViewBag.Municipios = new SelectList(ListaMunicipios, "IdMunicipio", "Municipio", IdMunicipio);

        }

        private void CargarGrafica_ProyectosTecnologia(int? IdEntidadFed, int? IdEmpresa, int? idMuncipio, DateTime? FechaInicio, DateTime? FechaFin, int? idEstatusProyecto)
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

        public void ExportarExcel()
        {
            ViewBag.CargarProyectos = Session["ReporteProyectos"];
            var datasource = ViewBag.CargarProyectos;

            WebGrid grid = new WebGrid(source: datasource, canPage: false, canSort: false);
            string GridData = grid.GetHtml(
                columns: new[]
                    {
                        grid.Column("P_Empresa"),
                        grid.Column("P_NombreProyecto","Proyecto"),
                        grid.Column("P_Tecnologia"),
                        grid.Column("P_FechaRegistro","Fecha de registro"),
                        grid.Column("P_MontoInversion","Monto de inversión")
                    }).ToString();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
            Response.ContentType = "application/ms-excel";
            Response.Write(GridData);
            Response.End();

        }

        public FileStreamResult PDF()
        {

            ViewBag.CargarProyectos = Session["ReporteProyectos"];
            var datasource = ViewBag.CargarProyectos;

            StringWriter sw = new StringWriter();
            string Reporte = "<Table style= \"width:100%;font-size:12px;border:dotted\"> ";
            Reporte = Reporte + "<tr style=\"font-weight:bold;\"><td>Empresa</td><td>Proyecto</td><td>Tecnología</td><td>Registro</td><td>Inversión(usd)</td></tr>";

            foreach (var client in datasource)
            {
                sw.WriteLine(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td style=\"text-align:right;\">{4}</td></tr>",
                    client.P_Empresa,
                    client.P_NombreProyecto,
                    client.P_Tecnologia,                    
                    client.P_FechaRegistro,
                    client.P_MontoInversion.ToString("C")));
            }
            Reporte = Reporte + sw.ToString();
            Reporte = Reporte + "</table>";

            string path = ConfigurationManager.AppSettings["PDF_Reportes"].ToString();
            string ContenidoPDF = System.IO.File.ReadAllText(path);
            ContenidoPDF = ContenidoPDF.Replace("#TituloReporte#", "Reporte de proyectos");
            ContenidoPDF = ContenidoPDF.Replace("#Reporte#", Reporte);

            var bytes = System.Text.Encoding.UTF8.GetBytes(ContenidoPDF);
            using (var input = new MemoryStream(bytes))
            {
                var output = new MemoryStream();
                var document = new iTextSharp.text.Document(PageSize.A4, 15, 15, 15, 15);
                var writer = PdfWriter.GetInstance(document, output);

                writer.CloseStream = false;
                document.Open();
                //document.Add(new Paragraph("Reporte de listado de proyectos"));

                Font fdefault = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK);


                var xmlWorker = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance();
                xmlWorker.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);

                document.Close();
                output.Position = 0;
                return new FileStreamResult(output, "application/pdf");
            }
        }

        public void ExportToCsv()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"Empresa\",\"Proyecto\",\"Tecnologia\",\"Monto inversión\",\"Fecha de registro\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ExportToCsv.csv");
            Response.ContentType = "text/csv";
            ViewBag.CargarProyectos = Session["ReporteProyectos"];

            var datasource = ViewBag.CargarProyectos;

            foreach (var client in datasource)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                    client.P_Empresa,
                    client.P_NombreProyecto,
                    client.P_Tecnologia,
                    client.P_MontoInversion,
                    client.P_FechaRegistro));
            }
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToTxt()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"Empresa\",\"Proyecto\",\"Tecnologia\",\"Monto inversión\",\"Fecha de registro\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ExportToTxt.txt");
            Response.ContentType = "text/txt";
            ViewBag.CargarProyectos = Session["ReporteProyectos"];

            var datasource = ViewBag.CargarProyectos;

            foreach (var client in datasource)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                    client.P_Empresa,
                    client.P_NombreProyecto,
                    client.P_Tecnologia,
                    client.P_MontoInversion,
                    client.P_FechaRegistro));
            }
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToXML()
        {
            ViewBag.CargarProyectos = Session["ReporteProyectos"];
            var datasource = ViewBag.CargarProyectos;
            ListaProyectos = datasource;

            if (datasource.Count > 0)
            {
                var xEle = new XElement("CatEmpresas",
                    from emp in ListaProyectos
                    select new XElement("CatProyectos",
                        new XElement("Empresa", emp.P_Empresa),
                        new XElement("NombreProyecto", emp.P_NombreProyecto),
                        new XElement("Tecnologia", emp.P_Tecnologia,
                        new XElement("FechaRegistro", emp.P_FechaRegistro,
                        new XElement("MontoInversion", emp.P_MontoInversion)))));
                HttpContext context = System.Web.HttpContext.Current;
                context.Response.Write(xEle);
                context.Response.ContentType = "application/xml";
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=EmpresasData.xml");
                context.Response.End();
            }
        }

        #endregion


    }
        
}