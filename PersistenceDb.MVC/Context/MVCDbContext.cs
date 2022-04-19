using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieWebApi.Domain.POCO;
using MovieWebApi.PersistenceDB.Context;

namespace PersistenceDb.MVC.Context
{
    public class MVCDbContext : IdentityDbContext<ApplicationUser>
    {
        public MVCDbContext(DbContextOptions<MVCDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MVCDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMovie> OrderMovies { get; set; }
    }

}
