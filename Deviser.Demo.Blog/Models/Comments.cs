using System;

namespace Deviser.Demo.Blog.Models
{
    public class Comments
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsApproved { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}