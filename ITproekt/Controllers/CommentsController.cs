using ITproekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITproekt.Controllers
{
    public class CommentsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments/Create
        public ActionResult Create()
        {
            //TODO
            return HttpNotFound();
        }

        // POST: Comments/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return HttpNotFound();
            }
            catch
            {
                return View();
            }
        }

        // POST: Comments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string newContent)
        {
            var currentComment = db.Comments.FirstOrDefault(comment => comment.ID == id);

            if (currentComment != null) {
                currentComment.Content = newContent;
                db.Entry(currentComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToRoute(new {
                    controller = "Posts",
                    action = "Details",
                    id = currentComment.PostID
                });
            }
            return RedirectToRoute(new {
                controller = "Posts",
                action = "Index"
            });
        }

        // POST: Comments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var currentComment = db.Comments.FirstOrDefault(comment => comment.ID == id);

            if (currentComment != null) {
                db.Comments.Remove(currentComment);
                db.SaveChanges();
                return RedirectToRoute(new {
                    controller = "Posts",
                    action = "Details",
                    id = currentComment.PostID
                });
            }

            return RedirectToRoute(new {
                controller = "Posts",
                action = "Index"
            });
        }
    }
}
