using ASPNETMVCReservation.Models;
using ASPNETMVCReservation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVCReservation.Controllers
{
    public class CollaborationController : Controller
    {
        public static DbEntities _context = new DbEntities();
        public ActionResult Index()
        {
            ViewBag.Users = _context.AspNetUsers.ToList();
            ViewBag.Taches = _context.Taches.ToList();

            var assignations = _context.TacheAssignations.ToList();

            List<Assignement> assignements = new List<Assignement>();

            foreach (var assignment in assignations)
            {
                // Obtenir le titre de la tâche
                string Desc = _context.Taches
                                      .Where(r => r.TacheID == assignment.Tache)
                                      .Select(r => r.Titre)
                                      .FirstOrDefault();

                // Obtenir le nom d'utilisateur
                string UserName = _context.AspNetUsers
                                          .Where(r => r.Id == assignment.Collaborateur)
                                          .Select(r => r.UserName)
                                          .FirstOrDefault();

                // Ajouter l'assignation à la liste
                assignements.Add(new Assignement
                {
                    Assignation = assignment.TacheAssignationID,
                    Tachedesc = Desc,
                    Collaborateur = UserName
                });
            }


            ViewBag.Assignation = assignements;



            return View();
        }

        [HttpPost]
        public ActionResult Assigner(TacheAssignation asign)
        {
            _context.TacheAssignations.Add(asign);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}