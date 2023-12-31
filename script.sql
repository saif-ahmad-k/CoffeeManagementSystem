USE [master]
GO
/****** Object:  Database [CoffeePricingDB]    Script Date: 8/7/2023 2:55:39 PM ******/
CREATE DATABASE [CoffeePricingDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CoffeePricingDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CoffeePricingDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CoffeePricingDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CoffeePricingDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CoffeePricingDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CoffeePricingDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CoffeePricingDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CoffeePricingDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CoffeePricingDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CoffeePricingDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CoffeePricingDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET RECOVERY FULL 
GO
ALTER DATABASE [CoffeePricingDB] SET  MULTI_USER 
GO
ALTER DATABASE [CoffeePricingDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CoffeePricingDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CoffeePricingDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CoffeePricingDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CoffeePricingDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CoffeePricingDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CoffeePricingDB', N'ON'
GO
ALTER DATABASE [CoffeePricingDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [CoffeePricingDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CoffeePricingDB]
GO
/****** Object:  Table [dbo].[tblCategory]    Script Date: 8/7/2023 2:55:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[Sequance] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProduct]    Script Date: 8/7/2023 2:55:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProduct](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProductPricing]    Script Date: 8/7/2023 2:55:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProductPricing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[PricingDate] [datetime] NOT NULL,
	[Price] [nvarchar](100) NULL,
	[Notes] [nvarchar](max) NULL,
	[ProductID] [int] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[ShippingPeriodID] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblShippingPeriod]    Script Date: 8/7/2023 2:55:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblShippingPeriod](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 8/7/2023 2:55:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[Id] [uniqueidentifier] NOT NULL,
	[FullName] [varchar](255) NOT NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](200) NULL,
	[Phone] [nvarchar](50) NULL,
	[Role] [varchar](40) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblCategory] ON 

INSERT [dbo].[tblCategory] ([ID], [Name], [UserID], [Sequance]) VALUES (2, N'test Origin 1', N'63ea998d-f430-45ab-ad3a-72775c021612', NULL)
INSERT [dbo].[tblCategory] ([ID], [Name], [UserID], [Sequance]) VALUES (3, N'test Origin 2', N'63ea998d-f430-45ab-ad3a-72775c021612', NULL)
SET IDENTITY_INSERT [dbo].[tblCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[tblProduct] ON 

INSERT [dbo].[tblProduct] ([ID], [Name], [CategoryID], [UserID]) VALUES (1, N'test Grade 1', 2, N'63ea998d-f430-45ab-ad3a-72775c021612')
INSERT [dbo].[tblProduct] ([ID], [Name], [CategoryID], [UserID]) VALUES (2, N'test  Grade 2', 2, N'63ea998d-f430-45ab-ad3a-72775c021612')
INSERT [dbo].[tblProduct] ([ID], [Name], [CategoryID], [UserID]) VALUES (3, N'Grade A', 3, N'63ea998d-f430-45ab-ad3a-72775c021612')
INSERT [dbo].[tblProduct] ([ID], [Name], [CategoryID], [UserID]) VALUES (4, N'Grade B', 3, N'63ea998d-f430-45ab-ad3a-72775c021612')
INSERT [dbo].[tblProduct] ([ID], [Name], [CategoryID], [UserID]) VALUES (5, N'Grade C', 3, N'63ea998d-f430-45ab-ad3a-72775c021612')
INSERT [dbo].[tblProduct] ([ID], [Name], [CategoryID], [UserID]) VALUES (6, N'Grade D', 3, N'63ea998d-f430-45ab-ad3a-72775c021612')
SET IDENTITY_INSERT [dbo].[tblProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[tblProductPricing] ON 

INSERT [dbo].[tblProductPricing] ([ID], [CreatedDate], [PricingDate], [Price], [Notes], [ProductID], [UserID], [ShippingPeriodID], [IsActive]) VALUES (1, CAST(N'2023-03-21T13:57:21.463' AS DateTime), CAST(N'2023-03-21T13:57:21.843' AS DateTime), N'112342', N'2', 3, N'63ea998d-f430-45ab-ad3a-72775c021612', NULL, NULL)
INSERT [dbo].[tblProductPricing] ([ID], [CreatedDate], [PricingDate], [Price], [Notes], [ProductID], [UserID], [ShippingPeriodID], [IsActive]) VALUES (2, CAST(N'2023-03-22T11:18:13.000' AS DateTime), CAST(N'2023-03-22T00:00:00.000' AS DateTime), N'1121', N'FOB', 3, N'63ea998d-f430-45ab-ad3a-72775c021612', NULL, 1)
INSERT [dbo].[tblProductPricing] ([ID], [CreatedDate], [PricingDate], [Price], [Notes], [ProductID], [UserID], [ShippingPeriodID], [IsActive]) VALUES (3, CAST(N'2023-03-22T11:18:35.397' AS DateTime), CAST(N'2023-03-22T11:18:35.397' AS DateTime), N'221', NULL, 2, N'63ea998d-f430-45ab-ad3a-72775c021612', NULL, 1)
INSERT [dbo].[tblProductPricing] ([ID], [CreatedDate], [PricingDate], [Price], [Notes], [ProductID], [UserID], [ShippingPeriodID], [IsActive]) VALUES (4, CAST(N'2023-03-22T11:18:52.000' AS DateTime), CAST(N'2023-03-22T00:00:00.000' AS DateTime), N'221', N'FOT', 4, N'63ea998d-f430-45ab-ad3a-72775c021612', NULL, 1)
SET IDENTITY_INSERT [dbo].[tblProductPricing] OFF
GO
SET IDENTITY_INSERT [dbo].[tblShippingPeriod] ON 

INSERT [dbo].[tblShippingPeriod] ([ID], [Name], [UserID]) VALUES (2, N'test 1', N'63ea998d-f430-45ab-ad3a-72775c021612')
SET IDENTITY_INSERT [dbo].[tblShippingPeriod] OFF
GO
INSERT [dbo].[tblUser] ([Id], [FullName], [Password], [Email], [Phone], [Role]) VALUES (N'63ea998d-f430-45ab-ad3a-72775c021612', N'Administrator', N'Y1ZqR2VwWkhHTTMyb1pKMjRPOUhVWENkTnZVRmlodFFEbDBaL2pxYUlOaz0', N'admin@yopmail.com', N'56896652', N'Admin')
INSERT [dbo].[tblUser] ([Id], [FullName], [Password], [Email], [Phone], [Role]) VALUES (N'61495c0b-a3cc-450d-89ef-a34ceb9b2576', N'Client Test', N'dkdHT1duNHd3NHNXcFhBQi9mUW9pQzVZT1IzS1Y2RVBFVTBiWW5sY0dDVT0', N'abc@yopmail.com', NULL, N'Normal')
GO
ALTER TABLE [dbo].[tblCategory]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblProduct]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[tblCategory] ([ID])
GO
ALTER TABLE [dbo].[tblProduct]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblProductPricing]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[tblProduct] ([ID])
GO
ALTER TABLE [dbo].[tblProductPricing]  WITH CHECK ADD FOREIGN KEY([ShippingPeriodID])
REFERENCES [dbo].[tblShippingPeriod] ([ID])
GO
ALTER TABLE [dbo].[tblProductPricing]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblShippingPeriod]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUser] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[pFOBOfferList]    Script Date: 8/7/2023 2:55:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pFOBOfferList]
AS
BEGIN
SELECT *
FROM [dbo].[tblProductPricing] pp
inner join [dbo].[tblProduct] p on p.ID = pp.ProductID
inner join [dbo].[tblCategory] c on c.ID = p.CategoryID
inner join [dbo].[tblUser] u on u.Id = pp.UserID
left join [dbo].[tblShippingPeriod] s on s.Id = pp.ShippingPeriodID
order by c.Sequance, c.Name
end
GO
USE [master]
GO
ALTER DATABASE [CoffeePricingDB] SET  READ_WRITE 
GO
