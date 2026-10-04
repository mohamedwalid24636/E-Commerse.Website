using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence.Identity
{
    public class StoreIdentityDbContext (DbContextOptions<StoreIdentityDbContext> Options) : IdentityDbContext<ApplicationUser> (Options)   
    {
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);  //(Automatic DbSets Applied)
            modelBuilder.Entity<Address>().ToTable("Addresses");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");  
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");  
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            // خذ بالك tables اتعملت لما ناذيت البيز كلاس و بعذيها لغتها تاتي 


        }



    }
}


//Identity DbContext 
// عشان متعملش Seven SetDbcontexts For Each Model inside The Identity 
// (Automatic DbSets)
// Add-Migration "IdentityInitialCreate"  -Context "StoreIdentityDbContext" -OutPutDir "Identity/Migrations"   ==> Default Project Persistence Layer 


// <- بنورث الحاجات ذيه من ال Package ->
//IdenttyDbContext 
//IdentityUser 





//when U Used : IdentityDbContext<ApplicationUser>    انت عملت تلت حاجات 
// 1- فيه سبع جذاول اتعملهم DbSet <> تلقائي  IdentityEntites 
// 2- في Configurations جايه مع ال IdentityPackage اتعملها Apply by base.OnModdelCreating()
// 3- انت عرفتهم انك هتستخذم Custom Application User Not Normal Identty User Model









//  <- Use Identity Package Steps -> 
// 1- Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
// 2- Create ApplicationUser in Models 
// 3- Create StoreIdentityDataBase 
// 4- Ask Clr To GetConnection