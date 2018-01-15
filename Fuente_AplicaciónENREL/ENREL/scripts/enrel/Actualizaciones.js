// ----------------------------- Actualizar formularios -------------------------------------------------------- //

function ActualizarElementosFormulario(elementocambiante) {

    var clave = elementocambiante.substring(0, 1);

    if (clave == "E") { clave = "E_"; }
    if (clave == "R") { clave = "RL_"; }
    if (clave == "P") { clave = "P_"; }
    if (clave == "U") { clave = "U_"; }

    var TextoCodigosPostales = "#" + clave + "CodigoPostal";
    var IdCodigoPostal = $(TextoCodigosPostales).val();

    var ListaEntidadesFederativas = "#" + clave + "IdEntidadFederativa";
    var ListaMunicipios = "#" + clave + "IdMunicipio";
    var ListaLocalidades = "#" + clave + "IdLocalidad";
    var ListaTecnologias = "#" + clave + "IdTecnologia";


    var IdEntidadFederativa = $(ListaEntidadesFederativas).val();
    var IdMunicipio = $(ListaMunicipios).val();
    var IdTecnologia = $(ListaTecnologias).val();

    if (elementocambiante.toLowerCase().indexOf("codigopostal") >= 0) {
        ActualizaEntidadesFederativas(IdCodigoPostal, ListaEntidadesFederativas, ListaMunicipios, ListaLocalidades);
    }
    if (elementocambiante.toLowerCase().indexOf("identidadfederativa") >= 0) {
        ActualizaMunicipios(IdEntidadFederativa, ListaMunicipios, ListaLocalidades);
    }
    if (elementocambiante.toLowerCase().indexOf("idmunicipio") >= 0) {
        ActualizaLocalidades(IdEntidadFederativa, IdMunicipio, ListaLocalidades);
    }
    if (elementocambiante.toLowerCase().indexOf("idtecnologia") >= 0) {
        ActualizaPreguntas(IdTecnologia, ListaTecnologias);
    }


}

function ActualizaEntidadesFederativas(IdCodigoPostal, ListaEntidadesFederativas, ListaMunicipios, ListaLocalidades) {
    var ddlEntidadFederativa = $(ListaEntidadesFederativas);
    ddlEntidadFederativa.empty();
    ddlEntidadFederativa.append($("<option></option>").val("").html("Selecciona un estado"));
    var TipoUbicacion = "Limitada";

    if (!isNaN(IdCodigoPostal)) {
        $.ajax
            (
                {
                    url: "/Auxiliar/GetEntidadesFederativas",
                    dataType: "json",
                    type: "GET",
                    contentType: 'application/json; charset=utf-8',
                    data: { StrIdCodigoPostal: IdCodigoPostal, StrIdEntidadFederativa: null },
                    success: function (data) {
                        var ddlEntidadFederativa = $(ListaEntidadesFederativas);
                        ddlEntidadFederativa.empty();
                        ddlEntidadFederativa.append($("<option></option>").val("0").html("Selecciona un estado"));
                        $.each(data, function (i, val) {
                            
                            TipoUbicacion = val.TipoUbicacion;
                            if (TipoUbicacion == "Completa")
                            {
                                
                                ddlEntidadFederativa.find('option[value=0]').remove();
                                ddlEntidadFederativa.append($("<option></option>").val(val.IdEntidadFederativa).html(val.EntidadFederativa));
                                AceptarElemento(ddlEntidadFederativa);

                                var ddlMunicipio = $(ListaMunicipios);
                                ddlMunicipio.empty();
                                ddlMunicipio.append($("<option></option>").val(val.IdMunicipioAsociado).html(val.MunicipioAsociado));
                                AceptarElemento(ddlMunicipio);

                                var ddlLocalidades = $(ListaLocalidades);
                                ddlLocalidades.empty();
                                ddlLocalidades.append($("<option></option>").val(val.IdLocalidadAsociada).html(val.LocalidadAsociada));
                                AceptarElemento(ddlLocalidades);
                            }
                            else
                            {                                                             
                                ddlEntidadFederativa.append($("<option></option>").val(val.IdEntidadFederativa).html(val.EntidadFederativa));
                            }               
                        });
                    },
                    error: function () {
                    }
                }
            );
    }

    if (TipoUbicacion == "Limitada") {
        var ddlMunicipio = $(ListaMunicipios);
        ddlMunicipio.empty();
        ddlMunicipio.append($("<option></option>").val("").html("Selecciona un municipio"));

        var ddlLocalidades = $(ListaLocalidades);
        ddlLocalidades.empty();
        ddlLocalidades.append($("<option></option>").val("").html("Selecciona una localidad"));
    }

    
}

