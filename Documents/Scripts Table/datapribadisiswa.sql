USE [DarbiV32]
GO

/****** Object:  Table [dbo].[datapribadisiswa]    Script Date: 5/21/2019 5:10:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[datapribadisiswa](
	[id] [nchar](10) NOT NULL,
	[idsiswa] [int] NOT NULL,
	[pob] [varchar](50) NOT NULL,
	[dob] [date] NOT NULL,
	[ayah] [varchar](100) NOT NULL,
	[ibu] [varchar](100) NOT NULL,
	[pekerjaan_ayah] [varchar](100) NOT NULL,
	[pekerjaan_ibu] [nchar](10) NOT NULL,
	[tlp_ayah] [varchar](50) NOT NULL,
	[tlp_ibu] [varchar](50) NOT NULL,
	[email_orangtua] [varchar](100) NOT NULL,
	[alamat] [text] NOT NULL,
	[kota] [varchar](100) NOT NULL,
	[provinsi] [varchar](100) NOT NULL,
	[kodepos] [varchar](50) NOT NULL,
	[negara] [varchar](100) NOT NULL,
	[anakke] [int] NOT NULL,
	[detailsaudara] [int] NOT NULL,
	[agama] [varchar](100) NOT NULL,
	[suku] [varchar](100) NOT NULL,
	[kewarganegaraan] [varchar](100) NOT NULL,
	[tinggibadan] [decimal](18, 0) NOT NULL,
	[beratbadan] [decimal](18, 0) NOT NULL,
	[goldarah] [varchar](50) NOT NULL,
 CONSTRAINT [PK_datapribadisiswa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


