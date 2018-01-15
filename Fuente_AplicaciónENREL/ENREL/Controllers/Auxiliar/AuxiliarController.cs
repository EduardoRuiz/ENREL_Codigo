using ENREL.Models.CodigosPostales;
using ENREL.Models.EntidadesFederativas;
using ENREL.Models.Localidades;
using ENREL.Models.Municipios;
using ENREL.Models.Proyectos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace ENREL.Controllers.Auxiliar
{
    public class AuxiliarController : Controller
    {
        #region Variables:

        LogicaEntidadesFederativas LogicaEntidadesFederativas = new LogicaEntidadesFederativas();
        LogicaMunicipios LogicaMunicipios = new LogicaMunicipios();
        LogicaLocalidades LogicaLocalidades = new LogicaLocalidades();
        LogicaCodigosPostales LogicaCodigosPostales = new LogicaCodigosPostales();
        LogicaProyectos LogicaProyecto = new LogicaProyectos();

        #endregion

        #region Métodos para actualizar listas desplegables por AJAX

        [HttpGet]
        public JsonResult GetEntidadesFederativas(string StrIdCodigoPostal)
        {
            List<CatEntidadesFederativas> ListaEntidadesFederativas = new List<CatEntidadesFederativas>();
            int IdCodigoPostal = 0;
            Int32.TryParse(StrIdCodigoPostal, out IdCodigoPostal);
            if (IdCodigoPostal > 0)
            {
                ListaEntidadesFederativas = LogicaEntidadesFederativas.L_SeleccionarEntidadesFederativas(IdCodigoPostal, null);
            }
            else
            {
                ListaEntidadesFederativas = LogicaEntidadesFederativas.L_SeleccionarEntidadesFederativas(null, null);
            }

            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = ListaEntidadesFederativas,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Request no válida"
                };
            }
        }

        [HttpGet]
        public JsonResult GetMunicipios(string StrIdEntidadFederativa)
        {
            List<UbicacionPorCP> ListaUbicacionesPorCP = new List<UbicacionPorCP>();
            List<CatMunicipios> ListaMunicipios = new List<CatMunicipios>();

            LogicaCodigosPostales LogicaCodigosPostales = new LogicaCodigosPostales();
            LogicaMunicipios LogicaMunicipios = new LogicaMunicipios();

            int? IdCodigoPostal = null, IdEntidadFederativa = null, IdMunicipio = null;

            if (StrIdEntidadFederativa != null && StrIdEntidadFederativa != "")
            { IdEntidadFederativa = Int32.Parse(StrIdEntidadFederativa); }


            ListaMunicipios = LogicaMunicipios.L_SeleccionarMunicipios(IdCodigoPostal, IdEntidadFederativa, IdMunicipio);

            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = ListaMunicipios,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Reques no valida"
                };
            }
        }

        [HttpGet]
        public JsonResult GetLocalidades( string StrIdEntidadFederativa, string StrIdMunicipio )
        {
            List<CatLocalidades> ListaLocalidades = new List<CatLocalidades>();
            List<UbicacionPorCP> ListaUbicacionesPorCP = new List<UbicacionPorCP>();
            
            LogicaCodigosPostales LogicaCodigosPostales = new LogicaCodigosPostales();
            LogicaLocalidades LogicaLocalidades = new LogicaLocalidades();
            int? IdCodigoPostal = null, IdEntidadFederativa = null, IdMunicipio = null, IdLocalidad = null;

            if (StrIdEntidadFederativa != null && StrIdEntidadFederativa != "")
            { IdEntidadFederativa = Int32.Parse(StrIdEntidadFederativa); }

            if (StrIdMunicipio != null && StrIdMunicipio != "")
            { IdMunicipio = Int32.Parse(StrIdMunicipio); }

            if(IdEntidadFederativa > 0 && IdMunicipio > 0)
            {
                ListaLocalidades = LogicaLocalidades.L_SeleccionarLocalidades(IdCodigoPostal, IdEntidadFederativa, IdMunicipio, IdLocalidad);
            }
            
            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = ListaLocalidades,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Request no valida"
                };
            }
        }

        [HttpGet]
        public JsonResult GetPreguntas(string StrIdTecnologia)
        {
        List<CatPreguntas> ListaPreguntas = new List<CatPreguntas>();
            int IdTecnologia = 0;
            Int32.TryParse(StrIdTecnologia, out IdTecnologia);

            ListaPreguntas = LogicaProyecto.L_SeleccionarPreguntas(IdTecnologia);
            ViewBag.ListaPreguntas = new SelectList(ListaPreguntas, "IdPregunta", "Pregunta");


            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = ListaPreguntas,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Request no válida"
                };
            }
        }

        [HttpGet]
        public JsonResult GetProyectoPreguntas(string StrIdProyecto)
        {
            List<ProyectoPreguntas> ListaPreguntas = new List<ProyectoPreguntas>();
            int IdProyecto = 0;
            Int32.TryParse(StrIdProyecto, out IdProyecto);

            ListaPreguntas = LogicaProyecto.L_SeleccionarProyectoPreguntas(IdProyecto);
            //ViewBag.ListaPreguntas = new SelectList(ListaPreguntas, "IdPregunta", "Pregunta");


            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = ListaPreguntas,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Request no válida"
                };
            }
        }

        #endregion

        #region Métodos para enviar proyecto a dependencias por webservice

        [HttpGet]
        public string EnviarProyectoADependenciaWS(string StrIdCodigoPostal)
        {
            string respuesta = "0";

            
            return respuesta;
        }

        #endregion
    }
}