using ENREL.Controllers.Auxiliar;
using ENREL.Models.Tecnologias;
using ENREL.Models.Tramites;
using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ENREL.Controllers.Tecnologias
{
    public class TecnologiasController : Controller
    {
        #region  Variables:


        #endregion

        #region Páginas:

        public ActionResult Index()
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    LogicaTecnologias LogicaTecnologia = new LogicaTecnologias();
                    List<CatTecnologias> Tecnologias = LogicaTecnologia.L_SeleccionarTecnologias();

                    return View(Tecnologias);
                }
                catch (Exception ex)
                {
                    List<CatTecnologias> Tecnologias = new List<CatTecnologias>();

                    TempData["notice"] = "Se produjo un error en la consulta";
                    Session["TipoAlerta"] = "Error";
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
            return View();
        }

        public ActionResult Insertar()
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    List<CatTramites> ListaTramites = new List<CatTramites>();
                    LogicaTramites LogicaTramites = new LogicaTramites();
                    ListaTramites = LogicaTramites.L_SeleccionarTramites();
                    ViewBag.ListaTramites = new SelectList(ListaTramites, "T_IdTramites", "T_Homoclave");
                    return View();
                }
                catch (Exception ex)
                {
                    TempData["notice"] = "Se produjo un error en la consulta";
                    Session["TipoAlerta"] = "Error";
                    return View("InicioAdministrador", "Home");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }

        public ActionResult ActualizarTecnologia(int IdTecnologia)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    LogicaTecnologias Logicatecnologia = new LogicaTecnologias();
                    
                    CatTecnologias Tecncologia = Logicatecnologia.L_DetallesTecnologias(IdTecnologia);
                    List<CatTecnologiaPreguntas> ListaPreguntas = new List<CatTecnologiaPreguntas>();
                    ListaPreguntas = Logicatecnologia.L_SeleccionarTecnologiaPreguntas(IdTecnologia);

                    List<CatTecnologiaTramites> ListaTramites = new List<CatTecnologiaTramites>();
                    ListaTramites = Logicatecnologia.L_SeleccionarTecnologiaTramites(IdTecnologia);

                    List<CatTramites> ListaTramitesDDL = new List<CatTramites>();
                    LogicaTramites LogicaTramites = new LogicaTramites();
                    ListaTramitesDDL = LogicaTramites.L_SeleccionarTramites();
                    ViewBag.ListaTramites = new SelectList(ListaTramitesDDL, "T_IdTramites", "T_Homoclave");
                    
                    ViewBag.TecnologiaPreguntas = ListaPreguntas;
                    ViewBag.TecnologiaTramites = ListaTramites;
                    ViewBag.IdTecnologia = IdTecnologia;
                    ViewBag.NombreTecnologia = Tecncologia.Tecnologia;
                    

                    return View(Tecncologia);
                }
                catch (Exception ex)
                {
                    TempData["notice"] = "Se produjo un error en la consulta";
                    Session["TipoAlerta"] = "Error";
                    return View("MenuAdministrador", "Administradores");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }

        public ActionResult EliminarTecnologia(int IdTecnologia, string Tecnologia)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                if (IdTecnologia != null)
                {
                    CatTecnologias CatTecnologia = new CatTecnologias();
                    CatTecnologia.IdTecnologia = IdTecnologia;
                    CatTecnologia.Tecnologia = Tecnologia;
                    ViewBag.NombreTecnologia = Tecnologia;
                    return View(CatTecnologia);
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

        #region Métodos auxiliares:

        [HttpPost]
        public ActionResult EjecutarAccionTecnología(CatTecnologias Tecnologia, string Accion, FormCollection Formulario)
        {
             CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            LogicaTecnologias LogicaTecnologia = new LogicaTecnologias();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();
            string Validacion = "";

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                switch (Accion)
                {
                    case "Insertar":
                        try
                        {
                            if (LogicaTecnologia.L_ActualizarTecnologia(Tecnologia))
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["notice"] = "El proyecto no ha sido actualizado, por favor revise la información";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Insertar", new { IdTecnologia = Tecnologia.IdTecnologia });
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["notice"] = "Lo sentimos, ha ocurrido un error.";
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Insertar");
                        }

                    case "Actualizar":
                        try
                        {
                            if (LogicaTecnologia.L_ActualizarTecnologia(Tecnologia) )
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["notice"] = "El proyecto no ha sido actualizado, por favor revise la información";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Actualizar", new { IdTecnologia = Tecnologia.IdTecnologia });
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["notice"] = "Lo sentimos, ha ocurrido un error.";
                            Session["TipoAlerta"] = "Error";
                            //CargarListasDesplegables(Empresa.E_CodigoPostal, Empresa.E_IdEntidadFederativa, Empresa.E_IdMunicipio, Empresa.E_IdLocalidad);
                            return RedirectToAction("Actualizar", new { IdEmpresa = Tecnologia.IdTecnologia });
                        }


                    case "Eliminar":
                        try
                        {
                            if (LogicaTecnologia.L_EliminarTecnologia(Tecnologia.IdTecnologia))
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                TempData["notice"] = "Lo siento ha hocurrido un error."; 
                                return RedirectToAction("Index");
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["notice"] = "No se ha podido eliminar la empresa.";
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Eliminar", new { IdTecnologia = Tecnologia.IdTecnologia });
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

        [HttpPost]
        public ActionResult Accion_Insertar(IEnumerable<HttpPostedFileBase> files, string Accion, FormCollection Formulario)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            LogicaTecnologias LogicaTecnologia = new LogicaTecnologias();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();
            string Validacion = "";
            CatTecnologias Tecnologia = new CatTecnologias();
            Tecnologia.Tecnologia = Formulario[1].ToString();

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    int IdTecnologia = LogicaTecnologia.L_InsertarTecnologia(Tecnologia);
                    if (IdTecnologia > 0)
                    {
                        try 
                        {
                            foreach (var item in Formulario)
                            {
                                string[] Cadena = (item.ToString()).Split('/');
                                if(Cadena[0] == "Pregunta")
                                {
                                    LogicaTecnologia.L_InsertarTecnologiaPregunta(IdTecnologia, Cadena[1]);
                                }
                                if (Cadena[0] == "Tramite")
                                {
                                    string IdFase = Cadena[3].Substring(0, 1);
                                    LogicaTecnologia.L_InsertarTecnologiaTramite(IdTecnologia, Convert.ToInt32(IdFase), Convert.ToInt32(Cadena[1]), Convert.ToInt32(Cadena[3]));
                                }


                            }
                        }
                        catch(Exception ex)
                        {
                            TempData["notice"] = "Ha ocurrido un error durante la carga de datos";
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Insertar");
                        }

                        try
                        {
                            foreach (var item in Formulario)
                            {
                                string[] Cadena = (item.ToString()).Split('/');

                                if (Cadena[0] == "Pregunta")
                                {

                                    LogicaTecnologia.L_ActualizarTecnologiaPregunta(IdTecnologia, Cadena[1]);
                                }
                                if (Cadena[0] == "Tramite")
                                {
                                    string IdFase = Cadena[3].Substring(0, 1);
                                    LogicaTecnologia.L_ActualizarTecnologiaTramite(IdTecnologia, Convert.ToInt32(IdFase), Convert.ToInt32(Cadena[1]), Convert.ToInt32(Cadena[3]));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["notice"] = "No se pudieron agregar las imágenes, verifica la información y da click en el botón 'Actualizar' para realizar los cambios a la tecnología";
                            Session["TipoAlerta"] = "Precaución";
                            return RedirectToAction("Insertar");
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "El proyecto no ha sido actualizado, por favor revise la información";
                        Session["TipoAlerta"] = "Error";
                        return RedirectToAction("Insertar");
                    }
                }
                catch (Exception ex)
                {
                    TempData["notice"] = "Lo sentimos, ha ocurrido un error.";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("Insertar");
                }

            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }

        [HttpPost]
        public ActionResult Accion_Actualizar(IEnumerable<HttpPostedFileBase> files, string Accion, int IdTecnologia, string NombreTecnologia,  FormCollection Formulario)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            LogicaTecnologias LogicaTecnologia = new LogicaTecnologias();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();
            string Validacion = "";
            CatTecnologias Tecnologia = new CatTecnologias();
            Tecnologia.Tecnologia = NombreTecnologia;

            if (Usuario != null && Usuario.U_IdUsuario > 0 && Usuario.U_IdTipoUsuario == 4)
            {
                try
                {
                    //int IdTecnologia = Convert.ToInt32(Formulario[1]);
                    if (LogicaTecnologia.L_ActualizarTecnologia(Tecnologia))
                    {
                        LogicaTecnologia.L_PrepararActualizacionTecnologia(IdTecnologia);
                        try
                        {
                            foreach (var item in Formulario)
                            {
                                string[] Cadena = (item.ToString()).Split('/');
                                
                                if (Cadena[0] == "Pregunta")
                                {
                                    
                                    LogicaTecnologia.L_ActualizarTecnologiaPregunta(IdTecnologia,Cadena[1]);
                                }
                                if (Cadena[0] == "Tramite")
                                {
                                    string IdFase = Cadena[3].Substring(0, 1);
                                    LogicaTecnologia.L_ActualizarTecnologiaTramite(IdTecnologia, Convert.ToInt32(IdFase), Convert.ToInt32(Cadena[1]), Convert.ToInt32(Cadena[3]));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["notice"] = "Ha ocurrido un error durante la carga de datos";
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Insertar");
                        }

                        try
                        {
                            int n_archivo = 0;
                            foreach(string fileName in Request.Files)
                            {
                                HttpPostedFileBase file = Request.Files[n_archivo];
                                string ext = Path.GetExtension(file.FileName);
                                string PathRaiz = Server.MapPath("~");
                                string NombreArchivo = file.FileName.ToLower();
                                if(NombreArchivo.Contains("diagrama"))
                                {
                                    string path = PathRaiz + "Content\\Imagenes\\MapasDeProceso\\" + Tecnologia.Tecnologia + ext;
                                    file.SaveAs(path);
                                }
                                if (NombreArchivo.Contains("plantilla"))
                                {
                                    string path = PathRaiz + "Content\\Imagenes\\MapasDeProceso\\" + Tecnologia.Tecnologia + ext;
                                    file.SaveAs(path);
                                }
                                
                                n_archivo++;
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            TempData["notice"] = "No se pudieron agregar las imágenes, verifica la información y da click en el botón 'Actualizar' para realizar los cambios a la tecnología";
                            Session["TipoAlerta"] = "Precaución";
                            return RedirectToAction("Insertar");
                        }
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["notice"] = "El proyecto no ha sido actualizado, por favor revise la información";
                        Session["TipoAlerta"] = "Error";
                        return RedirectToAction("Insertar");
                    }
                }
                catch (Exception ex)
                {
                    TempData["notice"] = "Lo sentimos, ha ocurrido un error.";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("Insertar");
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