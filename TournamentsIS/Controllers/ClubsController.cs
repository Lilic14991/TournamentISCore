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
    public class ClubsController : Controller
    {
        private IClubRepo _repo;



        public ClubsController(IClubRepo repo)
        {
            _repo = repo;
        }



        // GET: Clubs
        public IActionResult Index()
        {
            var clubs = _repo.Index();

            return View(clubs);
        }

        //GET: Clubs/Details/5
        public ViewResult Details(int? id)
        {
            var clubs = _repo.Details(id);
           
            if (id == null || clubs == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }
            return View(clubs);
        }





        //GET: Clubs/Create
        public IActionResult Create()
        {
            ViewData["TournamentId"] = new SelectList(_repo.TournamentList(), "TournamentId", "TournamentName");
            ViewData["GroupId"] = new SelectList(_repo.GroupList(), "GroupId", "GroupName");

            return View();
        }

        //POST: Clubs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ClubId,ClubName,ClubAddress,ClubBudget,ClubDesc,GroupId,TournamentId")] Clubs clubs)
        {


            if (_repo.ClubsExists(clubs.ClubName))
            {
                ModelState.AddModelError("ClubName", "Club with that name already exists");
            }
            if (string.IsNullOrWhiteSpace(clubs.ClubName))
            {
                ModelState.AddModelError("ClubName", "Enter club name");
            }
            if (string.IsNullOrWhiteSpace(clubs.ClubAddress))
            {
                ModelState.AddModelError("ClubAddress", "Enter club address");
            }
            if (string.IsNullOrWhiteSpace(clubs.ClubBudget.ToString()))
            {
                ModelState.AddModelError("ClubBudget", "Enter club's money");
            }




            if (ModelState.IsValid)
            {
                _repo.Create(clubs);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TournamentId"] = new SelectList(_repo.TournamentList(), "TournamentId", "TournamentName");
            ViewData["GroupId"] = new SelectList(_repo.GroupList(), "GroupId", "GroupName", clubs.GroupId);
            return View(clubs);
        }

        //GET: Clubs/Edit/5
        public ViewResult Edit(int? id)
        {
            var clubs = _repo.FindElementById(id);
            
            if (id == null || clubs == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }

            ViewData["GroupId"] = new SelectList(_repo.GroupList(), "GroupId", "GroupName", clubs.GroupId);
            ViewData["TournamentId"] = new SelectList(_repo.TournamentList(), "TournamentId", "TournamentName");
            return View(clubs);
        }
           





        //POST: Clubs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ClubId,ClubName,ClubAddress,ClubBudget,ClubDesc,GroupId,TournamentId")] Clubs clubs)
        {
            if (string.IsNullOrWhiteSpace(clubs.ClubName))
            {
                ModelState.AddModelError("ClubName", "Enter club name");
            }
            if (string.IsNullOrWhiteSpace(clubs.ClubAddress))
            {
                ModelState.AddModelError("ClubAddress", "Enter club address");
            }
            if (string.IsNullOrWhiteSpace(clubs.ClubAddress))
            {
                ModelState.AddModelError("ClubBudget", "Enter club's money");
            }

            if (ModelState.IsValid)
            {
                _repo.Edit(clubs);

                return RedirectToAction(nameof(Index));
            }

            ViewData["GroupId"] = new SelectList(_repo.GroupList(), "GroupId", "GroupName", clubs.GroupId);
            ViewData["TournamentId"] = new SelectList(_repo.TournamentList(), "TournamentId", "TournamentName");
            return View(clubs);
        }
        //GET: Clubs/Delete/5
        public ViewResult Delete(int? id)
        {
            var clubs = _repo.Delete(id);
            
            if (id == null || clubs == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }

            return View(clubs);
        }
         



        //POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Clubs clubs)
        {
            _repo.DeleteConfirmed(clubs);

            return RedirectToAction(nameof(Index));
        }













    }
}
