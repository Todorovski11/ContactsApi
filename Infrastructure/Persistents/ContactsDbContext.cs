using ContactsApi.Domain.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace ContactsApi.Infrastructure.Persistence
{
    public class ContactsDbContext : DbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Company)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CompanyId);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Country)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.CountryId);

            modelBuilder.Entity<User>()
               .HasIndex(u => u.Username) 
               .IsUnique();
        }
    }
}
