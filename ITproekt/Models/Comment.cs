﻿using System;

namespace ITproekt.Models {
    public class Comment {
        public int ID { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public int PostID { get; set; }
    }
}