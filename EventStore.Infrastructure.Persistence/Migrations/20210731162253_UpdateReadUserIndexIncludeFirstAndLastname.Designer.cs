﻿// <auto-generated />
using System;
using EventStore.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventStore.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(RealDbContext))]
    [Migration("20210731162253_UpdateReadUserIndexIncludeFirstAndLastname")]
    partial class UpdateReadUserIndexIncludeFirstAndLastname
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("EventStore.Infrastructure.Persistence.Entities.EventStoreRecord", b =>
                {
                    b.Property<string>("AggregateRootId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<string>("AggregateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DomainEventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Sequence")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("AggregateRootId", "Version");

                    b.ToTable("EventStore", "write");
                });

            modelBuilder.Entity("EventStore.Infrastructure.Persistence.Entities.User.ReadUser", b =>
                {
                    b.Property<string>("AggregateRootId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("AggregateRootId");

                    b.HasIndex(new[] { "UserName" }, "IX_Username")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL")
                        .IncludeProperties(new[] { "FirstName", "LastName" });

                    b.ToTable("User", "read");
                });
#pragma warning restore 612, 618
        }
    }
}
