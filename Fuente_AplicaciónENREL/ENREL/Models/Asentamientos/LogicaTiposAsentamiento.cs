using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ENREL.Models.Asentamientos
{
    public class LogicaTiposAsentamiento
    {

        #region Variables

        List<CatTiposAsentamiento> ListaTiposAsentamiento = new List<CatTiposAsentamiento>();
        DatosTiposAsentamiento DatosAuxiliar = new DatosTiposAsentamiento();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos Principales

        public List<CatTiposAsentamiento> L_SeleccionarTiposAsentamiento()
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarTiposAsentamiento();
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatTiposAsentamiento TipoAsentamiento = new CatTiposAsentamiento();
                    TipoAsentamiento.IdTipoAsentamiento = (Int32)dr["IdTipoAsentamiento"];
                    TipoAsentamiento.TipoAsentamiento = dr["TipoAsentamiento"].ToString();
                    ListaTiposAsentamiento.Add(TipoAsentamiento);
                }
            }

            return ListaTiposAsentamiento;
        }

        #endregion

        #region Métodos Auxiliares



        #endregion

    }
}