using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ENREL.Models.EntidadesFederativas
{
    public class LogicaEntidadesFederativas
    {
        #region Variables

        List<CatEntidadesFederativas> ListaEntidadesFederativas = new List<CatEntidadesFederativas>();
        DatosEntidadesFederativas DatosAuxiliar = new DatosEntidadesFederativas();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos Principales

        public List<CatEntidadesFederativas> L_SeleccionarEntidadesFederativas(int? IdCodigoPostal, int? IdEntidadFederativa)
        {
            if (IdCodigoPostal == null)
            {
                DtAuxiliar = DatosAuxiliar.D_SeleccionarEntidadesFederativas(IdEntidadFederativa);
            }
            else
            {
                DtAuxiliar = DatosAuxiliar.D_SeleccionarEntidadesFederativasPorCP(IdCodigoPostal);
                if (DtAuxiliar.Rows.Count <= 0)
                {
                    DtAuxiliar = DatosAuxiliar.D_SeleccionarEntidadesFederativasPorCPLimitado(IdCodigoPostal);
                }
                
            }

            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatEntidadesFederativas EntidadFederativa = new CatEntidadesFederativas();
                    EntidadFederativa.IdEntidadFederativa = (Int32)dr["IdEntidadFederativa"];
                    EntidadFederativa.EntidadFederativa = dr["EntidadFederativa"].ToString();
                    EntidadFederativa.IdMunicipioAsociado = (Int32)dr["IdMunicipio"];
                    EntidadFederativa.MunicipioAsociado = dr["Municipio"].ToString();
                    EntidadFederativa.IdLocalidadAsociada = (Int32)dr["IdLocalidad"];
                    EntidadFederativa.LocalidadAsociada = dr["Localidad"].ToString();
                    EntidadFederativa.TipoUbicacion = dr["TipoUbicacion"].ToString();
                    ListaEntidadesFederativas.Add(EntidadFederativa);
                }
            }

            return ListaEntidadesFederativas;
        }

        #endregion

        #region Métodos Auxiliares



        #endregion


    }
}