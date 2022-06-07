
using Microsoft.EntityFrameworkCore;
using ClinicaMD.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ClinicaMD.Web.Data.Entities;

namespace ClinicaMD.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ProcedureType> ProcedureTypes { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Procedure> Procedures { get; set; }

        public DbSet<ClinicHistory> ClinicHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProcedureType>()
                .HasIndex(x => x.Description)
                .IsUnique();
            modelBuilder.Entity<Doctor>()
                .HasIndex(x => x.RegMed)
                .IsUnique();
            modelBuilder.Entity<Patient>()
                .HasIndex(x => x.Document)
                .IsUnique();
            modelBuilder.Entity<Procedure>()
                .HasIndex(x => x.Id)
                .IsUnique();
            modelBuilder.Entity<ClinicHistory>()
               .HasIndex(x => x.Id)
               .IsUnique();
        }
        public static implicit operator ControllerContext(ApplicationDbContext v)
        {
            throw new NotImplementedException();
        }
    }
}

