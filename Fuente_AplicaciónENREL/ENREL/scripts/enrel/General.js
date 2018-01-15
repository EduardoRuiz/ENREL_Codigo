

//--------------------------------------------HABILITAR TRÁMITE----------------------------------------------

function EnviarTramite(urltramite, IdGlobalMacroTramite, RFCRepresentante, Homoclave, IdProyecto) 
{
    if (urltramite == null || urltramite == "") {
        urltramite = "http://www.gob.mx/cntse-rfts/buscar"
    }

    NotificarEnvioTramite(Homoclave, IdProyecto, RFCRepresentante);

    window.open(urltramite, '_blank');

}

function NotificarEnvioTramite(Homoclave, IdProyecto, RFCRepresentante)
{
    try {
        $.ajax
            (
                {
                    url: "/Proyectos/NotificarEnvioTramite",
                    dataType: "json",
                    type: "GET",
                    contentType: 'application/json; charset=utf-8',
                    data: { HomoclaveEnviada: Homoclave, ProyectoEnviado: IdProyecto, RFCRL: RFCRepresentante },
                    success: function (data) {
                      
                    },
                    error: function () {
                        return false;
                    }
                }
            );
    }
    catch (error) {
        return false;
    }

}

//------------------------------------------REGISTRO---------------------------------------------------------

function Registro_RegresarAEmpresa() 
{
    $('#Registro_DatosRepresentantes').attr('action', '/RegistrosInversionista/RegresarDatosEmpresa');
    $('#Registro_DatosRepresentantes').attr('onsubmit', 'return true');
    $('#Registro_DatosRepresentantes').submit();
}

function Registro_RegresarARepresentante() 
{
    $('#Registro_DatosUsuario').attr('action', '/RegistrosInversionista/RegresarDatosRepresentante');
    $('#Registro_DatosUsuario').attr('onsubmit', 'return true');
    $('#Registro_DatosUsuario').submit();
}

function Registro_RegresarAUsuario() 
{
    $('#Registro_DatosAcreditacion').attr('action', '/RegistrosInversionista/RegresarDatosUsuario');
    $('#Registro_DatosAcreditacion').attr('onsubmit', 'return true');
    $('#Registro_DatosAcreditacion').submit();
}

function CambiarColorAlerta(TipoAlerta) 
{

    $("#divAlerta").removeClass();

    if (TipoAlerta == "Error") {
        $("#divAlerta").addClass("alert alert-danger alert-dismissible");
    }
    if (TipoAlerta == "Correcto") {
        $("#divAlerta").addClass("alert alert-success alert-dismissible");
    }
    if (TipoAlerta == "Precaución") {
        $("#divAlerta").addClass("alert alert-warning alert-dismissible");
    }

}

function CrearTramiteEnDiagrama(Homoclave, color, px, py) 
{
    debugger;
    var color = "#" + color;

    tramites[Homoclave] = new joint.shapes.basic.Rect({
        position: { x: px, y: py },
        size: { width: 110, height: 30 },
        attrs: { rect: { fill: color }, text: { text: Homoclave, fill: 'white', 'font-size': 11 } },
        id: Homoclave
    });


    graph.addCells([tramites[Homoclave]])

}

//----------------------------------- DIGITAL ANALYTICS Y ENCUESTA -----------------------------------------
function udm_(e) 
{
    var t = "comScore=", n = document, r = n.cookie, i = "", s = "indexOf", o = "substring", u = "length", a = 2048, f, l = "&ns_", c = "&", h, p, d, v, m = window, g = m.encodeURIComponent || escape; if (r[s](t) + 1) for (d = 0, p = r.split(";"), v = p[u]; d < v; d++) h = p[d][s](t), h + 1 && (i = c + unescape(p[d][o](h + t[u]))); e += l + "_t=" + +(new Date) + l + "c=" + (n.characterSet || n.defaultCharset || "") + "&c8=" + g(n.title) + i + "&c7=" + g(n.URL) + "&c9=" + g(n.referrer), e[u] > a && e[s](c) > 0 && (f = e[o](0, a - 8).lastIndexOf(c), e = (e[o](0, f) + l + "cut=" + g(e[o](f + 1)))[o](0, a)), n.images ? (h = new Image, m.ns_p || (ns_p = h), h.src = e) : n.write("<", "p", "><", 'img src="', e, '" height="1" width="1" alt="*"', "><", "/p", ">")
};

