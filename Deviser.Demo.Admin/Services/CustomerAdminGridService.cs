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
    public class CustomerAdminGridService: IAdminGridService<Customer>
    {
        public async Task<PagedResult<Customer>> GetAll(int pageNo, int pageSize, string orderByProperties, FilterNode filter = null)
        {
            var customersJson = JsonConvert.SerializeObject(InMemoryDb.Customers);
            var resultCustomers = JsonConvert.DeserializeObject<ICollection<Customer>>(customersJson);
            if (filter != null)
            {
                resultCustomers = resultCustomers.ApplyFilter(filter).ToList();
            }
            var result = new PagedResult<Customer>(resultCustomers, pageNo, pageSize, orderByProperties);
            return await Task.FromResult(result);
        }

        public async Task<IAdminResult<Customer>> DeleteItem(string itemId)
        {
            var customerToRemove = InMemoryDb.Customers.FirstOrDefault(e => e.Id == Guid.Parse(itemId));
            if (!InMemoryDb.Customers.Remove(customerToRemove))
            {
                return await Task.FromResult<AdminResult<Customer>>(new AdminResult<Customer>()
                {
                    IsSucceeded = false,
                    ErrorMessage = "Unable to delete Customer"
                });
            }

            var result = new FormResult<Customer>(customerToRemove)
            {
                IsSucceeded = true,
                SuccessMessage = "Customer has been deleted"
            };
            return await Task.FromResult(result);
        }

        public async Task<IAdminResult> MarkDelivered(Customer item)
        {
            var resultItem = InMemoryDb.Customers.FirstOrDefault(e => e.Id == item.Id);
            resultItem.OrderStatus = OrderStatus.OrderStatuses.FirstOrDefault(s => s.Id == 4);
            //_pageRepository.DeletePage();
            if (resultItem != null)
            {
                return await Task.FromResult(new AdminResult(resultItem)
                {
                    IsSucceeded = true,
                    SuccessMessage = $"{item.Name} has been marked as delivered"
                });
            }

            return await Task.FromResult(new AdminResult(resultItem)
            {
                IsSucceeded = false,
                SuccessMessage = $"Unable to mark {item.Name} as delivered"
            });
        }
    }
}
