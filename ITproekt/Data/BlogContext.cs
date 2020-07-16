using ITproekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ITproekt.Data {
    public class BlogContext: DbContext {

        public BlogContext() : base() { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}