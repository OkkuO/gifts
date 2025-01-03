﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using snow_bot;

#nullable disable

namespace snow_bot.Migrations
{
    [DbContext(typeof(GiftDbContext))]
    [Migration("20241207213624_giftMigration")]
    partial class giftMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("snow_bot.GiftModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Bear")
                        .HasColumnType("integer");

                    b.Property<int>("Bottle")
                        .HasColumnType("integer");

                    b.Property<int>("Coal")
                        .HasColumnType("integer");

                    b.Property<int>("Dollar")
                        .HasColumnType("integer");

                    b.Property<int>("Matryoshka")
                        .HasColumnType("integer");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.Property<int>("Ring")
                        .HasColumnType("integer");

                    b.Property<int>("Socks")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GiftModels");
                });
#pragma warning restore 612, 618
        }
    }
}
