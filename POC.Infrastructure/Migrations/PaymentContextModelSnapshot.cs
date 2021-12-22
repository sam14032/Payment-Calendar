﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POC.Infrastructure.Context;

namespace POC.Infrastructure.Migrations
{
    [DbContext(typeof(PaymentContext))]
    partial class PaymentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("POC.Infrastructure.Entity.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("POC.Infrastructure.Entity.Date", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("NextPaymentDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaymentFrequency")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Dates");
                });

            modelBuilder.Entity("POC.Infrastructure.Entity.Payment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int>("ContactMethod")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("POC.Infrastructure.Entity.Comment", b =>
                {
                    b.HasOne("POC.Infrastructure.Entity.Payment", "Payment")
                        .WithOne("Comment")
                        .HasForeignKey("POC.Infrastructure.Entity.Comment", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("POC.Infrastructure.Entity.Date", b =>
                {
                    b.HasOne("POC.Infrastructure.Entity.Payment", "Payment")
                        .WithOne("Date")
                        .HasForeignKey("POC.Infrastructure.Entity.Date", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("POC.Infrastructure.Entity.Payment", b =>
                {
                    b.Navigation("Comment");

                    b.Navigation("Date");
                });
#pragma warning restore 612, 618
        }
    }
}
