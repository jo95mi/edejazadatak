USE [master]
GO
/****** Object:  Database [BazaEdejaZadatak]    Script Date: 1/26/2019 3:06:17 PM ******/
CREATE DATABASE [BazaEdejaZadatak]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BazaEdejaZadatak', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\BazaEdejaZadatak.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BazaEdejaZadatak_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\BazaEdejaZadatak_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BazaEdejaZadatak] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BazaEdejaZadatak].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BazaEdejaZadatak] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET ARITHABORT OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BazaEdejaZadatak] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BazaEdejaZadatak] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BazaEdejaZadatak] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BazaEdejaZadatak] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET RECOVERY FULL 
GO
ALTER DATABASE [BazaEdejaZadatak] SET  MULTI_USER 
GO
ALTER DATABASE [BazaEdejaZadatak] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BazaEdejaZadatak] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BazaEdejaZadatak] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BazaEdejaZadatak] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BazaEdejaZadatak] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BazaEdejaZadatak', N'ON'
GO
ALTER DATABASE [BazaEdejaZadatak] SET QUERY_STORE = OFF
GO
USE [BazaEdejaZadatak]
GO
/****** Object:  Table [dbo].[Faktura]    Script Date: 1/26/2019 3:06:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faktura](
	[BrojFakture] [varchar](10) NOT NULL,
	[DatumFakture] [date] NOT NULL,
	[Ukupno] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Faktura] PRIMARY KEY CLUSTERED 
(
	[BrojFakture] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stavka]    Script Date: 1/26/2019 3:06:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stavka](
	[BrojFakture] [varchar](10) NOT NULL,
	[RedniBroj] [int] NOT NULL,
	[Kolicina] [int] NOT NULL,
	[Cena] [decimal](18, 2) NOT NULL,
	[Ukupno] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Stavka] PRIMARY KEY CLUSTERED 
(
	[BrojFakture] ASC,
	[RedniBroj] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Faktura] ([BrojFakture], [DatumFakture], [Ukupno]) VALUES (N'1', CAST(N'2019-01-26' AS Date), CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Faktura] ([BrojFakture], [DatumFakture], [Ukupno]) VALUES (N'2', CAST(N'2019-01-26' AS Date), CAST(224.00 AS Decimal(18, 2)))
INSERT [dbo].[Stavka] ([BrojFakture], [RedniBroj], [Kolicina], [Cena], [Ukupno]) VALUES (N'1', 1, 3, CAST(20.00 AS Decimal(18, 2)), CAST(60.00 AS Decimal(18, 2)))
INSERT [dbo].[Stavka] ([BrojFakture], [RedniBroj], [Kolicina], [Cena], [Ukupno]) VALUES (N'1', 2, 6, CAST(15.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)))
INSERT [dbo].[Stavka] ([BrojFakture], [RedniBroj], [Kolicina], [Cena], [Ukupno]) VALUES (N'2', 1, 2, CAST(24.00 AS Decimal(18, 2)), CAST(48.00 AS Decimal(18, 2)))
INSERT [dbo].[Stavka] ([BrojFakture], [RedniBroj], [Kolicina], [Cena], [Ukupno]) VALUES (N'2', 2, 22, CAST(8.00 AS Decimal(18, 2)), CAST(176.00 AS Decimal(18, 2)))
ALTER TABLE [dbo].[Stavka]  WITH CHECK ADD  CONSTRAINT [FK_Stavka_Faktura] FOREIGN KEY([BrojFakture])
REFERENCES [dbo].[Faktura] ([BrojFakture])
GO
ALTER TABLE [dbo].[Stavka] CHECK CONSTRAINT [FK_Stavka_Faktura]
GO
USE [master]
GO
ALTER DATABASE [BazaEdejaZadatak] SET  READ_WRITE 
GO
