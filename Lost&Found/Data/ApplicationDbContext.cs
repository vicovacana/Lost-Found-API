using Lost_Found.Models;
using Microsoft.EntityFrameworkCore;

namespace Lost_Found.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Korisnik> Korisnici => Set<Korisnik>();
        public DbSet<StandardniKorisnik> StandardniKorisnici => Set<StandardniKorisnik>();
        public DbSet<Admin> Admini => Set<Admin>();
        public DbSet<Oglas> Oglasi => Set<Oglas>();
        public DbSet<Potrazivanje> Potrazivanja => Set<Potrazivanje>();
        public DbSet<Razgovor> Razgovori => Set<Razgovor>();
        public DbSet<Poruka> Poruke => Set<Poruka>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
