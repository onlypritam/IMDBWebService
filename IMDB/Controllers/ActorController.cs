using IMDB.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IMDB.Controllers
{
    public class ActorController : ApiController
    {
        private IMDBEntities db = new IMDBEntities();

 
        public IQueryable<Actor> GetActors()
        {
            return db.Actors;
        }

 
        [ResponseType(typeof(Actor))]
        public async Task<IHttpActionResult> GetActor(int id)
        {
            Actor actor = await db.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor);
        }

 
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActor(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actor.ActorId)
            {
                return BadRequest();
            }

            db.Entry(actor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        
        [ResponseType(typeof(Actor))]
        public async Task<IHttpActionResult> PostActor(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Actors.Add(actor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = actor.ActorName }, actor);
        }

        
        [ResponseType(typeof(Actor))]
        public async Task<IHttpActionResult> DeleteActor(int id)
        {
            Actor actor = await db.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            db.Actors.Remove(actor);
            await db.SaveChangesAsync();

            return Ok(actor);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool ActorExists(int id)
        {
            return db.Actors.Count(e => e.ActorId == id) > 0;
        }
    }
}