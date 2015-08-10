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
using MisAnuncios.Models;

namespace MisAnuncios.Controllers
{
    public class AnunciosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Anuncios
        public IQueryable<Anuncio> GetAnuncios()
        {
            var anuncios = from m in db.Anuncios
                           select m;
            anuncios = db.Anuncios.Include(p => p.Usuario);
            anuncios.Include(t => t.Cat);
           
            return anuncios;
        }

        // GET: api/Anuncios/5
        //[ResponseType(typeof(Anuncio))]
        //public IHttpActionResult GetAnuncio(int id)
        //{
        //    Anuncio anuncio = db.Anuncios.Find(id);
        //    if (anuncio == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(anuncio);
        //}

        // GET: api/Anuncios/Ciudad
        [HttpGet]
        [Route("api/Anuncios/{ciudad}")]
        [ResponseType(typeof(IQueryable<Anuncio>))]
        public IHttpActionResult GetAnuncioByCiudad(string ciudad)
        {
            var anuncios = from m in db.Anuncios
                         select m;
            anuncios = db.Anuncios.Include(p => p.Usuario);
            anuncios.Include(t => t.Cat);
            anuncios = anuncios.Where(s => s.Ciudad.Equals(ciudad));
           

            if (anuncios.Count()==0)
            {
                return NotFound();
            }

            return Ok(anuncios);
        }

        // PUT: api/Anuncios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnuncio(int id, Anuncio anuncio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != anuncio.ID)
            {
                return BadRequest();
            }

            db.Entry(anuncio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnuncioExists(id))
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

        // POST: api/Anuncios
        [ResponseType(typeof(Anuncio))]
        public IHttpActionResult PostAnuncio(Anuncio anuncio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Anuncios.Add(anuncio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = anuncio.ID }, anuncio);
        }

        // DELETE: api/Anuncios/5
        [ResponseType(typeof(Anuncio))]
        public IHttpActionResult DeleteAnuncio(int id)
        {
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return NotFound();
            }

            db.Anuncios.Remove(anuncio);
            db.SaveChanges();

            return Ok(anuncio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnuncioExists(int id)
        {
            return db.Anuncios.Count(e => e.ID == id) > 0;
        }
    }
}