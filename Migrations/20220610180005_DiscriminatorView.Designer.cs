﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackOverflowEntities.Entities;

#nullable disable

namespace StackOverflowEntities.Migrations
{
    [DbContext(typeof(StackOverflowContext))]
    [Migration("20220610180005_DiscriminatorView")]
    partial class DiscriminatorView
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QuestionTag", b =>
                {
                    b.Property<Guid>("QuestionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("QuestionsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("QuestionTag");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.DiscriminatorView", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToView("View_Discriminator");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.QuestionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastEdited")
                        .HasColumnType("datetime2");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("QuestionsRepliesComments");

                    b.HasDiscriminator<string>("Discriminator").HasValue("QuestionModel");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "C#"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Javascript"
                        },
                        new
                        {
                            Id = 3,
                            Name = "DependencyInjection"
                        },
                        new
                        {
                            Id = 4,
                            Name = ".Net"
                        },
                        new
                        {
                            Id = 5,
                            Name = ".NetCore"
                        },
                        new
                        {
                            Id = 6,
                            Name = ".Asp.NetCore"
                        },
                        new
                        {
                            Id = 7,
                            Name = "WebAPI"
                        },
                        new
                        {
                            Id = 8,
                            Name = "EntityFramework"
                        },
                        new
                        {
                            Id = 9,
                            Name = "SQL"
                        });
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.Comment", b =>
                {
                    b.HasBaseType("StackOverflowEntities.Entities.QuestionModel");

                    b.Property<Guid?>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ReplyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("AuthorId");

                    b.HasIndex("QuestionId");

                    b.HasDiscriminator().HasValue("Comment");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.Question", b =>
                {
                    b.HasBaseType("StackOverflowEntities.Entities.QuestionModel");

                    b.HasIndex("AuthorId");

                    b.HasDiscriminator().HasValue("Question");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.Reply", b =>
                {
                    b.HasBaseType("StackOverflowEntities.Entities.QuestionModel");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Reply_QuestionId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("QuestionId");

                    b.HasDiscriminator().HasValue("Reply");
                });

            modelBuilder.Entity("QuestionTag", b =>
                {
                    b.HasOne("StackOverflowEntities.Entities.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StackOverflowEntities.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.Comment", b =>
                {
                    b.HasOne("StackOverflowEntities.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StackOverflowEntities.Entities.Reply", "Reply")
                        .WithMany("Comments")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("StackOverflowEntities.Entities.Question", "Question")
                        .WithMany("Comments")
                        .HasForeignKey("QuestionId");

                    b.Navigation("Author");

                    b.Navigation("Question");

                    b.Navigation("Reply");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.Question", b =>
                {
                    b.HasOne("StackOverflowEntities.Entities.User", "Author")
                        .WithMany("Questions")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.Reply", b =>
                {
                    b.HasOne("StackOverflowEntities.Entities.User", "Author")
                        .WithMany("Replies")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StackOverflowEntities.Entities.Question", "Question")
                        .WithMany("Replies")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Questions");

                    b.Navigation("Replies");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.Question", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Replies");
                });

            modelBuilder.Entity("StackOverflowEntities.Entities.Reply", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
