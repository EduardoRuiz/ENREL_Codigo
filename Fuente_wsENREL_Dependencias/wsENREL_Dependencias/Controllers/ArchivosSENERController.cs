using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace wsENREL_Dependencias.Controllers
{
    public class ArchivosSENERController : ApiController
    {
        
        // POST api/archivossener
        public HttpResponseMessage Get(string IdTramite, string NombreDocumento)
        {
            try
            {
                string UrlArchivo = @"C:/inetpub/RepositorioVER/Tramites/" + IdTramite + "/" + NombreDocumento;
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                var stream = new System.IO.FileStream(UrlArchivo, System.IO.FileMode.Open);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/zip");

                return response;    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
