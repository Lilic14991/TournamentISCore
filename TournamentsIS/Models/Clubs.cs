using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentsIS.Models
{
    public partial class Clubs
    {
        public int ClubId { get; set; }
        [Display(Name = "Club Name")]
        public string ClubName { get; set; }
        [Display(Name = "Club Address")]
        public string ClubAddress { get; set; }
        [Display(Name = "Club Budget")]
        public double ClubBudget { get; set; }
        [Display(Name = "Club Description")]
        public string ClubDesc { get; set; }
        [Display(Name = "Group")]
        public int GroupId { get; set; }
        [Display(Name = "Tournament")]
        public int TournamentId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Tournaments Tournament { get; set; }
    }
}
