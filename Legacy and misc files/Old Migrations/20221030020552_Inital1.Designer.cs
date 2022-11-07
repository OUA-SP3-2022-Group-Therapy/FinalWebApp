﻿// <auto-generated />
using System;
using GroupTherapyWebAppFinal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GroupTherapyWebAppFinal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221030020552_Inital1")]
    partial class Inital1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("GroupTherapyWebAppFinal.Models.UserModel", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("UserModel");
                });
#pragma warning restore 612, 618
        }
    }
}