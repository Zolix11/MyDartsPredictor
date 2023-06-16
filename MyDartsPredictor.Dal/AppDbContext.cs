using Microsoft.EntityFrameworkCore;
using MyDartsPredictor.Dal.Entities;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<UsersInTournament> UsersInTournaments { get; set; }
    public DbSet<Prediction> Predictions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<UsersInTournament>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<UsersInTournament>()
            .HasOne(u => u.User)
            .WithMany(t => t.UsersInTournaments)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UsersInTournament>()
            .HasOne(u => u.Tournament)
            .WithMany(t => t.UsersInTournament)
            .HasForeignKey(u => u.TournamentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tournament>()
            .HasOne(t => t.FounderUser)
            .WithMany()
            .HasForeignKey(t => t.FounderUserId);

        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.UsersInTournament)
            .WithOne(u => u.Tournament)
            .HasForeignKey(u => u.TournamentId);

        modelBuilder.Entity<Tournament>()
            .HasMany(t => t.Games)
            .WithOne(g => g.Tournament)
            .HasForeignKey(g => g.TournamentId);


        modelBuilder.Entity<Game>()
            .HasOne(g => g.Tournament)
            .WithMany(t => t.Games)
            .HasForeignKey(g => g.TournamentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Game>()
            .HasOne(g => g.Result)
            .WithOne(r => r.Game)
            .HasForeignKey<Result>(r => r.GameId);


        modelBuilder.Entity<Game>()
            .HasMany(g => g.Predictions)
            .WithOne(p => p.Game)
            .HasForeignKey(p => p.GameId);

        modelBuilder.Entity<Result>()
            .HasOne(r => r.Game)
            .WithOne(g => g.Result)
            .HasForeignKey<Result>(r => r.GameId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.User)
            .WithMany(u => u.Predictions)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.Game)
            .WithMany(g => g.Predictions)
            .HasForeignKey(p => p.GameId);


        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();

    }

}