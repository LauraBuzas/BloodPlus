﻿using BloodPlus.Services;
using DatabaseAccess.Models;
using DatabaseAccess.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class DonorService
    {

        public Donor AddDonor(Donor donor, Address address)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                uow.AddressRepository.Add(address);
                uow.Save();

                donor.AddressId = address.Id;
                uow.DonorRepository.Add(donor);
                uow.Save();
                return donor;
            }
        }

        public List<MedicalAnalysis> GetMedicalAnalyses(string id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                return uow.DonorRepository.GetAll().Include(d => d.MedicalAnalysis)
                    .Where(d => d.Id == id)
                    .FirstOrDefault().MedicalAnalysis.Where(ma=>ma.IsFilled==true).ToList();
            }
        }
        

        public async Task AddRegistrationForDonation(DonorRegistrationForDonation donorRegistrationForDonation, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                donorRegistrationForDonation.RegistrationDate = DateTime.Now;
                //var donorId = uow.DonorRepository.GetAll().Where(d => d.CNP == donorRegistrationForDonation.CNP).First().Id;
                try
                {
                    donorRegistrationForDonation.DonorId = uow.DonorRepository.GetByFunc(donor => donor.CNP == donorRegistrationForDonation.CNP).Id;
                    donorRegistrationForDonation.Donor = uow.DonorRepository.GetByFunc(donor => donor.CNP == donorRegistrationForDonation.CNP);
                } catch (Exception) {
                    var address = new Address
                    {
                        City = donorRegistrationForDonation.CurrentCity,
                        County = donorRegistrationForDonation.CurrentCounty,
                        Street = "UNKNOWN",
                        Number = 0
                        
                    };

                    await userManager.CreateAsync(new ApplicationUser { Email = donorRegistrationForDonation.Email, UserName = donorRegistrationForDonation.Email }, "Password123.");
                    var donorAccount = await userManager.FindByEmailAsync(donorRegistrationForDonation.Email);
                    var role = await roleManager.FindByNameAsync("Donor");
                    await userManager.AddToRoleAsync(donorAccount, role.Name);

                    donorRegistrationForDonation.Donor = new Donor
                    {
                        CNP = donorRegistrationForDonation.CNP,
                        Address = address,
                        FirstName = donorRegistrationForDonation.Name,
                        LastName = donorRegistrationForDonation.Surname,
                        Id = donorAccount.Id
                    };

                    uow.DonorRepository.Add(donorRegistrationForDonation.Donor);
                }

                uow.DonorRegistrationForDonationRepository.Add(donorRegistrationForDonation);
                uow.Save();
            }
        }

        public async Task<int> SendEmails(IEmailSender _emailSender,int centerId)
        {
            int numberOfEmailsSent = 0;
            using (UnitOfWork uow = new UnitOfWork())
            {
                var centerAddress = uow.CenterRepository.GetAll().Include(c => c.Address)
                                    .First(c => c.Id == centerId).Address;
               
                var users = uow.DonorRepository.GetAll()
                                .Include(d => d.Address)
                                .Where(d => d.Address.City == centerAddress.City);

                await users.ForEachAsync(u =>
                {
                    var email = uow.ApplicationUserRepository.GetById(u.Id).Email;
                    var result = _emailSender.SendEmailConfirmationAsync(email, u, centerAddress);
                    numberOfEmailsSent += result.IsCompletedSuccessfully ? 1 : 0;
                });
            }
            return numberOfEmailsSent;

        }

        public List<Donor> GetDonors()
        {
            using (var uow = new UnitOfWork())
            {
                return uow.DonorRepository.GetAll().ToList();
            }
        }
        public Address GetDonorAddres(int id)
        {
            Address a = new Address();
            using (UnitOfWork uow = new UnitOfWork())
            {
                var addr = uow.AddressRepository.GetById(id);
                a = addr;



                //thrombocytes.Concat(thrombocyteqty);
            }
                
                return a;
            }
        public String GetDonorEmail(string id)
        {
            String m = "";
            using(UnitOfWork uow=new UnitOfWork())
            {
                var mail = uow.ApplicationUserRepository.GetAll().Where(u => u.Id.Equals(id)).First().Email;
                m = mail;
            }
            return m;
        }
        public String GetDonorPhone(string id)
        {
            String m = "";
            using (UnitOfWork uow = new UnitOfWork())
            {
                var ph = uow.ApplicationUserRepository.GetAll().Where(u => u.Id.Equals(id)).First().PhoneNumber;
                m = ph;
            }
            return m;
        }
        
    }
}
