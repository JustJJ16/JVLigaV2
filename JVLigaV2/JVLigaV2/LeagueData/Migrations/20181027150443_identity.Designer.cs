﻿// <auto-generated />
using System;
using JVLigaV2.LeagueData;
using LeagueData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LeagueData.Migrations
{
    [DbContext(typeof(LeagueContext))]
    [Migration("20181027150443_identity")]
    partial class identity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LeagueData.Models.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CITY");

                    b.Property<string>("STATE");

                    b.Property<string>("STREET");

                    b.Property<int>("ZIP_CODE");

                    b.HasKey("ID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("LeagueData.Models.Hall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressID");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AddressID");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("LeagueData.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EMAIL");

                    b.Property<string>("FIRST_NAME");

                    b.Property<string>("LAST_NAME");

                    b.Property<string>("PASSWD");

                    b.Property<int>("ROLE");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LeagueData.Models.Hall", b =>
                {
                    b.HasOne("LeagueData.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID");
                });
#pragma warning restore 612, 618
        }
    }
}
