USE [master]
GO
/****** Object:  Database [ConsultorioOdonBD]    Script Date: 15/10/2019 21:09:40 ******/
CREATE DATABASE [ConsultorioOdonBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ConsultorioOdonBD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ConsultorioOdonBD.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ConsultorioOdonBD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ConsultorioOdonBD_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ConsultorioOdonBD] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ConsultorioOdonBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ConsultorioOdonBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ConsultorioOdonBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ConsultorioOdonBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ConsultorioOdonBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ConsultorioOdonBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ConsultorioOdonBD] SET  MULTI_USER 
GO
ALTER DATABASE [ConsultorioOdonBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ConsultorioOdonBD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ConsultorioOdonBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ConsultorioOdonBD] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ConsultorioOdonBD] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ConsultorioOdonBD]
GO
/****** Object:  Table [dbo].[Anamnesis]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Anamnesis](
	[IdAnamnesis] [int] IDENTITY(1,1) NOT NULL,
	[Diabetes] [nchar](10) NULL,
	[Epilepsia] [nchar](10) NULL,
	[TBC] [nchar](10) NULL,
	[EnfRespiratorias] [nchar](10) NULL,
	[CardiopatiasH] [nchar](10) NULL,
	[Alergias] [nchar](10) NULL,
	[HepatitisVIH] [nchar](10) NULL,
	[EnfGastro] [nchar](10) NULL,
	[Hemorragias] [nchar](10) NULL,
	[Observacion] [nvarchar](250) NULL,
 CONSTRAINT [PK_AnamnesisAntecedentes] PRIMARY KEY CLUSTERED 
(
	[IdAnamnesis] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DetalleFactura]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleFactura](
	[IdDetalleFactura] [int] IDENTITY(1,1) NOT NULL,
	[Abono] [bigint] NULL,
	[FechaAbono] [datetime] NULL,
	[Observacion] [nvarchar](250) NULL,
	[Habilitado] [int] NULL,
	[IdFactura] [int] NULL,
	[IdTratamiento] [int] NULL,
 CONSTRAINT [PK_DetalleFactura] PRIMARY KEY CLUSTERED 
(
	[IdDetalleFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[IdEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](250) NULL,
	[Apellido] [nvarchar](250) NULL,
	[FechaContrato] [datetime] NULL,
	[Sueldo] [bigint] NULL,
	[Direccion] [nvarchar](150) NULL,
	[Barrio] [nvarchar](150) NULL,
	[Telefono] [bigint] NULL,
	[Celular] [bigint] NULL,
	[TieneUsuario] [int] NULL,
	[Habilitado] [int] NULL,
	[IdTipoUsuario] [int] NULL,
	[IdTipoContrato] [int] NULL,
	[IdSexo] [int] NULL,
	[IdTipoIdentificacion] [int] NULL,
 CONSTRAINT [PK_Empleado] PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExamenClinico]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamenClinico](
	[IdExamenClinico] [int] IDENTITY(1,1) NOT NULL,
	[Labios] [nchar](10) NULL,
	[Encias] [nchar](10) NULL,
	[Carrillos] [nchar](10) NULL,
	[Lengua] [nchar](10) NULL,
	[Paladar] [nchar](10) NULL,
	[PisoBoca] [nchar](10) NULL,
	[Uvula] [nchar](10) NULL,
	[GlandulasSalivales] [nchar](10) NULL,
	[Observacion] [nvarchar](300) NULL,
 CONSTRAINT [PK_ExamenPeriodontal] PRIMARY KEY CLUSTERED 
(
	[IdExamenClinico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Factura]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[IdFactura] [int] IDENTITY(1,1) NOT NULL,
	[Total] [bigint] NULL,
	[FechaFactura] [datetime] NULL,
	[Saldo] [bigint] NULL,
	[Observacion] [nvarchar](250) NULL,
	[Habilitado] [int] NULL,
	[IdPaciente] [int] NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[IdFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HistoriaClinica]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HistoriaClinica](
	[IdHistoriaClinica] [int] IDENTITY(1,1) NOT NULL,
	[Radiografia] [varbinary](max) NULL,
	[IdAnamnesis] [int] NULL,
	[IdExamenClinico] [int] NULL,
	[IdTratamiento] [int] NULL,
 CONSTRAINT [PK_HistoriaClinica] PRIMARY KEY CLUSTERED 
(
	[IdHistoriaClinica] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Municipio]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Municipio](
	[IdMunicipio] [int] NOT NULL,
	[Nombre] [nvarchar](255) NULL,
	[Habilitado] [int] NULL,
	[IdDepartamento] [int] NULL,
 CONSTRAINT [PK__Municipi__6100597866B4B152] PRIMARY KEY CLUSTERED 
(
	[IdMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paciente](
	[IdPaciente] [int] NOT NULL,
	[Nombre] [nvarchar](150) NULL,
	[Apellido] [nvarchar](150) NULL,
	[FechaNacimiento] [datetime] NULL,
	[Edad] [int] NULL,
	[Email] [nvarchar](200) NULL,
	[Direccion] [nvarchar](150) NULL,
	[Barrio] [nvarchar](150) NULL,
	[Telefono] [bigint] NULL,
	[Celular] [bigint] NULL,
	[Ocupacion] [nvarchar](150) NULL,
	[Aseguradora] [nvarchar](100) NULL,
	[Vinculacion] [nvarchar](100) NULL,
	[MotivoConsulta] [nvarchar](300) NULL,
	[Habilitado] [int] NULL,
	[IdSexo] [int] NULL,
	[IdMunicipio] [int] NULL,
	[IdTipoIdentificacion] [int] NULL,
	[IdResponsable] [int] NULL,
 CONSTRAINT [PK_Paciente] PRIMARY KEY CLUSTERED 
(
	[IdPaciente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pagina]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagina](
	[IdPagina] [int] IDENTITY(1,1) NOT NULL,
	[Mensaje] [nvarchar](100) NULL,
	[Accion] [nvarchar](50) NULL,
	[Controlador] [nvarchar](100) NULL,
	[Habilitado] [int] NULL,
 CONSTRAINT [PK_Pagina] PRIMARY KEY CLUSTERED 
(
	[IdPagina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Responsable]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Responsable](
	[IdResponsable] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NULL,
	[Apellido] [nvarchar](150) NULL,
	[Parentesco] [nvarchar](100) NULL,
	[Telefono] [bigint] NULL,
 CONSTRAINT [PK_Responsable] PRIMARY KEY CLUSTERED 
(
	[IdResponsable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rol]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[NombreR] [nvarchar](100) NULL,
	[Descripcion] [nvarchar](150) NULL,
	[Habilitado] [int] NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RolPagina]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolPagina](
	[IdRolPagina] [int] IDENTITY(1,1) NOT NULL,
	[Habilitado] [int] NULL,
	[IdRol] [int] NULL,
	[IdPagina] [int] NULL,
 CONSTRAINT [PK_RolPagina] PRIMARY KEY CLUSTERED 
(
	[IdRolPagina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sexo]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sexo](
	[IdSexo] [int] NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Habilitado] [int] NULL,
 CONSTRAINT [PK_Sexo] PRIMARY KEY CLUSTERED 
(
	[IdSexo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoContrato]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoContrato](
	[IdTipoContrato] [int] NOT NULL,
	[Nombre] [nvarchar](150) NULL,
	[Habilitado] [int] NULL,
 CONSTRAINT [PK_TipoContrato] PRIMARY KEY CLUSTERED 
(
	[IdTipoContrato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoIdentificacion]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoIdentificacion](
	[IdTipoIdentificacion] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NULL,
	[Numero] [nvarchar](50) NULL,
 CONSTRAINT [PK_TipoIdentificacion] PRIMARY KEY CLUSTERED 
(
	[IdTipoIdentificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuario](
	[IdTipoUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](150) NULL,
	[Descripcion] [nvarchar](250) NULL,
	[Habilitado] [int] NULL,
 CONSTRAINT [PK_TipoUsuario] PRIMARY KEY CLUSTERED 
(
	[IdTipoUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tratamiento]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tratamiento](
	[IdTratamiento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NULL,
 CONSTRAINT [PK_Tratamiento] PRIMARY KEY CLUSTERED 
(
	[IdTratamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 15/10/2019 21:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreU] [nvarchar](150) NULL,
	[Contraseña] [nvarchar](150) NULL,
	[Habilitado] [int] NULL,
	[IdRol] [int] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_Factura] FOREIGN KEY([IdFactura])
REFERENCES [dbo].[Factura] ([IdFactura])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [FK_DetalleFactura_Factura]
GO
ALTER TABLE [dbo].[DetalleFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleFactura_Tratamiento] FOREIGN KEY([IdTratamiento])
REFERENCES [dbo].[Tratamiento] ([IdTratamiento])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleFactura] CHECK CONSTRAINT [FK_DetalleFactura_Tratamiento]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Sexo] FOREIGN KEY([IdSexo])
REFERENCES [dbo].[Sexo] ([IdSexo])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Sexo]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_TipoContrato] FOREIGN KEY([IdTipoContrato])
REFERENCES [dbo].[TipoContrato] ([IdTipoContrato])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_TipoContrato]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_TipoIdentificacion] FOREIGN KEY([IdTipoIdentificacion])
REFERENCES [dbo].[TipoIdentificacion] ([IdTipoIdentificacion])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_TipoIdentificacion]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_TipoUsuario] FOREIGN KEY([IdTipoUsuario])
REFERENCES [dbo].[TipoUsuario] ([IdTipoUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_TipoUsuario]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Paciente] FOREIGN KEY([IdPaciente])
REFERENCES [dbo].[Paciente] ([IdPaciente])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Paciente]
GO
ALTER TABLE [dbo].[HistoriaClinica]  WITH CHECK ADD  CONSTRAINT [FK_HistoriaClinica_Anamnesis] FOREIGN KEY([IdAnamnesis])
REFERENCES [dbo].[Anamnesis] ([IdAnamnesis])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistoriaClinica] CHECK CONSTRAINT [FK_HistoriaClinica_Anamnesis]
GO
ALTER TABLE [dbo].[HistoriaClinica]  WITH CHECK ADD  CONSTRAINT [FK_HistoriaClinica_ExamenPeriodontal] FOREIGN KEY([IdExamenClinico])
REFERENCES [dbo].[ExamenClinico] ([IdExamenClinico])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistoriaClinica] CHECK CONSTRAINT [FK_HistoriaClinica_ExamenPeriodontal]
GO
ALTER TABLE [dbo].[HistoriaClinica]  WITH CHECK ADD  CONSTRAINT [FK_HistoriaClinica_Tratamiento] FOREIGN KEY([IdTratamiento])
REFERENCES [dbo].[Tratamiento] ([IdTratamiento])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HistoriaClinica] CHECK CONSTRAINT [FK_HistoriaClinica_Tratamiento]
GO
ALTER TABLE [dbo].[Paciente]  WITH CHECK ADD  CONSTRAINT [FK_Paciente_Municipio] FOREIGN KEY([IdMunicipio])
REFERENCES [dbo].[Municipio] ([IdMunicipio])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Paciente] CHECK CONSTRAINT [FK_Paciente_Municipio]
GO
ALTER TABLE [dbo].[Paciente]  WITH CHECK ADD  CONSTRAINT [FK_Paciente_Responsable] FOREIGN KEY([IdResponsable])
REFERENCES [dbo].[Responsable] ([IdResponsable])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Paciente] CHECK CONSTRAINT [FK_Paciente_Responsable]
GO
ALTER TABLE [dbo].[Paciente]  WITH CHECK ADD  CONSTRAINT [FK_Paciente_Sexo] FOREIGN KEY([IdSexo])
REFERENCES [dbo].[Sexo] ([IdSexo])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Paciente] CHECK CONSTRAINT [FK_Paciente_Sexo]
GO
ALTER TABLE [dbo].[Paciente]  WITH CHECK ADD  CONSTRAINT [FK_Paciente_TipoIdentificacion] FOREIGN KEY([IdTipoIdentificacion])
REFERENCES [dbo].[TipoIdentificacion] ([IdTipoIdentificacion])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Paciente] CHECK CONSTRAINT [FK_Paciente_TipoIdentificacion]
GO
ALTER TABLE [dbo].[RolPagina]  WITH CHECK ADD  CONSTRAINT [FK_RolPagina_Pagina] FOREIGN KEY([IdPagina])
REFERENCES [dbo].[Pagina] ([IdPagina])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolPagina] CHECK CONSTRAINT [FK_RolPagina_Pagina]
GO
ALTER TABLE [dbo].[RolPagina]  WITH CHECK ADD  CONSTRAINT [FK_RolPagina_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolPagina] CHECK CONSTRAINT [FK_RolPagina_Rol]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
USE [master]
GO
ALTER DATABASE [ConsultorioOdonBD] SET  READ_WRITE 
GO
