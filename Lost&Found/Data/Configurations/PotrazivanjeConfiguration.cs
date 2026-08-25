using Lost_Found.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lost_Found.Data.Configurations
{
    public class PotrazivanjeConfiguration : IEntityTypeConfiguration<Potrazivanje>
    {
        public void Configure(EntityTypeBuilder<Potrazivanje> builder)
        {
            builder.ToTable("Potrazivanje", t =>
                t.HasCheckConstraint("CK_Potrazivanje_Status", "\"status\" IN (0, 1, 2)"));

            builder.HasKey(p => new { p.KorisnikId, p.OglasId });

            builder.Property(p => p.KorisnikId).HasColumnName("korisnikID");
            builder.Property(p => p.OglasId).HasColumnName("oglasID");

            builder.Property(p => p.DatumKreiranja)
                .HasColumnName("datumKreiranja")
                .IsRequired()
                .HasDefaultValueSql("now()");

            builder.Property(p => p.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.DatumRazresavanja).HasColumnName("datumRazresavanja");

            builder.HasIndex(p => p.OglasId);

            builder.HasOne(p => p.Oglas)
                .WithMany(o => o.Potrazivanja)
                .HasForeignKey(p => p.OglasId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
