﻿using System;
using System.Collections.Generic;

namespace DatabaseAccess.Models
{
    public class Center
    {
        public Center()
        {
            Employees = new List<Employee>();
            BloodBags = new List<BloodBag>();
        }

        public int Id { get; set; }
        public String CenterName { get; set; }
        public double AvailableQuantity { get; set; }

        public List<Employee> Employees { get; set; }


        public CenterAdmin CenterAdmin { get; set; }

        public string CenterAdminId { get; set; }


        public List<BloodBag> BloodBags { get; set; }
    }
}
