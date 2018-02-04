﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SeminarMVCServis.DAL.Models;
using System;

namespace SeminarMVCServis.DAL.Migrations
{
    [DbContext(typeof(SeminarContext))]
    partial class SeminarContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.Adresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Broj");

                    b.Property<string>("Drzava");

                    b.Property<string>("Mjesto");

                    b.Property<string>("Ulica");

                    b.HasKey("Id");

                    b.ToTable("Adresa");
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.Gost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ime");

                    b.Property<string>("Prezime");

                    b.Property<int>("RestoranId");

                    b.HasKey("Id");

                    b.HasIndex("RestoranId");

                    b.ToTable("Gost");
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.GostJelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GostId");

                    b.Property<int>("JeloId");

                    b.HasKey("Id");

                    b.HasIndex("GostId");

                    b.HasIndex("JeloId");

                    b.ToTable("GostJelo");
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.Jelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Cijena");

                    b.Property<string>("Naziv");

                    b.Property<int?>("RestoranId");

                    b.HasKey("Id");

                    b.HasIndex("RestoranId");

                    b.ToTable("Jelo");
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.Restoran", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdresaId");

                    b.Property<string>("Naziv");

                    b.HasKey("Id");

                    b.HasIndex("AdresaId");

                    b.ToTable("Restoran");
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.Sastojak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Jedinica");

                    b.Property<string>("Naziv");

                    b.HasKey("Id");

                    b.ToTable("Sastojak");
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.SastojakJelo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JeloId");

                    b.Property<float>("Kolicina");

                    b.Property<int>("SastojakId");

                    b.HasKey("Id");

                    b.HasIndex("JeloId");

                    b.HasIndex("SastojakId");

                    b.ToTable("SastojakJelo");
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserName");

                    b.Property<int>("UserPIN");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.Gost", b =>
                {
                    b.HasOne("SeminarMVCServis.DAL.Models.Restoran", "Restoran")
                        .WithMany("Gosti")
                        .HasForeignKey("RestoranId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.GostJelo", b =>
                {
                    b.HasOne("SeminarMVCServis.DAL.Models.Gost", "Gost")
                        .WithMany()
                        .HasForeignKey("GostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SeminarMVCServis.DAL.Models.Jelo", "Jelo")
                        .WithMany()
                        .HasForeignKey("JeloId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.Jelo", b =>
                {
                    b.HasOne("SeminarMVCServis.DAL.Models.Restoran")
                        .WithMany("Menu")
                        .HasForeignKey("RestoranId");
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.Restoran", b =>
                {
                    b.HasOne("SeminarMVCServis.DAL.Models.Adresa", "Adresa")
                        .WithMany()
                        .HasForeignKey("AdresaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SeminarMVCServis.DAL.Models.SastojakJelo", b =>
                {
                    b.HasOne("SeminarMVCServis.DAL.Models.Jelo", "Jelo")
                        .WithMany()
                        .HasForeignKey("JeloId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SeminarMVCServis.DAL.Models.Sastojak", "Sastojak")
                        .WithMany()
                        .HasForeignKey("SastojakId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}