using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApi.PersistenceDB.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movies>
    {
        public void Configure(EntityTypeBuilder<Movies> builder)
        {
            builder.ToTable("Movies");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.ProducedYear)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(x => x.EndDate)
                .IsRequired()
                .HasColumnType("datetime"); ;

            builder.Property(x => x.Price)
                .IsRequired();
        }
    }
}
