using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deviser.Demo.Blog.DTO
{
    public class Comments
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsApproved { get; set; }
        public Guid PostId { get; set; }
    }

}
