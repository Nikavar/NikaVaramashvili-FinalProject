using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieWebApi.Domain.POCO;
using MovieWebApi.PersistenceDB.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApi.PersistenceDB.Context
{
    public class MovieDbContext : DbContext
    {
        #region Ctor

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }

        #endregion

        #region DbSets

        public DbSet<Users> Users { get; set; }
        public DbSet<Movies> Movies{ get; set; }
        public DbSet<OrderAPI> Orders { get; set; }

        #endregion

        #region Configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
