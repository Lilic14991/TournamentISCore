using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentsIS.Models;

namespace TournamentsIS.Repository
{
    public interface ITourRepo
    {
        IEnumerable<Tournaments> Index();

        Tournaments Create(Tournaments tournament);
        
        Tournaments FindElementById(int? TournamentID);
        Tournaments Edit(Tournaments tournament);
        Tournaments Delete(int? TournamentID);
        Tournaments DeleteConfirmed(Tournaments tournaments);
        Tournaments Details(int? TournamentID);

        bool TournamentsExists(string id);
    }
}
