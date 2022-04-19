using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieWebApi.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApi.PersistenceDB.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(15);



            //relations between 'SystemRoles' and 'Users'
            //builder.HasOne(x => x.Role)
            //    .WithMany(x => x.Users)
            //    .HasForeignKey(x => x.RoleId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
