using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using DomanLayer.Models.IdentityModule;
using DomanLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _DbContext,
        UserManager<ApplicationUser> _usermanager ,                       // UserManager , RoleManager => Needs Service Registretion (Allow Dependancy Inject so Ask CLR )
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _IdentityDbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                var PenddingMigrations = await _DbContext.Database.GetPendingMigrationsAsync();
                if (PenddingMigrations.Any())
                    {
                      await  _DbContext.Database.MigrateAsync();
                    }
                    if (!_DbContext.ProductBrands.Any())
                    {
                    //var ProductBrandsData = File.ReadAllText(@"../Infrastructure/Persistence/Data/DataSeed/brands.json");
                    //var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);
                    var ProductBrandsData = File.OpenRead(@"../Infrastructure/Persistence/Data/DataSeed/brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);   // DeserializeAsync تستقبل Stream
                    if (ProductBrands is not null && ProductBrands.Any())
                        {
                           await _DbContext.ProductBrands.AddRangeAsync(ProductBrands);
                        }
                    }

                    if (!_DbContext.ProductTypes.Any())
                    {
                        var ProductTypesData = File.OpenRead(@"../Infrastructure/Persistence/Data/DataSeed/types.json");
                        var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);
                        if (ProductTypes is not null && ProductTypes.Any())
                        {
                        await _DbContext.ProductTypes.AddRangeAsync(ProductTypes);
                        }
                    }

                    if (!_DbContext.Products.Any())
                    {
                        var ProductsData = File.OpenRead(@"../Infrastructure/Persistence/Data/DataSeed/Products.json");
                        var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                        if (Products is not null && Products.Any())
                        {
                            await _DbContext.Products.AddRangeAsync(Products);
                        }
                    }

                if (!_DbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodsDataStreem = File.OpenRead(@"../Infrastructure/Persistence/Data/DataSeed/delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodsDataStreem);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        await _DbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethods);
                    }
                }


                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               // To Do 
            }


            // await لو فيها عمليات مستنياني قلها تستني لغاية ما اخلص 
            // اعمل تاسك غيرها علي ما اخلص 


        }
        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_usermanager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        UserName = "MohamedWalid",
                        Email = "Mewalid24636@gmail.com",
                        PhoneNumber = "01005041584",
                        DisplayName = "SoftWare Engneerer"
                    };

                    var User02 = new ApplicationUser()
                    {
                        UserName = "EzzWalid",
                        Email = "Ezzo@gmail.com",
                        PhoneNumber = "01008057461",
                        DisplayName = "Miletary Man"
                    };


                    await _usermanager.CreateAsync(User01, "Mody1310#");     // بببعت ال باسوورذ هنا عشان يتعمله Hashing 
                    await _usermanager.CreateAsync(User02, "Ezz1310#");

                    await _usermanager.AddToRoleAsync(User01, "SuperAdmin");    // Assign A Specific Roles To Users
                    await _usermanager.AddToRoleAsync(User02, "Admin");

                }

                await _IdentityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}




// خذ بالك CreateAsync بتاع ال UserManager  و RoleManager  بيرجعلي بيعمل SaveChanger Automaticly inside it

// لما حصل مشكلة في اضافة ال يوزر رجلي مشكلة هنا   await _IdentityDbContext.SaveChangesAsync();