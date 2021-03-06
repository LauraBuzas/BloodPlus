﻿using System;
using System.Collections.Generic;
using System.Text;
using DatabaseAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestsProject.Models
{
    [TestClass]
    public class AddressTest {
        [TestMethod]
        public void SmokeTestAddress() {
            Address address = new Address() {
                Id = 1,
                City = "cluj",
                County = "cluj",
                Street = "kogalniceanu",
                Number = 1,
                ApartmentNumber = 10,
                Unit = "2",
                Floor = 3 }
            ;
            Assert.IsTrue(address.Id.Equals(1));
            Assert.IsTrue(address.City.Equals("cluj"));
            Assert.IsTrue(address.County.Equals("cluj"));
            Assert.IsTrue(address.Street.Equals("kogalniceanu"));
            Assert.IsTrue(address.Number.Equals(1));
            Assert.IsTrue(address.ApartmentNumber.Equals(10));
            Assert.IsTrue(address.Unit.Equals("2"));
            Assert.IsTrue(address.Floor.Equals(3));

            address.Id = 2;
            address.City = "Bucuresti";
            address.County = "Bucuresti";
            address.Street = "Street";
            address.Number = 420;
            address.ApartmentNumber = 20;
            address.Unit = "A";
            address.Floor = 4;

            Assert.IsTrue(address.Id.Equals(2));
            Assert.IsTrue(address.City.Equals("Bucuresti"));
            Assert.IsTrue(address.County.Equals("Bucuresti"));
            Assert.IsTrue(address.Street.Equals("Street"));
            Assert.IsTrue(address.Number.Equals(420));
            Assert.IsTrue(address.ApartmentNumber.Equals(20));
            Assert.IsTrue(address.Unit.Equals("A"));
            Assert.IsTrue(address.Floor.Equals(4));

        }
       
    }
}
