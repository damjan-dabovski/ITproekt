using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ITproekt.Models;

namespace ITproekt.Controllers.Api
{
    public class CommentsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CommentsApi
        public IQueryable<Comment> GetComments()
        {
            return db.Comments;
        }

        // GET: api/CommentsApi/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/CommentsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.ID)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CommentsApi
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(CommentPostApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentToAdd = new Comment() { AuthorName = model.AuthorName, Content = model.Comment.Content, PostID = model.PostId };
            var targetPost = db.Posts.FirstOrDefault(post => post.ID == model.PostId);
            if (targetPost != null) {
                targetPost.Comments.Add(commentToAdd);
                db.Entry(targetPost).State = EntityState.Modified;
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = commentToAdd.ID }, commentToAdd);
        }

        // DELETE: api/CommentsApi/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.ID == id) > 0;
        }
    }
}