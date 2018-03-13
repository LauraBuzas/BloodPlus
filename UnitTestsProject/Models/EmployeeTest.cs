﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BloodPlus.Models;
namespace UnitTestsProject
{
    [TestClass]
    public class EmployeeTest
    {
        
        [TestMethod]
        public void SmokeTestEmployee()
        {
            Employee employee1 = new Employee();
            Assert.IsTrue(employee1.EmployeeId == 0);
            Assert.IsTrue(employee1.EmployeeName.Equals(""));
            Assert.IsTrue(employee1.AdressId == 0);
            Assert.IsTrue(employee1.CenterId == 0);


            employee1.EmployeeId = 12;
            employee1.EmployeeName = "Ionescu";
            employee1.AdressId = 10;
            employee1.CenterId = 15;

            Assert.IsTrue(employee1.EmployeeId == 12);
            Assert.IsTrue(employee1.EmployeeName.Equals("Ionescu"));
            Assert.IsTrue(employee1.AdressId == 10);
            Assert.IsTrue(employee1.CenterId == 15);

            Employee employee2 = new Employee(13, "Vasilescu", 14, 10);
            Assert.IsTrue(employee2.EmployeeId == 13);
            Assert.IsTrue(employee2.EmployeeName.Equals("Vasilescu"));
            Assert.IsTrue(employee2.AdressId == 14);
            Assert.IsTrue(employee2.CenterId == 10);

        }
    }
}