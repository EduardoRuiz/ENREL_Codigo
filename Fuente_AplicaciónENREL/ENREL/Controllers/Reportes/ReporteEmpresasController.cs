using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.Serialization;
using ENREL.Models.Empresas;
using ENREL.Models.EntidadesFederativas;
using ENREL.Models.Municipios;
using ENREL.Models.Usuarios;
using ENREL.Models.Tecnologias;
using ENREL.Models.Graficas;
using Newtonsoft.Json;
using System.Configuration;

namespace ENREL.Controllers
{
    public class ReporteEmpresasController : Controller
    {

        #region Variables:

        LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
        List<CatEmpresas> ListaEmpresas = new List<CatEmpresas>();
        LogicaTecnologias logTecnologias = new LogicaTecnologias();
        List<CatTecnologias> listaTecnologias = new List<CatTecnologias>();
        List<CatEntidadesFederativas> ListaEntFed = new List<CatEntidadesFederativas>();
        List<CatMunicipios> ListaMunicipios = new List<CatMunicipios>();
        LogicaEntidadesFederativas LogEntFederativa = new LogicaEntidadesFederativas();
        LogicaMunicipios logMun = new LogicaMunicipios();

        #endregion

        #region Páginas:

        public ActionResult Index(FormCollection Formulario)
        {
            int? IdEntidad = null;
            int? IdMunicipio = null;
            DateTime? FechaInicio = null;
            DateTime? FechaFin = null;
            int? IdTecnologia = null;

            if (Formulario.Count > 0)
            {

                try { IdEntidad = Convert.ToInt32(Formulario[0]); TempData["IdEntidad"] = IdEntidad; }
                catch { }
                if (IdEntidad != null)
                {
                    try { IdMunicipio = Convert.ToInt32(Formulario[1]); TempData["IdMunicipio"] = IdMunicipio; }
                    catch { }
                }
                try { IdTecnologia = Convert.ToInt32(Formulario[2]); TempData["IdTecnologia"] = IdTecnologia; }
                catch { }
                try { FechaInicio = Convert.ToDateTime(Formulario[3]); TempData["FechaInicio"] = String.Format("{0:dd/MMMM/yyyy}", FechaInicio); }
                catch { }
                try { FechaFin = Convert.ToDateTime(Formulario[4]); TempData["FechaFin"] = String.Format("{0:dd/MMMM/yyyy}", FechaFin); }
                catch { }
            }


            try
            {
                

                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

                if (Usuario != null && Usuario.U_IdUsuario != 0)
                {
                    CargarGrafica_EmpresasEntidadFederativa(IdEntidad, IdMunicipio, FechaInicio, FechaFin, IdTecnologia);
                    CargarGraficaEmpresasTecnologia(IdEntidad, IdMunicipio, FechaInicio, FechaFin, IdTecnologia);
                    CargarListasDesplegables(IdEntidad, IdMunicipio, IdTecnologia);
                    ListaEmpresas = LogicaEmpresa.L_ReporteEmpresas(IdEntidad, IdMunicipio, FechaInicio, FechaFin, IdTecnologia).ToList();
                    ViewBag.ListadoEmpresas = ListaEmpresas;
                    Session["DatosEmpresas"] = ListaEmpresas;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(ListaEmpresas);

        }

        #endregion 

        #region Métodos Auxiliares:

        private void CargarGraficaEmpresasTecnologia(int? IdEntidadFed, int? IdMunicipio, DateTime? FechaInicio, DateTime? FechaFin, int? IdTecnologia)
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

        private void CargarListasDesplegables(int? IdEntidad, int? IdMunicipio, int? IdTecnologia)
        {
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

            listaTecnologias = logTecnologias.L_SeleccionarTecnologias();
            ViewBag.ListaTecnologias = new SelectList(listaTecnologias, "IdTecnologia", "Tecnologia", IdTecnologia);

        }

        public void ExportarExcel()
        {
            ViewBag.CargaEmpresas = Session["DatosEmpresas"];
            var datasource = ViewBag.CargaEmpresas;

            WebGrid grid = new WebGrid(source: datasource, canPage: false, canSort: false);
            string GridData = grid.GetHtml(
                columns: new[]
              {
                    grid.Column("E_RazonSocial", "Razón Social" ),
                    grid.Column("E_RFC"),
                    grid.Column("E_CorreoElectronico" ,"Correo-electrónico"),
                    grid.Column("E_EntidadFederativa"),
                    grid.Column("E_Municipio", "Municipio"),
                    grid.Column("E_FechaInicio", "Fecha de registro"),
                    grid.Column("E_MontoInversion","Monto de inversión")
              }).ToString();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
            Response.ContentType = "application/ms-excel";
            Response.Write(GridData);
            Response.End();

        }

        public FileStreamResult PDF()
        {
            ViewBag.CargaEmpresas = Session["DatosEmpresas"];
            var datasource = ViewBag.CargaEmpresas;

            StringWriter sw = new StringWriter();
            string Reporte = "<Table style= \"width:100%;font-size:12px;border:dotted\"> ";
            Reporte = Reporte + "<tr style=\"font-weight:bold;\"><td>Razón Social</td><td>RFC</td><td>Correo electrónico</td><td>Estado</td><td>Municipio</td><td>Registro</td><td>Inversión</td></tr>";

            foreach (var client in datasource)
            {
                sw.WriteLine(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td style=\"text-align:right;\">{6}</td></tr>",
                    client.E_RazonSocial,
                    client.E_RFC,
                    client.E_CorreoElectronico,
                    client.E_EntidadFederativa,
                    client.E_Municipio,
                    client.E_FechaInicio,
                    client.E_MontoInversion.ToString("C")));
            }
            Reporte = Reporte + sw.ToString();
            Reporte = Reporte + "</table>";

            string path = ConfigurationManager.AppSettings["PDF_Reportes"].ToString();
            string ContenidoPDF = System.IO.File.ReadAllText(path);
            ContenidoPDF = ContenidoPDF.Replace("#TituloReporte#", "Reporte de empresas");
            ContenidoPDF = ContenidoPDF.Replace("#Reporte#", Reporte);

            var bytes = System.Text.Encoding.UTF8.GetBytes(ContenidoPDF);
            using (var input = new MemoryStream(bytes))
            {
                var output = new MemoryStream();
                var document = new iTextSharp.text.Document(PageSize.A4, 15, 15, 0, 0);
                var writer = PdfWriter.GetInstance(document, output);

                writer.CloseStream = false;
                document.Open();

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
            sw.WriteLine("\"Empresa\",\"RFC\",\"Correo electrónico\",\"Estado\",\"Municipio\",\"Fecha de registro\",\"Monto de inversión\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ExportToCsv.csv");
            Response.ContentType = "text/csv";
            ViewBag.CargaEmpresas = Session["DatosEmpresas"];

            var datasource = ViewBag.CargaEmpresas;

            foreach (var client in datasource)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",
                    client.E_RazonSocial,
                    client.E_RFC,
                    client.E_CorreoElectronico,
                    client.E_EntidadFederativa,
                    client.E_Municipio,
                    client.E_FechaInicio,
                    client.E_MontoInversion));
                
            }
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToTxt()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"Empresa\",\"RFC\",\"Correo electrónico\",\"Estado\",\"Municipio\",\"Fecha de registro\",\"Monto de inversión\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ExportToTxt.txt");
            Response.ContentType = "text/txt";
            ViewBag.CargaEmpresas = Session["DatosEmpresas"];

            var datasource = ViewBag.CargaEmpresas;

            foreach (var client in datasource)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",
                    client.E_RazonSocial,
                    client.E_RFC,
                    client.E_CorreoElectronico,
                    client.E_EntidadFederativa,
                    client.E_Municipio,
                    client.E_FechaInicio,
                    client.E_MontoInversion));

            }
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToXML()
        {
            ViewBag.CargaEmpresas = Session["DatosEmpresas"];
            var datasource = ViewBag.CargaEmpresas;
            ListaEmpresas = datasource;

            if (datasource.Count > 0)
            {
                var xEle = new XElement("CatEmpresas",
                    from emp in ListaEmpresas
                    select new XElement("Empresa",
                        new XElement("E_RazonSocial", emp.E_RazonSocial),
                        new XElement("E_RFC", emp.E_RFC),
                        new XElement("E_CorreoElectronico", emp.E_CorreoElectronico),
                        new XElement("E_EntidadFederativa", emp.E_EntidadFederativa),
                        new XElement("E_Municipio", emp.E_Municipio),
                        new XElement("E_FechaInicio", emp.E_FechaInicio),
                        new XElement("E_MontoInversion", emp.E_MontoInversion)
                        ));
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