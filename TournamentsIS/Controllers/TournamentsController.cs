using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TournamentsIS.Models;
using TournamentsIS.Repository;

namespace TournamentsIS.Controllers
{
    public class TournamentsController : Controller
    {
        
        private ITourRepo _repo;

        public TournamentsController(ITourRepo repo)
        {
            _repo = repo;
        }

        // GET: Tournaments
        public IActionResult Index()
        {
            var tournaments = _repo.Index();

            return View(tournaments);
        }

        // GET: Tournaments/Details/5
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }
            var tournaments = _repo.Details(id);

            if (tournaments == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }
            return View(tournaments);
        }
            


        // GET: Tournaments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TournamentId,TournamentName,StartDate,EndDate,TournamentLocation")] Tournaments tournaments)
        {
            if(string.IsNullOrWhiteSpace(tournaments.TournamentName))
            {
                ModelState.AddModelError("TournamentName", "Enter tournament name");
                
            }
            if (string.IsNullOrWhiteSpace(tournaments.TournamentLocation))
            {
                ModelState.AddModelError("TournamentLocation", "Enter tournament location");
            }

            if(_repo.TournamentsExists(tournaments.TournamentName))
            {
                ModelState.AddModelError("TournamentName", "Tournament with that name already exists!");
            }

            if (ModelState.IsValid)
            {
                _repo.Create(tournaments);
                
                return RedirectToAction(nameof(Index));
            }
            return View(tournaments);
        }

        // GET: Tournaments/Edit/5
        public ViewResult Edit(int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }
            var tournaments = _repo.FindElementById(id);
            
            if (tournaments == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }
            return View(tournaments);
        }


        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("TournamentId,TournamentName,StartDate,EndDate,TournamentLocation")] Tournaments tournaments)
        {
            if(string.IsNullOrWhiteSpace(tournaments.TournamentName))
            {
                ModelState.AddModelError("TournamentName", "Enter new tournament name");
            }
            if(string.IsNullOrWhiteSpace(tournaments.TournamentLocation))
            {
                ModelState.AddModelError("TournamentLocation", "Enter new tournament location");
            }


            
            if (ModelState.IsValid)
            {
                _repo.Edit(tournaments);

                return RedirectToAction(nameof(Index));
            }
            return View(tournaments);

            
        }















        // GET: Tournaments/Delete/5
        public ViewResult Delete(int? id)
        {
            if (id == null)
            {
                return View("~/View/Shared/NotFoundErrorView.cshtml");
            }
            var tournaments = _repo.Delete(id);

            if (tournaments == null)
            {
                return View("~/View/Shared/NotFoundErrorView.cshtml");
            }
            return View(tournaments);

            

        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Tournaments tournaments)
        {
            _repo.DeleteConfirmed(tournaments);
            
            return RedirectToAction(nameof(Index));
        }

        
    }
}
