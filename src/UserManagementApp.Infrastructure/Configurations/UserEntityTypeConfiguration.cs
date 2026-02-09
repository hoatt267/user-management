using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagementApp.Domain.Constants;
using UserManagementApp.Domain.Entities;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Infrastructure.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(DataSchemaLength.LARGE);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(DataSchemaLength.SUPER_LARGE);
            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}