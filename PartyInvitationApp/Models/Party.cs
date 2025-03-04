using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyInvitationApp.Models
{
    public class Party
    {
        [Key]
        public int PartyId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Location { get; set; }

        public List<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
