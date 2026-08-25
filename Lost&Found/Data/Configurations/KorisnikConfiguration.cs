using Lost_Found.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lost_Found.Data.Configurations
{
    public class KorisnikConfiguration : IEntityTypeConfiguration<Korisnik>
    {
        public void Configure(EntityTypeBuilder<Korisnik> builder)
        {
            builder.ToTable("Korisnik", t =>
                t.HasCheckConstraint("CK_Korisnik_TipKorisnika", "\"tipKorisnika\" IN (0, 1)"));

            builder.HasKey(k => k.KorisnikId);
            builder.Property(k => k.KorisnikId).HasColumnName("korisnikID");

            builder.Property(k => k.KorisnickoIme)
                .HasColumnName("korisnickoIme")
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(k => k.KorisnickoIme).IsUnique();

            builder.Property(k => k.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(256);
            builder.HasIndex(k => k.Email).IsUnique();

            builder.Property(k => k.LozinkaHash)
                .HasColumnName("lozinka")
                .IsRequired();

            builder.Property(k => k.VremeKreiranja)
                .HasColumnName("vremeKreiranja")
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.HasMany(k => k.Poruke)
                .WithOne(p => p.Korisnik)
                .HasForeignKey(p => p.KorisnikId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property<int>("TipKorisnika").HasColumnName("tipKorisnika");
            builder.HasDiscriminator<int>("TipKorisnika")
                .HasValue<StandardniKorisnik>(0)
                .HasValue<Admin>(1);
        }
    }
}
