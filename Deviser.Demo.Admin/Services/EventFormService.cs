using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deviser.Admin.Config;
using Deviser.Admin.Services;
using Deviser.Demo.Admin.Models;

namespace Deviser.Demo.Admin.Services
{
    public class EventFormService: IAdminFormService<Guest>
    {
        public async Task<Guest> GetModel()
        {
            return await Task.FromResult(new Guest());
        }

        public async Task<IFormResult<Guest>> SaveModel(Guest item)
        {
            var guest = AddGuest(item);
            if (guest != null)
            {
                var result = new FormResult<Guest>(guest)
                {
                    IsSucceeded = true,
                    FormBehaviour = FormBehaviour.StayOnEditMode,
                    SuccessMessage = "New registration has been created successfully"
                };
                return await Task.FromResult(result);
            }
            else
            {
                var result = new FormResult<Guest>(guest)
                {
                    IsSucceeded = false,
                    FormBehaviour = FormBehaviour.StayOnEditMode,
                    SuccessMessage = "Error occurred while signing up :("
                };
                return await Task.FromResult(result);
            }
        }

        private Guest AddGuest(Guest item)
        {
            //Add Guest object to db
            return item;
        }
    }
}
