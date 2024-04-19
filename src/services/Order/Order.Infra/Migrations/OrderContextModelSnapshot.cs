﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Order.Infra.Contexts;

#nullable disable

namespace Order.Infra.Migrations
{
    [DbContext(typeof(OrderContext))]
    partial class OrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Order.Domain.Entities.OrderEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasMaxLength(100)
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("createAt");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnName("status");

                    b.Property<long>("Person")
                        .HasColumnType("BIGINT")
                        .HasColumnName("person");

                    b.Property<DateTime>("UpdateAt")
                        .HasMaxLength(100)
                        .HasColumnType("TIMESTAMP")
                        .HasColumnName("updateAt");

                    b.HasKey("Id");

                    b.ToTable("Orders", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
