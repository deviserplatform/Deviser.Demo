using System;
using System.Collections.Generic;

namespace Deviser.Demo.Blog.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}