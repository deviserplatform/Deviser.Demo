using System.Text;
using Deviser.Admin;
using Deviser.Admin.Config;
using Deviser.Core.Library.Multilingual;
using Deviser.Demo.Admin.Models;
using Deviser.Demo.Admin.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Deviser.Demo.Admin
{
    public class AdminConfigurator : IAdminConfigurator
    {
        public void ConfigureAdmin(IAdminBuilder adminBuilder)
        {
            adminBuilder.Register<Employee, EmployeeAdminService>(modelBuilder =>
            {
                modelBuilder.GridBuilder.Title = "Employee";
                modelBuilder.FormBuilder.Title = "Employee Details";

                modelBuilder.GridBuilder
                    .AddField(c => c.Name)
                    .AddField(c => c.Designation)
                    .AddField(c => c.Email)
                    .AddField(c=>c.Nationality)
                    .AddField(c => c.IsActive, option =>
                    {
                        option.DisplayName = "Is Active";
                        option.IsTrue = "Active";
                        option.IsFalse = "In Active";
                    });

                modelBuilder.GridBuilder.DisplayFieldAs(c => c.IsActive, LabelType.Badge, c => c.IsActiveBadgeClass);

                modelBuilder.FormBuilder
                    .AddKeyField(c => c.Id)
                    .AddField(c => c.Name, option => { option.EnableIn = FormMode.Create; })
                    .AddField(c => c.Designation)
                    .AddField(c => c.Email, option => option.ValidationType = ValidationType.Email)
                    .AddField(c => c.IsActive)
                    .AddSelectField(c => c.Nationality);

                modelBuilder.FormBuilder.Property(c => c.Nationality).HasLookup(sp => sp.GetService<EmployeeAdminService>().GetCountries(),
                    ke => ke.Code,
                    de => de.Name);
            });

            adminBuilder.RegisterGrid<Customer, CustomerAdminGridService>(builder =>
            {
                builder.Title = "Customers";

                builder
                    .AddKeyField(c=>c.Id)
                    .AddField(c => c.OrderId)
                    .AddField(c => c.Name)
                    .AddField(c => c.Email)
                    .AddField(c => c.OrderDate)
                    .AddField(c => c.OrderStatus)
                    .AddField(c => c.ShipDate)
                    .AddField(c => c.ShipCountry);

                builder.Property(c => c.ShipCountry).HasLookup(sp => sp.GetService<EmployeeAdminService>().GetCountries(),
                    ke => ke.Code,
                    de => de.Name);

                builder.Property(c => c.OrderStatus).HasLookup(sp => OrderStatus.OrderStatuses,
                    ke => ke.Id,
                    de => de.Name);

                builder.DisplayFieldAs(c => c.OrderStatus, LabelType.Badge, c => c.OrderStatusClass);

                builder.AddRowAction("MarkAsDelivered", "Mark As Delivered",
                    (provider, item) => provider.GetService<CustomerAdminGridService>().MarkDelivered(item));

                builder.HideEditButton();
            });

            adminBuilder.RegisterForm<Guest, EventFormService>(builder =>
            {
                builder
                     .AddFieldSet("General", fieldBuilder =>
                     {
                         fieldBuilder
                             .AddKeyField(c => c.Id)
                             .AddField(p => p.Name)
                             .AddSelectField(p => p.Gender)
                             .AddField(p => p.Email, option => option.ValidationType = ValidationType.Email);
                     })

                     .AddFieldSet("Dietary requirements", fieldBuilder =>
                     {
                         fieldBuilder
                             .AddField(p => p.IsTakePartInDinner)
                             .AddSelectField(p => p.FoodType);
                     });

                builder.Property(f => f.FoodType)
                    .ShowOn(f => f.IsTakePartInDinner)
                    .ValidateOn(f => f.IsTakePartInDinner);

                builder.Property(f => f.Gender).HasLookup(
                    sp => Gender.Genders,
                    ke => ke.Id,
                    de => de.Name);

                builder.Property(f => f.FoodType).HasLookup(
                    sp => FoodType.FoodTypes,
                    ke => ke.Id,
                    de => de.Name);
            });
        }
    }

    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
