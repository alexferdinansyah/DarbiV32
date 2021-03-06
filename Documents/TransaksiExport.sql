USE [DarbiV32]
GO
/****** Object:  Table [dbo].[Transaksi]    Script Date: 26/09/2019 13.35.58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaksi](
	[TransId] [int] IDENTITY(1,1) NOT NULL,
	[Nosisda] [nvarchar](max) NULL,
	[Namasiswa] [nvarchar](max) NULL,
	[Kelastingkat] [nvarchar](max) NULL,
	[Jenjang] [nvarchar](max) NULL,
	[totalBM] [nvarchar](max) NULL,
	[bayarBM] [int] NULL,
	[periode] [nvarchar](max) NULL,
	[bulanspp] [nvarchar](max) NULL,
	[bayarspp] [int] NULL,
	[tipebayar] [nvarchar](max) NULL,
	[komiteSekolah] [nvarchar](max) NULL,
	[tgltransfer] [datetime] NULL,
	[tglbayar] [datetime] NULL,
	[BankId] [int] NULL,
	[Banknm] [nvarchar](max) NULL,
	[SSId] [nvarchar](max) NULL,
	[JenisSS] [nvarchar](max) NULL,
	[bulanCA] [nvarchar](max) NULL,
	[bulanAJ] [nvarchar](max) NULL,
	[Nokwitansi] [nvarchar](max) NULL,
	[totalkeseluruhan] [nvarchar](max) NULL,
	[nominal] [nvarchar](max) NULL,
	[infospp] [nvarchar](max) NULL,
	[daftarUlang] [nvarchar](max) NULL,
	[cicilDaftarUlang] [int] NULL,
	[isCanceled] [bit] NOT NULL,
	[canceledBy] [nvarchar](max) NULL,
	[canceledDate] [datetime] NULL,
	[uang] [nvarchar](max) NULL,
	[kembalian] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Fullname] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Transaksi] PRIMARY KEY CLUSTERED 
(
	[TransId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
