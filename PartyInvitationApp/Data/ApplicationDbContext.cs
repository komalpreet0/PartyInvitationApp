using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Models;

namespace PartyInvitationApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Party> Parties { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Convert enum values to string in DB
            modelBuilder.Entity<Invitation>()
                .Property(inv => inv.Status)
                .HasConversion<string>();

            // Define relationships
            modelBuilder.Entity<Party>()
                .HasMany(p => p.Invitations)
                .WithOne(i => i.Party)
                .HasForeignKey(i => i.PartyId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
