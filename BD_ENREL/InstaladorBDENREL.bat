@echo off
echo Instalar base de datos ENREL
sqlcmd -i BDENREL_createdatabase.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Tablas y Procedimientos
sqlcmd -i BDENREL_tablasyprocedimientos.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Entidades Federativas
sqlcmd -i BDENREL_entidadesfederativas.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Municipios
sqlcmd -i BDENREL_municipios.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Localidades 1
sqlcmd -i BDENREL_localidades1.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Localidades 2
sqlcmd -i BDENREL_localidades2.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Localidades 3
sqlcmd -i BDENREL_localidades3.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Codigos Postales
sqlcmd -i BDENREL_codigospostales.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Tipos de colonias
sqlcmd -i BDENREL_tiposdeasentamientos.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Tipos de vialidad
sqlcmd -i BDENREL_tiposdevialidad.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Código Postal por Entidad Federativa
sqlcmd -i BDENREL_entidadesfederativas_cp.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Estatus de proyectos
sqlcmd -i BDENREL_estatusproyecto.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Estatus de tramites
sqlcmd -i BDENREL_estatustramite.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Tipos de usuario
sqlcmd -i BDENREL_tiposdeusuario.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Permisos
sqlcmd -i BDENREL_permisos.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Permisos por Tipo de Usuario
sqlcmd -i BDENREL_tiposdeusuario_permisos.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Tecnologias
sqlcmd -i BDENREL_tecnologias.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2


echo Cargar Tramites
sqlcmd -i BDENREL_tramites.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar TecnologiaTramites
sqlcmd -i BDENREL_tecnologiatramites.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar Coordenadas
sqlcmd -i BDENREL_coordenadas.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar TecnologiaPreguntas
sqlcmd -i bdENREL_tecnologiaspreguntas.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2

echo Cargar UsuariosDefault
sqlcmd -i BDENREL_usuariosdefault.sql -S SQL-APP\Ventanillaeneren -d master -U idom2 -P idom2



echo Listo
pause
exit