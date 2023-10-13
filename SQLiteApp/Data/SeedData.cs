using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NonProfitLibrary;

namespace SQLiteApp.Data;

public static class SeedData
{
    // this is an extension method to the ModelBuilder class
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactList>().HasData(
            GetContactList()
        );
        modelBuilder.Entity<TransactionType>().HasData(
            GetTransactionType()
        );
        modelBuilder.Entity<PaymentMethod>().HasData(
            GetPaymentMethod()
        );
        modelBuilder.Entity<Donations>().HasData(
            GetDonations()
        );
    }

    public static List<ContactList> GetContactList()
    {
        List<ContactList> contacts = new List<ContactList>()
            {
                new ContactList()
                {
                    AccountNo  = 1,
                    FirstName  = "John",
                    LastName   = "Doe",
                    Email      = "john@email.com",
                    Street     = "123 Main St",
                    City       = "Anytown",
                    PostalCode = "12345",
                    Country    = "USA",
                    Created    = DateTime.Now,
                    Modified   = DateTime.Now,
                    CreatedBy  = "System",
                    ModifiedBy = "System"
                },
                new ContactList()
                {
                    AccountNo = 2,
                    FirstName  = "Jane",
                    LastName   = "Doe",
                    Email      = "jane@email.com",
                    Street     = "123 Main St",
                    City       = "Anytown",
                    PostalCode = "12345",
                    Country    = "USA",
                    Created    = DateTime.Now,
                    Modified   = DateTime.Now,
                    CreatedBy  = "System",
                    ModifiedBy = "System"
                },
                new ContactList()
                {
                    AccountNo  = 3,
                    FirstName  = "Bob",
                    LastName   = "Smith",
                    Email      = "Bob@email.com",
                    Street     = "123 Main St",
                    City       = "Anytown",
                    PostalCode = "12345",
                    Country    = "CAN",
                    Created    = DateTime.Now,
                    Modified   = DateTime.Now,
                    CreatedBy  = "System",
                    ModifiedBy = "System"
                }
            };
        return contacts;
    }

    public static List<TransactionType> GetTransactionType()
    {
        List<TransactionType> transactionTypes = new List<TransactionType>()
            {
                new TransactionType()
                    {
                        TransactionTypeId = 1,
                        Name              = "Donation",
                        Description       = "Donation",
                        Created           = DateTime.Now,
                        Modified          = DateTime.Now,
                        CreatedBy         = "System",
                        ModifiedBy        = "System"
                    },
                    new TransactionType()
                    {
                        TransactionTypeId = 2,
                        Name              = "Membership",
                        Description       = "Membership",
                        Created           = DateTime.Now,
                        Modified          = DateTime.Now,
                        CreatedBy         = "System",
                        ModifiedBy        = "System"
                    },
                    new TransactionType()
                    {
                        TransactionTypeId = 3,
                        Name              = "Event",
                        Description       = "Event",
                        Created           = DateTime.Now,
                        Modified          = DateTime.Now,
                        CreatedBy         = "System",
                        ModifiedBy        = "System"
                    }
            };
        return transactionTypes;
    }

    public static List<PaymentMethod> GetPaymentMethod()
    {
        List<PaymentMethod> paymentMethods = new List<PaymentMethod>()
            {
                new PaymentMethod()
                    {
                        PaymentMethodId = 1,
                        Name            = "Cash",
                        Created         = DateTime.Now,
                        Modified        = DateTime.Now,
                        CreatedBy       = "System",
                        ModifiedBy      = "System"
                    },
                    new PaymentMethod()
                    {
                        PaymentMethodId = 2,
                        Name            = "Check",
                        Created         = DateTime.Now,
                        Modified        = DateTime.Now,
                        CreatedBy       = "System",
                        ModifiedBy      = "System"
                    },
                    new PaymentMethod()
                    {
                        PaymentMethodId = 3,
                        Name            = "Credit Card",
                        Created         = DateTime.Now,
                        Modified        = DateTime.Now,
                        CreatedBy       = "System",
                        ModifiedBy      = "System"
                    }
            };
        return paymentMethods;
    }

    public static List<Donations> GetDonations()
    {
        List<Donations> donations = new List<Donations>()
        {
            new Donations()
            {
                TransId           = 1,
                Date              = DateTime.Now,
                AccountNo         = 1,
                TransactionTypeId = 1,
                Amount            = 100,
                PaymentMethodId   = 1,
                Notes             = "Donation",
                Created           = DateTime.Now,
                Modified          = DateTime.Now,
                CreatedBy         = "System",
                ModifiedBy        = "System"
                },
            new Donations()
            {
                TransId           = 2,
                Date              = DateTime.Now,
                AccountNo         = 2,
                TransactionTypeId = 2,
                Amount            = 200,
                PaymentMethodId   = 2,
                Notes             = "Event",
                Created           = DateTime.Now,
                Modified          = DateTime.Now,
                CreatedBy         = "System",
                ModifiedBy        = "System"
            },
            new Donations()
            {
                TransId           = 3,
                Date              = DateTime.Now,
                AccountNo         = 3,
                TransactionTypeId = 3,
                Amount            = 300,
                PaymentMethodId   = 3,
                Notes             = "Membership",
                Created           = DateTime.Now,
                Modified          = DateTime.Now,
                CreatedBy         = "System",
                ModifiedBy        = "System"
            }
        };
        return donations;
    }
}
