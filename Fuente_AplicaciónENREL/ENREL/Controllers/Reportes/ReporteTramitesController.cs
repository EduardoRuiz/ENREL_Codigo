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
using ENREL.Models.Tramites;
using ENREL.Models.Usuarios;
using System.Configuration;

namespace ENREL.Controllers
{
    public class ReporteTramitesController : Controller
    {
        #region Variables:

        List<CatTramites> ListaTramites = new List<CatTramites>();

        #endregion

        #region Páginas:

        public ActionResult ReporteTramites(int idProyecto)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario != 0)
            {
                LogicaTramites LogicaTramite = new LogicaTramites();
                ListaTramites = LogicaTramite.L_ReporteTramites(idProyecto);
                Session["ListaTramites"] = ListaTramites;
                ViewBag.ListaTramites = ListaTramites;
                ViewBag.NombreProyecto = ListaTramites[0].T_NombreProyecto;
                return View(ListaTramites);
            }
            else
            {
                return RedirectToAction("InicioConsultor", "Home");
            }
        }

        #endregion

        #region Métodos Auxiliares:

        public void ExportarExcel()
        {
            ViewBag.ListaTramites = Session["ListaTramites"];
            var datasource = ViewBag.ListaTramites;

            WebGrid grid = new WebGrid(source: datasource, canPage: false, canSort: false);
            string GridData = grid.GetHtml(
                columns: new[]
              {
                   grid.Column("T_NombreProyecto","Nombre proyecto"),
                   grid.Column("T_NombreTramite","Nombre tramite"),
                   grid.Column("T_Estatus"),
                   grid.Column("T_FechaInicio","Fecha de registro")
              }).ToString();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
            Response.ContentType = "application/ms-excel";
            Response.Write(GridData);
            Response.End();

        }

        public FileStreamResult PDF()
        {

            ViewBag.ListaTramites = Session["ListaTramites"];
            var datasource = ViewBag.ListaTramites;

            //WebGrid grid = new WebGrid(source: datasource, canPage: false, canSort: false);
            //string GridData = grid.GetHtml(
            //    columns: new[]
            //  {
            //       grid.Column("NombreProyecto","Nombre proyecto"),
            //       grid.Column("T_NombreTramite","Nombre tramite"),
            //       grid.Column("Estatus"),
            //       grid.Column("FechaInicio","Fecha de registro")
            //  }).ToString();

            StringWriter sw = new StringWriter();
            string Reporte = "<Table style= \"width:100%;font-size:12px;border:dotted\"> ";
            Reporte = Reporte + "<tr style=\"font-weight:bold;\"><td>Proyecto</td><td>Tramite</td><td>Estatus</td><td>Registro</td></tr>";

            foreach (var client in datasource)
            {
                sw.WriteLine(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                    client.NombreProyecto,
                    client.T_NombreTramite,
                    client.Estatus,
                    client.FechaInicio));
            }
            Reporte = Reporte + sw.ToString();
            Reporte = Reporte + "</table>";

            string path = ConfigurationManager.AppSettings["PDF_Reportes"].ToString();
            string ContenidoPDF = System.IO.File.ReadAllText(path);
            ContenidoPDF = ContenidoPDF.Replace("#TituloReporte#", "Reporte de trámites");
            ContenidoPDF = ContenidoPDF.Replace("#Reporte#", Reporte);


            //string exportData = String.Format("<html><head>{0}</head><body>{1}</body></html>", "<style>table{ border-spacing: 10px; border-collapse: separate; }</style>", GridData);
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
            sw.WriteLine("\"Nombre proyecto\",\"Nombre tramite\",\"Estatus\",\"Fecha de registro\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ExportToCsv.csv");
            Response.ContentType = "text/csv";
            ViewBag.ListaTramites = Session["ListaTramites"];
            var datasource = ViewBag.ListaTramites;

            foreach (var client in datasource)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                    client.NombreProyecto,
                    client.T_NombreTramite,
                    client.Estatus,
                    client.FechaInicio));
            }
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToTxt()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"Nombre proyecto\",\"Nombre tramite\",\"Estatus\",\"Fecha de registro\"");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ExportToTxt.txt");
            Response.ContentType = "text/txt";
            ViewBag.ListaTramites = Session["ListaTramites"];
            var datasource = ViewBag.ListaTramites;

            foreach (var client in datasource)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                    client.NombreProyecto,
                    client.T_NombreTramite,
                    client.Estatus,
                    client.FechaInicio));
            }
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToXML()
        {
            ViewBag.ListaTramites = Session["ListaTramites"];
            var datasource = ViewBag.ListaTramites;
            ListaTramites = datasource;

            if (datasource.Count > 0)
            {
                var xEle = new XElement("CatTramites",
                    from pro in ListaTramites
                    select new XElement("CatProyectos",
                        new XElement("NombreProyecto", pro.T_NombreProyecto),
                        new XElement("T_NombreTramite", pro.T_NombreTramite),
                        new XElement("Estatus", pro.T_Estatus,
                        new XElement("FechaInicio", pro.T_FechaInicio))));
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