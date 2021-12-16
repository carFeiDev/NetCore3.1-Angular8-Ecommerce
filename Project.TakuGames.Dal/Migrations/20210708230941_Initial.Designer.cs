﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.TakuGames.Dal;

namespace Project.TakuGames.Dal.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20210708230941_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.TakuGames.Model.Domain.Cart", b =>
                {
                    b.Property<string>("CartId")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("int");

                    b.HasKey("CartId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.CartItems", b =>
                {
                    b.Property<int>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CartId")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartItemId")
                        .HasName("PK_CarItem_488B0B0AA0297D1C");

                    b.ToTable("CardItems");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.Categories", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CategoryId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("CategoryId")
                        .HasName("PK_Categori_19093A2B46B8DFC9");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.CustomerOrderDetails", b =>
                {
                    b.Property<int>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoverFileName")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("OrderDetailsId")
                        .HasName("PK_Customer_9DD74DBD81D9221B");

                    b.ToTable("CustomerOrderDetails");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.CustumerOrders", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<decimal>("CartTotal")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId")
                        .HasName("PK_Customer_C3905BCF96C8F1E7");

                    b.ToTable("CustumerOrders");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.Favoritelist", b =>
                {
                    b.Property<string>("FavoritelistId")
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnName("UserID")
                        .HasColumnType("int");

                    b.HasKey("FavoritelistId");

                    b.ToTable("Favoritelist");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.FavoritelistItems", b =>
                {
                    b.Property<int>("FavoritelistItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FavoritelistId")
                        .IsRequired()
                        .HasColumnType("varchar(36)")
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("FavoritelistItemId")
                        .HasName("PK__Wishlist__171E21A16A5148A4");

                    b.ToTable("FavoritelistItems");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("GameId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("CoverFileName")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("Description")
                        .HasColumnType("varchar(max)")
                        .IsUnicode(false);

                    b.Property<string>("Developer")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Platform")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Publisher")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Title")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("GameId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.Games", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.UserMaster", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("varchar(6)")
                        .HasMaxLength(6)
                        .IsUnicode(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<int>("UserTypeId")
                        .HasColumnName("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("UserId")
                        .HasName("PK_UserMast_1788CCAC2694A2ED");

                    b.ToTable("UserMaster");
                });

            modelBuilder.Entity("Project.TakuGames.Model.Domain.UserType", b =>
                {
                    b.Property<int>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeId");

                    b.ToTable("UserType");
                });
#pragma warning restore 612, 618
        }
    }
}
