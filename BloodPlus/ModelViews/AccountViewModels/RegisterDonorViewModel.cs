﻿using DatabaseAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodPlus.ModelViews.AccountViewModels
{
    public class RegisterDonorViewModel : RegisterViewModel
    {
        [Required]
        public string CNP { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //public string PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string County { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }


    }
}
