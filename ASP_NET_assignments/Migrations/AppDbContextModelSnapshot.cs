﻿// <auto-generated />
using ASP_NET_assignments.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASP_NET_assignments.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASP_NET_assignments.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phonenumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PeopleDataTable");

                    b.HasData(
                        new
                        {
                            Id = 399444226,
                            City = "Göteborg",
                            Name = "Jens Eresund",
                            Phonenumber = "+46706845909"
                        },
                        new
                        {
                            Id = 523178533,
                            City = "Staden",
                            Name = "Abel Abrahamsson",
                            Phonenumber = "+00123456789"
                        },
                        new
                        {
                            Id = 1591867130,
                            City = "Skogen",
                            Name = "Bror Björn",
                            Phonenumber = "+5555555555"
                        },
                        new
                        {
                            Id = 312203518,
                            City = "Luftslottet",
                            Name = "Örjan Örn",
                            Phonenumber = "1111111111"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
