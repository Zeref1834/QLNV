﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLNV.Data.EF;

#nullable disable

namespace QLNV.Data.Migrations
{
    [DbContext(typeof(QLNV_DbContext))]
    [Migration("20240227040933_QLNV")]
    partial class QLNV
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QLNV.Data.Model.Nhanvien", b =>
                {
                    b.Property<string>("MANV")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NGAYSINH")
                        .HasColumnType("datetime2");

                    b.Property<string>("TENNV")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MANV");

                    b.ToTable("Nhanviens");
                });
#pragma warning restore 612, 618
        }
    }
}
