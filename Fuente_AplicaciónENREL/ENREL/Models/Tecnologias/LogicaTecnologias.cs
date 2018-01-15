using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ENREL.Models.Tecnologias
{
    public class LogicaTecnologias
    {
        #region Variables:

        List<CatTecnologias> ListaTecnologias = new List<CatTecnologias>();
        DatosTecnologias DatosAuxiliar = new DatosTecnologias();
        DataTable DtAuxiliar = new DataTable();

        #endregion

        #region Métodos principales:

        public List<CatTecnologias> L_SeleccionarTecnologias()
        {
            DtAuxiliar = DatosAuxiliar.D_SeleccionarTecnologias();
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatTecnologias Tecnologia = new CatTecnologias();
                    Tecnologia.IdTecnologia = (Int32)dr["IdTecnologia"];
                    Tecnologia.Tecnologia = dr["Tecnologia"].ToString();
                    ListaTecnologias.Add(Tecnologia);
                }
            }

            return ListaTecnologias;
        }

        public List<CatTecnologiaPreguntas> L_SeleccionarTecnologiaPreguntas(int IdTecnologia)
        {
            List<CatTecnologiaPreguntas> ListaTecnologiaPreguntas = new List<CatTecnologiaPreguntas>();
            DtAuxiliar = DatosAuxiliar.D_SeleccionarTecnologiaPreguntas(IdTecnologia);
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatTecnologiaPreguntas TecnologiaPregunta = new CatTecnologiaPreguntas();
                    TecnologiaPregunta.Pregunta = dr["Pregunta"].ToString();
                    TecnologiaPregunta.IdTecnologia = (Int32)dr["IdTecnologia"];
                    TecnologiaPregunta.IdTecnologiaPregunta = (Int32)dr["IdTecnologiaPregunta"];

                    ListaTecnologiaPreguntas.Add(TecnologiaPregunta);
                }

                return ListaTecnologiaPreguntas;
            }
            else
            {
                ListaTecnologiaPreguntas = new List<CatTecnologiaPreguntas>();
                return ListaTecnologiaPreguntas;
            }

        }

        public List<CatTecnologiaTramites> L_SeleccionarTecnologiaTramites(int IdTecnologia)
        {
            List<CatTecnologiaTramites> ListaTecnologiaTramites = new List<CatTecnologiaTramites>();
            DtAuxiliar = DatosAuxiliar.D_SeleccionarTecnologiaTramites(IdTecnologia);
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatTecnologiaTramites TecnologiaTramite = new CatTecnologiaTramites();
                    TecnologiaTramite.IdTecnologia = (Int32)dr["IdTecnologia"];
                    TecnologiaTramite.IdTramite = (Int32)dr["IdTramite"];
                    TecnologiaTramite.Homoclave = dr["Homoclave"].ToString();
                    TecnologiaTramite.IdFase = (Int32)dr["IdFase"];
                    TecnologiaTramite.IdPosicion = (Int32)dr["IdPosicion"];
                    TecnologiaTramite.Activo = Convert.ToBoolean(dr["Activo"]);
                    TecnologiaTramite.IdTecnologiaTramite = (Int32)dr["IdTecnologiaTamite"];

                    ListaTecnologiaTramites.Add(TecnologiaTramite);
                }

                return ListaTecnologiaTramites;
            }
            else
            {
                ListaTecnologiaTramites = new List<CatTecnologiaTramites>();
                return ListaTecnologiaTramites;
            }

        }

        public CatTecnologias L_DetallesTecnologias(int IdTecnologia)
        {
            List<CatTecnologias> ListaTecnologias = new List<CatTecnologias>();
            DtAuxiliar = DatosAuxiliar.D_DetallesTecnologia(IdTecnologia);
            if (DtAuxiliar.Rows.Count > 0)
            {
                foreach (DataRow dr in DtAuxiliar.Rows)
                {
                    CatTecnologias Tecnologia = new CatTecnologias();
                    Tecnologia.Tecnologia = dr["Tecnologia"].ToString();
                    Tecnologia.IdTecnologia = (Int32)dr["IdTecnologia"];

                    ListaTecnologias.Add(Tecnologia);
                }

                return ListaTecnologias[0];
            }
            else
            {
                CatTecnologias Tecnologia = new CatTecnologias();
                return Tecnologia;
            }

        }

        public int L_InsertarTecnologia(CatTecnologias NuevaTecnologia)
        {
            int IdTecnologia;
            DatosTecnologias DatosTecnologias = new DatosTecnologias();
            IdTecnologia = DatosTecnologias.D_InsertarTecnologia(NuevaTecnologia);
            return IdTecnologia;         
        }

        public void L_InsertarTecnologiaPregunta(int IdTecnologia, string Pregunta)
        {
            DatosTecnologias DatosTecnologias = new DatosTecnologias();
            DatosTecnologias.D_InsertarTecnologiaPreguntas(IdTecnologia, Pregunta);
        }

        public void L_InsertarTecnologiaTramite(int IdTecnologia,int IdFase,int  IdTramite,int  IdPosicion)
        {
            DatosTecnologias DatosTecnologias = new DatosTecnologias();
            DatosTecnologias.D_InsertarTecnologiaTramites(IdTecnologia, IdFase, IdTramite, IdPosicion);
        }

        public bool L_ActualizarTecnologia(CatTecnologias Tecnologia)
        {
            DatosTecnologias DatosTecnologias = new DatosTecnologias();
            DatosTecnologias.D_ActualizarTecnologia(Tecnologia);
            return true;
        }

        public void L_ActualizarTecnologiaPregunta(int IdTecnologia, string pregunta)
        {
            DatosTecnologias DatosTecnologias = new DatosTecnologias();
            DatosTecnologias.D_ActualizarTecnologiaPregunta(IdTecnologia, pregunta);
        }

        public void L_ActualizarTecnologiaTramite(int IdTecnologia, int IdFase, int IdTramite, int IdPosicion)
        {
            DatosTecnologias DatosTecnologias = new DatosTecnologias();
            DatosTecnologias.D_ActualizarTecnologiaTramites(IdTecnologia, IdFase, IdTramite, IdPosicion);
        }

        public bool L_EliminarTecnologia(int IdTecnologia)
        {
            DatosTecnologias DatosTecnologias = new DatosTecnologias();
            DatosTecnologias.D_EliminarTecnologia(IdTecnologia);
            return true;           
        }

        #endregion

        #region Métodos auxiliares:

        public bool L_PrepararActualizacionTecnologia(int IdTecnologia)
        {
            DatosTecnologias DatosTecnologias = new DatosTecnologias();
            DatosTecnologias.D_EliminarTecnologiaPreguntasPorTecnologia(IdTecnologia);
            DatosTecnologias.D_EliminarTecnologiaTramitesPorTecnologia(IdTecnologia);
            return true;
        }

        #endregion

    }
}