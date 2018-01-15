using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ENREL.Models.RespuestasWS
{
    public class Datos_ENREL_Dep_WS
    {
        DataTable DtAuxiliar = new DataTable();
        SqlCommand SQLComandoAuxiliar = new SqlCommand();

        public int D_InsertarNotificacionIOP(string fpet, string fresp, int codigo, string descripcion, string idglobalM, string XML)
        {
            int IdRespuesta = 0;
            SqlConnection Conexion = EstablecerConexionBD();
            SQLComandoAuxiliar = CrearLlamadaStoredProcedure("spInsertarNotificacionIOP", Conexion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@fPet", fpet);
            SQLComandoAuxiliar.Parameters.AddWithValue("@fResp", fresp);
            SQLComandoAuxiliar.Parameters.AddWithValue("@codigo", codigo);
            SQLComandoAuxiliar.Parameters.AddWithValue("@descripcion", descripcion);
            SQLComandoAuxiliar.Parameters.AddWithValue("@idglobal", idglobalM);
            SQLComandoAuxiliar.Parameters.AddWithValue("@XML", XML);
            SQLComandoAuxiliar.Parameters.Add("@msg", SqlDbType.Int).Direction = ParameterDirection.Output;
            SQLComandoAuxiliar.ExecuteNonQuery();
            IdRespuesta = (int)SQLComandoAuxiliar.Parameters["@msg"].Value;
            SQLComandoAuxiliar.Connection.Dispose();
            return IdRespuesta;
        }
        public SqlConnection EstablecerConexionBD()
        {

            string CadenaConexion = ConfigurationManager.ConnectionStrings["CadenaDeConexion"].ConnectionString;
            SqlConnection Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaConexion;
            Conexion.Open();
            return Conexion;
        }

        public SqlCommand CrearLlamadaStoredProcedure(string Procedimiento, SqlConnection Conexion)
        {
            SqlCommand comando = new SqlCommand(Procedimiento, Conexion);
            comando.CommandType = CommandType.StoredProcedure;
            return comando;
        }

        public void CerrarConexionBD(SqlConnection Conexion)
        {
            Conexion.Close();
            Conexion.Dispose();
        }

    }
}