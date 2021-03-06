USE [BDENREL_QA]

GO
/****** Object:  User [inere]    Script Date: 16/06/2016 02:47:18 p.m. ******/
CREATE USER [inere] FOR LOGIN [inere] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [inere]
GO
/****** Object:  Table [dbo].[CatAPGV]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatAPGV](
	[IdAPGV] [int] IDENTITY(1,1) NOT NULL,
	[IdProyecto] [int] NOT NULL,
	[NombreRedElectrica] [varchar](200) NOT NULL,
	[DistanciaRedElectrica] [varchar](20) NOT NULL,
	[CaracteristicasRedElectrica] [varchar](200) NOT NULL,
	[NombreCarreteras] [varchar](400) NOT NULL,
	[DistanciaCarreteras] [varchar](20) NOT NULL,
	[CaracteristicasCarreteras] [varchar](200) NOT NULL,
	[NombreLocalidadUrbana] [varchar](200) NOT NULL,
	[DistanciaLocalidadUrbana] [varchar](20) NOT NULL,
	[CaracteristicasLocalidadUrbana] [varchar](200) NOT NULL,
	[NombreLocalidadRural] [varchar](200) NOT NULL,
	[DistanciaLocalidadRural] [varchar](20) NOT NULL,
	[CaracteristicasLocalidadRural] [varchar](200) NOT NULL,
	[NombreLocalidadIndigena] [varchar](200) NOT NULL,
	[DistanciaLocalidadIndigena] [varchar](20) NOT NULL,
	[CaracteristicasLocalidadIndigena] [varchar](200) NOT NULL,
	[NombreRiosPrincipales] [varchar](200) NOT NULL,
	[DistanciaRiosPrincipales] [varchar](20) NOT NULL,
	[CaracteristicasRiosPrincipales] [varchar](200) NOT NULL,
	[NombreVolcanesActivos] [varchar](200) NOT NULL,
	[DistanciaVolcanesActivos] [varchar](20) NOT NULL,
	[CaracteristicasVolcanesActivos] [varchar](200) NOT NULL,
	[NombreEstacionHidrometrica] [varchar](200) NOT NULL,
	[DistanciaEstacionHidrometrica] [varchar](20) NOT NULL,
	[FechaRegistro] [datetime] NULL,
	[CaracteristicasEstacionHidrometrica] [varchar](200) NOT NULL,
 CONSTRAINT [PK_CatAPGV] PRIMARY KEY CLUSTERED 
(
	[IdAPGV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatCodigosPostales]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatCodigosPostales](
	[IdCodigoPostal] [int] NOT NULL,
	[IdEntidadFederativa] [int] NOT NULL,
	[IdMunicipioINEGI] [int] NOT NULL,
	[IdLocalidadINEGI] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CatCoordenadas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatCoordenadas](
	[IdCoordenadas] [int] NOT NULL,
	[Fase] [int] NOT NULL,
	[Fila] [int] NOT NULL,
	[PosicionX] [int] NOT NULL,
	[PosicionY] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CatDiasInhabiles]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatDiasInhabiles](
	[IdDiaInhabil] [int] IDENTITY(1,1) NOT NULL,
	[Dia] [int] NOT NULL,
	[Mes] [int] NOT NULL,
	[Año] [int] NOT NULL,
	[FechaCompleta] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_CatDiasInhabiles] PRIMARY KEY CLUSTERED 
(
	[IdDiaInhabil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CatDocumentosDelProyecto]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatDocumentosDelProyecto](
	[IdDocumentosDeProyecto] [int] IDENTITY(1,1) NOT NULL,
	[IdProyecto] [int] NOT NULL,
	[UrlPlanoTerreno] [varchar](max) NULL,
	[UrlAPGV] [varchar](max) NULL,
	[UrlMIA] [varchar](max) NULL,
	[UrlAPI] [varchar](max) NULL,
	[UrlACUS] [varchar](max) NULL,
	[UrlExtras] [varchar](max) NULL,
 CONSTRAINT [PK_CatDocumentosDelProyecto] PRIMARY KEY CLUSTERED 
(
	[IdDocumentosDeProyecto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatEmpresas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatEmpresas](
	[IdEmpresa] [int] IDENTITY(1,1) NOT NULL,
	[NombreComercial] [varchar](50) NOT NULL,
	[RazonSocial] [varchar](50) NOT NULL,
	[RFC] [varchar](13) NOT NULL,
	[IdTipoCalle] [int] NOT NULL,
	[Calle] [varchar](100) NOT NULL,
	[NumeroExterior] [int] NOT NULL,
	[NumeroInterior] [varchar](50) NULL,
	[IdTipoColonia] [int] NOT NULL,
	[Colonia] [varchar](100) NOT NULL,
	[IdLocalidad] [int] NOT NULL,
	[CodigoPostal] [int] NULL,
	[Lada] [numeric](3, 0) NOT NULL,
	[TelefonoFijo] [numeric](10, 0) NOT NULL,
	[CorreoElectronico] [varchar](50) NOT NULL,
	[IdTipoCalleLateral1] [int] NULL,
	[CalleLateral1] [varchar](255) NULL,
	[IdTipoCalleLateral2] [int] NULL,
	[CalleLateral2] [varchar](255) NULL,
	[IdTipoCallePosterior] [int] NULL,
	[CallePosterior] [varchar](255) NULL,
	[DescripcionUbicacion] [varchar](255) NULL,
	[FechaRegistro] [datetime] NULL CONSTRAINT [DF_CatEmpresas_FechaRegistro]  DEFAULT (getdate()),
	[FechaSolicitud] [datetime] NULL,
	[FechaModificacion] [datetime] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_CatEmpresas] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CatEmpresas] UNIQUE NONCLUSTERED 
(
	[RFC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatEntidadesFederativas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatEntidadesFederativas](
	[IdEntidadFederativa] [int] NOT NULL,
	[EntidadFederativa] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CatEntidadesFederativas] PRIMARY KEY CLUSTERED 
(
	[IdEntidadFederativa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatEstatusProyecto]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatEstatusProyecto](
	[idEstatusProyecto] [int] NOT NULL,
	[EstatusProyecto] [varchar](20) NOT NULL,
 CONSTRAINT [PK_CatEstadosProyecto] PRIMARY KEY CLUSTERED 
(
	[idEstatusProyecto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatEstatusSolicitudRepresentante]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatEstatusSolicitudRepresentante](
	[IdEstatusSolicitudRepresentante] [int] NOT NULL,
	[EstatusSolicitudRepresentante] [varchar](15) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatEstatusTramite]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatEstatusTramite](
	[IdEstatusTramite] [int] NOT NULL,
	[Nombre] [varchar](15) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[Color] [varchar](50) NULL,
 CONSTRAINT [PK_CatEstadosTramite] PRIMARY KEY CLUSTERED 
(
	[IdEstatusTramite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatLocalidades]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatLocalidades](
	[IdLocalidad] [int] NOT NULL,
	[IdEntidadFederativa] [int] NOT NULL,
	[IdMunicipio] [int] NOT NULL,
	[IdLocalidadINEGI] [int] NOT NULL,
	[Localidad] [varchar](200) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatMunicipios]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatMunicipios](
	[IdMunicipio] [int] IDENTITY(1,1) NOT NULL,
	[IdEntidadFederativa] [int] NOT NULL,
	[IdMunicipioINEGI] [int] NOT NULL,
	[Municipio] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatPermisos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatPermisos](
	[IdPermiso] [int] NOT NULL,
	[Permiso] [varchar](50) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatProyectos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatProyectos](
	[IdProyecto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdEmpresa] [int] NOT NULL,
	[IdTecnologia] [int] NOT NULL,
	[IdLocalidad] [int] NOT NULL,
	[IdTipoColonia] [int] NULL,
	[Colonia] [varchar](100) NULL,
	[CodigoPostal] [int] NULL,
	[IdMunicipio] [int] NOT NULL,
	[Latitud] [float] NOT NULL,
	[Longitud] [float] NOT NULL,
	[CapacidadInstalada] [float] NOT NULL,
	[GeneracionAnual] [float] NOT NULL,
	[FactorPlanta] [float] NOT NULL,
	[MontoInversion] [float] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[IdGlobal] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
	[IdEstatusProyecto] [int] NOT NULL,
 CONSTRAINT [PK_CatProyectos] PRIMARY KEY CLUSTERED 
(
	[IdProyecto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatRecursos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatRecursos](
	[IdRecurso] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CatRecursos] PRIMARY KEY CLUSTERED 
(
	[IdRecurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatRepresentantesLegales]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatRepresentantesLegales](
	[IdRepresentanteLegal] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
	[PrimerApellido] [varchar](20) NOT NULL,
	[SegundoApellido] [varchar](20) NOT NULL,
	[CURP] [char](18) NOT NULL,
	[RFC] [varchar](13) NOT NULL,
	[IdTipoCalle] [int] NULL,
	[Calle] [varchar](50) NOT NULL,
	[NumeroExterior] [int] NOT NULL,
	[NumeroInterior] [varchar](5) NULL,
	[IdTipoColonia] [int] NOT NULL,
	[Colonia] [varchar](100) NOT NULL,
	[IdLocalidad] [int] NOT NULL,
	[CodigoPostal] [int] NULL,
	[Lada] [int] NULL,
	[TelefonoFijo] [int] NOT NULL,
	[ExtensionTelefonica] [int] NULL,
	[TelefonoMovil] [bigint] NULL,
	[CorreoElectronico] [varchar](50) NOT NULL,
	[IdEmpresa] [int] NOT NULL,
	[Vigencia] [datetime] NULL,
	[PoderNotarial] [varchar](50) NULL,
	[ActaConstitutiva] [varchar](50) NULL,
	[IdentifcacionOficial] [varchar](50) NULL,
	[CedulaRFC] [nchar](10) NULL,
	[FechaSolicitud] [datetime] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL CONSTRAINT [DF_CatRepresentantesLegales_FechaRegistro]  DEFAULT (getdate()),
	[FechaModificacion] [datetime] NULL,
	[Observaciones] [varchar](500) NULL,
	[Activo] [bit] NOT NULL,
	[IdEstatusSolicitudRepresentante] [int] NULL,
	[IdUsuarioRegistro] [int] NULL,
 CONSTRAINT [PK_CatRepresentantesLegales] PRIMARY KEY CLUSTERED 
(
	[IdRepresentanteLegal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatTecnologias]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatTecnologias](
	[IdTecnologia] [int] NOT NULL,
	[Tecnologia] [varchar](50) NOT NULL,
	[Homoclave] [varchar](50) NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_CatTecnologias] PRIMARY KEY CLUSTERED 
(
	[IdTecnologia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatTiposAccion]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatTiposAccion](
	[idAccion] [varchar](200) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Tablas] [varchar](10) NULL,
	[Parametros] [varchar](100) NOT NULL,
 CONSTRAINT [PK_TiposAcciones] PRIMARY KEY CLUSTERED 
(
	[idAccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatTiposAsentamiento]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatTiposAsentamiento](
	[IdTipoAsentamiento] [int] NOT NULL,
	[TipoAsentamiento] [varchar](21) NOT NULL,
 CONSTRAINT [PK_CatTipoAsentamientos] PRIMARY KEY CLUSTERED 
(
	[IdTipoAsentamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatTiposDia]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatTiposDia](
	[IdTipoDia] [int] NOT NULL,
	[TipoDia] [varchar](20) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatTiposUsuario]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatTiposUsuario](
	[IdTipoUsuario] [int] NOT NULL,
	[TipoUsuario] [varchar](20) NOT NULL,
 CONSTRAINT [PK_CatTiposUsuario] PRIMARY KEY CLUSTERED 
(
	[IdTipoUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatTiposVialidad]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatTiposVialidad](
	[IdTipoVialidad] [int] NOT NULL,
	[TipoVialidad] [varchar](255) NOT NULL,
 CONSTRAINT [PK_CatTipoVialidad] PRIMARY KEY CLUSTERED 
(
	[IdTipoVialidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatTramites](
	[IdTramite] [int] NOT NULL,
	[Nombre] [varchar](300) NOT NULL,
	[Homoclave] [varchar](30) NOT NULL,
	[Modalidad] [varchar](30) NULL,
	[TiempoRecepcionDocumentos] [int] NOT NULL,
	[TiempoResolucion] [int] NOT NULL,
	[Descripcion] [varchar](max) NULL,
	[RegistroCOFEMER] [varchar](50) NULL,
	[URLTramites] [varchar](250) NULL,
	[URLRequisitos] [varchar](250) NULL,
	[Dependencia] [varchar](20) NULL,
	[CorreoResponsable] [varchar](50) NULL,
	[CorreoSuperior] [varchar](50) NULL,
	[Activo] [bit] NULL,
	[PorcentajeAlertaA] [int] NULL,
	[PorcentajeAlertaB] [int] NULL,
	[PorcentajeAlertaC] [int] NULL,
	[IdTipoDia] [int] NULL,
 CONSTRAINT [PK_CatTramites] PRIMARY KEY CLUSTERED 
(
	[IdTramite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CatUsuarios]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CatUsuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpresa] [int] NULL,
	[IdTipoUsuario] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[CorreoElectronico] [varchar](50) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL CONSTRAINT [DF_CatUsuarios_FechaRegistro]  DEFAULT (getdate()),
	[FechaModificacion] [datetime] NULL,
	[Activo] [bit] NOT NULL,
	[ClaveReset] [varchar](200) NULL,
	[FechaLimiteReset] [datetime] NULL,
	[IdRepresentanteAsociado] [int] NULL,
	[CreadoDuranteRegistro] [bit] NULL,
 CONSTRAINT [PK_CatUsuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CatUsuarios] UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EntidadFederativa_CodigoPostal]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntidadFederativa_CodigoPostal](
	[IdEntFedCP] [int] NOT NULL,
	[IdEntidadFederativa] [int] NOT NULL,
	[CPInicial] [int] NOT NULL,
	[CPFinal] [int] NOT NULL,
 CONSTRAINT [PK_EntidadFederativa_CodigoPostal] PRIMARY KEY CLUSTERED 
(
	[IdEntFedCP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProyectoPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectoPreguntas](
	[IdProyectoPregunta] [int] IDENTITY(1,1) NOT NULL,
	[IdProyecto] [int] NOT NULL,
	[IdPregunta] [int] NOT NULL,
	[Respuesta] [bit] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[Modificar] [bit] NULL,
 CONSTRAINT [PK_ProyectoPregunta] PRIMARY KEY CLUSTERED 
(
	[IdProyectoPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProyectoSeguimiento]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProyectoSeguimiento](
	[IdProyectoSeguimiento] [int] IDENTITY(1,1) NOT NULL,
	[IdProyecto] [int] NOT NULL,
	[IdEstatusProyecto] [int] NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_ProyectoSeguimiento] PRIMARY KEY CLUSTERED 
(
	[IdProyectoSeguimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProyectosINERE]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProyectosINERE](
	[IdProyectoINERE] [int] IDENTITY(1,1) NOT NULL,
	[NombreProyecto] [varchar](50) NULL,
	[NombreEmpresa] [varchar](50) NULL,
	[Tecnologia] [varchar](50) NULL,
	[Tecnologia2] [varchar](50) NULL,
	[Municipio] [varchar](100) NULL,
	[Latitud] [float] NULL,
	[Longitud] [float] NULL,
	[CapacidadInstalada] [float] NULL,
	[Unidades] [int] NULL,
	[GeneracionAnual] [float] NULL,
	[FactorPlanta] [float] NULL,
	[MontoInversion] [float] NULL,
	[FechaInscripcion] [datetime] NULL,
	[Fase] [varchar](50) NULL,
	[EstadoProyecto] [varchar](50) NULL,
 CONSTRAINT [PK_ProyectosINERE] PRIMARY KEY CLUSTERED 
(
	[IdProyectoINERE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProyectoTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProyectoTramites](
	[IdProyectoTamite] [int] IDENTITY(1,1) NOT NULL,
	[IdProyecto] [int] NOT NULL,
	[IdTecnologiaTamite] [int] NOT NULL,
	[IdEstatusTramite] [int] NOT NULL,
	[DiasAgregados] [int] NULL,
	[Causa] [varchar](20) NULL,
	[FechaInicio] [date] NOT NULL,
	[FechaFinLegal] [date] NULL,
	[FechaFinReal] [date] NULL,
	[Resolutivo] [text] NOT NULL,
	[Observacion] [text] NULL,
	[FechaRegistro] [datetime] NOT NULL CONSTRAINT [DF_ProyectoTramites_StamTime]  DEFAULT (getdate()),
	[Activo] [bit] NOT NULL,
	[IdGlobal] [varchar](100) NULL,
 CONSTRAINT [PK_ProyectoTramites] PRIMARY KEY CLUSTERED 
(
	[IdProyectoTamite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistroDeAcciones]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegistroDeAcciones](
	[IdRegistro] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Accion] [varchar](200) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_RegistroDeMovimientos] PRIMARY KEY CLUSTERED 
(
	[IdRegistro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RespuestasTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RespuestasTramites](
	[IdRespuesta] [int] NOT NULL,
	[IdPregunta] [bit] NOT NULL,
	[IdProyecto] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TecnologiaPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TecnologiaPreguntas](
	[IdTecnologiaPregunta] [int] IDENTITY(1,1) NOT NULL,
	[IdTecnologia] [int] NOT NULL,
	[Pregunta] [varchar](500) NOT NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_CatPreguntasTrámites] PRIMARY KEY CLUSTERED 
(
	[IdTecnologiaPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TecnologiaRecursos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TecnologiaRecursos](
	[IdTecnologiaRecursos] [int] IDENTITY(1,1) NOT NULL,
	[IdTecnologia] [int] NOT NULL,
	[IdRecurso] [int] NOT NULL,
 CONSTRAINT [PK_TecnologiaRecursos] PRIMARY KEY CLUSTERED 
(
	[IdTecnologiaRecursos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TecnologiaTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TecnologiaTramites](
	[IdTecnologiaTamite] [int] NOT NULL,
	[IdTecnologia] [int] NOT NULL,
	[IdFase] [int] NOT NULL,
	[IdTramite] [int] NOT NULL,
	[IdPosicion] [int] NOT NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_TecnologiaTramites] PRIMARY KEY CLUSTERED 
(
	[IdTecnologiaTamite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoUsuarioPermiso]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuarioPermiso](
	[IdTipoUsuarioPermiso] [int] IDENTITY(1,1) NOT NULL,
	[IdTipoUsuario] [int] NOT NULL,
	[IdPermiso] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CatAPGV] ADD  CONSTRAINT [DF_CatAPGV_FechaRegistro]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[CatMunicipios]  WITH CHECK ADD  CONSTRAINT [FK_CatMunicipios_CatEntidadesFederativas] FOREIGN KEY([IdEntidadFederativa])
REFERENCES [dbo].[CatEntidadesFederativas] ([IdEntidadFederativa])
GO
ALTER TABLE [dbo].[CatMunicipios] CHECK CONSTRAINT [FK_CatMunicipios_CatEntidadesFederativas]
GO
ALTER TABLE [dbo].[CatUsuarios]  WITH CHECK ADD  CONSTRAINT [FK_CatUsuarios_CatTiposUsuario] FOREIGN KEY([IdTipoUsuario])
REFERENCES [dbo].[CatTiposUsuario] ([IdTipoUsuario])
GO
ALTER TABLE [dbo].[CatUsuarios] CHECK CONSTRAINT [FK_CatUsuarios_CatTiposUsuario]
GO
ALTER TABLE [dbo].[EntidadFederativa_CodigoPostal]  WITH CHECK ADD  CONSTRAINT [FK_EntidadFederativa_CodigoPostal_CatEntidadesFederativas] FOREIGN KEY([IdEntidadFederativa])
REFERENCES [dbo].[CatEntidadesFederativas] ([IdEntidadFederativa])
GO
ALTER TABLE [dbo].[EntidadFederativa_CodigoPostal] CHECK CONSTRAINT [FK_EntidadFederativa_CodigoPostal_CatEntidadesFederativas]
GO
ALTER TABLE [dbo].[ProyectoTramites]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoTramites_CatEstadosTramite] FOREIGN KEY([IdEstatusTramite])
REFERENCES [dbo].[CatEstatusTramite] ([IdEstatusTramite])
GO
ALTER TABLE [dbo].[ProyectoTramites] CHECK CONSTRAINT [FK_ProyectoTramites_CatEstadosTramite]
GO
ALTER TABLE [dbo].[ProyectoTramites]  WITH CHECK ADD  CONSTRAINT [FK_ProyectoTramites_TecnologiaTramites] FOREIGN KEY([IdTecnologiaTamite])
REFERENCES [dbo].[TecnologiaTramites] ([IdTecnologiaTamite])
GO
ALTER TABLE [dbo].[ProyectoTramites] CHECK CONSTRAINT [FK_ProyectoTramites_TecnologiaTramites]
GO
ALTER TABLE [dbo].[RegistroDeAcciones]  WITH CHECK ADD  CONSTRAINT [FK_RegistroDeMovimientos_TiposAcciones] FOREIGN KEY([Accion])
REFERENCES [dbo].[CatTiposAccion] ([idAccion])
GO
ALTER TABLE [dbo].[RegistroDeAcciones] CHECK CONSTRAINT [FK_RegistroDeMovimientos_TiposAcciones]
GO
ALTER TABLE [dbo].[TecnologiaRecursos]  WITH CHECK ADD  CONSTRAINT [FK_TecnologiaRecursos_CatRecursos] FOREIGN KEY([IdRecurso])
REFERENCES [dbo].[CatRecursos] ([IdRecurso])
GO
ALTER TABLE [dbo].[TecnologiaRecursos] CHECK CONSTRAINT [FK_TecnologiaRecursos_CatRecursos]
GO
ALTER TABLE [dbo].[TecnologiaRecursos]  WITH CHECK ADD  CONSTRAINT [FK_TecnologiaRecursos_CatTecnologias] FOREIGN KEY([IdTecnologia])
REFERENCES [dbo].[CatTecnologias] ([IdTecnologia])
GO
ALTER TABLE [dbo].[TecnologiaRecursos] CHECK CONSTRAINT [FK_TecnologiaRecursos_CatTecnologias]
GO
ALTER TABLE [dbo].[TecnologiaTramites]  WITH CHECK ADD  CONSTRAINT [FK_TecnologiaTramites_CatTecnologias] FOREIGN KEY([IdTecnologia])
REFERENCES [dbo].[CatTecnologias] ([IdTecnologia])
GO
ALTER TABLE [dbo].[TecnologiaTramites] CHECK CONSTRAINT [FK_TecnologiaTramites_CatTecnologias]
GO
ALTER TABLE [dbo].[TecnologiaTramites]  WITH CHECK ADD  CONSTRAINT [FK_TecnologiaTramites_CatTramites] FOREIGN KEY([IdTramite])
REFERENCES [dbo].[CatTramites] ([IdTramite])
GO
ALTER TABLE [dbo].[TecnologiaTramites] CHECK CONSTRAINT [FK_TecnologiaTramites_CatTramites]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarProyectoPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016/05/24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarProyectoPreguntas]
	@IdProyecto int
    ,@IdPregunta int
    ,@Respuesta bit
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.ProyectoPreguntas
	SET
           [Respuesta] = @Respuesta
           ,[FechaModificacion] = GETDATE()
	WHERE 
		IdProyecto = @IdProyecto
		and IdPregunta = @IdPregunta
END




GO
/****** Object:  StoredProcedure [dbo].[InsertarProyectoPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016/05/24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[InsertarProyectoPreguntas]
	@IdProyecto int
    ,@IdPregunta int
    ,@Respuesta bit
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [dbo].[ProyectoPreguntas]
           ([IdProyecto]
           ,[IdPregunta]
           ,[Respuesta]
           ,[FechaRegistro])
     VALUES
           (
		   @IdProyecto
           ,@IdPregunta
           ,@Respuesta
           ,GETDATE()
		   )
END




GO
/****** Object:  StoredProcedure [dbo].[SeleccionarTecnologiaPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-18
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SeleccionarTecnologiaPreguntas] 
	-- Add the parameters for the stored procedure here
	@IdTecnologia int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
	  IdTecnologiaPregunta
      ,IdTecnologia
      ,Pregunta
  FROM 
	  [dbo].[TecnologiaPreguntas]
  WHERE
	  IdTecnologia = @IdTecnologia
END




GO
/****** Object:  StoredProcedure [dbo].[SpActivarUsuariosPorRepresentante]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivarUsuariosPorRepresentante] 
	@IdRepresentante int
	,@Contrasenia varchar(200)
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE CU
	SET CU.Activo=1

	FROM dbo.CatUsuarios CU
	INNER JOIN dbo.CatRepresentantesLegales CR
	ON CU.IdRepresentanteAsociado = CR.IdRepresentanteLegal
	where 
		IdRepresentanteAsociado = @IdRepresentante
		and CreadoDuranteRegistro = 1

	UPDATE CU
	SET CU.Password=@Contrasenia

	FROM dbo.CatUsuarios CU
	INNER JOIN dbo.CatRepresentantesLegales CR
	ON CU.IdRepresentanteAsociado = CR.IdRepresentanteLegal
	where 
		IdRepresentanteAsociado = @IdRepresentante
		and IdTipoUsuario = 2
		and CreadoDuranteRegistro = 1

END





GO
/****** Object:  StoredProcedure [dbo].[SpActualizarEmpresa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-04-21
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarEmpresa] 
	@NombreComercial varchar(100)
    ,@RazonSocial varchar(100)
    ,@RFC varchar(100)
    ,@IdTipoCalle int
    ,@Calle varchar(100)
    ,@NumeroExterior int
    ,@NumeroInterior varchar(5) = null
    ,@IdTipoColonia int
    ,@Colonia varchar(100)
    ,@IdLocalidad int 
    ,@CodigoPostal int = null
    ,@Lada int 
    ,@TelefonoFijo int
    ,@CorreoElectronico varchar(100)
	,@IdEmpresa int
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE [dbo].[CatEmpresas]
   SET [NombreComercial] = @NombreComercial
      ,[RazonSocial] = @RazonSocial
      ,[RFC] = @RFC
      ,[IdTipoCalle] = @IdTipoCalle
      ,[Calle] = @Calle
      ,[NumeroExterior] = @NumeroExterior
      ,[NumeroInterior] = @NumeroInterior
      ,[IdTipoColonia] = @IdTipoColonia
      ,[Colonia] = @Colonia
      ,[IdLocalidad] = @IdLocalidad
      ,[CodigoPostal] = @CodigoPostal
      ,[Lada] = @Lada
      ,[TelefonoFijo] = @TelefonoFijo
      ,[CorreoElectronico] = @CorreoElectronico
      ,[FechaModificacion] = GETDATE()
 WHERE IdEmpresa = @IdEmpresa
END







GO
/****** Object:  StoredProcedure [dbo].[SpActualizarEstatusDiaInhabil]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Domingo 28 de Febrero del 2016
-- Description:	Permite eliminar logicamente un proyecto.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpActualizarEstatusDiaInhabil]
@IdDiaInhabil int
,@Activo bit

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE 	
		dbo.CatDiasInhabiles 
	SET
		Activo=@Activo
	where IdDiaInhabil=@IdDiaInhabil
	SET NOCOUNT OFF;
END





















GO
/****** Object:  StoredProcedure [dbo].[SpActualizarEstatusTramite]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-03-31
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarEstatusTramite]
	-- Add the parameters for the stored procedure here
	@IdProyecto int
	,@Homoclave varchar(30)
	,@IdEstatus int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
INSERT INTO [dbo].[ProyectoTramites]
           ([IdProyecto]
           ,[IdTecnologiaTamite]
           ,[IdEstatusTramite]
           ,[FechaInicio]
		   ,FechaFinLegal
		   ,FechaFinReal
           ,[Observacion]
           ,[FechaRegistro]
		   ,Resolutivo
           ,[Activo])
     Select TOP 1 @IdProyecto, PT.IdTecnologiaTamite, @IdEstatus,  FechaInicio, FechaFinLegal, FechaFinLegal, 'NA',GETDATE(),'NA',1  
	 FROM dbo.ProyectoTramites AS PT
	INNER JOIN dbo.TecnologiaTramites AS TT
       ON PT.IdTecnologiaTamite = TT.IdTecnologiaTamite
	INNER JOIN dbo.CatTramites CT
	ON CT.IdTramite = TT.IdTramite


	WHERE 
		PT.IdProyecto = @IdProyecto
		and Homoclave = @Homoclave

		ORDER BY FechaRegistro DESC

END









GO
/****** Object:  StoredProcedure [dbo].[SpActualizarEstatusUsuarioSENER]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Domingo 28 de Febrero del 2016
-- Description:	Permite eliminar logicamente un proyecto.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpActualizarEstatusUsuarioSENER]
@IdUsuario int
,@Activo bit

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE 	
		dbo.CatUsuarios 
	SET
		Activo=@Activo
	where IdUsuario=@IdUsuario
	SET NOCOUNT OFF;
END





















GO
/****** Object:  StoredProcedure [dbo].[SpActualizarProyecto]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Domingo 28 de Febrero del 2016
-- Description:	Actualiza datos del catalago proyectos.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpActualizarProyecto]
@IdProyecto int,
@IdTecnologia int,
@IdMunicipio int,
@Nombre varchar(50),
@Latitud float,
@Longitud float,
@CapacidadInstalada float,
@GeneracionAnual float,
@FactorPlanta float,
@MontoInversion money,
@IdLocalidad int,
@IdTipoColonia int = null,
@Colonia varchar(100) = null

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE 	CatProyectos SET
	IdTecnologia= @IdTecnologia,
	IdMunicipio= @IdMunicipio,
	Nombre= @Nombre,
	Latitud= @Latitud,
	Longitud= @Longitud,
	CapacidadInstalada= @CapacidadInstalada,
	GeneracionAnual= @GeneracionAnual,
	FactorPlanta= @FactorPlanta,
	MontoInversion= @MontoInversion,
	IdLocalidad= @IdLocalidad,
	IdTipoColonia = @IdTipoColonia,
	Colonia = @Colonia
	where IdProyecto=@IdProyecto
	SET NOCOUNT OFF;
END





















GO
/****** Object:  StoredProcedure [dbo].[SpActualizarProyectoPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016/05/24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarProyectoPreguntas]
	@IdProyecto int
    ,@IdPregunta int
    ,@Respuesta bit
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.ProyectoPreguntas
	SET
           [Respuesta] = @Respuesta
           ,[FechaModificacion] = GETDATE()
	WHERE 
		IdProyecto = @IdProyecto
		and IdPregunta = @IdPregunta
END




GO
/****** Object:  StoredProcedure [dbo].[SpActualizarProyectoSeguimiento]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarProyectoSeguimiento]
	@IdProyecto int
	,@IdEstatusProyecto int
AS
BEGIN
	SET NOCOUNT ON;

    INSERT INTO [dbo].[ProyectoSeguimiento]
		(
			IdProyecto
			,IdEstatusProyecto
			,FechaRegistro
		)
	VALUES
		(
			@IdProyecto
			,@IdEstatusProyecto
			,GETDATE()
		)
END




GO
/****** Object:  StoredProcedure [dbo].[SpActualizarRepresentanteLegal]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		JARN
-- Create date: 2016-04-21
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarRepresentanteLegal] 
	@Nombre varchar(100)
    ,@PrimerApellido varchar(100)
    ,@SegundoApellido varchar(100)
    ,@CURP varchar(100)
    ,@RFC varchar(100)
    ,@IdTipoCalle int
    ,@Calle varchar(100)
    ,@NumeroExterior int 
    ,@NumeroInterior varchar(5) = null
    ,@IdTipoColonia int
    ,@Colonia varchar(100)
    ,@IdLocalidad int
    ,@CodigoPostal int = null
    ,@Lada int = null
    ,@TelefonoFijo int
    ,@ExtensionTelefonica int = null
    ,@TelefonoMovil bigint
    ,@CorreoElectronico varchar(100)
	,@Activo bit
	,@IdEstatusSolicitudRepresentante int
	,@Observaciones varchar(300) = null
	,@IdRepresentanteLegal int
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [dbo].[CatRepresentantesLegales]
   SET 
		[Nombre] = @Nombre
      ,[PrimerApellido] = @PrimerApellido
      ,[SegundoApellido] = @SegundoApellido
      ,[CURP] = @CURP
      ,[RFC] = @RFC
      ,[IdTipoCalle] = @IdTipoCalle
      ,[Calle] = @Calle
      ,[NumeroExterior] = @NumeroExterior
      ,[NumeroInterior] = @NumeroInterior
      ,[IdTipoColonia] = @IdTipoColonia
      ,[Colonia] = @Colonia
      ,[IdLocalidad] = @IdLocalidad
      ,[CodigoPostal] = @CodigoPostal
      ,[Lada] = @Lada
      ,[TelefonoFijo] = @TelefonoFijo
      ,[ExtensionTelefonica] = @ExtensionTelefonica
      ,[TelefonoMovil] = @TelefonoMovil
      ,[CorreoElectronico] = @CorreoElectronico
	  ,[Activo] = @Activo
	  ,[Observaciones] = 'Información actualizada'
	  ,[IdEstatusSolicitudRepresentante] = @IdEstatusSolicitudRepresentante
      ,[FechaModificacion] = GETDATE()

 WHERE IdRepresentanteLegal = @IdRepresentanteLegal

	UPDATE [dbo].[CatUsuarios] SET [CorreoElectronico] = @CorreoElectronico 
	WHERE IdRepresentanteAsociado = @IdRepresentanteLegal
	AND IdTipoUsuario = 2
END







GO
/****** Object:  StoredProcedure [dbo].[SpActualizarTecnologia]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-03-31
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarTecnologia]
	-- Add the parameters for the stored procedure here
	@NombreTecnologia varchar(40)
	,@IdTecnologia int
	

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.CatTecnologias
	SET
		Tecnologia = @NombreTecnologia
	WHERE
		IdTecnologia = @IdTecnologia

END









GO
/****** Object:  StoredProcedure [dbo].[SpActualizarTecnologias]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-03-31
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarTecnologias]
	-- Add the parameters for the stored procedure here
	@IdTecnologia int
	,@NombreTecnologia varchar(40)

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.CatTecnologias
	SET
		Tecnologia = @NombreTecnologia
	WHERE
		IdTecnologia = @IdTecnologia

END









GO
/****** Object:  StoredProcedure [dbo].[SpActualizarTecnologiaTramite]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-18
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarTecnologiaTramite]

	@IdTecnologia int
	,@IdFase int
	,@IdTramite int
	,@IdPosicion int
	

AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IDSiguiente int
	SET @IDSiguiente = (select max(IdTecnologiaTamite) from DBO.TecnologiaTramites ) + 1

	IF EXISTS ( SELECT IdTecnologia FROM TecnologiaTramites WHERE IdTecnologia = @IdTecnologia and IdTramite = @IdTramite ) 
		UPDATE  dbo.TecnologiaTramites   SET  IdFase = @IdFase, IdPosicion = @IdPosicion, Activo = 1 
		WHERE IdTecnologia = @IdTecnologia and IdTramite = @IdTramite
	ELSE
		INSERT INTO [dbo].[TecnologiaTramites]
           ([IdTecnologiaTamite]
           ,[IdTecnologia]
           ,[IdFase]
           ,[IdTramite]
           ,[IdPosicion]
		   ,[Activo])
     VALUES
           (@IDSiguiente
           ,@IdTecnologia
           ,@IdFase
           ,@IdTramite
           ,@IdPosicion
		   ,1)

END









GO
/****** Object:  StoredProcedure [dbo].[SpActualizarTramite]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarTramite]
    @Nombre varchar(300)
    ,@Homoclave varchar(20)
    ,@TiempoRecepcionDocumentos int
    ,@TiempoResolucion int
    ,@Descripcion varchar(300)
    ,@RegistroCOFEMER varchar(50)
    ,@URLTramites varchar(250)
    ,@URLRequisitos varchar(250)
    ,@Dependencia varchar(20)
    ,@CorreoResponsable varchar(50)
    ,@CorreoSuperior varchar(50)
	,@PorcentajeAlertaA int
	,@PorcentajeAlertaB int
	,@PorcentajeAlertaC int
	,@IdTipoDias int
	,@IdTramite int
    
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[CatTramites]
   SET
      [Nombre] = @Nombre
      ,[Homoclave] = @Homoclave
      ,[TiempoRecepcionDocumentos] = @TiempoRecepcionDocumentos
      ,[TiempoResolucion] = @TiempoResolucion
      ,[Descripcion] = @Descripcion
      ,[RegistroCOFEMER] = @RegistroCOFEMER
      ,[URLTramites] = @URLTramites
      ,[URLRequisitos] = @URLRequisitos
      ,[Dependencia] = @Dependencia
      ,[CorreoResponsable] = @CorreoResponsable
      ,[CorreoSuperior] = @CorreoSuperior
      ,[PorcentajeAlertaA] = @PorcentajeAlertaA
      ,[PorcentajeAlertaB] = @PorcentajeAlertaB
      ,[PorcentajeAlertaC] = @PorcentajeAlertaC
      ,[IdTipoDia] = @IdTipoDias
 WHERE 
		IdTramite = @IdTramite

END



GO
/****** Object:  StoredProcedure [dbo].[SpActualizarUsuario]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-04-21
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpActualizarUsuario] 
	@Nombre varchar(100)
	,@CorreoElectronico varchar(100)
	,@Activo bit
	,@Password varchar(300)
	,@IdUsuario int

AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IdTipoUsuario int

	SET @IdTipoUsuario = (SELECT IdTipoUsuario FROM dbo.CatUsuarios WHERE IdUsuario = @IdUsuario)

    UPDATE [dbo].[CatUsuarios]
   SET 
      [Nombre] = @Nombre
      ,[CorreoElectronico] = @CorreoElectronico
	  ,Activo = @Activo
	  ,Password = @Password
      ,[FechaModificacion] = GETDATE()
 WHERE IdUsuario = @IdUsuario

 IF(@IdTipoUsuario = 2)
	UPDATE dbo.CatRepresentantesLegales SET CorreoElectronico = @CorreoElectronico


END







GO
/****** Object:  StoredProcedure [dbo].[SpAuxiliarSolicitarCambioContraseña]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-17
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpAuxiliarSolicitarCambioContraseña] 
	@NombreUsuario varchar(100)
	,@ClaveReset varchar(300)
	,@CorreoElectronico varchar(50) output

AS
BEGIN
	SET NOCOUNT ON;

	
	if (SELECT CorreoElectronico FROM dbo.CatUsuarios WHERE Nombre = @NombreUsuario) is not null
		BEGIN
			UPDATE [dbo].[CatUsuarios]
			SET 
			  ClaveReset = @ClaveReset
			  ,FechaLimiteReset = DATEADD(DAY,1, GETDATE())
			WHERE Nombre = @NombreUsuario

			SET @CorreoElectronico = (SELECT CorreoElectronico FROM dbo.CatUsuarios WHERE Nombre = @NombreUsuario)
		END
	else
		SET @CorreoElectronico = ''


END







GO
/****** Object:  StoredProcedure [dbo].[SpDetallesEmpresa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpDetallesEmpresa] 
	@IdEmpresa int
AS
BEGIN
	SET NOCOUNT ON;

    	SELECT
		IdEmpresa
		,NombreComercial
		,[RazonSocial]
		,[RFC]
		,[Colonia]
		,[Lada]
		,[TelefonoFijo]
		,[CorreoElectronico]
		,[CodigoPostal]
		,CL.[IdLocalidad]
		,CM.IdMunicipio
		,CEF.IdEntidadFederativa
		,[IdTipoColonia]
		,[IdTipoCalle]
		,[Calle]
		,[NumeroExterior]
		,[NumeroInterior]
		,[FechaRegistro]
		,[Activo]
		,CEF.EntidadFederativa
		,CM.IdMunicipioINEGI
		,CM.Municipio
		,CL.Localidad
	FROM [CatEmpresas] CE
	inner join dbo.CatLocalidades CL
	on CE.IdLocalidad = CL.IdLocalidad
	inner join dbo.CatEntidadesFederativas CEF
	on CEF.IdEntidadFederativa = CL.IdEntidadFederativa
	INNER join dbo.CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI

  where 
	idEmpresa = @IdEmpresa
	and CM.IdEntidadFederativa = CL.IdEntidadFederativa
END






GO
/****** Object:  StoredProcedure [dbo].[SpDetallesEmpresaPorRFC]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpDetallesEmpresaPorRFC] 
	@RFC VARCHAR(12)
AS
BEGIN
	SET NOCOUNT ON;

    	SELECT
		IdEmpresa
		,NombreComercial
		,[RazonSocial]
		,[RFC]
		,[Colonia]
		,[Lada]
		,[TelefonoFijo]
		,[CorreoElectronico]
		,[CodigoPostal]
		,CL.[IdLocalidad]
		,CM.IdMunicipio
		,CEF.IdEntidadFederativa
		,[IdTipoColonia]
		,[IdTipoCalle]
		,[Calle]
		,[NumeroExterior]
		,[NumeroInterior]
		,[FechaRegistro]
		,[Activo]
		,CEF.EntidadFederativa
		,CM.Municipio
		,CL.Localidad
	FROM [CatEmpresas] CE
	inner join dbo.CatLocalidades CL
	on CE.IdLocalidad = CL.IdLocalidad
	Inner join dbo.CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipio
	inner join dbo.CatEntidadesFederativas CEF
	on CEF.IdEntidadFederativa = CM.IdEntidadFederativa

  where RFC = @RFC
END






GO
/****** Object:  StoredProcedure [dbo].[SpDetallesProyecto]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Jueves 26 de Febrero del 2016
-- Description:	Obtiene el listado de un proyecto.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpDetallesProyecto]
(
@IdProyecto int
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT  
	CP.IdProyecto
	,CP.Nombre NombreProyecto
	,CP.IdEmpresa
	,CE.NombreComercial NombreEmpresa
	,CP.IdTecnologia
	,CT.Tecnologia
	,CP.IdTipoColonia
	,CP.Colonia
	,CP.IdLocalidad
	,CP.CodigoPostal
	,CL.Localidad
	,CP.IdMunicipio	
	,CM.Municipio
	,CEF.IdEntidadFederativa
	,CEF.EntidadFederativa
	,CP.IdEstatusProyecto
	,CP.Latitud
	,CP.Longitud
	,CP.CapacidadInstalada
	,CP.GeneracionAnual
	,CP.FactorPlanta
	,CP.MontoInversion
	,CP.FechaRegistro
	,CP.IdGlobal
	,CP.Activo
	,CEP.EstatusProyecto


	FROM     dbo.CatProyectos CP
	Inner join dbo.CatEmpresas CE
	on CE.IdEmpresa=CP.IdEmpresa
	inner join dbo.CatLocalidades CL
	on CP.IdLocalidad = CL.IdLocalidad
	inner join dbo.CatMunicipios CM
	on CP.IdMunicipio= CM.idMunicipio
	inner join dbo.CatEntidadesFederativas CEF
	on CM.IdEntidadFederativa = CEF.IdEntidadFederativa
	inner join dbo.CatTecnologias CT
	on CP.IdTecnologia= CT.IdTecnologia
	inner join dbo.CatEstatusProyecto CEP
	on CP.IdEstatusProyecto = CEP.idEstatusProyecto
	and CP.IdProyecto = @IdProyecto
	and CP.Activo = 1
SET NOCOUNT OFF;
END





















GO
/****** Object:  StoredProcedure [dbo].[SpDetallesRepresentanteLegal]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-22-04
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpDetallesRepresentanteLegal] 
	-- Add the parameters for the stored procedure here
	@IdRepresentanteLegal int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT 
		IdRepresentanteLegal
		,CRL.IdEmpresa
		,CRL.Activo   
		,CRL.Nombre
		,CRL.PrimerApellido
		,CRL.SegundoApellido
		,Concat(PrimerApellido, ' ', SegundoApellido, ' ', Nombre) NombreRepresentante
		,CURP
		,CRL.RFC
		,CM.IdMunicipio
		,CM.Municipio
		,CEF.IdEntidadFederativa
		,CEF.EntidadFederativa
		,CRL.IdLocalidad
		,CL.Localidad
		,CRL.IdTipoColonia
		,CRL.Colonia
		,CRL.CodigoPostal
		,CRL.IdTipoCalle
		,CRL.Calle
		,CRL.NumeroInterior
		,CRL.NumeroExterior
		,CRL.TelefonoFijo
		,CRL.Lada
		,CRL.ExtensionTelefonica
		,CRL.TelefonoMovil
		,CRL.CorreoElectronico
		,CRL.FechaSolicitud
		,CRL.FechaRegistro
		,CRL.FechaModificacion
		,CRL.ActaConstitutiva
		,CRL.PoderNotarial
		,CRL.CedulaRFC
		,CRL.IdentifcacionOficial
		,Crl.Observaciones
		,CRL.IdEstatusSolicitudRepresentante

  
  FROM CatRepresentantesLegales CRL
  inner join dbo.CatLocalidades CL
  on CL.IdLocalidad = CRL.IdLocalidad

	INNER join dbo.CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI
	inner join dbo.CatEntidadesFederativas CEF
	on CEF.IdEntidadFederativa = CM.IdEntidadFederativa
  inner join dbo.CatEmpresas CE
  on CRL.IdEmpresa = CE.IdEmpresa
	WHERE
		IdRepresentanteLegal = @IdRepresentanteLegal
		and CM.IdEntidadFederativa = CL.IdEntidadFederativa
END






GO
/****** Object:  StoredProcedure [dbo].[SpDetallesRepresentantePorRFC]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpDetallesRepresentantePorRFC] 
	-- Add the parameters for the stored procedure here
	@RFCRepresentanteLegal varchar(13)
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT IdRepresentanteLegal
	FROM dbo.CatRepresentantesLegales
	where RFC = @RFCRepresentanteLegal
END





GO
/****** Object:  StoredProcedure [dbo].[SpDetallesTecnologia]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-18
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpDetallesTecnologia]
	@IdTecnologia int

AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		IdTecnologia
		,Tecnologia
	FROM 
		dbo.CatTecnologias
	WHERE 
		Activo = 1
		and IdTecnologia = @IdTecnologia
	ORDER BY
		Tecnologia asc

END






GO
/****** Object:  StoredProcedure [dbo].[SpDetallesTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpDetallesTramites] 
	@IdTramite int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		CT.[IdTramite]
      ,CT.[Nombre]
      ,CT.[Homoclave]
      ,CT.[TiempoRecepcionDocumentos]
      ,CT.[TiempoResolucion]
      ,CT.[Descripcion]
      ,CT.[RegistroCOFEMER]
      ,CT.[URLTramites]
      ,CT.[URLRequisitos]
      ,CT.[Dependencia]
      ,CT.[CorreoResponsable] 
      ,CT.[CorreoSuperior] 
      ,CT.[PorcentajeAlertaA] 
      ,CT.[PorcentajeAlertaB] 
      ,CT.[PorcentajeAlertaC] 
      ,CT.[IdTipoDia] 
	  ,CT.[Activo]
  FROM [dbo].[CatTramites] CT

  where
  CT.IdTramite IS NULL OR IdTramite = ISNULL(@IdTramite, CT.IdTramite)

END








GO
/****** Object:  StoredProcedure [dbo].[SpDetallesUsuario]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpDetallesUsuario] 
	@IdUsuario int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		CU.Nombre
		,CU.CorreoElectronico
		,CU.Password
		,CU.Activo
		,CU.FechaRegistro
		,CU.FechaModificacion
		,CU.IdUsuario
		,CU.IdTipoUsuario
		,CU.IdEmpresa
		,CU.ClaveReset
		,CU.FechaLimiteReset
		
	FROM dbo.CatUsuarios CU

  where IdUsuario = @IdUsuario
END






GO
/****** Object:  StoredProcedure [dbo].[SpDetallesUsuarioPorRepresentante]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpDetallesUsuarioPorRepresentante] 
	@IdRepresentanteLegal int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		CU.Nombre
		,CU.CorreoElectronico
		,CU.Password
		,CU.Activo
		,CU.FechaRegistro
		,CU.FechaModificacion
		,CU.IdUsuario
		,CU.IdTipoUsuario
		,CU.IdEmpresa
		,CU.ClaveReset
		,CU.FechaLimiteReset
		
	FROM dbo.CatUsuarios CU

  where IdRepresentanteAsociado = @IdRepresentanteLegal
  AND IdTipoUsuario = 2
END






GO
/****** Object:  StoredProcedure [dbo].[SpEliminarEmpresa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpEliminarEmpresa] 
	@IdEmpresa int
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE
		dbo.CatUsuarios
	SET 
		Activo=0
	WHERE 
		IdEmpresa = @IdEmpresa

	UPDATE 
		DBO.CatRepresentantesLegales 
	SET 
		Activo = 0, Observaciones = 'Empresa eliminada', IdEstatusSolicitudRepresentante = 1
	WHERE 
		IdEmpresa = @IdEmpresa

	UPDATE 
		dbo.CatEmpresas
	SET 
		Activo = 0
	WHERE
		IdEmpresa = @IdEmpresa

END





GO
/****** Object:  StoredProcedure [dbo].[SpEliminarIntentoRegistro]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-06-06
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpEliminarIntentoRegistro] 
	@IdEmpresa int
AS
BEGIN
	SET NOCOUNT ON;
	
	DELETE FROM dbo.CatUsuarios
	WHERE IdEmpresa = @IdEmpresa

	DELETE FROM dbo.CatRepresentantesLegales
	WHERE IdEmpresa = @IdEmpresa

	DELETE FROM dbo.CatEmpresas
	WHERE IdEmpresa = @IdEmpresa

END





GO
/****** Object:  StoredProcedure [dbo].[SpEliminarProyecto]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Domingo 28 de Febrero del 2016
-- Description:	Permite eliminar logicamente un proyecto.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpEliminarProyecto]
@IdProyecto int

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE 	CatProyectos SET
	Activo=0
	where IdProyecto=@IdProyecto
	SET NOCOUNT OFF;
END





















GO
/****** Object:  StoredProcedure [dbo].[SpEliminarProyectos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016.05.04
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpEliminarProyectos]
	@IdProyecto INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE
		dbo.CatProyectos
	SET 
		Activo=0
	WHERE 
		IdProyecto = @IdProyecto;

	UPDATE
		dbo.ProyectoTramites
	SET 
		Activo=0
	WHERE 
		IdProyecto = @IdProyecto;

END





GO
/****** Object:  StoredProcedure [dbo].[SpEliminarRepresentanteLegal]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Domingo 28 de Febrero del 2016
-- Description:	Permite eliminar logicamente un proyecto.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpEliminarRepresentanteLegal]
@IdRepresentante int

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE 	
		dbo.CatRepresentantesLegales 
	SET
		Activo='false'
	where IdRepresentanteLegal=@IdRepresentante
	SET NOCOUNT OFF;
END





















GO
/****** Object:  StoredProcedure [dbo].[SpEliminarTecnologiaPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-18
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpEliminarTecnologiaPreguntas] 
	-- Add the parameters for the stored procedure here
	@IdTecnologia int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE
  FROM 
	  [dbo].[TecnologiaPreguntas]
  WHERE
	  IdTecnologia = @IdTecnologia
END




GO
/****** Object:  StoredProcedure [dbo].[SpEliminarTecnologiaPreguntasPorTecnologia]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-18
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpEliminarTecnologiaPreguntasPorTecnologia]
	@IdTecnologia int

	

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [dbo].[TecnologiaPreguntas]
	   SET 
		  Activo = 0
	 WHERE 
		IdTecnologia = @IdTecnologia

END









GO
/****** Object:  StoredProcedure [dbo].[SpEliminarTecnologias]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpEliminarTecnologias]
	@IdTecnologia int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE 	CatTecnologias 
	SET
		Activo=0
	WHERE IdTecnologia = @IdTecnologia
	SET NOCOUNT OFF;
END



GO
/****** Object:  StoredProcedure [dbo].[SpEliminarTecnologiaTramitesPorTecnologia]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-18
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpEliminarTecnologiaTramitesPorTecnologia]
	@IdTecnologia int

	

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE [dbo].[TecnologiaTramites]
	   SET 
		  Activo = 0
	 WHERE 
		IdTecnologia = @IdTecnologia

END









GO
/****** Object:  StoredProcedure [dbo].[SpEliminarTramite]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-18
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpEliminarTramite]
	@IdTramite int
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE 	dbo.CatTramites 
	SET
		Activo=0
	WHERE IdTramite = @IdTramite
	SET NOCOUNT OFF;
END



GO
/****** Object:  StoredProcedure [dbo].[SpEliminarUsuario]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Domingo 28 de Febrero del 2016
-- Description:	Permite eliminar logicamente un proyecto.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpEliminarUsuario]
@IdUsuario int

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE 	
		dbo.CatUsuarios 
	SET
		Activo=0
	where IdUsuario=@IdUsuario
	SET NOCOUNT OFF;
END





















GO
/****** Object:  StoredProcedure [dbo].[SpEmpresas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Jueves 19 de Febrero del 2016
-- Description:	Obtiene todas las Empresas.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpEmpresas]
AS
BEGIN
	SET NOCOUNT ON;
SELECT
	dbo.CatEmpresas.IdEmpresa,
	dbo.CatEmpresas.NombreComercial Nombre
	from dbo.CatEmpresas
	where dbo.CatEmpresas.Activo=1
SET NOCOUNT OFF;
END



GO
/****** Object:  StoredProcedure [dbo].[SpEstados]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SpEstados]
AS
BEGIN
	SET NOCOUNT ON;
SELECT IdEntidadFederativa, EntidadFederativa as Nombre from dbo.CatEntidadesFederativas

SET NOCOUNT OFF;
END



GO
/****** Object:  StoredProcedure [dbo].[SpEstatusProyectos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Sabado 27 de Febrero del 2016
-- Description:	Obtiene el listado de estatus.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpEstatusProyectos]
AS
BEGIN
	SET NOCOUNT ON;
SELECT  
		idEstatusProyecto,
		EstatusProyecto AS Nombre
FROM    CatEstatusProyecto
SET NOCOUNT OFF;
END












GO
/****** Object:  StoredProcedure [dbo].[SpGraficoEmpresasPorEntidadFederativa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =======================================================
-- Author:		JARN
-- Create date: Jueves 19 de Febrero del 2016
-- Description:	
	
-- =======================================================


CREATE PROCEDURE [dbo].[SpGraficoEmpresasPorEntidadFederativa]
  @IdEntidadFederativa int =null
 ,@IdMunicipio int =null
 ,@fechainicio datetime =null
 ,@fechafin datetime =null
  ,@IdTecnologia int=null
AS
BEGIN
	SET NOCOUNT ON;

/* 
	COUNT(CE.IdEmpresa) CantidadEmpresas
	,COUNT(CE.IdEmpresa) TotalDatos
	,CEF.EntidadFederativa
*/
select 
	COUNT(IdEmpresa) CantidadEmpresas
	,COUNT(IdEmpresa) TotalDatos
	,EntidadFederativa
