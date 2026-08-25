using Lost_Found.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lost_Found.Data.Configurations
{
    public class StandardniKorisnikConfiguration : IEntityTypeConfiguration<StandardniKorisnik>
    {
        public void Configure(EntityTypeBuilder<StandardniKorisnik> builder)
        {
            builder.HasMany(s => s.Oglasi)
                .WithOne(o => o.Kreator)
                .HasForeignKey(o => o.KreatorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Potrazivanja)
                .WithOne(p => p.Korisnik)
                .HasForeignKey(p => p.KorisnikId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
