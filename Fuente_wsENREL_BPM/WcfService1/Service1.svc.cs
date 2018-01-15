using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService1.ClasesAuxiliares;
using System.Web;
using System.Security.Cryptography;

namespace WcfService1
{
  
    public class Service1 : IService1
    {
        public string HabilitarTramite(string IdProyecto, string Homoclave, string Estatus)
        {
            DateTime FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            string registro = FechaActual.ToString() + " - WS_HabilitarTramite - ";

            string registrointento = registro + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "',Estatus='" + Estatus + "'";

            try
            {

            using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
            {
                RegistroDeErrores.WriteLine(registrointento);
            }

           
                int IdProyectoRecibido = Convert.ToInt32(IdProyecto);
                string EstatusRecibido = Estatus;

                if (Estatus == "habilitado")
                {
                    DataTable dt = new DataTable();
                    SqlConnection con = new SqlConnection();
                    string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

                    //Actualizar estatus
                    con.ConnectionString = conexion;
                    con.Open();
                    SqlCommand command = new SqlCommand("SpHabilitarTramite", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProyecto", IdProyectoRecibido);
                    command.Parameters.AddWithValue("@Homoclave", Homoclave);
                    command.ExecuteNonQuery();
                    con.Dispose();

                    //Confirmar actualización de estatus

                    con.ConnectionString = conexion;
                    con.Open();
                    command = new SqlCommand("SpSeleccionarEstatusTramite", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Homoclave", Homoclave);
                    command.Parameters.AddWithValue("@IdProyecto", IdProyectoRecibido);
                    command.Parameters.Add("@IdEstatus", SqlDbType.Int).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    int IdEstatusActualizado = (int)command.Parameters["@IdEstatus"].Value;
                    command.Dispose();
                    con.Dispose();

                    if (IdEstatusActualizado == 2)
                    {
                        return string.Format("ok");
                    }
                    else
                    {
                        return string.Format("error");
                    }

                }
                else
                {
                    return string.Format("error");
                }

            }
            catch (Exception ex)
            {
                registro = registro + ex.Message.ToString() + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "',Estatus='" + Estatus + "'";

                using (StreamWriter RegistroDeExc = new FileInfo(ConfigurationManager.AppSettings["RegistroExcepciones"].ToString()).AppendText())
                {
                    RegistroDeExc.WriteLine(registro);
                }
                return string.Format("error");
            }


        }

        public string ConfirmarBPM(string IdProyecto, string IdEstatus)
        {
            DateTime FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            string registro = FechaActual.ToString() + " - WS_ConfirmarBPM - ";

            string registrointento = registro + "Datos: IdProyecto='" + IdProyecto + "'";

            using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
            {
                RegistroDeErrores.WriteLine(registrointento);
            }

            try
            {
                int IdProyectoRecibido = Convert.ToInt32(IdProyecto);
                int IdEstatusProyecto = Convert.ToInt32(IdEstatus);

                if (IdEstatusProyecto == 2 || IdEstatusProyecto == 6)
                {
                    DataTable dt = new DataTable();
                    SqlConnection con = new SqlConnection();

                    string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                    con.ConnectionString = conexion;
                    con.Open();
                    SqlCommand command = new SqlCommand("SpActualizarProyectoSeguimiento", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProyecto", IdProyectoRecibido);
                    command.Parameters.AddWithValue("@IdEstatusProyecto", IdEstatusProyecto);
                    command.ExecuteNonQuery();

                    return string.Format("ok");
                }
                else
                {
                    return string.Format("Error");
                }


            }
            catch (Exception ex)
            {
                registro = registro + ex.Message.ToString() + "Datos: IdProyecto='" + IdProyecto + "'";

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registro);
                }
                return string.Format("error");
            }


        }

        public string ConfirmarRecepcionEstatus(string IdProyecto, string Homoclave, string Estatus)
        {
            try
            {
                DateTime FechaActual = new DateTime();
                FechaActual = DateTime.Now;
                string registro = FechaActual.ToString() + " - WS_ConfirmarEstatusBPM - ";

                string registrointento = registro + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "',Estatus='" + Estatus + "'";

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registrointento);
                }

                int IdProyectoRecibido = Convert.ToInt32(IdProyecto);
                int EstatusRecibido = 14;

                switch (Estatus.ToUpper())
                {
                    case "ENVIADO": EstatusRecibido = 3;
                        break;
                    case "RECIBIDO": EstatusRecibido = 4;
                        break;
                    case "INICIADO": EstatusRecibido = 5;
                        break;
                    case "EN PROCESO": EstatusRecibido = 6;
                        break;
                    case "DETENIDO": EstatusRecibido = 7;
                        break;
                    case "PREVENCION": EstatusRecibido = 8;
                        break;
                    case "AUTORIZADO": EstatusRecibido = 9;
                        break;
                    case "DENEGADO": EstatusRecibido = 16;
                        break;
                    case "RECHAZADO": EstatusRecibido = 11;
                        break;
                    case "CANCELADO": EstatusRecibido = 12;
                        break;
                    case "PRORROGA": EstatusRecibido = 13;
                        break;
                    default: EstatusRecibido = 14;
                        break;

                }

                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection();
                string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

                //Actualizar estatus
                con.ConnectionString = conexion;
                con.Open();
                SqlCommand command = new SqlCommand("SpConfirmarRecepcionEstatus", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProyecto", IdProyectoRecibido);
                command.Parameters.AddWithValue("@Homoclave", Homoclave);
                command.Parameters.AddWithValue("@Estatus", EstatusRecibido);
                command.ExecuteNonQuery();
                con.Dispose();

                FechaActual = DateTime.Now;
                registro = FechaActual.ToString() + " - WS_ConfirmarEstatusBPM - ";
                registro = registro + "Confirmación de estatus realizada en BD. " + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "',Estatus='" + Estatus + "',Respuesta a enviar='ok'";

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registro);
                }

