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

        // POST: Comments/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Content")] Comment comment, int postId)
        {
            var commentToAdd = new Comment() { AuthorName = "TEMP", Content = comment.Content, PostID = postId };
            var targetPost = db.Posts.FirstOrDefault(post => post.ID == postId);
            if (targetPost != null) {
                targetPost.Comments.Add(commentToAdd);
                db.Entry(targetPost).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToRoute(new {
                controller = "Posts",
                action = "Details",
                id = postId
            });
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
