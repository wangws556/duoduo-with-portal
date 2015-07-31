USE [master]
GO
/****** Object:  Database [CentralData]    Script Date: 4/9/2013 10:38:44 PM ******/
CREATE DATABASE [CentralData]
 CONTAINMENT = NONE
 GO
ALTER DATABASE [CentralData] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CentralData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CentralData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CentralData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CentralData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CentralData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CentralData] SET ARITHABORT OFF 
GO
ALTER DATABASE [CentralData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CentralData] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [CentralData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CentralData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CentralData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CentralData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CentralData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CentralData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CentralData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CentralData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CentralData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CentralData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CentralData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CentralData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CentralData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CentralData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CentralData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CentralData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CentralData] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CentralData] SET  MULTI_USER 
GO
ALTER DATABASE [CentralData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CentralData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CentralData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CentralData] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CentralData', N'ON'
GO
USE [CentralData]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[HomeAddress] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[Image_Id] [int] NULL,
	[IsBuiltIn] [bit] NOT NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BlockHistory]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockHistory](
	[Id] [int] NOT NULL,
	[OptUser_Id] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[Application_Id] [int] NOT NULL,
	[BlockType_Id] [int] NOT NULL,
	[IsBlock] [bit] NOT NULL,
	[Content] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BlockHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[BlockList]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockList](
	[Id] [int] NOT NULL,
	[Application_Id] [int] NULL,
	[BlockType_Id] [int] NOT NULL,
	[Content] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_BlockList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[BlockType]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Days] [int] NOT NULL, 
	[IsBuiltIn] [bit] NOT NULL,
 CONSTRAINT [PK_BlockType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Command]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Command](
	[Id] [int] NOT NULL,
	[Application_Id] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[Image_Id] [int] NULL,
	[IsBuiltIn] [bit] NOT NULL,
	[CommandType] [int] NOT NULL,
	[ActionName] [nvarchar](100) NULL,
	[Money] [int] NULL,
 CONSTRAINT [PK_Command] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gift]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gift](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NULL,
	[Price] [int] NOT NULL,
	[Score] [int] NOT NULL,
	[Money] [int] NULL,
	[RunWay] [int] NULL,
	[RoomBroadcast] [int] NULL,
	[WorldBroadcast] [int] NULL,
	[Unit] [nvarchar](50) NOT NULL,
	[GiftGroup_Id] [int] NULL,
	[Image_Id] [int] NOT NULL,
 CONSTRAINT [PK_Gift] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GiftGroup]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiftGroup](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[Image_Id] [int] NULL,
 CONSTRAINT [PK_GiftGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Image]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Image](
	[Id] [int] NOT NULL,
	[ImageType_Id] [int] NOT NULL,
	[ImageGroup] [nvarchar](100) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Ext] [nchar](10) NOT NULL,
	[TheImage] [varbinary](max) NOT NULL,
	[IsBuiltIn] [bit] NOT NULL default 0,
	[GifImage] [varbinary](max) NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ImageType]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[IsBuiltIn] [bit] NOT NULL,
 CONSTRAINT [PK_ImageType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] NOT NULL,
	[Application_Id] [int] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Order] [int] NULL,
	[Image_Id] [int] NULL,
	[IsBuiltIn] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleCommand]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleCommand](
	[Id] [int] ,
	[SourceRole_Id] [int] NOT NULL,
	[TargetRole_Id] [int] NOT NULL,
	[Command_Id] [int] NOT NULL,
	[IsManagerCommand] [bit] NOT NULL,
	[IsBuiltIn] [bit] NOT NULL,
	[Application_Id] [int] NOT NULL,
 CONSTRAINT [PK_RoleCommand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Room]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[HostUser_Id] [int] NULL,
	[AgentUser_Id] [int] NULL,
	[RoomGroup_Id] [int] NULL,
	[MaxUserCount] [int] NULL,
	[Password] [nvarchar](max) NULL,
	[Hide] [bit] NOT NULL,
	[PublicMicCount] [smallint] NOT NULL,
	[PrivateMicCount] [smallint] NOT NULL,
	[SecretMicCount] [smallint] NOT NULL,
	[PublicChatEnabled] [bit] NOT NULL,
	[PrivateChatEnabled] [bit] NOT NULL,
	[GiftEnabled] [bit] NOT NULL,
	[ServiceIp] [nvarchar](50) NULL,
	[PublicMicTime] [int] NULL,
	[Image_Id] [int] NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoomBlackList]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomBlackList](
	[Room_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
 CONSTRAINT [PK_RoomBlackList] PRIMARY KEY CLUSTERED 
(
	[Room_Id] ASC,
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoomGroup]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomGroup](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[Image_Id] [int] NULL,
	[ParentGroup_Id] [int] NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_RoomGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoomRole]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomRole](
	[Room_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Role_Id] [int] NOT NULL,
 CONSTRAINT [PK_RoomRole] PRIMARY KEY CLUSTERED 
(
	[Room_Id] ASC,
	[User_Id] ASC,
	[Role_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Password] [nvarchar](max) NOT NULL,
	[NickName] [nvarchar](50) NULL,
	[ApplicationCreated_Id] [int] NULL,
	[PasswordQuestion] [nvarchar](100) NULL,
	[PasswordAnswer] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[Age] [int] NULL,
	[LastLoginTime] [datetime] NULL,
	[Image_Id] [int] NULL,
	[IsBuiltIn] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserApplicationInfo]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserApplicationInfo](	
	[User_Id] [int] NOT NULL,
	[Application_Id] [int] NOT NULL,
	[Money] [int] NULL,
	[AgentMoney] [int] NULL,
	[Score] [int] NULL,
	[Role_Id] [int] NOT NULL,
 CONSTRAINT [PK_UserApplicationInfo] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC,
	[Application_Id] ASC	
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserIdHistory]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserIdHistory](
	[Id] [int] identity,
	[OptUser_Id] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[Role_Id] [int] NOT NULL,
	[StartId] [int] NOT NULL,
	[EndId] [int] NULL,
	[Application_Id] [int] NOT NULL,
 CONSTRAINT [PK_UserIdHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserIdList]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserIdList](
	[Application_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,	
	[Owner_Id] [int] NULL,
	[Role_Id] [int] NOT NULL,
	[IsUsed] [bit] NOT NULL,
 CONSTRAINT [PK_UserIdList] PRIMARY KEY CLUSTERED 
(
	[Application_Id] ASC,
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DepositHistory]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepositHistory](
	[Id] [int] Identity,
	[OptUser_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Application_Id] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[Money] [int] NULL,
	[IsAgent] [bit] NULL,
	[Score] [int] NULL,
 CONSTRAINT [PK_DepositHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExchangeHistory]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeHistory](
	[Id] [int] Identity,
	[OptUser_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Application_Id] [int] NOT NULL,
	[ApplyTime] [datetime] NOT NULL,
	[SettlementTime] [datetime] NOT NULL,
	[Score] [int] NULL,
	[Money] [int] NULL,
	[Cache] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_ExchangeHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/******************Object: Table [dbo].[ExchangeRate] **************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeRate](
	[Id] [int] Identity,
	[Application_Id] [int] NOT NULL,
	[ScoreToMoney] [int] NULL,
	[MoneyToCache] [int] NULL,
	[ScoreToCache] [int] NULL,
	[ValidTime] [datetime] NOT NULL,
	 CONSTRAINT [PK_ExchangeRate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[GiftInOutHistory]    Script Date: 4/21/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiftInOutHistory](
	[Id] [int] NOT NULL,
	[SourceUser_Id] [int] NOT NULL,
	[TargetUser_Id] [int] NOT NULL,
	[Gift_Id] [int] NOT NULL,
	[Room_Id] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
 CONSTRAINT [PK_GiftInOutHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[RoomConfig]    Script Date: 4/21/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomConfig](
	[Id] [int] NOT NULL,
	[Room_Id] [int] NOT NULL,
	[Tag] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[Value] [nvarchar](100) NULL,	
 CONSTRAINT [PK_RoomConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[UserConfig]    Script Date: 4/21/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserConfig](
	[Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Tag] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[Value] [nvarchar](100) NULL,	
 CONSTRAINT [PK_UserConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[TokenCache]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenCache](	
	[User_Id] [int] NOT NULL,
	[Application_Id] [int] NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TokenCache] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC,
	[Application_Id] ASC	
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserConfig] WITH CHECK ADD CONSTRAINT [FK_UserConfig] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User]([Id])
GO

ALTER TABLE [dbo].[RoomConfig] WITH CHECK ADD CONSTRAINT [FK_RoomConfig] FOREIGN KEY([Room_Id])
REFERENCES [dbo].[Room]([Id])
GO

ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [FK_Application_Image] FOREIGN KEY([Image_Id])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [FK_Application_Image]
GO
ALTER TABLE [dbo].[BlockHistory]  WITH CHECK ADD  CONSTRAINT [FK_BlockHistory_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[BlockHistory] CHECK CONSTRAINT [FK_BlockHistory_Application]
GO
ALTER TABLE [dbo].[BlockHistory]  WITH CHECK ADD  CONSTRAINT [FK_BlockHistory_BlockType] FOREIGN KEY([BlockType_Id])
REFERENCES [dbo].[BlockType] ([Id])
GO
ALTER TABLE [dbo].[BlockHistory] CHECK CONSTRAINT [FK_BlockHistory_BlockType]
GO
ALTER TABLE [dbo].[BlockHistory]  WITH CHECK ADD  CONSTRAINT [FK_BlockHistory_User] FOREIGN KEY([OptUser_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[BlockHistory] CHECK CONSTRAINT [FK_BlockHistory_User]
GO

ALTER TABLE [dbo].[BlockList]  WITH CHECK ADD  CONSTRAINT [FK_BlockList_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[BlockList] CHECK CONSTRAINT [FK_BlockList_Application]
GO
ALTER TABLE [dbo].[BlockList]  WITH CHECK ADD  CONSTRAINT [FK_BlockList_BlockType] FOREIGN KEY([BlockType_Id])
REFERENCES [dbo].[BlockType] ([Id])
GO
ALTER TABLE [dbo].[BlockList] CHECK CONSTRAINT [FK_BlockList_BlockType]
GO

ALTER TABLE [dbo].[Command]  WITH CHECK ADD  CONSTRAINT [FK_Command_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[Command] CHECK CONSTRAINT [FK_Command_Application]
GO
ALTER TABLE [dbo].[Command]  WITH CHECK ADD  CONSTRAINT [FK_Command_Image] FOREIGN KEY([Image_Id])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[Command] CHECK CONSTRAINT [FK_Command_Image]
GO
ALTER TABLE [dbo].[Gift]  WITH CHECK ADD  CONSTRAINT [FK_Gift_GiftGroup] FOREIGN KEY([GiftGroup_Id])
REFERENCES [dbo].[GiftGroup] ([Id])
GO
ALTER TABLE [dbo].[Gift] CHECK CONSTRAINT [FK_Gift_GiftGroup]
GO
ALTER TABLE [dbo].[Gift]  WITH CHECK ADD  CONSTRAINT [FK_Gift_Image] FOREIGN KEY([Image_Id])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[Gift] CHECK CONSTRAINT [FK_Gift_Image]
GO
ALTER TABLE [dbo].[GiftGroup]  WITH CHECK ADD  CONSTRAINT [FK_GiftGroup_Image] FOREIGN KEY([Image_Id])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[GiftGroup] CHECK CONSTRAINT [FK_GiftGroup_Image]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Image_ImageType] FOREIGN KEY([ImageType_Id])
REFERENCES [dbo].[ImageType] ([Id])
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Image_ImageType]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_Application]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_Image] FOREIGN KEY([Image_Id])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_Image]
GO
ALTER TABLE [dbo].[RoleCommand]  WITH CHECK ADD  CONSTRAINT [FK_RoleCommand_Command] FOREIGN KEY([Command_Id])
REFERENCES [dbo].[Command] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleCommand] CHECK CONSTRAINT [FK_RoleCommand_Command]
GO
ALTER TABLE [dbo].[RoleCommand]  WITH CHECK ADD  CONSTRAINT [FK_RoleCommand_SourceRole] FOREIGN KEY([SourceRole_Id])
REFERENCES [dbo].[Role] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleCommand] CHECK CONSTRAINT [FK_RoleCommand_SourceRole]
GO
ALTER TABLE [dbo].[RoleCommand]  WITH CHECK ADD  CONSTRAINT [FK_RoleCommand_TargetRole] FOREIGN KEY([TargetRole_Id])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[RoleCommand] CHECK CONSTRAINT [FK_RoleCommand_TargetRole]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Image] FOREIGN KEY([Image_Id])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Image]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_RoomGroup] FOREIGN KEY([RoomGroup_Id])
REFERENCES [dbo].[RoomGroup] ([Id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_RoomGroup]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_User] FOREIGN KEY([HostUser_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_User]
GO
ALTER TABLE [dbo].[Room] WITH CHECK ADD CONSTRAINT [FK_Room_Agent_User] FOREIGN KEY ([AgentUser_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [Fk_Room_Agent_User]
GO
ALTER TABLE [dbo].[RoomBlackList]  WITH CHECK ADD  CONSTRAINT [FK_RoomBlackList_Room] FOREIGN KEY([Room_Id])
REFERENCES [dbo].[Room] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoomBlackList] CHECK CONSTRAINT [FK_RoomBlackList_Room]
GO
ALTER TABLE [dbo].[RoomBlackList]  WITH CHECK ADD  CONSTRAINT [FK_RoomBlackList_User] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoomBlackList] CHECK CONSTRAINT [FK_RoomBlackList_User]
GO
ALTER TABLE [dbo].[RoomGroup]  WITH CHECK ADD  CONSTRAINT [FK_RoomGroup_Image] FOREIGN KEY([Image_Id])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[RoomGroup] CHECK CONSTRAINT [FK_RoomGroup_Image]
GO
ALTER TABLE [dbo].[RoomGroup]  WITH CHECK ADD  CONSTRAINT [FK_RoomGroup_RoomGroup] FOREIGN KEY([ParentGroup_Id])
REFERENCES [dbo].[RoomGroup] ([Id])
GO
ALTER TABLE [dbo].[RoomGroup] CHECK CONSTRAINT [FK_RoomGroup_RoomGroup]
GO
ALTER TABLE [dbo].[RoomRole]  WITH CHECK ADD  CONSTRAINT [FK_RoomRole_Room] FOREIGN KEY([Room_Id])
REFERENCES [dbo].[Room] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoomRole] CHECK CONSTRAINT [FK_RoomRole_Room]
GO
ALTER TABLE [dbo].[RoomRole]  WITH CHECK ADD  CONSTRAINT [FK_RoomRole_User] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoomRole] CHECK CONSTRAINT [FK_RoomRole_User]
GO
ALTER TABLE [dbo].[RoomRole] WITH CHECK ADD CONSTRAINT [FK_RoomRole_Role] FOREIGN KEY ([Role_Id])
REFERENCES [dbo].[Role] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoomRole] CHECK CONSTRAINT [FK_RoomRole_Role]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Application] FOREIGN KEY([ApplicationCreated_Id])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Application]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Image] FOREIGN KEY([Image_Id])
REFERENCES [dbo].[Image] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Image]
GO
ALTER TABLE [dbo].[UserApplicationInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserApplicationInfo_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[UserApplicationInfo] CHECK CONSTRAINT [FK_UserApplicationInfo_Application]
GO
ALTER TABLE [dbo].[UserApplicationInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserApplicationInfo_Role] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UserApplicationInfo] CHECK CONSTRAINT [FK_UserApplicationInfo_Role]
GO
ALTER TABLE [dbo].[UserApplicationInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserApplicationInfo_User] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserApplicationInfo] CHECK CONSTRAINT [FK_UserApplicationInfo_User]
GO

ALTER TABLE [dbo].[TokenCache]  WITH CHECK ADD  CONSTRAINT [FK_TokenCache_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[TokenCache] CHECK CONSTRAINT [FK_TokenCache_Application]
GO
ALTER TABLE [dbo].[TokenCache]  WITH CHECK ADD  CONSTRAINT [FK_TokenCache_User] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[TokenCache] CHECK CONSTRAINT [FK_TokenCache_User]
GO
ALTER TABLE [dbo].[UserIdHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserIdHistory_Application] FOREIGN KEY([Application_id])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[UserIdHistory] CHECK CONSTRAINT [FK_UserIdHistory_Application]
GO
ALTER TABLE [dbo].[UserIdHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserIdHistory_Role] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UserIdHistory] CHECK CONSTRAINT [FK_UserIdHistory_Role]
GO
ALTER TABLE [dbo].[UserIdHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserIdHistory_User] FOREIGN KEY([OptUser_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserIdHistory] CHECK CONSTRAINT [FK_UserIdHistory_User]
GO
ALTER TABLE [dbo].[UserIdList] WITH CHECK ADD CONSTRAINT [FK_UserIdList_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application]([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserIdList] CHECK CONSTRAINT [FK_UserIdList_Application]
GO

ALTER TABLE [dbo].[UserIdList]  WITH CHECK ADD  CONSTRAINT [FK_UserIdList_Role] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Role] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserIdList] CHECK CONSTRAINT [FK_UserIdList_Role]
GO

ALTER TABLE [dbo].[DepositHistory]  WITH CHECK ADD  CONSTRAINT [FK_DepositHistory_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[DepositHistory] CHECK CONSTRAINT [FK_DepositHistory_Application]
GO
ALTER TABLE [dbo].[DepositHistory]  WITH CHECK ADD  CONSTRAINT [FK_DepositHistory_OptUser] FOREIGN KEY([OptUser_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[DepositHistory] CHECK CONSTRAINT [FK_DepositHistory_OptUser]
GO
ALTER TABLE [dbo].[DepositHistory]  WITH CHECK ADD  CONSTRAINT [FK_DepositHistory_User] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[DepositHistory] CHECK CONSTRAINT [FK_DepositHistory_User]
GO
ALTER TABLE [dbo].[ExchangeHistory]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeHistory_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExchangeHistory] CHECK CONSTRAINT [FK_ExchangeHistory_Application]
GO
ALTER TABLE [dbo].[ExchangeHistory]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeHistory_User] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ExchangeHistory] CHECK CONSTRAINT [FK_ExchangeHistory_User]
GO
ALTER TABLE [dbo].[ExchangeHistory]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeHistory_OptUser] FOREIGN KEY([OptUser_Id])
REFERENCES [dbo].[User] ([Id])
GO

/************************************Constraint for Tabel [dbo].[ExchangeRate]******************************/

ALTER TABLE [dbo].[ExchangeRate]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeRate_Application] FOREIGN KEY([Application_Id])
REFERENCES [dbo].[Application] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExchangeRate] CHECK CONSTRAINT [FK_ExchangeRate_Application]
GO

