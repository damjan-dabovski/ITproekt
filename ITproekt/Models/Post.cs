using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITproekt.Models {
    public class Post {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public Post() {
            DateModified = DateCreated = DateTime.UtcNow;
        }

        public Post Update(Post newVersion) {
            Title = newVersion.Title;
            Content = newVersion.Content;
            DateModified = DateTime.UtcNow;
            return this;
        }
    }
}