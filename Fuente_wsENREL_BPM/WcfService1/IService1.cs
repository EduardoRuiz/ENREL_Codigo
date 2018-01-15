using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService1.ClasesAuxiliares;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "HabilitarTramite/{idProyecto}/{Homoclave}/{Estatus}")]
        string HabilitarTramite(string IdProyecto, string Homoclave, string Estatus);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ConfirmarRecepcionEstatus/{idProyecto}/{Homoclave}/{Estatus}")]
        string ConfirmarRecepcionEstatus(string IdProyecto, string Homoclave, string Estatus);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ConfirmarBPM/{idProyecto}/{IdEstatus}")]
        string ConfirmarBPM(string IdProyecto, string IdEstatus);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "EnviarAlerta/{idProyecto}/{Homoclave}/{TipoAlerta}")]
        string EnviarAlerta(string IdProyecto, string Homoclave, string TipoAlerta);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Opcional/{idProyecto}/{IdPregunta}")]
        string Opcional(string IdProyecto, string IdPregunta);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "DatosEmpresaPorProyecto/{idProyecto}")]
        string DatosEmpresaPorProyecto(string idProyecto);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ObtenerAlertas/{idProyecto}/{Homoclave}/{Variable}")]
        int ObtenerAlertas(string idProyecto, string Homoclave, string Variable);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "ObtenerEstatus/{idProyecto}/{Homoclave}")]
        int ObtenerEstatus(string IdProyecto, string Homoclave);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
