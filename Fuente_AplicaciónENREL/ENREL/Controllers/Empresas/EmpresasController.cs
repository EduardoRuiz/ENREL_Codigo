using ENREL.Controllers.Auxiliar;
using ENREL.Models.Empresas;
using ENREL.Models.Usuarios;
using ENREL.Models.RepresentantesLegales;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace ENREL.Controllers.Empresas
{
    public class EmpresasController : Controller
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();

        #endregion

        #region Páginas:

        public ActionResult Detalles()
        {
            CatEmpresas Empresa = new CatEmpresas();
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                Empresa = LogicaEmpresa.L_DetallesEmpresa(Usuario.U_IdEmpresa);
                if (Empresa.E_CodigoPostal == "") { Empresa.E_CodigoPostal = null; }
                return View(Empresa);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Empresa: Detalles");
                return View("Index", "Home");
            }
        }

        #endregion

        #region Métodos Auxiliares:

        [HttpPost]
        public ActionResult EjecutarAccionEmpresa(CatEmpresas Empresa, string Accion, IEnumerable<HttpPostedFileBase> files)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            MetodosGenerales MetodoGeneral = new MetodosGenerales();
            string Validacion = "";

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario <= 2)
            {               
                switch (Accion)
                {

                    case "Actualizar":
                        try
                        {
                            Validacion = MetodoGeneral.ValidarFIEL(files, Empresa.E_ClavePrivada, Empresa.E_RFC);
                            if (Validacion == "Validación exitosa")
                            {
                                if (LogicaEmpresa.L_ActualizarEmpresa(Empresa))
                                {
                                    return RedirectToAction("InicioInversionista", "Home");
                                }
                                else
                                {
                                    TempData["notice"] = "El proyecto no ha sido actualizado, por favor revise la información";
                                    Session["TipoAlerta"] = "Error";
                                    CargarListasDesplegables(Empresa.E_CodigoPostal, Empresa.E_IdEntidadFederativa, Empresa.E_IdMunicipio, Empresa.E_IdLocalidad);
                                    return RedirectToAction("Actualizar", new { IdEmpresa = Empresa.E_IdEmpresa });
                                }
                            }
                            else
                            {
                                TempData["notice"] = "Los datos de la empresa no han sido actualizados, credenciales inválidas";
                                Session["TipoAlerta"] = "Error";
                                CargarListasDesplegables(Empresa.E_CodigoPostal, Empresa.E_IdEntidadFederativa, Empresa.E_IdMunicipio, Empresa.E_IdLocalidad);
                                return RedirectToAction("Actualizar", new { IdEmpresa = Empresa.E_IdEmpresa });
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["notice"] = "No se pudo establecer conexión con el SAT, intente de nuevo por favor.";
                            Session["TipoAlerta"] = "Error";
                            CargarListasDesplegables(Empresa.E_CodigoPostal, Empresa.E_IdEntidadFederativa, Empresa.E_IdMunicipio, Empresa.E_IdLocalidad);
                            return RedirectToAction("Actualizar", new { IdEmpresa = Empresa.E_IdEmpresa });
                        }
                        

                    case "Eliminar":
                        try
                        {
                            //string RFCRepresentante = MetodoGeneral.ObtenerRFCRepresentante(files);
                            //LogicaRepresentantesLegales LogicaRepresentanteLegal = new LogicaRepresentantesLegales();

                            Validacion = MetodoGeneral.ValidarFIEL(files, Empresa.E_ClavePrivada, Empresa.E_RFC);

                           

                            if (Validacion == "Validación exitosa")
                            {
                                if (LogicaEmpresa.L_EliminarEmpresa(Empresa.E_IdEmpresa))
                                {
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    TempData["notice"] = "No es posible eliminar una empresa si existen proyectos con trámites sin concluir, en caso de querer dar de baja los proyectos pendientes, favor de comunicarse con un administrador de SENER.";
                                    Session["TipoAlerta"] = "Error";
                                    CargarListasDesplegables(Empresa.E_CodigoPostal, Empresa.E_IdEntidadFederativa, Empresa.E_IdMunicipio, Empresa.E_IdLocalidad);
                                    return RedirectToAction("Eliminar", new { IdEmpresa = Empresa.E_IdEmpresa });
                                }
                            }
                            else
                            {
                                TempData["notice"] = "La empresa no ha sido eliminado, credenciales inválidas";
                                Session["TipoAlerta"] = "Error";
                                CargarListasDesplegables(Empresa.E_CodigoPostal, Empresa.E_IdEntidadFederativa, Empresa.E_IdMunicipio, Empresa.E_IdLocalidad);
                                return RedirectToAction("Eliminar", new { IdEmpresa = Empresa.E_IdEmpresa });
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["notice"] = "No se pudo validar su firma en el SAT, revise su infomración e intente de nuevo por favor.";
                            CargarListasDesplegables(Empresa.E_CodigoPostal, Empresa.E_IdEntidadFederativa, Empresa.E_IdMunicipio, Empresa.E_IdLocalidad);
                            return RedirectToAction("Eliminar", new { IdEmpresa = Empresa.E_IdEmpresa });
                        }
                    default:
                        return RedirectToAction("Index");
                }

            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }

        private void CargarListasDesplegables(string CodigoPostal, int? IdEntidadFederativa, int? IdMunicipio, int? IdLocalidad)
        {
            ViewBag.EntidadesFederativas = MetodoGeneral.CargarEntidadesFederativas(CodigoPostal);
            ViewBag.Municipios = MetodoGeneral.CargarMunicipios(CodigoPostal, IdEntidadFederativa, IdMunicipio, IdLocalidad);
            ViewBag.Localidades = MetodoGeneral.CargarLocalidades(CodigoPostal, IdEntidadFederativa, IdMunicipio, IdLocalidad);
            ViewBag.TiposAsentamiento = MetodoGeneral.CargarTiposAsentamiento();
            ViewBag.TiposVialidad = MetodoGeneral.CargarTiposVialidad();
            ViewBag.Tecnologias = MetodoGeneral.CargarTecnologias();
        }

        #endregion
    }
}