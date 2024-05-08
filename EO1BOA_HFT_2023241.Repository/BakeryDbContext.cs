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
        public virtual DbSet<Bread> Bread { get; set; }
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
            modelBuilder.Entity<Oven>(x=>x.HasOne(y=>y.Bread).WithMany(z=>z.Ovens).HasForeignKey(t=>t.BreadId).OnDelete(DeleteBehavior.Cascade));


            //DbSeed
            var bakeries = new List<Bakery>();

            bakeries.Add(new Bakery() {BakeryId =  1,Location="Kunszentmárton",Rating=4.2,Name="Szaszkó Pékség" });
            bakeries.Add(new Bakery() { BakeryId = 2, Location = "Budapest", Rating = 4.8, Name = "Müller Pékség" });
            bakeries.Add(new Bakery() { BakeryId = 3, Location = "Sopron", Rating = 4.4, Name = "Niki Pékség" });
            bakeries.Add(new Bakery() { BakeryId = 4, Location = "Siófok", Rating = 4.0, Name = "Champion Pékség" });

            var breads = new List<Bread>();

            breads.Add(new Bread() {BreadId =  1,Name="Csokis Muffin",IsDessert=true,Weight=100,BakeryId=1 });
            breads.Add(new Bread() { BreadId = 2, Name = "Vizes Zsemle", IsDessert = false, Weight = 55, BakeryId = 2 });
            breads.Add(new Bread() { BreadId = 3, Name = "Kovászos Kenyér", IsDessert = false, Weight = 750, BakeryId = 3 });
            breads.Add(new Bread() { BreadId = 4, Name = "Dobos Torta", IsDessert = true, Weight = 1920, BakeryId = 4 });
            breads.Add(new Bread() { BreadId = 5, Name = "Fehércsokis Stracsatella torta", IsDessert = true, Weight = 2400, BakeryId = 1 });
            breads.Add(new Bread() { BreadId = 6, Name = "Kenyér", IsDessert = false, Weight = 1000, BakeryId = 2 });
            breads.Add(new Bread() { BreadId = 7, Name = "Kókuszgolyó", IsDessert = true, Weight = 25, BakeryId = 3 });
            breads.Add(new Bread() { BreadId = 8, Name = "Kifli", IsDessert = true, Weight = 100, BakeryId = 4 });
            breads.Add(new Bread() { BreadId = 9, Name = "Epertorta", IsDessert = true, Weight = 1969, BakeryId = 1 });
            breads.Add(new Bread() { BreadId = 10, Name = "Kalács", IsDessert = true, Weight = 889, BakeryId = 2 });

            var ovens = new List<Oven>();

            ovens.Add(new Oven() {OvenId=1  ,BakingTime=20,     Price=1.6,   BreadCapacity=2,   BreadId = 1 });
            ovens.Add(new Oven() { OvenId = 2, BakingTime = 40, Price = 4.5, BreadCapacity = 14, BreadId = 2 });
            ovens.Add(new Oven() { OvenId = 3, BakingTime = 65, Price = 3.1, BreadCapacity = 2, BreadId = 3 });
            ovens.Add(new Oven() { OvenId = 4, BakingTime = 100, Price = 2.9, BreadCapacity = 21, BreadId = 4 });


            modelBuilder.Entity<Bakery>().HasData(bakeries);
            modelBuilder.Entity<Bread>().HasData(breads);
            modelBuilder.Entity<Oven>().HasData(ovens);


            base.OnModelCreating(modelBuilder);
        }
    }
}
