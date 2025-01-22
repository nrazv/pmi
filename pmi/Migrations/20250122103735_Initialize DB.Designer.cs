﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pmi.DataContext;

#nullable disable

namespace pmi.Migrations
{
    [DbContext(typeof(PmiDb))]
    [Migration("20250122103735_Initialize DB")]
    partial class InitializeDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("pmi.Project.Models.ProjectEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("DomainName")
                        .HasColumnType("TEXT");

                    b.Property<string>("IpAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectInfoId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ProjectInfoId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("pmi.Project.Models.ProjectInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ProjectsInfo");
                });

            modelBuilder.Entity("pmi.Project.Models.ProjectEntity", b =>
                {
                    b.HasOne("pmi.Project.Models.ProjectInfo", "ProjectInfo")
                        .WithMany()
                        .HasForeignKey("ProjectInfoId");

                    b.Navigation("ProjectInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
