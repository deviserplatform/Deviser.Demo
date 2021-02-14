using System;

namespace Deviser.Demo.Blog.DTO
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PostCount { get; set; }
    }
}