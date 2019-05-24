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

<<<<<<< HEAD
		public DbSet<Jenjang> Jenjangs { get; set; }
=======
        public DbSet<Tingkat> Tingkats { get; set; }
        public DbSet<Bank> Banks { get; set; }
        

>>>>>>> 911c1ea6dfe489dd1edcc34639d190bf150371b8

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}