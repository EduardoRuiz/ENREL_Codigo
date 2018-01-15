//Funciones de validación
var enviado = 0;

function ValidarFormulario(event, item) {
    wevent = event || window.event || event.srcElement;

    var cuenta_errores = 0;
    var Formulario = "#" + $(item).attr("id");
    var MensajeDeSalida = "";
    var PrimerError = "True";

    //Recorre elementos del formulario
    $(Formulario).find(':input').each(function () {
        try {
            var elemento = this;
            var Type = elemento.type;
            var TiposDeObjeto = ['text', 'textarea', 'select-one', 'number', 'password', 'file'];
            var NoPermitidos = ['file1', 'file2', 'E_ClavePrivada', 'RL_ClavePrivada', 'U_ClavePrivada', 'P_ClavePrivada'];

            //Excluye los objetos de la Fiel en esta validación
            if (TiposDeObjeto.indexOf(Type) >= 0 && NoPermitidos.indexOf(elemento.id) < 0) {
                var Alerta = "#Alerta_" + elemento.id;
                var Mensaje = "#Mensaje_" + elemento.id;

                //Valida que los objetos tengan algún valor 
                if ($(Alerta).length != 0 && (elemento.value == null || elemento.value == "")) {
                    if (PrimerError == "True") {
                        $("#divAlertaFormulario").css('display', 'block');
                        this.focus(); PrimerError = "False";
                    }
                    MensajeDeSalida = "Este campo es obligatorio";
                    ErrorFormulario(elemento, MensajeDeSalida, event);
                    cuenta_errores++;
                }
                else {
                    AceptarElemento(item);

                    //Valida el formato de los objetos
                    if (Alerta.toLowerCase().indexOf("_correo") >= 0) { if (!ValidarEmail(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                    if (Alerta.toLowerCase().indexOf("u_nombre") >= 0) {
                        if (MinCaracteres(this, 6)) { if (!ValidarNombreUsuario(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                        else { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } }
                    }

                    if (Alerta.toLowerCase().indexOf("rl_nombre") >= 0) {
                        if (MinCaracteres(this, 1)) { if (!ValidarNombresComunes(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                        else { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } }
                    }

                    if (Alerta.toLowerCase().indexOf("p_nombreproyecto") >= 0) {
                        if (MinCaracteres(this, 1)) { if (!ValidarNombresComunes(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                        else { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } }
                    }
                    if (Alerta.toLowerCase().indexOf("e_razonsocial") >= 0) {
                        if (MinCaracteres(this, 1)) { if (!ValidarRazonSocial(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                        else { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } }
                    }
                    if (Alerta.toLowerCase().indexOf("p_unidades") >= 0) {
                        if (MinCaracteres(this, 1)) { if (!ValidarNumeroUnidades(elemento, event)) { cuenta_errores++; } }
                    }

                    if (Alerta.toLowerCase().indexOf("e_rfc") >= 0) { if (!ValidarRFCEmpresa(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                    if (Alerta.toLowerCase().indexOf("rl_rfc") >= 0) {
                        if (!ValidarRFCPersona(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } }
                    }
                    if (Alerta.toLowerCase().indexOf("_password") >= 0) { if (!ValidarContraseña(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                    if (Alerta.toLowerCase().indexOf("factor") >= 0) { if (!ValidarFactorPlanta(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                    if (Alerta.toLowerCase().indexOf("capacidad") >= 0) { if (!ValidarCapacidad(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                    if (Alerta.toLowerCase().indexOf("latitud") >= 0) { if (!ValidarCoordenadasLat(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                    if (Alerta.toLowerCase().indexOf("longitud") >= 0) { if (!ValidarCoordenadasLong(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                    if (Alerta.toLowerCase().indexOf("numeroexterior") >= 0) { if (!ValidarNumeroExterior(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                    if (Alerta.toLowerCase().indexOf("numerointerior") >= 0) { if (!ValidarNumeroInterior(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                    if (Alerta.toLowerCase().indexOf("_lada") >= 0) {
                        if (MinCaracteres(this, 2)) { if (!ValidarLADA(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                        else { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } }
                    }
                    if (Alerta.toLowerCase().indexOf("fijo") >= 0) {
                        if (MinCaracteres(this, 8)) { if (!ValidarTelefonoFijo(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                        else { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } }
                    }
                    if (Alerta.toLowerCase().indexOf("movil") >= 0) {
                        if (MinCaracteres(this, 10)) { if (!ValidarTelefonoMovil(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                        else { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } }
                    }
                    if (Alerta.toLowerCase().indexOf("postal") >= 0) {
                        if (MinCaracteres(this, 0)) { if (!ValidarCPFormulario(elemento, event)) { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } } }
                        else { cuenta_errores++; if (PrimerError == "True") { this.focus(); PrimerError = "False"; $("#divAlertaFormulario").css('display', 'block'); } }
                    }

                    

                    if (Type == 'file' && $(Alerta).length != 0) {
                        if (Alerta.toLowerCase().indexOf("file") < 0) {
                            if (!ValidarArchivos(elemento, event)) {
                                cuenta_errores++;
                                if (PrimerError == "True") {
                                    this.focus(); PrimerError = "False";
                                    $("#divAlertaFormulario").css('display', 'block');
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (error) {
        }

    });

    //Obtener dato de capacidad instalada en MW de proyecto.
    try {
       
        if ($("#P_CapacidadInstalada").val() <= 10) {
            //alert("mAYOR A 10 MW Peña");
            $("#Preguntas td:contains('MW')").each(
                function () {
                    //alert('Encontrada Peñabot');
                    $(this).parent().find('select option[value="1"]').prop("selected", true);
                    //alert($(this).parent().find('select option:selected').text());
                }
                );
        }
        else
        {
            $("#Preguntas td:contains('MW')").each(
                function () {
                    //alert('Encontrada Peñabot');
                    $(this).parent().find('select option[value="0"]').prop("selected", true);
                    //alert($(this).parent().find('select option:selected').text());
                }
                );
        }
    }
    catch (ex) { }

    if (cuenta_errores > 0)
    { return false; }
    else
    { return true }
}

function ValidarFormularioTecnologia(event, item) {
    wevent = event || window.event || event.srcElement;

    var cuenta_errores = 0;
    var Formulario = "#" + $(item).attr("id");
    var MensajeDeSalida = "";

    //Recorre elementos del formulario
    $(Formulario).find(':input').each(function () {
        try {
            var elemento = this;
            var Type = elemento.type;
            var TiposDeObjeto = ['text', 'textarea', 'select-one', 'number', 'password', 'file'];
            var NoPermitidos = ['file1', 'file2', 'E_ClavePrivada', 'RL_ClavePrivada', 'U_ClavePrivada', 'P_ClavePrivada'];

            //Excluye los objetos de la Fiel en esta validación
            if (TiposDeObjeto.indexOf(Type) >= 0 && NoPermitidos.indexOf(elemento.id) < 0) {
                var Alerta = "#Alerta_" + elemento.id;
                var Mensaje = "#Mensaje_" + elemento.id;

                //Valida que los objetos tengan algún valor 
                if ($(Alerta).length != 0 && (elemento.value == null || elemento.value == "")) {

                    MensajeDeSalida = "Este campo es obligatorio";
                    ErrorFormulario(elemento, MensajeDeSalida, event);
                    cuenta_errores++;
                }
                else {
                    AceptarElemento(item);

                    //Valida el formato de los objetos
                    if (Type == 'file' && $(Alerta).length != 0) {
                        if (Alerta.toLowerCase().indexOf("file") < 0) {
                            if (!ValidarImagenes(elemento, event)) { cuenta_errores++; }
                        }
                    }
                }
            }
        }
        catch (error) {
        }

    });
    if (cuenta_errores > 0)
    { return false; }
    else
    { return true }
}

function ValidarFIEL(event, item) {

    wevent = event || window.event || event.srcElement;
    var Formulario = "#" + $(item).attr("id");
    var MensajeDeSalida = "";
    var cuenta_errores = 0;

    //Recorre elementos del formulario
    $(Formulario).find(':input').each(function () {
        var elemento = this;
        var Type = elemento.type;

        var Alerta = "#Alerta_" + elemento.id;
        var Mensaje = "#Mensaje_" + elemento.id;

        AceptarElemento(elemento);
        var archivoValido;

        //Valida contraseña nula
        if (elemento.id == 'RL_ClavePrivada' && (elemento.value == null || elemento.value == "")) {
            MensajeDeSalida = "Este campo es obligatorio";
            $('#divAlertaErrorFiel').css('display', 'block');
            ErrorFormulario(elemento, MensajeDeSalida, event);
            cuenta_errores++;
        }


        //"Valida .cer y .key
        if (elemento.id == 'file1' || elemento.id == 'file2') {
            var resultado = ValidarArchivoFiel2(elemento, event);
            if (!resultado) {
                $('#divAlertaErrorFiel').css('display', 'block');
                cuenta_errores++;
            }
        }
    });

    if (cuenta_errores > 0) {
        $('#EnviarFormulario').attr('disabled', false);
        cuenta_errores == 0;
        return false;
    }
    else {
        cuenta_errores == 0;
        return true;
    }
}

function ErrorFormulario(elemento, MensajeError, event) {
    try {
        var Alerta = "#Alerta_" + $(elemento).attr("id");
        var Mensaje = "#Mensaje_" + $(elemento).attr("id");
        var Item = "#" + $(elemento).attr("id");
        if (event != null) {
            event.preventDefault ? event.preventDefault() : (event.returnValue = false);
        }

        $(Alerta).addClass("form-text form-text-error");
        $(Alerta).css("color", "#D0021B");
        if ($(elemento).val().length == 0 || $(elemento).val() == null) {
            if ($(Item).attr('type') != "file") {
                $(Mensaje).text('Este campo es obligatorio');
            }
            else {
                $(Mensaje).text(MensajeError);
            }

        }
        else {
            $(Mensaje).text(MensajeError);
        }

        $(Mensaje).css("form-text form-text-error");
        $(elemento).addClass("form-control-error");//  css("form-control form-control-error");
    }
    catch (error) {
    }

}

function AceptarElemento(elemento) {
    var Alerta = "#Alerta_" + $(elemento).attr("id");
    var Mensaje = "#Mensaje_" + $(elemento).attr("id");
    $(Alerta).removeClass("form-text-error");
    $(Alerta).css("color", "#545454");
    $(Mensaje).text("");
    $(Mensaje).css("form-text");
    $(elemento).removeClass("form-control-error");
}

// ------------------------ VALIDACIONES PARTICULARES .----------------------- //

function ValidarRFCEmpresa(item, event) {
    if ($(item).val().length == 12) {
        item.value = (item.value.trim()).toUpperCase();

        if (/^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))/.test(item.value)) {
            AceptarElemento(item);
            return (true);
        }
        else {
            ErrorFormulario(item, "Formato incorrecto.", event);
            return (false);
        }
    }
    else if ($(item).val().length == 13) {
        return ValidarRFCPersona(item, event);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }

}

function ValidarRFCPersona(item, event) {

    item.value = (item.value.trim()).toUpperCase();

    if (/^(([A-Z]|[a-z]|\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))/.test(item.value)) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
}

function ValidarNombresComunes(item, event) {
    item.value = (item.value.trim()).toUpperCase();

    //if (/^[A-Za-z ñÑáéíóúÁÉÍÓÚ.0-9]$/.test(item.value))
    if (!item.value.search(/^[a-zA-Z0-9 ñÑáéíóúÁÉÍÓÚ.0-9]*$/)) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
}

function ValidarRazonSocial(item, event) {
    item.value = (item.value.trim()).toUpperCase();

    //if (/^[A-Za-z ñÑáéíóúÁÉÍÓÚ.0-9]$/.test(item.value))
    if (!item.value.search(/^[a-zA-Z0-9 ,ñÑáéíóúÁÉÍÓÚ.0-9]*$/)) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
}

function ValidarNombresEstrictamente(item, event) {
    item.value = (item.value.trim()).toUpperCase();
    //if (/^[A-Za-z ñÑáéíóúÁÉÍÓÚ]$/.test(item.value))
    if (!item.value.search(/^(?=.*[A-Z])([a-zA-ZñÑáéíóúÁÉÍÓÚ\s]){1,50}$/)) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
}

function ValidarNombreUsuario(item, event) {
    item.value = (item.value.trim()).toUpperCase();
    //var patron = ;
    // En caso de querer validar cadenas con espacios usar: /^[a-zA-Z\s]*$/
    if (!item.value.search(/^[a-zA-Z0-9]*$/)) {
        AceptarElemento(item);
        return (true);
    }

    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
}

function ValidarCURP(item, event) {
    item.value = (item.value.trim()).toUpperCase();
    if (/^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$/.test(item.value)) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
}

function ValidarEmail(item, event) {
    item.value = (item.value.trim()).toUpperCase();
    if (/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}/.test(item.value)) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
}

function ValidarContraseña(item, event) {
    item.value = item.value.trim();
    if (!item.value.search(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)([A-Za-z\d$@$!%*?&-]|[^ ]){6,30}$/)) {
        AceptarElemento(item);
        return (true);
    }

    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }

}

function ValidarArchivos(item, event) {

    var sizebyte = item.files[0].size;
    sizebyte = (sizebyte / 1048576);

    var fileExtension = ['pdf'];
    var extension = item.files[0].name.split('.').pop().toLowerCase();
    var contenttype = "";
    var fileReader = new FileReader();

    fileReader.onloadend = function (e) {
        var arr = (new Uint8Array(e.target.result)).subarray(0, 4);
        for (var i = 0; i < arr.length; i++) {
            contenttype += arr[i].toString(16);
        }

        var MensajeDeSalida = "El archivo no es de tipo PDF o su tamaño es mayor al permitido (5 MB)";
        if (sizebyte > 5 || extension != "pdf" || contenttype != "25504446") {
            item.value = "";
            ErrorFormulario(item, MensajeDeSalida, event);
            return false;
        }
        else {
            AceptarElemento(item);
            return true;
        }
    };

    fileReader.readAsArrayBuffer(item.files[0]);
    if (sizebyte > 5 || extension != "pdf") {
        return false;
    }
    else {
        return true;
    }

}

function ValidarImagenes(item, event) {

    var sizebyte = item.files[0].size;
    sizebyte = (sizebyte / 1048576);

    var fileExtension = ['jpg', 'png'];
    var extension = item.files[0].name.split('.').pop().toLowerCase();

    var MensajeDeSalida = "El archivo debe ser menor a 5 MB en formato JPEG o PNEG";

    if (sizebyte <= 5 && (extension == "jpg" || extension == "png")) {
        AceptarElemento(item);
        return (true);

    }
    else {
        item.value = "";
        ErrorFormulario(item, MensajeDeSalida, event);
        return (false);
    }
}

function ValidarArchivoFiel(item, event) {

    var sizebyte = item.files[0].size;
    sizebyte = (sizebyte / 1048576);
    var extension = item.files[0].name.split('.').pop().toLowerCase();
    var contenttype = "";
    var fileReader = new FileReader();

    fileReader.onloadend = function (e) {
        var arr = (new Uint8Array(e.target.result)).subarray(0, 4);
        for (var i = 0; i < arr.length; i++) {
            contenttype += arr[i].toString(16);
        }
        var MensajeDeSalida = "Por favor selecciona archivos con extensión CER o KEY menores a 5 MB(megas)";

        if (item.id == 'file1') {
            if (extension.toLowerCase().indexOf("cer") != 0 || contenttype.substring(0, 4) != "3082") {
                MensajeDeSalida = "Por favor selecciona archivos con extensión 'cer'";
                item.value = "";
                ErrorFormulario(item, MensajeDeSalida, event);
                return false;
            }
            else {
                AceptarElemento(item);
                return true;
            }
        }
        else if (item.id == 'file2') {
            if (extension.toLowerCase().indexOf("key") != 0 || contenttype.substring(0, 4) != "3082") {
                item.value = "";
                MensajeDeSalida = "Por favor selecciona archivos con extensión 'key'";
                ErrorFormulario(item, MensajeDeSalida, event);
                return false;
            }
            else {
                AceptarElemento(item);
                return true;
            }
        }
    };

    fileReader.readAsArrayBuffer(item.files[0]);
}

function ValidarArchivoFiel2(item, event) {

    if (item.files[0] != null) {
        var sizebyte = item.files[0].size;
        sizebyte = (sizebyte / 1048576);
        var extension = item.files[0].name.split('.').pop().toLowerCase();
        var contenttype = "";
        var fileReader = new FileReader();
        var MensajeDeSalida = "Por favor selecciona archivos con extensión CER o KEY menores a 5 MB(megas)";

        if (item.id == 'file1') {
            if (extension.toLowerCase().indexOf("cer") != 0) {
                MensajeDeSalida = "Por favor selecciona archivos con extensión 'cer'";
                item.value = "";
                ErrorFormulario(item, MensajeDeSalida, event);
                return false;
            }
            else {
                AceptarElemento(item);
                return true;
            }
        }
        else if (item.id == 'file2') {
            if (extension.toLowerCase().indexOf("key") != 0) {
                MensajeDeSalida = "Por favor selecciona archivos con extensión 'key'";
                item.value = "";
                ErrorFormulario(item, MensajeDeSalida, event);
                return false;
            }
            else {
                AceptarElemento(item);
                return true;
            }
        }
    }
    else {
        MensajeDeSalida = "Por favor selecciona un archivo";
        item.value = "";
        ErrorFormulario(item, MensajeDeSalida, event);
        return false;
    }



}

function ValidarFactorPlanta(item, event) {
    if (item.value > 1 || item.value < 0 || item.value == "" || item.value == null) {
        item.value = "";
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
    else {
        AceptarElemento(item);
        return (true);
    }
}

function ValidarCapacidad(item, event) {
    if (item.value < 0.5 || item.value > 1000 || item.value == "" || item.value == null) {
        //item.value = "";
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
    else {
        AceptarElemento(item);
        return (true);
    }
}

function ValidarCoordenadasLat(item, event) {
    //if (/^-{0,1}\d\.{0,1}\d+$/.test(item.value))
    if (!item.value.search(/^[0-9-.]*$/) && item.value >= 14.4 && item.value <= 32.7) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }

}

function ValidarCoordenadasLong(item, event) {
    //if (/^-{0,1}\d\.{0,1}\d+$/.test(item.value))
    if (!item.value.search(/^[0-9-.]*$/) && item.value >= -117 && item.value <= -86.88) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }

}

function ValidarObjeto(item, event) {
    wevent = event || window.event || event.srcElement;
    var elementocambiante = $(item).attr("id");


    ActualizarElementosFormulario(elementocambiante);

    var Alerta = "#Alerta_" + $(item).attr("id");
    var Mensaje = "#Mensaje_" + $(item).attr("id");

    if ($(Alerta).length != 0 && ($(item).val() == null || $(item).val() == "")) {
        MensajeDeSalida = "Este campo es obligatorio.";
        ErrorFormulario(item, MensajeDeSalida, event);
    }
    else {
        AceptarElemento(item);
    }



};

function ValidarNumeros(item, event) {
    if ((event.keyCode < 37 || event.keyCode > 40) && event.keyCode != 8 && event.keyCode != 46) {
        item.value = item.value.replace(/[^0-9]/g, '');
    }
}

function ValidarNumeroExterior(item, event) {
    item.value = (item.value.trim()).toUpperCase();


    if (item.value == "SN" || !item.value.search(/^[0-9]{0,5}\s[A-Za-z]{0,5}$/) || ((Number(item.value) > 0) && (Number(item.value) != "NaN"))) //Cambios realizados el 30/03/17
    {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }

    //if (!item.value.search(/^[0-9]*$/)) (!item.value.search(/^[a-z A-Z 0-9]{1}[a-z A-Z 0-9]?/g))
    //{
    //    AceptarElemento(item);
    //    return (true);
    //}

}
function ValidarNumeroUnidades(item, event) {



    if (!item.value.search(/^[0-9]*$/)) //Cambios realizados el 30/03/17
    {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }



}


function ValidarNumeroInterior(item, event) { //Cambio realizado el 17 de marzo de 2017.
    item.value = (item.value.trim()).toUpperCase();

    if (item.value == "" || item.value == "SN" || !item.value.search(/^[0-9A-Za-z]{0,6}$/) || ((Number(item.value) > 0) && (Number(item.value) != "NaN"))) { //Cambios realizados el 30/03/17
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }
}

function ValidarNumerosDecimales(item, event) {
    if ((event.keyCode < 37 || event.keyCode > 40) && event.keyCode != 8 && event.keyCode != 46) {
        item.value = item.value.replace(/[^0-9.+-]/g, '');
    }
}

function ValidarCP(item, event) {
    if ($(item).val().length > 0) {
        if ($(item).val().length == 5) {
            var elementocambiante = $(item).attr("id");
            ActualizarElementosFormulario(elementocambiante);
            AceptarElemento(item);
            return (true);
        }
        else {
            ErrorFormulario(item, "Formato incorrecto.", event);
            return (false);
        }

    }
    else {
        var elementocambiante = $(item).attr("id");
        ActualizarElementosFormulario(elementocambiante);
        AceptarElemento(item);
        return (true);
    }

}

function ValidarLADA(item, event) {
    if ($(item).val().length == 2) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }

}

function ValidarTelefonoFijo(item, event) {
    if ($(item).val().length == 8) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }

}

function ValidarTelefonoMovil(item, event) {
    if ($(item).val().length == 10) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }

}

function ValidarCPFormulario(item, event) {
    if ($(item).val().length == 0 || $(item).val().length == 5) {
        AceptarElemento(item);
        return (true);
    }
    else {
        ErrorFormulario(item, "Formato incorrecto.", event);
        return (false);
    }

}

function MinCaracteres(item, valor) {

    if (/^\s|\s$/g.test(item.value)) {
    }
    var vals = item.value;
    var dos = vals.replace(/^\s|\s$/g, '');
    item.value = dos;

    var Item = $(item).val();
    var Min = Item.length;

    //Agregar validación para número exterior
    if (Min < valor) {
        if (valor == 1) {
            ErrorFormulario(item, "Formato incorrecto", event);
            return (false);
        }
        {
            ErrorFormulario(item, "Formato incorrecto", event);
            return (false);
        }
    }
    else {
        AceptarElemento(item);
        return (true);
    }
}

function AutorizacionInformacionSeleccionada() {

    if (document.getElementById('CompartirDatos').value > 0 && document.getElementById('LeerCompartirDatos').checked) {
        document.getElementById('BtnEnviarRegistro').style.visibility = 'visible';
    }
    else {
        document.getElementById('BtnEnviarRegistro').style.visibility = 'hidden';
    }
}

function ObtenerTipoArchivo(item) {
    var header = "";
    var fileReader = new FileReader();
    fileReader.onloadend = function (e) {
        var arr = (new Uint8Array(e.target.result)).subarray(0, 4);
        //var header = "";
        for (var i = 0; i < arr.length; i++) {
            header += arr[i].toString(16);
        }

    };
    fileReader.readAsArrayBuffer(item.files[0]);
    return header;

}

// ------------------------ NUEVAS VALIDACIONES .----------------------- //

function ValidarSimilitudContraseñas(ObjetoContraseña1, ObjetoContraseña2) {
    $("#divAlerta").css('display', 'none');
    $("#divAlertaFormulario").css('display', 'none');
    $("#divAlertaCoincidencia").css('display', 'none');

    if (($(ObjetoContraseña1).val()) == ($(ObjetoContraseña2).val())) {
        var expresion = "/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)([A-Za-z\d$@$!%*?&-]|[^ ]){6,30}$/";
        var texto1 = $(ObjetoContraseña1).val();
        if (!texto1.search(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)([A-Za-z\d$@$!%*?&-]|[^ ]){6,30}$/)) {
            ConfirmarCambioPassword();
        }
        else {
            ErrorFormulario($('#ActualizarContraseña'), "Formato incorrecto.", event);
            CerrarConfirmacion();
        }

    }
    else {
        $("#divAlertaCoincidencia").css('display', 'block');
        return false;
    }
}

function ValidarNumerosTelefono(item, event) {
    if ((event.keyCode < 37 || event.keyCode > 40) && event.keyCode != 8 && event.keyCode != 46) {
        item.value = item.value.replace(/[^0-9]/g, '');
    }
}






