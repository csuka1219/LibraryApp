﻿// <auto-generated />
using System;
using LibraryWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryWebApi.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Library.Book", b =>
                {
                    b.Property<int>("InvNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvNumber"), 1L, 1);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LoanId")
                        .HasColumnType("int");

                    b.Property<string>("Publisher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("InvNumber");

                    b.HasIndex("LoanId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.LibraryMember", b =>
                {
                    b.Property<int>("ReaderNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReaderNumber"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LoanId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReaderNumber");

                    b.HasIndex("LoanId");

                    b.ToTable("LibraryMembers");
                });

            modelBuilder.Entity("Library.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("InvNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReaderNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("returnDeadline")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Library.Book", b =>
                {
                    b.HasOne("Library.Loan", null)
                        .WithMany("Books")
                        .HasForeignKey("LoanId");
                });

            modelBuilder.Entity("Library.LibraryMember", b =>
                {
                    b.HasOne("Library.Loan", null)
                        .WithMany("LibraryMembers")
                        .HasForeignKey("LoanId");
                });

            modelBuilder.Entity("Library.Loan", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("LibraryMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
