using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITproekt.Helpers;
using ITproekt.Models;
using Microsoft.AspNet.Identity;

namespace ITproekt.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(string q, string category = "title")
        {
            var posts = db.Posts
                .Include(post => post.Comments)
                .ToList();

            if (!String.IsNullOrEmpty(q)) {
                switch (category.ToLower()) {
                    case "title":
                        posts = posts.Where(post => post.Title.ToLower().Contains(q.ToLower())).ToList();
                        break;
                    case "content":
                        posts = posts.Where(post => post.Content.ToLower().Contains(q.ToLower())).ToList();
                        break;
                    default:
                        posts = posts.Where(post => post.Title.ToLower().Contains(q.ToLower())).ToList();
                        break;
                }
            }

            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post postToReturn = db.Posts
                .Where(post => post.ID == id)
                .Include(post => post.Comments)
                .FirstOrDefault();
            if (postToReturn == null)
            {
                return HttpNotFound();
            }
            return View(postToReturn);
        }

        // GET: Posts/Create
        [HttpGet]
        [Authorize(Roles = Roles.ADMIN)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,DateCreated,DateModified")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [HttpGet]
        [Authorize(Roles = Roles.ADMIN)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [Authorize(Roles = Roles.ADMIN)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Title, Content")] Post post, int id)
        {
            var postToUpdate = db.Posts
                .Where(p => p.ID == id)
                .Include(p => p.Comments)
                .FirstOrDefault();

            if (postToUpdate != null) {
                postToUpdate.Update(post);
            }

            if (ModelState.IsValid)
            {
                db.Entry(postToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = postToUpdate.ID });
            }
            return View(postToUpdate);
        }

        //POST: Posts/Preview
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = Roles.ADMIN)]
        public ActionResult Preview(string titleValue, string contentValue) {
            var post = new Post() { Title = titleValue, Content = contentValue };
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = Roles.ADMIN)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = Roles.ADMIN)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
