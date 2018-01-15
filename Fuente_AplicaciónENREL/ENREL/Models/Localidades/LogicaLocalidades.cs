using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ENREL.Models.Localidades
{
    public class LogicaLocalidades
    {
        #region Variables

        List<CatLocalidades> ListaLocalidades = new List<CatLocalidades>();
        DatosLocalidades DatosAuxiliar = new DatosLocalidades();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos Principales

        public List<CatLocalidades> L_SeleccionarLocalidades(int? IdCodigoPostal, int? IdEntidadFederativa, int? IdMunicipio, int? IdLocalidad)
        {
            if (IdCodigoPostal == null)
            {
                DtAuxiliar = DatosAuxiliar.D_SeleccionarLocalidades(IdEntidadFederativa, IdMunicipio, IdLocalidad);
            }
            else
            {
                DtAuxiliar = DatosAuxiliar.D_SeleccionarLocalidadesPorINEGI(IdEntidadFederativa, IdMunicipio, IdLocalidad);
            }

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatLocalidades Localidad = new CatLocalidades();
                    Localidad.IdLocalidad = (Int32)dr["IdLocalidad"];
                    Localidad.Localidad = dr["Localidad"].ToString();
                    ListaLocalidades.Add(Localidad);
                }
            }

            return ListaLocalidades;
        }

        public List<CatLocalidades> L_SeleccionarLocalidadPorCodigoPostal(int IdEntidadFederativa, int IdMunicipioINEGI, int IdLocalidadINEGI)
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarLocalidades(IdEntidadFederativa, IdMunicipioINEGI, IdLocalidadINEGI);
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatLocalidades Localidad = new CatLocalidades();
                    Localidad.IdLocalidad = (Int32)dr["IdLocalidad"];
                    Localidad.Localidad = dr["Localidad"].ToString();
                    ListaLocalidades.Add(Localidad);
                }
            }

            return ListaLocalidades;
        }

        #endregion

        #region Métodos Auxiliares



        #endregion


    }
}