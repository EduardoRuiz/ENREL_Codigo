using ENREL.Controllers.Auxiliar;
using ENREL.Models.DiasInhabiles;
using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ENREL.Controllers
{
    public class DiasInhabilesController : Controller
    {

        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        LogicaDiasInhabiles LogicaDiasInhabiles = new LogicaDiasInhabiles();

        #endregion

        #region Páginas:

        public ActionResult Index(FormCollection Formulario)
        {
            DateTime? FechaCalendario = null;

            if (Formulario.Count > 0)
            {

                try { FechaCalendario = Convert.ToDateTime(Formulario[0]); TempData["FechaCalendario"] = String.Format("{0:dd/MMMM/yyyy}", FechaCalendario); }
                catch { }
            }

            return View();
        }

        [HttpPost]
        public ActionResult FechasHabiles(int Año, int Mes)
        {
            List<CatDiasInhabiles> ListaDiasInhabiles = LogicaDiasInhabiles.L_SeleccionarDiasInhabiles(Mes, Año);
            return View(ListaDiasInhabiles);
        }

        #endregion

        #region Métodos Auxiliares:

        //[HttpPost]
        public ActionResult CambiarEstatusFechaInhabil( int IdDiaInhabil, bool IdEstatusDiaInhabil)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            MetodosGenerales MetodoGeneral = new MetodosGenerales();

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario <= 4)
            {
                LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
                CatDiasInhabiles FechaInhabil = new CatDiasInhabiles();
                FechaInhabil.IdDiaInhabil = IdDiaInhabil;
                FechaInhabil.Activo = !IdEstatusDiaInhabil;

                if (LogicaDiasInhabiles.L_ActualizarDiaInhabil(IdDiaInhabil, !IdEstatusDiaInhabil))
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["notice"] = "Operación no concluida.";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("Index");
                }


            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }

        //[HttpPost]
        public ActionResult InsertarDiaInhabil(CatDiasInhabiles DiaInhabil)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            MetodosGenerales MetodoGeneral = new MetodosGenerales();

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario <= 4)
            {
                LogicaUsuarios LogicaUsuario = new LogicaUsuarios();

                if (LogicaDiasInhabiles.L_InsertarDiaInhabil(DiaInhabil))
                {

                    TempData["notice"] = "La fecha ha sido agregada al calendario de días inhábiles";
                    Session["TipoAlerta"] = "Correcto";
                    return RedirectToAction("GestionUsuariosSENER", "Administrador");
                }
                else
                {
                    TempData["notice"] = "Disculpa, la fecha no ha sido agregada al calendario de días inhábiles";
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