FROM 
(
SELECT distinct
	CE.IdEmpresa AS IdEmpresa
	,CEF.EntidadFederativa AS EntidadFederativa
 
	FROM DBO.CatEmpresas CE
	inner join dbo.CatLocalidades CL
	on CE.IdLocalidad = CL.IdLocalidad
	INNER JOIN CatEntidadesFederativas CEF
	on CEF.IdEntidadFederativa = CL.IdEntidadFederativa
	INNER JOIN CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI
	INNER JOIN dbo.CatProyectos CP
	on CP.IdEmpresa=CE.IdEmpresa
	INNER JOIN dbo.CatTecnologias CT
	on CT.IdTecnologia=CP.IdTecnologia
 
WHERE 
CE.Activo=1
AND ce.IdEmpresa !=1	
AND CM.IdEntidadFederativa = CEF.IdEntidadFederativa	
AND  (CM.IdMunicipio IS NULL OR CM.IdMunicipio = ISNULL(@IdMunicipio, CM.IdMunicipio) )
  AND (CEF.IdEntidadFederativa IS NULL or CEF.IdEntidadFederativa = ISNULL(@IdEntidadFederativa, CEF.IdEntidadFederativa) )
  AND ((@fechainicio IS NULL) or CE.fecharegistro >= @fechainicio)
  AND ((@fechafin IS NULL) or CE.fecharegistro <= @fechafin)
  AND (CT.IdTecnologia IS NULL or CT.IdTecnologia = ISNULL(@IdTecnologia, CT.IdTecnologia) )  
  
) T


  group by T.EntidadFederativa


