﻿// <auto-generated />
using Drozdzynski_Debowska.Telescopes.DAOSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Drozdzynski_Debowska.Telescopes.DAOSQL.Migrations
{
    [DbContext(typeof(DAOSQL))]
    [Migration("20241208144115_somechange")]
    partial class somechange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("DAOSQL.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("DAOSQL.Telescope", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Aperture")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FocalLength")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OpticalSystem")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProducerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Telescopes");
                });

            modelBuilder.Entity("DAOSQL.Telescope", b =>
                {
                    b.HasOne("DAOSQL.Producer", "Producer")
                        .WithMany("Telescopes")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("DAOSQL.Producer", b =>
                {
                    b.Navigation("Telescopes");
                });
#pragma warning restore 612, 618
        }
    }
}
