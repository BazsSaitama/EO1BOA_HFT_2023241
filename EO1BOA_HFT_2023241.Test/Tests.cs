﻿using EO1BOA_HFT_2023241.Logic;
using EO1BOA_HFT_2023241.Models;
using EO1BOA_HFT_2023241.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EO1BOA_HFT_2023241.Test
{   
    [TestFixture]
    class Tests
    {
        BreadLogic breadLogic;
        OvenLogic ovenLogic;
        BakeryLogic bakeryLogic;
        Mock<IRepository<Bread>> mockBreadRepository;
        Mock<IRepository<Bakery>> mockBakeryRepository;
        Mock<IRepository<Oven>> mockOvenRepository;
        Bread bread;
        Oven oven;
        Bakery bakery;

        [SetUp]
        public void Setup() 
        {
            mockBakeryRepository = new Mock<IRepository<Bakery>>();
            mockBreadRepository = new Mock<IRepository<Bread>>();
            mockOvenRepository = new Mock<IRepository<Oven>>();

            bakery = new Bakery();
            bakery.Name = "Lipóti Pékség";
            bakery.Location = "Budapest";
            bakery.Rating = 4.5;
            bakery.Breads = new List<Bread>();

            bread = new Bread();
            bread.Name = "Túrós batyu";
            bread.IsDessert = true;
            bread.Ovens = new List<Oven>();
            bread.Weight = 200;
            bread.BakeryId = 2;

            oven = new Oven();
            oven.BreadCapacity = 4;
            oven.BakingTime = 2;
            oven.Price = 2.2;
            oven.BreadId = 2;

            oven.Bread = bread;
            bread.Ovens.Add(oven);
            bakery.Breads.Add(bread);

            Bread breead = new Bread();
            breead.Name = "Kovászos kifli";
            breead.IsDessert = false;
            breead.Ovens = new List<Oven>();
            breead.Weight = 2;
            breead.BakeryId = 4;

            Bakery bakeery = new Bakery();
            bakeery.Name = "Pici Pékség";
            bakeery.Location = "Százhalombatta";
            bakeery.Rating = 3.9;
            bakeery.Breads = new List<Bread>();

            Oven oveen = new Oven();
            oven.BreadCapacity = 1;
            oven.BakingTime = 2;
            oven.Price = 5;
            oven.BreadId = 1;

            oveen.Bread = breead;
            breead.Ovens.Add(oveen);
            bakeery.Breads.Add(breead);

            List<Bread> breads = new List<Bread> { bread, breead };
            List<Bakery> bakeries = new List<Bakery> { bakery, bakeery };
            List<Oven> ovens = new List<Oven> { oven, oveen };

            mockBreadRepository.Setup(w => w.ReadAll()).Returns(breads.AsQueryable);
            mockBakeryRepository.Setup(w => w.ReadAll()).Returns(bakeries.AsQueryable);
            mockOvenRepository.Setup(b => b.ReadAll()).Returns(ovens.AsQueryable);

            breadLogic = new BreadLogic(mockBreadRepository.Object);
            bakeryLogic = new BakeryLogic(mockBakeryRepository.Object);
            ovenLogic = new OvenLogic(mockOvenRepository.Object);
        }
        //3 Create tests
        [Test]
        public void BakeryCreateTest()
        {
            Bakery testbakery = new Bakery() { BakeryId = 1, Name = "Culi Pékség", Location = "Kistarcsa" };
            bakeryLogic.Create(testbakery);
            mockBakeryRepository.Verify(w => w.Create(testbakery), Times.Once);
            Assert.IsNotNull(testbakery);
        }
        [Test]
        public void BreadCreateTest()
        {
            Bread testbread = new Bread() { BreadId = 1, Weight = 250, Name = "Kakaós Csiga", IsDessert = true};
            breadLogic.Create(testbread);
            mockBreadRepository.Verify(w => w.Create(testbread), Times.Once);
            Assert.IsNotNull(testbread);
        }

        [Test]
        public void OvenCreateTest()
        {
            Oven testoven = new Oven() { Price = 2, BakingTime = 1, BreadCapacity = 1};
            ovenLogic.Create(testoven);
            mockOvenRepository.Verify(b => b.Create(testoven), Times.Once);
            Assert.IsNotNull(testoven);
        }
        //7 Tests Non Cruds:
        //public IQueryable<Oven> OvensByCapacity(int capacity);
        //public IQueryable<Bread> AllBreadsFromBakery(string bakery);
        //public Oven MostExpensiveOvenInBakery(string bakery);
        //public IQueryable<Bread> AllSweetsFromBakery(string bakery);
        //public Bread LightestBread(string bakery);

        [Test]
        public void ListOvensByCapacityTest()
        {
            int capacityTest = 2;
            var result = bakeryLogic.OvensByCapacity(capacityTest);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(x => x.BreadCapacity == capacityTest));
        }
        [Test]
        public void ListOvensByCapacity_EmptyListTest()
        {
            int nonExistingVintage = 13;

            var result = bakeryLogic.OvensByCapacity(nonExistingVintage);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }


        [Test]
        public void ListBreadsByBakeryTest()
        {
            string peksegNevTest = "Niki Pékség"; //Bugfixed in Dbset
            var result = bakeryLogic.AllBreadsFromBakery(peksegNevTest);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.All(x => x.Name == peksegNevTest));
        }

        [Test]
        public void ListBreadsByBakery2_NoBakeryLikeThis_ReturnsFalseTest() 
        {
            string peksegNevTest = "asd Pékség";
            var result = bakeryLogic.AllBreadsFromBakery(peksegNevTest);

            Assert.IsEmpty(result);
           
        }
        [Test]
        public void MostExpensiveOvenTest() 
        {
            var result = bakeryLogic.MostExpensiveOvenInBakery();
            Assert.AreEqual(result.Price, 5);
        }
        [Test]
        public void LightestBreadTest()
        {
            var result = bakeryLogic.LightestBread();
            Assert.AreEqual(result.Weight, 2);
        }
        [Test]
        public void AllSweetsFromBakeryTest()
        {
            string bakerytest = "Niki péség";
            var result = bakeryLogic.AllSweetsFromBakery(bakerytest);
            Assert.NotNull(result);
            Assert.IsTrue(result.All(x => x.IsDessert == true));
        }

    }
}
