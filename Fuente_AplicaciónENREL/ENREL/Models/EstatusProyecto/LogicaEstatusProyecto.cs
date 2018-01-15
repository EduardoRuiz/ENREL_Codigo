using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ENREL.Models.EstatusProyecto;

namespace ENREL.Models.EstatusProyecto
{
    public class LogicaEstatusProyecto
    {
        #region Variables:

        DataTable dtEstProyectos = new DataTable();
        List<CatEstatusProyecto> ListEstProyetos = new List<CatEstatusProyecto>();
        DatosEstatusProyecto proc = new DatosEstatusProyecto();

        #endregion

        #region Métodos principales:

        public List<CatEstatusProyecto> ListaProyectos()
        {
            dtEstProyectos = proc.EstatusProyectos();
            if (dtEstProyectos.Rows.Count > 0)
            {
                foreach (DataRow drow in dtEstProyectos.Rows)
                {
                    CatEstatusProyecto estP = new CatEstatusProyecto();
                    estP.IdEstatusProyecto = (Int32)drow["idEstatusProyecto"];
                    estP.EstadoProyecto = drow["Nombre"].ToString();
                    ListEstProyetos.Add(estP);
                }
            }
            return ListEstProyetos;
        }

        #endregion

        #region Métodos auxiliares:

        #endregion

        

    }
}