function uid_call(a, b) 
{
    ui_c2 = 17183199; // your corporate c2 client value
    ui_ns_site = 'gobmx'; // your sites identifier
    window.b_ui_event = window.c_ui_event != null ? window.c_ui_event : "", window.c_ui_event = a;
    var ui_pixel_url = 'https://sb.scorecardresearch.com/p?c1=2&c2=' + ui_c2 + '&ns_site=' + ui_ns_site + '&name=' + a + '&ns_type=hidden&type=hidden&ns_ui_type=' + b;
    var b = "comScore=", c = document, d = c.cookie, e = "", f = "indexOf", g = "substring", h = "length", i = 2048, j, k = "&ns_", l = "&", m, n, o, p, q = window, r = q.encodeURIComponent || escape; if (d[f](b) + 1) for (o = 0, n = d.split(";"), p = n[h]; o < p; o++) m = n[o][f](b), m + 1 && (e = l + unescape(n[o][g](m + b[h]))); ui_pixel_url += k + "_t=" + +(new Date) + k + "c=" + (c.characterSet || c.defaultCharset || "") + "&c8=" + r(c.title) + e + "&c7=" + r(c.URL) + "&c9=" + r(c.referrer) + "&b_ui_event=" + b_ui_event + "&c_ui_event=" + c_ui_event, ui_pixel_url[h] > i && ui_pixel_url[f](l) > 0 && (j = ui_pixel_url[g](0, i - 8).lastIndexOf(l), ui_pixel_url = (ui_pixel_url[g](0, j) + k + "cut=" + r(ui_pixel_url[g](j + 1)))[g](0, i)), c.images ? (m = new Image, q.ns_p || (ns_p = m), m.src = ui_pixel_url) : c.write("<p><img src='", ui_pixel_url, "' height='1' width='1' alt='*'></p>");
}

function getDescargaDocumento(CodigoTramite) {
    /*Código del trámite    */
    var Codigo = CodigoTramite.substring(0, 11);
    getEncuestaHC(Codigo);  // Llamada a la encuesta 
}

//-------------------------------------------AUXILIARES GENERALES-------------------------------------------

function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}

function FormatoDeDatosNumericos() {

    var elements = document.getElementsByTagName("input");
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].type == "text") {
            if (elements[i].id.toLowerCase().indexOf("_lada") >= 0) { if (elements[i].value.length > 0) { elements[i].value = pad(elements[i].value, 2); } }
            if (elements[i].id.toLowerCase().indexOf("fijo") >= 0) { if (elements[i].value.length > 0) { elements[i].value = pad(elements[i].value, 8); } }
            if (elements[i].id.toLowerCase().indexOf("celular") >= 0) { if (elements[i].value.length > 0) { elements[i].value = pad(elements[i].value, 10); } }
            if (elements[i].id.toLowerCase().indexOf("postal") >= 0) { if (elements[i].value.length > 0) { elements[i].value = pad(elements[i].value, 5); } }
        }
    }
}

function ResetClave() {
    $("#P_ClavePrivada").value = "";
    $("#RL_ClavePrivada").value = "";
    $("#E_ClavePrivada").value = "";
    $("#U_ClavePrivada").value = "";
}

function ConfirmLeave() {
    $.ajax
    (
        {
            url: "/Home/Logout",
            dataType: "json",
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            data: {},
            success: function (data) {
               
            },
            error: function () {
                return false;
            }
        }
    );
}

function AjustarMargenPagina(NombreDePagina) 
{

    var HgAlturaMenu = $("#MenuEnrel").outerHeight();
    if (NombreDePagina == "Inicio")
    {
        var HgAlturaMenu = HgAlturaMenu + 41;
        $("#ImagenInicio").css({ 'margin-top': HgAlturaMenu + "px" });
    }
    else
    {
        var HgAlturaMenu = HgAlturaMenu - 41;
        $("#Contenido").css({ 'margin-top': HgAlturaMenu + "px" });
    }

    $("#E_Lada").keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });

}

