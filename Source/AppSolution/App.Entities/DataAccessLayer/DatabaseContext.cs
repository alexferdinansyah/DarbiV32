using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using App.Entities.Models;

namespace App.Entities.DataAccessLayer
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext() : base("name=DatabaseContext")
		{
			//Database.SetInitializer<DatabaseContext>(new CreateDatabaseIfNotExists<DatabaseContext>());
			//Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
			//Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseAlways<DatabaseContext>());
			Database.SetInitializer<DatabaseContext>(new DatabaseInitializer());

			//Disable initializer
			//Database.SetInitializer<DatabaseContext>(null);
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Module> Modules { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<RequestForgotPassword> RequestForgotPasswords { get; set; }

		public DbSet<Bank> Banks { get; set; }
		public DbSet<Jenjang> Jenjangs { get; set; }
        public DbSet<Tingkat> Tingkats { get; set; }
        public DbSet<Siswa> Siswas { get; set; }
		public DbSet<Kelas> Kelass { get; set; }
        public DbSet<SchoolSupport> SchoolSupports { get; set; }
        public DbSet<Biaya> Biayas { get; set; }
        public DbSet<HistoryBiaya> HistoryBiayas { get; set; }
        public DbSet<RegSiswa> RegSiswas { get; set; }
        public DbSet<DetailSaudara> DetailSaudaras { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}