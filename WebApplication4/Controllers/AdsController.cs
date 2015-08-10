using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MisAnuncios.Models;

namespace MisAnuncios.Controllers
{
    public class AdsController : Controller
    {
        private static bool created = false;
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult SearchingAdsByCity()
        {
            return View();
        }

        // GET: Ads
        public ActionResult Index(string SearchCityString, string adCategory)
        {
            ViewBag.Message = "";

            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            if (user != null)
            {
                string city = user.City;
                ViewBag.city = city;
            }
            var CatLst = new List<string>();

            var CatQry = from d in db.Anuncios
                           orderby d.Cat.Nombre
                           select d.Cat.Nombre;

            CatLst.AddRange(CatQry.Distinct());
            ViewBag.adCategory = new SelectList(CatLst);

            var anuncios = from m in db.Anuncios
                           select m;

            anuncios = db.Anuncios.Include(p => p.Usuario);
            anuncios.Include(t => t.Cat);

            if (!String.IsNullOrEmpty(SearchCityString))
            {
                anuncios = anuncios.Where(s => s.Ciudad.Equals(SearchCityString));
            }

            if (!String.IsNullOrEmpty(adCategory))
            {
                anuncios = anuncios.Where(x => x.Cat.Nombre.Equals(adCategory));
            }

            if (created==true)
            {
                ViewBag.Message="Tu anuncio se ha creado correctamente";
                created = false;
            }

            if (anuncios.Count() == 0)
            {
                ViewBag.Message = "No se han encontrado resultados";
                anuncios = from m in db.Anuncios
                           select m;
            }
            return View(anuncios);
        }

        // GET: Ads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // GET: Ads/Create
        [Authorize]
        public ActionResult Create()
        {
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            string city = user.City;
            ViewBag.city = city;

            var CatLst = new List<string>();

            var CatQry = from d in db.Categorias
                         orderby d.Nombre
                         select d.Nombre;

            CatLst.AddRange(CatQry.Distinct());
            ViewBag.adCategory = new SelectList(db.Categorias, "ID", "Nombre");

            return View();
        }

        // POST: Ads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Titulo, Contenido, Cat_ID, Usuario_Id")] Anuncio anuncio, int selectedCategoryId)
        {

            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
            string city = user.City;
            anuncio.Usuario = user;
            anuncio.Ciudad = city;
            Categoria cat = db.Categorias.Find(selectedCategoryId);
            anuncio.Cat = cat;

            if (ModelState.IsValid)
            {
                db.Anuncios.Add(anuncio);
                db.SaveChanges();
                created  = true;
                return RedirectToAction("Index");
            }

            ViewBag.adCategory = new SelectList(db.Categorias, "ID", "Nombre", anuncio.Cat.ID);

            return View(anuncio);
        }

        // GET: Ads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // POST: Ads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDusuario,Ciudad,Titulo,Contenido,IDcategoria")] Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anuncio);
        }

        // GET: Ads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anuncio anuncio = db.Anuncios.Find(id);
            db.Anuncios.Remove(anuncio);
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
