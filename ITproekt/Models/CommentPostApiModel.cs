using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITproekt.Models {
    public class CommentPostApiModel {
        public Comment Comment { get; set; }
        public int PostId { get; set; }
        public string AuthorName { get; set; }
    }
}