using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENREL.Models.ServiciosWeb
{
    public class consultarEstatusMacroTramiteResponse
    {
        private byte codigoField;

        private string ipField;

        private string mensajeField;

        private cadenaInteroperabilidad[] cadenaInteroperabilidadField;

        private string estatusMacrotramiteField;

        private string fechaActualizacionMacroTramiteField;

        private string fechaRegistroRefMacroTramiteField;

        private string idGlobalMacroTramiteField;

        private byte idOperadorField;

        private string identidadField;

        private byte noTramitesField;

        private byte todosField;

        public byte codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        public string ip
        {
            get
            {
                return this.ipField;
            }
            set
            {
                this.ipField = value;
            }
        }

        public string mensaje
        {
            get
            {
                return this.mensajeField;
            }
            set
            {
                this.mensajeField = value;
            }
        }

        public cadenaInteroperabilidad[] cadenaInteroperabilidad
        {
            get
            {
                return this.cadenaInteroperabilidadField;
            }
            set
            {
                this.cadenaInteroperabilidadField = value;
            }
        }

        public string estatusMacrotramite
        {
            get
            {
                return this.estatusMacrotramiteField;
            }
            set
            {
                this.estatusMacrotramiteField = value;
            }
        }

        public string fechaActualizacionMacroTramite
        {
            get
            {
                return this.fechaActualizacionMacroTramiteField;
            }
            set
            {
                this.fechaActualizacionMacroTramiteField = value;
            }
        }

        public string fechaRegistroRefMacroTramite
        {
            get
            {
                return this.fechaRegistroRefMacroTramiteField;
            }
            set
            {
                this.fechaRegistroRefMacroTramiteField = value;
            }
        }

        public string idGlobalMacroTramite
        {
            get
            {
                return this.idGlobalMacroTramiteField;
            }
            set
            {
                this.idGlobalMacroTramiteField = value;
            }
        }

        public byte idOperador
        {
            get
            {
                return this.idOperadorField;
            }
            set
            {
                this.idOperadorField = value;
            }
        }

        public string identidad
        {
            get
            {
                return this.identidadField;
            }
            set
            {
                this.identidadField = value;
            }
        }

        public byte noTramites
        {
            get
            {
                return this.noTramitesField;
            }
            set
            {
                this.noTramitesField = value;
            }
        }

        public byte todos
        {
            get
            {
                return this.todosField;
            }
            set
            {
                this.todosField = value;
            }
        }
    }

    public partial class cadenaInteroperabilidad
    {

        private string fechaReferenciaField;

        private string idGlobalTramiteField;

        private string identidadField;

        private cadenaInteroperabilidadListaEstatus[] listaEstatusField;

        private byte operadorField;

        /// <remarks/>
        public string fechaReferencia
        {
            get
            {
                return this.fechaReferenciaField;
            }
            set
            {
                this.fechaReferenciaField = value;
            }
        }

        /// <remarks/>
        public string idGlobalTramite
        {
            get
            {
                return this.idGlobalTramiteField;
            }
            set
            {
                this.idGlobalTramiteField = value;
            }
        }

        /// <remarks/>
        public string identidad
        {
            get
            {
                return this.identidadField;
            }
            set
            {
                this.identidadField = value;
            }
        }

        public cadenaInteroperabilidadListaEstatus[] listaEstatus
        {
            get
            {
                return this.listaEstatusField;
            }
            set
            {
                this.listaEstatusField = value;
            }
        }

        /// <remarks/>
        public byte operador
        {
            get
            {
                return this.operadorField;
            }
            set
            {
                this.operadorField = value;
            }
        }
    }

    public partial class cadenaInteroperabilidadListaEstatus
    {

        private string estatusField;

        private string fechaRegistroField;

        private string notaField;

        private string resolucionField;

        /// <remarks/>
        public string estatus
        {
            get
            {
                return this.estatusField;
            }
            set
            {
                this.estatusField = value;
            }
        }

        /// <remarks/>
        public string fechaRegistro
        {
            get
            {
                return this.fechaRegistroField;
            }
            set
            {
                this.fechaRegistroField = value;
            }
        }

        /// <remarks/>
        public string nota
        {
            get
            {
                return this.notaField;
            }
            set
            {
                this.notaField = value;
            }
        }

        /// <remarks/>
        public string resolucion
        {
            get
            {
                return this.resolucionField;
            }
            set
            {
                this.resolucionField = value;
            }
        }
    }

}