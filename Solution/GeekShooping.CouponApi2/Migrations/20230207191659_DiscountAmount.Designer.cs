﻿// <auto-generated />
using GeekShooping.CouponApi.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekShooping.CouponApi.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20230207191659_DiscountAmount")]
    partial class DiscountAmount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GeekShooping.CouponApi.Model.Coupon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id");

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("coupon_code");

                    b.Property<decimal>("DiscountAmount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("Discount_amount");

                    b.HasKey("Id");

                    b.ToTable("coupon");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CouponCode = "ERUDIO_2022_10",
                            DiscountAmount = 10m
                        },
                        new
                        {
                            Id = 2L,
                            CouponCode = "ERUDIO_2022_15",
                            DiscountAmount = 15m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
