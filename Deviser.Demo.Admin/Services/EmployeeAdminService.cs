using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deviser.Admin.Config;
using Deviser.Admin.Config.Filters;
using Deviser.Admin.Data;
using Deviser.Admin.Extensions;
using Deviser.Admin.Services;
using Deviser.Demo.Admin.Data;
using Deviser.Demo.Admin.Models;
using Newtonsoft.Json;

namespace Deviser.Demo.Admin.Services
{
    public class EmployeeAdminService : IAdminService<Employee>
    {
        public async Task<PagedResult<Employee>> GetAll(int pageNo, int pageSize, string orderByProperties, FilterNode filter = null)
        {
            var employeesJson= JsonConvert.SerializeObject(InMemoryDb.Employees);
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
            var result = InMemoryDb.Employees.FirstOrDefault(e => e.Id == Guid.Parse(itemId));
            return await Task.FromResult(result);
        }

        public async Task<IFormResult<Employee>> CreateItem(Employee item)
        {
            item.Id = Guid.NewGuid();
            InMemoryDb.Employees.Add(item);
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
            var employeeToUpdate = InMemoryDb.Employees.FirstOrDefault(e => e.Id == item.Id);

            employeeToUpdate.Name = item.Name;
            employeeToUpdate.Designation = item.Designation;
            employeeToUpdate.Email = item.Email;
            employeeToUpdate.Nationality = InMemoryDb.Countries.FirstOrDefault(c => c.Code == item.Nationality.Code);
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
            var employeeToRemove = InMemoryDb.Employees.FirstOrDefault(e => e.Id == Guid.Parse(itemId));
            if (!InMemoryDb.Employees.Remove(employeeToRemove))
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
            return InMemoryDb.Countries;
        }
    }
}