using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure {
    public class StamDataContext : DbContext {
        public StamDataContext(DbContextOptions<StamDataContext> contextOptions) : base(contextOptions) {
        }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Proceeding> Proceedings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
    }
}
