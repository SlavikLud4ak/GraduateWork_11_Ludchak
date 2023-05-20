﻿using GraduateWork_11_Ludchak.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace GraduateWork_11_Ludchak.Data
{
    internal class VacationDBContext : DbContext
    {
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<LogUser>? LogUsers { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                "https://yaroslavl-db.documents.azure.com:443/",
                "hTNHPcGMtJ4jgnCJs8vjciuRkHW04mB0E2wWEvgImP5mv7NCW337XkO1q4G3mtLVE6xyycdxVLTFACDbERbWBQ==",
                "vc-db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configuring Employees
            modelBuilder.Entity<Employee>()
                    .ToContainer("Employees") // ToContainer
                    .HasPartitionKey(e => e.Id); // Partition Key

            // configuring LogUsers
            modelBuilder.Entity<LogUser>()
                .ToContainer("LogUsers") // ToContainer
                .HasPartitionKey(c => c.Id); // Partition Key

            modelBuilder.Entity<Employee>().OwnsMany(p => p.VacationPlans);
        }
    }
}
