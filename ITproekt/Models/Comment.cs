using System;
using System.ComponentModel.DataAnnotations;

namespace ITproekt.Models {
    public class Comment {
        public int ID { get; set; }
        public string AuthorName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }

        public int PostID { get; set; }

        public Comment() {
            DateCreated = DateTime.UtcNow;
        }
    }
}