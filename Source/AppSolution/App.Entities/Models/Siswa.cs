using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Entities.Models
{
    public class Siswa
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Siswa Id")]
        public int SiswaId { get; set; }
        public int RegId { get; set; }

        [Required]
        [Display(Name = "No Sisda")]
        public string Nosisda { get; set; }

        [Display(Name = "Nama Lengkap")]
        public string Fullname { get; set; }

        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

        [Display(Name = "NISN")]
        public string Nisn { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Jenis Kelamin")]
        public string Sex { get; set; }

        [Display(Name = "Tempat Lahir")]
        public string Pob { get; set; }

        [Display(Name = "Tanggal Lahir")]
        public string Dob { get; set; }

        [Display(Name = "Nama Ayah")]
        public string NamaAyah { get; set; }

        [Display(Name = "Nama Ibu")]
        public string NamaIbu { get; set; }

        [Display(Name = "Pekerjaan Ayah")]
        public string PekerjaanAyah { get; set; }

        [Display(Name = "Pekerjaan Ibu")]
        public string PekerjaanIbu { get; set; }

        [Display(Name = "Nomor Telpon Ayah")]
        public string NoTelpAyah { get; set; }

        [Display(Name = "Nomor Telpon Ibu")]
        public string NoTelpIbu { get; set; }

        [Display(Name = "Email Orangtua")]
        public string EmailOrtu { get; set; }

        [Display(Name = "Alamat")]
        public string Alamat { get; set; }

        [Display(Name = "Kota")]
        public string Kota { get; set; }

        [Display(Name = "Provinsi")]
        public string Provinsi { get; set; }

        [Display(Name = "Kode Pos")]
        public string KodePos { get; set; }

        [Display(Name = "Negara")]
        public string Negara { get; set; }

        [Display(Name = "Anak ke")]
        public string Anakke { get; set; }

        [Display(Name = "Detail Saudara")]
        public string DetailSaudara { get; set; }

        [Display(Name = "Agama")]
        public string Agama { get; set; }

        [Display(Name = "Suku")]
        public string Suku { get; set; }

        [Display(Name = "Kewarganegaraan")]
        public string Kewarganegaraan { get; set; }

        [Display(Name = "Tinggi Badan")]
        public string TinggiBadan { get; set; }

        [Display(Name = "BeratBadan")]
        public string BeratBadan { get; set; }

        [Display(Name = "Golongan Darah")]
        public string Goldar { get; set; }

        [Display(Name = "Kelas")]
        public string Kelas { get; set; }

        [Display(Name = "Kontak Siswa")]
        public string KontakSiswa { get; set; }

        [Display(Name = "Sekolah Asal")]
        public string SekolahAsal { get; set; }

        [Display(Name = "Status Sekolah Asal")]
        public string StatSekolahAsal { get; set; }

        [Display(Name = "Jarak Rumah Ke Sekolah")]
        public string JarakRumahSekolah { get; set; }

        [Display(Name = "Kategori Spp")]
        public string KatSpp { get; set; }

        [Display(Name = "Tipe Discount")]
        public string TypeDisc { get; set; }

        [Display(Name = "Nominal Discount")]
        public string NomDisc { get; set; }

        [Display(Name = "Total Discount")]
        public string totaldisc { get; set; }

        [Display(Name = "Tingkat")]
        public int? TingkatId { get; set; }

        public string Tingkat { get; set; }

        //Bulan
        [Display(Name = "Periode Daftar")]
        public string PerDaftar { get; set; }

        //Tahunnya
        [Display(Name = "Tahun")]
        public string Year { get; set; }

        [Display(Name = "Tahap Pembayaran 1")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Tahapsatu { get; set; }

        [Display(Name = "Tahap Pembayaran 2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Tahapdua { get; set; }

        [Display(Name = "Kategori Biaya Masuk")]
        public string KatAdm { get; set; }

        [Display(Name = "Tipe Discount BM")]
        public string TypeDiscAdm { get; set; }

        [Display(Name = "Nominal Discount BM")]
        public string NomDiscAdm { get; set; }

        [Display(Name = "Tanggal Daftar")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TglDaftar { get; set; }
    }
}