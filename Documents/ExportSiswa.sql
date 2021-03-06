USE [DarbiV32]
GO
/****** Object:  Table [dbo].[Siswa]    Script Date: 26-Sep-19 13:29:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Siswa](
	[SiswaId] [int] IDENTITY(1,1) NOT NULL,
	[RegId] [int] NOT NULL,
	[Nosisda] [nvarchar](max) NOT NULL,
	[Fullname] [nvarchar](max) NULL,
	[Nickname] [nvarchar](max) NULL,
	[Nisn] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Sex] [nvarchar](max) NULL,
	[Pob] [nvarchar](max) NULL,
	[Dob] [nvarchar](max) NULL,
	[NamaAyah] [nvarchar](max) NULL,
	[NamaIbu] [nvarchar](max) NULL,
	[PekerjaanAyah] [nvarchar](max) NULL,
	[PekerjaanIbu] [nvarchar](max) NULL,
	[NoTelpAyah] [nvarchar](max) NULL,
	[NoTelpIbu] [nvarchar](max) NULL,
	[EmailOrtu] [nvarchar](max) NULL,
	[Alamat] [nvarchar](max) NULL,
	[Kota] [nvarchar](max) NULL,
	[Provinsi] [nvarchar](max) NULL,
	[KodePos] [nvarchar](max) NULL,
	[Negara] [nvarchar](max) NULL,
	[Anakke] [nvarchar](max) NULL,
	[DetailSaudara] [nvarchar](max) NULL,
	[Agama] [nvarchar](max) NULL,
	[Suku] [nvarchar](max) NULL,
	[Kewarganegaraan] [nvarchar](max) NULL,
	[TinggiBadan] [nvarchar](max) NULL,
	[BeratBadan] [nvarchar](max) NULL,
	[Goldar] [nvarchar](max) NULL,
	[Kelas] [nvarchar](max) NULL,
	[KontakSiswa] [nvarchar](max) NULL,
	[SekolahAsal] [nvarchar](max) NULL,
	[StatSekolahAsal] [nvarchar](max) NULL,
	[JarakRumahSekolah] [nvarchar](max) NULL,
	[KatSpp] [nvarchar](max) NULL,
	[TypeDisc] [nvarchar](max) NULL,
	[NomDisc] [nvarchar](max) NULL,
	[totaldisc] [nvarchar](max) NULL,
	[TingkatId] [int] NULL,
	[Tingkat] [nvarchar](max) NULL,
	[PerDaftar] [nvarchar](max) NULL,
	[Year] [nvarchar](max) NULL,
	[Tahapsatu] [datetime] NULL,
	[Tahapdua] [datetime] NULL,
	[KatAdm] [nvarchar](max) NULL,
	[TypeDiscAdm] [nvarchar](max) NULL,
	[NomDiscAdm] [nvarchar](max) NULL,
	[TglDaftar] [datetime] NULL,
 CONSTRAINT [PK_dbo.Siswa] PRIMARY KEY CLUSTERED 
(
	[SiswaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Siswa] ON 

INSERT [dbo].[Siswa] ([SiswaId], [RegId], [Nosisda], [Fullname], [Nickname], [Nisn], [IsActive], [Sex], [Pob], [Dob], [NamaAyah], [NamaIbu], [PekerjaanAyah], [PekerjaanIbu], [NoTelpAyah], [NoTelpIbu], [EmailOrtu], [Alamat], [Kota], [Provinsi], [KodePos], [Negara], [Anakke], [DetailSaudara], [Agama], [Suku], [Kewarganegaraan], [TinggiBadan], [BeratBadan], [Goldar], [Kelas], [KontakSiswa], [SekolahAsal], [StatSekolahAsal], [JarakRumahSekolah], [KatSpp], [TypeDisc], [NomDisc], [totaldisc], [TingkatId], [Tingkat], [PerDaftar], [Year], [Tahapsatu], [Tahapdua], [KatAdm], [TypeDiscAdm], [NomDiscAdm], [TglDaftar]) VALUES (1, 0, N'2019000001', N'Aulia Raina', N'Aulia', N'0856', 1, N'Perempuan', N'Depok', N'28 Desember 2004', N'Abi', N'Umi', N'Wirausaha', N'Ibu Rumah Tangga', N'0812999666', N'0813444555', N'AbiUmi@gmail.com', N'Jl. Beji Timur', N'Depok', N'Jawa Barat', N'16453', N'Indonesia', N'2', N'1', N'Islam', N'Sunda', N'Indonesia', N'160 cm', N'55kg', N'O', N'9 Makkah', N'0896777888', N'SD Angkasa', N'Swasta', N'5km', N'Umum', N'Rp', N'50000', NULL, 13, N'9', N'Juli', N'2019', CAST(N'2019-09-17T13:12:59.257' AS DateTime), CAST(N'2019-09-17T13:12:59.257' AS DateTime), N'Umum', N'%', N'10', CAST(N'2019-09-17T13:12:59.257' AS DateTime))
INSERT [dbo].[Siswa] ([SiswaId], [RegId], [Nosisda], [Fullname], [Nickname], [Nisn], [IsActive], [Sex], [Pob], [Dob], [NamaAyah], [NamaIbu], [PekerjaanAyah], [PekerjaanIbu], [NoTelpAyah], [NoTelpIbu], [EmailOrtu], [Alamat], [Kota], [Provinsi], [KodePos], [Negara], [Anakke], [DetailSaudara], [Agama], [Suku], [Kewarganegaraan], [TinggiBadan], [BeratBadan], [Goldar], [Kelas], [KontakSiswa], [SekolahAsal], [StatSekolahAsal], [JarakRumahSekolah], [KatSpp], [TypeDisc], [NomDisc], [totaldisc], [TingkatId], [Tingkat], [PerDaftar], [Year], [Tahapsatu], [Tahapdua], [KatAdm], [TypeDiscAdm], [NomDiscAdm], [TglDaftar]) VALUES (2, 0, N'2019000002', N'Awal Ahmad', N'Awal', N'0858', 1, N'Laki-laki', N'Lamongan', N'30 Juli 2004', N'Papa', N'Mama', N'Pegawai Negri', N'Wirausaha', N'0816888777', N'0834777666', N'PapaMama@gmail.com', N'Kp. Sukamaju', N'Depok', N'Jawa Barat', N'16455', N'Indonesia', N'1', N'1', N'Islam', N'Betawi', N'Indonesia', N'165 cm', N'60kg', N'AB', N'Little Black Ant', N'0896333444', N'PG', N'Negri', N'20km', N'Umum', N'Rp', N'85000', NULL, 2, N'PG', N'Juli', N'2019', CAST(N'2019-09-17T13:12:59.260' AS DateTime), CAST(N'2019-09-17T13:12:59.260' AS DateTime), N'Umum', N'%', N'30', CAST(N'2019-09-24T00:00:00.000' AS DateTime))
INSERT [dbo].[Siswa] ([SiswaId], [RegId], [Nosisda], [Fullname], [Nickname], [Nisn], [IsActive], [Sex], [Pob], [Dob], [NamaAyah], [NamaIbu], [PekerjaanAyah], [PekerjaanIbu], [NoTelpAyah], [NoTelpIbu], [EmailOrtu], [Alamat], [Kota], [Provinsi], [KodePos], [Negara], [Anakke], [DetailSaudara], [Agama], [Suku], [Kewarganegaraan], [TinggiBadan], [BeratBadan], [Goldar], [Kelas], [KontakSiswa], [SekolahAsal], [StatSekolahAsal], [JarakRumahSekolah], [KatSpp], [TypeDisc], [NomDisc], [totaldisc], [TingkatId], [Tingkat], [PerDaftar], [Year], [Tahapsatu], [Tahapdua], [KatAdm], [TypeDiscAdm], [NomDiscAdm], [TglDaftar]) VALUES (3, 0, N'2019000003', N'Muhammad Abdul', N'Abdul', N'2019000021', 1, N'Laki-laki', N'Depok', N'24 Agustus 2006', N'Abi', N'Bunda', N'Karyawan Swasta', N'Ibu Rumah Tangga', N'085395229011', N'085395229011', N'ortu@gmail.com', N'Jl. Karet Hijau', N'Depok', N'Jawa Barat', N'16453', N'Indonesia', N'3', NULL, N'Indonesia', N'Sunda', N'Indonesia', N'148 cm', N'50 kg', NULL, N'9 Makkah', N'087766211182', N'SD Angkasa', N'Swasta', N'5 km', N'Umum', N'Rp', N'200000', NULL, 13, N'9', N'Juli', N'2019', CAST(N'2019-09-11T00:00:00.000' AS DateTime), CAST(N'2019-09-18T00:00:00.000' AS DateTime), N'Umum', N'%', N'0', CAST(N'2019-09-18T00:00:00.000' AS DateTime))
INSERT [dbo].[Siswa] ([SiswaId], [RegId], [Nosisda], [Fullname], [Nickname], [Nisn], [IsActive], [Sex], [Pob], [Dob], [NamaAyah], [NamaIbu], [PekerjaanAyah], [PekerjaanIbu], [NoTelpAyah], [NoTelpIbu], [EmailOrtu], [Alamat], [Kota], [Provinsi], [KodePos], [Negara], [Anakke], [DetailSaudara], [Agama], [Suku], [Kewarganegaraan], [TinggiBadan], [BeratBadan], [Goldar], [Kelas], [KontakSiswa], [SekolahAsal], [StatSekolahAsal], [JarakRumahSekolah], [KatSpp], [TypeDisc], [NomDisc], [totaldisc], [TingkatId], [Tingkat], [PerDaftar], [Year], [Tahapsatu], [Tahapdua], [KatAdm], [TypeDiscAdm], [NomDiscAdm], [TglDaftar]) VALUES (4, 0, N'2019000004', N'Abidzar Mukhlis', N'Izar', N'9920111', 1, N'Laki-laki', N'Bandung', N'18 Juli 2006', N'Ismail', N'Maryam', N'Pegawai BUMN', N'Ibu Rumah Tangga', N'085395232134', N'085395232134', N'ortu@gmail.com', N'Jl. Kalimantan', N'Depok', N'Jawa Barat', N'16453', N'Indonesia', N'2', NULL, N'Islam', N'Sunda', N'Indonesia', N'145 cm', N'49 kg', NULL, N'9 Madinah', N'082131476611', N'SD 3', N'Negeri', N'6 km', N'Umum', N'Rp', N'200000', NULL, 13, N'9', N'Juli', N'2019', CAST(N'2019-09-10T00:00:00.000' AS DateTime), CAST(N'2019-09-16T00:00:00.000' AS DateTime), N'Umum', N'%', N'0', CAST(N'2019-09-18T00:00:00.000' AS DateTime))
INSERT [dbo].[Siswa] ([SiswaId], [RegId], [Nosisda], [Fullname], [Nickname], [Nisn], [IsActive], [Sex], [Pob], [Dob], [NamaAyah], [NamaIbu], [PekerjaanAyah], [PekerjaanIbu], [NoTelpAyah], [NoTelpIbu], [EmailOrtu], [Alamat], [Kota], [Provinsi], [KodePos], [Negara], [Anakke], [DetailSaudara], [Agama], [Suku], [Kewarganegaraan], [TinggiBadan], [BeratBadan], [Goldar], [Kelas], [KontakSiswa], [SekolahAsal], [StatSekolahAsal], [JarakRumahSekolah], [KatSpp], [TypeDisc], [NomDisc], [totaldisc], [TingkatId], [Tingkat], [PerDaftar], [Year], [Tahapsatu], [Tahapdua], [KatAdm], [TypeDiscAdm], [NomDiscAdm], [TglDaftar]) VALUES (5, 0, N'2019000005', N'Muhammad Rizky Firmansyah', N'Rizky', N'39310883019', 1, N'Laki-laki', N'Bandung', N'14 April 2006', N'Amirul', N'Fatimah', N'Karyawan Swasta', N'Ibu Rumah Tangga', N'082183839991', N'082183839991', N'ortu@gmail.com', N'Jl. Karet Hijau', N'Depok', N'Jawa Barat', N'16453', N'Indonesia', N'2', NULL, N'Islam', N'Sunda', N'Indonesia', N'170 cm', N'63 kg', N'AB', N'Little Bee', N'082221388100', N'PG', N'Negeri', N'8 km', N'Umum', N'Rp', N'200000', NULL, 3, N'TK A', N'Juli', N'2019', CAST(N'2019-09-06T00:00:00.000' AS DateTime), CAST(N'2019-09-23T00:00:00.000' AS DateTime), N'Umum', N'%', N'10', CAST(N'2019-09-24T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Siswa] OFF
