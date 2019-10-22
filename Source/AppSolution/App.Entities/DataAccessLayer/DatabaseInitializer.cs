using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Entities.Models;
using System.Data.Entity;

namespace App.Entities.DataAccessLayer
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {


            var roles = new List<Role>
            {
                new Role {Name = "Admin",IsActive = true},
                new Role {Name = "Operator",IsActive = true},
                //new Role {Name = "Role Manager",IsActive = true},
                //new Role {Name = "Module Manager",IsActive = true},
                //new Role {Name = "Dashboard Access",IsActive = true}
            };
            roles.ForEach(s => context.Roles.Add(s));
            context.SaveChanges();

            Role Admin = context.Roles.Where(x => x.Name == "Admin").FirstOrDefault();
            Role Operator = context.Roles.Where(x => x.Name == "Operator").FirstOrDefault();
            //Role RoleManager = context.Roles.Where(x => x.Name == "Role Manager").FirstOrDefault();
            //Role ModuleManager = context.Roles.Where(x => x.Name == "Module Manager").FirstOrDefault();
            //Role DashboardAccess = context.Roles.Where(x => x.Name == "Dashboard Access").FirstOrDefault();

            //Admin.Roles.Add(UserManager);
            //Admin.Roles.Add(RoleManager);
            //Admin.Roles.Add(ModuleManager);
            //Admin.Roles.Add(DashboardAccess);

            //context.Entry(Admin).State = EntityState.Modified;
            //context.SaveChanges();

            //var modules = new List<Module>
            //{
            //    new Module {Name = "User", Area = "AccessManagement", Controller="User", Action = "Index", Roles = context.Roles.Where(x => x.Name == UserManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "User Request Forgot Password", Area = "AccessManagement", Controller="RequestForgotPassword", Action = "Index", Roles = context.Roles.Where(x => x.Name == UserManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "User Request Forgot Password Update Password", Area = "AccessManagement", Controller="RequestForgotPassword", Action = "UpdateUserPassword", Roles = context.Roles.Where(x => x.Name == UserManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "User Create", Area = "AccessManagement", Controller="User", Action = "Create", Roles = context.Roles.Where(x => x.Name == UserManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "User Edit", Area = "AccessManagement", Controller="User", Action = "Edit", Roles = context.Roles.Where(x => x.Name == UserManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "User Delete", Area = "AccessManagement", Controller="User", Action = "Delete", Roles = context.Roles.Where(x => x.Name == UserManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "User Details", Area = "AccessManagement", Controller="User", Action = "Details", Roles = context.Roles.Where(x => x.Name == UserManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "User Manage Role", Area = "AccessManagement", Controller="User", Action = "ManageRole", Roles = context.Roles.Where(x => x.Name == UserManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "User Remove Role From User", Area = "AccessManagement", Controller="User", Action = "RemoveRoleFromUser", Roles = context.Roles.Where(x => x.Name == UserManager.Name).ToList(), IsActive = true},
            //};
            //modules.ForEach(s => context.Modules.Add(s));
            //context.SaveChanges();

            //modules = new List<Module>
            //{
            //    new Module {Name = "Role", Area = "AccessManagement", Controller="Role", Action = "Index", Roles = context.Roles.Where(x => x.Name == RoleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Role Create", Area = "AccessManagement", Controller="Role", Action = "Create", Roles = context.Roles.Where(x => x.Name == RoleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Role Edit", Area = "AccessManagement", Controller="Role", Action = "Edit", Roles = context.Roles.Where(x => x.Name == RoleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Role Delete", Area = "AccessManagement", Controller="Role", Action = "Delete", Roles = context.Roles.Where(x => x.Name == RoleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Role Details", Area = "AccessManagement", Controller="Role", Action = "Details", Roles = context.Roles.Where(x => x.Name == RoleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Role Manage Child", Area = "AccessManagement", Controller="Role", Action = "ManageChild", Roles = context.Roles.Where(x => x.Name == RoleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Role Manage Module", Area = "AccessManagement", Controller="Role", Action = "ManageModule", Roles = context.Roles.Where(x => x.Name == RoleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Role Remove Child From Role", Area = "AccessManagement", Controller="Role", Action = "RemoveChildFromRole", Roles = context.Roles.Where(x => x.Name == RoleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Role Remove Module From Role", Area = "AccessManagement", Controller="Role", Action = "RemoveModuleFromRole", Roles = context.Roles.Where(x => x.Name == RoleManager.Name).ToList(), IsActive = true},
            //};
            //modules.ForEach(s => context.Modules.Add(s));
            //context.SaveChanges();

            //modules = new List<Module>
            //{
            //    new Module {Name = "Module", Area = "AccessManagement", Controller="Module", Action = "Index", Roles = context.Roles.Where(x => x.Name == ModuleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Module Create", Area = "AccessManagement", Controller="Module", Action = "Create", Roles = context.Roles.Where(x => x.Name == ModuleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Module Edit", Area = "AccessManagement", Controller="Module", Action = "Edit", Roles = context.Roles.Where(x => x.Name == ModuleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Module Delete", Area = "AccessManagement", Controller="Module", Action = "Delete", Roles = context.Roles.Where(x => x.Name == ModuleManager.Name).ToList(), IsActive = true},
            //    new Module {Name = "Module Details", Area = "AccessManagement", Controller="Module", Action = "Details", Roles = context.Roles.Where(x => x.Name == ModuleManager.Name).ToList(), IsActive = true},
            //};
            //modules.ForEach(s => context.Modules.Add(s));
            //context.SaveChanges();

            var transaksis = new List<Transaksi>
            {
                //new Transaksi {Nosisda = "2019000001", Namasiswa = "Aulia Raina", Kelastingkat = "9 Makkah", Jenjang = "SMP", totalBM = "12000000", bayarBM = 10000000, periode = "2019-2020", bulanspp = "Juni", bayarspp = 500000, tipebayar = "tunai",
                //tglbayar = DateTime.UtcNow.Date},
                //new Transaksi {Nosisda = "2019000002", Namasiswa = "Awal Ahmad", Kelastingkat = "PG", Jenjang = "PG", totalBM = "12000000", bayarBM = 5000000, periode = "2019-2020", bulanspp = "Januari", bayarspp = 450000, tipebayar = "transfer",
                //tgltransfer = DateTime.UtcNow.Date, tglbayar = DateTime.UtcNow.Date, BankId = 2},
            };
            transaksis.ForEach(s => context.Transaksis.Add(s));
            context.SaveChanges();

            //var historybiayas = new List<HistoryBiaya>
            //{
            //    new HistoryBiaya {PeriodeHB = "2018-2019", KategoriHB = "SPP", JenisHB = "SPP",
            //    TingkatHB = 2, NomHisBiaya = 500000},
            //    new HistoryBiaya {PeriodeHB = "2018-2019", KategoriHB = "Biaya Masuk", JenisHB = "Biaya Masuk",
            //    TingkatHB = 2, NomHisBiaya = 12000000}
            //};
            //historybiayas.ForEach(s => context.HistoryBiayas.Add(s));
            //context.SaveChanges();

            /*var rekapbiayamasuks = new List<RekapBiayaMasuk>
            {
                new RekapBiayaMasuk {Nosisda="0011856", Namasiswa="Aulia Raina", Jenjang="SMP", Tingkat="9", periode="2019",
                tanggalhistory="15-Juni-2019"}
            };
            rekapbiayamasuks.ForEach(s => context.RekapBiayaMasuks.Add(s));
            context.SaveChanges();*/

            var siswas = new List<Siswa>
            {
                //new Siswa {Nosisda = "2019000001", Fullname = "Aulia Raina", Nickname = "Aulia", Nisn = "0856", IsActive = true, Sex = "Perempuan", Pob = "Depok", Dob = "28 Desember 2004",
                //NamaAyah = "Abi", NamaIbu = "Umi", PekerjaanAyah = "Wirausaha", PekerjaanIbu= "Ibu Rumah Tangga", NoTelpAyah = "0812999666", NoTelpIbu = "0813444555",
                //EmailOrtu = "AbiUmi@gmail.com", Alamat = "Jl. Beji Timur", Kota = "Depok", Provinsi = "Jawa Barat", KodePos = "16453", Negara = "Indonesia", Anakke = "2", DetailSaudara = "1",
                //Agama = "Islam", Suku = "Sunda", Kewarganegaraan = "Indonesia", TinggiBadan = "160 cm", BeratBadan = "55kg", Goldar = "O", Kelas = "9 Makkah",
                //KontakSiswa = "0896777888", SekolahAsal = "SD Angkasa", StatSekolahAsal = "Swasta", JarakRumahSekolah = "5km", KatSpp = "Umum", TypeDisc = "Rp", NomDisc = "50000", TingkatId = 1,
                //PerDaftar = "2019-2020", Year = "2019", Tahapsatu = DateTime.Now, Tahapdua = DateTime.Now, KatAdm = "Umum", TypeDiscAdm = "%",
                //NomDiscAdm = "10", TglDaftar = DateTime.Now},

                //new Siswa {Nosisda = "2019000002", Fullname = "Raditya", Nickname = "Radit", Nisn = "0857", IsActive = true, Sex = "Laki-laki", Pob = "Lamongan", Dob = "30 Desember 2004",
                //NamaAyah = "Papa", NamaIbu = "Mama", PekerjaanAyah = "Pegawai Negri", PekerjaanIbu= "Wirausaha", NoTelpAyah = "0816888777", NoTelpIbu = "0834777666",
                //EmailOrtu = "PapaMama@gmail.com", Alamat = "Kp. Sukamaju", Kota = "Depok", Provinsi = "Jawa Barat", KodePos = "16455", Negara = "Indonesia", Anakke = "1", DetailSaudara = "1",
                //Agama = "Kristen", Suku = "Betawi", Kewarganegaraan = "Indonesia", TinggiBadan = "165 cm", BeratBadan = "60kg", Goldar = "AB", Periode = "2016-2019", Kelas = "9 Madinah",
                //StatKat = "Umum", KontakSiswa = "0896333444", SekolahAsal = "SD Tadika", StatSekolahAsal = "Negri", JarakRumahSekolah = "20km", Tgldaftar = "20 Juli 2012", GelTest = "1"},

                //new Siswa {Nosisda = "0011858", Fullname = "Hisyam Putra", Nickname = "Hisyam", Nisn = "0858", IsActive = true, Sex = "Laki-laki", Pob = "Bogor", Dob = "28 Oktober 2007",
                //NamaAyah = "Ayah", NamaIbu = "Ibu", PekerjaanAyah = "Wiraswasta", PekerjaanIbu= "Ibu Rumah Tangga", NoTelpAyah = "0816999000", NoTelpIbu = "0834111333",
                //EmailOrtu = "AyahIbu@gmail.com", Alamat = "Kp. Sidamukti", Kota = "Depok", Provinsi = "Jawa Barat", KodePos = "16456", Negara = "Indonesia", Anakke = "2", DetailSaudara = "2",
                //Agama = "Islam", Suku = "Betawi", Kewarganegaraan = "Indonesia", TinggiBadan = "170 cm", BeratBadan = "68kg", Goldar = "O", Periode = "2016-2019", Kelas = "TK A",
                //StatKat = "Umum", KontakSiswa = "089544433", SekolahAsal = "TK Darbi", StatSekolahAsal = "Darbi", JarakRumahSekolah = "5km", Tgldaftar = "20 Juli 2017", GelTest = "1"},

                //Agama = "Kristen", Suku = "Betawi", Kewarganegaraan = "Indonesia", TinggiBadan = "165 cm", BeratBadan = "60kg", Goldar = "AB", Kelas = "9 Madinah",
                //KontakSiswa = "0896333444", SekolahAsal = "SD Tadika", StatSekolahAsal = "Negri", JarakRumahSekolah = "20km", KatSpp = "Umum", TypeDisc = "Rp", NomDisc = "70000", TingkatId = 1,
                //PerDaftar = "2019-2020", Year = "2019", Tahapsatu = DateTime.Now, Tahapdua = DateTime.Now, KatAdm = "Umum", TypeDiscAdm = "%",
                //NomDiscAdm = "20", TglDaftar = DateTime.Now},

                //new Siswa {Nosisda = "2019000002", Fullname = "Awal Ahmad", Nickname = "Awal", Nisn = "0858", IsActive = true, Sex = "Laki-laki", Pob = "Lamongan", Dob = "30 Juli 2004",
                //NamaAyah = "Papa", NamaIbu = "Mama", PekerjaanAyah = "Pegawai Negri", PekerjaanIbu= "Wirausaha", NoTelpAyah = "0816888777", NoTelpIbu = "0834777666",
                //EmailOrtu = "PapaMama@gmail.com", Alamat = "Kp. Sukamaju", Kota = "Depok", Provinsi = "Jawa Barat", KodePos = "16455", Negara = "Indonesia", Anakke = "1", DetailSaudara = "1",
                //Agama = "Islam", Suku = "Betawi", Kewarganegaraan = "Indonesia", TinggiBadan = "165 cm", BeratBadan = "60kg", Goldar = "AB", Kelas = "PG",
                //KontakSiswa = "0896333444", SekolahAsal = "PG", StatSekolahAsal = "Negri", JarakRumahSekolah = "20km", KatSpp = "Umum", TypeDisc = "Rp", NomDisc = "85000", TingkatId = 2,
                //PerDaftar = "2019-2020", Year = "2019", Tahapsatu = DateTime.Now, Tahapdua = DateTime.Now, KatAdm = "Umum", TypeDiscAdm = "%",
                //NomDiscAdm = "30", TglDaftar = DateTime.Now}
            };
            siswas.ForEach(s => context.Siswas.Add(s));
            context.SaveChanges();

            /*var banks = new List<Bank>
            {
                new Bank {Bankname = "bank BCA"},
                new Bank {Bankname = "bank Mandiri"},
                new Bank {Bankname = "bank BRI"}
            };
            banks.ForEach(s => context.Banks.Add(s));
            context.SaveChanges();*/

            var modules = new List<Module>
            {
                new Module {Name = "Dashboard", Area = "Dashboard", Controller="Dashboard", Action = "Index", IsActive = true},
            };
            modules.ForEach(s => context.Modules.Add(s));
            context.SaveChanges();

            var users = new List<User>
            {
                //new User {Email = "admin@admin.com",Password = Security.GetHashString("Password123"), Roles = context.Roles.Where(x => x.Name == Admin.Name).ToList() ,IsActive = true},
                //new User {Email = "user@user.com",Password = Security.GetHashString("Password123"),Roles = context.Roles.Where(x => x.Name == DashboardAccess.Name).ToList(), IsActive = true}
                new User {Fullname = "Admin", Username="admin1",Password = Security.GetHashString("admin123"), Role_Id = 1}
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var jenjangs = new List<Jenjang>
            {
                //new Jenjang {JenjangName="Toddler"},
                //new Jenjang {JenjangName="PG"},
                //new Jenjang {JenjangName="TK A"},
                //new Jenjang {JenjangName="TK B"},
                //new Jenjang {JenjangName="SD"},
                //new Jenjang {JenjangName="SMP"}
            };
            jenjangs.ForEach(s => context.Jenjangs.Add(s));
            context.SaveChanges();

            var bulan = new List<Bulan>
            {
                new Bulan {namaBulan="Januari"},
                new Bulan {namaBulan="Februari"},
                new Bulan {namaBulan="Maret"},
                new Bulan {namaBulan="April"},
                new Bulan {namaBulan="Mei"},
                new Bulan {namaBulan="Juni"},
                new Bulan {namaBulan="Juli"},
                new Bulan {namaBulan="Agustus"},
                new Bulan {namaBulan="September"},
                new Bulan {namaBulan="Oktober"},
                new Bulan {namaBulan="November"},
                new Bulan {namaBulan="Desember"},
            };
            bulan.ForEach(s => context.Bulans.Add(s));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}