function ActualizaMunicipios(IdEntidadFederativa, ListaMunicipios, ListaLocalidades) {

    var ddlMunicipios = $(ListaMunicipios);
    ddlMunicipios.empty();
    ddlMunicipios.append($("<option></option>").val("").html("Selecciona un municipio"));

    if (!isNaN(IdEntidadFederativa)) {
        $.ajax
            (
                {
                    url: "/Auxiliar/GetMunicipios",
                    dataType: "json",
                    type: "GET",
                    contentType: 'application/json; charset=utf-8',
                    data: { StrIdEntidadFederativa: IdEntidadFederativa },
                    success: function (data) {
                        $.each(data, function (i, val) {
                            ddlMunicipios.append($("<option></option>").val(val.IdMunicipio).html(val.Municipio));
                        });
                    },
                    error: function () {
                    }
                }
            );
    }

    var ddlLocalidades = $(ListaLocalidades);
    ddlLocalidades.empty();
    ddlLocalidades.append($("<option></option>").val("").html("Selecciona una localidad"));
}

function ActualizaLocalidades(IdEntidadFederativa, IdMunicipio, ListaLocalidades) {

    var ddlLocalidades = $(ListaLocalidades);
    ddlLocalidades.empty();
    ddlLocalidades.append($("<option></option>").val("").html("Selecciona una localidad"));

    if (!isNaN(IdMunicipio)) {
        $.ajax
            (
                {
                    url: "/Auxiliar/GetLocalidades",
                    dataType: "json",
                    type: "GET",
                    contentType: 'application/json; charset=utf-8',
                    data: { StrIdEntidadFederativa: IdEntidadFederativa, StrIdMunicipio: IdMunicipio },
                    success: function (data) {
                        $.each(data, function (i, val) {
                            ddlLocalidades.append($("<option></option>").val(val.IdLocalidad).html(val.Localidad));
                        });
                    },
                    error: function () {
                    }
                }
            );
    }

}

function ActualizaLocalizacionPorCP(IdCodigoPostal, ListaEntidadesFederativas, ListaMunicipios, ListaLocalidades) {
    var Mensaje_EF = "#Mensaje_" + Modelo + "IdEntidadFederativa";
    var Alerta_EF = "#Alerta_" + Modelo + "IdEntidadFederativa";

    var Mensaje_M = "#Mensaje_" + Modelo + "IdMunicipio";
    var Alerta_M = "#Alerta_" + Modelo + "IdMunicipio";

    var Mensaje_L = "#Mensaje_" + Modelo + "IdLocalidad";
    var Alerta_L = "#Alerta_" + Modelo + "IdLocalidad";

    var CodigoPostal = parseInt($(idCPJQ).val());

    var ddlEntidadesFederativas = $(idEntidadFederativaJQ);
    ddlEntidadesFederativas.empty();
    ddlEntidadesFederativas.append($("<option></option>").val("").html("Selecciona un estado"));

    var ddlMunicipios = $(idMunicipioJQ);
    ddlMunicipios.empty();
    ddlMunicipios.append($("<option></option>").val("").html("Selecciona un municipio"));

    var ddlLocalidades = $(idLocalidadJQ);
    ddlLocalidades.empty();
    ddlLocalidades.append($("<option></option>").val("").html("Selecciona una localidad"));

    $(Alerta_EF).css("color", "#d0021b");
    $(Mensaje_EF).text("Este campo es obligatorio");
    $(idEntidadFederativaJQ).css("border", "2px solid #d0021b");

    $(Alerta_M).css("color", "#d0021b");
    $(Mensaje_M).text("Este campo es obligatorio");
    $(idMunicipioJQ).css("border", "2px solid #d0021b");

    $(Alerta_L).css("color", "#d0021b");
    $(Mensaje_L).text("Este campo es obligatorio");
    $(idLocalidadJQ).css("border", "2px solid #d0021b");

    if (isNaN(CodigoPostal)) {
        var url_busqueda = "/Auxiliar/GetEntidadesFederativas";
    }
    else {

        var url_busqueda = "/Auxiliar/GetEntidadesFederativas";

        $(Alerta_EF).css("color", "black");
        $(Mensaje_EF).text("");
        $(idEntidadFederativaJQ).css("border", "1px solid #ccc");
    }

    $.ajax
        (
            {
                url: url_busqueda,
                dataType: "json",
                type: "GET",
                contentType: 'application/json; charset=utf-8',
                data: { CodigoPostal: CodigoPostal },
                success: function (data) {
                    $.each(data, function (i, val) {
                        ddlEntidadesFederativas.append($("<option></option>").val(val.IdEntidadFederativa).html(val.Nombre));
                        if (!isNaN(CodigoPostal)) {
                            ddlEntidadesFederativas.val(val.IdEntidadFederativa);
                        }
                        ActualizaMunicipios(idEntidadFederativaJQ, idMunicipioJQ);
                    });
                },
                error: function () {
                }
            }
        );


}



