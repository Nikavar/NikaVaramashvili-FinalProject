using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApi.PersistenceDB.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderAPI>
    {
        public void Configure(EntityTypeBuilder<OrderAPI> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.MovieId).IsRequired();
            builder.Property(x=>x.UserId).IsRequired(); 
            builder.Property(x=>x.Price).IsRequired();
        }
    }
}
