﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorldsAdriftServer.Helper.Data;

#nullable disable

namespace WorldsAdriftServer.Migrations
{
    [DbContext(typeof(GameContext))]
    partial class GameContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("WorldsAdriftServer.Objects.CharacterSelection.CharacterCreationData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Cosmetics")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsMale")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("SeenIntro")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Server")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("SkippedTutorial")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UniversalColors")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("serverIdentifier")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
