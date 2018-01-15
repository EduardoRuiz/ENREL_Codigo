using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ENREL.Models.Vialidades
{
    public class LogicaTiposVialidad
    {
        #region Variables

        List<CatTiposVialidad> ListaTiposVialidad = new List<CatTiposVialidad>();
        DatosTiposVialidad DatosAuxiliar = new DatosTiposVialidad();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos Principales

        public List<CatTiposVialidad> L_SeleccionarTiposVialidad()
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarTiposVialidad();
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatTiposVialidad TipoVialidad = new CatTiposVialidad();
                    TipoVialidad.IdTipoVialidad = (Int32)dr["IdTipoVialidad"];
                    TipoVialidad.TipoVialidad = dr["TipoVialidad"].ToString();
                    ListaTiposVialidad.Add(TipoVialidad);
                }
            }

            return ListaTiposVialidad;
        }

        #endregion

        #region Métodos Auxiliares



        #endregion

    }
}