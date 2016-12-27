using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using WebDev.Data.Base;
using WebDev.Models;

namespace WebDev.Data.SqlCe
{
    public partial class PersonDbContext : DbContext, IUnitOfWork
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetupPersonEntity(modelBuilder);
            SetupAddressEntity(modelBuilder);
        }

        private static void SetupPersonEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(p => p.Id);
            modelBuilder.Entity<Person>().Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Person>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<Person>().Property(p => p.LastName).IsRequired();
            modelBuilder.Entity<Person>().Property(p => p.HomeTown).IsRequired();

            modelBuilder.Entity<Person>().HasMany(p => p.Addresses).WithMany(c => c.Occupants);
        }

        private static void SetupAddressEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasKey(p => p.Id);
            modelBuilder.Entity<Address>().Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Address>().Property(p => p.Street).IsRequired();
            modelBuilder.Entity<Address>().Property(p => p.City).IsOptional();
            modelBuilder.Entity<Address>().Property(p => p.State).IsOptional();
            //modelBuilder.Entity<Address>().Property(p => p.ZipCode).IsOptional();
        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
