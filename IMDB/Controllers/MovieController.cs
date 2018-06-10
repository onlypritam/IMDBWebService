using IMDB.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IMDB.Controllers
{
    public class MovieController : ApiController
    {
        private IMDBEntities db = new IMDBEntities();

        // GET: api/Movie
        public IQueryable<Movie> GetMovies1()
        {
            return db.Movies1;
        }

        // GET: api/Movie/5
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> GetMovie(int id)
        {
            Movie movie = await db.Movies1.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movie/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            try
            {
                CreateMovieActorRelation(movie);
                movie.Actors.Clear();
                db.Entry(movie).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movie
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            movie.Actors.Clear();
            db.Movies1.Add(movie);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = movie.MovieName }, movie);
        }

        // DELETE: api/Movie/5
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> DeleteMovie(int id)
        {
            Movie movie = await db.Movies1.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies1.Remove(movie);
            await db.SaveChangesAsync();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies1.Count(e => e.MovieId == id) > 0;
        }

        private void CreateMovieActorRelation(Movie movie)
        {
            try
            {
                string SqlCommandStr;
                using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IMDBConnection"].ConnectionString))
                {
                    conn.Open();
                    foreach (Actor actor in movie.Actors)
                    {
                        using (SqlCommand cmd = new SqlCommand("select count(*) from [dbo].[Actors] where ActorId = " + actor.ActorId ))
                        {
                            cmd.Connection = conn;
                            if((int)cmd.ExecuteScalar() > 0)
                            {
                                SqlCommandStr = "INSERT INTO [dbo].[MovieActor] ([MovieId],[ActorId]) VALUES (" + movie.MovieId + "," + actor.ActorId + ")";
                            }
                            else
                            {
                                continue;
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand(SqlCommandStr))
                        {
                            cmd.Connection = conn;
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch
            {
                throw;
            }
        }
    }
}