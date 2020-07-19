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
    public class PostsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PostsApi
        public IQueryable<Post> GetPosts()
        {
            return db.Posts
                .Include(post => post.Comments);
        }

        // GET: api/PostsApi/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(int id)
        {
            Post postToReturn = db.Posts
                .Where(post => post.ID == id)
                .Include(post => post.Comments)
                .FirstOrDefault();
            if (postToReturn == null)
            {
                return NotFound();
            }

            return Ok(postToReturn);
        }

        // PUT: api/PostsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.ID)
            {
                return BadRequest();
            }

            var postToUpdate = db.Posts
                .Where(p => p.ID == id)
                .Include(p => p.Comments)
                .FirstOrDefault();

            if (postToUpdate != null) {
                postToUpdate.Update(post);
            }

            db.Entry(postToUpdate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/PostsApi
        [ResponseType(typeof(Post))]
        public IHttpActionResult PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Posts.Add(post);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.ID }, post);
        }

        // DELETE: api/PostsApi/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult DeletePost(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            db.Posts.Remove(post);
            db.SaveChanges();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.ID == id) > 0;
        }
    }
}