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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        RelationshipsSetup(modelBuilder);
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(new[]
            {
                new User {Id = 1,Name = "Zolix", AuthUID = "EJEgB8nlGpNt9DMN8ig5Gq6ETzA2" },
                new User {Id=2, Name = "Pista", AuthUID = "CNH4Se4MIbXI3CwJhUEldSpnVoK2" },
            });
        modelBuilder.Entity<Tournament>()
            .HasData(new[]
            {
                new Tournament
                {
                    Id = new TournamentId(1), FounderUserId = 1, FoundationTime = DateTime.Now.ToUniversalTime(),
                    Name = "Szupi Tournament"
                }
            });
    }
    private void RelationshipsSetup(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasIndex(p => p.AuthUID);

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

        modelBuilder.Entity<Tournament>()
            .Property(p => p.Id)
            .HasConversion(stronglyId => stronglyId.Value, intID => new TournamentId(intID))
            .ValueGeneratedOnAdd();


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
            .Property(e => e.MatchDate)
            .HasConversion(new DateWithTimeZoneConverter());

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
    }

}