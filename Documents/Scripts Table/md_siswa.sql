USE [DarbiV32]
GO

/****** Object:  Table [dbo].[md_siswa]    Script Date: 5/21/2019 5:05:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[md_siswa](
	[id] [int] NOT NULL,
	[nosisda] [varchar](50) NOT NULL,
	[fullname] [varchar](100) NOT NULL,
	[nickname] [varchar](50) NOT NULL,
	[sex] [int] NOT NULL,
	[nisn] [varchar](50) NOT NULL,
	[isactive] [int] NOT NULL,
	[periode] [varchar](50) NOT NULL,
	[kelas] [int] NOT NULL,
	[category_status] [varchar](50) NOT NULL,
	[mycontact] [varchar](50) NOT NULL,
	[school_origin] [varchar](50) NOT NULL,
	[school_origin_status] [nchar](10) NOT NULL,
	[privatedata] [int] NOT NULL,
	[school_house_distance] [int] NOT NULL,
	[reg_date] [date] NOT NULL,
	[gelombang_test] [int] NOT NULL,
	[tahap1] [date] NOT NULL,
	[tahap2] [date] NOT NULL,
 CONSTRAINT [PK_md_siswa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


