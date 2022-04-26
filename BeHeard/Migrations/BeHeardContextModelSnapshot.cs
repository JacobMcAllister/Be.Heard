﻿// <auto-generated />
using System;
using BeHeard.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeHeard.Migrations
{
    [DbContext(typeof(BeHeardContext))]
    partial class BeHeardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BeHeard.Application.Models.ActivityResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<int>("Counter")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Decibel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<int>("Exercise")
                        .HasColumnType("int");

                    b.Property<int>("SentenceSet")
                        .HasColumnType("int");

                    b.Property<int>("Syllable")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("ActivityResults");
                });

            modelBuilder.Entity("BeHeard.Application.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ApartmentNumber")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("BeHeard.Application.Models.Preferences", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ColorBlindMode")
                        .HasColumnType("bit");

                    b.Property<bool>("DarkMode")
                        .HasColumnType("bit");

                    b.Property<bool>("TextToSpeech")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("BeHeard.Application.Models.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MasterVolume")
                        .HasColumnType("int");

                    b.Property<Guid?>("PreferencesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubscriptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PreferencesId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("BeHeard.Application.Models.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("BeHeard.Application.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("icon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BeHeard.Application.Models.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SettingsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SettingsId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("BeHeard.Application.Models.ActivityResult", b =>
                {
                    b.HasOne("BeHeard.Application.Models.UserProfile", null)
                        .WithMany("ActivityResults")
                        .HasForeignKey("UserProfileId");
                });

            modelBuilder.Entity("BeHeard.Application.Models.Settings", b =>
                {
                    b.HasOne("BeHeard.Application.Models.User", "User")
                        .WithOne("Settings")
                        .HasForeignKey("BeHeard.Application.Models.Settings", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeHeard.Application.Models.Preferences", "Preferences")
                        .WithMany()
                        .HasForeignKey("PreferencesId");

                    b.HasOne("BeHeard.Application.Models.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId");

                    b.Navigation("Preferences");

                    b.Navigation("Subscription");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BeHeard.Application.Models.User", b =>
                {
                    b.HasOne("BeHeard.Application.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("BeHeard.Application.Models.UserProfile", b =>
                {
                    b.HasOne("BeHeard.Application.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("BeHeard.Application.Models.UserProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BeHeard.Application.Models.Settings", "Settings")
                        .WithMany()
                        .HasForeignKey("SettingsId");

                    b.Navigation("Settings");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BeHeard.Application.Models.User", b =>
                {
                    b.Navigation("Profile");

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("BeHeard.Application.Models.UserProfile", b =>
                {
                    b.Navigation("ActivityResults");
                });
#pragma warning restore 612, 618
        }
    }
}
