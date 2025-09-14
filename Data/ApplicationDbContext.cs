using LagerverwaltungApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LagerverwaltungApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kategorie> Kategorien { get; set; } = null!;

        public DbSet<LagerabgangEintrag> Lagerabgaenge { get; set; }

        public DbSet<Produkt> Artikel { get; set; } = default!;
        public DbSet<Bestellung> Bestellungen { get; set; } = null!;
        public DbSet<Warenausgang> Warenausgaenge { get; set; }
        public DbSet<Benutzer> Benutzer { get; set; }
        public DbSet<Bestellposition> Bestellpositionen { get; set; }
        public string? Kommentar { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bestellposition>()
                .HasOne(bp => bp.Bestellung)
                .WithMany(b => b.Positionen)
                .HasForeignKey(bp => bp.BestellungId)
                .OnDelete(DeleteBehavior.Restrict); // ❗️Wichtig: NICHT Cascade

            modelBuilder.Entity<Bestellposition>()
                .HasOne(bp => bp.Artikel)
                .WithMany()
                .HasForeignKey(bp => bp.ArtikelId)
                .OnDelete(DeleteBehavior.Restrict); // ❗️Wichtig: Auch hier

            modelBuilder.Entity<Bestellung>()
                .HasOne(b => b.Benutzer)
                .WithMany()
                .HasForeignKey(b => b.BenutzerId)
                .OnDelete(DeleteBehavior.SetNull); // Optional
        }






    }
}
