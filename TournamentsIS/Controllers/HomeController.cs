using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TournamentsIS.Models;
using TournamentsIS.ViewModels;

namespace TournamentsIS.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private TournamentDBContext _context;

        public HomeController(TournamentDBContext context)
        {
            _context = context;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            var clubs = _context.Clubs.ToList();

            var groups = _context.Groups.ToList();
            var tournaments = _context.Tournaments.ToList();

            var viewModel = new TournamentVewModel()
            {
                Clubs = clubs,
                Groups = groups,
                Tournaments = tournaments
            };


            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
