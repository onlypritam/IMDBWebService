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
    public class ProducerController : ApiController
    {
        private IMDBEntities db = new IMDBEntities();


        public IQueryable<Producer> GetProducers()
        {
            return db.Producers;
        }


        [ResponseType(typeof(Producer))]
        public async Task<IHttpActionResult> GetProducer(int id)
        {
            Producer producer = await db.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            return Ok(producer);
        }


        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProducer(int id, Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producer.ProducerId)
            {
                return BadRequest();
            }

            db.Entry(producer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducerExists(id))
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


        [ResponseType(typeof(Producer))]
        public async Task<IHttpActionResult> PostProducer(Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Producers.Add(producer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = producer.ProducerName }, producer);
        }


        [ResponseType(typeof(Producer))]
        public async Task<IHttpActionResult> DeleteProducer(int id)
        {
            Producer producer = await db.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            db.Producers.Remove(producer);
            await db.SaveChangesAsync();

            return Ok(producer);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private bool ProducerExists(int id)
        {
            return db.Producers.Count(e => e.ProducerId == id) > 0;
        }
    }
}