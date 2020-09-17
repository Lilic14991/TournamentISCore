using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentsIS.Models;

namespace TournamentsIS.Repository
{
    public interface IClubRepo
    {
        IEnumerable<Clubs> Index();
        IEnumerable<Tournaments> TournamentList();
        IEnumerable<Groups> GroupList();
        Clubs Create(Clubs clubs);

        Clubs FindElementById(int? ClubsID);
        Clubs Edit(Clubs clubs);
        Clubs Delete(int? ClubsID);
        Clubs DeleteConfirmed(Clubs clubs);
        Clubs Details(int? ClubsID);


        bool ClubsExists(string cName);
    }
}
