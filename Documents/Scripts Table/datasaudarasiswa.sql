USE [DarbiV32]
GO

/****** Object:  Table [dbo].[datasaudarasiswa]    Script Date: 5/21/2019 5:11:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[datasaudarasiswa](
	[id] [int] NOT NULL,
	[idsiswa] [int] NOT NULL,
	[namasaudara] [varbinary](100) NOT NULL,
	[sex] [int] NOT NULL,
 CONSTRAINT [PK_datasaudarasiswa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


