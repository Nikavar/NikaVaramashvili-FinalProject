using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieWebApi.PersistenceDB.Context;
using MovieWebApi.PersistenceDB.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApi.PersistenceDB.Extensions
{
    public static class MigrationManager
    {
        //public static IHost MigrateDatabase(this IHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        using (var context = scope.ServiceProvider.GetRequiredService<MovieDbContext>())
        //        {
        //            try
        //            {
        //                context.Database.Migrate();
        //                MovieManagementSeed.SeedInitialData(context);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //        }
        //    }
        //    return host;
        //}
    }
}

