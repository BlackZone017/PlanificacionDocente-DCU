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
    public class HomeController : Controller
    {
        private finaldcuEntities db = new finaldcuEntities();
        public ActionResult Index()
        {
            if(Session["UserID"] != null)
            {
                int uid = (int)Session["UserID"];
                return View(db.Materia.Where(m => m.Usuario_ID == uid).ToList());
            }
            return View();
        }

        [HttpPost]
        public ActionResult login(Usuarios login)
        {
            if (db.Usuarios.Where(n => n.email == login.email && n.password == login.password).ToList().Count > 0)
            {
                Usuarios s = db.Usuarios.Where(n => n.email == login.email).First();
                Session["UserEmail"] = s.email;
                Session["UserID"] = s.ID;
                return RedirectToAction("Index");
            }else
            {
                ViewBag.Error = "Usuario y/o Contraseña Incorrecta";
            }  
            return View();
        }


        
        public ActionResult login()
        {
            return View();
        }

        public ActionResult logout()
        {
            
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("login","Home");
        }

        public ActionResult registrarse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult registrarse([Bind(Include = "email, password")] Usuarios reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Usuarios.Add(reg);
                    db.SaveChanges();

                    ViewBag.notificacion = "Usuario Registrado";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.notificacion = "El usuario ya existe";
            }
            //todo
            
            return View();
        }
    }
}