﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231225161535_Migration3")]
    partial class Migration3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Models.Item", b =>
                {
                    b.Property<Guid>("GUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalImageUrls")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GUID");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WebApplication1.Models.Film", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Item");

                    b.Property<int>("FilmColorState")
                        .HasColumnType("int");

                    b.Property<int>("FilmExposure")
                        .HasColumnType("int");

                    b.Property<int>("FilmISO")
                        .HasColumnType("int");

                    b.Property<int>("FilmSize")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Film");

                    b.HasData(
                        new
                        {
                            GUID = new Guid("0ad5f087-44ac-4489-bd34-5551db859bdb"),
                            AdditionalImageUrls = "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]",
                            Brand = "Ilford",
                            Description = "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.",
                            IsAvailable = true,
                            ItemType = 0,
                            Price = 10.99m,
                            Quantity = 400,
                            Title = "Kodak Portra 400",
                            TitleImageUrl = "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg",
                            FilmColorState = 0,
                            FilmExposure = 36,
                            FilmISO = 400,
                            FilmSize = 35
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
