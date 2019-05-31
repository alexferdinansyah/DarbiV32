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

        [Required]
        [Display(Name = "No Sisda")]
        public string Nosisda { get; set; }

        [Required]
        [Display(Name = "Nama Lengkap")]
        public string Fullname { get; set; }

        [Required]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "NISN")]
        public string Nisn { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Jenis Kelamin")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Tempat Lahir")]
        public string Pob { get; set; }

        [Required]
        [Display(Name = "Tanggal Lahir")]
        public string Dob { get; set; }

        [Required]
        [Display(Name = "Nama Ayah")]
        public string NamaAyah { get; set; }

        [Required]
        [Display(Name = "Nama Ibu")]
        public string NamaIbu { get; set; }

        [Required]
        [Display(Name = "Pekerjaan Ayah")]
        public string PekerjaanAyah { get; set; }

        [Required]
        [Display(Name = "Pekerjaan Ibu")]
        public string PekerjaanIbu { get; set; }

        [Required]
        [Display(Name = "Nomor Telpon Ayah")]
        public string NoTelpAyah { get; set; }

        [Required]
        [Display(Name = "Nomor Telpon Ibu")]
        public string NoTelpIbu { get; set; }

        [Required]
        [Display(Name = "Email Orangtua")]
        public string EmailOrtu { get; set; }

        [Required]
        [Display(Name = "Alamat")]
        public string Alamat { get; set; }

        [Required]
        [Display(Name = "Kota")]
        public string Kota { get; set; }

        [Required]
        [Display(Name = "Provinsi")]
        public string Provinsi { get; set; }

        [Required]
        [Display(Name = "Kode Pos")]
        public string KodePos { get; set; }

        [Required]
        [Display(Name = "Negara")]
        public string Negara { get; set; }

        [Required]
        [Display(Name = "Anak ke")]
        public string Anakke { get; set; }

        //[Required]
        [Display(Name = "Detail Saudara")]
        public string DetailSaudara { get; set; }

        [Required]
        [Display(Name = "Agama")]
        public string Agama { get; set; }

        [Required]
        [Display(Name = "Suku")]
        public string Suku { get; set; }

        [Required]
        [Display(Name = "Kewarganegaraan")]
        public string Kewarganegaraan { get; set; }

        [Required]
        [Display(Name = "Tinggi Badan")]
        public string TinggiBadan { get; set; }

        [Required]
        [Display(Name = "BeratBadan")]
        public string BeratBadan { get; set; }

        [Required]
        [Display(Name = "Golongan Darah")]
        public string Goldar { get; set; }

        [Required]
        [Display(Name = "Periode")]
        public string Periode { get; set; }

        [Required]
        [Display(Name = "Kelas")]
        public string Kelas { get; set; }

        [Required]
        [Display(Name = "Status Kategori")]
        public string StatKat { get; set; }

        [Required]
        [Display(Name = "Kontak Siswa")]
        public string KontakSiswa { get; set; }

        [Required]
        [Display(Name = "Sekolah Asal")]
        public string SekolahAsal { get; set; }

        [Required]
        [Display(Name = "Status Sekolah Asal")]
        public string StatSekolahAsal { get; set; }

        [Required]
        [Display(Name = "Jarak Rumah Ke Sekolah")]
        public string JarakRumahSekolah { get; set; }

        [Required]
        [Display(Name = "Tanggal Daftar")]
        public string Tgldaftar { get; set; }

        [Required]
        [Display(Name = "Gelombang Test")]
        public string GelTest { get; set; }
    }
}