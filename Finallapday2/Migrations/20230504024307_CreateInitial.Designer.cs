﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.Database;

#nullable disable

namespace Final_lap_day2.Migrations
{
    [DbContext(typeof(DataDbContext))]
    [Migration("20230504024307_CreateInitial")]
    partial class CreateInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebApplication2.Models.employees", b =>
                {
                    b.Property<string>("empId")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("empName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("hireDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("positionId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("empId");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("WebApplication2.Models.positions", b =>
                {
                    b.Property<string>("positionId")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<float>("baseSalary")
                        .HasColumnType("float");

                    b.Property<string>("positionName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("salaryIncreaseRate")
                        .HasColumnType("float");

                    b.HasKey("positionId");

                    b.ToTable("positions");
                });
#pragma warning restore 612, 618
        }
    }
}
