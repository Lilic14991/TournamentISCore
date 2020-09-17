using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentsIS.Models;
using TournamentsIS.ViewModels;

namespace TournamentsIS.Repository
{
    public class ClubRepo : IClubRepo
    {
        private TournamentDBContext _context;

        public ClubRepo(TournamentDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Clubs> Index()
        {
            var clubs = _context.Clubs
                .Include(c => c.Group)
                .Include(t => t.Tournament).ToList();
            return clubs;
        }


        public Clubs Create(Clubs clubs)
        {
            _context.Add(clubs);
            _context.SaveChanges();

            return clubs;
        }


        public Clubs Delete(int? ClubsID)
        {
            var clubs = _context.Clubs
                .Include(c => c.Group)
                .Include(t => t.Tournament)
                .FirstOrDefault(m => m.ClubId == ClubsID);

            return clubs;
        }

        public Clubs DeleteConfirmed(Clubs clubs)
        {
            var club = _context.Clubs.Find(clubs.ClubId);
            _context.Clubs.Remove(club);
            _context.SaveChanges();

            return clubs;
        }

        public Clubs Details(int? ClubsID)
        {
            var clubs = _context.Clubs
                .Include(c => c.Group)
                .Include(t => t.Tournament)
                .FirstOrDefault(m => m.ClubId == ClubsID);

            
            return clubs;
        }


        public IEnumerable<Tournaments> TournamentList()
        {
            return _context.Tournaments.ToList();
        }



        public Clubs Edit(Clubs clubs)
        {
            _context.Update(clubs);
            _context.SaveChanges();

            return clubs;
        }

        public Clubs FindElementById(int? ClubsID)
        {
            var clubs =_context.Clubs.Find(ClubsID);

            return clubs;
        }

        public IEnumerable<Groups> GroupList()
        {
            return _context.Groups.ToList();
        }

        public bool ClubsExists(string cName)
        {
            return _context.Clubs.Any(c => c.ClubName == cName);
        }

        
    }
}
