using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentsIS.Models
{
    public partial class Tournaments
    {
        public Tournaments()
        {
            Clubs = new HashSet<Clubs>();
        }

        public int TournamentId { get; set; }
        [Display(Name = "Tournament Name")]
        public string TournamentName { get; set; }
        [DataType(DataType.Date),Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), Display(Name = "Start Date")] 
        public DateTime EndDate { get; set; }
        [Display(Name = "Tournament Location")]
        public string TournamentLocation { get; set; }

        public virtual ICollection<Clubs> Clubs { get; set; }
    }
}
