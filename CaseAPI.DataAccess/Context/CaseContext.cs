using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CaseAPI.Domain.Models;

namespace CaseAPI.DataAccess.Context
{
    public class CaseContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public CaseContext(DbContextOptions<CaseContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(x =>
            {
                x.Property(y => y.Id).HasDefaultValueSql("NEWID()");
                x.HasIndex(y => y.Name).IsUnique();
                x.HasOne(y => y.Contact)
                .WithMany(z => z.Accounts)
                .HasForeignKey(y => y.ContactId).IsRequired();

            });

            modelBuilder.Entity<Incident>(x =>
            {
                x.HasKey(y => y.Name);
                x.Property(y => y.Name).HasDefaultValueSql("NEWID()");
                x.HasIndex(y => y.Name).IsUnique();
                x.HasOne(y => y.Account)
                .WithMany(z => z.Incidents)
                .HasForeignKey(y => y.AccountId).IsRequired();
            });

            modelBuilder.Entity<Contact>(x =>
            {
                x.Property(y => y.Id).HasDefaultValueSql("NEWID()");
                x.HasIndex(y => y.Email).IsUnique();
            });
        }
    }
}
