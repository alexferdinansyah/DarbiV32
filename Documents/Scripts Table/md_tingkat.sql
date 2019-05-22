USE [DarbiV32]
GO

/****** Object:  Table [dbo].[md_tingkat]    Script Date: 5/21/2019 2:10:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[md_tingkat](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[jenjang] [int] NOT NULL,
 CONSTRAINT [PK_md_tingkat] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


