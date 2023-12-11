using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO1BOA_HFT_2023241.Models;

namespace EO1BOA_HFT_2023241.Repository
{
    public class BakeryDbContext : DbContext
    {
        public virtual DbSet<Bread> Bready { get; set; }
        public virtual DbSet<Bakery> Bakeries { get; set; }
        public virtual DbSet<Oven> Ovens { get; set; }

        public BakeryDbContext() {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("Bakery");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bread>(x => x.HasOne(y => y.Bakery).WithMany(z => z.Breads).HasForeignKey(t => t.BakeryId).OnDelete(DeleteBehavior.Cascade));
            modelBuilder.Entity<Oven>(x=>x.HasOne(y=>y.Bread).WithMany(z=>z.Ovens).HasForeignKey(t=>t.OvenId).OnDelete(DeleteBehavior.Cascade));
        }
    }
}
