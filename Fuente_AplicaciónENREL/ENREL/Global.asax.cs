using ENREL.Controllers.Auxiliar;
using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ENREL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
        }



        //Salir de la aplicación
        protected void Session_End(Object sender, EventArgs e)
        {
            MetodosGenerales MetodoGeneral = new MetodosGenerales();
            LogicaUsuarios LogicaUsuario = new LogicaUsuarios();
            CatUsuarios UsuarioEntrante = (CatUsuarios)Session["Usuario"];

            if (UsuarioEntrante != null)
            {
                LogicaUsuario.L_ActualizarUsuarioEntrada(UsuarioEntrante.U_IdUsuario, false);
            }

            Session.Clear();
            //Application_Start();
        }


        //Controlar errores
        protected void Application_Error(object sender, EventArgs e)
        {
            //Recuperar el ultimo error del servidor
            Exception exception = Server.GetLastError();
            //limpiar la salida del contenido de la respuesta html que es mostrada si no se controla el error
            Response.Clear();
            HttpException httpException = exception as HttpException;
            MetodosGenerales MetodoGeneral = new MetodosGenerales();

            if (httpException == null)
            {
                MetodoGeneral.RegistroDeError("Error no mostrado.", exception.ToString());
                Response.Redirect("/Error/Index");
            }
            else //Si se genera una Exception Http, debe ser controlada
            {
                switch (httpException.GetHttpCode())
                {
                    //Segun el numero de error, redirigir a una accion definida en el controlador ErrorController
                    case 403:
                        MetodoGeneral.RegistroDeError(httpException.Message, "Errores generales");
                        Response.Redirect("/Error/HttpError403");
                        break;
                    case 404:
                        MetodoGeneral.RegistroDeError(httpException.Message, "Errores generales");
                        Response.Redirect("/Error/HttpError404");
                        break;
                    case 500:
                        MetodoGeneral.RegistroDeError(httpException.Message, "Errores generales");
                        Response.Redirect("/Error/HttpError500");
                        break;
                    case 502:
                        MetodoGeneral.RegistroDeError(httpException.Message, "Errores generales");
                        Response.Redirect("/Error/HttpError502");
                        break;
                    case 503:
                        MetodoGeneral.RegistroDeError(httpException.Message, "Errores generales");
                        Response.Redirect("/Error/HttpError503");
                        break;
                    default:
                        MetodoGeneral.RegistroDeError(httpException.Message, "Errores generales");
                        Response.Redirect("/Error/Index");
                        break;
                }
            }
        }
    }
}
