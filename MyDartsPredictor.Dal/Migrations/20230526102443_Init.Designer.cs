﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MyDartsPredictor.Dal.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230526102443_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Games", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("MatchDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Player1Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Player2Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ResultId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResultId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int?>("GamesId")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("PredictionId")
                        .HasColumnType("int");

                    b.Property<int>("ResultId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("GamesId");

                    b.HasIndex("PredictionId");

                    b.HasIndex("ResultId");

                    b.HasIndex("UserId");

                    b.HasIndex("UsersId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Prediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int?>("GamesId")
                        .HasColumnType("int");

                    b.Property<int>("PredictionScore")
                        .HasColumnType("int");

                    b.Property<int>("PredictionWinner")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("GamesId");

                    b.HasIndex("UserId");

                    b.HasIndex("UsersId");

                    b.ToTable("Predictions");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Score")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WinnerPlayer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FoundationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FounderUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FounderUserId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AzureAdB2CId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.UsersInTournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EarnedPoints")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TournamentId");

                    b.HasIndex("UserId");

                    b.HasIndex("UsersId");

                    b.ToTable("UsersInTournaments");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Games", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.Result", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId");

                    b.HasOne("MyDartsPredictor.Dal.Entities.Tournament", "Tournament")
                        .WithMany("Games")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Result");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Point", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.Games", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyDartsPredictor.Dal.Entities.Games", null)
                        .WithMany("Points")
                        .HasForeignKey("GamesId");

                    b.HasOne("MyDartsPredictor.Dal.Entities.Prediction", "Prediction")
                        .WithMany()
                        .HasForeignKey("PredictionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyDartsPredictor.Dal.Entities.Result", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyDartsPredictor.Dal.Entities.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyDartsPredictor.Dal.Entities.Users", null)
                        .WithMany("Points")
                        .HasForeignKey("UsersId");

                    b.Navigation("Game");

                    b.Navigation("Prediction");

                    b.Navigation("Result");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Prediction", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.Games", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyDartsPredictor.Dal.Entities.Games", null)
                        .WithMany("Predictions")
                        .HasForeignKey("GamesId");

                    b.HasOne("MyDartsPredictor.Dal.Entities.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyDartsPredictor.Dal.Entities.Users", null)
                        .WithMany("Predictions")
                        .HasForeignKey("UsersId");

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Result", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.Games", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Tournament", b =>
                {
                    b.HasOne("MyDartsPredictor.Dal.Entities.Users", "FounderUser")
                        .WithMany()
                        .HasForeignKey("FounderUserId")
                        .OnDelete(DeleteBehavior.Restrict)
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

                    b.HasOne("MyDartsPredictor.Dal.Entities.Users", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyDartsPredictor.Dal.Entities.Users", null)
                        .WithMany("UsersInTournaments")
                        .HasForeignKey("UsersId");

                    b.Navigation("Tournament");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Games", b =>
                {
                    b.Navigation("Points");

                    b.Navigation("Predictions");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Tournament", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("UsersInTournament");
                });

            modelBuilder.Entity("MyDartsPredictor.Dal.Entities.Users", b =>
                {
                    b.Navigation("Points");

                    b.Navigation("Predictions");

                    b.Navigation("UsersInTournaments");
                });
#pragma warning restore 612, 618
        }
    }
}
