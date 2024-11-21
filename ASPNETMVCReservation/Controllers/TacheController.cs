using ASPNETMVCReservation.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVCReservation.Controllers
{
    public class TacheController : Controller
    {
        public static DbEntities _context = new DbEntities();
        // public static 
        public ActionResult Index()
        {
            string USERID = User.Identity.GetUserId();

            SqlParameter SqlparamUserID = new SqlParameter("@ParamUitlisateurID", USERID);

            var results = _context.Database.SqlQuery<Tach>("exec SelAfficherTacheParUtilisateur @ParamUitlisateurID", SqlparamUserID
                );

            return View(results);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var tache = _context.Taches.Where(r=>r.TacheID==id).First();

            return View(tache);
        }

        [HttpPost]  
        public ActionResult Edit(Tach tache)
        {
           var tacheActuel = _context.Taches.Where(r=>r.TacheID == tache.TacheID).First();
            tacheActuel.Titre = tache.Titre;
            tacheActuel.DateEcheance = tache.DateEcheance;  
            tacheActuel.Statut = tache.Statut;  
            tacheActuel.Priorite = tache.Priorite;  
            tacheActuel.DateFinReelle = tache.DateFinReelle;

            _context.SaveChanges();

            return RedirectToAction("Index");   
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Users = _context.AspNetUsers.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tach newTask)
        {

            newTask.DateFinReelle = null;
            newTask.DateCreation = DateTime.Now;
            newTask.Statut = "En cours ...";
            _context.Taches.Add(newTask);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var taches_a_supprimer = _context.Taches.Where(r => r.TacheID == id).First();

            if (taches_a_supprimer != null)
            {
                _context.Taches.Remove(taches_a_supprimer);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}