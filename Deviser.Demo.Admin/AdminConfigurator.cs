using System;
using System.Text;
using Deviser.Admin;
using Deviser.Admin.Config;
using Deviser.Core.Common.DomainTypes;
using Deviser.Core.Data.Repositories;
using Deviser.Core.Library.Multilingual;
using Deviser.Demo.Admin.Models;
using Deviser.Demo.Admin.Services;
using Microsoft.Extensions.DependencyInjection;
using FieldType = Deviser.Admin.Config.FieldType;

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
                    .AddField(c => c.Nationality)
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
                    .AddKeyField(c => c.Id)
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

            adminBuilder.RegisterTreeAndForm<Folder, FolderAdminService>(builder =>
            {
                builder.TreeBuilder.Title = "File Manager";
                builder.FormBuilder.Title = "File Manager";
                builder.TreeBuilder.ConfigureTree(p => p.Id,
                    p => p.Name,
                    p => p.Parent,
                    p => p.SubFolders,
                    p => p.SortOrder);

                var formBuilder = builder.FormBuilder;
                var adminId = Guid.Parse("5308b86c-a2fc-4220-8ba2-47e7bec1938d");
                var urlId = Guid.Parse("bfefa535-7af1-4ddc-82c0-c906c948367a");
                var standardId = Guid.Parse("4c06dcfd-214f-45af-8404-ff84b412ab01");

                formBuilder
                     .AddFieldSet("General", fieldBuilder =>
                     {
                         fieldBuilder
                             .AddField(p => p.Name);
                     })

                     .AddFieldSet("Permissions", fieldBuilder =>
                     {
                         fieldBuilder.AddCheckBoxMatrix(p => p.PagePermissions,
                             p => p.RoleId,
                             p => p.PermissionId,
                             p => p.Id,
                             p => p.FolderId, typeof(Role), typeof(Permission),
                             option => option.IsRequired = false);
                     });

                formBuilder.Property(f => f.PagePermissions).HasMatrixLookup<Role, Permission, Guid>(
                    sp => sp.GetService<IRoleRepository>().GetRoles(),
                    ke => ke.Id,
                    de => de.Name,
                    sp => sp.GetService<IPermissionRepository>().GetPagePermissions(),
                    ke => ke.Id,
                    de => de.Name);
            });
        }
    }
}
