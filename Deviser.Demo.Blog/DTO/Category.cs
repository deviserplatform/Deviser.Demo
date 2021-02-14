using System;
using System.Collections.Generic;
using Deviser.Admin.Attributes;

namespace Deviser.Demo.Blog.DTO
{
    public class Category : IComparable<Category>
    {
        [Order]
        public Guid Id { get; set; }

        [Order]
        public string Name { get; set; }

        public List<Post> Posts { get; set; }

        public int PostCount { get; set; }

        public int CompareTo(Category other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0) return idComparison;
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }
}