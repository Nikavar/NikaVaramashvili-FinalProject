using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieWebApi.PersistenceDB.Context;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;

namespace MovieWebApi.PersistenceDB.Seed
{
    public class MovieManagementSeed
    {
        public static void SeedInitialData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MovieDbContext>();
                context.Database.EnsureCreated();

                //Seed For Users
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<Users>
                    {
                        new Users()
                        {
                            FirstName="Nika",
                            LastName="Varamashvili",
                            UserName="VARNIKOLOZ",
                            Password="Varama123!"

                        }
                    });
                    context.SaveChanges();
                }

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
    }
}
