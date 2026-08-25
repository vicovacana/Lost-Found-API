using Lost_Found.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lost_Found.Data.Configurations
{
    public class PorukaConfiguration : IEntityTypeConfiguration<Poruka>
    {
        public void Configure(EntityTypeBuilder<Poruka> builder)
        {
            builder.ToTable("Poruka");

            builder.HasKey(p => p.PorukaId);
            builder.Property(p => p.PorukaId).HasColumnName("porukaID");

            builder.Property(p => p.KorisnikId).HasColumnName("korisnikID").IsRequired();
            builder.Property(p => p.RazgovorId).HasColumnName("razgovorID").IsRequired();

            builder.Property(p => p.DatumKreiranja)
                .HasColumnName("datumKreiranja")
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.Property(p => p.Sadrzaj)
                .HasColumnName("sadrzaj")
                .IsRequired()
                .HasMaxLength(4000);

            builder.HasIndex(p => new { p.RazgovorId, p.DatumKreiranja });

            builder.HasOne(p => p.Razgovor)
                .WithMany(r => r.Poruke)
                .HasForeignKey(p => p.RazgovorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
