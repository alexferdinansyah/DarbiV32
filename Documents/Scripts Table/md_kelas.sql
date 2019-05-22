USE [DarbiV32]
GO

/****** Object:  Table [dbo].[md_kelas]    Script Date: 5/21/2019 2:10:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[md_kelas](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[tingkat] [int] NOT NULL,
	[jenjang] [int] NOT NULL,
 CONSTRAINT [PK_md_kelas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


