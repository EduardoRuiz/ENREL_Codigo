using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ENREL.Models.TiposDia
{
    public class LogicaTiposDia
    {

        #region Variables:

        List<CatTiposDia> ListaTiposDias = new List<CatTiposDia>();
        DatosTiposDia DatosAuxiliar = new DatosTiposDia();
        DataTable dtAuxiliar = new DataTable();

        #endregion

        #region Métodos Principales:

        public List<CatTiposDia> L_ListaTiposDia()
        {
            dtAuxiliar = DatosAuxiliar.D_SeleccionarTiposDia();

            if (dtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in dtAuxiliar.Rows)
                {
                    CatTiposDia TipoDia = new CatTiposDia();
                    TipoDia.IdTipoDia = (Int32)dr["IdTipoDia"];
                    TipoDia.TipoDia = dr["TipoDia"].ToString();
                    ListaTiposDias.Add(TipoDia);
                }
            }

            return ListaTiposDias;
        }

        #endregion

    }

}