require([
  "dojo/dom", "dojo/_base/array", "dojo/promise/all", "dojo/json",
  "dijit/registry", "dojo/parser",
  "esri/domUtils", "esri/graphic", "esri/graphicsUtils",
  "esri/geometry/Point", "esri/tasks/Geoprocessor",
  "esri/tasks/FeatureSet",
  "esri/config", "esri/request", "dojo/domReady!"
], function (
  dom, array, all, JSON,
  registry, parser,
  domUtils, Graphic, graphicsUtils,
  Point, Geoprocessor,
  FeatureSet,
  esriConfig, esriRequest
) {
    var geoprocessor;
    parser.parse();

    //TIEMPO DE ESPERA PARA LA EJECUCION DEL GEOPROCESO.
    esriConfig.defaults.io.timeout = 300000;

    //DECLARACION DE URL Y ESPACIO REFERENCIAL DEL PROCESO QUE SE UTILIZARA PARA OBTENER LOS SITIOS CERCANOS
    geoprocessor = new Geoprocessor("https://dgel.energia.gob.mx/arcgis/rest/services/VER/Proximidad/GPServer/Cercania");
    //geoprocessor = new Geoprocessor("http://inere.energia.gob.mx:6080/arcgis/rest/services/VER/Proximidad/GPServer/Cercania");
    //geoprocessor = new Geoprocessor("https://dgel.energia.gob.mx/arcgis/rest/services/VER/Proximidad/GPServer/Cercania");
    geoprocessor.setOutSpatialReference({ wkid: 4326 });

    IniciarGeoProceso();

    //FUNCION BOTON PARA INICIAR PROCESO DE GEOPROCESO Y CREACION DE PDF
    registry.byId("crear").on("click", function () {
        var Mensaje = "Click recibido";
        //document.getElementById('cargandoinere').show();

        //GUARDA EL VALOR DE LATITUD Y LONGUITUD DEL FORMULARIO
        var Lat = document.getElementById("sLat").value;
        var Lo = document.getElementById("sLong").value;

        Mensaje = "Latitutd: " + Lat + ", Longitud: " + Lo;

        //CREA PUNTO PARA MANDAR A SERVICIO DE GEOPROCESO
        var punto = new Point(new Point(Lo, Lat));
        var clickPointGraphic = new Graphic(punto);

        var featureSet = new FeatureSet();
        featureSet.features = [clickPointGraphic];
        //PARAMETRO QUE SOLICITA EL GEOPROCESO
        var params = {
            "Punto": featureSet
        };

        //MANDA EL PARAMETRO Y COMIENZA LA EJECUCION DEL GEOPROCESO
        var Mensaje = "";
        Mensaje = 'MANDA EL PARAMETRO Y COMIENZA LA EJECUCION DEL GEOPROCESO. Parametros: ' + params.toString();

        geoprocessor.execute(params);
        //AL TERMINAR EL GEOPROCESO SE ENVIAN LOS DATOS OBTENIDOS A LA FUNCION ONTASKSUCCESS
        dojo.connect(geoprocessor, "onExecuteComplete", onTaskSuccess);
        //EN CASO DE SURGIR UN ERROR SE ENVIA A LA FUNCION ONTASKFAILURE
        dojo.connect(geoprocessor, "onError", onTaskFailure);


    });

    //RESULTADOS DE GEOPROCESO PARA LA OBTENCION DE DATOS PARA APGV
    function onTaskSuccess(results, messages) {
        var Mensaje = "El geoproceso corrió con éxito";
        //document.getElementById("spop0").value= results[0].value.features[0].attributes.Nombre1;
        //NOTA spop0 CAMBIARA CUANDO YA SE PUEDA MOSTRAR EL VALOR DE TENSIONES POR LA LINEA ANTERIOR COMENTADA
        //DATOS DE PROXIMIDAD DE PARA NOMBRE
        document.getElementById("spop0").value = "Sin Información";
        document.getElementById("spop1").value = "Sin Información";
        document.getElementById("spop2").value = results[0].value.features[2].attributes.Nombre1;
        document.getElementById("spop3").value = results[0].value.features[3].attributes.Nombre1;
        document.getElementById("spop4").value = results[0].value.features[4].attributes.Nombre1;
        document.getElementById("spop5").value = results[0].value.features[5].attributes.Nombre1;
        document.getElementById("spop6").value = results[0].value.features[6].attributes.Nombre1;
        document.getElementById("spop7").value = results[0].value.features[7].attributes.Nombre1;
        //DATOS DE DISTANCIA
        document.getElementById("sDis0").value = results[0].value.features[0].attributes.NEAR_DIST.toFixed(2);
        document.getElementById("sDis1").value = results[0].value.features[1].attributes.NEAR_DIST.toFixed(2);
        document.getElementById("sDis2").value = results[0].value.features[2].attributes.NEAR_DIST.toFixed(2);
        document.getElementById("sDis3").value = results[0].value.features[3].attributes.NEAR_DIST.toFixed(2);
        document.getElementById("sDis4").value = results[0].value.features[4].attributes.NEAR_DIST.toFixed(2);
        document.getElementById("sDis5").value = results[0].value.features[5].attributes.NEAR_DIST.toFixed(2);
        document.getElementById("sDis6").value = results[0].value.features[6].attributes.NEAR_DIST.toFixed(2);
        document.getElementById("sDis7").value = results[0].value.features[7].attributes.NEAR_DIST.toFixed(2);

        //document.getElementById("sCar0").value= results[0].value.features[0].attributes.Caract;
        //NOTA sCar0 CAMBIARA CUNDO LA INFOMACION DE TENSIONES PUEDA SER MOSTRADA POR LA LINEA ANTERIOR COMENTADA
        //DATOS DE CARACTERISTICAS
        document.getElementById("sCar0").value = "Sin Información";
        document.getElementById("sCar1").value = results[0].value.features[1].attributes.Caract;
        document.getElementById("sCar2").value = results[0].value.features[2].attributes.Caract;
        document.getElementById("sCar3").value = results[0].value.features[3].attributes.Caract;
        document.getElementById("sCar4").value = results[0].value.features[4].attributes.Caract;
        document.getElementById("sCar5").value = results[0].value.features[5].attributes.Caract;
        document.getElementById("sCar6").value = results[0].value.features[6].attributes.Caract;
        document.getElementById("sCar7").value = results[0].value.features[7].attributes.Caract;
        //ENVIA DATOS A LA CREACION DEL PDF
        document.getElementById("myForm").submit();
    }

    //FUNCION DE ERROR EN CASO DE QUE LA EJECUCION DEL GEOPROCESO SEA INTERRUMPIDA O ERRONEA
    function onTaskFailure(error) {
        // MENSAJE CON ERROR OBTENIDO
        var Mensaje = "El geoproceso corrió con fallas: " + error;
    }

    function IniciarGeoProceso() {
        //document.getElementById('cargandoinere').show();
        var Mensaje = "Iniciando geoproceso";

        //GUARDA EL VALOR DE LATITUD Y LONGUITUD DEL FORMULARIO
        var Lat = document.getElementById("sLat").value;
        var Lo = document.getElementById("sLong").value;

        Mensaje = "Latitutd: " + Lat + ", Longitud: " + Lo;

        //CREA PUNTO PARA MANDAR A SERVICIO DE GEOPROCESO
        var punto = new Point(new Point(Lo, Lat));
        var clickPointGraphic = new Graphic(punto);

        var featureSet = new FeatureSet();
        featureSet.features = [clickPointGraphic];
        //PARAMETRO QUE SOLICITA EL GEOPROCESO
        var params = {
            "Punto": featureSet
        };

        //MANDA EL PARAMETRO Y COMIENZA LA EJECUCION DEL GEOPROCESO
        Mensaje = "Ejecutando geoproceso";
        geoprocessor.execute(params);
        //AL TERMINAR EL GEOPROCESO SE ENVIAN LOS DATOS OBTENIDOS A LA FUNCION ONTASKSUCCESS
        dojo.connect(geoprocessor, "onExecuteComplete", onTaskSuccess);
        //EN CASO DE SURGIR UN ERROR SE ENVIA A LA FUNCION ONTASKFAILURE
        dojo.connect(geoprocessor, "onError", onTaskFailure);


    }

    //IniciarGeoProceso();

});
