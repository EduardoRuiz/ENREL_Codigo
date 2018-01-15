using ENREL.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ENREL.Models.DiasInhabiles
{
    public class LogicaDiasInhabiles
    {
        #region Variables:

        DatosDiasInhabiles DatosAuxiliar = new DatosDiasInhabiles();
        DataTable DtAuxiliar = new DataTable();
        List<CatDiasInhabiles> ListaTiposUsuario = new List<CatDiasInhabiles>();

        #endregion

        #region Métodos Principales:

        public List<CatDiasInhabiles> L_SeleccionarDiasInhabiles(int Mes, int Año)
        {
            DataTable dtDdiasInhabiles = new DataTable();
            List<CatDiasInhabiles> ListaDiasInhabiles = new List<CatDiasInhabiles>();

            dtDdiasInhabiles = DatosAuxiliar.D_SeleccionarDiasInhabiles(Mes, Año);

            if (dtDdiasInhabiles.Rows.Count > 0)
            {

                foreach (DataRow row in dtDdiasInhabiles.Rows)
                {
                    CatDiasInhabiles DiaInhabil = new CatDiasInhabiles();
                    DiaInhabil.IdDiaInhabil = (Int32)row["IdDiaInhabil"];
                    DiaInhabil.Dia = (Int32)row["Dia"];
                    DiaInhabil.Mes = (Int32)row["Mes"];
                    DiaInhabil.Año = (Int32)row["Año"];
                    DiaInhabil.FechaCompleta = String.Format("{0:dd/MMMM/yyyy}",(DateTime)row["FechaCompleta"]);
                    DiaInhabil.Activo = (bool)row["Activo"];
                    
                    ListaDiasInhabiles.Add(DiaInhabil);
                }
            }
            return ListaDiasInhabiles;
        }

        public bool L_InsertarDiaInhabil(CatDiasInhabiles DiaInhabil)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_InsertarDiaInhabil(DiaInhabil);
            return Resultado;
        }

        public bool L_ActualizarDiaInhabil(int IdDiaInhabil, bool IdEstatus)
        {
            bool Resultado = false;
            Resultado = DatosAuxiliar.D_ActualizarEstatusDiaInhabil(IdDiaInhabil, IdEstatus);
            return Resultado;
        }

        #endregion

        #region Métodos Auxiliares:

        #endregion
    }
}