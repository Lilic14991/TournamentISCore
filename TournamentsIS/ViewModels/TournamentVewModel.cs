using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentsIS.Models;

namespace TournamentsIS.ViewModels
{
    public class TournamentVewModel
    {
        public IEnumerable<Tournaments> Tournaments { get; set; }
        public IEnumerable<Clubs> Clubs { get; set; }
        public IEnumerable<Groups> Groups { get; set; }


    }




}