                return string.Format("ok");

            }
            catch (Exception ex)
            {
                DateTime FechaActual = new DateTime();
                FechaActual = DateTime.Now;
                string registro = FechaActual.ToString() + " - WS_ConfirmarEstatusBPM - ";
                registro = registro + ex.Message.ToString() + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "',Estatus='" + Estatus + "'";

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registro);
                }
                return string.Format("error");
            }


        }

        public string EnviarAlerta(string IdProyecto, string Homoclave, string TipoAlerta)
        {
            DateTime FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            string registro = FechaActual.ToString() + " - WS_EnviarAlerta - ";

            string registrointento = registro + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "',TipoAlerta='" + TipoAlerta + "'";

            using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
            {
                RegistroDeErrores.WriteLine(registrointento);
            }

            string respuesta = "error";


            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection();

            try
            {
                int IdProyectoRecibido = Convert.ToInt32(IdProyecto);
                string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                con.ConnectionString = conexion;
                con.Open();

                SqlCommand command = new SqlCommand("SpSeleccionarDatosAlertas", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProyecto", IdProyectoRecibido);
                command.Parameters.AddWithValue("@Homoclave", Homoclave);
                command.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(dt);
                con.Dispose();

                MailMessage email = new MailMessage("enrel@sener.mx", ConfigurationManager.AppSettings["CorreoAdministrador"].ToString());
                email.To.Add(new MailAddress(ConfigurationManager.AppSettings["CorreoAdministrador2"].ToString()));
                email.Subject = "Asunto ( Alerta de trámite " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";

                string ContenidoMensaje = "";
                //Obtener la plantilla en HTML:
                string path = ConfigurationManager.AppSettings["Html_Alerta"].ToString();
                string ContenidoCorreo = System.IO.File.ReadAllText(path);

                int DiasTotales = (Int32)dt.Rows[0][7];
                ContenidoCorreo = ContenidoCorreo.Replace("#Dependencia#", dt.Rows[0][4].ToString());
                if (dt.Rows[0][3].ToString() != null)
                {
                    ContenidoCorreo = ContenidoCorreo.Replace("#HomoclaveAsignada#", dt.Rows[0][3].ToString());
                }
                else
                {
                    ContenidoCorreo = ContenidoCorreo.Replace("#HomoclaveAsignada#", dt.Rows[0][2].ToString());
                }
                ContenidoCorreo = ContenidoCorreo.Replace("#IdGlobalMacro#", dt.Rows[0][1].ToString());
                ContenidoCorreo = ContenidoCorreo.Replace("#Estatus#", dt.Rows[0][11].ToString());
                ContenidoCorreo = ContenidoCorreo.Replace("#Tecnologia#", dt.Rows[0][12].ToString());

                double Porcentaje = 0.0;
                int DiasTranscurridos = 0;
                switch (TipoAlerta)
                {
                    case "a":
                        //email.To.Add(new MailAddress(dt.Rows[0][5].ToString()));
                        Porcentaje = (Int32)dt.Rows[0][8];
                        DiasTranscurridos = (Int32)(DiasTotales * (Porcentaje / 100));
                        ContenidoCorreo = ContenidoCorreo.Replace("#DiasTranscurridos#", DiasTranscurridos.ToString());
                        ContenidoCorreo = ContenidoCorreo.Replace("#Porcentaje#", dt.Rows[0][9].ToString());
                        break;
                    case "b":
                        //email.To.Add(new MailAddress(dt.Rows[0][5].ToString()));
                        Porcentaje = (Int32)dt.Rows[0][9];
                        DiasTranscurridos = (Int32)(DiasTotales * (Porcentaje / 100));
                        ContenidoCorreo = ContenidoCorreo.Replace("#DiasTranscurridos#", DiasTranscurridos.ToString());
                        ContenidoCorreo = ContenidoCorreo.Replace("#Porcentaje#", dt.Rows[0][9].ToString());
                        break;
                    case "c":
                        //email.To.Add(new MailAddress(dt.Rows[0][5].ToString()));
                        //email.To.Add(new MailAddress(dt.Rows[0][6].ToString()));
                        Porcentaje = (Int32)dt.Rows[0][10];
                        DiasTranscurridos = (Int32)(DiasTotales * (Porcentaje / 100));
                        ContenidoCorreo = ContenidoCorreo.Replace("#DiasTranscurridos#", DiasTranscurridos.ToString());
                        ContenidoCorreo = ContenidoCorreo.Replace("#Porcentaje#", dt.Rows[0][9].ToString());
                        break;
                    case "d":
                        ContenidoCorreo = ContenidoCorreo.Replace("#Dependencia#", "Administrador");
                        ContenidoCorreo = ContenidoCorreo.Replace("#DiasTranscurridos#", "0");
                        ContenidoCorreo = ContenidoCorreo.Replace("#Porcentaje#", "0");
                        break;


                    default: break;

                }


                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(ContenidoCorreo, null, "text/html");


                //Obtener imágenes:


                LinkedResource Logotipo_SENER = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_SENER"].ToString());
                Logotipo_SENER.ContentId = "Logotipo_SENER";
                htmlView.LinkedResources.Add(Logotipo_SENER);

                LinkedResource Logotipo_MEXICO = new LinkedResource(ConfigurationManager.AppSettings["Logotipo_MEXICO"].ToString());
                Logotipo_MEXICO.ContentId = "Logotipo_MEXICO";
                htmlView.LinkedResources.Add(Logotipo_MEXICO);


                email.AlternateViews.Add(htmlView);
                email.IsBodyHtml = true;
                email.Priority = MailPriority.High;


                SmtpClient smtp = new SmtpClient();
                smtp.Host = "172.16.70.110";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Send(email);
                email.Dispose();

                respuesta = "ok";

                return string.Format(respuesta);
            }
            catch (Exception ex)
            {
                registro = registro + ex.Message.ToString() + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "',TipoAlerta='" + TipoAlerta + "'";

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registro);
                }
                return string.Format(respuesta);
            }


        }

        public string Opcional(string IdProyecto, string IdPregunta)
        {
            DateTime FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            string registro = FechaActual.ToString() + " - WS_Opcional - ";
            string respuesta = "error";

            string registrointento = registro + "Datos: IdProyecto='" + IdProyecto + "',IdPregunta='" + IdPregunta + "'";

            using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
            {
                RegistroDeErrores.WriteLine(registrointento);
            }

            try
            {
                int IdProyectoRecibido = Convert.ToInt32(IdProyecto);
                string Homoclave = IdPregunta;
                bool Estatus = false;

                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection();

                string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                con.ConnectionString = conexion;
                con.Open();
                SqlCommand command = new SqlCommand("SpSeleccionarEstatusRequerido", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProyecto", IdProyectoRecibido);
                command.Parameters.AddWithValue("@Homoclave", Homoclave);
                command.Parameters.Add("@Estatus", SqlDbType.Bit).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                Estatus = (bool)command.Parameters["@Estatus"].Value;
                con.Dispose();

                if (Estatus)
                {
                    respuesta = "si";
                }
                else
                {
                    respuesta = "no";
                }


                return string.Format(respuesta);
            }
            catch (Exception ex)
            {
                registro = registro + ex.Message.ToString() + "Datos: IdProyecto='" + IdProyecto + "','" + IdPregunta + "'";

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registro);
                }
                return string.Format(respuesta);
            }


        }

        public string DatosEmpresaPorProyecto(string IdProyecto)
        {
            DateTime FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            string registro = FechaActual.ToString() + " - WS_DatosEmpresaPorProyecto - ";

            string registrointento = registro + "Datos: IdProyecto='" + IdProyecto + "'";

            using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
            {
                RegistroDeErrores.WriteLine(registrointento);
            }

            int IdProyectoRecibido = Convert.ToInt32(IdProyecto);
            DataTable dtDatosEmpresa = new DataTable();
            string resultado = "";
            SqlConnection con = new SqlConnection();

            try
            {
                string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                con.ConnectionString = conexion;
                con.Open();
                SqlCommand command = new SqlCommand("SpSeleccionarDatosEmpresaPorProyecto", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProyecto", IdProyectoRecibido);
                command.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(dtDatosEmpresa);

                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dtDatosEmpresa.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dtDatosEmpresa.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                command.Connection.Dispose();
                return serializer.Serialize(rows);

            }
            catch (Exception ex)
            {
                registro = registro + ex.Message.ToString() + "Datos: IdProyecto='" + IdProyecto + "'";

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registro);
                }
                return string.Format("Error");
            }
        }

        public int ObtenerAlertas(string IdProyecto, string Homoclave, string Variable)
        {
            DateTime FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            string registro = FechaActual.ToString() + " - WS_ObtenerAlertas - ";

            string registrointento = registro + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "',TipoAlerta='" + Variable + "'";

            using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
            {
                RegistroDeErrores.WriteLine(registrointento);
            }

            try
            {

                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection();
                int IdProyectoRecibido = Convert.ToInt32(IdProyecto);

                string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                con.ConnectionString = conexion;
                con.Open();
                SqlCommand command = new SqlCommand("SpSeleccionarAlertasPorTramite", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Homoclave", Homoclave);
                command.Parameters.AddWithValue("@IdProyecto", IdProyectoRecibido);
                command.ExecuteNonQuery();
                SqlDataAdapter dr = new SqlDataAdapter(command);
                dr.Fill(dt);
                con.Dispose();
                int respuesta = -1;

                if (dt.Rows.Count > 0)
                {
                    switch (Variable)
                    {
                        case "DiasTotales":
                            try { respuesta = (int)dt.Rows[0][0]; }
                            catch { }
                            return respuesta;
                        case "DiasTranscurridos":
                            try { respuesta = (int)dt.Rows[0][1]; }
                            catch { }
                            return respuesta;
                        case "Porcentaje1":
                            try { respuesta = (int)dt.Rows[0][2]; }
                            catch { }
                            return respuesta;
                        case "Porcentaje2":
                            try { respuesta = (int)dt.Rows[0][3]; }
                            catch { }
                            return respuesta;
                        case "Porcentaje3":
                            try { respuesta = (int)dt.Rows[0][4]; }
                            catch { }
                            return respuesta;
                        default: return -1;
                    }
                }
                else
                {
                    return -1;
                }


            }
            catch (Exception ex)
            {
                registro = registro + ex.Message.ToString() + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "',tipoAlerta='" + Variable + "'";

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registro);
                }
                return -1;
            }


        }

        public int ObtenerEstatus(string IdProyecto, string Homoclave)
        {
            DateTime FechaActual = new DateTime();
            FechaActual = DateTime.Now;
            string registro = FechaActual.ToString() + " - WS_ObtenerEstatus - ";
            int Estatus = 0;

            string registrointento = registro + "Datos: IdProyecto='" + IdProyecto + "',Homoclave='" + Homoclave + "'";

            using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
            {
                RegistroDeErrores.WriteLine(registrointento);
            }

            try
            {
                int IdProyectoRecibido = Convert.ToInt32(IdProyecto);              

                SqlConnection con = new SqlConnection();

                string conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                con.ConnectionString = conexion;
                con.Open();
                SqlCommand command = new SqlCommand("SpSeleccionarEstatusTramite", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProyecto", IdProyectoRecibido);
                command.Parameters.AddWithValue("@Homoclave", Homoclave);
                command.Parameters.Add("@IdEstatus", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                Estatus = (Int32)command.Parameters["@IdEstatus"].Value;
                con.Dispose();

                return Estatus;
            }
            catch (Exception ex)
            {
                registro = registro + ex.Message.ToString() + "Datos: IdProyecto='" + IdProyecto + "', Homoclave'" + Homoclave + "'";

                using (StreamWriter RegistroDeErrores = new FileInfo(ConfigurationManager.AppSettings["RegistroErrores"].ToString()).AppendText())
                {
                    RegistroDeErrores.WriteLine(registro);
                }
                Estatus = -1;
                return Estatus;
            }


        }

    }
}
