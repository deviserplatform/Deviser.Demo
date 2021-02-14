using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deviser.Admin.Web.Controllers;
using Deviser.Core.Library.Modules;

namespace Deviser.Demo.Blog.Controllers
{
    [Module("DemoAdminBlog")]
    public class AdminController : AdminController<AdminConfigurator>
    {
        public AdminController(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }
    }
}
