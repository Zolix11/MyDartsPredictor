﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyDartsPredictor.Dal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Player1Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Player2Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ResultId")
                        .HasColumnType("integer");

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Prediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("PredictionScore")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PredictionWinner")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Predictions");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Score")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WinnerPlayer")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.ToTable("Results");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FoundationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FounderUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FounderUserId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthUID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.UsersInTournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EarnedPoints")
                        .HasColumnType("integer");

                    b.Property<int>("TournamentId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersInTournaments");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Game", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.Tournament", "Tournament")
                        .WithMany("Games")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Prediction", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.Game", "Game")
                        .WithMany("Predictions")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyDartsPredictor.Dal.Entities.User", "User")
                        .WithMany("Predictions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Result", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.Game", "Game")
                        .WithOne("Result")
                        .HasForeignKey("MyDartsPredictor.Dal.Entities.Result", "GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Tournament", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.User", "FounderUser")
                        .WithMany()
                        .HasForeignKey("FounderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FounderUser");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.UsersInTournament", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.Tournament", "Tournament")
                        .WithMany("UsersInTournament")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyDartsPredictor.Dal.Entities.User", "User")
                        .WithMany("UsersInTournaments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Tournament");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Game", b =>
                {
                    b.Navigation("Predictions");

                    b.Navigation("Result");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Tournament", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("UsersInTournament");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.User", b =>
                {
                    b.Navigation("Predictions");

                    b.Navigation("UsersInTournaments");
                });
#pragma warning restore 612, 618
        }
    }
}