SET NOCOUNT OFF;
END










GO
/****** Object:  StoredProcedure [dbo].[SpGraficoEmpresasPorTecnologia]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =======================================================
-- Author:		JARN
-- Create date: Jueves 19 de Febrero del 2016
-- Description:	
	
-- =======================================================


CREATE PROCEDURE [dbo].[SpGraficoEmpresasPorTecnologia]
  @IdEntidadFederativa int =null
 ,@IdMunicipio int =null
 ,@fechainicio datetime =null
 ,@fechafin datetime =null
  ,@IdTecnologia int=null
AS
BEGIN
	SET NOCOUNT ON;

	
select 
	COUNT(IdEmpresa) CantidadEmpresas
	,COUNT(IdEmpresa) TotalDatos
	,Tecnologia
FROM 
(
SELECT distinct
	CE.IdEmpresa AS IdEmpresa
	,CT.Tecnologia AS Tecnologia
 
	FROM DBO.CatEmpresas CE
	inner join dbo.CatLocalidades CL
	on CE.IdLocalidad = CL.IdLocalidad
	INNER JOIN CatEntidadesFederativas CEF
	on CEF.IdEntidadFederativa = CL.IdEntidadFederativa
	INNER JOIN CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI
	INNER JOIN dbo.CatProyectos CP
	on CP.IdEmpresa=CE.IdEmpresa
	INNER JOIN dbo.CatTecnologias CT
	on CT.IdTecnologia=CP.IdTecnologia
 
WHERE 
CE.Activo=1
AND ce.IdEmpresa !=1	
AND CM.IdEntidadFederativa = CEF.IdEntidadFederativa	
AND (CM.IdMunicipio IS NULL OR CM.IdMunicipio = ISNULL(@IdMunicipio, CM.IdMunicipio) )
AND (CEF.IdEntidadFederativa IS NULL or CEF.IdEntidadFederativa = ISNULL(@IdEntidadFederativa, CEF.IdEntidadFederativa) )
AND ((@fechainicio IS NULL) or CE.fecharegistro >= @fechainicio)
AND ((@fechafin IS NULL) or CE.fecharegistro <= @fechafin)
AND (CT.IdTecnologia IS NULL or CT.IdTecnologia = ISNULL(@IdTecnologia, CT.IdTecnologia) )
  
  GROUP BY CT.Tecnologia, CE.IdEmpresa
  
) T


  group by T.Tecnologia
SET NOCOUNT OFF;
END










GO
/****** Object:  StoredProcedure [dbo].[SpGraficoInversionPorEntidadFederativa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		JARN
-- Create date: 2016-05-11
-- Description:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpGraficoInversionPorEntidadFederativa]
	@fechainicio datetime =null,
	@fechafin datetime =null
AS
BEGIN
	SET NOCOUNT ON;


	

SELECT 
	SUM(CP.MontoInversion) MontoInversion,
	CEF.EntidadFederativa ,
	COUNT(CP.MontoInversion) TotalDatos
FROM 
	CatProyectos CP
	INNER JOIN CatTecnologias CT
	on CP.IdTecnologia=CT.IdTecnologia
	INNER JOIN dbo.CatLocalidades CL
	on CP.IdLocalidad = CL.IdLocalidad
	INNER JOIN CatEntidadesFederativas CEF
	on CEF.IdEntidadFederativa = CL.IdEntidadFederativa
	INNER JOIN CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI
WHERE 
	CP.Activo=1
	AND CL.IdEntidadFederativa = CM.IdEntidadFederativa
	AND ((@fechainicio IS NULL) or CP.fecharegistro >= @fechainicio)
	AND ((@fechafin IS NULL) or CP.fecharegistro <= @fechafin)
GROUP BY CEF.EntidadFederativa	
SET NOCOUNT OFF;
END











GO
/****** Object:  StoredProcedure [dbo].[SpGraficoInversionPorTecnologia]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Jueves 19 de Febrero del 2016
-- Description:	Obtiene el monto total de inversiones por proyecto
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpGraficoInversionPorTecnologia]
	@fechainicio datetime =null,
	@fechafin datetime =null
AS
BEGIN
	SET NOCOUNT ON;

	

select 
sum(CP.MontoInversion) MontoInversion,
	CT.Tecnologia,
	Count(CP.MontoInversion) TotalDatos
 from CatProyectos CP
 inner join CatTecnologias CT
 on CP.IdTecnologia=CT.IdTecnologia
 where cp.Activo=1
 	AND ((@fechainicio IS NULL) or CP.fecharegistro >= @fechainicio)
	AND ((@fechafin IS NULL) or CP.fecharegistro <= @fechafin)
  group by CT.Tecnologia	
SET NOCOUNT OFF;
END











GO
/****** Object:  StoredProcedure [dbo].[SpGraficoProyectosPorEntidadFederativa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		JARN
-- Create date: 2016-05-11
-- Description:
	
-- =======================================================


CREATE PROCEDURE [dbo].[SpGraficoProyectosPorEntidadFederativa]

	@IdEmpresa int =null,
	@IdEntidadFederativa int= null,
	@IdMunicipio int=null,
	@fechainicio datetime =null,
	@fechafin datetime =null,
	@IdEstatusProyecto int=null

AS
BEGIN
	SET NOCOUNT ON;

	

	SELECT 
		COUNT(CP.IdProyecto) CantidadProyectos,
		COUNT(CP.IdProyecto) TotalDatos,
		CEF.EntidadFederativa
	FROM CatProyectos CP
		INNER JOIN CatTecnologias CT
		ON CP.IdTecnologia=CT.IdTecnologia
		INNER JOIN dbo.CatLocalidades CL
		ON CP.IdLocalidad = CL.IdLocalidad
		INNER JOIN CatEntidadesFederativas CEF
		ON CEF.IdEntidadFederativa = CL.IdEntidadFederativa
		INNER JOIN CatMunicipios CM
		ON CL.IdMunicipio = CM.IdMunicipioINEGI
		INNER JOIN dbo.CatEmpresas CE
		ON CE.IdEmpresa = CP.IdEmpresa
	WHERE 
		(CE.IdEmpresa IS NULL OR CE.IdEmpresa = ISNULL(@IdEmpresa, CE.IdEmpresa) )
		AND (CP.idEstatusProyecto IS NULL OR CP.idEstatusProyecto = ISNULL(@IdEstatusProyecto,CP.idEstatusProyecto) ) 
		AND ((@fechainicio IS NULL) or CP.fecharegistro >= @fechainicio)
		AND ((@fechafin IS NULL) or CP.fecharegistro <= @fechafin)
		AND (CM.IdMunicipio IS NULL or  CM.IdMunicipio= ISNULL(@IdMunicipio,  CM.IdMunicipio) )
		AND ( CM.IdEntidadFederativa IS NULL or  CM.IdEntidadFederativa = ISNULL(@IdEntidadFederativa,  CM.IdEntidadFederativa) )
		AND  CP.IdEmpresa != 1
		AND CL.IdEntidadFederativa = CM.IdEntidadFederativa
		AND CP.Activo=1
	GROUP BY CEF.EntidadFederativa	
SET NOCOUNT OFF;
END










GO
/****** Object:  StoredProcedure [dbo].[SpGraficoProyectosPorTecnologia]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Jueves 19 de Febrero del 2016
-- Description:	Obtiene el listado de los proyectos por tecnologia
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpGraficoProyectosPorTecnologia]
	@IdEmpresa int =null,
	@IdEntidadFederativa int= null,
	@IdMunicipio int=null,
	@fechainicio datetime =null,
	@fechafin datetime =null,
	@IdEstatusProyecto int=null
AS
BEGIN
	SET NOCOUNT ON;
	

SELECT 
	COUNT(CP.IdProyecto) CantidadProyectos,
	COUNT(CP.IdProyecto) TotalDatos,
	CT.Tecnologia
FROM CatProyectos CP
	INNER JOIN CatTecnologias CT
	ON CP.IdTecnologia=CT.IdTecnologia
	INNER JOIN dbo.CatEmpresas CE
	ON CE.IdEmpresa = CP.IdEmpresa
	inner join dbo.CatLocalidades CL
	on CP.IdLocalidad = CL.IdLocalidad
	inner join dbo.CatEntidadesFederativas CEF
	on CEF.IdEntidadFederativa = CL.IdEntidadFederativa
	INNER join dbo.CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI
WHERE 	
	(CE.IdEmpresa IS NULL OR CE.IdEmpresa = ISNULL(@IdEmpresa, CE.IdEmpresa) )
	AND (CP.idEstatusProyecto IS NULL OR CP.idEstatusProyecto = ISNULL(@IdEstatusProyecto,CP.idEstatusProyecto) ) 
	AND ((@fechainicio IS NULL) or CP.fecharegistro >= @fechainicio)
	AND ((@fechafin IS NULL) or CP.fecharegistro <= @fechafin)
	AND (CM.IdMunicipio IS NULL or  CM.IdMunicipio= ISNULL(@IdMunicipio,  CM.IdMunicipio) )
	AND ( CM.IdEntidadFederativa IS NULL or  CM.IdEntidadFederativa = ISNULL(@IdEntidadFederativa,  CM.IdEntidadFederativa) )
	AND  CP.IdEmpresa != 1
	AND CM.IdEntidadFederativa = CL.IdEntidadFederativa
	AND CP.Activo=1
GROUP BY CP.IdTecnologia,CT.Tecnologia	
SET NOCOUNT OFF;
END










GO
/****** Object:  StoredProcedure [dbo].[SpInsertarDiaInhabil]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-15
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertarDiaInhabil]
	 @Dia int
	 ,@Mes int
	 ,@Año int
	 ,@Activo bit
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FechaVarChar varchar(18);
	DECLARE @Fecha datetime;
	SET @FechaVarChar = CONCAT(@Año, '-', @Mes, '-',  @Dia, ' 00:00:00');
	
	SET @Fecha = CONVERT(datetime, @FechaVarChar,120);

	UPDATE 	
		dbo.CatDiasInhabiles 
	SET
		Activo=@Activo
	WHERE   
		Dia = @Dia 
        AND     Mes = @Mes
		AND		Año = @Año

	INSERT  dbo.CatDiasInhabiles (Dia, Mes, Año, FechaCompleta, Activo) 
	SELECT  @Dia, @Mes, @Año, @Fecha, @Activo
	WHERE   NOT EXISTS 
        (   SELECT  1
            FROM    dbo.CatDiasInhabiles 
            WHERE   Dia = @Dia 
            AND     Mes = @Mes
			AND		Año = @Año
        );

	SET NOCOUNT OFF;

    
END




GO
/****** Object:  StoredProcedure [dbo].[SpInsertarEmpresa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-21
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertarEmpresa] 
	@NombreComercial varchar(100)
    ,@RazonSocial varchar(100)
    ,@RFC varchar(100)
    ,@IdTipoCalle int
    ,@Calle varchar(100)
    ,@NumeroExterior int
    ,@NumeroInterior varchar(5) = null
    ,@IdTipoColonia int
    ,@Colonia varchar(100)
    ,@IdLocalidad int 
    ,@CodigoPostal int = null
    ,@Lada int 
    ,@TelefonoFijo int
    ,@CorreoElectronico varchar(100)
    ,@Activo bit
	,@IdEmpresa int out
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FechaActual datetime;
	set @FechaActual = GETDATE();

    -- Insert statements for procedure here
	INSERT INTO dbo.CatEmpresas
           (NombreComercial
           ,RazonSocial
           ,RFC
           ,IdTipoCalle
           ,Calle
           ,NumeroExterior
           ,NumeroInterior
           ,IdTipoColonia
           ,Colonia
           ,IdLocalidad
           ,CodigoPostal
           ,Lada
           ,TelefonoFijo
           ,CorreoElectronico
           ,FechaRegistro
           ,FechaSolicitud
           ,Activo)
     VALUES
           (@NombreComercial
           ,@RazonSocial
           ,@RFC
           ,@IdTipoCalle
           ,@Calle
           ,@NumeroExterior
           ,@NumeroInterior
           ,@IdTipoColonia
           ,@Colonia
           ,@IdLocalidad
           ,@CodigoPostal
           ,@Lada
           ,@TelefonoFijo
           ,@CorreoElectronico
           ,@FechaActual
           ,@FechaActual
           ,@Activo)

	--Regresar Id asignado a la empresa:
	set @idEmpresa = (Select IdEmpresa from dbo.CatEmpresas where FechaRegistro = @fechaactual and RFC = @RFC)
END







GO
/****** Object:  StoredProcedure [dbo].[SpInsertarProyectos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles / Ramos Nolazco Jesús Alejandro
-- Create date: Jueves 26 de Febrero del 2016 / Lunes 06 de Marzo del 2016
-- Description:	Inserta un nuevo registro en el catalogo de proyectos
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpInsertarProyectos]
(
@IdEmpresa int,
@IdTecnologia int,
@IdMunicipio int,
@Nombre varchar(50),
@Latitud float,
@Longitud float,
@CapacidadInstalada float,
@GeneracionAnual float,
@FactorPlanta float,
@MontoInversion money,
@IdLocalidad int,
@IdTipoColonia int = null,
@Colonia varchar(100) = null,
@IdProyectoAsignado int out
)
AS
BEGIN
	SET NOCOUNT ON;
	declare @FechaActual datetime = getdate()
	declare @IdProyecto int
	declare @Empresa varchar(50)
	declare @Tecnologia varchar(50)
	declare @Municipio varchar(100)
	declare @IdGlobal varchar(100)
	DECLARE @FechaJuliana int
	DECLARE @Folio char(8)
	DECLARE @FechaFolio char(13)
	DECLARE @Verificador int
	DECLARE @Pares int
	DECLARE @Impares int
	DECLARE @Modulo int
	DECLARE @HomoclaveTecnologia varchar(20)

	SET @FechaJuliana = (SELECT RIGHT(CAST(YEAR(GETDATE()) AS CHAR(4)),2) + RIGHT('000' + CAST(DATEPART(dy, getdate()) AS varchar(3)),3))

	INSERT INTO CatProyectos
	(
		IdEmpresa,
		IdTecnologia,
		IdMunicipio,
		IdEstatusProyecto,
		Nombre,
		Latitud,
		Longitud,
		CapacidadInstalada,
		GeneracionAnual,
		FactorPlanta,
		MontoInversion,
		IdGlobal,
		Activo,
		FechaRegistro,
		IdLocalidad,
		IdTipoColonia,
		Colonia
	)
	values
	(
		@IdEmpresa,
		@IdTecnologia,
		@IdMunicipio,
		1,
		@Nombre,
		@Latitud,
		@Longitud,
		@CapacidadInstalada,
		@GeneracionAnual,
		@FactorPlanta,
		@MontoInversion,
		'AAA',
		1,
		@FechaActual,
		@IdLocalidad,
		@IdTipoColonia,
		@Colonia
	)

	set @IdProyectoAsignado = (Select IdProyecto from dbo.CatProyectos where Nombre = @Nombre and FechaRegistro = @FechaActual and IdEmpresa = @IdEmpresa)
	set @Empresa = (Select NombreComercial from dbo.CatEmpresas where IdEmpresa = @IdEmpresa)
	set @Tecnologia = (Select Tecnologia from dbo.CatTecnologias where IdTecnologia = @IdTecnologia)
	set @Municipio = (Select Municipio from dbo.CatMunicipios where IdMunicipio = @IdMunicipio)

	SET @FechaJuliana = (SELECT RIGHT(CAST(YEAR(GETDATE()) AS CHAR(4)),2) + RIGHT('000' + CAST(DATEPART(dy, getdate()) AS varchar(3)),3))
	SET @Folio = RIGHT('00000000' + (CAST(@IdProyectoAsignado AS VARCHAR(8))),8)
	SET @FechaFolio = CONCAT(@FechaJuliana,@Folio)
	SET @Pares = (SELECT  SUM( cast(SUBSTRING(Col, 2, 1) as int) + cast(SUBSTRING(Col, 4, 1) as int) + cast(SUBSTRING(Col, 6, 1) as int)
								+ cast(SUBSTRING(Col, 8, 1) as int) + cast(SUBSTRING(Col, 10, 1) as int) + cast(SUBSTRING(Col, 12, 1) as int))
					FROM (VALUES (@FechaFolio)) t (Col)
					)

	SET @Impares = (SELECT  SUM( cast(SUBSTRING(Col, 1, 1) as int) + cast(SUBSTRING(Col, 3, 1) as int) + cast(SUBSTRING(Col, 5, 1) as int) 
								+ cast(SUBSTRING(Col, 7, 1) as int) + cast(SUBSTRING(Col, 9, 1) as int) + cast(SUBSTRING(Col, 11, 1) as int) + cast(SUBSTRING(Col, 13, 1) as int))
					FROM (VALUES (@FechaFolio)) t (Col)
					)

	SET @Verificador = SUM(@Pares + (@Impares * 3))
	SET @Modulo = (@Verificador % 10)
	IF @Modulo != 0
		SET @Verificador = 10 - @Modulo

	SET @HomoclaveTecnologia = (SELECT Homoclave FROM dbo.CatTecnologias WHERE IdTecnologia = @IdTecnologia)

	SET @IdGlobal = CONCAT(@HomoclaveTecnologia, ' ', @FechaJuliana, ' ', @Folio, ' ', @Verificador )

	UPDATE dbo.CatProyectos SET IdGlobal = @IdGlobal WHERE IdProyecto = @IdProyectoAsignado



INSERT INTO [dbo].[ProyectosINERE]
           ([NombreProyecto]
           ,[NombreEmpresa]
           ,[Tecnologia]
           ,[Tecnologia2]
           ,[Municipio]
           ,[Latitud]
           ,[Longitud]
           ,[CapacidadInstalada]
           ,[Unidades]
           ,[GeneracionAnual]
           ,[FactorPlanta]
           ,[MontoInversion]
           ,[FechaInscripcion]
           ,[Fase]
           ,[EstadoProyecto])
     VALUES
           (@Nombre
           ,@Empresa
           ,@Tecnologia
           ,'NA'
           ,@Municipio
           ,@Latitud
           ,@Longitud
           ,@CapacidadInstalada
           ,'MW'
           ,@GeneracionAnual
           ,@FactorPlanta
           ,@MontoInversion
           ,@FechaActual
           ,'NA'
           ,'REGISTRADO')

INSERT INTO [dbo].[ProyectoTramites]
           ([IdProyecto]
           ,[IdTecnologiaTamite]
           ,[IdEstatusTramite]
           ,[FechaInicio]
           ,[Resolutivo]
           ,[Observacion]
           ,[FechaRegistro]
		   ,FechaFinLegal
		   ,FechaFinReal
           ,[Activo])
 SELECT
           @IdProyectoAsignado
           ,IdTecnologiaTamite
           ,1
           ,@FechaActual
           ,'NA'
           ,'NA'
           ,@FechaActual
		   ,DATEADD (DAY , CT.TiempoResolucion , @FechaActual )
		   ,DATEADD (DAY , CT.TiempoResolucion , @FechaActual )
		   ,1
		   FROM dbo.TecnologiaTramites TT
		   inner join dbo.CatTramites CT
		   ON TT.IdTramite = CT.IdTramite

		   WHERE IdTecnologia = @IdTecnologia and TT.Activo = 1;

INSERT INTO [dbo].[ProyectoPreguntas]
           ([IdProyecto]
		   ,[IdPregunta]
           ,[Respuesta]
           ,[FechaRegistro]
		   ,Modificar
		   )
 SELECT
           @IdProyectoAsignado
           ,TP.IdTecnologiaPregunta
           ,0
           ,@FechaActual
		   ,1
           
		   FROM dbo.TecnologiaPreguntas TP
		   WHERE  TP.IdTecnologia = @IdTecnologia and Activo = 1;

INSERT INTO [dbo].[ProyectoSeguimiento]
	(IdProyecto
	,IdEstatusProyecto
	,FechaRegistro)
VALUES
	(@IdProyectoAsignado
	,1
	,GETDATE())


SELECT @IdProyectoAsignado;




SET NOCOUNT OFF;
END











GO
/****** Object:  StoredProcedure [dbo].[SpInsertarRepresentanteLegal]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertarRepresentanteLegal] 
	@Nombre varchar(100)
    ,@PrimerApellido varchar(100)
    ,@SegundoApellido varchar(100)
    ,@CURP varchar(100)
    ,@RFC varchar(100)
    ,@IdTipoCalle int
    ,@Calle varchar(100)
    ,@NumeroExterior int 
    ,@NumeroInterior varchar(5) = null
    ,@IdTipoColonia int
    ,@Colonia varchar(100)
    ,@IdLocalidad int
    ,@CodigoPostal int = null
    ,@Lada int = null
    ,@TelefonoFijo int
    ,@ExtensionTelefonica int = null
    ,@TelefonoMovil bigint
    ,@CorreoElectronico varchar(100)
    ,@IdEmpresa int
    ,@Activo bit
	,@IdEstatusSolicitudRepresentante int
	,@IdRepresentanteLegal int out
AS
BEGIN
	DECLARE @FechaActual datetime;
	set @FechaActual = GETDATE();

	INSERT INTO dbo.CatRepresentantesLegales
           (Nombre
           ,PrimerApellido
           ,SegundoApellido
           ,CURP
           ,RFC
           ,IdTipoCalle
           ,Calle
           ,NumeroExterior
           ,NumeroInterior
           ,IdTipoColonia
           ,Colonia
           ,IdLocalidad
           ,CodigoPostal
           ,Lada
           ,TelefonoFijo
           ,ExtensionTelefonica
           ,TelefonoMovil
           ,CorreoElectronico
           ,IdEmpresa
           ,FechaSolicitud
           ,Activo
           ,IdEstatusSolicitudRepresentante)
     VALUES
	 (
		 @Nombre
		,@PrimerApellido
		,@SegundoApellido
		,@CURP
		,@RFC
		,@IdTipoCalle
		,@Calle
		,@NumeroExterior
		,@NumeroInterior
		,@IdTipoColonia
		,@Colonia
		,@IdLocalidad
		,@CodigoPostal
		,@Lada
		,@TelefonoFijo
		,@ExtensionTelefonica
		,@TelefonoMovil
		,@CorreoElectronico
		,@IdEmpresa
		,@FechaActual
		,@Activo
		,1
	 )

	SET @IdRepresentanteLegal = 
	(	SELECT 
			IdRepresentanteLegal 
		FROM 
			dbo.CatRepresentantesLegales 
		where 
			IdEmpresa = @IdEmpresa
			and FechaRegistro = @FechaActual
			)
END






GO
/****** Object:  StoredProcedure [dbo].[SpInsertarTecnologiaPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertarTecnologiaPreguntas]
	@IdTecnologia int
	,@Pregunta varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].TecnologiaPreguntas
           (IdTecnologia
           ,Pregunta
		   ,Activo)
     VALUES
           (@IdTecnologia,@Pregunta, 1)
END



GO
/****** Object:  StoredProcedure [dbo].[SpInsertarTecnologias]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertarTecnologias]
	@NombreTecnologia varchar(100)
	,@IdTecnologia int out
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[CatTecnologias]
           (Tecnologia, Activo)
     VALUES
           (@NombreTecnologia, 1)

	SET @IdTecnologia = 
	(	SELECT top 1
			IdTecnologia 
		FROM 
			dbo.CatTecnologias
		order by IdTecnologia desc
	)
END



GO
/****** Object:  StoredProcedure [dbo].[SpInsertarTecnologiaTramite]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertarTecnologiaTramite]

    @IdTecnologia int
    ,@IdFase int
    ,@IdTramite int
    ,@IdPosicion int
    
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IDSiguiente int
	SET @IDSiguiente = (select max(IdTecnologiaTamite) from DBO.TecnologiaTramites ) + 1

	INSERT INTO [dbo].[TecnologiaTramites]
           ([IdTecnologiaTamite]
           ,[IdTecnologia]
           ,[IdFase]
           ,[IdTramite]
           ,[IdPosicion]
		   ,[Activo])
     VALUES
           (@IDSiguiente
           ,@IdTecnologia
           ,@IdFase
           ,@IdTramite
           ,@IdPosicion
		   ,1)
END



GO
/****** Object:  StoredProcedure [dbo].[SpInsertarTramite]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-16
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertarTramite]
    @Nombre varchar(300)
    ,@Homoclave varchar(20)
    ,@TiempoRecepcionDocumentos int
    ,@TiempoResolucion int
    ,@Descripcion varchar(300)
    ,@RegistroCOFEMER varchar(50)
    ,@URLTramites varchar(250)
    ,@URLRequisitos varchar(250)
    ,@Dependencia varchar(20)
    ,@CorreoResponsable varchar(50)
    ,@CorreoSuperior varchar(50)
	,@PorcentajeAlertaA int
	,@PorcentajeAlertaB int
	,@PorcentajeAlertaC int
	,@IdTipoDias int
	,@IdTramite int out
    
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @IDSiguiente int
	SET @IDSiguiente = (select max(IdTramite) from DBO.CatTramites ) + 1

	INSERT INTO [dbo].[CatTramites]
           (IdTramite
           ,Nombre
           ,Homoclave
           ,TiempoRecepcionDocumentos
           ,TiempoResolucion
           ,Descripcion
           ,RegistroCOFEMER
           ,URLTramites
           ,URLRequisitos
           ,Dependencia
           ,CorreoResponsable
           ,CorreoSuperior
           ,Activo
		   ,PorcentajeAlertaA 
		   ,PorcentajeAlertaB 
		   ,PorcentajeAlertaC
		   ,IdTipoDia
		  )
     VALUES
           (@IDSiguiente
		    ,@Nombre
			,@Homoclave
			,@TiempoRecepcionDocumentos
			,@TiempoResolucion
			,@Descripcion
			,@RegistroCOFEMER
			,@URLTramites
			,@URLRequisitos
			,@Dependencia
			,@CorreoResponsable
			,@CorreoSuperior
			,1
			,@PorcentajeAlertaA
			,@PorcentajeAlertaB
			,@PorcentajeAlertaC
			,@IdTipoDias
		   )

		SET @IdTramite = 
	(	SELECT top 1
			IdTramite 
		FROM 
			dbo.CatTramites
		order by IdTramite desc
	)
END



GO
/****** Object:  StoredProcedure [dbo].[SpInsertarUsuario]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-21
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertarUsuario]
	@IdEmpresa int
    ,@IdTipoUsuario int
    ,@Nombre varchar(100)
    ,@Password varchar(300)
    ,@CorreoElectronico varchar(100)
	,@IdRepresentanteAsociado int
    ,@Activo bit
	,@CreadoDuranteRegistro bit
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FechaActual datetime;
	set @FechaActual = GETDATE();

    INSERT INTO dbo.CatUsuarios
           (IdEmpresa
           ,IdTipoUsuario
           ,Nombre
           ,Password
           ,CorreoElectronico
		   ,IdRepresentanteAsociado
           ,FechaRegistro
           ,Activo
		   ,CreadoDuranteRegistro)
     VALUES
	 (
		@IdEmpresa
		,@IdTipoUsuario
		,@Nombre
		,@Password
		,@CorreoElectronico
		,@IdRepresentanteAsociado
		,@FechaActual
		,@Activo
		,@CreadoDuranteRegistro
	 )
END







GO
/****** Object:  StoredProcedure [dbo].[SpInsertarUsuarioSENER]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-21
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpInsertarUsuarioSENER]
	@IdEmpresa int
    ,@IdTipoUsuario int
    ,@Nombre varchar(100)
    ,@Password varchar(300)
    ,@CorreoElectronico varchar(100)
    ,@Activo bit
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FechaActual datetime;
	set @FechaActual = GETDATE();

    INSERT INTO dbo.CatUsuarios
           (IdEmpresa
           ,IdTipoUsuario
           ,Nombre
           ,Password
           ,CorreoElectronico
           ,FechaRegistro
           ,Activo)
     VALUES
	 (
		@IdEmpresa
		,@IdTipoUsuario
		,@Nombre
		,@Password
		,@CorreoElectronico
		,@FechaActual
		,@Activo
	 )
END







GO
/****** Object:  StoredProcedure [dbo].[spReporteEmpresas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  JARN
-- Create date: 2016-03-18
-- Description: Reporte de Empresas con parámetros opcionales
-- =============================================

CREATE PROCEDURE [dbo].[spReporteEmpresas]
 -- Add the parameters for the stored procedure here
  @IdEntidadFederativa int =null
 ,@IdMunicipio int =null
 ,@fechainicio datetime =null
 ,@fechafin datetime =null
  ,@IdTecnologia int=null
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT distinct
 CEF.EntidadFederativa NombreEntidad,
 CM.Municipio  NombreMunicipio,
 --CT.Tecnologia,
 CE.RazonSocial, 
 CE.RFC,
 CE.CorreoElectronico, 
 CE.fecharegistro,
 SUM(CP.MontoInversion) MontoInversion
 FROM DBO.CatEmpresas CE
 inner join dbo.CatLocalidades CL
on CE.IdLocalidad = CL.IdLocalidad
INNER JOIN CatEntidadesFederativas CEF
on CEF.IdEntidadFederativa = CL.IdEntidadFederativa
INNER JOIN CatMunicipios CM
on CL.IdMunicipio = CM.IdMunicipioINEGI
INNER JOIN dbo.CatProyectos CP
 on CP.IdEmpresa=CE.IdEmpresa
 INNER JOIN dbo.CatTecnologias CT
 on CT.IdTecnologia=CP.IdTecnologia
 
  WHERE 
  (CM.IdMunicipio IS NULL OR CM.IdMunicipio = ISNULL(@IdMunicipio, CM.IdMunicipio) )
  AND (CEF.IdEntidadFederativa IS NULL or CEF.IdEntidadFederativa = ISNULL(@IdEntidadFederativa, CEF.IdEntidadFederativa) )
  AND ((@fechainicio IS NULL) or CE.fecharegistro >= @fechainicio)
  AND ((@fechafin IS NULL) or CE.fecharegistro <= @fechafin)
  AND (CT.IdTecnologia IS NULL or CT.IdTecnologia = ISNULL(@IdTecnologia, CT.IdTecnologia) )
  and CE.Activo=1
  AND ce.IdEmpresa !=1	
  AND CM.IdEntidadFederativa = CEF.IdEntidadFederativa	 


  Group by CEF.EntidadFederativa, CM.Municipio, CE.RazonSocial, CE.RFC, CE.CorreoElectronico, CE.FechaRegistro

  ORDER BY CEF.EntidadFederativa, CM.Municipio, CE.RFC, CE.CorreoElectronico, CE.FechaRegistro DESC 
END



GO
/****** Object:  StoredProcedure [dbo].[spReporteMontoInversion]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
-- =============================================
-- Author:  PAMA
-- Create date: 2016-03-30
-- Description: Reporte de Empresas con parámetros opcionales
-- =============================================

CREATE PROCEDURE [dbo].[spReporteMontoInversion]
  @IdEntidadFederativa int =null
 ,@IdMunicipio int =null
 ,@fechainicio datetime =null
 ,@fechafin datetime =null
 ,@IdTecnologia int=null
 ,@IdEmpresa int=null
AS
BEGIN
 -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.
 SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT 
 CM.Municipio NombreMunicipio,
  CEF.EntidadFederativa NombreEntidad,
 CE.NombreComercial NombreEmpresa,
 CE.RazonSocial, 
 CE.RFC,
 SUM(CP.MontoInversion) MontoInversion
 FROM DBO.CatEmpresas CE
 inner join dbo.CatLocalidades CL
	on CE.IdLocalidad = CL.IdLocalidad
	INNER JOIN CatEntidadesFederativas CEF
	on CEF.IdEntidadFederativa = CL.IdEntidadFederativa
	INNER JOIN CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI
 INNER JOIN dbo.CatProyectos CP
 on CP.IdEmpresa=CE.IdEmpresa
 INNER JOIN dbo.CatTecnologias CT
 on CT.IdTecnologia=CP.IdTecnologia
 
  WHERE 
  (CL.IdMunicipio IS NULL OR CL.IdMunicipio = ISNULL(@IdMunicipio, CL.IdMunicipio) )
  AND (CM.IdEntidadFederativa IS NULL or CM.IdEntidadFederativa = ISNULL(@IdEntidadFederativa, CM.IdEntidadFederativa) )
  AND (CT.IdTecnologia IS NULL or CT.IdTecnologia = ISNULL(@IdTecnologia, CT.IdTecnologia) )
  AND (CE.IdEmpresa IS NULL or CE.IdEmpresa = ISNULL(@IdEmpresa, CE.IdEmpresa) )
  AND ((@fechainicio IS NULL) or CE.fecharegistro >= @fechainicio)
  AND ((@fechafin IS NULL) or CE.fecharegistro <= @fechafin)
  and CE.Activo=1
  and CM.IdEntidadFederativa = CEF.IdEntidadFederativa

  GROUP BY CE.RFC,CE.NombreComercial,CE.RazonSocial, CEF.EntidadFederativa, CM.Municipio
END





GO
/****** Object:  StoredProcedure [dbo].[SpReporteProyecto]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:  Panuncio Andres Moreno Arguelles
-- Create date: Jueves 26 de Febrero del 2016
-- Description: Obtiene el listado de proyectos activos con filtros
-- Versión:  1.0.0
-- Modificed: 
-- Modified by: 
-- Comments: 
-- =======================================================


CREATE PROCEDURE [dbo].[SpReporteProyecto]
(
@IdEmpresa int =null,
@IdEntidadFederativa int= null,
@IdMunicipio int=null,
@fechainicio datetime =null,
@fechafin datetime =null,
@IdEstatusProyecto int=null
)
AS
BEGIN
 SET NOCOUNT ON;
SELECT  
 CEF.IdEntidadFederativa,
 CEF.EntidadFederativa as NombreEntidadF,
 dbo.CatProyectos.IdEmpresa,
 dbo.CatEmpresas.NombreComercial as NombreEmpresa,
 dbo.CatProyectos.IdTecnologia,
 dbo.CatTecnologias.Tecnologia as Tecnologia,
 dbo.CatProyectos.IdMunicipio,
 CM.Municipio as Municipio,
 dbo.CatEstatusProyecto.EstatusProyecto as EstadoProyecto,
 dbo.CatProyectos.Nombre as NombreProyecto,
 dbo.CatProyectos.Latitud,
 dbo.CatProyectos.Longitud,
 dbo.CatProyectos.CapacidadInstalada,
 dbo.CatProyectos.GeneracionAnual,
 dbo.CatProyectos.FactorPlanta,
 dbo.CatProyectos.MontoInversion,
 dbo.CatProyectos.FechaRegistro,
 dbo.CatProyectos.Activo,
 dbo.CatProyectos.IdProyecto
 FROM     dbo.CatProyectos
 Inner join
 dbo.CatEmpresas
 on dbo.CatEmpresas.IdEmpresa = dbo.CatProyectos.IdEmpresa
 	inner join dbo.CatLocalidades CL
	on CatProyectos.IdLocalidad = CL.IdLocalidad
	inner join dbo.CatEntidadesFederativas CEF
	on CEF.IdEntidadFederativa = CL.IdEntidadFederativa
	INNER join dbo.CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI
 inner join dbo.CatTecnologias
 on dbo.CatProyectos.IdTecnologia=dbo.CatTecnologias.IdTecnologia
 inner join dbo.CatEstatusProyecto
 on dbo.CatProyectos.IdEstatusProyecto=dbo.CatEstatusProyecto.idEstatusProyecto
 and dbo.CatProyectos.Activo=1
  WHERE 
  (dbo.CatEmpresas.IdEmpresa IS NULL OR dbo.CatEmpresas.IdEmpresa = ISNULL(@IdEmpresa, dbo.CatEmpresas.IdEmpresa) )
  AND (dbo.CatEstatusProyecto.idEstatusProyecto IS NULL OR dbo.CatEstatusProyecto.idEstatusProyecto = ISNULL(@IdEstatusProyecto,dbo.CatEstatusProyecto.idEstatusProyecto) )
  AND (CM.IdMunicipio IS NULL or  CM.IdMunicipio= ISNULL(@IdMunicipio,  CM.IdMunicipio) )
  AND ( CM.IdEntidadFederativa IS NULL or  CM.IdEntidadFederativa = ISNULL(@IdEntidadFederativa,  CM.IdEntidadFederativa) )
  AND ((@fechainicio IS NULL) or dbo.CatProyectos.fecharegistro >= @fechainicio)
  AND ((@fechafin IS NULL) or dbo.CatProyectos.fecharegistro <= @fechafin)
  and  dbo.CatEmpresas.IdEmpresa != 1
  and CM.IdEntidadFederativa = CL.IdEntidadFederativa

SET NOCOUNT OFF;
END






GO
/****** Object:  StoredProcedure [dbo].[SpReporteProyectos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-06
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpReporteProyectos]
	@IdEmpresa int =null,
	@IdTecnologia int = null,
	@IdEntidadFederativa int= null,
	@IdMunicipio int=null,
	@Fechainicio datetime =null,
	@Fechafin datetime =null,
	@IdEstatusProyecto int=null
AS
BEGIN
	SET NOCOUNT ON;

    SELECT  
		 CEF.EntidadFederativa,
		 CE.NombreComercial as NombreEmpresa,
		 CT.Tecnologia,
		 CM.Municipio,
		 CEP.EstatusProyecto,
		 CP.Nombre as NombreProyecto,
		 CP.MontoInversion,
		 CP.FechaRegistro,
		 CP.IdProyecto
	FROM  CatProyectos CP
		INNER JOIN CatEmpresas CE
		on CE.IdEmpresa = CP.IdEmpresa
		INNER JOIN CatLocalidades  CL
		on CP.IdLocalidad = CL.IdLocalidad
		INNER JOIN CatEntidadesFederativas CEF
		on CEF.IdEntidadFederativa = CL.IdEntidadFederativa
		INNER JOIN CatMunicipios CM
		on CL.IdMunicipio = CM.IdMunicipioINEGI
		INNER JOIN CatTecnologias CT
		on CP.IdTecnologia= CT.IdTecnologia
		INNER JOIN CatEstatusProyecto CEP
		on CP.IdEstatusProyecto = CEP.idEstatusProyecto

	WHERE 
	  (CE.IdEmpresa IS NULL OR CE.IdEmpresa = ISNULL(@IdEmpresa, CE.IdEmpresa) )
	  AND (CEP.idEstatusProyecto IS NULL OR CEP.idEstatusProyecto = ISNULL(@IdEstatusProyecto,CEP.idEstatusProyecto) )
	  AND (CP.IdTecnologia IS NULL OR CP.IdTecnologia = ISNULL(@IdTecnologia,CP.IdTecnologia) )
	  AND (CM.IdMunicipio IS NULL or  CM.IdMunicipio= ISNULL(@IdMunicipio,  CM.IdMunicipio) )
	  AND ( CM.IdEntidadFederativa IS NULL or  CM.IdEntidadFederativa = ISNULL(@IdEntidadFederativa,  CM.IdEntidadFederativa) )
	  AND ((@Fechainicio IS NULL) or CP.fecharegistro >= @Fechainicio)
	  AND ((@Fechafin IS NULL) or CP.fecharegistro <= @Fechafin)
	  AND  CE.IdEmpresa != 1
	  AND CM.IdEntidadFederativa = CEF.IdEntidadFederativa
END




GO
/****** Object:  StoredProcedure [dbo].[SpReporteTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SpReporteTramites] 
 -- Add the parameters for the stored procedure here
 @IdProyecto int
AS
BEGIN
 SET NOCOUNT ON;

 SELECT 
  CT.idTramite
  ,CT.Homoclave
  ,CT.Nombre NombreTramite
  ,PT.FechaInicio
  ,ET.Nombre Estatus
  ,PT.FechaFinLegal
  ,CT.URLTramites
  ,CT.URLRequisitos
  ,CP.Nombre nombreProyecto
  FROM [dbo].[ProyectoTramites] PT
  INNER JOIN dbo.TecnologiaTramites TT
  ON PT.IdTecnologiaTamite = TT.IdTecnologiaTamite
  INNER JOIN dbo.CatTramites CT
  ON TT.IdTramite = CT.IdTramite
  INNER JOIN dbo.CatEstatusTramite ET
  ON PT.IdEstatusTramite = ET.IdEstatusTramite
  inner join [dbo].[CatProyectos] CP
  on CP.IdProyecto=PT.IdProyecto
  WHERE PT.IdProyecto = @IdProyecto

  END




GO
/****** Object:  StoredProcedure [dbo].[SpResetProyectoPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016/05/24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpResetProyectoPreguntas]
	@IdProyecto int

AS
BEGIN
	SET NOCOUNT ON;
	UPDATE dbo.ProyectoPreguntas
	SET
           [Respuesta] = 0
           ,[FechaModificacion] = GETDATE()
	WHERE 
		IdProyecto = @IdProyecto

END




GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarAlertasPorTramite]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-06-09
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarAlertasPorTramite] 
	-- Add the parameters for the stored procedure here
	@Homoclave varchar(30)
	,@IdProyecto int
AS
BEGIN

	SELECT TOP 1 
		(DATEDIFF (DAY,FechaInicio,FechaFinReal)) AS DiasTotales
		,(DATEDIFF (DAY,FechaInicio,GETDATE())) as DiasTranscurridos
		,PorcentajeAlertaA
		,PorcentajeAlertaB
		,PorcentajeAlertaC

	FROM dbo.ProyectoTramites PT
	INNER JOIN dbo.TecnologiaTramites TT
	ON TT.IdTecnologiaTamite = PT.IdTecnologiaTamite
	INNER JOIN dbo.CatTramites CT
	ON CT.IdTramite = TT.IdTramite
	WHERE
		PT.IdProyecto = @IdProyecto
		AND CT.Homoclave = @Homoclave
	ORDER BY FechaRegistro DESC
	
END





GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarAvanceTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarAvanceTramites] 
	@IdProyecto int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT count(PT.IdProyectoTamite) as CantidadTramites1 
	FROM dbo.ProyectoTramites PT 
	where IdProyecto = @IdProyecto and IdEstatusTramite = 7

	UNION

	SELECT count( PT.IdTecnologiaTamite) as CantidadTramites2
	FROM dbo.ProyectoTramites PT 
	where IdProyecto = @IdProyecto 
END





GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarCodigosPostales]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarCodigosPostales]
	-- Parametros
	@IdCodigoPostal int = null
AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		IdCodigoPostal
	FROM dbo.CatCodigosPostales
	WHERE 
		(IdCodigoPostal IS NULL OR IdCodigoPostal = ISNULL(@IdCodigoPostal, IdCodigoPostal))
	ORDER BY IdCodigoPostal asc

END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarDiasInhabiles]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  JARN
-- Create date: 2016-02-22
-- Description: Obtener los datos de un usario
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarDiasInhabiles]
	@Mes int
	,@Año int
AS

BEGIN
 SET NOCOUNT ON;

 SELECT
	[IdDiaInhabil]
    ,[Dia]
    ,[Mes]
    ,[Año]
    ,[FechaCompleta]
    ,[Activo]
  FROM dbo.CatDiasInhabiles CDI
  WHERE 
	CDI.Activo = 1


  ORDER BY CDI.Año, CDI.Mes, CDI.Dia

END








GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarEmpresas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-05
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarEmpresas] 
	
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [IdEmpresa]
      ,[NombreComercial]
      ,[RazonSocial]
      ,[RFC]
      ,[FechaRegistro]
      ,[FechaSolicitud]
      ,[FechaModificacion]
      ,[Activo]
  FROM [dbo].[CatEmpresas]
END




GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarEntidadesFederativas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarEntidadesFederativas] 
	@IdEntidadFederativa int = null
AS
BEGIN
	SET NOCOUNT ON;

    SELECT 
		IdEntidadFederativa, EntidadFederativa
	FROM dbo.CatEntidadesFederativas
	WHERE
		(IdEntidadFederativa IS NULL OR IdEntidadFederativa = ISNULL(@IdEntidadFederativa, IdEntidadFederativa))
	ORDER BY
		EntidadFederativa asc
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarEntidadesFederativasPorCP]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-21
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarEntidadesFederativasPorCP]
	@IdCodigoPostal int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		EFCP.IdEntidadFederativa
		,CEF.EntidadFederativa
	FROM
		dbo.EntidadFederativa_CodigoPostal EFCP
	INNER JOIN dbo.CatEntidadesFederativas CEF
	ON CEF.IdEntidadFederativa = EFCP.IdEntidadFederativa
	WHERE
		EFCP.CPFinal >= @IdCodigoPostal
		AND EFCP.CPInicial <= @IdCodigoPostal
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarEstatusProyecto]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-05
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarEstatusProyecto]
	
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
	   [idEstatusProyecto]
      ,[EstatusProyecto]
  FROM [dbo].[CatEstatusProyecto]
END




GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarEstatusTramite]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-06-09
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarEstatusTramite] 
	-- Add the parameters for the stored procedure here
	@Homoclave varchar(30)
	,@IdProyecto int
	,@IdEstatus int out
AS
BEGIN
	SET NOCOUNT ON;
	SET @IdEstatus = (
		SELECT TOP 1 IdEstatusTramite
		FROM dbo.ProyectoTramites PT
		INNER JOIN dbo.TecnologiaTramites TT
		ON TT.IdTecnologiaTamite = PT.IdTecnologiaTamite
		INNER JOIN dbo.CatTramites CT
		ON CT.IdTramite = TT.IdTramite
		WHERE
			PT.IdProyecto = @IdProyecto
			AND CT.Homoclave = @Homoclave
		ORDER BY FechaRegistro DESC
	)
	
END





GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarEstatusTramiteCompletos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Jueves 26 de Febrero del 2016
-- Description:	Obtiene el listado de proyectos asociados a una empresa
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


create PROCEDURE [dbo].[SpSeleccionarEstatusTramiteCompletos]

AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM dbo.CatEstatusTramite

SET NOCOUNT OFF;
END




















GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarLocalidades]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarLocalidades] 
	@IdEntidadFederativa int 
	,@IdMunicipio int
	,@IdLocalidad int = null
AS
BEGIN
	SET NOCOUNT ON;

    SELECT 
		IdLocalidad, Localidad
	FROM dbo.CatLocalidades CL
	INNER JOIN dbo.CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI
	WHERE
		--(CL.IdLocalidad IS NULL OR CL.IdLocalidad = ISNULL(@IdLocalidad, CL.IdLocalidad))
		--AND 
		CM.IdMunicipio = @IdMunicipio
		AND CL.IdEntidadFederativa = @IdEntidadFederativa
	ORDER BY 
		IdLocalidad
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarLocalidadesPorCP]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarLocalidadesPorCP]
	@IdEntidadFederativa int 
	,@IdMunicipioINEGI int
	,@IdLocalidadINEGI int 
AS
BEGIN
	SET NOCOUNT ON;

    SELECT 
		IdLocalidad, Localidad
	FROM dbo.CatLocalidades CL
	INNER JOIN dbo.CatMunicipios CM
	on CL.IdMunicipio = CM.IdMunicipioINEGI
	WHERE
		CL.IdLocalidad = @IdLocalidadINEGI
		AND CM.IdMunicipio = @IdMunicipioINEGI
		AND CL.IdEntidadFederativa = @IdEntidadFederativa
	ORDER BY
		Localidad
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarMunicipios]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarMunicipios] 
	-- Add the parameters for the stored procedure here
	@IdEntidadFederativa int = null
	,@IdMunicipio int = null
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		IdMunicipio
		,Municipio
	FROM
		dbo.CatMunicipios
	WHERE
		(IdEntidadFederativa IS NULL OR IdEntidadFederativa = ISNULL(@IdEntidadFederativa,IdEntidadFederativa))
		AND(IdMunicipio IS NULL OR IdMunicipio = ISNULL(@IdMunicipio,IdMunicipio))
	ORDER BY 
		Municipio
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarMunicipiosPorCP]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarMunicipiosPorCP]
	-- Add the parameters for the stored procedure here
	@IdEntidadFederativa int = null
	,@IdMunicipio int = null
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		IdMunicipio
		,Municipio
	FROM
		dbo.CatMunicipios
	WHERE
		IdEntidadFederativa = @IdEntidadFederativa
		AND IdMunicipioINEGI = @IdMunicipio
	ORDER BY 
		Municipio
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarPreguntas] 
	-- Add the parameters for the stored procedure here
	@IdTecnologia int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
		IdTecnologiaPregunta
      ,Pregunta

  FROM dbo.TecnologiaPreguntas
  where IdTecnologia = @IdTecnologia

  order by IdTecnologiaPregunta
END







GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarProyectos]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Jueves 26 de Febrero del 2016
-- Description:	Obtiene el listado de proyectos asociados a una empresa
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


CREATE PROCEDURE [dbo].[SpSeleccionarProyectos]
(
@IdEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT  
	dbo.CatEntidadesFederativas.IdEntidadFederativa,
	dbo.CatEntidadesFederativas.EntidadFederativa as EntidadFederativa,
	dbo.CatProyectos.IdEmpresa,
	dbo.CatEmpresas.NombreComercial as NombreEmpresa,
	dbo.CatProyectos.IdTecnologia,
	dbo.CatTecnologias.Tecnologia as Tecnologia,
	dbo.CatProyectos.IdMunicipio,
	dbo.CatMunicipios.Municipio as Municipio,
	dbo.CatEstatusProyecto.EstatusProyecto,
	dbo.CatProyectos.Nombre as NombreProyecto,
	dbo.CatProyectos.Latitud,
	dbo.CatProyectos.Longitud,
	dbo.CatProyectos.CapacidadInstalada,
	dbo.CatProyectos.GeneracionAnual,
	dbo.CatProyectos.FactorPlanta,
	dbo.CatProyectos.MontoInversion,
	dbo.CatProyectos.FechaRegistro,
	dbo.CatProyectos.IdGlobal,
	dbo.CatProyectos.Activo,
	dbo.CatProyectos.IdProyecto
	FROM     dbo.CatProyectos
	Inner join
	dbo.CatEmpresas
	on dbo.CatEmpresas.IdEmpresa=@IdEmpresa
	inner join dbo.CatMunicipios
	on dbo.CatProyectos.idMunicipio=dbo.CatMunicipios.idMunicipio
	inner join dbo.CatTecnologias
	on dbo.CatProyectos.IdTecnologia=dbo.CatTecnologias.IdTecnologia
	inner join dbo.CatEstatusProyecto
	on dbo.CatProyectos.IdEstatusProyecto=dbo.CatEstatusProyecto.idEstatusProyecto
	inner join dbo.CatEntidadesFederativas
	on dbo.CatMunicipios.IdEntidadFederativa=dbo.CatEntidadesFederativas.IdEntidadFederativa
	and dbo.CatProyectos.IdEmpresa=@IdEmpresa
	and dbo.CatProyectos.Activo=1
SET NOCOUNT OFF;
END




















GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarProyectoSeguimiento]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-24
-- Description:	
-- =============================================
CREATE	PROCEDURE [dbo].[SpSeleccionarProyectoSeguimiento]
	@IdProyecto int
	,@IdSeguimiento int out
AS
BEGIN
	SET NOCOUNT ON;

	SET @IdSeguimiento = 
	(SELECT top 1
      [IdEstatusProyecto]
	FROM [dbo].[ProyectoSeguimiento]
	WHERE 
		IdProyecto = @IdProyecto
		
	ORDER BY FechaRegistro DESC)
END




GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarProyectoTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarProyectoTramites] 
	@IdProyecto int

AS
BEGIN
	SET NOCOUNT ON;

	SELECT 

		TT.IdFase
		,Max(CT.idTramite) IdTramite
		,CT.Homoclave
		--,COUNT(CT.Homoclave)
		,Max(CT.Nombre) NombreTramite
		,max(PT.IdEstatusTramite) IdEstatus
		,Max(ET.Nombre )Estatus
		,COUNT(ET.Nombre) Nombre
		--,ET.Color Color
		,Max(Et.Color) Color
		,Max(PT.FechaInicio) FechaInicio
		,Max(PT.FechaFinReal) FechaFinReal
		,Max(CT.URLTramites) URLTramites
		,Max(CT.URLRequisitos) URLRequisitos
		,Max(PT.FechaRegistro) FechaRegistro
		,Dependencia
		,Max(CC.PosicionX) PosicionX
		,Max(CC.PosicionY) PosicionY
		,Max(PT.DiasAgregados) DiasAgregados

  FROM [dbo].[ProyectoTramites] PT
  INNER JOIN dbo.TecnologiaTramites TT
  ON PT.IdTecnologiaTamite = TT.IdTecnologiaTamite
  INNER JOIN dbo.CatCoordenadas CC
  on TT.IdPosicion = CC.IdCoordenadas
  INNER JOIN dbo.CatTramites CT
  ON TT.IdTramite = CT.IdTramite
  INNER JOIN dbo.CatEstatusTramite ET
  ON PT.IdEstatusTramite = ET.IdEstatusTramite

  WHERE 
	PT.IdProyecto = @IdProyecto
	and PT.IdEstatusTramite >= 1
	AND IdFase < 7

  GROUP BY 	TT.IdFase, CT.Homoclave, CT.Dependencia

order by TT.IdFase, Ct.Dependencia asc



END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarRepresentantesLegalesPorEmpresa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarRepresentantesLegalesPorEmpresa] 
	@IdEmpresa int

AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		CE.NombreComercial
		,IdRepresentanteLegal
		,Nombre
		,CONCAT(PrimerApellido, ' ', SegundoApellido, ' ',Nombre) NombreRepresentante
		,CRL.RFC
		,CURP
		,CRL.Calle
		,CRL.NumeroExterior
		,CRL.NumeroInterior
		,CRL.Colonia
		,CRL.CodigoPostal
		,CRL.Lada
		,CRL.TelefonoFijo
		,CRL.ExtensionTelefonica
		,CRL.TelefonoMovil
		,CRL.CorreoElectronico
		,CRL.FechaSolicitud
		,CRL.FechaRegistro
		,Observaciones
		,CRL.IdTipoCalle
		,CRL.IdTipoColonia
		,CL.Localidad
		,CM.Municipio
		,CEF.EntidadFederativa
		,CRL.IdEmpresa
		,CRL.IdLocalidad
		,CM.IdMunicipio
		,CEF.IdEntidadFederativa
		,IdEstatusSolicitudRepresentante
		,Observaciones

	FROM 
		dbo.CatRepresentantesLegales CRL
	INNER JOIN 
		dbo.CatEmpresas CE
	ON CRL.IdEmpresa = CE.IdEmpresa
	INNER JOIN dbo.CatLocalidades CL
	ON CL.IdLocalidad = CRL.idLocalidad
	INNER JOIN dbo.CatMunicipios CM
	ON CM.IdMunicipio = CL.IdMunicipio
	INNER JOIN dbo.CatEntidadesFederativas CEF
	ON CEF.IdEntidadFederativa = CM.IdEntidadFederativa
	WHERE
		 CRL.IdEmpresa = @IdEmpresa
		 and CRL.Activo = 1;
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarRepresentantesLegalesPorValidar]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarRepresentantesLegalesPorValidar] 

AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		CE.NombreComercial
		,IdRepresentanteLegal
		,CONCAT(PrimerApellido, ' ', SegundoApellido, ' ',Nombre) NombreRepresentante
		,CRL.FechaSolicitud
		,Observaciones

	FROM 
		dbo.CatRepresentantesLegales CRL
	INNER JOIN 
		dbo.CatEmpresas CE
	ON CRL.IdEmpresa = CE.IdEmpresa
	WHERE
		IdEstatusSolicitudRepresentante = 1
		and CRL.Activo = 1
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarRespuesta]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016/05/24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarRespuesta]
	@IdProyecto int
    ,@IdPregunta int
    ,@RespuestaInversionista bit out
AS
BEGIN
	SET NOCOUNT ON;
		
	UPDATE dbo.ProyectoPreguntas
	SET
           Modificar = 0
           ,[FechaModificacion] = GETDATE()
	WHERE 
		IdProyecto = @IdProyecto
		and IdPregunta = @IdPregunta

	SET @RespuestaInversionista = (SELECT Respuesta FROM dbo.ProyectoPreguntas WHERE IdProyecto = @IdProyecto AND IdPregunta = @IdPregunta)
END




GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarSeguimiento]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-05-24
-- Description:	
-- =============================================
CREATE	PROCEDURE [dbo].[SpSeleccionarSeguimiento]
	@IdProyecto int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
      [IdEstatusProyecto]
	FROM [dbo].[ProyectoSeguimiento]
	WHERE 
		IdProyecto = @IdProyecto
END




GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarTecnologiaPreguntas]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-18
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarTecnologiaPreguntas]
	@IdTecnologia int

AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		IdTecnologiaPregunta
		,IdTecnologia
		,Pregunta
		,Activo
	FROM 
		dbo.TecnologiaPreguntas
	WHERE 
		IdTecnologia = @IdTecnologia
		AND Activo = 1
	ORDER BY
		IdTecnologiaPregunta asc

END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarTecnologias]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarTecnologias]

AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		IdTecnologia
		,Tecnologia
	FROM 
		dbo.CatTecnologias
	WHERE 
		Activo = 1
	ORDER BY
		Tecnologia asc

END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarTecnologiaTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-05-18
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarTecnologiaTramites]
	@IdTecnologia int

AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
      TT.IdTecnologiaTamite
      ,TT.IdTecnologia
	  ,CT.Homoclave
      ,TT.IdFase
      ,TT.IdTramite
      ,TT.IdPosicion
      ,TT.Activo
  FROM [dbo].[TecnologiaTramites] TT
  INNER JOIN dbo.CatTramites CT
  ON TT.IdTramite = CT.IdTramite


  WHERE 
	IdTecnologia = @IdTecnologia

END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarTiposAsentamiento]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarTiposAsentamiento] 
	
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		 IdTipoAsentamiento,
		 TipoAsentamiento
	FROM dbo.CatTiposAsentamiento
	ORDER BY
		TipoAsentamiento asc

END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarTiposDias]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-15-18
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarTiposDias] 
	
AS
BEGIN

	SET NOCOUNT ON;

	SELECT 
		[IdTipoDia]
		,[TipoDia]
	FROM 
		[dbo].[CatTiposDia]
	ORDER BY
		 [IdTipoDia] asc

END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarTiposUsuario]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-06
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarTiposUsuario] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select IdTipoUsuario, TipoUsuario from dbo.CatTiposUsuario
	ORDER BY IdTipoUsuario
END



GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarTiposUsuarioSENER]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-06
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarTiposUsuarioSENER] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT IdTipoUsuario, TipoUsuario from dbo.CatTiposUsuario

	WHERE IdTipoUsuario > 2
	ORDER BY IdTipoUsuario
END



GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarTiposVialidad]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarTiposVialidad] 
	
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		 IdTipoVialidad,
		 TipoVialidad
	FROM 
		dbo.CatTiposVialidad
	ORDER BY
		 TipoVialidad asc

END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarTramites]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-24
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarTramites] 


AS
BEGIN
	SET NOCOUNT ON;

	SELECT 

		CT.idTramite
		,CT.Homoclave
		,CT.Nombre NombreTramite
		,CT.URLTramites
		,CT.URLRequisitos
		,CT.Dependencia
		,CT.CorreoResponsable
		,CT.CorreoSuperior
		,CT.Activo

  FROM dbo.CatTramites CT
 
  WHERE Activo = 1

  ORDER BY  CT.Dependencia,	CT.Homoclave




END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarUbicacionPorCP]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-20
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarUbicacionPorCP]
	@IdCodigoPostal int
AS
BEGIN

	SET NOCOUNT ON;

    SELECT 
		'ENTIDAD FEDERATIVA' AreaGeografica, IdEntidadFederativa IdPosicion 
	FROM dbo.CatCodigosPostales
	WHERE 
		IdCodigoPostal = @IdCodigoPostal

	UNION

	SELECT 
		'MUNICIPIO' AreaGeografica, IdMunicipioINEGI IdPosicion
	FROM dbo.CatCodigosPostales
	WHERE 
		IdCodigoPostal = @IdCodigoPostal

	UNION

	SELECT 
		'LOCALIDAD' AreaGeografica, IdLocalidadINEGI IdPosicion
	FROM dbo.CatCodigosPostales
	WHERE 
		IdCodigoPostal = @IdCodigoPostal

END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarUsuarioPorNombre]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarUsuarioPorNombre] 
	 @NombreUsuario varchar(100)
	,@Password varchar(200)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		CU.[IdUsuario]
		,CU.[IdTipoUsuario]
		,CU.[Nombre]
		,CU.[Password]
		,CU.[CorreoElectronico]
		,CU.[IdEmpresa]
		,CU.FechaRegistro
		,CU.FechaModificacion
		,CU.[Password]
		,CU.Activo
		,IdRepresentanteAsociado
	FROM 
		[dbo].[CatUsuarios] CU
	WHERE 
		CU.Activo = 1 
		and CU.Nombre = @NombreUsuario 
		and Password = @Password
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarUsuariosPorClaveReset]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarUsuariosPorClaveReset] 
	 @ClaveReset varchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @FechaActual datetime
	SET @FechaActual = GETDATE()

    SELECT
		[IdUsuario]
		,[IdTipoUsuario]
		,[Nombre]
		,[Password]
		,[CorreoElectronico]
		,[IdEmpresa]
		,FechaRegistro
		,FechaModificacion
		,[Password]
		,Activo
	FROM 
		[dbo].[CatUsuarios]
	WHERE 
		Activo = 1
		and ClaveReset = @ClaveReset 
		and FechaLimiteReset >= @FechaActual
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarUsuariosPorEmpresa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarUsuariosPorEmpresa] 
	@IdEmpresa int
	,@Activo bit
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		CU.Nombre
		,CU.CorreoElectronico
		,CU.IdEmpresa
		,CU.IdTipoUsuario
		,CU.IdUsuario
		,CU.Activo
	FROM 
		dbo.CatUsuarios CU
	
	WHERE
		IdEmpresa = @IdEmpresa
		and Activo = 1
		and IdTipoUsuario = 1

END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarUsuariosPorNombreUnicamente]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JARN
-- Create date: 2016-04-22
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarUsuariosPorNombreUnicamente] 
	 @Nombre varchar(100)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		[IdUsuario]
		,[IdTipoUsuario]
		,[Nombre]
		,[Password]
		,[CorreoElectronico]
		,[IdEmpresa]
		,FechaRegistro
		,FechaModificacion
		,[Password]
		,Activo
	FROM 
		[dbo].[CatUsuarios]
	WHERE 
		Nombre = @Nombre
END






GO
/****** Object:  StoredProcedure [dbo].[SpSeleccionarUsuariosSENER]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  JARN
-- Create date: 2016-02-22
-- Description: Obtener los datos de un usario
-- =============================================
CREATE PROCEDURE [dbo].[SpSeleccionarUsuariosSENER]

	@IdTipoUsuario int = null
	,@Activo bit = null

AS

BEGIN
 SET NOCOUNT ON;

 SELECT
	CU.[IdUsuario]
    ,CU.[IdTipoUsuario]
	,CU.[Nombre]
	,CU.[Password]
	,CU.[CorreoElectronico]
	,CU.[IdEmpresa]
	,CU.FechaRegistro
	,CU.FechaModificacion
	,CE.NombreComercial NombreEmpresa
	,CTU.TipoUsuario
	,CU.Activo

  FROM [dbo].[CatUsuarios] CU
	  INNER JOIN dbo.CatEmpresas CE
	  on CU.IdEmpresa = CE.IdEmpresa
	  INNER JOIN dbo.CatTiposUsuario CTU
	  on CTU.IdTipoUsuario = CU.IdTipoUsuario
  WHERE 
	--CU.IdTipoUsuario IS NULL OR CU.IdTipoUsuario = ISNULL(@IdTipoUsuario, CU.IdTipoUsuario)
	--AND CU.Activo IS NULL OR CU.Activo = ISNULL(@Activo, CU.Activo)
	CU.Activo = @Activo
	AND CU.IdTipoUsuario = @IdTipoUsuario

  ORDER BY CU.Nombre, CU.CorreoElectronico, CU.FechaRegistro

END








GO
/****** Object:  StoredProcedure [dbo].[SpUsuario]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Jueves 19 de Febrero del 2016
-- Description:	Obtiene datos del usuario.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


create PROCEDURE [dbo].[SpUsuario]
(
@IdUsuario int
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT 
		idUsuario,
		IdTipoUsuario,
		Nombre,
		CorreoElectronico
FROM CatUsuarios WITH (NOLOCK) 
WHERE IdUsuario=@IdUsuario
	
SET NOCOUNT OFF;
END
















GO
/****** Object:  StoredProcedure [dbo].[SpUsuarioEmpresa]    Script Date: 16/06/2016 02:47:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		Panuncio Andres Moreno Arguelles
-- Create date: Lunes 29 de Febrero del 2016
-- Description:	Obtiene el usuario logeado.
-- Versión:		1.0.0
-- Modificed:	
-- Modified by:	
-- Comments:	
-- =======================================================


create PROCEDURE [dbo].[SpUsuarioEmpresa]
@IdUsuario int
AS
BEGIN
	SET NOCOUNT ON;
SELECT  
		CatUsuarios.IdUsuario,
		CatUsuarios.IdTipoUsuario,
		CatUsuarios.Nombre,
		CatUsuarios.CorreoElectronico		
FROM    CatUsuarios
WHERE CatUsuarios.IdUsuario=@IdUsuario
SET NOCOUNT OFF;
END




GO
