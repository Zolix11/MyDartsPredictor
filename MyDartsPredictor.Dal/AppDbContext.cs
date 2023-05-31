using Microsoft.EntityFrameworkCore;
using MyDartsPredictor.Dal.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Games> Games { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Point> Points { get; set; }
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
            .WithMany()
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
            .HasForeignKey(t => t.FounderUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Games>()
            .HasOne(g => g.Tournament)
            .WithMany(t => t.Games)
            .HasForeignKey(g => g.TournamentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Result>()
            .HasOne(r => r.Game)
            .WithMany()
            .HasForeignKey(r => r.GameId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Result>().HasOne(r => r.Game)
            .WithMany()
            .HasForeignKey(r => r.GameId)
            .HasPrincipalKey(r => r.Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.Game)
            .WithMany()
            .HasForeignKey(p => p.GameId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Point>()
         .HasOne(p => p.User)
         .WithMany()
         .HasForeignKey(p => p.UserId)
         .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Point>()
            .HasOne(p => p.Game)
            .WithMany()
            .HasForeignKey(p => p.GameId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Point>()
            .HasOne(p => p.Prediction)
            .WithMany()
            .HasForeignKey(p => p.PredictionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Point>()
            .HasOne(p => p.Result)
            .WithMany()
            .HasForeignKey(p => p.ResultId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }


}