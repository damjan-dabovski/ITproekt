using ITproekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITproekt.Controllers {
    public class HomeController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index() {
            var newestPosts = (from post in db.Posts
                              orderby post.DateCreated descending
                              select post).Take(3).ToList();

            var hotProducts = (from product in db.Products
                               select product).Take(3).ToList();

            var model = new HomepageViewModel() { HotProducts = hotProducts, NewestPosts = newestPosts };

            return View(model);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}