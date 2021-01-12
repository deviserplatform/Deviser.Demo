using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deviser.Admin.Config;
using Deviser.Admin.Config.Filters;
using Deviser.Admin.Data;
using Deviser.Admin.Extensions;
using Deviser.Admin.Services;
using Deviser.Core.Common;
using Deviser.Core.Common.FileProviders;
using Newtonsoft.Json;

namespace Deviser.Demo.Admin
{
    public class EmployeeAdminService : IAdminService<Employee>
    {
        private static readonly string CountriesJson = EmbeddedProvider.GetFileContentAsString(typeof(EmployeeAdminService).Assembly, "countries.json");
        private static readonly ICollection<Country> Countries = SDJsonConvert.DeserializeObject<List<Country>>(CountriesJson);
        private static readonly ICollection<Employee> Employees = new List<Employee>()
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

        public async Task<PagedResult<Employee>> GetAll(int pageNo, int pageSize, string orderByProperties, FilterNode filter = null)
        {
            var employeesJson= JsonConvert.SerializeObject(Employees);
            var resultEmployees = JsonConvert.DeserializeObject<ICollection<Employee>>(employeesJson);
            if (filter != null)
            {
                resultEmployees = resultEmployees.ApplyFilter(filter).ToList();
            }
            var result = new PagedResult<Employee>(resultEmployees, pageNo, pageSize, orderByProperties);
            return await Task.FromResult(result);
        }

        public async Task<Employee> GetItem(string itemId)
        {
            var result = Employees.FirstOrDefault(e => e.Id == Guid.Parse(itemId));
            return await Task.FromResult(result);
        }

        public async Task<IFormResult<Employee>> CreateItem(Employee item)
        {
            item.Id = Guid.NewGuid();
            Employees.Add(item);
            var resultEmployee = item; 
            var result = new FormResult<Employee>(resultEmployee)
            {
                IsSucceeded = true,
                SuccessMessage = "Employee has been created"
            };
            return await Task.FromResult(result);
        }

        public async Task<IFormResult<Employee>> UpdateItem(Employee item)
        {
            var employeeToUpdate = Employees.FirstOrDefault(e => e.Id == item.Id);

            employeeToUpdate.Name = item.Name;
            employeeToUpdate.Designation = item.Designation;
            employeeToUpdate.Email = item.Email;
            employeeToUpdate.Nationality = Countries.FirstOrDefault(c => c.Code == item.Nationality.Code);
            employeeToUpdate.IsActive = item.IsActive;

            var resultEmployee = employeeToUpdate;
            var result = new FormResult<Employee>(resultEmployee)
            {
                IsSucceeded = true,
                SuccessMessage = "Employee has been updated"
            };
            return await Task.FromResult(result);
        }

        public async Task<IAdminResult<Employee>> DeleteItem(string itemId)
        {
            var employeeToRemove = Employees.FirstOrDefault(e => e.Id == Guid.Parse(itemId));
            if (!Employees.Remove(employeeToRemove))
            {
                return await Task.FromResult<AdminResult<Employee>>(new AdminResult<Employee>()
                {
                    IsSucceeded = false,
                    ErrorMessage = "Unable to delete Employee"
                });
            }

            var result = new FormResult<Employee>(employeeToRemove)
            {
                IsSucceeded = true,
                SuccessMessage = "Employee has been deleted"
            };
            return await Task.FromResult(result);
        }

        public ICollection<Country> GetCountries()
        {
            return Countries;
        }
    }
}