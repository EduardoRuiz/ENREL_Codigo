using ENREL.Controllers.Auxiliar;
using ENREL.Models.Empresas;
using ENREL.Models.Proyectos;
using ENREL.Models.RepresentantesLegales;
using ENREL.Models.ServiciosWeb;
using ENREL.Models.TiposDia;
using ENREL.Models.Tramites;
using ENREL.Models.Usuarios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace ENREL.Controllers.Tramites
{
    public class TramitesController : Controller
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        LogicaTramites LogicaTramite = new LogicaTramites();


        #endregion

        #region Páginas:

        public ActionResult Index()
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    List<CatTramites> ListaTramites = new List<CatTramites>();
                    ListaTramites = LogicaTramite.L_SeleccionarTramites();
                    return View(ListaTramites);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Logout", "Home");
                }

            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }

        public ActionResult ActualizarTramite(int IdTramite)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                if (IdTramite > 0)
                {
                    List<CatTiposDia> ListaTiposDias = new List<CatTiposDia>();
                    LogicaTiposDia LogicatiposDia = new LogicaTiposDia();
                    ListaTiposDias = LogicatiposDia.L_ListaTiposDia();
                    ViewBag.ListaTiposDia = new SelectList(ListaTiposDias, "IdTipoDia", "TipoDia");

                    CatTramites CatTramite = LogicaTramite.L_DetallesTramites(IdTramite);
                    ViewBag.HomoClave = CatTramite.T_HomoClave;
                    return View(CatTramite);
                }
                else
                {
                    TempData["notice"] = "Se produjo un error";
                    Session["TipoAlerta"] = "Error";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }


        }

        public ActionResult InsertarTramite()
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                List<CatTiposDia> ListaTiposDias = new List<CatTiposDia>();
                LogicaTiposDia LogicatiposDia = new LogicaTiposDia();
                ListaTiposDias = LogicatiposDia.L_ListaTiposDia();
                ViewBag.ListaTiposDia =  new SelectList(ListaTiposDias, "IdTipoDia", "TipoDia");
                return View();
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }


        }

        public ActionResult EliminarTramite(int IdTramite)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                if (IdTramite != null)
                {
                    CatTramites CatTramite = LogicaTramite.L_DetallesTramites(IdTramite);
                    ViewBag.HomoClave = CatTramite.T_HomoClave;
                    return View(CatTramite);
                }
                else
                {
                    TempData["notice"] = "Se produjo un error";
                    Session["TipoAlerta"] = "Error";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }


        }






        #endregion

        #region Métodos Auxiliares:

        [HttpPost]
        public ActionResult Accion_Insertar(CatTramites NuevoTramite)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            LogicaTramites LogicaTecnologia = new LogicaTramites();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    int IdTramite = LogicaTramite.L_InsertarTramite(NuevoTramite);
                    TempData["notice"] = "El nuevo trámite ha sido agregado";
                    Session["TipoAlerta"] = "Correcto";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["notice"] = "El trámite no se pudo insertar";
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }

        [HttpPost]
        public ActionResult Accion_Actualizar(CatTramites NuevoTramite)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            LogicaTramites LogicaTecnologia = new LogicaTramites();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    if(LogicaTramite.L_ActualizarTramite(NuevoTramite))
                    {
                        TempData["notice"] = "El trámite ha sido actualizado";
                        Session["TipoAlerta"] = "Correcto";
                    }
                    else
                    {
                        TempData["notice"] = "El trámite no ha sido agregado";
                        Session["TipoAlerta"] = "Error";
                    }                    
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["notice"] = "El trámite no se pudo actualizar";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }

        [HttpPost]
        public ActionResult Accion_Eliminar(FormCollection Formulario)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            LogicaTramites LogicaTecnologia = new LogicaTramites();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    int IdTramite = Convert.ToInt32(Formulario[1]);
                    if (IdTramite > 0)
                    {
                        LogicaTramite.L_EliminarTramite(IdTramite);
                        TempData["notice"] = "Trámimte eliminado";
                        Session["TipoAlerta"] = "Precaución";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "Este trámite no existe en la base de datos";
                        Session["TipoAlerta"] = "Error";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    TempData["notice"] = "Lo sentimos, el trámite no se pudo eliminar";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }



        #endregion


    }
}