using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ENREL.Controllers.Auxiliar
{
    public class WS_Gobmx
    {
        public int metodo { get; set; }
        public string idGlobalMacroTramite { get; set; }
        public string idOperador { get; set; }
        public string fechaHora { get; set; }
        public string identidad { get; set; }
        public string ip { get; set; }
        public int tipoIdentidad { get; set; }
        public string email { get; set; }
    }

    public class EstatusTramite
    {
        public string estatus { get; set; }
        public string fechaRegistro { get; set; }
        public string nota { get; set; }
        public string resolucion { get; set; }
    }

    public class CadenaInteroperabilidad
    {
        public string fechaReferencia { get; set; }
        public string idGlobalTramite { get; set; }
        public string identidad { get; set; }
        public IList<EstatusTramite> listaEstatus { get; set; }
        public string operador { get; set; }
    }

    public class RespuestaConsultaMacroTramite
    {
        public int codigo { get; set; }
        public string ip { get; set; }
        public string mensaje { get; set; }
        public IList<CadenaInteroperabilidad> cadenaInteroperabilidad { get; set; }
        public string estatusMacrotramite { get; set; }
        public DateTime? fechaActualizacionMacroTramite { get; set; }
        public DateTime? fechaRegistroRefMacroTramite { get; set; }
        public string idGlobalMacroTramite { get; set; }
        public string idOperador { get; set; }
        public string identidad { get; set; }
        public int noTramites { get; set; }
        public string todos { get; set; }
    }


    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://TargetNamespace.com/EstatusTramitesRest_consultarEstatusMacroTramite_reque" +
        "st")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://TargetNamespace.com/EstatusTramitesRest_consultarEstatusMacroTramite_reque" +
        "st", IsNullable = false)]
    public partial class consultarEstatusMacroTramiteResponse
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("cadenaInteroperabilidad", Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "")]
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("listaEstatus")]
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

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
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
