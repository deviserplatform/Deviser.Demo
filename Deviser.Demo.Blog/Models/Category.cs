﻿using System;
using System.Collections.Generic;

namespace Deviser.Demo.Blog.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}