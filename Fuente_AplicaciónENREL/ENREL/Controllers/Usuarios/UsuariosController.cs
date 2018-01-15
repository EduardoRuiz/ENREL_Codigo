using ENREL.Controllers.Auxiliar;
using ENREL.Models.Empresas;
using ENREL.Models.RepresentantesLegales;
using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ENREL.Controllers.Usuarios
{
    public class UsuariosController : Controller
    {
        #region Variables:

        MetodosGenerales MetodoGeneral = new MetodosGenerales();
        LogicaUsuarios LogicaUsuario = new LogicaUsuarios();

        #endregion

        #region Páginas

        public ActionResult Index()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                List<CatUsuarios> ListaUsuarios = new List<CatUsuarios>();
                ListaUsuarios = LogicaUsuario.L_SeleccionarUsuariosPorEmpresa(Usuario.U_IdEmpresa, true);
                return View(ListaUsuarios);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: Index");
                return View("Index", "Home");
            }
        }

        public ActionResult Insertar()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                ViewBag.RFCRepresentante = Usuario.U_RFCRepresentanteAsociado;
                return View();
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: Insertar");
                return RedirectToAction("Index");
            }

        }

        public ActionResult Actualizar()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario <= 0 || Usuario.U_IdTipoUsuario > 4)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                Usuario.U_Password = "";
                return View(Usuario);
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: Actualizar");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Detalles()
        {
            try
            {
                int IdUsuario = Convert.ToInt32(Request.Form["IdUsuario"]);
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                if(IdUsuario > 0)
                {
                    CatUsuarios UsuarioPorEditar = LogicaUsuario.L_DetallesUsuario(IdUsuario);
                    
                    return View(UsuarioPorEditar);
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: Detalles");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Eliminar()
        {
            try
            {
                int IdUsuario = Convert.ToInt32(Request.Form["IdUsuario"]);
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                if (IdUsuario > 0)
                {
                    CatUsuarios UsuarioPorEditar = LogicaUsuario.L_DetallesUsuario(IdUsuario);
                    ViewBag.RFCRepresentante = Usuario.U_RFCRepresentanteAsociado;
                    ViewBag.NombreUsuarioEliminar = UsuarioPorEditar.U_Nombre;
                    return View(UsuarioPorEditar);
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: Detalles");
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public ActionResult CambioDeContraseñaPorExtravio(string ClaveReset)
        {
            try
            {
                CatUsuarios UsuarioPorEditar = LogicaUsuario.L_DetallesUsuarioPorClaveReset(ClaveReset);
                if (UsuarioPorEditar.U_IdUsuario > 0)
                {
                    ViewBag.NombreUsuario = UsuarioPorEditar.U_Nombre;
                    ViewBag.ClaveReset = ClaveReset;
                    return View(UsuarioPorEditar);
                }
                else
                {
                    TempData["notice"] = "Ha expirado el tiempo del enlace, solicita el cambio de contraseña.";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("SolicitudCambioDeContraseña", "Home");
                }
                
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "CambioDeContraseñaPorExtravio");
                return RedirectToAction("Index","Home");
            }

            

        }

        public ActionResult InsertarUsuarioSENER()
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario == null || Usuario.U_IdTipoUsuario != 4)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                CargarListasDesplegables(null, null);
                return View();
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: InsertarUsuarioSENER");
                return RedirectToAction("Index","Administrador");
            }
            

        }

        #endregion

        #region Métodos Auxiliares:

        [HttpPost]
        public ActionResult EjecutarAccionUsuario(CatUsuarios UsuarioPorEditar, string Accion, IEnumerable<HttpPostedFileBase> files)
        {
            string RFCAsociado = Session["RFCAsociado"].ToString();
            string Validacion = "";
            MetodosGenerales MetodoGeneral = new MetodosGenerales();


            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];

                if (Usuario == null || Usuario.U_IdTipoUsuario != 2)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }

                switch (Accion)
                {
                    case "Insertar":
                        Validacion = MetodoGeneral.ValidarFIEL(files, UsuarioPorEditar.U_ClavePrivada, RFCAsociado);
                        if (Validacion == "Validación exitosa")
                        {
                            LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
                            CatUsuarios UsuarioExistente = LogicaUsuario.L_DetallesUsuarioPorNombreUnicamente(UsuarioPorEditar.U_Nombre);

                            if (UsuarioExistente.U_IdUsuario != null && UsuarioExistente.U_IdUsuario > 0)
                            {
                                TempData["notice"] = "Ya existe una cuenta de usuario con el mismo nombre, por favor elige otro.";
                                Session["TipoAlerta"] = "Error";
                                return View("Insertar", UsuarioPorEditar);
                            }
                            else
                            {
                                if (UsuarioPorEditar.U_Password != UsuarioPorEditar.U_ConfirmarPassword)
                                {
                                    TempData["notice"] = "Las contraseñas no coinciden.";
                                    Session["TipoAlerta"] = "Error";
                                    return View("Insertar", UsuarioPorEditar);
                                }
                                else
                                {
                                    int IdRepresentanteLegal = Usuario.U_IdRepresentanteAsociado;
                                    UsuarioPorEditar.U_Activo = true;
                                    UsuarioPorEditar.U_IdTipoUsuario = 1;
                                    if (LogicaUsuario.L_InsertarUsuario(UsuarioPorEditar, Usuario.U_IdEmpresa, IdRepresentanteLegal))
                                    {
                                        TempData["notice"] = "El usuario operativo ha sido creado.";
                                        Session["TipoAlerta"] = "Correcto";
                                        return RedirectToAction("Index");
                                    }
                                    else
                                    {
                                        TempData["notice"] = "No se pudo crear el Representante legal, por favor revise la información";
                                        Session["TipoAlerta"] = "Error";
                                        return RedirectToAction("Insertar", UsuarioPorEditar);
                                    }
                                }

                            }

                        }
                        else
                        {
                            TempData["notice"] = Validacion;
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Insertar", UsuarioPorEditar);
                        }


                    case "Eliminar":
                        Validacion = MetodoGeneral.ValidarFIEL(files, UsuarioPorEditar.U_ClavePrivada, RFCAsociado);
                        if (Validacion == "Validación exitosa")
                        {
                            if (LogicaUsuario.L_EliminarUsuario(UsuarioPorEditar.U_IdUsuario))
                            {
                                if (UsuarioPorEditar.U_IdUsuario == Usuario.U_IdUsuario)
                                {
                                    return RedirectToAction("Index","Home");
                                }
                                else
                                {
                                    TempData["notice"] = "Se ha eliminado el usuario.";
                                    Session["TipoAlerta"] = "Correcto";
                                    return RedirectToAction("Index", "Usuarios");
                                }

                            }
                            else
                            {
                                TempData["notice"] = "Operación no concluida, favor de comunicarse con un administrador de SENER.";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Eliminar", new { IdUsuario = UsuarioPorEditar.U_IdUsuario });
                            }
                        }
                        else
                        {
                            TempData["notice"] = Validacion;
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Eliminar", new { IdUsuario = UsuarioPorEditar.U_IdUsuario });
                        }



                    default:
                        return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: EjecutarAccionUsuario(" + Accion + ")");
                return RedirectToAction("Index");
            }

        }

        //[HttpPost]
        public ActionResult EjecutarAccionUsuarioSENER(CatUsuarios UsuarioPorEditar, string Accion, int IdUsuarioPorEditar, bool IdEstatusUsuario)
        {
            CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
            CatEmpresas Empresa = new CatEmpresas();
            MetodosGenerales MetodoGeneral = new MetodosGenerales();

            try
            {
                if (Usuario == null || Usuario.U_IdTipoUsuario != 4)
                {
                    TempData["notice"] = "La sesión ha expirado.";
                    return RedirectToAction("Logout", "Home");
                }
                LogicaEmpresas LogicaEmpresa = new LogicaEmpresas();
                LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
                Empresa = LogicaEmpresa.L_DetallesEmpresa(Usuario.U_IdEmpresa);

                switch (Accion)
                {
                    case "Insertar":

                        CatUsuarios UsuarioExistente = LogicaUsuario.L_DetallesUsuarioPorNombreUnicamente(UsuarioPorEditar.U_Nombre);

                        if (UsuarioExistente.U_IdUsuario != null && UsuarioExistente.U_IdUsuario > 0)
                        {
                            TempData["notice"] = "Ya existe una usuario con el mismo nombre, por favor elige otro.";
                            Session["TipoAlerta"] = "Error";
                            CargarListasDesplegables(null, null);
                            return View("InsertarUsuarioSENER", UsuarioPorEditar);
                        }
                        else
                        {
                            if (UsuarioPorEditar.U_Password != UsuarioPorEditar.U_ConfirmarPassword)
                            {
                                TempData["notice"] = "La contraseñas no coinciden.";
                                Session["TipoAlerta"] = "Error";
                                CargarListasDesplegables(null, null);
                                return View("InsertarUsuarioSENER", UsuarioPorEditar);
                            }
                            else
                            {
                                UsuarioPorEditar.U_Activo = true;
                                if (LogicaUsuario.L_InsertarUsuarioSENER(UsuarioPorEditar, Usuario.U_IdEmpresa))
                                {
                                    TempData["notice"] = "El usuario ha sido creado.";
                                    Session["TipoAlerta"] = "Correcto";
                                    return RedirectToAction("GestionUsuariosSENER", "Administrador");
                                }
                                else
                                {
                                    TempData["notice"] = "Error de sistema.";
                                    Session["TipoAlerta"] = "Error";
                                    CargarListasDesplegables(null, null);
                                    return View("InsertarUsuarioSENER", UsuarioPorEditar);
                                }
                            }

                        }

                    case "CambiarEstatus":
                        UsuarioPorEditar.U_IdUsuario = IdUsuarioPorEditar;
                        UsuarioPorEditar.U_Activo = !IdEstatusUsuario;

                        if (LogicaUsuario.L_ActualizarEstatusUsuarioSENER(UsuarioPorEditar.U_IdUsuario, UsuarioPorEditar.U_Activo))
                        {
                            if (UsuarioPorEditar.U_IdUsuario == Usuario.U_IdUsuario)
                            {
                                return RedirectToAction("GestionUsuariosSENER", "Administrador");
                            }
                            else
                            {
                                TempData["notice"] = "Se realizó el cambio de estatus.";
                                Session["TipoAlerta"] = "Correcto";
                                return RedirectToAction("GestionUsuariosSENER", "Administrador");
                            }

                        }
                        else
                        {
                            TempData["notice"] = "Operación no concluida.";
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Eliminar", new { IdUsuario = UsuarioPorEditar.U_IdUsuario });
                        }



                    default:
                        return RedirectToAction("GestionUsuariosSENER", "Administrador");
                }


            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: EjecutarAccionUsuario(" + Accion + ")");
                return RedirectToAction("Actualizar", "Administrador");
            }

            
        }

        [HttpPost]
        public ActionResult ActualizarNombreUsuario(CatUsuarios UsuarioPorEditar, string Caracteristica)
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario != null && Usuario.U_IdUsuario > 0)
                {
                    if (LogicaUsuario.L_ValidarUsuario(Usuario.U_Nombre, UsuarioPorEditar.U_PasswordActualParaCambiarNombre))
                    {
                        string NombreAnterior = Usuario.U_Nombre;
                        CatUsuarios UsuarioExistente = LogicaUsuario.L_DetallesUsuarioPorNombreUnicamente(UsuarioPorEditar.U_Nombre);
                        if (UsuarioExistente.U_IdUsuario <= 0)
                        {
                            UsuarioPorEditar.U_Password = UsuarioPorEditar.U_PasswordActualParaCambiarNombre;
                            if (LogicaUsuario.L_ActualizarUsuario(Usuario, UsuarioPorEditar, Caracteristica))
                            {
                                //LogicaUsuario.L_ActualizarNombreUsuarioSP(NombreAnterior, UsuarioPorEditar.U_Nombre);
                                //MembershipUser UsuarioSharePoint = Membership(UsuarioPorEditar.U_Nombre);
                                //if (UsuarioSharePoint != null)
                                //{
                                //    UsuarioSharePoint.UserName = UsuarioPorEditar.U_AntiguoNombre;
                                //    Membership.UpdateUser(UsuarioSharePoint);
                                //}
                                TempData["notice"] = "Tu sesión expiró debido al cambio de nombre de usuario.";
                                //bool cambio = LogicaUsuario.L_ValidarUsuario(Usuario.U_Nombre, UsuarioPorEditar.U_Password);
                                Session["TipoAlerta"] = "Correcto";
                                return RedirectToAction("Logout", "Home");
                            }
                            else
                            {
                                TempData["notice"] = "Lo siento, ha ocurrido un error al tratar de actualizar el nombre de usuario.";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Actualizar");
                            }
                        }
                        else
                        {
                            TempData["notice"] = "El nombre de usuario ya existe";
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Actualizar");
                        }

                    }
                    else
                    {
                        TempData["notice"] = "No se pudo confirmar la contraseña.";
                        Session["TipoAlerta"] = "Error";
                        return RedirectToAction("Actualizar");
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: ActualizarNombreDeUsuario");
                return RedirectToAction("Actualizar");
            }
        }

        [HttpPost]
        public ActionResult ActualizarCorreo(CatUsuarios UsuarioPorEditar, string Caracteristica)
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario != null && Usuario.U_IdUsuario > 0)
                {
                    if (LogicaUsuario.L_ValidarUsuario(Usuario.U_Nombre, UsuarioPorEditar.U_PasswordActualParaCambiarCorreo))
                    {
                        UsuarioPorEditar.U_Password = UsuarioPorEditar.U_PasswordActualParaCambiarCorreo;
                        if (LogicaUsuario.L_ActualizarUsuario(Usuario, UsuarioPorEditar, Caracteristica))
                        {
                            //MembershipUser UsuarioSharePoint = Membership.GetUser(Usuario.U_Nombre);
                            //if (UsuarioSharePoint != null)
                            //{
                            //    UsuarioSharePoint.Email = UsuarioPorEditar.U_CorreoElectronico;
                            //    Membership.UpdateUser(UsuarioSharePoint);
                            //}

                            TempData["notice"] = "El correo electróninco ha sido modificado. Toda comunicación será realizada a través de este nuevo correo.";
                                                       
                            Session["TipoAlerta"] = "Correcto";
                            bool cambio = LogicaUsuario.L_ValidarUsuario(Usuario.U_Nombre, UsuarioPorEditar.U_Password);
                            return RedirectToAction("Actualizar");
                        }
                        else
                        {
                            TempData["notice"] = "Lo siento, ha ocurrido un error al tratar de actualizar el correo electrónico.";
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Actualizar");
                        }
                    }
                    else
                    {
                        TempData["notice"] = "La contraseña actual es incorrecta.";
                        Session["TipoAlerta"] = "Error";
                        return RedirectToAction("Actualizar");
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: ActualizarCorreo");
                return RedirectToAction("Actualizar");
            }
        }

        [HttpPost]
        public ActionResult ActualizarContraseña(CatUsuarios UsuarioPorEditar, string Caracteristica)
        {
            try
            {
                CatUsuarios Usuario = (CatUsuarios)Session["Usuario"];
                if (Usuario != null && Usuario.U_IdUsuario > 0)
                {
                    if (UsuarioPorEditar.U_NuevoPassword == UsuarioPorEditar.U_ConfirmarPassword)
                    {
                        if (LogicaUsuario.L_ValidarUsuario(Usuario.U_Nombre, UsuarioPorEditar.U_PasswordActualParaCambiarPassword))
                        {
                            //UsuarioPorEditar.U_Password = UsuarioPorEditar.U_PasswordActualParaCambiarPassword;
                            if (LogicaUsuario.L_ActualizarUsuario(Usuario, UsuarioPorEditar, Caracteristica))
                            {
                                
                                TempData["notice"] = "Tu sesión expiró debido al cambio de contraseña.";
                                //bool cambio = LogicaUsuario.L_ValidarUsuario(Usuario.U_Nombre, UsuarioPorEditar.U_Password);
                                Session["TipoAlerta"] = "Correcto";
                                return RedirectToAction("Logout", "Home");
                            }
                            else
                            {
                                TempData["notice"] = "Lo siento, ha ocurrido un error al tratar de actualizar la contraseña.";
                                Session["TipoAlerta"] = "Error";
                                return RedirectToAction("Actualizar");
                            }
                        }
                        else
                        {
                            TempData["notice"] = "No se pudo confirmar la contraseña.";
                            Session["TipoAlerta"] = "Error";
                            return RedirectToAction("Actualizar");
                        }
                    }
                    else
                    {
                        TempData["notice"] = "La nueva contraseña no coincide con la confirmación.";
                        Session["TipoAlerta"] = "Error";
                        return RedirectToAction("Actualizar");
                    }
                }
                else
                {
                    return RedirectToAction("Logout", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: ActualizarContraseña");
                return RedirectToAction("Actualizar");
            }
        }

        [HttpPost]
        public ActionResult ActualizarContraseñaExtravio(CatUsuarios UsuarioPorEditar, string Caracteristica, string ClaveReset)
        {
            try
            {
                if (UsuarioPorEditar.U_NuevoPassword == UsuarioPorEditar.U_ConfirmarPassword)
                {
                    //UsuarioPorEditar.U_Password = UsuarioPorEditar.U_PasswordActualParaCambiarPassword;
                    if (LogicaUsuario.L_ActualizarUsuario(UsuarioPorEditar, UsuarioPorEditar, Caracteristica))
                    {
                        //Membership.DeleteUser(UsuarioPorEditar.U_Nombre);

                        TempData["notice"] = "La contraseña ha sido cambiada exitosamente.";
                        //bool cambio = LogicaUsuario.L_ValidarUsuario(Usuario.U_Nombre, UsuarioPorEditar.U_Password);
                        Session["TipoAlerta"] = "Correcto";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["notice"] = "Lo siento, ha ocurrido un error al tratar de actualizar la contraseña.";
                        Session["TipoAlerta"] = "Error";
                        return RedirectToAction("CambioDeContraseñaPorExtravio", new { ClaveReset = ClaveReset });
                    }
                }
                else
                {
                    TempData["notice"] = "La nueva contraseña no coincide con la confirmación.";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("CambioDeContraseñaPorExtravio", new { ClaveReset = ClaveReset });
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: ActualizarContraseña");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult ActualizarContraseñaPorExtravio(CatUsuarios UsuarioPorEditar, string Caracteristica)
        {
            try
            {
                if (UsuarioPorEditar.U_Password == UsuarioPorEditar.U_ConfirmarPassword)
                {
                    if (LogicaUsuario.L_ActualizarUsuario(UsuarioPorEditar, UsuarioPorEditar, Caracteristica))
                    {
                        return RedirectToAction("Logout", "Home");
                    }
                    else
                    {
                        TempData["notice"] = "Lo siento, ha ocurrido un error al tratar de actualizar la contraseña.";
                        Session["TipoAlerta"] = "Error";
                        return RedirectToAction("CambioDeContraseñaPorExtravio");
                    }
                }
                else
                {
                    TempData["notice"] = "La nueva contraseña no coincide con la confirmación";
                    Session["TipoAlerta"] = "Error";
                    return RedirectToAction("Actualizar");
                }
            }
            catch (Exception ex)
            {
                TempData["notice"] = ConfigurationManager.AppSettings["MensajeError"].ToString();
                Session["TipoAlerta"] = "Error";
                MetodoGeneral.RegistroDeError(ex.Message, "Usuarios: ActualizarContraseñaPorExtravio");
                return RedirectToAction("Index", "Home");
            }
        }

        private void CargarListasDesplegables(int? IdTipoUsuario, int? IdEstatusUsuario)
        {
            LogicaUsuarios LogicaUsuario = new LogicaUsuarios();

            List<CatTiposUsuario> ListaTiposUsuario = LogicaUsuario.SeleccionarTiposUsuario();
            ViewBag.TiposUsuarios = new SelectList(ListaTiposUsuario, "IdTipoUsuario", "TipoUsuario", IdTipoUsuario);

            List<CatEstatusUsuario> ListaEstatusUsuario = LogicaUsuario.SeleccionarTiposEstatusUsuario();
            ViewBag.TiposEstatusUsuarios = new SelectList(ListaEstatusUsuario, "IdEstatusUsuario", "EstatusUsuario", IdEstatusUsuario);

        }

        #endregion
    }
}