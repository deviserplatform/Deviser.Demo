using System;
using System.Collections.Generic;
using Deviser.Admin.Attributes;
using Deviser.Admin.Config;

namespace Deviser.Demo.Blog.DTO
{
    public class Post
    {
        [Order]
        public Guid Id { get; set; }

        [Order]
        public string Title { get; set; }
        
        [Order]
        [FieldInfo(FieldType.TextArea)]
        public string Summary { get; set; }

        [Order]
        [FieldInfo(FieldType.Image)]
        public string Thumbnail { get; set; }

        [Order]
        [FieldInfo(FieldType.RichText)]
        public string Content { get; set; }
        
        [Order]
        public bool IsCommentEnabled { get; set; }

        [Order]
        public Guid BlogId { get; set; }

        [Order]
        public Guid CategoryId { get; set; }

        [Order]
        public Category Category { get; set; }

        [Order]
        public ICollection<Tag> Tags { get; set; }

        [Order]
        public List<Comments> Comments { get; set; }

        [Order]
        public DateTime CreatedOn { get; set; }

        [Order]
        public Guid CreatedBy { get; set; }

        [Order]
        public DateTime ModifiedOn { get; set; }

        [Order]
        public Guid ModifiedBy { get; set; }

    }
}