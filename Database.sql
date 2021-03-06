USE [IMDB]
GO
/****** Object:  Table [dbo].[Actors]    Script Date: 10-06-2018 02:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actors](
	[ActorId] [int] IDENTITY(1,1) NOT NULL,
	[ActorName] [nvarchar](200) NOT NULL,
	[Gender] [nvarchar](50) NULL,
	[DateOfBirth] [date] NULL,
	[BioData] [nvarchar](max) NULL,
 CONSTRAINT [PK_Actors] PRIMARY KEY CLUSTERED 
(
	[ActorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieActor]    Script Date: 10-06-2018 02:24:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieActor](
	[MovieId] [int] NOT NULL,
	[ActorId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 10-06-2018 02:24:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieId] [int] IDENTITY(1,1) NOT NULL,
	[MovieName] [nvarchar](200) NOT NULL,
	[DateOfRelease] [date] NULL,
	[Plot] [nvarchar](max) NULL,
	[Poster] [nvarchar](500) NULL,
	[ProducerId] [int] NOT NULL,
 CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED 
(
	[MovieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producers]    Script Date: 10-06-2018 02:24:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producers](
	[ProducerId] [int] IDENTITY(1,1) NOT NULL,
	[ProducerName] [nvarchar](200) NOT NULL,
	[DateOfBirth] [date] NULL,
	[BioData] [nvarchar](max) NULL,
 CONSTRAINT [PK_Producers] PRIMARY KEY CLUSTERED 
(
	[ProducerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[MovieActor]  WITH CHECK ADD  CONSTRAINT [FKActorId] FOREIGN KEY([ActorId])
REFERENCES [dbo].[Actors] ([ActorId])
GO
ALTER TABLE [dbo].[MovieActor] CHECK CONSTRAINT [FKActorId]
GO
ALTER TABLE [dbo].[MovieActor]  WITH CHECK ADD  CONSTRAINT [FKMovieId] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([MovieId])
GO
ALTER TABLE [dbo].[MovieActor] CHECK CONSTRAINT [FKMovieId]
GO
ALTER TABLE [dbo].[Movies]  WITH CHECK ADD  CONSTRAINT [FK_ProducerId] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Producers] ([ProducerId])
GO
ALTER TABLE [dbo].[Movies] CHECK CONSTRAINT [FK_ProducerId]
GO
