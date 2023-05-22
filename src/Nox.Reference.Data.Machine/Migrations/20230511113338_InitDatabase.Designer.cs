﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nox.Reference.Data.Machine;

#nullable disable

namespace Nox.Reference.Data.Machine.Migrations
{
    [DbContext(typeof(MachineDbContext))]
    [Migration("20230511113338_InitDatabase")]
    partial class InitDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Nox.Reference.Data.MacAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("IEEERegistry")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MacPrefix")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OrganizationAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OrganizationName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MacAddress");
                });
#pragma warning restore 612, 618
        }
    }
}
