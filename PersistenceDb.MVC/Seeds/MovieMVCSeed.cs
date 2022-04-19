using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using PersistenceDb.MVC.Context;
using MovieWebApi.Domain.POCO;
using MovieWebApi.PersistenceDB.Context;

namespace PersistenceDb.MVC.Seeds
{
    public class MovieMVCSeed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MVCDbContext>();
                context.Database.EnsureCreated();

                //Seed for Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movies>
                    {
                        new Movies()
                    {
                        Title = "House",
                        ProducedYear = 2004,
                        Description = "Witty and arrogant medical doctor Gregory House",
                        PosterUrl = "D:/TBC Acad 2.0/Final Project/PosterData/DoctorHouse.jpg",
                        //PosterUrl="file:///D:/TBC%20Acad%202.0/Final%20Project/PosterData/DoctorHouse.jpg",
                        StartDate = DateTime.Now.AddDays(-2),
                        EndDate = DateTime.Now.AddDays(2),
                        Subject = "The series follows the life of anti-social, pain killer addict, witty and arrogant medical doctor Gregory House with  only half a muscle in his right leg...",
                        Price = 15.6,
                        IsActive = true
                    },

                        new Movies()
                    {
                         Title = "LIE TO ME",
                        ProducedYear = 2009,
                        Description = "Dr. Cal Lightman teaches a course in body language...",
                        PosterUrl = "D:\\TBC Acad 2.0\\Final Project\\PosterData\\LieToMe.jpg",
                        //PosterUrl = "file:///D://TBC%20Acad%202.0//Final%20Project//PosterData//LieToMe.jpg",
                        //PosterUrl = "https://usaupload.com/6i45/LieToMe.jpg",
                        StartDate = DateTime.Now.AddDays(-3),
                        EndDate = DateTime.Now.AddDays(3),
                        Subject = "Dr. Cal Lightman teaches a course in body language and makes an honest fortune exploiting it. He's employed by various public authorities in ...",
                        Price = 25.6,
                        IsActive = true
                    },

                        new Movies()
                        {
                            Title = "RONIN",
                            ProducedYear = 1998,
                            Description = "Ronin is the Japanese word used for Samurai without a master...",
                            PosterUrl = "D:\\TBC Acad 2.0\\Final Project\\PosterData\\Ronin.jpg",
                            //PosterUrl = "file:///D://TBC%20Acad%202.0//Final%20Project//PosterData//Ronin.jpg",
                            //PosterUrl = "https://usaupload.com/6i49/Ronin.jpg",
                            StartDate = DateTime.Now.AddDays(-1),
                            EndDate = DateTime.Now.AddDays(2),
                            Subject = "Ronin is the Japanese word used for Samurai without a master. In this case, the Ronin are outcast specialists of every kind, whose services are ...",
                            Price = 17.80,
                            IsActive = true
                        }                        
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(SystemRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(SystemRoles.Admin));

                }
                if (!await roleManager.RoleExistsAsync(SystemRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(SystemRoles.User));

                }
                if (!await roleManager.RoleExistsAsync(SystemRoles.Manager))
                {
                    await roleManager.CreateAsync(new IdentityRole(SystemRoles.Manager));

                }
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var admin = await userManager.FindByEmailAsync("admin@tbcmovie.com");
                
                if (admin == null)
                {

                    var newAdmin = new ApplicationUser()
                    {
                        FullName = "Admin Name",
                        UserName = "Admin",
                        Email = "admin@tbcmovie.com",
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(newAdmin, "Admin123!");
                    await userManager.AddToRoleAsync(newAdmin, SystemRoles.Admin);

                }

                var manager = await userManager.FindByEmailAsync("manager@tbcmovie.com");
                if (manager == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        FullName = "Manager Name",
                        UserName = "Manager",
                        Email = "manager@tbcmovie.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newUser, "Manager123!");
                    await userManager.AddToRoleAsync(newUser, SystemRoles.Manager);
                }


                var user = await userManager.FindByEmailAsync("user@tbcmovie.com");
                if (user == null)
                {
                    var newUser = new ApplicationUser()
                    {
                        FullName = "User Name",
                        UserName = "User",
                        Email = "user@tbcmovie.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newUser, "User123!");
                    await userManager.AddToRoleAsync(newUser, SystemRoles.User);
                }

            }     
        }
    }
}
