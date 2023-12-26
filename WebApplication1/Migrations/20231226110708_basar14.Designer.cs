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
    [Migration("20231226110708_basar14")]
    partial class basar14
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
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("ItemBrandId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("WebApplication1.Models.Camera", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Item");

                    b.Property<int>("CameraFilmFormat")
                        .HasColumnType("int");

                    b.Property<int>("CameraFocalLength")
                        .HasColumnType("int");

                    b.Property<int>("CameraMaxShutterSpeed")
                        .HasColumnType("int");

                    b.Property<int>("CameraMegapixel")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Camera");

                    b.HasData(
                        new
                        {
                            GUID = new Guid("ddfbf00e-024a-4dc5-b82d-6324ac25f908"),
                            AdditionalImageUrls = "[\"https://example.com/canon_eos_rebel_t7_1.jpg\",\"https://example.com/canon_eos_rebel_t7_2.jpg\"]",
                            Brand = "Canon",
                            Description = "A versatile and affordable entry-level DSLR camera.",
                            IsAvailable = true,
                            ItemBrandId = 0,
                            ItemType = 1,
                            Price = 499.99m,
                            Quantity = 100,
                            Title = "Canon EOS Rebel T7",
                            TitleImageUrl = "https://example.com/canon_eos_rebel_t7.jpg",
                            CameraFilmFormat = 35,
                            CameraFocalLength = 50,
                            CameraMaxShutterSpeed = 1000,
                            CameraMegapixel = 20
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.Film", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Item");

                    b.Property<int>("FilmColorState")
                        .HasColumnType("int");

                    b.Property<int>("FilmExposure")
                        .HasColumnType("int");

                    b.Property<int>("FilmFormat")
                        .HasColumnType("int");

                    b.Property<int>("FilmISO")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Film");

                    b.HasData(
                        new
                        {
                            GUID = new Guid("e5574e36-2afa-4dd7-931e-c10fcecc0383"),
                            AdditionalImageUrls = "[\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\",\"https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg\"]",
                            Brand = "Kodak",
                            Description = "Kodak Portra 400 is a color negative film great for portraits, fashion and commercial shoots. This film is known for its beautiful skin tones and natural colors.",
                            IsAvailable = true,
                            ItemBrandId = 2,
                            ItemType = 0,
                            Price = 10.99m,
                            Quantity = 400,
                            Title = "Kodak Portra 400",
                            TitleImageUrl = "https://www.bhphotovideo.com/images/images2500x2500/kodak_6031678_portra_400_color_negative_35mm_1038169.jpg",
                            FilmColorState = 0,
                            FilmExposure = 36,
                            FilmFormat = 35,
                            FilmISO = 400
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
