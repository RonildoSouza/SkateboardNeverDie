﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkateboardNeverDie.Infrastructure.Database;

namespace SkateboardNeverDie.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210106231522_ChangeColumnTypeInTableStances")]
    partial class ChangeColumnTypeInTableStances
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("SkateboardNeverDie.Domain.Skaters.Skater", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("NaturalStanceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NaturalStanceId");

                    b.ToTable("Skaters");
                });

            modelBuilder.Entity("SkateboardNeverDie.Domain.Stances.Stance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Stances");
                });

            modelBuilder.Entity("SkateboardNeverDie.Domain.Tricks.Trick", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Id", "Name")
                        .IsUnique();

                    b.ToTable("Tricks");
                });

            modelBuilder.Entity("SkateboardNeverDie.Domain.Skaters.Skater", b =>
                {
                    b.HasOne("SkateboardNeverDie.Domain.Stances.Stance", "NaturalStance")
                        .WithMany()
                        .HasForeignKey("NaturalStanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("SkateboardNeverDie.Domain.Skaters.SkaterTrick", "SkaterTricks", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("SkaterId")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("TrickId")
                                .HasColumnType("TEXT");

                            b1.HasKey("Id");

                            b1.HasIndex("SkaterId");

                            b1.HasIndex("TrickId");

                            b1.ToTable("SkaterTricks");

                            b1.WithOwner()
                                .HasForeignKey("SkaterId");

                            b1.HasOne("SkateboardNeverDie.Domain.Tricks.Trick", "Trick")
                                .WithMany()
                                .HasForeignKey("TrickId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.OwnsMany("SkateboardNeverDie.Domain.Skaters.SkaterTrickVariation", "SkaterTrickVariations", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("TEXT");

                                    b2.Property<Guid>("SkaterTrickId")
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("StanceId")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.HasKey("Id");

                                    b2.HasIndex("SkaterTrickId");

                                    b2.HasIndex("StanceId");

                                    b2.ToTable("SkaterTrickVariations");

                                    b2.WithOwner()
                                        .HasForeignKey("SkaterTrickId");

                                    b2.HasOne("SkateboardNeverDie.Domain.Stances.Stance", "Stance")
                                        .WithMany()
                                        .HasForeignKey("StanceId")
                                        .OnDelete(DeleteBehavior.Cascade)
                                        .IsRequired();

                                    b2.Navigation("Stance");
                                });

                            b1.Navigation("SkaterTrickVariations");

                            b1.Navigation("Trick");
                        });

                    b.Navigation("NaturalStance");

                    b.Navigation("SkaterTricks");
                });
#pragma warning restore 612, 618
        }
    }
}
