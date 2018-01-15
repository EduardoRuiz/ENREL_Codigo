using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ENREL.Controllers.Auxiliar
{
    public class Ubicacion
    {
        public int IdEntidadFederativa { get; set; }
        public string EntidadFederativa { get; set; }
        public int IdMunicipio { get; set; }
        public string Municipio { get; set; }
        public int IdLocalidad { get; set; }
        public string Localidad { get; set; }

        public Ubicacion L_SeleccionarUbicacion(int? IdCodigoPostal)
        {
            DataTable DtAuxiliar = new DataTable();
            Ubicacion UbicacionPorCP = new Ubicacion();
            DtAuxiliar = D_SeleccionarUbicacion(IdEntidadFederativa);

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    UbicacionPorCP.IdEntidadFederativa = (Int32)dr["IdEntidadFederativa"];
                    UbicacionPorCP.EntidadFederativa = dr["EntidadFederativa"].ToString();
                    UbicacionPorCP.IdMunicipio = (Int32)dr["IdMunicipio"];
                    UbicacionPorCP.Municipio = dr["Municipio"].ToString();
                    UbicacionPorCP.IdLocalidad = (Int32)dr["IdLocalidad"];
                    UbicacionPorCP.Localidad = dr["Localidad"].ToString();
                }
            }

            return UbicacionPorCP;
        }

        public DataTable D_SeleccionarUbicacion(int? IdCodigoPostal)
        {
            MetodosGenerales MetodoGeneral = new MetodosGenerales();
            DataTable DtAuxiliar = new DataTable();
            SqlCommand SQLComandoAuxiliar = new SqlCommand();

            DtAuxiliar.Rows.Clear();
            DtAuxiliar.Columns.Clear();
            SqlConnection Conexion = MetodoGeneral.EstablecerConexionBD();
            SQLComandoAuxiliar = MetodoGeneral.CrearLlamadaStoredProcedure("SpSeleccionarEntidadesFederativas", Conexion);

            if (IdEntidadFederativa != null) { SQLComandoAuxiliar.Parameters.AddWithValue("@IdEntidadFederativa", IdEntidadFederativa); }


            SQLComandoAuxiliar.ExecuteNonQuery();
            SqlDataAdapter dr = new SqlDataAdapter(SQLComandoAuxiliar);
            dr.Fill(DtAuxiliar);
            SQLComandoAuxiliar.Connection.Dispose();
            return DtAuxiliar;
        }
    }
}