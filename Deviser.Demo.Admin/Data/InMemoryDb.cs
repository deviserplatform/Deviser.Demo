using System;
using System.Collections.Generic;
using System.Linq;
using Deviser.Core.Common;
using Deviser.Core.Common.FileProviders;
using Deviser.Demo.Admin.Models;
using Deviser.Demo.Admin.Services;
using Newtonsoft.Json;

namespace Deviser.Demo.Admin.Data
{
    public class InMemoryDb
    {
        private static readonly string CountriesJson = EmbeddedProvider.GetFileContentAsString(typeof(EmployeeAdminService).Assembly, "countries.json");

        private static Folder RootFolder = new Folder()
        {
            Id = Guid.NewGuid(),
            Name = "Root",
            SortOrder = 0,
            SubFolders = new List<Folder>()
            {
            }
        };

        private static ICollection<Folder> SubFolders = new List<Folder>()
        {
            new Folder()
            {
                Id = Guid.NewGuid(),
                Name = "Folder 1",
                SortOrder = 0,
                ParentId = RootFolder.Id,
                SubFolders = new List<Folder>()
                {
                }
            },
            new Folder()
            {
                Id = Guid.NewGuid(),
                Name = "Folder 2",
                SortOrder = 0,
                ParentId = RootFolder.Id,
                SubFolders = new List<Folder>()
                {
                }
            },
            new Folder()
            {
                Id = Guid.NewGuid(),
                Name = "Folder 3",
                SortOrder = 0,
                ParentId = RootFolder.Id,
                SubFolders = new List<Folder>()
                {
                }
            }
        };

        static InMemoryDb()
        {
            var flatFolders = new List<Folder>()
            {
                RootFolder
            };
            flatFolders.AddRange(SubFolders);
            FlatFolders = flatFolders;
        }

        public static readonly ICollection<Country> Countries = SDJsonConvert.DeserializeObject<List<Country>>(CountriesJson);

        public static readonly ICollection<Employee> Employees = new List<Employee>()
        {
            new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Abigail Adam",
                Designation = "COO",
                Email = "abigail.adam@deviser.demo.com",
                IsActive = true,
                Nationality = Countries.First(c => c.Code == "AU")

            },
            new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Alexandra Harry",
                Designation = "CFO",
                Email = "alexandra.harry@deviser.demo.com",
                IsActive = false,
                Nationality = Countries.First(c => c.Code == "BR")
            },
            new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Alison Edward",
                Designation = "CIO",
                Email = "alison.harry@deviser.demo.com",
                IsActive = false,
                Nationality = Countries.First(c => c.Code == "CH")
            },
            new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Emily Harry",
                Designation = "COO",
                Email = "emily.harry@deviser.demo.com",
                IsActive = true,
                Nationality = Countries.First(c => c.Code == "AU")
            },
            new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Samantha Nathan",
                Designation = "CEO",
                Email = "alison.nathan@deviser.demo.com",
                IsActive = true,
                Nationality = Countries.First(c => c.Code == "IN")
            },
            new Employee()
            {
                Id = Guid.NewGuid(),
                Name = "Amy Jackson",
                Designation = "COO",
                Email = "amy.Jackson@deviser.demo.com",
                IsActive = true,
                Nationality = Countries.First(c => c.Code == "GB")
            }
        };

        public static readonly ICollection<Customer> Customers = new List<Customer>()
        {
            new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Abigail Adam",
                OrderId = "ORD5001",
                Email = "abigail.adam@deviser.demo.com",
                OrderDate = new DateTime(2020,01,30),
                OrderStatus = OrderStatus.OrderStatuses.First(o=>o.Id ==1),
                ShipDate =  new DateTime(2020,02,25),
                ShipCountry = Countries.First(c => c.Code == "AU")

            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Alexandra Harry",
                OrderId = "ORD5002",
                Email = "alexandra.harry@deviser.demo.com",
                OrderDate = new DateTime(2020,01,15),
                OrderStatus = OrderStatus.OrderStatuses.First(o=>o.Id ==2),
                ShipDate =  new DateTime(2020,02,10),
                ShipCountry = Countries.First(c => c.Code == "BR")
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Alison Edward",
                OrderId = "ORD5003",
                Email = "alison.harry@deviser.demo.com",
                OrderDate = new DateTime(2020,01,23),
                OrderStatus = OrderStatus.OrderStatuses.First(o=>o.Id ==4),
                ShipDate =  new DateTime(2020,02,11),
                ShipCountry = Countries.First(c => c.Code == "CH")
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Emily Harry",
                OrderId = "ORD5004",
                Email = "emily.harry@deviser.demo.com",
                OrderDate = new DateTime(2020,01,23),
                ShipDate =  new DateTime(2020,02,10),
                OrderStatus = OrderStatus.OrderStatuses.First(o=>o.Id ==4),
                ShipCountry = Countries.First(c => c.Code == "AU")
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Samantha Nathan",
                OrderId = "ORD5005",
                Email = "alison.nathan@deviser.demo.com",
                OrderDate = new DateTime(2020,01,25),
                ShipDate =  new DateTime(2020,02,14),
                OrderStatus = OrderStatus.OrderStatuses.First(o=>o.Id ==3),
                ShipCountry = Countries.First(c => c.Code == "IN")
            },
            new Customer()
            {
                Id = Guid.NewGuid(),
                Name = "Amy Jackson",
                OrderId = "ORD5006",
                Email = "amy.Jackson@deviser.demo.com",
                OrderDate = new DateTime(2020,02,15),
                ShipDate =  new DateTime(2020,03,10),
                OrderStatus = OrderStatus.OrderStatuses.First(o=>o.Id ==4),
                ShipCountry = Countries.First(c => c.Code == "GB")
            }
        };

        public static ICollection<Folder> FlatFolders { get; }
    }
}
