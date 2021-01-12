using System.Text;
using Deviser.Admin;
using Deviser.Admin.Config;
using Deviser.Core.Library.Multilingual;
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
        }
    }

    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
