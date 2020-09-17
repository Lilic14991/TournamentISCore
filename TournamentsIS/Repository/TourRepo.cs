using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentsIS.Models;

namespace TournamentsIS.Repository
{
    public class TourRepo : ITourRepo
    {
        private TournamentDBContext _context;

        public TourRepo(TournamentDBContext context)
        {
            _context = context;
        }

        public Tournaments Create(Tournaments tournament)
        {
            _context.Add(tournament);
            _context.SaveChanges();
            return tournament;
        }

        public Tournaments Delete(int? TournamentID)
        {
            var tournaments = _context.Tournaments
                .FirstOrDefault(m => m.TournamentId == TournamentID);

            return tournaments;
        }

        public Tournaments DeleteConfirmed(Tournaments tournaments)
        {
            var tournament = _context.Tournaments.Find(tournaments.TournamentId);
            _context.Tournaments.Remove(tournament);
            _context.SaveChanges();

            return tournament;
        }

        public Tournaments Details(int? TournamentID)
        {
            var tournaments = _context.Tournaments
                .FirstOrDefault(m => m.TournamentId == TournamentID);

            return tournaments;
        }

        public Tournaments Edit(Tournaments tournament)
        {
            _context.Update(tournament);
            _context.SaveChanges();

            return tournament;
        }

        public Tournaments FindElementById(int? TournamentID)
        {
            var tournaments = _context.Tournaments.Find(TournamentID);

            return tournaments;
        }

        public IEnumerable<Tournaments> Index()
        {
            var tournament = _context.Tournaments.ToList();

            return tournament;
        }

        public bool TournamentsExists(string id)
        {
            return _context.Tournaments.Any(e => e.TournamentName == id);
        }
    }
}
