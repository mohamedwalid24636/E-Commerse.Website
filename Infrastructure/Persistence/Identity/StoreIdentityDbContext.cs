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
    public class StoreIdentityDbContext (DbContextOptions<StoreIdentityDbContext> Options) : IdentityDbContext<IdentityUser> (Options)   
    {
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);  //(Automatic DbSets Applied)
            modelBuilder.Entity<Address>().ToTable("Addresses");
            modelBuilder.Entity<IdentityUser>().ToTable("Users");  
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
// عشان متحعملش Seven Dbcontexts For Each Model inside The Identity 
// (Automatic DbSets)
// Add-Migration "IdentityInitialCreate"  -Context "StoreIdentityDbContext" -OutPutDir "Identity/Migrations"   ==> Default Project Persistence Layer 


// <- بنورث الحاجات ذيه من ال Package ->
//IdenttyDbContext 
//IdentityUser 















//  <- Use Identity Package Steps -> 
// 1- Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
// Create ApplicationUser : IdentityUser 
// Create (MedoIdentityDbContext) : IdentityDbContext<IdentityUser>
// Make Connection 