/************************************Constraint for Tabel [dbo].[GiftInOUtHistory]******************************/
ALTER TABLE [dbo].[GiftInOutHistory] WITH CHECK ADD CONSTRAINT [FK_GiftInOutHistory_SourceUser] FOREIGN KEY([SourceUser_Id])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GiftInOutHistory]  WITH CHECK ADD  CONSTRAINT [FK_GiftInOutHistory_Gift] FOREIGN KEY([Gift_Id])
REFERENCES [dbo].[Gift] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[GiftInOutHistory] WITH CHECK ADD CONSTRAINT [FK_GiftInOutHistory_Room] FOREIGN KEY([Room_Id])
REFERENCES [dbo].[Room] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

USE [master]
GO

ALTER DATABASE [CentralData] SET  READ_WRITE 
GO

USE [CentralData]
GO

/****** Object:  View [dbo].[RoleCommandView]    Script Date: 4/14/2013 3:05:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[RoleCommandView]
AS
SELECT        dbo.Command.Name as Command_Name, dbo.Command.Application_Id as Command_Application_Id, dbo.Command.CommandType, dbo.Command.Money, dbo.Command.ActionName,  dbo.RoleCommand.Id, dbo.RoleCommand.SourceRole_Id, 
                         dbo.RoleCommand.TargetRole_Id, dbo.RoleCommand.Command_Id, dbo.RoleCommand.IsManagerCommand, dbo.RoleCommand.IsBuiltIn, dbo.RoleCommand.Application_Id
FROM            dbo.Command INNER JOIN
                         dbo.RoleCommand ON dbo.Command.Id = dbo.RoleCommand.Command_Id

GO

USE [CentralData]
GO
CREATE PROCEDURE AddDefaultImage 
	@imgType int,
	@imgGroup nvarchar,
	@imgId int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @name nvarchar(500)
	declare @ext nvarchar(50)
	declare @theimage varbinary(max)
	declare @giftimage varbinary(max)
	select @imgId = max(Id) from Image with(TABLOCK, HOLDLOCK)
	select @name = Name, @ext=Ext, @theimage = TheImage,@giftimage = GifImage from Image where Id = 0
	insert into Image values(@imgId+1,@imgType,@imgGroup, @name, @ext, @theimage,0,@giftimage)
END
GO
/****** Object:  StoredProcedure [dbo].[AddUserIds]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddUserIds] 
	@isdirect bit,
	@start int ,
	@end int,
	@roleid int,	
	@applicationid int,
	@ownerid int,
	@password nvarchar(max)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @id int
	declare @now Date
	declare @isRegister bit
	if(@roleid <> 3)
		set @isRegister =0
	else
		set @isRegister=1
	set @id = @start
	set @now = SYSDATETIME()
	if @end < @start	
		set @end=@start
	
	while @id <= @end
	begin		
		if(@isdirect = 0) or (@isRegister = 1)
		begin
			if not exists (select 1 from UserIdList where User_Id=@id and (Application_Id=@applicationid or Application_Id=1))			
				insert into UserIdList values( @applicationid, @id, @ownerid,@roleid,0);
		end
		else
		begin			
			declare @imgId int
			--ImageType 10 HeaderImage
			exec AddDefaultImage 10, '', @imgId output
			if not exists (select 1 from [User] where Id = @id)
			begin
				insert into [User] (Id, ApplicationCreated_Id, [Password], [IsBuiltIn], [Image_Id],[NickName]) values(@id,@applicationid,@password,0, @imgId,'')					
				insert into [UserApplicationInfo]([Application_Id],[User_Id],[Role_Id]) values(@applicationid,@id, @roleid)
			end
				 
			merge UserIdList as target 
			using (select @id,@applicationid,@ownerid,@roleid,1) as source(id,applicationid,ownerid,roleid,isused)
			on(target.User_Id = source.id and target.Application_Id = source.applicationid)
			when matched then
			update set IsUsed=source.isused
			when not matched then
			insert values(source.applicationid,source.id,source.ownerid,source.roleid,source.isused);
		end
		set @id = @id +1
	end
END
GO

/****** Object:  StoredProcedure [dbo].[GetRoleCommands]    Script Date: 4/9/2013 10:38:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[GetRoleCommandsForUser]
	@userid int 	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select * from RoleCommandView where SourceRole_Id in (select Role_Id from UserApplicationInfo where [User_Id] = @userid)
END
GO


/*****************Object: StoredProcedure [dbo].[AssignRoomsToAgent]****************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[AssignRoomsToAgent]
	@startId int,
	@endId int,
	@agentId int
AS
BEGIN
	SET NOCOUNT ON;
	while(@startId <= @endId)
	begin
		update [dbo].[Room] set AgentUser_Id = @agentId where Id = @startId
		set @startId = @startId + 1
	end
END
GO

/*****************Object: StoredProcedure [dbo].[HasCommand]****************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[HasCommand]
	@userid int,	
	@cmdid int,
	@targetappid int,
	@targetroleid int
AS
BEGIN
	SET NOCOUNT ON;
	if exists (select 1 from UserApplicationInfo join RoleCommand on (RoleCommand.SourceRole_Id = UserApplicationInfo.Role_Id 
				and UserApplicationInfo.Application_Id = RoleCommand.Application_Id) where 
				[User_Id]=@userid and Command_Id=1 and RoleCommand.TargetRole_Id=@targetroleid and 
				(RoleCommand.Application_Id=1 or RoleCommand.Application_Id=@targetappid))
		return 1
	else 
	begin		
		if exists (select 1 from UserApplicationInfo join RoleCommand on (RoleCommand.SourceRole_Id = UserApplicationInfo.Role_Id 
				and UserApplicationInfo.Application_Id = RoleCommand.Application_Id) where 
				[User_Id]=@userid and Command_Id=@cmdid and RoleCommand.TargetRole_Id=@targetroleid and 
				RoleCommand.Application_Id=@targetappid)
		return 1		
	end
	return 0
END
GO

create PROCEDURE [dbo].[AssignAgentUserIds]
	@appid int,
	@start int,
	@end int,
	@agent int
AS
BEGIN
	SET NOCOUNT ON;
	declare @id int
	set @id = @start
	while @id <= @end
	begin
		update UserIdList set Owner_Id=@agent where User_Id=@id and Application_Id=@appid		
		set @id = @id +1
	END
END
GO

CREATE PROCEDURE [dbo].[GetApplicationsForCommand]
	@userid int,
	@cmdid int
AS
BEGIN
	SET NOCOUNT ON;

	if exists (select 1 from RoleCommand where (Command_Id = 1 or Command_Id = @cmdid) and Application_Id = 1)
		select * from Application
	else
	begin
		select * from [Application] where Id in 
			(select Application_Id from RoleCommand where (Command_Id=1 or Command_Id=@cmdid) and SourceRole_Id in 
				(select Role_Id from UserApplicationInfo where [User_Id] = @userid))		
	end
END