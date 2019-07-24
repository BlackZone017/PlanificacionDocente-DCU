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
    public class MateriasController : Controller
    {
        private finaldcuEntities db = new finaldcuEntities();

        // GET: Materias
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                int id = (int)Session["UserID"];
                var materia = db.Materia.Where(m => m.Usuario_ID == id);

                return View(materia.ToList());
            }
            ViewBag.Error = "No se ha iniciado seccion";
            return View();
        }

        // GET: Materias/Details/5
        public ActionResult Details(int? id)
        {
                if (Session["UserID"] != null)
                {
                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                int uid = (int)Session["UserID"];
                Materia materia = db.Materia.Where(m => m.Usuario_ID == uid && m.id == id).FirstOrDefault(); ;
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
            }
            ViewBag.Error = "No se ha iniciado seccion";
            return View("Index");
        }

        // GET: Materias/Create
        public ActionResult Create()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.Usuario_ID = new SelectList(db.Usuarios, "ID", "email");
                return View();
            }
            ViewBag.Error = "No se ha iniciado seccion";
            return View("Index");
        }

        // POST: Materias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre")] Materia materia)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
            {
                materia.Usuario_ID = (int)Session["UserID"];
                db.Materia.Add(materia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Usuario_ID = new SelectList(db.Usuarios, "ID", "email", materia.Usuario_ID);
            return View(materia);
            }
            ViewBag.Error = "No se ha iniciado seccion";
            return View("Index");
        }

        // GET: Materias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int uid = (int)Session["UserID"];
                Materia materia = db.Materia.Where(m=> m.Usuario_ID == uid && m.id == id).FirstOrDefault();
                if (materia == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Usuario_ID = new SelectList(db.Usuarios, "ID", "email", materia.Usuario_ID);
                return View(materia);
                }
            ViewBag.Error = "No se ha iniciado seccion";
            return View("Index");
        }

        // POST: Materias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre")] Materia materia)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
            {
                    materia.Usuario_ID = (int)Session["UserID"];
                db.Entry(materia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Usuario_ID = new SelectList(db.Usuarios, "ID", "email", materia.Usuario_ID);
            return View(materia);
            }
            ViewBag.Error = "No se ha iniciado seccion";
            return View("Index");
        }

        // GET: Materias/Delete/5
        public ActionResult Delete(int? id)
        {
                if (Session["UserID"] != null)
                {
                    if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                int uid = (int)Session["UserID"];
                Materia materia = db.Materia.Where(m => m.Usuario_ID == uid && m.id == id).FirstOrDefault();
                if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
            }
            ViewBag.Error = "No se ha iniciado seccion";
            return View("Index");
        }

        // POST: Materias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] != null)
            {
                int uid = (int)Session["UserID"];

                var asignacion = db.asignacion.Where(m => m.Materia_ID == id);
                foreach(var item in asignacion)
                {
                    db.asignacion.Remove(item);
                }
         
                Materia materia = db.Materia.Where(m => m.Usuario_ID == uid && m.id == id).FirstOrDefault();
                db.Materia.Remove(materia);
  
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            ViewBag.Error = "No se ha iniciado seccion";
            return View("Index");
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
