﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TurkishTalk.Persistance;

#nullable disable

namespace TurkishTalk.Persistance.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240531174159_cn")]
    partial class cn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TurkishTalk.Persistance.Models.AlfabetTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("Tests")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("AlfabetTask");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.GrammarTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RadioTests")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rule")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("Tests")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("GrammarTask");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ProgresGrammar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GrammarTaskId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("scope")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GrammarTaskId");

                    b.HasIndex("UserId");

                    b.ToTable("ProgresGrammar");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ProgresRead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ReadTaskId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("scope")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReadTaskId");

                    b.HasIndex("UserId");

                    b.ToTable("ProgresRead");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ProgresWrite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WriteTaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WriteTaskId");

                    b.ToTable("ProgresWrite");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ProgressAlfabet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlfabetTaskId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("scope")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlfabetTaskId");

                    b.HasIndex("UserId");

                    b.ToTable("ProgressAlfabet");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ReadTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("Tests")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TextReadingExample")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("VoiceExample")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("VoiceExampleMimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("ReadTask");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeshedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.WordDictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AlfabetTaskId")
                        .HasColumnType("int");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("WriteTaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlfabetTaskId");

                    b.HasIndex("WriteTaskId");

                    b.ToTable("WordDictionary");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.WriteTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FixString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FixStringCorrect")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rule")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("Tests")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("WriteTask");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.AlfabetTask", b =>
                {
                    b.HasOne("TurkishTalk.Persistance.Models.Section", "Section")
                        .WithMany("AlfabetTasks")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.GrammarTask", b =>
                {
                    b.HasOne("TurkishTalk.Persistance.Models.Section", "Section")
                        .WithMany("GrammarTasks")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ProgresGrammar", b =>
                {
                    b.HasOne("TurkishTalk.Persistance.Models.GrammarTask", "GrammarTask")
                        .WithMany("ProgresGrammars")
                        .HasForeignKey("GrammarTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TurkishTalk.Persistance.Models.User", "User")
                        .WithMany("ProgresGrammar")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GrammarTask");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ProgresRead", b =>
                {
                    b.HasOne("TurkishTalk.Persistance.Models.ReadTask", "ReadTask")
                        .WithMany("ProgresRead")
                        .HasForeignKey("ReadTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TurkishTalk.Persistance.Models.User", "User")
                        .WithMany("ProgresRead")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReadTask");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ProgresWrite", b =>
                {
                    b.HasOne("TurkishTalk.Persistance.Models.User", "User")
                        .WithMany("ProgresWrite")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TurkishTalk.Persistance.Models.WriteTask", "WriteTask")
                        .WithMany("ProgresWrite")
                        .HasForeignKey("WriteTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WriteTask");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ProgressAlfabet", b =>
                {
                    b.HasOne("TurkishTalk.Persistance.Models.AlfabetTask", "AlfabetTask")
                        .WithMany("ProgressAlfabet")
                        .HasForeignKey("AlfabetTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TurkishTalk.Persistance.Models.User", "User")
                        .WithMany("ProgressAlfabet")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AlfabetTask");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ReadTask", b =>
                {
                    b.HasOne("TurkishTalk.Persistance.Models.Section", "Section")
                        .WithMany("ReadTasks")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.WordDictionary", b =>
                {
                    b.HasOne("TurkishTalk.Persistance.Models.AlfabetTask", "AlfabetTask")
                        .WithMany("WordDictionary")
                        .HasForeignKey("AlfabetTaskId");

                    b.HasOne("TurkishTalk.Persistance.Models.WriteTask", "WriteTask")
                        .WithMany("WordDictionary")
                        .HasForeignKey("WriteTaskId");

                    b.Navigation("AlfabetTask");

                    b.Navigation("WriteTask");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.WriteTask", b =>
                {
                    b.HasOne("TurkishTalk.Persistance.Models.Section", "Section")
                        .WithMany("WriteTasks")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.AlfabetTask", b =>
                {
                    b.Navigation("ProgressAlfabet");

                    b.Navigation("WordDictionary");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.GrammarTask", b =>
                {
                    b.Navigation("ProgresGrammars");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.ReadTask", b =>
                {
                    b.Navigation("ProgresRead");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.Section", b =>
                {
                    b.Navigation("AlfabetTasks");

                    b.Navigation("GrammarTasks");

                    b.Navigation("ReadTasks");

                    b.Navigation("WriteTasks");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.User", b =>
                {
                    b.Navigation("ProgresGrammar");

                    b.Navigation("ProgresRead");

                    b.Navigation("ProgresWrite");

                    b.Navigation("ProgressAlfabet");
                });

            modelBuilder.Entity("TurkishTalk.Persistance.Models.WriteTask", b =>
                {
                    b.Navigation("ProgresWrite");

                    b.Navigation("WordDictionary");
                });
#pragma warning restore 612, 618
        }
    }
}
