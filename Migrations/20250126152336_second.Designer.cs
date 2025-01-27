﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskList.Data;

#nullable disable

namespace TaskList.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250126152336_second")]
    partial class second
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskList.Models.Tag", b =>
                {
                    b.Property<int>("Tag_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Tag_Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Tag_Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("TaskList.Models.Task", b =>
                {
                    b.Property<int>("Task_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Task_Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Task_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskList.Models.TaskTag", b =>
                {
                    b.Property<int>("Task_Id")
                        .HasColumnType("int");

                    b.Property<int>("Tag_Id")
                        .HasColumnType("int");

                    b.HasKey("Task_Id", "Tag_Id");

                    b.HasIndex("Tag_Id");

                    b.ToTable("TaskTags");
                });

            modelBuilder.Entity("TaskList.Models.User", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("User_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskList.Models.Task", b =>
                {
                    b.HasOne("TaskList.Models.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskList.Models.TaskTag", b =>
                {
                    b.HasOne("TaskList.Models.Tag", "Tag")
                        .WithMany("TaskTags")
                        .HasForeignKey("Tag_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskList.Models.Task", "Task")
                        .WithMany("TaskTags")
                        .HasForeignKey("Task_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskList.Models.Tag", b =>
                {
                    b.Navigation("TaskTags");
                });

            modelBuilder.Entity("TaskList.Models.Task", b =>
                {
                    b.Navigation("TaskTags");
                });

            modelBuilder.Entity("TaskList.Models.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
