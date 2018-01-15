function EjecutarReporteProyectosTramites(event, item)
{
    try
    {
        $.ajax({
            url: '/Consultor/VistaReporte_ProyectosTramites',
            type: 'POST',
            data: $('#ConsultarProyectos').serialize(),
            dataType: 'html'
        })
        .success(function(result)
        {
            $('#VistaParcial').html(result);
        })
        .error(function (xhr, status)
        {
        }
        )
    }
    catch(error)
    {
    }
    
}

function ExportarInformacionProyectos(ListaProyectos, TipoArchivo) {
    try {
        $.ajax({
            url: '/ReporteProyectos/ExportarXML',
            contentType: "application/json",
            async: true,
            type: "POST",
            data: JSON.stringify(ListaProyectos),
            error: function (jqXHR, textStatus, errorThrown) {
         //       console.log("FAIL: " + errorThrown);
            },
            success: function (data) {
            }
        });
    }
    catch (error) {
    }

}