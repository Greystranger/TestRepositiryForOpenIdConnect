﻿// <auto-generated />
using System;
using EFCoreTutorialsConsoleApp.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreTutorialsConsoleApp.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EFCoreTutorialsConsoleApp.DomainModels.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("EFCoreTutorialsConsoleApp.DomainModels.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName");

                    b.Property<double>("Height");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("Photo");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("EFCoreTutorialsConsoleApp.DomainModels.Entities.StudentAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("District");

                    b.Property<string>("Region");

                    b.Property<int>("StudentId");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("StudentAddress");
                });

            modelBuilder.Entity("EFCoreTutorialsConsoleApp.DomainModels.Entities.Student", b =>
                {
                    b.HasOne("EFCoreTutorialsConsoleApp.DomainModels.Entities.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EFCoreTutorialsConsoleApp.DomainModels.Entities.StudentAddress", b =>
                {
                    b.HasOne("EFCoreTutorialsConsoleApp.DomainModels.Entities.Student", "Student")
                        .WithOne("Address")
                        .HasForeignKey("EFCoreTutorialsConsoleApp.DomainModels.Entities.StudentAddress", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
