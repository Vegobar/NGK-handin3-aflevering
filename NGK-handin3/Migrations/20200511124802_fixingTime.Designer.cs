﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NGK_handin3.Data;

namespace NGK_handin3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200511124802_fixingTime")]
    partial class fixingTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NGK_handin3.Model.Time", b =>
                {
                    b.Property<int>("entry")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("minutes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("month")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("entry");

                    b.ToTable("Time");
                });

            modelBuilder.Entity("NGK_handin3.Model.WeatherObservation", b =>
                {
                    b.Property<int>("WeatherObservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AirPressure")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Timeentry")
                        .HasColumnType("int");

                    b.HasKey("WeatherObservationId");

                    b.HasIndex("Timeentry");

                    b.ToTable("Weather");
                });

            modelBuilder.Entity("NGK_handin3.Model.WeatherObservation", b =>
                {
                    b.HasOne("NGK_handin3.Model.Time", "Time")
                        .WithMany()
                        .HasForeignKey("Timeentry");
                });
#pragma warning restore 612, 618
        }
    }
}