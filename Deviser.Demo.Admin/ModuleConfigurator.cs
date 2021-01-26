using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Deviser.Core.Common.Module;
using Deviser.Core.Library.Modules;
using Deviser.Demo.Admin.Models;
using Deviser.Demo.Admin.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Deviser.Demo.Admin
{
    public class ModuleConfigurator : IModuleConfigurator
    {
        public void ConfigureModule(IModuleManifest moduleManifest)
        {
            moduleManifest.ModuleMetaInfo.ModuleName = "DemoAdmin";
            moduleManifest.ModuleMetaInfo.ModuleVersion = typeof(ModuleConfigurator).GetTypeInfo().Assembly.GetName().Version.ToString();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<EmployeeAdminService>();
            services.AddScoped<CustomerAdminGridService>();
            services.AddScoped<EventFormService>(); 
        }
    }
}
