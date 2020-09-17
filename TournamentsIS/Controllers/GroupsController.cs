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
    public class GroupsController : Controller
    {
        
        private IGroupRepo _repo;

        public GroupsController(IGroupRepo repo)
        {
            _repo = repo;
        }


        // GET: Groups
        public IActionResult Index()
        {
            var groups = _repo.Index();

            return View(groups);
        }

        // GET: Groups/Details/5
        public ViewResult Details(int? id)
        {
            var groups = _repo.Details(id);

            if (id == null || groups == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }
           
            return View(groups);
        }
            


        // GET: Groups/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GroupId,GroupName,GroupLength")] Groups groups)
        {
            if(string.IsNullOrWhiteSpace(groups.GroupName))
            {
                ModelState.AddModelError("GroupName", "Enter group name");
            }
            
            if(_repo.GroupsExists(groups.GroupName))
            {
                ModelState.AddModelError("GroupName", "Group already exists!");
            }

            if (ModelState.IsValid)
            {
                _repo.Create(groups);

                return RedirectToAction(nameof(Index));
            }
            
            return View(groups);
        }

        // GET: Groups/Edit/5
        public ViewResult Edit(int? id)
        {
            var groups = _repo.FindElementById(id);

            if (id == null || groups == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }

            return View(groups);
        }

            

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("GroupId,GroupName,GroupLength")] Groups groups)
        {
            if(string.IsNullOrWhiteSpace(groups.GroupName))
            {
                ModelState.AddModelError("GroupName", "Enter group name");
            }
            
            
            if (ModelState.IsValid)
            {
                _repo.Edit(groups);
                
                return RedirectToAction(nameof(Index));
            }
           
            return View(groups);
        }





        // GET: Groups/Delete/5
        public ViewResult Delete(int? id)
        {
            var groups = _repo.Delete(id);
            if (id == null || groups == null)
            {
                return View("~/Views/Shared/NotFoundErrorView.cshtml");
            }

            return View(groups);
            
        }



        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Groups groups)
        {
            _repo.DeleteConfirmed(groups);

            return RedirectToAction(nameof(Index));
        }


    }
}