function Iniciar(Titulo) {
    var index = window.location.href.lastIndexOf("/") + 1;
    var Pagina = window.location.href.substr(index);

    //Limpieza de página
    try {
        FormatoDeDatosNumericos();
        $("#P_ClavePrivada").val("");
        $("#RL_ClavePrivada").val("");
        $("#R_ClavePrivada").val("");
        $("#U_ClavePrivada").val("");
        AutorizacionInformacionSeleccionada();

    }
    catch (ex) {

    }

    //Opciones de salida
    try {
        $(window).on('mouseover', (function () { window.onbeforeunload = null; }));

        $(window).on('mouseout', (function () { window.onbeforeunload = ConfirmLeave; }));

        $(window).on('onkeydown ',
            (
                function SalirPorTeclado(evt) {
                    if (!evt) evt = event;
                    //Alt + f4
                    if (evt.altKey && evt.keyCode == 115) {
                        return false;
                    }
                }
            )
        );

        $(document).keydown(function (e) {
            if (e.key == "F5") {

            }
            if (e.key == "F4") {
                window.onbeforeunload = ConfirmLeave;
            }
            if (e.keyCode == "13") {
                e.preventDefault();
                return false;
            }
        });

    }
    catch (ex) {

    }

    //Páginas
    try {

    }
    catch (ex) {

    }

}

function CrearTramiteEnDiagrama(Homoclave, color, px, py, urlrequisitos, requerido) 
{
    try 
    {
        var color = "#" + color;
        var TextoTramite = "";
        var ColorLinea = "#000000";
        var LineaPunteada = "0,0";
        if (requerido == "False") 
        {
            TextoTramite = TextoTramite + Homoclave;
            ColorLinea = "#0000FF";
            LineaPunteada = "10,2";
        }
        else 
        {
            TextoTramite = Homoclave;
        }

        var posicionx = px - 25;
        var posiciony = py;
        var posicionxText = posicionx / 2;
        var posicionyText = posiciony / 2;

        tramites[Homoclave] = new joint.shapes.basic.Rect({
            markup: "<g class='rotatable'><g class='scalable'><rect/></g><a href='" + urlrequisitos + "'><text/></a></g>",
            position: { x: posicionx, y: posiciony },
            size: { width: 1, height: 1 },

            attrs: {
                '.label': { text: 'Any text here', 'text-anchor': 'end' },
                rect: {
                    fill: {
                        type: 'linearGradient',
                        stops: [
                            { offset: '53%', color: '#ffffff' },
                            { offset: '47%', color: color }
                        ],
                        attrs: { x1: '0%', y1: '0%', x2: '0%', y2: '100%' }
                    },
                    width: '147px',
                    height: '50px',
                    stroke: ColorLinea,
                    'stroke-width': 1.6,
                    rx: 5, ry: 5
                },
                text: {
                    text: TextoTramite,
                    fill: 'black',
                    'text-anchor': 'middle',
                    'font-size': 16,
                    'font-weight': 'bold',
                    'y-alignment': 'middle',
                    'font-family': 'Open sans, helvetica, sans-serif',
                    y: '1em',
                    x: '1em',
                    transform: 'matrix(1,0,0,1,' + 57 + ',' + 9 + ')',
                },
                a: { href: urlrequisitos, cursor: 'pointer', target: '_blank' }
            }
            , id: Homoclave
        });
        graph.addCells([tramites[Homoclave]]);

        //-------------------



    }
    catch (error) {

    }


}


// -------------------------------------------INICIAR--------------------------------------------------------

$gmx(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    AjustarMargenPagina('@ViewBag.Title');
    var ValorTipoAlerta = $('#TipoAlerta').val();
    CambiarColorAlerta(ValorTipoAlerta);

    Iniciar();

    window.location.hash = "no-back-button";
    window.location.hash = "Again-No-back-button" //chrome
    window.onhashchange = function () { window.location.hash = "no-back-button"; }

});



function validarcapacidad(){

    if ($('#P_CapacidadInstalada').val() < 10)
    {

    }


}
