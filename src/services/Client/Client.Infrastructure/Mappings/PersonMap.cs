﻿using Client.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Client.Infrastructure.Mappings;

public class PersonMap : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("id")
                .HasColumnType("BIGINT");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(80)");

            builder.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("role")
                .HasColumnType("VARCHAR(10)");
            
            builder.Property(x => x.Document)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("document")
                .HasColumnType("VARCHAR(10)");
        
            builder.Property(x => x.CreateAt)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("createAt")
                .HasColumnType("TIMESTAMP");

            builder.Property(x => x.UpdatedAt)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("updatedAt")
                .HasColumnType("TIMESTAMP");
    }   
}
