using System;
using System.Collections.Generic;

namespace Deviser.Demo.Blog.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Thumbnail { get; set; }
        public string Content { get; set; }
        public bool IsCommentEnabled { get; set; }
        public Guid BlogId { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public List<Comments> Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}