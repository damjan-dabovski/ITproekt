using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITproekt.Models {
    public class HomepageViewModel {
        public List<Post> NewestPosts { get; set; }
        public List<Product> HotProducts { get; set; }
    }
}