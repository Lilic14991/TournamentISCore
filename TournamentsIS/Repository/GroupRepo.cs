using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TournamentsIS.Models;

namespace TournamentsIS.Repository
{
    public class GroupRepo : IGroupRepo
    {
        private TournamentDBContext _context;

        public GroupRepo(TournamentDBContext context)
        {
            _context = context;
        }

        public Groups Create(Groups groups)
        {
            _context.Add(groups);
            _context.SaveChanges();

            return groups;
        }

        public Groups Delete(int? GroupsID)
        {
            var groups = _context.Groups
                .FirstOrDefault(m => m.GroupId == GroupsID);

            return groups;
        }

        public Groups DeleteConfirmed(Groups groups)
        {
            var group = _context.Groups.Find(groups.GroupId);
            _context.Groups.Remove(group);
            _context.SaveChanges();

            return groups;
        }

        public Groups Details(int? GroupsID)
        {
            var groups = _context.Groups
                .FirstOrDefault(m => m.GroupId == GroupsID);

            return groups;
        }



        public Groups Edit(Groups groups)
        {
            _context.Update(groups);
            _context.SaveChanges();

            return groups;
        }

        public Groups FindElementById(int? GroupsID)
        {
            var groups = _context.Groups.Find(GroupsID);

            return groups;
        }

        public bool GroupsExists(string gName)
        {
            return _context.Groups.Any(g => g.GroupName == gName);
        }

        public IEnumerable<Groups> Index()
        {
            var groups = _context.Groups.ToList();

            return groups;
        }
    }
}
