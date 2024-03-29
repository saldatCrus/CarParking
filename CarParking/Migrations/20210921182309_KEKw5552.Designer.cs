﻿// <auto-generated />
using System;
using CarParking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarParking.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210921182309_KEKw5552")]
    partial class KEKw5552
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("CarParking.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecondName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("CarParking.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarParking.Models.ParkingHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTimeEvent")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ParkingPlaceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CarId");

                    b.HasIndex("ParkingPlaceId");

                    b.ToTable("ParkingHistories");
                });

            modelBuilder.Entity("CarParking.Models.ParkingPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CarId");

                    b.ToTable("ParkingPlaces");
                });

            modelBuilder.Entity("CarParking.Models.Car", b =>
                {
                    b.HasOne("CarParking.Models.Account", null)
                        .WithMany("Cars")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("CarParking.Models.ParkingHistory", b =>
                {
                    b.HasOne("CarParking.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("CarParking.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.HasOne("CarParking.Models.ParkingPlace", "ParkingPlace")
                        .WithMany()
                        .HasForeignKey("ParkingPlaceId");

                    b.Navigation("Account");

                    b.Navigation("Car");

                    b.Navigation("ParkingPlace");
                });

            modelBuilder.Entity("CarParking.Models.ParkingPlace", b =>
                {
                    b.HasOne("CarParking.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("CarParking.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.Navigation("Account");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("CarParking.Models.Account", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
