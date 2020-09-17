using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentsIS.Models;

namespace TournamentsIS.Repository
{
    public interface IGroupRepo
    {
        IEnumerable<Groups> Index();

        
        Groups Create(Groups groups);

        Groups FindElementById(int? GroupsID);
        Groups Edit(Groups groups);
        Groups Delete(int? GroupsID);
        Groups DeleteConfirmed(Groups groups);
        Groups Details(int? GroupsID);


        bool GroupsExists(string gName);
    }
}
