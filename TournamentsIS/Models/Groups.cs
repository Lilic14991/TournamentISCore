using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TournamentsIS.Models
{
    public partial class Groups
    {
        public Groups()
        {
            Clubs = new HashSet<Clubs>();
        }

        public int GroupId { get; set; }
        [Display(Name ="Group Name")]
        public string GroupName { get; set; }
        [Display(Name = "Group Length")]
        public int GroupLength { get; set; }

        public virtual ICollection<Clubs> Clubs { get; set; }
    }
}
