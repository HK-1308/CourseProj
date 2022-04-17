﻿// <auto-generated />
using System;
using CourseProj.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CourseProj.Migrations
{
    [DbContext(typeof(DBContent))]
    [Migration("20220413171128_TV13042022")]
    partial class TV13042022
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourseProj.Data.Models.Collection", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BooleanField1_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("BooleanField1_visible")
                        .HasColumnType("bit");

                    b.Property<string>("BooleanField2_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("BooleanField2_visible")
                        .HasColumnType("bit");

                    b.Property<string>("BooleanField3_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("BooleanField3_visible")
                        .HasColumnType("bit");

                    b.Property<string>("DateField1_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DateField1_visible")
                        .HasColumnType("bit");

                    b.Property<string>("DateField2_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DateField2_visible")
                        .HasColumnType("bit");

                    b.Property<string>("DateField3_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DateField3_visible")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumericField1_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NumericField1_visible")
                        .HasColumnType("bit");

                    b.Property<string>("NumericField2_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NumericField2_visible")
                        .HasColumnType("bit");

                    b.Property<string>("NumericField3_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NumericField3_visible")
                        .HasColumnType("bit");

                    b.Property<string>("StringField1_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("StringField1_visible")
                        .HasColumnType("bit");

                    b.Property<string>("StringField2_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("StringField2_visible")
                        .HasColumnType("bit");

                    b.Property<string>("StringField3_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("StringField3_visible")
                        .HasColumnType("bit");

                    b.Property<string>("TextField1_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TextField1_visible")
                        .HasColumnType("bit");

                    b.Property<string>("TextField2_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TextField2_visible")
                        .HasColumnType("bit");

                    b.Property<string>("TextField3_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TextField3_visible")
                        .HasColumnType("bit");

                    b.Property<string>("Theme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("userID");

                    b.ToTable("Collection");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ItemID");

                    b.HasIndex("UserID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("BooleanField1")
                        .HasColumnType("bit");

                    b.Property<bool>("BooleanField2")
                        .HasColumnType("bit");

                    b.Property<bool>("BooleanField3")
                        .HasColumnType("bit");

                    b.Property<int>("CollectionID")
                        .HasColumnType("int");

                    b.Property<string>("DateField1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateField2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateField3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumericField1")
                        .HasColumnType("int");

                    b.Property<int>("NumericField2")
                        .HasColumnType("int");

                    b.Property<int>("NumericField3")
                        .HasColumnType("int");

                    b.Property<string>("StringField1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StringField2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StringField3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextField1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextField2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextField3")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CollectionID");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Like", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ItemID");

                    b.HasIndex("UserID");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "admin"
                        },
                        new
                        {
                            ID = 2,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("CourseProj.Data.Models.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("CourseProj.Data.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("Unblocked")
                        .HasColumnType("bit");

                    b.Property<byte[]>("salt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ID");

                    b.HasIndex("RoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            ID = 69,
                            Email = "admin",
                            Password = "LdkOetqPnCn8mflQJGFwMA==",
                            RoleId = 1,
                            Unblocked = true,
                            salt = new byte[] { 64, 95, 46, 173, 165, 185, 179, 3 }
                        });
                });

            modelBuilder.Entity("ItemTag", b =>
                {
                    b.Property<int>("itemsID")
                        .HasColumnType("int");

                    b.Property<int>("tagsID")
                        .HasColumnType("int");

                    b.HasKey("itemsID", "tagsID");

                    b.HasIndex("tagsID");

                    b.ToTable("ItemTag");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Collection", b =>
                {
                    b.HasOne("CourseProj.Data.Models.User", "user")
                        .WithMany("Collections")
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Comment", b =>
                {
                    b.HasOne("CourseProj.Data.Models.Item", "Item")
                        .WithMany("comments")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CourseProj.Data.Models.User", "User")
                        .WithMany("comments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Item", b =>
                {
                    b.HasOne("CourseProj.Data.Models.Collection", "Collection")
                        .WithMany("Items")
                        .HasForeignKey("CollectionID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Like", b =>
                {
                    b.HasOne("CourseProj.Data.Models.Item", "Item")
                        .WithMany("likes")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CourseProj.Data.Models.User", "User")
                        .WithMany("likes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CourseProj.Data.Models.User", b =>
                {
                    b.HasOne("CourseProj.Data.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ItemTag", b =>
                {
                    b.HasOne("CourseProj.Data.Models.Item", null)
                        .WithMany()
                        .HasForeignKey("itemsID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CourseProj.Data.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("tagsID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseProj.Data.Models.Collection", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Item", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("likes");
                });

            modelBuilder.Entity("CourseProj.Data.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CourseProj.Data.Models.User", b =>
                {
                    b.Navigation("Collections");

                    b.Navigation("comments");

                    b.Navigation("likes");
                });
#pragma warning restore 612, 618
        }
    }
}
