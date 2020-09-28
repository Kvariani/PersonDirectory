using Microsoft.EntityFrameworkCore;
using PersonDirectory.Core.Entities;

namespace PersonDirectory.Infrastructure.DBContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<RelatedPersonToPerson> RelatedPersonToPerson { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasIndex(i => new { i.Firstname, i.Lastname, i.IDNumber });

            modelBuilder.Entity<RelatedPersonToPerson>()
                .HasKey(bc => new { bc.PersonId, bc.RelatedPersonId });

            modelBuilder.Entity<RelatedPersonToPerson>()
                .HasOne(bc => bc.Person)
                .WithMany(b => b.RelatedPersons)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasForeignKey(bc => bc.PersonId);

            modelBuilder.Entity<RelatedPersonToPerson>()
                .HasOne(bc => bc.RelatedPerson)
                .WithMany(c => c.RelatedOn)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasForeignKey(bc => bc.RelatedPersonId);

            modelBuilder.Entity<TelNumber>()
                .HasOne(x => x.Person)
                .WithMany(x => x.TelNumbers)
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}