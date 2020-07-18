using ITproekt.Helpers;
using ITproekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITproekt.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        [HttpGet]
        [Authorize(Roles = Roles.ADMIN)]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
    }
}