function ActualizaPreguntas(IdTecnologia, ListaTecnologias) {
    
    item = 0;
    TablaPreguntas = "";
    $("#Preguntas > tbody").html("");


    if (!isNaN(IdTecnologia)) {
        $.ajax
            (
                {
                    url: "/Auxiliar/GetPreguntas",
                    dataType: "json",
                    type: "GET",
                    contentType: 'application/json; charset=utf-8',
                    data: { StrIdTecnologia: IdTecnologia },

                    success: function (data) {
                        $.each(data, function (i, val) {
                            if (val != null) {
                                x = "table-row";
                                if (val.Pregunta.indexOf("MW") >= 0) {
                                    x = "none";
                                }
                                TablaPreguntas = TablaPreguntas +
                                    "<tr style='display:" + x + ";'><td style='display:none'>"
                                    + val.IdPregunta
                                    + "</td><td>"
                                    + val.Pregunta
                                    //+ "</td><td><input type='checkbox' name = Pregunta_" + val.IdPregunta + "></td>"
                                    + "</td><td><select class='form-control ns_ form-control-error' data-val='true' data-val-number='The field P_IdTecnologia must be a number.' data-val-required='El campo P_IdPregunta es obligatorio.' id = Pregunta_" + val.IdPregunta + " name = Pregunta_" + val.IdPregunta + " onblur='ValidarObjeto(this,event);'>"
                                    + "<option value='0'>No</option>"
                                    + "<option value='1'>Sí</option>"
                                    + "</select></td></tr>";
                            }

                        });
                        $("#Preguntas").append(TablaPreguntas);
                    },
                    error: function () {
                    }
                }
            );
    }

}

function ActualizaPreguntasInicio(IdTecnologia) {

    item = 0;
    TablaPreguntas = "";
    $("#Preguntas > tbody").html("");


    if (!isNaN(IdTecnologia)) {
    
        $.ajax
            (
                {
                    url: "/Auxiliar/GetPreguntas",
                    dataType: "json",
                    type: "GET",
                    contentType: 'application/json; charset=utf-8',
                    data: { StrIdTecnologia: IdTecnologia },

                    success: function (data) {
                        $.each(data, function (i, val) {
                            if (val != null) {
                                visible = true;
                                if (val.Pregunta.indexOf("MW") >= 0)
                                {
                                    visible = false;
                                }
                                TablaPreguntas = TablaPreguntas +
                                    "<tr style='visible:" + visible.toString() + ";'><td>"
                                    + val.IdPregunta
                                    + "</td><td>"
                                    + val.Pregunta
                                    //+ "</td><td><input type='checkbox' name = Pregunta_" + val.IdPregunta + "></td>"
                                    + "</td><td><select class='form-control ns_ form-control-error' data-val='true' data-val-number='The field P_IdTecnologia must be a number.' data-val-required='El campo P_IdPregunta es obligatorio.' id = Pregunta_" + val.IdPregunta + " name = Pregunta_" + val.IdPregunta + " onblur='ValidarObjeto(this,event);'>"
                                    + "<option value='0'>No</option>"
                                    + "<option value='1'>Sí</option>"
                                    + "</select></td></tr>";
                            }

                        });
                        $("#Preguntas").append(TablaPreguntas);
                    },
                    error: function () {
                    }
                }
            );
    }

}

function ActualizaProyectoPreguntas(IdProyecto) {
    item = 0;
    TablaPreguntas = "";
    $("#Preguntas > tbody").html("");


    if (!isNaN(IdProyecto)) {
        $.ajax
            (
                {
                    url: "/Auxiliar/GetProyectoPreguntas",
                    dataType: "json",
                    type: "GET",
                    contentType: 'application/json; charset=utf-8',
                    data: { StrIdProyecto: IdProyecto },

                    success: function (data) {
                        $.each(data, function (i, val) {
                            if (val != null) {
                                TablaPreguntas = TablaPreguntas +
                                    "<tr><td>"
                                    + val.Pregunta
                                    + "</td><td style='text-align:center'>";
                                if (val.Respuesta == true)
                                {
                                    TablaPreguntas = TablaPreguntas + "Sí";
                                }
                                else
                                {
                                    TablaPreguntas = TablaPreguntas + "No";
                                }
                                    
                                + "</td></tr>";
                                //if (val.Modificar == true)
                                //{
                                //    TablaPreguntas =
                                //    TablaPreguntas + "<td><select class='form-control ns_ form-control-error' data-val='true' data-val-number='The field P_IdTecnologia must be a number.' data-val-required='El campo P_IdPregunta es obligatorio.' id = Pregunta_" + val.IdPregunta + " name = Pregunta_" + val.IdPregunta + " onblur='ValidarObjeto(this,event);'>"
                                //    + "<option value='0'>No</option>"
                                //    + "<option value='1'>Sí</option>"
                                //    + "</select></td></tr>";
                                //}
                                //else
                                //{
                                //    TablaPreguntas = TablaPreguntas + "<td></td></tr>";
                                //}
                            }


                        });
                        $("#Preguntas").append(TablaPreguntas);
                    },
                    error: function () {
                    }
                }
            );
    }

}