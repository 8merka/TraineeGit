using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TraineeWEB.Models;

namespace TraineeWEB.Data
{
    public class EFFilmoSearchDbContext : DbContext
    {
        public EFFilmoSearchDbContext(DbContextOptions<EFFilmoSearchDbContext> options) : base(options)
        { }
        public DbSet<Film> Films{ get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ActorRole> ActorRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorRole>()
                .HasKey(actorRole => new { actorRole.ActorId, actorRole.FilmId });

            modelBuilder.Entity<ActorRole>()
                .HasOne(actorRole => actorRole.Actor)
                .WithMany(actor => actor.ActorRoles)
                .HasForeignKey(actorRole => actorRole.ActorId);

            modelBuilder.Entity<ActorRole>()
                .HasOne(actorRole => actorRole.Film)
                .WithMany(film => film.ActorRoles)
                .HasForeignKey(actorRole => actorRole.FilmId);
        }

    }
}
