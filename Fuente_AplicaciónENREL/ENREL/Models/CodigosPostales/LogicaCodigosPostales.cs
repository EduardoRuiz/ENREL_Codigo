using ENREL.Controllers.Auxiliar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ENREL.Models.CodigosPostales
{
    public class LogicaCodigosPostales
    {
        #region Variables:

        List<CatCodigosPostales> ListaCodigosPostales = new List<CatCodigosPostales>();
        List<UbicacionPorCP> ListaUbicaciones = new List<UbicacionPorCP>();
        DatosCodigosPostales DatosAuxiliar = new DatosCodigosPostales();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos Principales:

        public List<CatCodigosPostales> L_SeleccionarCodigosPostales(int? IdCodigoPostal)
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarCodigosPostales(IdCodigoPostal);
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatCodigosPostales CodigoPostal = new CatCodigosPostales();
                    CodigoPostal.IdCodigoPostal = (Int32)dr["IdCodigoPostal"];
                    CodigoPostal.CodigoPostal = dr["IdCodigoPostal"].ToString();
                    ListaCodigosPostales.Add(CodigoPostal);
                }
            }

            return ListaCodigosPostales;
        }

        public List<UbicacionPorCP> L_SeleccionarUbicacionPorCP(int? IdCodigoPostal)
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarUbicacionPorCP(IdCodigoPostal);
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    UbicacionPorCP Ubicacion = new UbicacionPorCP();
                    Ubicacion.TipoUbicacion = dr["AreaGeografica"].ToString();
                    Ubicacion.IdUbicacion = (Int32)dr["IdPosicion"];
                    ListaUbicaciones.Add(Ubicacion);
                }
            }

            return ListaUbicaciones;
        }

        #endregion

        #region Métodos Auxiliares:



        #endregion

    }
}