using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using dcuFinal.Models;

namespace dcuFinal.Controllers
{
    public class AsignacionesController : Controller
    {
        private finaldcuEntities db = new finaldcuEntities();

        // GET: Asignaciones
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            int uid = (int)Session["UserID"];
            var asignacion = db.asignacion.Where(a => a.Materia.Usuario_ID == uid).ToList();
            return View(asignacion.ToList());
        }

        //[HttpPost]
        //public ActionResult ver(string nombre)
        //{
        //    if (Session["UserID"] == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        //    }
        //    ViewBag.nombre = new SelectList(db.Materia, "nombre", "nombre");
        //    var asignacion = db.asignacion.Include(p => p.Materia);
        //    if (!String.IsNullOrEmpty(nombre))
        //    {
        //        int uid = (int)Session["UserID"];
        //        asignacion = db.asignacion.Where(a => a.Materia.Usuario_ID == uid && a.Materia.nombre == nombre);
        //    }
            
        //    return View(asignacion.ToList());
        //}

        public ActionResult verAsignaciones(int? mid)
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if(mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int uid = (int)Session["UserID"];
            var asignacion = db.asignacion.Where(a => a.Materia.Usuario_ID == uid && a.Materia_ID == mid).ToList();
            ViewBag.nombreMateria = db.Materia.Where(m => m.id == mid).FirstOrDefault().nombre;
            return View("Index",asignacion);
        }

        // GET: Asignaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int uid = (int)Session["UserID"];
            asignacion asignacion = db.asignacion.Where(a => a.ID == id && a.Materia.Usuario_ID == uid).FirstOrDefault();
            if (asignacion == null)
            {
                return HttpNotFound();
            }
            return View(asignacion);
        }

        // GET: Asignaciones/Create
        public ActionResult Create()
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            int uid = (int)Session["UserID"];
            List<SelectListItem> lst = new List<SelectListItem>();

            List<Materia> m = db.Materia.Where(d => d.Usuario_ID == uid).ToList();

            foreach (var s in m)
            {
                lst.Add(new SelectListItem() { Text = s.nombre, Value = s.id.ToString() });
            }
            ViewBag.materias = lst;
            return View();
        }

        // POST: Asignaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Materia_ID,fecha,nombre,grado,año,competencia,estrategia,recursos,evaluacion")] asignacion asignacion)
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (ModelState.IsValid)
            {
                db.asignacion.Add(asignacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            int uid = (int)Session["UserID"];
            List<SelectListItem> lst = new List<SelectListItem>();

            List<Materia> m = db.Materia.Where(d => d.Usuario_ID == uid).ToList();

            foreach (var s in m)
            {
                lst.Add(new SelectListItem() { Text = s.nombre, Value = s.id.ToString() });
            }
            ViewBag.materias = lst;
            return View(asignacion);
        }

        // GET: Asignaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            int uid = (int)Session["UserID"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asignacion asignacion = db.asignacion.Where(a => a.ID == id && a.Materia.Usuario_ID == uid).FirstOrDefault();
            if (asignacion == null)
            {
                return HttpNotFound();
            }
            
            List<SelectListItem> lst = new List<SelectListItem>();

            List<Materia> m = db.Materia.Where(d => d.Usuario_ID == uid).ToList();

            foreach (var s in m)
            {
                lst.Add(new SelectListItem() { Text = s.nombre, Value = s.id.ToString() });
            }
            ViewBag.materias = lst;
            return View(asignacion);
        }

        // POST: Asignaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Materia_ID,fecha,nombre,grado,año,competencia,estrategia,recursos,evaluacion")] asignacion asignacion)
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (ModelState.IsValid)
            {
                db.Entry(asignacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int uid = (int)Session["UserID"];
            List<SelectListItem> lst = new List<SelectListItem>();

            List<Materia> m = db.Materia.Where(d => d.Usuario_ID == uid).ToList();

            foreach (var s in m)
            {
                lst.Add(new SelectListItem() { Text = s.nombre, Value = s.id.ToString() });
            }
            ViewBag.materias = lst;
            return View(asignacion);
        }

        // GET: Asignaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int uid = (int)Session["UserID"];
            asignacion asignacion = db.asignacion.Where(a => a.ID == id && a.Materia.Usuario_ID == uid).FirstOrDefault();
            if (asignacion == null)
            {
                return HttpNotFound();
            }
            return View(asignacion);
        }

        // POST: Asignaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            int uid = (int)Session["UserID"];
            asignacion asignacion = db.asignacion.Where(a => a.ID == id && a.Materia.Usuario_ID == uid).FirstOrDefault();
            db.asignacion.Remove(asignacion);
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
