﻿// <auto-generated />
using System;
using ApiOnlineShopWeb.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiOnlineShopWeb.Migrations
{
    [DbContext(typeof(ApiOnlineShopWebContext))]
    partial class ApiOnlineShopWebContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("AmountOfDiscount")
                        .HasColumnType("float");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("EndDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<long?>("MaxNumberOfUses")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("TypeOfDiscount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Coupon", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AmountOfDiscount = 111.0,
                            Code = "TestInRange",
                            EndDate = new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            MaxNumberOfUses = 323L,
                            StartDate = new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            TypeOfDiscount = 2
                        },
                        new
                        {
                            Id = 2,
                            AmountOfDiscount = 22.0,
                            Code = "TestOutOfRange",
                            EndDate = new DateTimeOffset(new DateTime(2020, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            MaxNumberOfUses = 32L,
                            StartDate = new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            TypeOfDiscount = 2
                        },
                        new
                        {
                            Id = 3,
                            AmountOfDiscount = 25.0,
                            Code = "Test",
                            EndDate = new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            MaxNumberOfUses = 670L,
                            StartDate = new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            TypeOfDiscount = 1
                        },
                        new
                        {
                            Id = 4,
                            AmountOfDiscount = 50.0,
                            Code = "Testing",
                            EndDate = new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            MaxNumberOfUses = 554L,
                            StartDate = new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            TypeOfDiscount = 1
                        },
                        new
                        {
                            Id = 5,
                            AmountOfDiscount = 75.0,
                            Code = "TestMaxNumberOfUses",
                            EndDate = new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            MaxNumberOfUses = 0L,
                            StartDate = new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)),
                            TypeOfDiscount = 1
                        });
                });

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 1,
                            Name = "Persil",
                            Picture = "persil.jpg",
                            Price = 5.99m,
                            Producer = "Henkel"
                        },
                        new
                        {
                            Id = 2,
                            Category = 4,
                            Name = "Inkpad 4",
                            Picture = "inkpad4.jpg",
                            Price = 239.99m,
                            Producer = "Pocketbook"
                        },
                        new
                        {
                            Id = 3,
                            Category = 2,
                            Name = "Giotto",
                            Picture = "giotto.jpg",
                            Price = 2.99m,
                            Producer = "Ferrero"
                        },
                        new
                        {
                            Id = 4,
                            Category = 3,
                            Name = "Reis",
                            Picture = "reis.jpg",
                            Price = 0.99m,
                            Producer = "Bioland"
                        });
                });

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.ProductInCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionHistoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TransactionHistoryId");

                    b.ToTable("ProductInCart", (string)null);
                });

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.TransactionHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTimeOffset>("PaymentDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("TransactionHistory", (string)null);
                });

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GivenName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 34,
                            City = "Hamburg",
                            Country = "Germany",
                            EMail = "igor@gmail.com",
                            GivenName = "Igor",
                            HouseNumber = 154,
                            Password = "123456",
                            PostalCode = 22526,
                            Street = "Berner Chaussee",
                            Surname = "Il"
                        },
                        new
                        {
                            Id = 2,
                            Age = 38,
                            City = "Harburg",
                            Country = "Germany",
                            EMail = "yury@gmail.com",
                            GivenName = "Yury",
                            HouseNumber = 22,
                            Password = "123456",
                            PostalCode = 22041,
                            Street = "Harburger Chaussee",
                            Surname = "Spi"
                        },
                        new
                        {
                            Id = 3,
                            Age = 33,
                            City = "Berlin",
                            Country = "Germany",
                            EMail = "dirk@gmail.com",
                            GivenName = "Dirk",
                            HouseNumber = 232,
                            Password = "123456",
                            PostalCode = 25014,
                            Street = "Berliner Straße",
                            Surname = "Es"
                        });
                });

            modelBuilder.Entity("CouponTransactionHistory", b =>
                {
                    b.Property<int>("CouponsId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionHistoriesId")
                        .HasColumnType("int");

                    b.HasKey("CouponsId", "TransactionHistoriesId");

                    b.HasIndex("TransactionHistoriesId");

                    b.ToTable("TransactionHistoryToCoupons", (string)null);
                });

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.ProductInCart", b =>
                {
                    b.HasOne("ApiOnlineShopWeb.Domain.Product", "Product")
                        .WithMany("CartProduct")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiOnlineShopWeb.Domain.TransactionHistory", "TransactionHistory")
                        .WithMany("ProductsInCart")
                        .HasForeignKey("TransactionHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("TransactionHistory");
                });

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.TransactionHistory", b =>
                {
                    b.HasOne("ApiOnlineShopWeb.Domain.Product", null)
                        .WithMany("TransactionHistories")
                        .HasForeignKey("ProductId");

                    b.HasOne("ApiOnlineShopWeb.Domain.User", "User")
                        .WithMany("TransactionHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CouponTransactionHistory", b =>
                {
                    b.HasOne("ApiOnlineShopWeb.Domain.Coupon", null)
                        .WithMany()
                        .HasForeignKey("CouponsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiOnlineShopWeb.Domain.TransactionHistory", null)
                        .WithMany()
                        .HasForeignKey("TransactionHistoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.Product", b =>
                {
                    b.Navigation("CartProduct");

                    b.Navigation("TransactionHistories");
                });

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.TransactionHistory", b =>
                {
                    b.Navigation("ProductsInCart");
                });

            modelBuilder.Entity("ApiOnlineShopWeb.Domain.User", b =>
                {
                    b.Navigation("TransactionHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
