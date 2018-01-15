using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ENREL.Models.Municipios
{
    public class LogicaMunicipios
    {

        #region Variables

        List<CatMunicipios> ListaMunicipios = new List<CatMunicipios>();
        DatosMunicipios DatosAuxiliar = new DatosMunicipios();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos Principales

        public List<CatMunicipios> L_SeleccionarMunicipios(int? IdCodigoPostal, int? IdEntidadFederativa, int? IdMunicipio)
        {
            if (IdCodigoPostal == null)
            {
                DtAuxiliar = DatosAuxiliar.D_SeleccionarMunicipios(IdEntidadFederativa, IdMunicipio);
            }
            else
            {
                DtAuxiliar = DatosAuxiliar.D_SeleccionarMunicipiosPorCP(IdEntidadFederativa, IdMunicipio);
            }

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatMunicipios Municipio = new CatMunicipios();
                    Municipio.IdMunicipio = (Int32)dr["IdMunicipio"];
                    Municipio.Municipio = dr["Municipio"].ToString();
                    ListaMunicipios.Add(Municipio);
                }
            }

            return ListaMunicipios;
        }

        #endregion

        #region Métodos Auxiliares



        #endregion


    }
}