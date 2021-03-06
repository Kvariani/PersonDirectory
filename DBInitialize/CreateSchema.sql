USE [master]
GO

/****** Object:  Database [PersonDirectory]    Script Date: 28/Sep/20 10:12:16 ******/
CREATE DATABASE [PersonDirectory]

GO

ALTER DATABASE [PersonDirectory] SET COMPATIBILITY_LEVEL = 140
GO

ALTER DATABASE [PersonDirectory] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PersonDirectory] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PersonDirectory] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PersonDirectory] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PersonDirectory] SET ARITHABORT OFF 
GO

ALTER DATABASE [PersonDirectory] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PersonDirectory] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PersonDirectory] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PersonDirectory] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PersonDirectory] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PersonDirectory] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PersonDirectory] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PersonDirectory] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PersonDirectory] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PersonDirectory] SET  DISABLE_BROKER 
GO

ALTER DATABASE [PersonDirectory] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PersonDirectory] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PersonDirectory] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PersonDirectory] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PersonDirectory] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PersonDirectory] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PersonDirectory] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PersonDirectory] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [PersonDirectory] SET  MULTI_USER 
GO

ALTER DATABASE [PersonDirectory] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PersonDirectory] SET DB_CHAINING OFF 
GO

ALTER DATABASE [PersonDirectory] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [PersonDirectory] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [PersonDirectory] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [PersonDirectory] SET QUERY_STORE = OFF
GO

USE [PersonDirectory]
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [PersonDirectory] SET  READ_WRITE 
GO





USE [PersonDirectory]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 28/Sep/20 10:10:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](100) NOT NULL,
	[Lastname] [nvarchar](100) NOT NULL,
	[Gender] [int] NOT NULL,
	[IDNumber] [nvarchar](100) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[FindPerson]    Script Date: 28/Sep/20 10:10:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION  [dbo].[FindPerson](
	@searchString nvarchar(50),
    @pageIndex int,
	@pageSize int)
	   RETURNS TABLE
AS   
return
select p.* from Persons p
join STRING_SPLIT(@searchString, ' ') t on p.Firstname + '_' + p.Lastname + '_' + p.IDNumber like N'%' + t.value + '%' and LEN(TRIM(t.value)) > 0
order by id 
OFFSET @pageSize * (@pageIndex - 1) ROWS FETCH NEXT @pageSize ROWS ONLY
GO
/****** Object:  Table [dbo].[RelatedPersonToPerson]    Script Date: 28/Sep/20 10:10:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelatedPersonToPerson](
	[PersonId] [int] NOT NULL,
	[RelatedPersonId] [int] NOT NULL,
	[RelationType] [int] NOT NULL,
 CONSTRAINT [PK_RelatedPersonToPerson] PRIMARY KEY CLUSTERED 
(
	[PersonId] ASC,
	[RelatedPersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TelNumber]    Script Date: 28/Sep/20 10:10:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TelNumber](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](max) NULL,
	[TelNumberType] [int] NOT NULL,
	[PersonId] [int] NOT NULL,
 CONSTRAINT [PK_TelNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[RelatedPersonToPerson]  WITH CHECK ADD  CONSTRAINT [FK_RelatedPersonToPerson_Persons_PersonId] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([ID])
GO
ALTER TABLE [dbo].[RelatedPersonToPerson] CHECK CONSTRAINT [FK_RelatedPersonToPerson_Persons_PersonId]
GO
ALTER TABLE [dbo].[RelatedPersonToPerson]  WITH CHECK ADD  CONSTRAINT [FK_RelatedPersonToPerson_Persons_RelatedPersonId] FOREIGN KEY([RelatedPersonId])
REFERENCES [dbo].[Persons] ([ID])
GO
ALTER TABLE [dbo].[RelatedPersonToPerson] CHECK CONSTRAINT [FK_RelatedPersonToPerson_Persons_RelatedPersonId]
GO
ALTER TABLE [dbo].[TelNumber]  WITH CHECK ADD  CONSTRAINT [FK_TelNumber_Persons_PersonId] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TelNumber] CHECK CONSTRAINT [FK_TelNumber_Persons_PersonId]
GO
CREATE NONCLUSTERED INDEX [IX_Persons_Firstname_Lastname_IDNumber] ON [dbo].[Persons]
(
	[Firstname] ASC,
	[Lastname] ASC,
	[IDNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO