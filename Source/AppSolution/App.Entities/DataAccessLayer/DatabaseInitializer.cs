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


            //var roles = new List<Role>
            //{
            //    new Role {Name = "Admin",IsActive = true},
            //    new Role {Name = "User Manager",IsActive = true},
            //    new Role {Name = "Role Manager",IsActive = true},
            //    new Role {Name = "Module Manager",IsActive = true},
            //    new Role {Name = "Dashboard Access",IsActive = true}
            //};
            //roles.ForEach(s => context.Roles.Add(s));
            //context.SaveChanges();

            //Role Admin = context.Roles.Where(x => x.Name == "Admin").FirstOrDefault();
            //Role UserManager = context.Roles.Where(x => x.Name == "User Manager").FirstOrDefault();
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

            var banks = new List<Bank>
            {
                new Bank {Bankname = "bank mandiri"}
            };
            banks.ForEach(s => context.Banks.Add(s));
            context.SaveChanges();

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
                new User {Fullname = "Admin", Username="admin1",Password = Security.GetHashString("admin123")}
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}